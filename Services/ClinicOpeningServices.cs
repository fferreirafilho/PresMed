using PresMed.Data;
using System.Threading.Tasks;
using System.Linq;
using PresMed.Models;
using Microsoft.EntityFrameworkCore;

namespace PresMed.Services {
    public class ClinicOpeningServices : IClinicOpeningServices {
        private readonly BancoContext _context;

        public ClinicOpeningServices(BancoContext context) {
            _context = context;
        }
        public async Task<ClinicOpening> ListAsync() {
            return await _context.ClinicOpening.FirstAsync();
        }

        public async Task UpdateAsync(ClinicOpening clinic) {
            _context.ClinicOpening.Update(clinic);
            await _context.SaveChangesAsync();
        }
    }
}
