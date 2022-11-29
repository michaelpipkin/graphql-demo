using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraphQLDemoBase.Models
{
    [GraphQLDescription("You can navigate to Course and Student from here")]
    [GraphQLName("EnrollmentTable")]
    public class Enrollment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnrollmentId { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        [GraphQLDescription("In the DB this is a number")] 
        public Grade? Grade { get; set; } // this returns a letter name.

        public virtual Course Course { get; set; }
        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }
    }
}