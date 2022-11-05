using System;
using System.ComponentModel.DataAnnotations;

namespace PresMed.Models {
    public class Prescription {
        public int Id { get; set; }
        public int AttendanceId { get; set; }
        public int MedicineId { get; set; }

        public DateTime Time { get; set; }
        public int Days { get; set; }
        public string Dosage { get; set; }

        public Prescription() { }
        public Prescription(int id, int attendanceId, int medicineId, DateTime time, int days, string dosage) {
            Id = id;
            AttendanceId = attendanceId;
            MedicineId = medicineId;
            Time = time;
            Days = days;
            Dosage = dosage;
        }

    }
}
