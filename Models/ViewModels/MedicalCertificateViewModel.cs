using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace PresMed.Models.ViewModels {
    public class MedicalCertificateViewModel {


        public int Id { get; set; }
        public Attendance Attendance { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        public int Cid { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Dias")]
        public int Days { get; set; }
        public IEnumerable<Cid> ListCid { get; set; }
    }
}
