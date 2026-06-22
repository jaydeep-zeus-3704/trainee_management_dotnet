
using Microsoft.EntityFrameworkCore;
using trainee_management.Database;
using trainee_management.Exceptions;
using trainee_management.Models.DTOs;
using trainee_management.Models.Entities;
using trainee_management.Services;

public class TaskAssignmentService : ITaskAssignmentService
{
    private readonly AppDBContext _context;
    private readonly ICacheService _cache_service;

    public TaskAssignmentService(AppDBContext context, ICacheService cache_service)
    {
        _context = context;
        _cache_service = cache_service;
    }

    public async Task CreateTaskAssignment(TaskAssignmentRequest request)
    {
        bool mentorExists = await _context.Mentor.AsNoTracking().AnyAsync(m => m.Id == request.MentorId);
        if (!mentorExists) throw new NotFoundException("Mentor Id not found");

        bool traineeExists = await _context.Trainee.AsNoTracking().AnyAsync(m => m.Id == request.TraineeId);
        if (!traineeExists) throw new NotFoundException("Trainee Id not found");

        bool learningTaskExists = await _context.LearningTask.AsNoTracking().AnyAsync(m => m.Id == request.LearningTaskId);
        if (!learningTaskExists) throw new NotFoundException("Learning task  Id not found");

        if (request.AssignedDate > request.DueDate)
        {
            throw new ValidationException("DueDate should not be before AssignedDate.");
        }

        TaskAssignment assigned_task = new TaskAssignment(request);
        await _context.TaskAssignment.AddAsync(assigned_task);
        await _context.SaveChangesAsync();
    }

    public async Task<List<TaskAssignmentResponse>> GetTaskAssignments()
    {
        List<TaskAssignmentResponse> task_assignments = await _context.TaskAssignment.Select(t => new TaskAssignmentResponse(t)).ToListAsync();
        return task_assignments;
    }

    public async Task<TaskAssignmentResponse> GetTaskAssignmentById(int id)
    {
        string key = $"task_assignment_{id}";
        TaskAssignment? task = await _cache_service.GetDataAsync<TaskAssignment>(key);
        if (task == null)
        {
            task = await _context.TaskAssignment.FindAsync(id)
            ?? throw new NotFoundException("task not assigned witht this id");
            await _cache_service.SetDataAsync(key, task);
        }
        TaskAssignmentResponse response = new TaskAssignmentResponse(task);
        return response;
    }

    public async Task UpdateTaskAssignmentStatus(int id, TaskAssignmentUpdate request)
    {
        TaskAssignment task = await _context.TaskAssignment.FindAsync(id)
       ?? throw new NotFoundException("task not assigned with this id");
        task.Status = request.Status;
        string key = $"task_assignment_{id}";
       
        await _context.SaveChangesAsync();
        await _cache_service.SetDataAsync(key, task);
        
    }



}