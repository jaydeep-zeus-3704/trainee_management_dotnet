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


    public async  Task createLearningTask(LearningTaskRequest request)
    {
            LearningTask task=new LearningTask(request);
            await _context.LearningTask.AddAsync(task);
            await _context.SaveChangesAsync();
    }

    public async Task<LearningTaskResponse> getLearningTaskById(int id)
    {
        LearningTask task = await _context.LearningTask.FindAsync(id)
        ?? throw new NotFoundException("task with this id doesn't exist");
        LearningTaskResponse response =new LearningTaskResponse(task);
        return response;
    }

    public async Task deleteLearningTask(int id)
    {
        LearningTask task = await _context.LearningTask.FindAsync(id) 
        ?? throw new NotFoundException("task with this id doesn't exist");
        _context.LearningTask.Remove(task);
        await _context.SaveChangesAsync();
    }

    public async Task updateTask(int id,LearningTaskRequest request)
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
    
     public async Task<GetAllDTO<LearningTaskResponse>> getLearningTasks(string searchParams, string status, int pageNumber, int pageSize)
    {
        IQueryable<LearningTask> learningTasks = _context.LearningTask;
        learningTasks = await filterBySearch(searchParams, status, learningTasks);
        learningTasks = getPaginatedData(pageNumber, pageSize, learningTasks);
        List<LearningTaskResponse> learningTaskList = await learningTasks.Select(m => new LearningTaskResponse(m)).ToListAsync();
        GetAllDTO<LearningTaskResponse> response = new GetAllDTO<LearningTaskResponse>
        {
            pageNumber = pageNumber,
            pageSize = pageSize,
            totalCount = learningTaskList.Count,
            data = learningTaskList
        };
        return response;
    }


    public async Task<IQueryable<LearningTask>> filterBySearch(string searchParams, string status, IQueryable<LearningTask> learningtasks)
    {
        if (!string.IsNullOrWhiteSpace(searchParams))
        {
            searchParams = searchParams.Trim().ToLower();
            learningtasks = learningtasks.Where(t =>
                t.Title.ToLower().Contains(searchParams) ||
                t.Description.ToLower().Contains(searchParams) ||
                t.ExpectedTechStack.ToLower().Contains(searchParams) 
            );
        }

        if (!string.IsNullOrWhiteSpace(status) && Enum.TryParse<LearningTaskStatus>(status, true, out var result))
        {
            learningtasks = learningtasks.Where(t => t.Status == result);
        }
        return learningtasks;
    }

    //pagination
    public IQueryable<LearningTask> getPaginatedData(int pageNumber, int pageSize, IQueryable<LearningTask> learningTasks)
    {
        learningTasks = learningTasks.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        return learningTasks;
    }




}