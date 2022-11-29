using GraphQLDemoBase.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemoBase
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options) {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
    }
}