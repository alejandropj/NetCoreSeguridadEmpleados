using Microsoft.AspNetCore.Mvc;
using NetCoreSeguridadEmpleados.Models;
using NetCoreSeguridadEmpleados.Repositories;

namespace NetCoreSeguridadEmpleados.Controllers
{
    public class EmpleadosController : Controller
    {
        private RepositoryEmpleados repo;
        public EmpleadosController(RepositoryEmpleados repo)
        {
            this.repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            List<Empleado> empleados = await this.repo.GetEmpleadosAsync();
            return View(empleados);
        }
        public async Task<IActionResult> Details(int idempleado)
        {
            Empleado emp = await this.repo.FindEmpleadoAsync(idempleado);
            return View(emp);
        }/*
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }*/
    }
}
