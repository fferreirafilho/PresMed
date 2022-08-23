using PresMed.Models;
using System.Threading.Tasks;

namespace PresMed.Services {
    public interface ITimeServices {

        public Task InsertAsync(Time time);

        public Task UpdateAsync(Time time);
    }
}
