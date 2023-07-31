using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocAppointApi.Models
{
    public class Medecin: User
    {
         public int medocId { get; set; }
        public bool validation { get; set; }
        public int sperid { get; set; }
        [ForeignKey("sperid")]
       
        public Specialite Specialite { get; set; }
    }
}
