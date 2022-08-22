using PresMed.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PresMed.Services {
    public interface IProceduresServices {
        public Task InsertAsync(Procedures procedure);

        public Task<List<Procedures>> FindAllActiveAsync();
        public Task<List<Procedures>> FindAllDisableAsync();

        public Task<Procedures> FindByIdAsync(int id);

        public Task UpdateAsync(Procedures procedure);
        public Procedures TransformUpperCase(Procedures procedure);
    }
}
