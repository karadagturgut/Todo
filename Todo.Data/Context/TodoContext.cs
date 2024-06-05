using Microsoft.EntityFrameworkCore;


namespace Todo.Data
{
    public class TodoContext : DbContext
    {
        public virtual DbSet<Assignments> Assigments { get; set; }
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

    }
}
