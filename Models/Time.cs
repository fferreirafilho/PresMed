using System;
using System.ComponentModel.DataAnnotations;

namespace PresMed.Models {
    public class Time {

        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime InitialHour { get; set; }
        [Required]
        public DateTime FinalHour { get; set; }
        [Required]
        public Person Person { get; set; }

        public Time() { }

        public Time(DateTime initialHour, DateTime finalHour, Person person) {
            InitialHour = initialHour;
            FinalHour = finalHour;
            Person = person;
        }
    }
}
