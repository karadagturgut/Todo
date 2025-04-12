using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Todo.Core;
using Todo.Core.Entity;
using Todo.Core.Entity.LessonUnit;


namespace Todo.Data
{
    public class TodoContext : IdentityDbContext<TodoUser, TodoRole, int>
    {

        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }
        public TodoContext()
        {
                
        }

        public virtual DbSet<Assignment> Assigments { get; set; }
        public virtual DbSet<AssignmentStatus> AssignmentStatus { get; set; }
        public virtual DbSet<Board> Boards { get; set; }
        public DbSet<ActionRole> ActionRoles { get; set; }
        public DbSet<UserBoard> UserBoards { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonUnit> LessonsUnit { get; set; }
        public DbSet<UserTimeTracker> UserTimeTracker { get; set; }
        public DbSet<AssignmentComment> AssignmentComments { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Document> Documents { get; set; }
        
    }
}
