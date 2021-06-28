using System;
using System.Collections.Generic;
using AssignmentManager.Server.Data;
using AssignmentManager.Server.Models;
using AssignmentManager.Shared;
using System.Collections.Immutable;
using IdentityServer4.EntityFramework.Extensions;
 using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
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

            builder.Entity<IdentityRole>().ToTable("IdentityRoles");
            
            DataSeeder.SeedData(builder);
        }
    }
}