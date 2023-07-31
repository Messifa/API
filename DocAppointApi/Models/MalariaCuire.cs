using System.ComponentModel.DataAnnotations;

namespace DocAppointApi.Models
{
    public class MalariaCuire
    {
        [Key] public int malaid { get; set; }

        public string designationM { get; set; }
        public string descriptionM { get; set; }
    }
}
