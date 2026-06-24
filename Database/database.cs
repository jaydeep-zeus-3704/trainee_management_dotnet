
using Microsoft.EntityFrameworkCore;
using trainee_management.Models;
using trainee_management.Models.Entities;

namespace trainee_management.Database;
public class AppDBContext(DbContextOptions<AppDBContext> options) : DbContext(options)
{
    public DbSet< Trainee > Trainee {get;set;}
    public DbSet<User> User {get;set;}
    public DbSet<Mentor> Mentor {get;set;}

    public DbSet<LearningTask> LearningTask {get;set;}

    public DbSet<TaskAssignment> TaskAssignment {get;set;}
    public DbSet<Submission> Submission{get;set;}

    public DbSet<Review> Review {get;set;}

    public DbSet<Metadata> Metadata {get;set;}

    public DbSet<ProcessingJob> ProcessingJob {get;set;}

}