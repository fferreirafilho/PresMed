using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PresMed.Models;
using System.Threading.Tasks;

namespace PresMed.ViewComponents {
    public class Menu : ViewComponent{
        public async Task<IViewComponentResult> InvokeAsync() {

            string sessionUser = HttpContext.Session.GetString("sessionLoggedUser");
            if (string.IsNullOrEmpty(sessionUser)) return null;

            Person person = JsonConvert.DeserializeObject<Person>(sessionUser);

            return View(person);
        }
    }
}
