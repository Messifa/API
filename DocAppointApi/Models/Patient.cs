using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace DocAppointApi.Models
{
    public class Patient: User
    {
        
        public string AdresseP { get; set; }
        public string Sexe { get; set; }
        public int malaid { get; set; }
        [ForeignKey("malaid")]
        
        public  MalariaCuire MalariaCuire { get; set; }
    }
}
