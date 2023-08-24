using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DocAppointApi.Datas;
using DocAppointApi.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace DocAppointApi.Services
{
    public class UserService
    {
        private readonly DbContextRed _dbContext;

        public UserService(DbContextRed dbContext)
        {
            _dbContext = dbContext;
        }
        

        
        

        public async Task<Patient> LoginPatient(string email, string password)
        {
            try
            {
                // Rechercher et vérifier l'utilisateur dans la base de données
                var patient = await _dbContext.Patients.FirstOrDefaultAsync(p => p.Email == email && p.Password == password);

                if (patient == null)
                {
                    throw new Exception("Les informations de connexion sont invalides.");
                }

                return patient;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la connexion de l'utilisateur : " + ex.Message);
            }
        }
        public async Task<Medecin> LoginMedecin(string email, string password)
        {
            try
            {
                // Rechercher et vérifier l'utilisateur dans la base de données
                var medecin = await _dbContext.Medecins.FirstOrDefaultAsync(m => m.Email == email && m.Password == password);

                if (medecin == null)
                {
                    throw new Exception("Les informations de connexion sont invalides.");
                }

                return medecin;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la connexion de l'utilisateur : " + ex.Message);
            }
        }

        public async Task<Adminis> LoginAdmin(string email, string password)
        {
            try
            {
                // Rechercher et vérifier l'utilisateur dans la base de données
                var admin = await _dbContext.Adminis.FirstOrDefaultAsync(a => a.Email == email && a.Password == password);

                if (admin == null)
                {
                    throw new Exception("Les informations de connexion sont invalides.");
                }

                return admin;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la connexion de l'utilisateur : " + ex.Message);
            }
        }
        private bool CheckIfPatientExists(string email)
        {
            // Vérifier si l'utilisateur existe déjà dans la base de données
            return _dbContext.Patients.Any(p => p.Email == email);
        }
        private bool CheckIfMedecinExists(string email)
        {
            // Vérifier si l'utilisateur existe déjà dans la base de données
            return _dbContext.Medecins.Any(m => m.Email == email);
        }
        private bool CheckIfAdminExists(string email)
        {
            // Vérifier si l'utilisateur existe déjà dans la base de données
            return _dbContext.Adminis.Any(a => a.Email == email);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                // Rechercher l'utilisateur dans la base de données par adresse e-mail
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la recherche de l'utilisateur : " + ex.Message);
            }

        }

       
        public async Task<List<Patient>> GetPatientsAsync()
        {
            var patients = await _dbContext.Patients.ToListAsync();
            return patients;
        }
        public async Task<List<Medecin>> GetMedecinsAsync()
        {
            
          var medecin=  await _dbContext.Medecins.ToListAsync();
            return medecin;
        }

        private string GenerateJwtToken(User user)
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
       
        public async Task<string> CreateSessionToken(User user)
        {
            try
            {
                // Rechercher l'utilisateur dans la base de données par adresse e-mail
                var registeredUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

                if (registeredUser == null)
                {
                    throw new Exception("L'utilisateur n'a pas été trouvé.");
                }

                // Générer le jeton JWT pour l'utilisateur
                string token = GenerateJwtToken(registeredUser);

                return token;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la création du jeton de session : " + ex.Message);
            }
        }
    }
}
