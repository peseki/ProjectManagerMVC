using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectManagerMVC.Models;
using System.Security.Cryptography.X509Certificates;
using static Azure.Core.HttpHeader;

namespace ProjectManagerMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Project> Project { get; set; } = default!;
        public DbSet<Employee> Employee { get; set; } = default!;

        public DbSet<EmployeeProject> EmployeeProject { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<EmployeeProject>()
                .HasKey(m => new{m.EmployeeID, m.ProjectID});

        }
    }
}