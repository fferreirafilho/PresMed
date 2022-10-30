using System;
using System.ComponentModel.DataAnnotations;

namespace PresMed.Models {
    public class Prescription {
        public int Id { get; set; }
        public Attendance Attendance { get; set; }
        public Medicine Medicine { get; set; }

        public DateTime Time { get; set; }
        public int Days { get; set; }
        public string Dosage { get; set; }

        public Prescription() { }
        public Prescription(int id, Attendance attendance, Medicine medicine, DateTime time, int days, string dosage) {
            Id = id;
            Attendance = attendance;
            Medicine = medicine;
            Time = time;
            Days = days;
            Dosage = dosage;
        }

    }
}
