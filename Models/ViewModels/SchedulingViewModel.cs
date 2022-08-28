using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PresMed.Models.ViewModels {
    public class SchedulingViewModel {
        public List<Person> Doctors { get; set; }
        public DateTime AttendenceDate { get; set; }
        public Time Time { get; set; }
        public List<Scheduling> schedulings { get; set; }
    }

}
