using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DocAppointApi.Datas;
using DocAppointApi.Models;

namespace DocAppointApi.Repositories
{
    public class RDVRepository
    {
        private readonly DbContextRed _dbcontext;

        public RDVRepository(DbContextRed dbcontext)
        {
            _dbcontext = dbcontext;
        }
        
        public async Task<Patient> GetPatientById(int patientId)
        {
            return await _dbcontext.Patients.FirstOrDefaultAsync(p => p.PatientId == patientId);
        }

        public async Task<Medecin> GetMedecinById(int medocId)
        {
            return await _dbcontext.Medecins.FirstOrDefaultAsync(m => m.medocId == medocId);
        }

        public async Task<RDVM> CreateAppointment(RDVM appointment)
        {
            _dbcontext.RDVMs.Add(appointment);
            await _dbcontext.SaveChangesAsync();
            return appointment;
        }

        public async Task<RDVM> GetAppointmentById(int RDVMId)
        {
            return await _dbcontext.RDVMs.FirstOrDefaultAsync(a => a.RDVId == RDVMId);
        }

        public async Task SaveChanges()
        {
            await _dbcontext.SaveChangesAsync();
        }
    }
}