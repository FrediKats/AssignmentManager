using System;
using System.Collections.Generic;
using AssignmentManager.Server.Data;
using AssignmentManager.Server.Models;
using AssignmentManager.Shared;
using System.Collections.Immutable;
using IdentityServer4.EntityFramework.Extensions;
 using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AssignmentManager.Server.Persistence.Contexts
{
    public class AppDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public AppDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<InstructorSubject> InstructorSubjects { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Solution> Solutions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Student>().Property(p => p.IsuId).ValueGeneratedNever();
            
            builder.Entity<Instructor>().Property(p => p.IsuId).ValueGeneratedNever();

            builder.Entity<Instructor>().ToTable("Instructors");
            builder.Entity<Instructor>().HasKey(p => p.IsuId);
            builder.Entity<Instructor>().Property(p => p.IsuId)
                .IsRequired()
                .ValueGeneratedNever();
            builder.Entity<Instructor>().Property(p => p.LastName)
                .IsRequired();
            builder.Entity<Instructor>().Property(p => p.FirstName)
                .IsRequired();
            builder.Entity<Instructor>().Property(p => p.PatronymicName);
            builder.Entity<Instructor>().Property(p => p.Email)
                .IsRequired();

            builder.Entity<Subject>().ToTable("Subjects");
            builder.Entity<Subject>().HasKey(p => p.SubjectId);
            builder.Entity<Subject>().Property(p => p.SubjectId)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Entity<Subject>().Property(p => p.SubjectName)
                .IsRequired();

            builder.Entity<InstructorSubject>().ToTable("InstructorSubjects");
            builder.Entity<InstructorSubject>().HasKey(p => p.Id);
            builder.Entity<InstructorSubject>().Property(p => p.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Entity<InstructorSubject>().Property(p => p.IsuId)
                .IsRequired();
            builder.Entity<InstructorSubject>().Property(p => p.SubjectId)
                .IsRequired();
            DataSeeder.SeedData(builder);
        }
    }
}