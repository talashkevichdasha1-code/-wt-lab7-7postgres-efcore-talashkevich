using Microsoft.EntityFrameworkCore;
using wt_lab7_7postgres_efcore_talashkevich.Models;

namespace wt_lab7_7postgres_efcore_talashkevich.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Member> Members => Set<Member>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Member>().HasData(
            new Member
            {
                Id = 1,
                Name = "Анна Петрова",
                Age = 20,
                MembershipPlanId = 1,
                TrainerId = 1,
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 10, 10, 0, 0, DateTimeKind.Utc)
            },
            new Member
            {
                Id = 2,
                Name = "Максим Иванов",
                Age = 25,
                MembershipPlanId = 2,
                TrainerId = 2,
                IsActive = true,
                CreatedAt = new DateTime(2026, 2, 15, 12, 0, 0, DateTimeKind.Utc)
            },
            new Member
            {
                Id = 3,
                Name = "Елена Смирнова",
                Age = 30,
                MembershipPlanId = 1,
                TrainerId = 1,
                IsActive = false,
                CreatedAt = new DateTime(2026, 3, 5, 9, 0, 0, DateTimeKind.Utc)
            },
            new Member
            {
                Id = 4,
                Name = "София Морозова",
                Age = 22,
                MembershipPlanId = 3,
                TrainerId = 2,
                IsActive = true,
                CreatedAt = new DateTime(2026, 4, 20, 14, 0, 0, DateTimeKind.Utc)
            },
            new Member
            {
                Id = 5,
                Name = "Дмитрий Козлов",
                Age = 35,
                MembershipPlanId = 2,
                TrainerId = 3,
                IsActive = false,
                CreatedAt = new DateTime(2026, 5, 12, 11, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}