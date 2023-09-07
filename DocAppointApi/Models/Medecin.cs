using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocAppointApi.Models
{
    public class Medecin: User
    {
        public string Specialite { get; set; }
        
    }
}
