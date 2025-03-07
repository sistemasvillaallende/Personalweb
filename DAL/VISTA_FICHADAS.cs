using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public class VISTA_FICHADAS : DALBase
    {
        public DateTime FECHA { get; set; }
        public string E1 { get; set; }
        public string S1 { get; set; }
        public string E2 { get; set; }
        public string S2 { get; set; }
        public string ENTRADA_TARDE { get; set; }
        public string SALIDA_ANTES { get; set; }
        public string SALIDA_INTERMEDIA { get; set; }
        public string HORAS { get; set; }
        public string HORAS_EXTRAS { get; set; }
        public string AUSENCIAS { get; set; }
        public string FERIADOS { get; set; }
        public int CONTROL { get; set; }

        public VISTA_FICHADAS()
        {
            FECHA = DateTime.Now;
            E1 = string.Empty;
            S1 = string.Empty;
            E2 = string.Empty;
            S2 = string.Empty;
            ENTRADA_TARDE = string.Empty;
            SALIDA_ANTES = string.Empty;
            SALIDA_INTERMEDIA = string.Empty;
            HORAS = string.Empty;
            HORAS_EXTRAS = string.Empty;
            AUSENCIAS = string.Empty;
            FERIADOS = string.Empty;
            CONTROL = 0;
        }

        private static List<VISTA_FICHADAS> mapeo(SqlDataReader dr)
        {
            List<VISTA_FICHADAS> lst = new List<VISTA_FICHADAS>();
            VISTA_FICHADAS obj;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    obj = new VISTA_FICHADAS();
                    if (!dr.IsDBNull(0)) { obj.FECHA = dr.GetDateTime(0); }
                    if (!dr.IsDBNull(1)) { obj.E1 = dr.GetString(1); }
                    if (!dr.IsDBNull(2)) { obj.S1 = dr.GetString(2); }
                    if (!dr.IsDBNull(3)) { obj.E2 = dr.GetString(3); }
                    if (!dr.IsDBNull(4)) { obj.S2 = dr.GetString(4); }
                    if (!dr.IsDBNull(5)) { obj.ENTRADA_TARDE = dr.GetString(5); }
                    if (!dr.IsDBNull(6)) { obj.SALIDA_ANTES = dr.GetString(6); }
                    if (!dr.IsDBNull(7)) { obj.SALIDA_INTERMEDIA = dr.GetString(7); }
                    if (!dr.IsDBNull(8)) { obj.HORAS = dr.GetString(8); }
                    if (!dr.IsDBNull(9)) { obj.HORAS_EXTRAS = dr.GetString(9); }
                    if (!dr.IsDBNull(10)) { obj.AUSENCIAS = dr.GetString(10); }
                    if (!dr.IsDBNull(11)) { obj.FERIADOS = dr.GetString(11); }
                    if (!dr.IsDBNull(12)) { obj.CONTROL = dr.GetInt32(12); }
                    lst.Add(obj);
                }
            }
            return lst;
        }

        public static List<VISTA_FICHADAS> read(int legajo, int anio, int mes)
        {
            try
            {
                List<VISTA_FICHADAS> lst = new List<VISTA_FICHADAS>();
                using (SqlConnection con = GetConnection("ClockCard"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"
                        SELECT DISTINCT FECHA,
	                    (SELECT MIN(HORA) FROM R_LFICHDIA 
	                    WHERE LEGAJO = A.LEGAJO AND YEAR(FECHA)=YEAR(A.FECHA) AND MONTH(FECHA)=MONTH(A.FECHA) AND 
	                    DAY(FECHA)=DAY(A.FECHA)
	                    AND CODIGO = -1 AND CANTIDAD=0 AND ENTSAL='E') AS E1, --ENTRADA 1
	                    (SELECT MIN(HORA) FROM R_LFICHDIA 
	                    WHERE LEGAJO = A.LEGAJO AND YEAR(FECHA)=YEAR(A.FECHA) AND MONTH(FECHA)=MONTH(A.FECHA) 
	                    AND DAY(FECHA)=DAY(A.FECHA)
	                    AND CODIGO = -1 AND CANTIDAD=0 AND ENTSAL='S') AS S1, --SALIDA 1
	                    (SELECT MAX(HORA) FROM R_LFICHDIA 
	                    WHERE LEGAJO = A.LEGAJO AND YEAR(FECHA)=YEAR(A.FECHA) 
	                    AND MONTH(FECHA)=MONTH(A.FECHA) AND DAY(FECHA)=DAY(A.FECHA)
	                    AND CODIGO = -1 AND CANTIDAD=0 AND ENTSAL='E'
	                    HAVING MAX(HORA) <> MIN(HORA)) AS E2, --ENTRADA 2
	                    (SELECT MAX(HORA) FROM R_LFICHDIA 
	                    WHERE LEGAJO = A.LEGAJO AND YEAR(FECHA)=YEAR(A.FECHA) AND MONTH(FECHA)=MONTH(A.FECHA) 
	                    AND DAY(FECHA)=DAY(A.FECHA)
	                    AND CODIGO = -1 AND CANTIDAD=0 AND ENTSAL='S'
	                    HAVING MAX(HORA) <> MIN(HORA)) AS S2, --SALIDA 2

	                    (SELECT 'Entra tarde ' + ISNULL(LTRIM(RTRIM(DESCRIP)),'')  FROM R_LFICHDIA 
	                    WHERE LEGAJO = A.LEGAJO AND YEAR(FECHA)=YEAR(A.FECHA) AND MONTH(FECHA)=MONTH(A.FECHA) 
	                    AND DAY(FECHA)=DAY(A.FECHA)
	                    AND TIPONOV = 'ENT.TARDE') 'ENTRADA TARDE',

	                    (SELECT 'Sale antes ' + ISNULL(LTRIM(RTRIM(DESCRIP)),'')  FROM R_LFICHDIA 
	                    WHERE LEGAJO = A.LEGAJO AND YEAR(FECHA)=YEAR(A.FECHA) AND MONTH(FECHA)=MONTH(A.FECHA) 
	                    AND DAY(FECHA)=DAY(A.FECHA)
	                    AND TIPONOV = 'SAL.ANTES') 'SALIDA ANTES',

	                    (SELECT 'Salida intermedia ' + ISNULL(LTRIM(RTRIM(DESCRIP)),'')  FROM R_LFICHDIA 
	                    WHERE LEGAJO = A.LEGAJO AND YEAR(FECHA)=YEAR(A.FECHA) AND MONTH(FECHA)=MONTH(A.FECHA) 
	                    AND DAY(FECHA)=DAY(A.FECHA)
	                    AND TIPONOV = 'INTERMEDIA') 'SALIDA INTERMEDIA',

	                    (SELECT CONVERT(VARCHAR(5),CANTIDAD) + ' hs. ' + ISNULL(LTRIM(RTRIM(DESCRIP)),'') FROM R_LFICHDIA 
	                    WHERE LEGAJO = A.LEGAJO AND YEAR(FECHA)=YEAR(A.FECHA) AND MONTH(FECHA)=MONTH(A.FECHA) 
	                    AND DAY(FECHA)=DAY(A.FECHA)
	                    AND TIPONOV = 'PRESENTE' AND DESCRIP = 'Normales') 'HORAS',

	                    (SELECT  CONVERT(VARCHAR(5),CANTIDAD) + ' hs. ' + ISNULL(LTRIM(RTRIM(DESCRIP)),'')  FROM R_LFICHDIA 
	                    WHERE LEGAJO = A.LEGAJO AND YEAR(FECHA)=YEAR(A.FECHA) AND MONTH(FECHA)=MONTH(A.FECHA) 
	                    AND DAY(FECHA)=DAY(A.FECHA)
	                    AND TIPONOV = 'EXTRAS') 'HORAS EXTRAS',

	                    (SELECT ISNULL(LTRIM(RTRIM(DESCRIP)),'')  FROM R_LFICHDIA 
	                    WHERE LEGAJO = A.LEGAJO AND YEAR(FECHA)=YEAR(A.FECHA) AND MONTH(FECHA)=MONTH(A.FECHA) 
	                    AND DAY(FECHA)=DAY(A.FECHA)
	                    AND TIPONOV = 'AUSENTE') 'AUSENCIAS',

	                    (SELECT ISNULL(LTRIM(RTRIM(DESCRIP)),'')  FROM R_LFICHDIA 
	                    WHERE LEGAJO = A.LEGAJO AND YEAR(FECHA)=YEAR(A.FECHA) AND MONTH(FECHA)=MONTH(A.FECHA) 
	                    AND DAY(FECHA)=DAY(A.FECHA)
	                    AND TIPONOV = 'PRESENTE' AND DESCRIP <> 'Normales') 'FERIADOS',

	                    (SELECT COUNT(*) FROM R_LFICHDIA WHERE LEGAJO = A.LEGAJO AND YEAR(FECHA)=YEAR(A.FECHA) AND MONTH(FECHA)=MONTH(A.FECHA) AND 
	                    DAY(FECHA)=DAY(A.FECHA)
	                    AND TIPONOV='A FICHADA') AS CONTROL

                        FROM R_LFICHDIA A 
                        WHERE LEGAJO = @legajo AND YEAR(FECHA)=@anio AND MONTH(FECHA)=@mes";

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

        public static VISTA_FICHADAS getByPk(
        )
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT *FROM VISTA_FICHADAS WHERE");
                VISTA_FICHADAS obj = null;
                using (SqlConnection con = GetConnection("ClockCard"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<VISTA_FICHADAS> lst = mapeo(dr);
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

        public static int insert(VISTA_FICHADAS obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO VISTA_FICHADAS(");
                sql.AppendLine("FECHA");
                sql.AppendLine(", E1");
                sql.AppendLine(", S1");
                sql.AppendLine(", E2");
                sql.AppendLine(", S2");
                sql.AppendLine(", ENTRADA_TARDE");
                sql.AppendLine(", SALIDA_ANTES");
                sql.AppendLine(", SALIDA_INTERMEDIA");
                sql.AppendLine(", HORAS");
                sql.AppendLine(", HORAS_EXTRAS");
                sql.AppendLine(", AUSENCIAS");
                sql.AppendLine(", FERIADOS");
                sql.AppendLine(", CONTROL");
                sql.AppendLine(")");
                sql.AppendLine("VALUES");
                sql.AppendLine("(");
                sql.AppendLine("@FECHA");
                sql.AppendLine(", @E1");
                sql.AppendLine(", @S1");
                sql.AppendLine(", @E2");
                sql.AppendLine(", @S2");
                sql.AppendLine(", @ENTRADA_TARDE");
                sql.AppendLine(", @SALIDA_ANTES");
                sql.AppendLine(", @SALIDA_INTERMEDIA");
                sql.AppendLine(", @HORAS");
                sql.AppendLine(", @HORAS_EXTRAS");
                sql.AppendLine(", @AUSENCIAS");
                sql.AppendLine(", @FERIADOS");
                sql.AppendLine(", @CONTROL");
                sql.AppendLine(")");
                sql.AppendLine("SELECT SCOPE_IDENTITY()");
                using (SqlConnection con = GetConnection("ClockCard"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@FECHA", obj.FECHA);
                    cmd.Parameters.AddWithValue("@E1", obj.E1);
                    cmd.Parameters.AddWithValue("@S1", obj.S1);
                    cmd.Parameters.AddWithValue("@E2", obj.E2);
                    cmd.Parameters.AddWithValue("@S2", obj.S2);
                    cmd.Parameters.AddWithValue("@ENTRADA_TARDE", obj.ENTRADA_TARDE);
                    cmd.Parameters.AddWithValue("@SALIDA_ANTES", obj.SALIDA_ANTES);
                    cmd.Parameters.AddWithValue("@SALIDA_INTERMEDIA", obj.SALIDA_INTERMEDIA);
                    cmd.Parameters.AddWithValue("@HORAS", obj.HORAS);
                    cmd.Parameters.AddWithValue("@HORAS_EXTRAS", obj.HORAS_EXTRAS);
                    cmd.Parameters.AddWithValue("@AUSENCIAS", obj.AUSENCIAS);
                    cmd.Parameters.AddWithValue("@FERIADOS", obj.FERIADOS);
                    cmd.Parameters.AddWithValue("@CONTROL", obj.CONTROL);
                    cmd.Connection.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void update(VISTA_FICHADAS obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE  VISTA_FICHADAS SET");
                sql.AppendLine("FECHA=@FECHA");
                sql.AppendLine(", E1=@E1");
                sql.AppendLine(", S1=@S1");
                sql.AppendLine(", E2=@E2");
                sql.AppendLine(", S2=@S2");
                sql.AppendLine(", ENTRADA_TARDE=@ENTRADA_TARDE");
                sql.AppendLine(", SALIDA_ANTES=@SALIDA_ANTES");
                sql.AppendLine(", SALIDA_INTERMEDIA=@SALIDA_INTERMEDIA");
                sql.AppendLine(", HORAS=@HORAS");
                sql.AppendLine(", HORAS_EXTRAS=@HORAS_EXTRAS");
                sql.AppendLine(", AUSENCIAS=@AUSENCIAS");
                sql.AppendLine(", FERIADOS=@FERIADOS");
                sql.AppendLine(", CONTROL=@CONTROL");
                sql.AppendLine("WHERE");
                using (SqlConnection con = GetConnection("ClockCard"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@FECHA", obj.FECHA);
                    cmd.Parameters.AddWithValue("@E1", obj.E1);
                    cmd.Parameters.AddWithValue("@S1", obj.S1);
                    cmd.Parameters.AddWithValue("@E2", obj.E2);
                    cmd.Parameters.AddWithValue("@S2", obj.S2);
                    cmd.Parameters.AddWithValue("@ENTRADA_TARDE", obj.ENTRADA_TARDE);
                    cmd.Parameters.AddWithValue("@SALIDA_ANTES", obj.SALIDA_ANTES);
                    cmd.Parameters.AddWithValue("@SALIDA_INTERMEDIA", obj.SALIDA_INTERMEDIA);
                    cmd.Parameters.AddWithValue("@HORAS", obj.HORAS);
                    cmd.Parameters.AddWithValue("@HORAS_EXTRAS", obj.HORAS_EXTRAS);
                    cmd.Parameters.AddWithValue("@AUSENCIAS", obj.AUSENCIAS);
                    cmd.Parameters.AddWithValue("@FERIADOS", obj.FERIADOS);
                    cmd.Parameters.AddWithValue("@CONTROL", obj.CONTROL);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void delete(VISTA_FICHADAS obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("DELETE  VISTA_FICHADAS ");
                sql.AppendLine("WHERE");
                using (SqlConnection con = GetConnection("ClockCard"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

