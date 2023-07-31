using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocAppointApi.Models
{
    public class TraitemtP
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string MedocTr {get; set; }
        public string MedocAvis { get; set; }
    }
}
