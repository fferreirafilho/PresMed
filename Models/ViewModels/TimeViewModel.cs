using System.Collections.Generic;
using System.Threading.Tasks;

namespace PresMed.Models.ViewModels {
    public class TimeViewModel {
        public List<Person> Person { get; set; }
        public List<Time> Time { get; set; }

        public TimeViewModel() { }

    }
}
