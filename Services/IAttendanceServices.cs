using PresMed.Models;
using System;
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
        public Task InsertMedicalCertificateAsync(MedicalCertificate medicalCertificate);
        public Task UpdateMedicalCertificateAsync(MedicalCertificate medicalCertificate);
        public Task<MedicalCertificate> FindMedicalCertificateByAttendanceId(int id);
        public Task<List<Attendance>> FindAttendanceByPatientId(int id);
        public Task<List<Attendance>> FindAttendanceByDoctorIdAndDate(int id, DateTime initial, DateTime final);
        public Task<List<Scheduling>> FindSchedulingByDoctorIdAndDate(int id, DateTime initial, DateTime final);

    }
}
