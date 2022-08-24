using PresMed.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PresMed.Services {
    public interface ITimeServices {

        public Task InsertAsync(Time time);

        public Task UpdateAsync(Time time);

        public Task<List<Time>> FindAllActiveAsync();
    }
}
