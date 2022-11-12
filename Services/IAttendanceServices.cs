using PresMed.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PresMed.Services {
    public interface IAttendanceServices {

        public Task InsertAttendanceAsync(Attendance attendance);
        public Task<Attendance> FindBySchedulingId(int id);
        public Task<Attendance> FindAttendanceByIdAsync(int id);
        public Task<Prescription> FindPrescriptionById(int id);
        public Task<List<Prescription>> FindPrescriptionByAttendanceId(int id);
        public Task InsertPrescriptionAsync(Prescription prescription);
        public Task DeletePrescriptionAsync(int id);
        public Task UpdateAsync(Attendance attendance);


    }
}
