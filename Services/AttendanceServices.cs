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
        public async Task InsertAsync(Attendance attendance) {
            await _context.Attendance.AddAsync(attendance);
            await _context.SaveChangesAsync();
        }
    }
}
