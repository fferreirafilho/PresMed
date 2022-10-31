using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PresMed.Models.ViewModels {
    public class PrescriptionViewModel {
        public Person Doctor { get; set; }
        public Person Patient { get; set; }
        public Medicine Medicine { get; set; }
        public string Report { get; set; }
        public IEnumerable<Medicine> Medicines { get; set; }
        public IEnumerable<Prescription> Prescription { get; set; }
    }
}
