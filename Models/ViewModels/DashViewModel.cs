

using System.Collections.Generic;

namespace PresMed.Models.ViewModels {
    public class DashViewModel {
        public List<Scheduling> Agendado { get; set; }
        public List<Scheduling> Confirmado { get; set; }
        public List<Scheduling> Finalizado { get; set; }
        public List<Scheduling> Em_atendimento { get; set; }
        public List<Scheduling> Aguardando_atendimento { get; set; }
    }
}
