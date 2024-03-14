using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using NetCoreSeguridadEmpleados.Repositories;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NetCoreSeguridadEmpleados.Filters
{
    public class AuthorizeEmpleadosAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            string controller = context.RouteData.Values["controller"].ToString();
            string action = context.RouteData.Values["action"].ToString();
            Debug.WriteLine("Controller: " + controller);
            Debug.WriteLine("Action: " + action);
            ITempDataProvider provider = 
                context.HttpContext.RequestServices.GetService
                <ITempDataProvider>();
            var TempData = provider.LoadTempData(context.HttpContext);
            TempData["controller"] = controller;
            TempData["action"] = action;

            provider.SaveTempData(context.HttpContext, TempData);
            if (user.Identity.IsAuthenticated == false)
            {
                context.Result = this.GetRoute("Managed", "Login");
            }
            else
            {
                //Comprobar el rol para permitir acceso
                if (user.IsInRole("PRESIDENTE") == false
                    && user.IsInRole("ANALISTA") == false
                    && user.IsInRole("DIRECTOR") == false)
                {
                    context.Result = this.GetRoute("Managed", "ErrorAcceso");
                }
            }
        }
        private RedirectToRouteResult GetRoute
            (string controller, string action)
        {
            RouteValueDictionary ruta = new RouteValueDictionary
                (
                    new { controller = controller, action = action });
            RedirectToRouteResult result = new RedirectToRouteResult(ruta);
            return result;
        }
    }
}
