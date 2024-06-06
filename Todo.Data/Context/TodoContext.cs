using Microsoft.EntityFrameworkCore;
using Todo.Data.Entity;


namespace Todo.Data
{
    public class TodoContext : DbContext
    {
        public virtual DbSet<Assignments> Assigments { get; set; }
        public virtual DbSet<AssignmentStatus> AssignmentStatus { get; set; }
        public virtual DbSet<Boards> Boards { get; set; }
        public virtual DbSet<BoardsAssignments> BoardsAssignments { get; set; }
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

    }
}
