using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL;

namespace BLL
{
    public class SeguridadB
    {

        SeguridadD objSeguridad = null;
        bool? estaAutenticado = null;
        string nombre;
        int id_oficina;

        public SeguridadB()
        {
            nombre = "";
            id_oficina = 0;
            objSeguridad = new SeguridadD();
        }

        public string Nombre
        {
            get { return nombre; }
        }

        public bool ValidUser(string user, string password)
        {
            Entities.Auditoria oAudita = new Entities.Auditoria();
            oAudita.id_auditoria = 0;
            oAudita.fecha_movimiento = DateTime.Now.ToString();
            oAudita.menu = "LOGIN SISTEMA DE SUELDOS";
            oAudita.proceso = "logueo";
            oAudita.identificacion = user.ToString();
            oAudita.autorizaciones = "";
            oAudita.observaciones = string.Format("Login del Usuario {0}", user);
            oAudita.detalle = "";
            oAudita.usuario = user;
            DAL.AuditoriaD.Insert_movimiento(oAudita);
            return objSeguridad.ValidUser(user, password, out nombre);
        }

        public bool ValidaPermiso(string user, string proceso, out int id_oficina)
        {
            return objSeguridad.ValidaPermiso(user, proceso, out id_oficina);
        }

        public bool ValidaPermiso(string user, string proceso)
        {
            return objSeguridad.ValidaPermiso(user, proceso);
        }

        #region Nueva Seguridad


        public bool Autenticar(string user, string password, out string nombre)
        {
            return objSeguridad.Autenticar(user, password, out nombre);
        }

        public bool? EstaAutenticado
        {
            get { return estaAutenticado; }
        }

        public bool Autorizacion(string user, string proceso)
        {
            return objSeguridad.ValidaPermiso(user, proceso, out id_oficina);
        }

        public bool AutorizaOpcionesMenu(int tag, string login)
        {
            return objSeguridad.AutorizaOpcionesMenu(tag, login);
        }

        public DataSet OpcionesMenu()
        {
            return objSeguridad.OpcionesMenu();
        }

        public string MenuFuncion(int id_opcion)
        {
            return objSeguridad.MenuFuncion(id_opcion);
        }

        #endregion
    }

}
