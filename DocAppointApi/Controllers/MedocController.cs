using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using DocAppointApi.Models;
using DocAppointApi.Services;
using Microsoft.EntityFrameworkCore;
using DocAppointApi.Datas;

namespace DocAppointApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedocController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ConService _conService;
        private readonly TraitService _traitService;
        private readonly DbContextRed _dbContext;

        public MedocController(UserService userService, ConService conService, TraitService traitService, DbContextRed dbContext)
        {
            _userService = userService;
            _conService = conService;
            _traitService = traitService;
            _dbContext = dbContext;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterMedecin([FromBody] Medecin medecin)
        {
            try
            {

                _dbContext.Medecins.Add(medecin);
                await _dbContext.SaveChangesAsync();

                return Ok("vous etes inscrits");
            }
            catch (Exception ex)
            {
                // En cas d'erreur, renvoyer une réponse "BadRequest" avec le message d'erreur
                throw ex;
            }
        }


        private bool CheckIfMedecinExists(string Email)
        {
            // Utilisez le service UserService pour vérifier si l'utilisateur existe déjà dans la base de données
            var user = _userService.GetUserByEmail(Email);

            // Retournez true si l'utilisateur existe, sinon retournez false
            return user != null;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginMedecin([FromBody] Medecin medecin)
        {
            try
            {
                // Rechercher le médecin dans la base de données à l'aide de "medecin.Email" et "medecin.Password"
                var authenticatedMedecin = await _userService.LoginMedecin(medecin.Email, medecin.Password);

                // Si le médecin est trouvé, créer une session utilisateur et renvoyer une réponse "Ok"
                if (authenticatedMedecin != null)
                {
                    // Créer une session utilisateur (par exemple, utiliser un jeton JWT)
                    var token = CreateSessionToken(authenticatedMedecin);

                    // Retourner une réponse "Ok" avec le jeton
                    return Ok("vous etes connectes.");
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
        [HttpPost("consult")]
        public async Task<IActionResult> Register([FromBody] Consecration medo)
        {
            try
            {
                // Créez un nouvel objet User à partir des propriétés de Patient
                var medoy = new Consecration
                {
                    consDate = medo.consDate,
                    consName = medo.consName,
                    consPoids = medo.consPoids,
                    consTaille = medo.consTaille,
                    consTension = medo.consTension,
                   
                    // Ajoutez d'autres propriétés communes entre Patient et User ici
                };

                // Enregistrez le nouvel utilisateur dans la base de données
                var registeredUser = await _conService.Register(medoy);

                // Générer le jeton de session pour le patient nouvellement enregistré
                var token = _conService.CreateSessionToken(registeredUser);

                // Retourner une réponse "Ok" avec le patient créé et le jeton de session
                return Ok("une consultation reussie");
            }
            catch (Exception ex)
            {
                // En cas d'erreur, renvoyer une réponse "BadRequest" avec le message d'erreur
                // return BadRequest(ex.Message);
                throw ex;
                return BadRequest("Une erreur s'est produite");
            }
        }

       

        [HttpPost("traitemt")]
        public async Task<IActionResult> Regis([FromBody] TraitemtP traits)
        {
            try
            {
                // Créez un nouvel objet User à partir des propriétés de Patient
                var trait = new TraitemtP
                {
                    Pid = traits.Pid,
                    MedocTr = traits.MedocTr,
                    MedocAvis = traits.MedocAvis
                    

                    // Ajoutez d'autres propriétés communes entre Patient et User ici
                };

                // Enregistrez le nouvel utilisateur dans la base de données
                var registUser = await _traitService.Regis(trait);

                // Générer le jeton de session pour le patient nouvellement enregistré
                var token = _traitService.CreateSessionToken(registUser);

                // Retourner une réponse "Ok" avec le patient créé et le jeton de session
                return Ok(" reussie");
            }
            catch (Exception ex)
            {
                // En cas d'erreur, renvoyer une réponse "BadRequest" avec le message d'erreur
                // return BadRequest(ex.Message);
                throw ex;
                return BadRequest("Une erreur s'est produite");
            }
        }

        private object CreateSessionToken(User authenticatedMedecin)
        {
            try
            {
                // Utilisez le UserService pour générer le jeton de session
                var token = _userService.CreateSessionToken(authenticatedMedecin);

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