using DocAppointApi.Datas;
using DocAppointApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace DocAppointApi.Services
{
    public class TraitService
    {
        private readonly DbContextRed _dbContext;

        public TraitService(DbContextRed dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TraitemtP> Regis(TraitemtP trait)
        {
            try
            {

                // Enregistrer l'utilisateur dans la base de données
                _dbContext.TraitemtPs.Add(trait);
                await _dbContext.SaveChangesAsync();

                return trait;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'inscription de l'utilisateur : " + ex.Message, ex);
            }
        }

        public async Task<string> CreateSessionToken(TraitemtP trait)
        {
            try
            {
                // Rechercher l'utilisateur dans la base de données par adresse e-mail
                var registUser = await _dbContext.TraitemtPs.FirstOrDefaultAsync(t => t.Id == trait.Id);

                if (registUser == null)
                {
                    throw new Exception("L'utilisateur n'a pas été trouvé.");
                }

                // Générer le jeton JWT pour l'utilisateur
                string token = GenerateJwtToken(registUser);

                return token;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la création du jeton de session : " + ex.Message);
            }
        }
        public async Task<List<TraitemtP>> GetTraitementAsync(string lastName)
        {

            var patient = await _dbContext.Users.FirstOrDefaultAsync(u => u.LastName == lastName);

            if (patient == null)
            {
                throw new Exception("Patient non trouvé.");
            }

            return await _dbContext.TraitemtPs.Where(c => c.Pid == patient.LastName).ToListAsync();
        }

        private string GenerateJwtToken(TraitemtP trait)
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

        
    }
}
