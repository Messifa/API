using System.ComponentModel.DataAnnotations;

namespace DocAppointApi.Models
{
    public class Statut
    {
        [Key] public int RDVPId { get; set; }
        public string val { get; set; }
    }
}
