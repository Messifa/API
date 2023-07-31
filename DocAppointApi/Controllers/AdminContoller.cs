using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using DocAppointApi.Models;
using DocAppointApi.Services;

namespace DocAppointApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly AdminService _adminService;

        public AdminController(UserService userService, AdminService adminService)
        {
            _userService = userService;
            _adminService = adminService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] Adminis adminis)
        {
            try
            {
                // Créez un nouvel objet User à partir des propriétés de Patient
                var user = new User
                {
                    FirstName = adminis.FirstName,
                    LastName = adminis.LastName,
                    username = adminis.username,
                    Phonenumber = adminis.Phonenumber,
                    Age = adminis.Age,
                    Email = adminis.Email,
                    Password = adminis.Password,
                    Avatar = adminis.Avatar,
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

        private bool CheckIfUserExists(string Email)
        {
            // Utilisez le service UserService pour vérifier si l'utilisateur existe déjà dans la base de données
            var user = _userService.GetUserByEmail(Email);

            // Retournez true si l'utilisateur existe, sinon retournez false
            return user != null;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] Adminis adminis)
        {
            try
            {
                // Rechercher le médecin dans la base de données à l'aide de "medecin.Email" et "medecin.Password"
                var authenticatedAdminis = await _userService.LoginUser(adminis.Email, adminis.Password);

                // Si le médecin est trouvé, créer une session utilisateur et renvoyer une réponse "Ok"
                if (authenticatedAdminis != null)
                {
                    // Créer une session utilisateur (par exemple, utiliser un jeton JWT)
                    var token = CreateSessionToken(authenticatedAdminis);

                    // Retourner une réponse "Ok" avec le jeton
                    return Ok("vous etes connectes");
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

        private object CreateSessionToken(User authenticatedAdminis)
        {
            try
            {
                // Utilisez le UserService pour générer le jeton de session
                var token = _userService.CreateSessionToken(authenticatedAdminis);

                // Retourner une réponse "Ok" avec le jeton
                return Ok("vous etes connectes");
            }
            catch (Exception ex)
            {
                // En cas d'erreur, renvoyer une réponse "BadRequest" avec le message d'erreur
                return BadRequest(ex.Message);
            }
        }







        [HttpPost("appointments/{appointmentId}/validate")]
        public async Task<IActionResult> ValidateAppointment(int appointmentId)
        {
            try
            {
                var validatedAppointment = await _adminService.ValidateAppointment(appointmentId, "Valider");
                return Ok(validatedAppointment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("appointments/{appointmentId}/reject")]
        public async Task<IActionResult> RejectAppointment(int appointmentId)
        {
            try
            {
                var rejectedAppointment = await _adminService.ValidateAppointment(appointmentId, "Refuser");
                return Ok(rejectedAppointment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("appointments/{appointmentId}/status")]
        public async Task<IActionResult> GetAppointmentStatus(int appointmentId)
        {
            try
            {
                var appointmentStatus = await _adminService.GetAppointmentStatus(appointmentId);
                return Ok(appointmentStatus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}