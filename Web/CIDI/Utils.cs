using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.CIDI
{
    public class Utils
    {
        public static DAL.UsuarioLoginCIDI ObtenerUsuarioLogueado(string HashCookie)
        {
            DAL.UsuarioLoginCIDI objReturn = null;
            Entrada entrada = new Entrada();
            entrada.IdAplicacion = Config.CiDiIdAplicacion;
            entrada.Contrasenia = Config.CiDiPassAplicacion;
            entrada.HashCookie = HashCookie;
            entrada.TimeStamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            entrada.TokenValue = Config.ObtenerToken_SHA1(entrada.TimeStamp);

            Usuario obj = Config.LlamarWebAPI<Entrada, Usuario>(APICuenta.Usuario.Obtener_Usuario_Aplicacion, entrada);

            if (obj.Respuesta.Resultado == Config.CiDi_OK)
            {
                objReturn = DAL.UsuarioLoginCIDI.getByCuit(obj.CUIL);
                objReturn.apellido = obj.Apellido;
                objReturn.cuit = obj.CUIL;
                objReturn.cuit_formateado = obj.CuilFormateado;
                objReturn.nombre = obj.Nombre;
                objReturn.nombre_completo = obj.NombreFormateado;
                objReturn.sessionHash = HashCookie;

                return objReturn;
            }
            else
            {
                return null;
            }
        }
    }
}