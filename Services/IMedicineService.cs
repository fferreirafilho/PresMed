using PresMed.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PresMed.Services {
    public interface IMedicineService {

        public Task InsertAsync(Medicine medicine);

        public Task<List<Medicine>> FindAllAsync();

        public Task<Medicine> FindByIdAsync(int id);

        public Task UpdateAsync(Medicine medicine);
        public Medicine TransformUpperCase(Medicine medicine);

    }
}
