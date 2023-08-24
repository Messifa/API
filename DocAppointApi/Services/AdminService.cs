using System;
using System.Threading.Tasks;
using DocAppointApi.Datas;
using DocAppointApi.Models;
using DocAppointApi.Repositories;
using DocAppointApi.Services;
using Microsoft.EntityFrameworkCore;

namespace DocAppointApi.Services
{
    public class AdminService
    {
        private readonly DbContextRed _dbContext;
        private readonly RDVRepository _rdvRepository;

        public AdminService(RDVRepository rdvRepository, DbContextRed dbContext)
        {
            _rdvRepository = rdvRepository;
            _dbContext = dbContext;
        }
       


        public async Task<RDVM> ValidateAppointment(int RDVMId, string RDVlibelle)
        {
            try
            {
                var appointment = await _rdvRepository.GetAppointmentById(RDVMId);
                if (appointment == null)
                {
                    throw new Exception("Le rendez-vous spécifié n'a pas été trouvé.");
                }

                appointment.IsValidated = true;
                // Mettez à jour d'autres propriétés du rendez-vous si nécessaire

                await _rdvRepository.SaveChanges();

                return appointment;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la validation du rendez-vous : " + ex.Message);
            }
        }

        public async Task<Statut> GetAppointmentStatus(int appointmentId)
        {
            try
            {
                var appointment = await _rdvRepository.GetAppointmentById(appointmentId);
                if (appointment == null)
                {
                    throw new Exception("Le rendez-vous spécifié n'a pas été trouvé.");
                }

                var status = new Statut
                {
                    RDVPId = appointment.RDVId,
                    val = appointment.IsValidated ? "Validé" : "En attente de validation"
                };

                return status;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la récupération du statut du rendez-vous : " + ex.Message);
            }
        }

       
    }
}
