using AssignmentManager.Server.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AssignmentManager.Server.Persistence.Contexts
{
        public class AppDbContext : ApiAuthorizationDbContext<ApplicationUser> 
        { 
            public DbSet<Speciality> Specialities { get; set; } 
            public AppDbContext(
                DbContextOptions options, 
                IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions) 
            {
                
            }
            
            protected override void OnModelCreating(ModelBuilder builder) 
            { 
                base.OnModelCreating(builder);
                
                builder.Entity<Speciality>().ToTable("Specialities"); 
                builder.Entity<Speciality>().HasKey(p => p.Id);
                builder.Entity<Speciality>().Property(p => p.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();
                builder.Entity<Speciality>().Property(p => p.StudyType)
                    .IsRequired();
                builder.Entity<Speciality>().Property(p => p.Code)
                    .IsRequired();
            
                
            builder.Entity<Speciality>()
                .HasMany(p => p.Groups)
                .WithOne(p => p.Speciality)
                .HasForeignKey(p => p.SpecialityId);


            builder.Entity<Group>().ToTable("Groups");
            builder.Entity<Group>().HasKey(p => p.Id);
            builder.Entity<Group>().Property(p => p.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Entity<Group>().Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(10);
            builder.Entity<Group>()
                .HasMany<Student>(p => p.Students)
                .WithOne(p => p.Group)
                .HasForeignKey(p => p.GroupId);

            builder.Entity<Student>().ToTable("Students");
            builder.Entity<Student>().HasKey(p => p.IsuId);
            builder.Entity<Student>().Property(p => p.IsuId)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Entity<Student>().Property(p => p.Email)
                .IsRequired();
            builder.Entity<Student>().Property(p => p.Lastname)
                .IsRequired();
            builder.Entity<Student>().Property(p => p.Name)
                .IsRequired();
        }
    }
}