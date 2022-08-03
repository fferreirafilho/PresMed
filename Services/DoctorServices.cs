using PresMed.Data;
using PresMed.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PresMed.Services {
    public class DoctorServices : IDoctorServices {
        private readonly BancoContext _context;

        public DoctorServices(BancoContext context) {
            _context = context;
        }

        public async Task InsertAsync(Doctor doctor) {
            _context.Doctor.Add(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Doctor>> FindAllAsync() {
            return await _context.Doctor.ToListAsync();
        }
        public async Task<Doctor> FindByIdAsync(int id) {
            return await _context.Doctor.FirstOrDefaultAsync(obj => obj.Id == id);
        }
    }
}
