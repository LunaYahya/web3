using Microsoft.EntityFrameworkCore;

namespace E_learninig.Models
{
    public class EleariningContext : DbContext
    {

        public EleariningContext(DbContextOptions<EleariningContext> options)

: base(options)

        { }


        public DbSet<Student> Student { get; set; } = default!;
        public DbSet<Course> Course { get; set; } = default!;
        public DbSet<Enrollment> Enrollment { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Enrollments)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId);
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Enrollments)
                .WithOne(e => e.Course)
                .HasForeignKey(e => e.CourseId);
            modelBuilder.Entity<Student>().HasData(


                 new Student { StudentId = 1, FirstName = "Ahmad", LastName = "Ali", Email = "ahmad@student.com", Password = "123456" },
        new Student { StudentId = 2, FirstName = "Sara", LastName = "Mohammad", Email = "sara@student.com", Password = "123456" },
        new Student { StudentId = 3, FirstName = "Omar", LastName = "Hassan", Email = "omar@student.com", Password = "123456" },
        new Student { StudentId = 4, FirstName = "Laila", LastName = "Mahmoud", Email = "laila@student.com", Password = "123456" }
    );

            modelBuilder.Entity<Course>().HasData(
                 new Course { CourseId = 1, Title = "Math ", Credits = 3 },
        new Course { CourseId = 2, Title = "Physics ", Credits = 4 },
        new Course { CourseId = 3, Title = "Programming C#", Credits = 3 },
        new Course { CourseId = 4, Title = "English", Credits = 2 }
                );

            modelBuilder.Entity<Enrollment>().HasData(
                 new Enrollment { EnrollmentId = 1, StudentId = 1, CourseId = 1, EnrollmentDate = DateTime.Parse("2026-01-10"), Grade = "A" },
        new Enrollment { EnrollmentId = 2, StudentId = 1, CourseId = 3, EnrollmentDate = DateTime.Parse("2026-01-11"), Grade = "B+" },
        new Enrollment { EnrollmentId = 3, StudentId = 2, CourseId = 2, EnrollmentDate = DateTime.Parse("2026-01-10"), Grade = "A-" },
        new Enrollment { EnrollmentId = 4, StudentId = 2, CourseId = 4, EnrollmentDate = DateTime.Parse("2026-01-12"), Grade = "B" },
        new Enrollment { EnrollmentId = 5, StudentId = 3, CourseId = 1, EnrollmentDate = DateTime.Parse("2026-01-11"), Grade = "C+" },
        new Enrollment { EnrollmentId = 6, StudentId = 4, CourseId = 3, EnrollmentDate = DateTime.Parse("2026-01-12"), Grade = "B" }
                );
        }

    }
}
