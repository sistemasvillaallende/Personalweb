using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.CIDI
{
    public class Respuesta
    {
        public String Resultado { get; set; }
        public String CodigoError { get; set; }
        public String ExisteUsuario { get; set; }
        public String SesionHash { get; set; }
    }

    public class RespuestaCom : Respuesta
    {
        public String TokenCom { get; set; }
    }

    public class RespuestaReNaPer : Respuesta
    {
        /// <summary>
        /// 99 DNI Tarjeta valido
        /// 1 Problemas de BD
        /// 4 Sin DNI digital
        /// 5 Problemas internos
        /// 2 Persona no encontrada
        /// </summary>
        public int NumeroError { get; set; }
    }

    public class RespuestaInformacion : Respuesta
    {
        public Informacion Informacion { get; set; }

        public RespuestaInformacion()
        {
            Informacion = new Informacion();
        }
    }

    public class RespuestaInformacionListado : Respuesta
    {
        public List<Informacion> InformacionList { get; set; }

        public RespuestaInformacionListado()
        {
            InformacionList = new List<Informacion>();
        }
    }
}