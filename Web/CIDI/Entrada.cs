using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.CIDI
{
    public class Entrada
    {
        public int IdAplicacion { get; set; }
        public String Contrasenia { get; set; }
        public String HashCookie { get; set; }
        public String TokenValue { get; set; }
        public String TimeStamp { get; set; }
        public String CUIL { get; set; }
    }

    public class EntradaLogin : Entrada
    {
        public String ContraseniaUsuario { get; set; }
    }

    public class EntradaCUIL : Entrada
    {
        public String CUIL { get; set; }
    }

    public class EntradaDomicilio : Entrada
    {
        public DomicilioNTB Domicilio { get; set; }

        public EntradaDomicilio()
        {
            Domicilio = new DomicilioNTB();
        }
    }

    public class EntradaLocalidad : Entrada
    {
        public int IdDepartamento { get; set; }
    }

    public class EntradaBarrio : Entrada
    {
        public int IdLocalidad { get; set; }
    }

    public class EntradaDenuncia : Entrada
    {
        public Denuncia Denuncia { get; set; }

        public EntradaDenuncia()
        {
            Denuncia = new Denuncia();
        }
    }

    public class EntradaDenuncia2 : Entrada
    {
        public String NombreInstitucion { get; set; }
        public int IdDepartamento { get; set; }
        public int IdLocalidad { get; set; }
        public int IdBarrio { get; set; }
        public String Calle { get; set; }
        public String Numero { get; set; }
        public String Piso { get; set; }
        public String Depto { get; set; }
        public int IdTipoInstitucion { get; set; }
        public String TelArea { get; set; }
        public String TelNro { get; set; }
        public String CelArea { get; set; }
        public String CelNro { get; set; }
        public String EmailInstitucion { get; set; }
        public String CuilReferente { get; set; }
        public String CargoReferente { get; set; }
        public String Situacion { get; set; }
        public String MenoresInvolucrados { get; set; }
        public String MenoresUbicacion { get; set; }
        public String AdultoReferente { get; set; }
        public String Comentarios { get; set; }
    }

    public class EntradaCom : Entrada
    {
        public int IdAplicacionCom { get; set; }
        public String SesionHash { get; set; }
        public String TokenCom { get; set; }
        public EntradaCom()
        {
            this.IdAplicacionCom = this.IdAplicacionCom;
            this.SesionHash = this.SesionHash;
            this.TokenCom = this.TokenCom;
        }
    }

    public class EntradaPin : Entrada
    {
        public string CUIL { get; set; }
        public int Pin { get; set; }

        public EntradaPin()
        {
            this.Pin = this.Pin;
            this.CUIL = this.CUIL;
        }
    }

    public class EntradaReNaPer : Entrada
    {
        public String CUIL { get; set; }
        public String Id_Sexo { get; set; }
    }

    public class EntradaPrivado : Entrada
    {
        public String CUIL { get; set; }
    }
}