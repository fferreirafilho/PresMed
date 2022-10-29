using PresMed.Models;
using System.Threading.Tasks;

namespace PresMed.Services {
    public interface IAttendanceServices {

        public Task<Scheduling> FindByIdAsync(int id);

    }
}
