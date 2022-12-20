using PresMed.Migrations;
using PresMed.Models;
using System.Threading.Tasks;

namespace PresMed.Services {
    public interface IClinicSetingsServices {

        public Task<ClinicSetings> ListAsync();

        public Task UpdateAsync(ClinicSetings clinic);
    }
}
