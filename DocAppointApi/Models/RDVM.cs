using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocAppointApi.Models
{
    public class RDVM
    {
        public bool IsValidated;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RDVId { get; set; }
        public DateTime Datedb { get; set; }
        public string RDVlibelle { get; set; }
        public DateTime Datefin { get; set; }
        [ForeignKey("medocId")]
        public int medocId { get; set; }
        public Medecin Medecin{ get; set; }

        [ForeignKey("PatientId")]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }


    }
}
