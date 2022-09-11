using System;

namespace PresMed.Models {
    public class ClinicOpening {
        public int Id { get; set; }
        public DateTime InitialHour { get; set; }
        public DateTime EndHour { get; set; }

        public ClinicOpening() { }
        public ClinicOpening(DateTime initialHour, DateTime endHour) {
            InitialHour = initialHour;
            EndHour = endHour;
        }
    }
}
