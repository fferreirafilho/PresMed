using PresMed.Data;
using PresMed.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace PresMed.Services {
    public class AttendanceServices : IAttendanceServices {

        private readonly BancoContext _context;

        public AttendanceServices(BancoContext context) {
            _context = context;
        }
        public async Task InsertAttendanceAsync(Attendance attendance) {
            await _context.Attendance.AddAsync(attendance);
            await _context.SaveChangesAsync();
        }

        public async Task<Attendance> FindBySchedulingId(int id) {
            return await _context.Attendance.FirstOrDefaultAsync(x => x.Scheduling.Id == id);
        }

        public async Task<Attendance> FindAttendanceByIdAsync(int id) {
            return await _context.Attendance.Include(x => x.Scheduling).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertPrescriptionAsync(Prescription prescription) {
            await _context.Prescription.AddAsync(prescription);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Attendance attendance) {
            _context.Attendance.Update(attendance);
            await _context.SaveChangesAsync();
        }


        public async Task DeletePrescriptionAsync(int id) {
            var prescription = await FindPrescriptionById(id);
            _context.Prescription.Remove(prescription);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Prescription>> FindPrescriptionByAttendanceId(int id) {
            return await _context.Prescription.Include(x => x.Attendance).Include(x => x.Medicine).Where(x => x.Attendance.Id == id).ToListAsync();
        }

        public Task<Prescription> FindPrescriptionById(int id) {
            return _context.Prescription.Include(x => x.Attendance).Include(x => x.Attendance.Scheduling).Include(x => x.Attendance.Patient).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
