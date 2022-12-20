using PresMed.Data;
using System.Threading.Tasks;
using System.Linq;
using PresMed.Models;
using Microsoft.EntityFrameworkCore;

namespace PresMed.Services {
    public class ClinicSetingsServices : IClinicSetingsServices {
        private readonly BancoContext _context;

        public ClinicSetingsServices(BancoContext context) {
            _context = context;
        }
        public async Task<ClinicSetings> ListAsync() {
            return await _context.ClinicSetings.FirstAsync();
        }

        public async Task UpdateAsync(ClinicSetings clinic) {
            _context.ClinicSetings.Update(clinic);
            await _context.SaveChangesAsync();
        }
    }
}
