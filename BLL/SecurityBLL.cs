using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace BLL
{
    public class SecurityBLL
    {
        SecurityDAL objSeguridad = null;

        //bool? estaAutenticado = false;
        public string nombre { get; set; }
        public int id_oficina { get; set; }

        public SecurityBLL()
        {
            nombre = "";
            id_oficina = 0;
            objSeguridad = new DAL.SecurityDAL();
        }


        public bool ValidUser(string user, string password)
        {
            return objSeguridad.ValidUser(user, password, nombre);
        }

        public bool ValidaPermiso(string user, string proceso, out int id_oficina)
        {
            return objSeguridad.ValidaPermiso(user, proceso, out id_oficina);
        }

        public bool ValidaPermiso(string user, string Proceso)
        {
            return objSeguridad.ValidaPermiso(user, Proceso);
        }
        public bool ValidaPermisoDesestimaAdministrador(string user, string proceso)
        {
            return objSeguridad.ValidaPermisoDesestimaAdministrador(user, proceso);
        }

    }
}
