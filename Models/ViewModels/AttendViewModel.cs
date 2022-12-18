using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PresMed.Models.ViewModels {
    public class AttendViewModel {

        public int Id { get; set; }
        public Person Doctor { get; set; }
        public Person Patient { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Prontuário")]
        public string Report { get; set; }
        public Scheduling Scheduling { get; set; }
        public List<Attendance> listAttendance { get; set; }
        public AttendViewModel() { }

    }
}
