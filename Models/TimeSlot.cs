using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraphQLDemoBase.Models
{
    [GraphQLName("TimeSlotTable")]
    [GraphQLDescription("The timeslots for each course can vary")]
    public class TimeSlot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TimeSlotId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
