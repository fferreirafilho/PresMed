using PresMed.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PresMed.Services {
    public interface IDoctorServices {
        public Task InsertAsync(Doctor doctor);

        public Task<List<Doctor>> FindAllAsync();

        public Task<Doctor> FindByIdAsync(int id);
    }
}
