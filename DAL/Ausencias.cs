using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public class Ausencias : DALBase
    {
        public int LEG_LEGAJO { get; set; }
        public string LEG_APYNOM { get; set; }
        public DateTime LEG_ULTPROC { get; set; }
        public DateTime RES_FECHA { get; set; }
        public string RES_TIPONOV { get; set; }
        public int CON_CODIGO { get; set; }
        public Single RES_CANTIDAD { get; set; }
        public string RES_PAGAR { get; set; }
        public string RES_MANUAL { get; set; }
        public string CON_DESCRIP { get; set; }
        public string LEG_ACTIVO { get; set; }
        public int SEC_CODIGO { get; set; }

        public Ausencias()
        {
            LEG_LEGAJO = 0;
            LEG_APYNOM = string.Empty;
            LEG_ULTPROC = DateTime.Now;
            RES_FECHA = DateTime.Now;
            RES_TIPONOV = string.Empty;
            CON_CODIGO = 0;
            RES_CANTIDAD = 0;
            RES_PAGAR = string.Empty;
            RES_MANUAL = string.Empty;
            CON_DESCRIP = string.Empty;
            LEG_ACTIVO = string.Empty;
            SEC_CODIGO = 0;
        }

        private static List<Ausencias> mapeo(SqlDataReader dr)
        {
            List<Ausencias> lst = new List<Ausencias>();
            Ausencias obj;
            if (dr.HasRows)
            {
                int LEG_LEGAJO = dr.GetOrdinal("LEG_LEGAJO");
                int LEG_APYNOM = dr.GetOrdinal("LEG_APYNOM");
                int LEG_ULTPROC = dr.GetOrdinal("LEG_ULTPROC");
                int RES_FECHA = dr.GetOrdinal("RES_FECHA");
                int RES_TIPONOV = dr.GetOrdinal("RES_TIPONOV");
                int CON_CODIGO = dr.GetOrdinal("CON_CODIGO");
                int RES_CANTIDAD = dr.GetOrdinal("RES_CANTIDAD");
                int RES_PAGAR = dr.GetOrdinal("RES_PAGAR");
                int RES_MANUAL = dr.GetOrdinal("RES_MANUAL");
                int CON_DESCRIP = dr.GetOrdinal("CON_DESCRIP");
                int LEG_ACTIVO = dr.GetOrdinal("LEG_ACTIVO");
                int SEC_CODIGO = dr.GetOrdinal("SEC_CODIGO");
                while (dr.Read())
                {
                    obj = new Ausencias();
                    if (!dr.IsDBNull(LEG_LEGAJO)) { obj.LEG_LEGAJO = dr.GetInt32(LEG_LEGAJO); }
                    if (!dr.IsDBNull(LEG_APYNOM)) { obj.LEG_APYNOM = dr.GetString(LEG_APYNOM); }
                    if (!dr.IsDBNull(LEG_ULTPROC)) { obj.LEG_ULTPROC = dr.GetDateTime(LEG_ULTPROC); }
                    if (!dr.IsDBNull(RES_FECHA)) { obj.RES_FECHA = dr.GetDateTime(RES_FECHA); }
                    if (!dr.IsDBNull(RES_TIPONOV)) { obj.RES_TIPONOV = dr.GetString(RES_TIPONOV); }
                    if (!dr.IsDBNull(CON_CODIGO)) { obj.CON_CODIGO = dr.GetInt32(CON_CODIGO); }
                    if (!dr.IsDBNull(RES_CANTIDAD)) { obj.RES_CANTIDAD = dr.GetFloat(RES_CANTIDAD); }
                    if (!dr.IsDBNull(RES_PAGAR)) { obj.RES_PAGAR = dr.GetString(RES_PAGAR); }
                    if (!dr.IsDBNull(RES_MANUAL)) { obj.RES_MANUAL = dr.GetString(RES_MANUAL); }
                    if (!dr.IsDBNull(CON_DESCRIP)) { obj.CON_DESCRIP = dr.GetString(CON_DESCRIP); }
                    if (!dr.IsDBNull(LEG_ACTIVO)) { obj.LEG_ACTIVO = dr.GetString(LEG_ACTIVO); }
                    if (!dr.IsDBNull(SEC_CODIGO)) { obj.SEC_CODIGO = dr.GetInt32(SEC_CODIGO); }
                    lst.Add(obj);
                }
            }
            return lst;
        }

        public static List<Ausencias> read(int mes, int dia, int ejercicio)
        {
            try
            {
                List<Ausencias> lst = new List<Ausencias>();
                using (SqlConnection con = GetConnection("ClockCard"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        @"SELECT *FROM R_LAUSENC
                        WHERE YEAR(RES_FECHA)=@ejercicio AND 
                        MONTH(RES_FECHA)=@MES AND DAY(RES_FECHA)=@DIA
                        AND LEG_ACTIVO='SI'";
                    cmd.Parameters.AddWithValue("@MES", mes);
                    cmd.Parameters.AddWithValue("@DIA", dia);
                    cmd.Parameters.AddWithValue("@ejercicio", ejercicio);
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    lst = mapeo(dr);
                    return lst;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<Ausencias> read(int mes, int dia, int idSecretaria, int ejercicio)
        {
            try
            {
                List<Ausencias> lst = new List<Ausencias>();
                using (SqlConnection con = GetConnection("ClockCard"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        @"SELECT A.*FROM R_LAUSENC A
                        INNER JOIN SIIMVA.dbo.EMPLEADOS B ON A.LEG_LEGAJO=B.legajo
                        WHERE YEAR(RES_FECHA)=@ejercicio AND 
                        MONTH(RES_FECHA)=@MES AND DAY(RES_FECHA)=@DIA
                        AND LEG_ACTIVO='SI' AND B.id_secretaria=@id_secretaria";
                    cmd.Parameters.AddWithValue("@MES", mes);
                    cmd.Parameters.AddWithValue("@DIA", dia);
                    cmd.Parameters.AddWithValue("@id_secretaria", idSecretaria);
                    cmd.Parameters.AddWithValue("@ejercicio", ejercicio);
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    lst = mapeo(dr);
                    return lst;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<Ausencias> readDireccion(int mes, int dia, int idDireccion, int ejercicio)
        {
            try
            {
                List<Ausencias> lst = new List<Ausencias>();
                using (SqlConnection con = GetConnection("ClockCard"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        @"SELECT A.*FROM R_LAUSENC A
                        INNER JOIN SIIMVA.dbo.EMPLEADOS B ON A.LEG_LEGAJO=B.legajo
                        WHERE YEAR(RES_FECHA)=@ejercicio AND 
                        MONTH(RES_FECHA)=@MES AND DAY(RES_FECHA)=@DIA
                        AND LEG_ACTIVO='SI' AND B.id_direccion=@id_direccion";
                    cmd.Parameters.AddWithValue("@MES", mes);
                    cmd.Parameters.AddWithValue("@DIA", dia);
                    cmd.Parameters.AddWithValue("@id_direccion", idDireccion);
                    cmd.Parameters.AddWithValue("@ejercicio", ejercicio);
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    lst = mapeo(dr);
                    return lst;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

