using System.Collections.Generic;

namespace PresMed.Models {
    public class Attendance {
        public int Id { get; set; }
        public Person Doctor { get; set; }
        public Person Patient { get; set; }
        public string Report { get; set; }

        public ICollection<Medicine> Medicines { get; set; }


        public ICollection<Prescription> AttendanceMedicines { get; set; }
        public Attendance() { }
        public Attendance(int id, Person doctor, Person patient, string report) {
            Id = id;
            Doctor = doctor;
            Patient = patient;
            Report = report;
        }
    }
}
