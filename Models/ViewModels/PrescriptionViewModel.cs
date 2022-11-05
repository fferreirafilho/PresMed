using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PresMed.Models.ViewModels {
    public class PrescriptionViewModel {
        public int AttendanceId { get; set; }
        public Medicine Medicine { get; set; }
        public IEnumerable<Medicine> Medicines { get; set; }
        public IEnumerable<Prescription> Prescription { get; set; }
    }
}
