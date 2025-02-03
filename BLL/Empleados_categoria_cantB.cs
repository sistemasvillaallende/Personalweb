using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entities;

namespace BLL
{
    public class Empleados_categoria_cantB
    {

        public static Empleados_categoria_cant GetEmpleadosByCategoria(int codCategoria)
        {
            return Empleados_categoria_cantD.GetEmpleadosByCategoria(codCategoria);
        }

    }
}