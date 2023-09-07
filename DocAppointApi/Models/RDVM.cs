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
        public string Category { get; set; }
        public DateTime Datefin { get; set; }
        public int medocid { get; set; }
        [ForeignKey("medocId")]

        public Medecin Medecin { get; set; }
        public int Patientid { get; set; }
        [ForeignKey("Patientid")]

        public Patient Patient { get; set; }



    }
}
