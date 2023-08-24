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

       // public async Task<List<Medecin>> GetAvailableDoctors(DateTime appointmentDateTime)
      //  {
      //      var availableDoctors = await _dbcontext.Medecins
       //         .Where(m => m.RDVM.Any(r => r.Datedb == appointmentDateTime))
       //         .ToListAsync();

        //    return availableDoctors;
      //  }

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