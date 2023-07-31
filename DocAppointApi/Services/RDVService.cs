using System;
using System.Threading.Tasks;
using DocAppointApi.Models;
using DocAppointApi.Repositories;
using DocAppointApi.Services;

namespace DocAppointApi.Services
{
    public class RDVService
    {
        private readonly RDVRepository _rdvRepository;

        public RDVService(RDVRepository rdvRepository)
        {
            _rdvRepository = rdvRepository;
        }

        public async Task<RDVM> CreateAppointment(RDVM appointment)
        {
            try
            {
                // Vérifier si le patient existe dans la base de données
                var existingPatient = await _rdvRepository.GetPatientById(appointment.PatientId);
                if (existingPatient == null)
                {
                    throw new Exception("Le patient spécifié n'existe pas.");
                }

                // Vérifier si le médecin existe dans la base de données
                var existingMedecin = await _rdvRepository.GetMedecinById(appointment.medocId);
                if (existingMedecin == null)
                {
                    throw new Exception("Le médecin spécifié n'existe pas.");
                }

                // Enregistrer le nouveau rendez-vous dans la base de données
                var createdAppointment = await _rdvRepository.CreateAppointment(appointment);

                return createdAppointment;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la création du rendez-vous : " + ex.Message);
            }
        }
    }
}
