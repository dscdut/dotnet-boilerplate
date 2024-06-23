using Microsoft.EntityFrameworkCore;
using DotnetBoilerplate.Domain.Entities;

namespace DotnetBoilerplate.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.FullName).HasColumnName("name");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.Password).HasColumnName("password");
                entity.Property(e => e.RoleId).HasColumnName("role_id");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
                entity.Property(e => e.IsSuperUser).HasColumnName("is_superuser");
                entity.Property(e => e.IsStaff).HasColumnName("is_staff");
                entity.Property(e => e.IsActive).HasColumnName("is_active");
                entity.Property(e => e.LastLogin).HasColumnName("last_login");
                entity.Property(e => e.DateJoined).HasColumnName("date_joined");

                // Set DateTimeKind to Utc for DateTime properties
                entity.Property(e => e.CreatedAt).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
                entity.Property(e => e.UpdatedAt).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
                entity.Property(e => e.LastLogin).HasConversion(v => v, v => DateTime.SpecifyKind(v ?? DateTime.UtcNow, DateTimeKind.Utc));
                entity.Property(e => e.DateJoined).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

                // Make the Email property unique
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<User>().ToTable("users_user");

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
                entity.Property(e => e.CreatedAt).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
                entity.Property(e => e.UpdatedAt).HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            });
            modelBuilder.Entity<Role>().ToTable("roles_role");

            modelBuilder.Entity<Role>()
                .HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            var adminRole = new Role { Id = 1, Name = "admin", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            var memberRole = new Role { Id = 2, Name = "member", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            var defaultPassword = BCrypt.Net.BCrypt.HashPassword("String@123");
            var user1 = new User { Id = 1, FullName = "Admin", Email = "admin@email.com", Password = defaultPassword, RoleId = 1, IsSuperUser = true, IsStaff = false, IsActive = true, CreatedAt = DateTime.UtcNow, DateJoined = DateTime.UtcNow, LastLogin = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            var user2 = new User { Id = 2, FullName = "Long", Email = "long@email.com", Password = defaultPassword, RoleId = 2, IsSuperUser = false, IsStaff = false, IsActive = true, CreatedAt = DateTime.UtcNow, DateJoined = DateTime.UtcNow, LastLogin = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };

            modelBuilder.Entity<Role>().HasData(adminRole, memberRole);
            modelBuilder.Entity<User>().HasData(user1, user2);

            base.OnModelCreating(modelBuilder);
        }
    }
}
