using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace web.reportes
{
    public class Planilla_Cheques
    {
        public int legajo { get; set; }
        public string nombre { get; set; }
        public string banco { get; set; }
        public string nro_caja_ahorro { get; set; }
        public decimal sueldo_neto { get; set; }

        public Planilla_Cheques()
        {
            legajo = 0;
            nombre = string.Empty;
            banco = string.Empty;
            nro_caja_ahorro = string.Empty;
            sueldo_neto = 0;
        }

        private static List<Planilla_Cheques> mapeo(SqlDataReader dr)
        {
            List<Planilla_Cheques> lst = new List<Planilla_Cheques>();
            Planilla_Cheques obj;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    obj = new Planilla_Cheques();
                    if (!dr.IsDBNull(0)) { obj.legajo = dr.GetInt32(0); }
                    if (!dr.IsDBNull(1)) { obj.nombre = dr.GetString(1); }
                    if (!dr.IsDBNull(2)) { obj.banco = dr.GetString(2); }
                    if (!dr.IsDBNull(3)) { obj.nro_caja_ahorro = dr.GetString(3); }
                    if (!dr.IsDBNull(4)) { obj.sueldo_neto = dr.GetDecimal(4); }
                    lst.Add(obj);
                }
            }
            return lst;
        }

        public static List<Planilla_Cheques> read(int anio, int cod_tipo_liq, int nro_liq)
        {
            try
            {
                StringBuilder strSQL = new StringBuilder();

                strSQL.AppendLine("SELECT");
                strSQL.AppendLine("e.legajo,");
                strSQL.AppendLine("e.nombre,");
                strSQL.AppendLine("b.nom_banco,");
                strSQL.AppendLine("e.nro_caja_ahorro,");
                strSQL.AppendLine("importe_total = l1.sueldo_neto");
                strSQL.AppendLine("FROM LIQUIDACIONES l WITH(NOLOCK)");
                strSQL.AppendLine("JOIN LIQ_X_EMPLEADO l1 ON");
                strSQL.AppendLine("l.anio = l1.anio AND");
                strSQL.AppendLine("l.cod_tipo_liq = l1.cod_tipo_liq AND");
                strSQL.AppendLine("l.nro_liquidacion = l1.nro_liquidacion");
                strSQL.AppendLine("JOIN EMPLEADOS e ON");
                strSQL.AppendLine("l1.legajo = e.legajo");
                strSQL.AppendLine("JOIN Bancos b ON");
                strSQL.AppendLine("b.cod_banco = e.cod_banco");
                strSQL.AppendLine("WHERE");
                strSQL.AppendLine("l.anio=@anio and");
                strSQL.AppendLine("l.cod_tipo_liq=@cod_tipo_liq and");
                strSQL.AppendLine("l.nro_liquidacion=@nro_liq");
                strSQL.AppendLine("ORDER BY e.legajo");
                List <Planilla_Cheques> lst = new List<Planilla_Cheques>();
                using (SqlConnection con = DAL.DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liq", nro_liq);
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

