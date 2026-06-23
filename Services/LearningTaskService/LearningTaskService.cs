using Microsoft.EntityFrameworkCore;
using trainee_management.Database;
using trainee_management.Enums;
using trainee_management.Exceptions;
using trainee_management.Models;
using trainee_management.Models.DTOs;
namespace trainee_management.Services;

public class LearningTaskService:ILearningTaskService
{
    private readonly AppDBContext _context;
    public LearningTaskService(AppDBContext context)
    {
        _context = context;
    }


    public async  Task CreateLearningTask(LearningTaskRequest request)
    {
            LearningTask task=new LearningTask(request);
            await _context.LearningTask.AddAsync(task);
            await _context.SaveChangesAsync();
    }

    public async Task<LearningTaskResponse> GetLearningTaskById(int id)
    {
        LearningTask task = await _context.LearningTask.FindAsync(id)
        ?? throw new NotFoundException("task with this id doesn't exist");
        LearningTaskResponse response =new LearningTaskResponse(task);
        return response;
    }

    public async Task DeleteLearningTask(int id)
    {
        LearningTask task = await _context.LearningTask.FindAsync(id) 
        ?? throw new NotFoundException("task with this id doesn't exist");
        _context.LearningTask.Remove(task);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTask(int id,LearningTaskRequest request)
    {
        LearningTask task = await _context.LearningTask.FindAsync(id) 
        ?? throw new NotFoundException("task with this id doesn't exist");
        task.Title=request.Title;
        task.Description=request.Description;
        task.DueDate=request.DueDate;
        task.ExpectedTechStack=request.ExpectedTechStack;
        task.updatedAt=DateTime.Now;
        await _context.SaveChangesAsync();
    }
    
     public async Task<GetAllDTO<LearningTaskResponse>> GetLearningTasks(string searchParams, string status, int pageNumber, int pageSize)
    {
            List<LearningTask> learningTasks=[];
            if(Enum.TryParse(status,true,out LearningTaskStatus result))
            {
                learningTasks=_context.LearningTask.FromSqlInterpolated($"CALL GetLearningTasks({searchParams},{(int)result},{pageNumber},{pageSize})").ToList();
            }
            else
            {
                throw new ValidationException("Invalid Status");
            }
        List<LearningTaskResponse> learningTaskList =  learningTasks.Select(m => new LearningTaskResponse(m)).ToList();
        GetAllDTO<LearningTaskResponse> response = new GetAllDTO<LearningTaskResponse>
        {
            pageNumber = pageNumber,
            pageSize = pageSize,
            totalCount = learningTaskList.Count,
            data = learningTaskList
        };
        return response;
    }
}