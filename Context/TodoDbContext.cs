using Microsoft.EntityFrameworkCore;
using C_sharp_todo_api.Models;

namespace C_sharp_todo_api.Context
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {

        }
        public DbSet<TodoTask> Tasks { get; set; }
    }
}
