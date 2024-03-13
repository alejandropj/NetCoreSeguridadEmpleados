using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NetCoreSeguridadEmpleados.Models
{
    [Table("EMP")]
    public class Empleado
    {
        [Key]
        [Column("EMP_NO")]
        public int IdEmpleado { get; set; }
        [Column("APELLIDO")]
        public string Apellido { get; set; }
        [Column("OFICIO")]
        public string Oficio { get; set; }        
        [Column("DIR")]
        public int Direccion { get; set; }        
        [Column("FECHA_ALT")]
        public DateTime Fecha { get; set; }
        [Column("SALARIO")]
        public int Salario { get; set; }           
        [Column("COMISION")]
        public int Comision { get; set; }        
        [Column("DEPT_NO")]
        public int Departamento { get; set; }
    }
}
