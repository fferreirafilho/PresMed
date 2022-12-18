using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using PresMed.Models;

namespace PresMed.Filters {
    public class PageOnlyDoctor : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext context) {

            string sessionUser = context.HttpContext.Session.GetString("sessionLoggedUser");
            if (string.IsNullOrEmpty(sessionUser)) {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "login" }, { "action", "Index" } });

            }
            else {
                Person person = JsonConvert.DeserializeObject<Person>(sessionUser);
                if (person == null) {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "login" }, { "action", "Index" } });
                }
                if (person.PersonType != Models.Enums.PersonType.Doctor) {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Restricted" }, { "action", "Index" } });
                }

            }

            base.OnActionExecuting(context);
        }
    }
}