using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class HORARIOS : DALBase
    {
        public int TUR_CODIGO { get; set; }
        public string TUR_TIPO { get; set; }
        public string TUR_DESCRIP { get; set; }
        public string DIA_NOMBRE { get; set; }
        public string INT_DESDE { get; set; }
        public string INT_HASTA { get; set; }
        public int DIA_NUMERO { get; set; }
        public HORARIOS()
        {
            TUR_CODIGO = 0;
            TUR_TIPO = string.Empty;
            TUR_DESCRIP = string.Empty;
            DIA_NOMBRE = string.Empty;
            INT_DESDE = string.Empty;
            INT_HASTA = string.Empty;
            DIA_NUMERO = 0;
        }
        private static List<HORARIOS> mapeo(SqlDataReader dr)
        {
            List<HORARIOS> lst = new List<HORARIOS>();
            HORARIOS obj;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    obj = new HORARIOS();
                    if (!dr.IsDBNull(0)) { obj.TUR_CODIGO = dr.GetInt32(0); }
                    if (!dr.IsDBNull(1)) { obj.TUR_TIPO = dr.GetString(1); }
                    if (!dr.IsDBNull(2)) { obj.TUR_DESCRIP = dr.GetString(2); }
                    if (!dr.IsDBNull(3)) { obj.DIA_NOMBRE = dr.GetString(3); }
                    if (!dr.IsDBNull(4)) { obj.DIA_NUMERO = dr.GetInt32(4); }
                    if (!dr.IsDBNull(5)) { obj.INT_DESDE = dr.GetString(5); }
                    if (!dr.IsDBNull(6)) { obj.INT_HASTA = dr.GetString(6); }
                    lst.Add(obj);
                }
            }
            return lst;
        }
        public static List<HORARIOS> getByPk(int legajo)
        {
            try
            {
                HORARIOS obj = null;
                using (SqlConnection con = GetConnection("ClockCard"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"
                            SELECT B.TUR_CODIGO, B.TUR_TIPO, B.TUR_DESCRIP, D.DIA_NOMBRE, D.DIA_NUMERO, C.INT_DESDE, C.INT_HASTA
                            FROM LEGAJO A
                            INNER JOIN TURNO B ON A.TUR_CODIGO = B.TUR_CODIGO
                            INNER JOIN INTERVALO C ON A.TUR_CODIGO =  C.TUR_CODIGO
                            INNER JOIN DIA D ON D.DIA_NUMERO = C.DIA_NUMERO AND D.TUR_CODIGO = A.TUR_CODIGO
                            WHERE C.INT_OBLIG = 'SI' AND A.LEG_LEGAJO = @LEG_LEGAJO
                            UNION
                            SELECT B.TUR_CODIGO, E.TUR_TIPO, E.TUR_DESCRIP, D.DIA_NOMBRE, D.DIA_NUMERO, C.INT_DESDE, C.INT_HASTA
                            FROM LEGAJO A
                            INNER JOIN TURALTER B ON A.LEG_LEGAJO = B.LEG_LEGAJO
                            INNER JOIN TURNO E ON B.TUR_CODIGO=E.TUR_CODIGO
                            INNER JOIN INTERVALO C ON B.TUR_CODIGO =  C.TUR_CODIGO
                            INNER JOIN DIA D ON D.DIA_NUMERO = C.DIA_NUMERO AND D.TUR_CODIGO = A.TUR_CODIGO
                            WHERE C.INT_OBLIG = 'SI' AND A.LEG_LEGAJO = @LEG_LEGAJO
                            ORDER BY D.DIA_NUMERO
                                ";
                    cmd.Parameters.AddWithValue("@LEG_LEGAJO", legajo);

                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<HORARIOS> lst = mapeo(dr);
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
