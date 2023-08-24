using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace DocAppointApi.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userId { get; set; }
        public  string username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public  string? Email { get; set; }
        public string Phonenumber  { get; set; }
      
        public  string? Password { get; set; }
        public void SetPassword(string Password)
        {
            // Hachez le mot de passe en utilisant un algorithme de hachage fort
            using (var sha256 = SHA256.Create())
            {
                var passwordBytes = Encoding.UTF8.GetBytes(Password);
                var hashBytes = sha256.ComputeHash(passwordBytes);
                Password = Convert.ToBase64String(hashBytes);
            }
        }
        public string Avatar { get; set; }
        
    }
}
