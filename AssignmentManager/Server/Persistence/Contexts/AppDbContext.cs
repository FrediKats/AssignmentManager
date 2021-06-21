using System.Collections.Immutable;
using AssignmentManager.Server.Models;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Speciality>().ToTable("Specialities");
            builder.Entity<Speciality>().HasKey(p => p.Id);
            builder.Entity<Speciality>().Property(p => p.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Entity<Speciality>().Property(p => p.EnumStudyType)
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
                .ValueGeneratedNever();
            builder.Entity<Instructor>().Property(p => p.LastName)
                .IsRequired();
            builder.Entity<Instructor>().Property(p => p.FirstName)
                .IsRequired();
            builder.Entity<Instructor>().Property(p => p.PatronymicName);
            builder.Entity<Instructor>().Property(p => p.Email)
                .IsRequired();
            builder.Entity<Instructor>().HasData(
                new Instructor()
                {
                    IsuId = 111112, 
                    LastName = "Mayatin", 
                    FirstName = "Alexander", 
                    PatronymicName = "Vladimirovich",
                    Email = "e@mail.ru"
                }
            );
            builder.Entity<Instructor>().HasData(
                new Instructor()
                {
                    IsuId = 111113, 
                    LastName = "Beresnev", 
                    FirstName = "Artem", 
                    PatronymicName = "Dmitrievich",
                    Email = "e@mail.ru"
                }
            );
            builder.Entity<Instructor>().HasData(
                new Instructor()
                {
                    IsuId = 111114, 
                    LastName = "Mavrin", 
                    FirstName = "Pavel", 
                    Email = "e@mail.ru"
                }
            );
            builder.Entity<Instructor>().HasData(
                new Instructor()
                {
                    IsuId = 111115, 
                    LastName = "Priiskalov", 
                    FirstName = "Roman", 
                    PatronymicName = "Andreevich",
                    Email = "e@mail.ru"
                }
            );

            builder.Entity<Subject>().ToTable("Subjects");
            builder.Entity<Subject>().HasKey(p => p.SubjectId);
            builder.Entity<Subject>().Property(p => p.SubjectId)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Entity<Subject>().Property(p => p.SubjectName)
                .IsRequired();
            builder.Entity<Subject>().HasData(
                new Subject() {SubjectId = 1, SubjectName = "OS"},
                new Subject(){SubjectId = 2, SubjectName = "DB"}
            );
            
            builder.Entity<InstructorSubject>().ToTable("InstructorSubjects");
            builder.Entity<InstructorSubject>().HasKey(p => p.Id);
            builder.Entity<InstructorSubject>().Property(p => p.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Entity<InstructorSubject>().Property(p => p.IsuId)
                .IsRequired();
            builder.Entity<InstructorSubject>().Property(p => p.SubjectId)
                .IsRequired();
            builder.Entity<InstructorSubject>().HasData(
                new InstructorSubject() {Id = 1, SubjectId = 1, IsuId = 111112},
                new InstructorSubject() {Id = 2, SubjectId = 2, IsuId = 111112}
            );
        }
    }
}