using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using trainee_management.Models.Entities;
namespace trainee_management.Database;
public class AppDBContext(DbContextOptions<AppDBContext> options) : DbContext(options)
{
    public DbSet< Trainee > Trainee {get;set;}
    public DbSet<User> User {get;set;}
}