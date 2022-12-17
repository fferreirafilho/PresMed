using System.ComponentModel.DataAnnotations;
using System;
using iTextSharp.text;
using System.Collections.Generic;

namespace PresMed.Models.ViewModels {
    public class TimeViewModel {

        public int Id { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Horario inicial de atendimento")]
        public DateTime InitialHour { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Horario final de atendimento")]
        public DateTime FinalHour { get; set; }

        [Required]
        [Display(Name = "Tempo de atendimento")]
        public DateTime ServiceTime { get; set; }

        [Required]
        [Display(Name = "Dia inicial")]
        public DateTime InitialDay { get; set; }

        [Display(Name = "Dia Final")]
        public DateTime? FinalDay { get; set; }

        public int HourPerDay { get; set; }

        public Person Person { get; set; }

        public List<Time> ListTime { get; set; }

        public TimeViewModel() { }

    }
}
