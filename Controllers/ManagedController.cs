using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NetCoreSeguridadEmpleados.Models;
using NetCoreSeguridadEmpleados.Repositories;
using System.Security.Claims;

namespace NetCoreSeguridadEmpleados.Controllers
{
    public class ManagedController : Controller
    {
        private RepositoryEmpleados repo;
        public ManagedController(RepositoryEmpleados repo)
        {
            this.repo = repo;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            int idEmpleado = int.Parse(password);
            Empleado emp = await this.repo.LogInEmpleadoAsync(username, idEmpleado);
            if(emp != null)
            {
                ClaimsIdentity identity = 
                    new ClaimsIdentity(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        ClaimTypes.Name, ClaimTypes.Role);
                Claim claimName = new Claim(ClaimTypes.Name, emp.Apellido);
                identity.AddClaim(claimName);

                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    userPrincipal);
                return RedirectToAction("PerfilEmpleado", "Empleados");
            }
            else
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectas";
                return View();
            }
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
                );
            return RedirectToAction("Index", "Home");

        }
    }
}
