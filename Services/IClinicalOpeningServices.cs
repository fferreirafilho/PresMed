using System.Threading.Tasks;

namespace PresMed.Services {
    public interface IClinicalOpeningServices {

        public Task<ClinicalOpeningServices> ListAsync();
    }
}
