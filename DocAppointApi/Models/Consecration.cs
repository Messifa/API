using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace DocAppointApi.Models
{
    public class Consecration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int consId {  get; set; }

        public string consName { get; set; }
        public DateTime consDate { get; set; }
        public string consTaille { get; set; }
        public int consTension { get; set; }
        public int consPoids { get; set; }
       
    }
}
