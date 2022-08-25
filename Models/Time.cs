using System;
using System.ComponentModel.DataAnnotations;

namespace PresMed.Models {
    public class Time {

        [Required]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Horario inicial de atendimento")]
        public DateTime InitialHour { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Horario final de atendimento")]
        public DateTime FinalHour { get; set; }

        public Person Person { get; set; }

        public Time() { }

        public Time(DateTime initialHour, DateTime finalHour, Person person) {
            InitialHour = initialHour;
            FinalHour = finalHour;
            Person = person;
        }
    }
}
