using PresMed.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PresMed.Services {
    public interface IDoctorServices {
        public Task InsertAsync(Person doctor);

        public Task<List<Person>> FindAllActiveAsync();
        public Task<List<Person>> FindAllDisableAsync();

        public Task<Person> FindByIdAsync(int id);

        public Task UpdateAsync(Person doctor);
    }
}
