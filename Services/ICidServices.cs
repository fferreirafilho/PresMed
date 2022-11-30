using PresMed.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PresMed.Services {
    public interface ICidServices {
        public Task InsertAsync(Cid cid);

        public Task<List<Cid>> FindAllAsync();

        public Task<Cid> FindByIdAsync(int id);

        public Task UpdateAsync(Cid cid);
        public Cid TransformUpperCase(Cid cid);
    }
}
