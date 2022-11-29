using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraphQLDemoBase.Models
{
    [Table("Courses")]
    [GraphQLName("CourseTable")]
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public int InstructorId { get; set; }
        public int TimeSlotId { get; set; }
        public int CourseLocationId { get; set; }

        public virtual Instructor Instructor { get; set; }
        public virtual TimeSlot TimeSlot { get; set; }

        [GraphQLDescription("The foreign key tag is needed here since the property of CourseLocationid does not match the primary key table property of TeachingLocationId")]
        [ForeignKey("CourseLocationId")]
        public virtual TeachingLocation TeachingLocation { get; set; }

        [GraphQLDescription("from courses you can jump to enrollments too")]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}