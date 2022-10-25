using System;
using System.ComponentModel.DataAnnotations;

namespace PresMed.Models {
    public class Prescription {
        public int Id { get; set; }

        public int AttendanceId { get; set; }
        public Attendance Attendance { get; set; }
        public int Medicineid { get; set; }
        public Medicine Medicine { get; set; }

        public DateTime Time { get; set; }
        public int Days { get; set; }
        public string Dosage { get; set; }

    }
}
