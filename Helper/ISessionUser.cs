using PresMed.Models;

namespace PresMed.Helper {
    public interface ISessionUser {
        void createSessionUser(Person person);
        void removeSessionUser();
        Person FindSessionUser();
    }
}
