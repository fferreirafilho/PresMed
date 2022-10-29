using PresMed.Data;
using PresMed.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace PresMed.Services {
    public class AttendanceServices : IAttendanceServices {

        private readonly BancoContext _context;

        public AttendanceServices(BancoContext context) {
            _context = context;
        }

        public async Task<Scheduling> FindByIdAsync(int id) {
            return await _context.Scheduling.FirstOrDefaultAsync(x => x.Id == id);
        }

    }
}
