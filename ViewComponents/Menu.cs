using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PresMed.Models;
using System.Threading.Tasks;

namespace PresMed.ViewComponents {
    public class Menu : ViewComponent {
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<IViewComponentResult> InvokeAsync() {

            string sessionUser = HttpContext.Session.GetString("sessionLoggedUser");
            if (string.IsNullOrEmpty(sessionUser)) return null;

            Person person = JsonConvert.DeserializeObject<Person>(sessionUser);

            return View(person);
        }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    }
}
