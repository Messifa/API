using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocAppointApi.Models
{
    public class Medecin: User
    {
        public int medocId { get; set; }
        public string Specialite { get; set; }
        
    }
}
