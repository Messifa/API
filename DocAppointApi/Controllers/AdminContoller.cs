using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using DocAppointApi.Models;
using DocAppointApi.Services;
using DocAppointApi.Repositories;
using Microsoft.EntityFrameworkCore;
using DocAppointApi.Datas;

namespace DocAppointApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly AdminService _adminService;
        private readonly DbContextRed _dbContext;



        public AdminController(UserService userService, AdminService adminService, DbContextRed dbContext)
        {
            _userService = userService;
            _adminService = adminService;
            _dbContext = dbContext;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAdmin([FromBody] Adminis admin)
        {
            try
            {

                _dbContext.Adminis.Add(admin);
                await _dbContext.SaveChangesAsync();

                return Ok("vous etes inscrits");
            }
            catch (Exception ex)
            {
                // En cas d'erreur, renvoyer une réponse "BadRequest" avec le message d'erreur
                throw ex;
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAdmin([FromBody] Adminis adminis)
        {
            if (adminis == null)
            {
                return BadRequest("Objet administrateur non valide.");
            }
            try
            {

                var authenticatedAdminis = await _userService.LoginAdmin(adminis.Email, adminis.Password);


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

        [HttpPost("Approve")]
        public IActionResult validation(int sperid)
        {
            try
            {
                var app = _dbContext.RDVMs.FirstOrDefault(a => a.RDVId == sperid );
                if (app == null)
                {
                    return BadRequest("Demande de rendez-vous introuvable.");
                }

                var valideapp = new Specialite
                {
                    AppointmentTime = app.Datedb,
                    Description = app.RDVlibelle,
                    Category = app.Category,
                    PatientId = app.PatientId,
                    DoctorId = app.medocId
                };
                _dbContext.Specialites.Add(valideapp);
                _dbContext.RDVMs.Remove(app);
                _dbContext.SaveChanges();
                return Ok(valideapp);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Une erreur s'est produite lors de l'approbation du rendez-vous.");
            }
        }
        [HttpPost("reject")]
        public IActionResult Refusation(int appointmentId)
        {
            try
            {
                var pendingAppointment = _dbContext.RDVMs.FirstOrDefault(p => p.RDVId == appointmentId);

                if (pendingAppointment == null)
                {
                    return NotFound("Demande de rendez-vous introuvable.");
                }

                _dbContext.RDVMs.Remove(pendingAppointment);
                _dbContext.SaveChanges();

                return Ok("Rendez-vous refusé avec succès.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Une erreur s'est produite lors du refus du rendez-vous.");
            }
        }
    


       [HttpGet("patients")]
        public async Task<ActionResult<List<Patient>>> GetPatients()
        {
            var lespatients = await _userService.GetPatientsAsync();
            return Ok(lespatients);
        }

        [HttpGet("medecins")]
        public async Task<ActionResult<List<Medecin>>> GetMedecins()
        {
               var medecins = await _userService.GetMedecinsAsync();
               
                return Ok(medecins);
          
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







    }
}