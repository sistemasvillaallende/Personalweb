using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public class REPORTE_FICHA : DALBase
    {
        public int LEGAJO { get; set; }
        public string NOMBRE { get; set; }
        public DateTime ULTPROC { get; set; }
        public string ACTIVO { get; set; }
        public int SECTOR { get; set; }
        public DateTime FECHA { get; set; }
        public string HORA { get; set; }
        public string ENTSAL { get; set; }
        public string ORIGEN { get; set; }
        public string TIPONOV { get; set; }
        public Single CANTIDAD { get; set; }
        public string PAGAR { get; set; }
        public int CODIGO { get; set; }
        public string TIPO { get; set; }
        public string DESCRIP { get; set; }

        public REPORTE_FICHA()
        {
            LEGAJO = 0;
            NOMBRE = string.Empty;
            ULTPROC = DateTime.Now;
            ACTIVO = string.Empty;
            SECTOR = 0;
            FECHA = DateTime.Now;
            HORA = string.Empty;
            ENTSAL = string.Empty;
            ORIGEN = string.Empty;
            TIPONOV = string.Empty;
            CANTIDAD = 0;
            PAGAR = string.Empty;
            CODIGO = 0;
            TIPO = string.Empty;
            DESCRIP = string.Empty;
        }

        private static List<REPORTE_FICHA> mapeo(SqlDataReader dr)
        {
            List<REPORTE_FICHA> lst = new List<REPORTE_FICHA>();
            REPORTE_FICHA obj;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    obj = new REPORTE_FICHA();
                    if (!dr.IsDBNull(0)) { obj.LEGAJO = dr.GetInt32(0); }
                    if (!dr.IsDBNull(1)) { obj.NOMBRE = dr.GetString(1); }
                    if (!dr.IsDBNull(2)) { obj.ULTPROC = dr.GetDateTime(2); }
                    if (!dr.IsDBNull(3)) { obj.ACTIVO = dr.GetString(3); }
                    if (!dr.IsDBNull(4)) { obj.SECTOR = dr.GetInt32(4); }
                    if (!dr.IsDBNull(5)) { obj.FECHA = dr.GetDateTime(5); }
                    if (!dr.IsDBNull(6)) { obj.HORA = dr.GetString(6); }
                    if (!dr.IsDBNull(7)) { obj.ENTSAL = dr.GetString(7); }
                    if (!dr.IsDBNull(8)) { obj.ORIGEN = dr.GetString(8); }
                    if (!dr.IsDBNull(9)) { obj.TIPONOV = dr.GetString(9); }
                    if (!dr.IsDBNull(10)) { obj.CANTIDAD = dr.GetFloat(10); }
                    if (!dr.IsDBNull(11)) { obj.PAGAR = dr.GetString(11); }
                    if (!dr.IsDBNull(12)) { obj.CODIGO = dr.GetInt32(12); }
                    if (!dr.IsDBNull(13)) { obj.TIPO = dr.GetString(13); }
                    if (!dr.IsDBNull(14)) { obj.DESCRIP = dr.GetString(14); }
                    lst.Add(obj);
                }
            }
            return lst;
        }

        public static List<REPORTE_FICHA> read(int legajo, int anio, int mes)
        {
            try
            {
                List<REPORTE_FICHA> lst = new List<REPORTE_FICHA>();
                using (SqlConnection con = GetConnection("ClockCard"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        "SELECT *FROM R_LFICHDIA WHERE LEGAJO = @legajo AND YEAR(FECHA)=@anio AND MONTH(FECHA)=@mes";
                    cmd.Parameters.AddWithValue("@legajo", legajo);
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@mes", mes);
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
        public static List<REPORTE_FICHA> getRazones(int legajo, int anio)
        {
            try
            {
                List<REPORTE_FICHA> lst = new List<REPORTE_FICHA>();
                using (SqlConnection con = GetConnection("ClockCard"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        "SELECT * FROM R_LFICHDIA WHERE LEGAJO= @legajo AND CODIGO=114 AND YEAR(FECHA) = @anio";
                    cmd.Parameters.AddWithValue("@legajo", legajo);
                    cmd.Parameters.AddWithValue("@anio", anio);
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
        public static REPORTE_FICHA getByPk(
        )
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT *FROM REPORTE_FICHA WHERE");
                REPORTE_FICHA obj = null;
                using (SqlConnection con = GetConnection("ClockCard"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<REPORTE_FICHA> lst = mapeo(dr);
                    if (lst.Count != 0)
                        obj = lst[0];
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}

