using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PresMed.Models.ViewModels {
    public class AttendanceViewModel {

        public Person Person { get; set; }
        public IEnumerable<Scheduling> Schedulings { get; set; }
        [Display(Name = "Agenda")]
        public Scheduling Scheduling { get; set; }
        [Display(Name = "Horario")]
        public Time Hour { get; set; }

    }
}
