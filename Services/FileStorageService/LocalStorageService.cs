
using Microsoft.EntityFrameworkCore;
using trainee_management.Database;
using trainee_management.Enums;
using trainee_management.Exceptions;
using trainee_management.Models.DTOs;
using trainee_management.Models.Entities;
using System.Security.Claims;

namespace trainee_management.Services;
public class LocalStorageService : IFileStorageSerivce
{
   private readonly AppDBContext _context;
   private readonly string _storage_path;
   private readonly IRabbitMQPublisher  _publisher;
   private readonly IHttpContextAccessor _httpContextAccessor;
   private readonly IConfiguration _configuration;

   private readonly ICacheService _cache_service;

   public LocalStorageService(AppDBContext context,IRabbitMQPublisher publisher,IConfiguration configuration,ICacheService cache_service)
   {
    _context=context;
    _storage_path=Environment.GetEnvironmentVariable("LocalStorage")!;
    _publisher=publisher;
    _configuration=configuration;
    _cache_service=cache_service;
   }

    public async Task<SubmissionFilesResponse> SaveAsync(IFormFile file,int userId,int submissionId)
    {
        FileValidator.validateFile(file);
         if (_httpContextAccessor.HttpContext!=null)
        {
           bool isTrainee= _httpContextAccessor.HttpContext.User.IsInRole("TRAINEE");  
           if(!isTrainee) throw new NotFoundException("Forbidden, User with role admin/mentor cannot perform this operation"); 
        }
        else
        {
            throw new ForbidenException("Forbidden");
        }
        var submission = await _context.Submission
            .Include(s => s.TaskAssignment)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == submissionId);

        if (submission == null || submission.TaskAssignment == null)
            throw new ValidationException("Invalid Submission Id");

        int TraineeId=submission.TaskAssignment.TraineeId ;
        Trainee trainee=await _context.Trainee.FindAsync(TraineeId) ?? 
        throw new NotFoundException("Trainee not found !");

        string currentUserEmail=_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)!;
        if (!trainee.Email.Equals(currentUserEmail))
        {
            throw new UnauthorizedAccessException("You are not allowed to upload files to this submission.");
        }

        string originalFileName=file.FileName;
        string fileName=Guid.NewGuid().ToString()+Path.GetExtension(originalFileName);
        string filePath=Path.Combine(_storage_path+fileName);
        
        {
        using FileStream stream=new FileStream(filePath,FileMode.Create);
        await file.CopyToAsync(stream);
        }

        string checkSum=ChecksumHelper.CalculateSha256(filePath);
        User user=await _context.User.FindAsync(userId) ??
        throw new NotFoundException($"User with id ${userId} not found");
        Metadata data=new Metadata
        {
            ContentType=file.ContentType,
            CheckSum=checkSum,
            Size=(int)file.Length,
            UploadedByUser=userId,
            Timestamp=DateTime.UtcNow,
            OriginalFileName=originalFileName,
            GeneratedStorageName=fileName,
            SubmissionId=submissionId
        };
        await _context.Metadata.AddAsync(data);
        await _context.SaveChangesAsync();
        Guid correlationId=Guid.NewGuid();
        SubmissionProcessingRequested message=new SubmissionProcessingRequested
        {
            FileId=data.Id,
            MessageId=Guid.NewGuid(),
            CorrelationId=correlationId,
            ContractVersion=int.Parse(_configuration["RabbitMQMessage:ContractVersion"]!),
            RequestedAt=DateTime.UtcNow,
            SubmissionId=submissionId
        };
        ProcessingJob job=new ProcessingJob
        {
            Attempts=0,
            Status=JobStatus.QUEUED,
            CorrelationId=correlationId,
        };
        await _cache_service.SetDataAsync($"retry-count-{correlationId}","0");
        await _context.ProcessingJob.AddAsync(job);
        await _context.SaveChangesAsync();
        await _publisher.PublishMessageAsync(message);
        return new SubmissionFilesResponse(data,user.Username);
    }

    public async Task DeleteAsync(int id)
    {
        Metadata data=await _context.Metadata.FindAsync(id) ?? 
        throw new NotFoundException($"Metadata for id {id} not found");
        string FileName=data.GeneratedStorageName;
        string FilePath=Path.Combine(_storage_path,FileName);
        
        if (!File.Exists(FilePath))
        {
            throw new FileNotFoundException($"File not found: {FilePath}");
        }
        else
        {
            File.Delete(FilePath);
            _context.Metadata.Remove(data);
            await _context.SaveChangesAsync();
        }
    }

    public Task<bool> ExistsAsync(string fileName)
    {
        string FilePath=Path.Combine(_storage_path,fileName);
        return Task.FromResult(File.Exists(FilePath)); 
    }

     public async Task<FileStream> OpenReadAsync(int id)
    {
        Metadata data=await _context.Metadata.FindAsync(id) ?? 
        throw new NotFoundException($"Metadata for id {id} not found");
        string FileName=data.GeneratedStorageName;
        string FilePath=Path.Combine(_storage_path,FileName);
        if (!File.Exists(FilePath))
            throw new FileNotFoundException($"File not found: {FilePath}");
        FileStream stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true);
        return stream;
    }


    

}