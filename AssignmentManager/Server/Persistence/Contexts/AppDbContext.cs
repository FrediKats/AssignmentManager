using AssignmentManager.Server.Models;
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


            builder.Entity<Group>().ToTable("Groups");
            builder.Entity<Group>().HasKey(p => p.Id);
            builder.Entity<Group>().Property(p => p.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Entity<Group>().Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(10);
            builder.Entity<Group>()
                .HasMany(p => p.Students)
                .WithOne(p => p.Group)
                .HasForeignKey(p => p.GroupId);

            builder.Entity<Student>().ToTable("Students");
            builder.Entity<Student>().HasKey(p => p.IsuId);
            builder.Entity<Student>().Property(p => p.IsuId)
                .IsRequired()
                .ValueGeneratedNever();
            builder.Entity<Student>().Property(p => p.Email)
                .IsRequired();
            builder.Entity<Student>().Property(p => p.Lastname)
                .IsRequired();
            builder.Entity<Student>().Property(p => p.Name)
                .IsRequired();

            builder.Entity<Instructor>().ToTable("Instructors");
            builder.Entity<Instructor>().HasKey(p => p.IsuId);
            builder.Entity<Instructor>().Property(p => p.IsuId)
                .IsRequired()
                .HasMaxLength(6);
            builder.Entity<Instructor>().Property(p => p.LastName)
                .IsRequired();
            builder.Entity<Instructor>().Property(p => p.FirstName)
                .IsRequired();
            builder.Entity<Instructor>().Property(p => p.PatronymicName)
                .IsRequired();
            builder.Entity<Instructor>().Property(p => p.Email)
                .IsRequired();

            builder.Entity<Subject>().ToTable("Subjects");
            builder.Entity<Subject>().HasKey(p => p.SubjectId);
            builder.Entity<Subject>().Property(p => p.SubjectId)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Entity<Subject>().Property(p => p.SubjectName)
                .IsRequired();
        }
    }
}