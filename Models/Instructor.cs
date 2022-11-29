using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraphQLDemoBase.Models
{
    [GraphQLName("InstructorTable")]
    [GraphQLDescription("Each Course has one instructor")]
    [Table("Instructors")]
    public class Instructor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InstructorId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        [GraphQLDescription("This is stored at 0 and 1 but displays as Male and Female ")]
        public Gender Gender { get; set; }
    }

    public enum Gender
    {
        Male, Female
    }
}
