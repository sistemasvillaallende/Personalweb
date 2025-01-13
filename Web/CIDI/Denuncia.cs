using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.CIDI
{
    public class Denuncia
    {
        private String _TelFormateado = String.Empty;
        private String _CelFormateado = String.Empty;

        public String NombreInstitucion { get; set; }

        public int IdDenuncia { get; set; }

        public String CargoReferente { get; set; }

        public String Situacion { get; set; }

        public String MenoresInvolucrados { get; set; }

        public String MenoresUbicacion { get; set; }

        public int IdVin { get; set; }

        public String Comentarios { get; set; }

        public String CuilReferente { get; set; }

        public String Fecha { get; set; }

        public String AdultoReferente { get; set; }

        public String TelArea { get; set; }

        public String TelNro { get; set; }

        public String CelArea { get; set; }

        public String CelNro { get; set; }

        public String EmailInstitucion { get; set; }

        public TipoInstitucion TipoInstitucion { get; set; }

        public List<TipoInstitucion> TipoInstitucionList { get; set; }

        public DomicilioNTB Domicilio { get; set; }

        public Respuesta Respuesta { get; set; }

        public Denuncia()
        {
            TipoInstitucion = new TipoInstitucion();
            TipoInstitucionList = new List<TipoInstitucion>();
            Domicilio = new DomicilioNTB();
            Respuesta = new Respuesta();
        }
    }

    public class TipoInstitucion
    {
        public int IdTipoInstitucion { get; set; }

        public String Nombre { get; set; }
    }
}