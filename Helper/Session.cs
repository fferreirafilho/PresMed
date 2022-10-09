using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PresMed.Models;

namespace PresMed.Helper {
    public class Session : ISessionUser {

        private readonly IHttpContextAccessor _httpContext;

        public Session(IHttpContextAccessor httpContext) {
            _httpContext = httpContext;
        }

        public void createSessionUser(Person person) {
            string value = JsonConvert.SerializeObject(person);
            _httpContext.HttpContext.Session.SetString("sessionLoggedUser", value);
        }

        public Person FindSessionUser() {
            string sessionUser = _httpContext.HttpContext.Session.GetString("sessionLoggedUser");
            if(string.IsNullOrEmpty(sessionUser)) return null;
            return JsonConvert.DeserializeObject<Person>(sessionUser);
        }

        public void removeSessionUser() {
            _httpContext.HttpContext.Session.Remove("sessionLoggedUser");
        }
    }
}
