using Microsoft.EntityFrameworkCore;
using NetCoreSeguridadEmpleados.Data;
using NetCoreSeguridadEmpleados.Models;
using System.Numerics;

namespace NetCoreSeguridadEmpleados.Repositories
{
    public class RepositoryEmpleados
    {
        private EmpleadosContext context;
        public RepositoryEmpleados(EmpleadosContext context)
        {
            this.context = context;
        }

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            var empleados = await this.context.Empleados.ToListAsync();
            return empleados;
        }
        public async Task<Empleado> FindEmpleadoAsync(int idEmpleado)
        {
            return await this.context.Empleados.FirstOrDefaultAsync
                (z => z.IdEmpleado == idEmpleado);
        }
        public async Task<List<Empleado>> GetEmpleadosDepartamentoAsync(int idDept)
        {
            return await this.context.Empleados.Where(x => x.Departamento == idDept).ToListAsync();
        }
        public async Task UpdateSalarioEmpleadosDepartamentoAsync(int idDept, int incremento)
        {
            List<Empleado> empleados = await this.GetEmpleadosDepartamentoAsync(idDept);
            foreach(Empleado emp in empleados)
            {
                emp.Salario += incremento;
            }
            await this.context.SaveChangesAsync();
        }
        public async Task<Empleado> LogInEmpleadoAsync(string apellido, int idEmpleado)
        {
            Empleado emp = await this.context.Empleados.Where
                (z => z.Apellido == apellido && z.IdEmpleado == idEmpleado)
                .FirstOrDefaultAsync();
            return emp;
        }
    }
}
