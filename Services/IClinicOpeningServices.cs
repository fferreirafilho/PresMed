using PresMed.Migrations;
using PresMed.Models;
using System.Threading.Tasks;

namespace PresMed.Services {
    public interface IClinicOpeningServices {

        public Task<ClinicOpening> ListAsync();

        public Task UpdateAsync(ClinicOpening clinic);
    }
}
