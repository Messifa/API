using Microsoft.AspNetCore.Mvc;
using DocAppointApi.Models;
using DocAppointApi.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DocAppointApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly char[] _tokenSecretKey;

        public PatientController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] Patient patient)
        {
            try
            {
                // Créez un nouvel objet User à partir des propriétés de Patient
                var user = new User
                {
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    username = patient.username,
                    Phonenumber = patient.Phonenumber,
                    Age = patient.Age,
                    Email = patient.Email,
                    Password = patient.Password,
                    Avatar = patient.Avatar,
                    // Ajoutez d'autres propriétés communes entre Patient et User ici
                };

                // Enregistrez le nouvel utilisateur dans la base de données
                var registeredUser = await _userService.RegisterUser(user);

                // Générer le jeton de session pour le patient nouvellement enregistré
                var token = _userService.CreateSessionToken(registeredUser);

                // Retourner une réponse "Ok" avec le patient créé et le jeton de session
                return Ok("vous etes inscrits");
            }
            catch (Exception ex)
            {
                // En cas d'erreur, renvoyer une réponse "BadRequest" avec le message d'erreur
                throw ex;
                return BadRequest("Une erreur s'est produite");
            }
        }

        // ... (autres méthodes du contrôleur)
    
        private bool CheckIfUserExists(string Email)
        {
            // Utilisez le service UserService pour vérifier si l'utilisateur existe déjà dans la base de données
            var user = _userService.GetUserByEmail(Email);

            // Retournez true si l'utilisateur existe, sinon retournez false
            return user != null;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] Patient patient)
        {
            try
            {
                // Rechercher le médecin dans la base de données à l'aide de "medecin.Email" et "medecin.Password"
                var authenticatedPatient = await _userService.LoginUser(patient.Email, patient.Password);

                // Si le médecin est trouvé, créer une session utilisateur et renvoyer une réponse "Ok"
                if (authenticatedPatient != null)
                {
                    // Créer une session utilisateur (par exemple, utiliser un jeton JWT)
                    var token = CreateSessionToken(authenticatedPatient);

                    // Retourner une réponse "Ok" avec le jeton
                    return Ok("vous etes connectes ");
                    
                }
                // Sinon, renvoyer une réponse "BadRequest" avec un message d'erreur indiquant que les informations de connexion sont invalides
                else
                {
                    return BadRequest("Les informations de connexion sont invalides.");
                }
            }
            catch (Exception ex)
            {
                // En cas d'erreur, renvoyer une réponse "BadRequest" avec le message d'erreur
                return BadRequest(ex.Message);
            }
        }

        private object CreateSessionToken(User authenticatedPatient)
        {
            try
            {
                // Utilisez le UserService pour générer le jeton de session
                var token = _userService.CreateSessionToken(authenticatedPatient);

                // Retourner une réponse "Ok" avec le jeton
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                // En cas d'erreur, renvoyer une réponse "BadRequest" avec le message d'erreur
                return BadRequest(ex.Message);
            }
        }
    }
        
}
