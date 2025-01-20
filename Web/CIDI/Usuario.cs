using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.CIDI
{
    public class Usuario
    {
        public String CUIL { get; set; }
        public String CuilFormateado { get; set; }
        public String NroDocumento { get; set; }
        public String Apellido { get; set; }
        public String Nombre { get; set; }
        public String NombreFormateado { get; set; }
        public String FechaNacimiento { get; set; }
        public String Id_Sexo { get; set; }
        public String PaiCodPais { get; set; }
        public int Id_Numero { get; set; }
        public int? Id_Estado { get; set; }
        public String Estado { get; set; }
        public String Email { get; set; }
        public String TelArea { get; set; }
        public String TelNro { get; set; }
        public String TelFormateado { get; set; }
        public String CelArea { get; set; }
        public String CelNro { get; set; }
        public String CelFormateado { get; set; }
        public String Empleado { get; set; }
        public String Id_Empleado { get; set; }
        public String FechaRegistro { get; set; }
        public String FechaBloqueo { get; set; }
        public String IdAplicacionOrigen { get; set; }
        public String TieneRepresentados { get; set; }
        public Afip Afip { get; set; }
        public Domicilio Domicilio { get; set; }
        public Representado Representado { get; set; }
        public Respuesta Respuesta { get; set; }
        public string foto { get; internal set; }

        public Usuario()
        {
            Afip = new Afip();
            Domicilio = new Domicilio();
            Representado = new Representado();
            Respuesta = new Respuesta();
        }
    }

    public class UsuarioExterno : Usuario
    {
        public List<Representado> Representados { get; set; }
        public UsuarioExterno()
        {
            Representados = new List<Representado>();
        }
    }

    public class UsuarioReNaPer
    {
        private String _CuilFormateado = String.Empty;
        private String _NombreFormateado = String.Empty;
        private String FecNac = String.Empty;
        public int IdTramitePrincipal { get; set; }
        public int IdTramiteTarjetaReimpresa { get; set; }
        public String Ejemplar { get; set; }
        public String Vencimiento { get; set; }
        public String Emision { get; set; }
        public String CUIL { get; set; }
        public String CuilFormateado
        {
            get
            {
                if (!String.IsNullOrEmpty(CUIL))
                {
                    return String.Format("{0}-{1}-{2}", CUIL.Substring(0, 2), CUIL.Substring(2, 8), CUIL.Substring(10, 1));
                }
                else
                {
                    return String.Empty;
                }
            }
            set
            {
                _CuilFormateado = value;
            }
        }
        public int NroDocumento { get; set; }
        public String Id_Sexo { get; set; }
        public String Apellido { get; set; }
        public String Nombre { get; set; }
        public String NombreFormateado { get; set; }
        public String FechaNacimiento { get; set; }
        public DomicilioReNaPer Domicilio { get; set; }
        public FallecimientoReNaPer Fallecimiento { get; set; }
        public String IdCiudadano { get; set; }
        public String Foto { get; set; }
        public RespuestaReNaPer Respuesta { get; set; }
        public UsuarioReNaPer()
        {
            Domicilio = new DomicilioReNaPer();
            Fallecimiento = new FallecimientoReNaPer();
            Respuesta = new RespuestaReNaPer();
        }
    }

    public class UsuarioPrivado
    {
        public String CUIL { get; set; }
        public String CuilFormateado { get; set; }
        public String NroDocumento { get; set; }
        public String Apellido { get; set; }
        public String Nombre { get; set; }
        public String NombreFormateado { get; set; }
        public String FechaNacimiento { get; set; }
        public String Id_Sexo { get; set; }
        public int? Id_Estado { get; set; }
        public String Estado { get; set; }
        public Respuesta Respuesta { get; set; }

        public UsuarioPrivado()
        {
            Respuesta = new Respuesta();
        }
    }

    public class Afip
    {
        public String Login { get; set; }
        public String Fecha { get; set; }
    }
}