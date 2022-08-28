using PresMed.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PresMed.Services {
    public interface IPatientServices {
        public Task InsertAsync(Person patient);
        public Task<List<Person>> FindAllAsync();
        public Task<List<Person>> FindAllActiveAsync();
        public Task<List<Person>> FindAllDisableAsync();
        public Task<Person> FindByIdAsync(int id);
        public Task UpdateAsync(Person patient);
        public Person TransformUpperCase(Person patient);
    }
}
