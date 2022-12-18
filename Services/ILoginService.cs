using PresMed.Models;
using PresMed.Models.ViewModels;
using System.Threading.Tasks;

namespace PresMed.Services {
    public interface ILoginService {

        public Task<Person> FindByLoginAsync(string user);
        public Task ChangePasswordAsync(Person person);

    }
}
