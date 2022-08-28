using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PresMed.Models.ViewModels {
    public class SchedulingViewModel {
        public List<Person> Doctors { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        public int IdDoctor { get; set; }

        public Person Patient { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        public DateTime AttendenceDate { get; set; }

        public Time Time { get; set; }

        public List<Scheduling> Schedulings { get; set; }
    }

}
