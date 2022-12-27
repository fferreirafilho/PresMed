using System;
using System.ComponentModel.DataAnnotations;

namespace PresMed.Models {
    public class Time {

        public int Id { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [DataType(DataType.Time)]
        [Display(Name = "Horario inicial de atendimento")]
        public DateTime InitialHour { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [DataType(DataType.Time)]
        [Display(Name = "Horario final de atendimento")]
        public DateTime FinalHour { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Tempo de atendimento")]
        public DateTime ServiceTime { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Dia inicial")]
        public DateTime InitialDay { get; set; }

        [Display(Name = "Dia Final")]
        public DateTime? FinalDay { get; set; }

        public int HourPerDay { get; set; }

        public Person Person { get; set; }

        public Time() { }

        public Time(DateTime initialHour, DateTime finalHour, Person person, DateTime serviceTime, int hourPerDay, DateTime itinialDay) {
            InitialHour = initialHour;
            FinalHour = finalHour;
            Person = person;
            ServiceTime = serviceTime;
            HourPerDay = hourPerDay;
            InitialDay = itinialDay;
        }
    }
}
