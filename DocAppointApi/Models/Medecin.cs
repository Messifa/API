using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocAppointApi.Models
{
    public class Medecin: User
    {
         
        public bool validation { get; set; }
        public string Specialite { get; set; }
        
    }
}
