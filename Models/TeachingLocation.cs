using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraphQLDemoBase.Models
{
    [GraphQLName("TeachingLocationTable")]
    public class TeachingLocation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeachingLocationId { get; set; }
        public string Building { get; set; }
        public string Room { get; set; }
    }
}
