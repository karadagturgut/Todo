using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Todo.Core;
using Todo.Core.Entity;
using Todo.Core.Entity.LessonUnit;


namespace Todo.Data
{
    public class TodoContext : IdentityDbContext<TodoUser, TodoRole, int>
    {
        public virtual DbSet<Assignments> Assigments { get; set; }
        public virtual DbSet<AssignmentStatus> AssignmentStatus { get; set; }
        public virtual DbSet<Boards> Boards { get; set; }
        public DbSet<ActionRoles> ActionRoles { get; set; }
        public DbSet<UserBoard> UserBoards { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonUnit> LessonsUnit { get; set; }
        public DbSet<UserTimeTracker> UserTimeTracker { get; set; }
        public DbSet<AssignmentComment> AssignmentComments { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Document> Documents { get; set; }
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

    }
}
