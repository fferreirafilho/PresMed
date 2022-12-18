using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace PresMed.Models {
    public class MedicalCertificate {

        public int Id { get; set; }
        public Attendance Attendance { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        public Cid Cid { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Dias")]
        public int Days { get; set; }


    }
}
