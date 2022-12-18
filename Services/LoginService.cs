using Microsoft.EntityFrameworkCore;
using PresMed.Data;
using PresMed.Models;
using PresMed.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace PresMed.Services {
    public class LoginService : ILoginService {
        private readonly BancoContext _context;
        public LoginService(BancoContext context) {
            _context = context;
        }

        public async Task ChangePasswordAsync(Person person) {
            _context.Person.Update(person);
            await _context.SaveChangesAsync();
        }

        public async Task<Person> FindByLoginAsync(string user) {
            return await _context.Person.FirstOrDefaultAsync(x => x.User.ToUpper() == user.ToUpper());
        }
    }
}
