using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocAppointApi.Models
{
    public class Specialite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int speid { get; set; }
        public string Designation { get; set; }
        public string Description { get; set; }

    }
}
