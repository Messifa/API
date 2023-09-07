using DocAppointApi.Datas;
using DocAppointApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace DocAppointApi.Services
{
    public class ConService
    {
        private readonly DbContextRed _dbContext;

        public ConService(DbContextRed dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Consecration> Register(Consecration medoy)
        {
            try
            {
                // Vérifier si l'utilisateur existe déjà dans la base de données à l'aide de "user.Email"
               

                // Enregistrer l'utilisateur dans la base de données
                _dbContext.Consecrations.Add(medoy);
                await _dbContext.SaveChangesAsync();

                return medoy;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'inscription de l'utilisateur : " + ex.Message, ex);
            }
        }

        public async Task<string> CreateSessionToken(Consecration medoy)
        {
            try
            {
                // Rechercher l'utilisateur dans la base de données par adresse e-mail
                var registerUser = await _dbContext.Consecrations.FirstOrDefaultAsync(m => m.consName == medoy.consName);

                if (registerUser == null)
                {
                    throw new Exception("L'utilisateur n'a pas été trouvé.");
                }

                // Générer le jeton JWT pour l'utilisateur
                string token = GenerateJwtToken(registerUser);

                return token;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la création du jeton de session : " + ex.Message);
            }
        }
        public async Task<List<Consecration>> GetConsultationsByPatientLastNameAsync(string lastName)
        {
            
            var patient = await _dbContext.Users.FirstOrDefaultAsync(u => u.LastName == lastName);

            if (patient == null)
            {
                throw new Exception("Patient non trouvé.");
            }

            return await _dbContext.Consecrations.Where(c => c.consName == patient.LastName).ToListAsync();
        }

        private string GenerateJwtToken(Consecration medoy)
        {
            // Générer une clé de 256 bits
            byte[] keyBytes = new byte[32]; // 32 bytes = 256 bits
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(keyBytes);
            }

            // Convertir la clé en chaîne Base64 pour stockage ou utilisation ultérieure
            string base64Key = Convert.ToBase64String(keyBytes);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(base64Key); // Convertir la clé en tableau de bytes
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Configurer le reste du tokenDescriptor comme avant
                // ...
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

       // private bool CheckIfCons(string conName)
       // {
            // Vérifier si l'utilisateur existe déjà dans la base de données
           
      //  }

        
    }
}
