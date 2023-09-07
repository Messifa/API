using Microsoft.AspNetCore.Mvc;
using DocAppointApi.Models;
using DocAppointApi.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Cors.Infrastructure;
using DocAppointApi.Repositories;
using DocAppointApi.Datas;
using Microsoft.EntityFrameworkCore;

namespace DocAppointApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ConService _conService;
        private readonly TraitService _traitService;
        private readonly RDVRepository _rdvRepository;
        private readonly DbContextRed _dbContext;
        private readonly char[] _tokenSecretKey;

        public PatientController(UserService userService, ConService conService, TraitService traitService,  DbContextRed dbContext)
        {
            _userService = userService;
            _conService = conService;
            _traitService = traitService;
            _dbContext = dbContext;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterPatient([FromBody] Patient patient)
        {  
            try
            {
                _dbContext.Patients.Add(patient);
                await _dbContext.SaveChangesAsync();    
                
                return Ok("vous etes inscrits");
            }
            catch (Exception ex)
            {
                // En cas d'erreur, renvoyer une réponse "BadRequest" avec le message d'erreur
                return BadRequest("Les informations de connexion sont invalides.");
            }
        }

     
    
        
        [HttpPost("login")]
        public async Task<IActionResult> LoginPatient([FromBody] Patient patient)
        {
            if (string.IsNullOrEmpty(patient.Email))
            {
                return BadRequest("L'email est obligatoire.");
            }
            try
            {
                // Rechercher le médecin dans la base de données à l'aide de "medecin.Email" et "medecin.Password"
                var authenticatedPatient = await _userService.LoginPatient(patient.Email, patient.Password);

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
        [HttpPost("BookAppointment")]
        public IActionResult BookAppointment(RDVM Appointmt)
        {
            try
            {
                var newRDV = new RDVM
                {
                    Datedb = Appointmt.Datedb,
                    RDVlibelle = Appointmt.RDVlibelle,
                    Category = Appointmt.Category,

                };
                var availableDoctors = GetAvailableDoctors(Appointmt.Category, Appointmt.Datedb);
                if (availableDoctors == null || availableDoctors.Count == 0)
                {
                    return BadRequest("Aucun médecin disponible à cette heure dans cette catégorie.");
                }

                var selectedDoctor = availableDoctors.FirstOrDefault(d => d.Specialite == Appointmt.Category);
                if (selectedDoctor == null)
                {
                    return BadRequest("Médecin introuvable.");
                }

                _dbContext.RDVMs.Add(newRDV);
                _dbContext.SaveChanges();

                return Ok("Votre demande de rendez-vous a été enregistrée.");
            }
            catch (Exception ex)
            {
                return BadRequest("Une erreur s'est produite lors de la création du RDV.");
            }
        }

        private List<Medecin> GetAvailableDoctors(string category, DateTime datedb)
        {
            return _dbContext.Medecins
                .Where(d => d.Specialite == category 
                            )
                .ToList();
        }

        //[HttpPost("Appoint")]
       // public IActionResult Appointment(RDVM Appointmt)
       // {
          //  try
          //  {
              //  var availableDoctors = GetAvailableDoctors(Appointmt.Category, Appointmt.Datedb);
              //  if (availableDoctors == null || availableDoctors.Count == 0)
              //  {
              //      return BadRequest("Aucun médecin disponible à cette heure dans cette catégorie.");
              //  }

           //     var newRDV = new RDVM
            //    {
              //      Datedb = Appointmt.Datedb,
                //    RDVlibelle = Appointmt.RDVlibelle,
                  //  Category = Appointmt.Category,
                    //medocId = Appointmt.medocId
             //   };

            //    _dbContext.RDVMs.Add(newRDV);
            //    _dbContext.SaveChanges();

             //   return Ok("Votre demande a été enregistrée");
          //  }
          //  catch (Exception ex)
         //   {
             //   return BadRequest("Une erreur s'est produite lors de la création du RDV.");
         //   }
       // }

       // private List<Medecin> GetAvailableDoctors(string category, DateTime datedb)
       // {
          //  return _dbContext.Medecins
          //      .Where(d => d.Specialite == category &&
          //                  !_dbContext.RDVMs.Any(a => a.medocId == d.userId && a.Datedb == datedb))
          //      .ToList();
       // }

        [HttpGet("consultations")]
        public async Task<IActionResult> GetConsultationsByPatientLastName(string lastName)
        {
            try
            {
                var consultations = await _conService.GetConsultationsByPatientLastNameAsync(lastName);
                var consultationInfoList = consultations.Select(c => new Consecration
                {
                    consName = c.consName,
                   consDate = c.consDate,
                    consPoids = c.consPoids,
                    consTaille = c.consTaille,
                    consTension = c.consTension
                }).ToList();

                return Ok(consultationInfoList);
            }
            catch (Exception ex)
            {
                return BadRequest("Une erreur est survenue lors de la récuperation des traitements : " + ex.Message);
            }
        }
        [HttpGet("traitmt")]
        public async Task<IActionResult> GetTraitement(string LastName)
        {
            try
            {
                var traitemt = await _traitService.GetTraitementAsync(LastName);
                var traitemtInfoList = traitemt.Select(t => new TraitemtP
                {
                    Pid = t.Pid,
                    MedocTr = t.MedocTr,

                }).ToList();

                return Ok(traitemtInfoList);
            }

            catch (Exception ex)
            {
                return BadRequest("Une erreur est survenue lors de la récupération des consultations : " + ex.Message);
            }
        }
        private bool CheckIfUserExists(string Email)
        {
            // Utilisez le service UserService pour vérifier si l'utilisateur existe déjà dans la base de données
            var user = _userService.GetUserByEmail(Email);

            // Retournez true si l'utilisateur existe, sinon retournez false
            return user != null;
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
