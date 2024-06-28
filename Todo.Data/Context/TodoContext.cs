using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Todo.Core;


namespace Todo.Data
{
    public class TodoContext : IdentityDbContext<TodoUser,TodoRole,int>
    {
        public virtual DbSet<Assignments> Assigments { get; set; }
        public virtual DbSet<AssignmentStatus> AssignmentStatus { get; set; }
        public virtual DbSet<Boards> Boards { get; set; }
        public DbSet<ActionRoles> ActionRoles { get; set; }
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

    }
}
