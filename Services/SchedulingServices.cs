using Microsoft.EntityFrameworkCore;
using System.Linq;
using PresMed.Data;
using PresMed.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PresMed.Services {
    public class SchedulingServices : ISchedulingServices {

        private readonly BancoContext _context;

        public SchedulingServices(BancoContext context) {
            _context = context;
        }

        public async Task<List<Scheduling>> FindByDateAndIdAsync(int id, DateTime AttendenceDate) {

            try {

                DateTime yesterday = AttendenceDate.Subtract(TimeSpan.FromDays(1));
                DateTime tomorrow = AttendenceDate.AddDays(1);

                return await _context.Scheduling.Include(x => x.Doctor).Include(x => x.Patient).Where(x => x.Doctor.Id == id && x.HourAttendence < tomorrow && x.HourAttendence > yesterday).OrderBy(x => x.HourAttendence).ToListAsync();
            }
            catch (Exception e) {
                throw new Exception($"Erro listar {e.Message}");
            }
        }
    }
}
