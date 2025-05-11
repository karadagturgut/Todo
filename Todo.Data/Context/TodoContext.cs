using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Todo.Core;
using Todo.Core.Entity;
using Todo.Core.Entity.Assignments;


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

        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<AssignmentStatus> AssignmentStatuses { get; set; }
        public virtual DbSet<Board> Boards { get; set; }
        public DbSet<ActionRole> ActionRoles { get; set; }
        public DbSet<UserBoard> UserBoards { get; set; }
        public DbSet<UserTimeTracker> UserTimeTrackers { get; set; }
        public DbSet<AssignmentComment> AssignmentComments { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<ActionRole>().HasData(
                new ActionRole { Id = 1, Action = "/Auth/Login", Roles = "", IsPublic = true },
                new ActionRole { Id = 2, Action = "/Auth/Register", Roles = "", IsPublic = true },
                new ActionRole { Id = 3, Action = "/Auth/ChangePassword", Roles = "", IsPublic = true },
                new ActionRole { Id = 4, Action = "/GoogleLogin", Roles = "", IsPublic = true },
                new ActionRole { Id = 5, Action = "/Auth/UserProfile", Roles = "SuperAdmin,Admin,User", IsPublic = false },
                new ActionRole { Id = 6, Action = "/Assignment/Statuses", Roles = "SuperAdmin,Admin,User", IsPublic = false },
                new ActionRole { Id = 9, Action = "/Assignment/GetAll", Roles = "SuperAdmin,Admin,User", IsPublic = false },
                new ActionRole { Id = 12, Action = "/Assignment/Add", Roles = "SuperAdmin,Admin,User", IsPublic = false },
                new ActionRole { Id = 14, Action = "/Assignment/Update", Roles = "SuperAdmin,Admin,User", IsPublic = false },
                new ActionRole { Id = 15, Action = "/Assignment/Delete", Roles = "SuperAdmin,Admin", IsPublic = false },
                new ActionRole { Id = 16, Action = "/Assignment/FilterByStatus", Roles = "SuperAdmin,Admin,User", IsPublic = false },
                new ActionRole { Id = 17, Action = "/Assignment/FilterByName", Roles = "SuperAdmin,Admin,User", IsPublic = false },
                new ActionRole { Id = 19, Action = "/Board/ActiveBoards", Roles = "SuperAdmin,Admin", IsPublic = false },
                new ActionRole { Id = 20, Action = "/Board/Add", Roles = "SuperAdmin,Admin", IsPublic = false },
                new ActionRole { Id = 21, Action = "/Board/Update", Roles = "SuperAdmin,Admin", IsPublic = false },
                new ActionRole { Id = 23, Action = "/Board/Delete", Roles = "SuperAdmin,Admin", IsPublic = false },
                new ActionRole { Id = 24, Action = "/Board/GetUserBoards", Roles = "SuperAdmin,Admin,User", IsPublic = false },
                new ActionRole { Id = 25, Action = "/Board/GetOrganizationBoards", Roles = "SuperAdmin,Admin,User", IsPublic = false },
                new ActionRole { Id = 27, Action = "/Comment/Add", Roles = "SuperAdmin,Admin,User", IsPublic = false },
                new ActionRole { Id = 28, Action = "/Comment/Edit", Roles = "SuperAdmin,Admin,User", IsPublic = false },
                new ActionRole { Id = 29, Action = "/Comment/AssignmentComments", Roles = "SuperAdmin,Admin,User", IsPublic = false },
                new ActionRole { Id = 30, Action = "/Comment/Delete", Roles = "SuperAdmin,Admin,User", IsPublic = false },
                new ActionRole { Id = 31, Action = "/Document/AddDocument", Roles = "SuperAdmin,Admin,User", IsPublic = false },
                new ActionRole { Id = 32, Action = "/Organization/AllOrganizations", Roles = "SuperAdmin,Admin", IsPublic = false },
                new ActionRole { Id = 33, Action = "/Organization/AddOrganization", Roles = "SuperAdmin,Admin", IsPublic = false },
                new ActionRole { Id = 34, Action = "/Organization/DeleteOrganization", Roles = "SuperAdmin,Admin", IsPublic = false },
                new ActionRole { Id = 35, Action = "/Organization/UpdateOrganization", Roles = "SuperAdmin,Admin", IsPublic = false },
                new ActionRole { Id = 36, Action = "/RecentlyVisited/Get", Roles = "SuperAdmin,Admin", IsPublic = false },
                new ActionRole { Id = 37, Action = "/TimeTracker/Add", Roles = "SuperAdmin,Admin,User", IsPublic = false },
                new ActionRole { Id = 39, Action = "/TimeTracker/Update", Roles = "SuperAdmin,Admin,User", IsPublic = false },
                new ActionRole { Id = 40, Action = "/TimeTracker/Get", Roles = "SuperAdmin,Admin,User", IsPublic = false },
                new ActionRole { Id = 41, Action = "/TimeTracker/Delete", Roles = "SuperAdmin,Admin,User", IsPublic = false },
                new ActionRole { Id = 42, Action = "/UserRole/SetRoles", Roles = "SuperAdmin", IsPublic = false }
            );

            modelBuilder.Entity<Organization>()
                .HasIndex(o => o.Name)
                .IsUnique();

            modelBuilder.Entity<Board>()
                .HasOne(b => b.Organization)
                .WithMany()
                .HasForeignKey(b => b.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Board)
                .WithMany()
                .HasForeignKey(a => a.BoardId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Organization)
                .WithMany()
                .HasForeignKey(a => a.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Status)
                .WithMany()
                .HasForeignKey(a => a.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AssignmentUser>()
                .HasOne(au => au.Assignment)
                .WithMany(a => a.AssignedUsers)
                .HasForeignKey(au => au.AssignmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AssignmentUser>()
                .HasOne(au => au.User)
                .WithMany()
                .HasForeignKey(au => au.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserBoard>()
                .HasOne(ub => ub.Board)
                .WithMany()
                .HasForeignKey(ub => ub.BoardId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserBoard>()
                .HasOne(ub => ub.User)
                .WithMany()
                .HasForeignKey(ub => ub.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Document>()
                .HasOne(d => d.Board)
                .WithMany()
                .HasForeignKey(d => d.BoardId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Document>()
                .HasOne(d => d.Organization)
                .WithMany()
                .HasForeignKey(d => d.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Document>()
                .HasOne(d => d.UploadUser)
                .WithMany()
                .HasForeignKey(d => d.UploadUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Document>()
                .HasOne(d => d.Assignment)
                .WithMany()
                .HasForeignKey(d => d.AssignmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserTimeTracker>()
                .HasOne(t => t.Assignment)
                .WithMany()
                .HasForeignKey(t => t.AssignmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserTimeTracker>()
                .HasOne(t => t.Organization)
                .WithMany()
                .HasForeignKey(t => t.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserTimeTracker>()
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
