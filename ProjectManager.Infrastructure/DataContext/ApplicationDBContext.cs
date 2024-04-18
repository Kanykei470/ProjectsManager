using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectsManager.Domain.Entities.Employees;
using ProjectsManager.Domain.Entities.ProjectMembers;
using ProjectsManager.Domain.Entities.Projects;

namespace ProjectManager.Infrastructure.DataContext
{
    public class ApplicationDBContext : DbContext   
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public virtual DbSet<ProjectsData> Projects { get; set; }
        public virtual DbSet<EmployeesData> Employees { get; set; }
        public virtual DbSet<ProjectMembersData> ProjectsMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeesData>(entity =>
            {
                entity.ToTable("Employees");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).HasColumnName("Name").HasMaxLength(50);
                entity.Property(e => e.MiddleName).HasColumnName("MiddleName").HasMaxLength(50);
                entity.Property(e => e.LastName).HasColumnName("LastName").HasMaxLength(50);
                entity.Property(e => e.Email).HasColumnName("Email").HasMaxLength(20);
            });


            modelBuilder.Entity<ProjectsData>(entity =>
            {
                entity.ToTable("Projects");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).HasColumnName("Name");
                entity.Property(e => e.CustomerName).HasColumnName("CustomerName").HasMaxLength(50);
                entity.Property(e => e.ExecutorName).HasColumnName("ExecutorName").HasMaxLength(50);
                entity.Property(e => e.StartDate).HasColumnName("StartDate");
                entity.Property(e => e.EndDate).HasColumnName("EndDate");
                entity.Property(e => e.Priority).HasColumnName("Priority").HasMaxLength(1);
            });


            modelBuilder.Entity<ProjectMembersData>(entity =>
            {
                entity.ToTable("ProjectsMembers");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.IsProjectManager).HasColumnName("IsProjectManager").HasDefaultValue(false);
            });

            modelBuilder.Entity<ProjectMembersData>()
                .HasOne(pe => pe.Employee)
                .WithMany(p => p.ProjectMembers)
                .HasForeignKey(pe => pe.EmployeesId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<ProjectMembersData>()
                .HasOne(pe => pe.Project)
                .WithMany(p => p.ProjectMembers)
                .HasForeignKey(pe => pe.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
