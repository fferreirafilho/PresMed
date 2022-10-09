using PresMed.Models;
using PresMed.Models.ViewModels;
using System.Threading.Tasks;

namespace PresMed.Services {
    public interface ILoginService {

        Task<Person> FindByLoginAsync(string user);

    }
}
