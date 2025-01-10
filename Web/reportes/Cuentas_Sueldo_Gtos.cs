using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace web.reportes
{
    public class Cuentas_Sueldo_Gto
    {
        public int legajo { get; set; }
        public string nombre { get; set; }
        public int cod_categoria { get; set; }
        public string des_categoria { get; set; }
        public string tarea { get; set; }
        public decimal sueldo_basico { get; set; }
        public int antiguedad_ant { get; set; }
        public string fecha_ingreso { get; set; }
        public string nro_cta_sb { get; set; }
        public string nro_cta_gastos { get; set; }

        public Cuentas_Sueldo_Gto()
        {
            legajo = 0;
            nombre = "";
            cod_categoria = 0;
            des_categoria = "";
            tarea = "";
            sueldo_basico = 0;
            antiguedad_ant = 0;
            fecha_ingreso = "";
            nro_cta_sb = "";
            nro_cta_gastos = "";
        }

        private static List<Cuentas_Sueldo_Gto> mapeo(SqlDataReader dr)
        {
            List<Cuentas_Sueldo_Gto> lst = new List<Cuentas_Sueldo_Gto>();
            Cuentas_Sueldo_Gto obj;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    obj = new Cuentas_Sueldo_Gto();
                    if (!dr.IsDBNull(0)) { obj.legajo = dr.GetInt32(0); }
                    if (!dr.IsDBNull(1)) { obj.nombre = dr.GetString(1); }
                    if (!dr.IsDBNull(2)) { obj.cod_categoria = dr.GetInt32(2); }
                    if (!dr.IsDBNull(3)) { obj.des_categoria = dr.GetString(3); }
                    if (!dr.IsDBNull(4)) { obj.tarea = dr.GetString(4); }
                    if (!dr.IsDBNull(5)) { obj.sueldo_basico = dr.GetDecimal(5); }
                    if (!dr.IsDBNull(6)) { obj.antiguedad_ant = dr.GetInt32(6); }
                    if (!dr.IsDBNull(7)) { obj.fecha_ingreso = dr.GetDateTime(7).ToShortDateString(); }
                    if (!dr.IsDBNull(8)) { obj.nro_cta_sb = dr.GetString(8); }
                    if (!dr.IsDBNull(9)) { obj.nro_cta_gastos = dr.GetString(9); }
                    lst.Add(obj);
                }
            }
            return lst;
        }
        public static List<Cuentas_Sueldo_Gto> read()
        {
            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.AppendLine("Select ");
                strSQL.AppendLine("a.legajo,");
                strSQL.AppendLine("a.nombre,");
                strSQL.AppendLine("b.cod_categoria,");
                strSQL.AppendLine("b.des_categoria,");
                strSQL.AppendLine("a.tarea,");
                strSQL.AppendLine("b.sueldo_basico,");
                strSQL.AppendLine("a.antiguedad_ant,");
                strSQL.AppendLine("a.fecha_ingreso,");
                strSQL.AppendLine("a.nro_cta_sb,");
                strSQL.AppendLine("a.nro_cta_gastos");
                strSQL.AppendLine("FROM EMPLEADOS a, CATEGORIAS b");
                strSQL.AppendLine("WHERE");
                strSQL.AppendLine("a.fecha_baja IS NULL AND");
                strSQL.AppendLine("a.cod_categoria = b.cod_categoria");
                strSQL.AppendLine("ORDER BY a.legajo");
                List<Cuentas_Sueldo_Gto> lst = new List<Cuentas_Sueldo_Gto>();
                using (SqlConnection con = DAL.DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    //cmd.Parameters.AddWithValue("@anio", anio);
                    //cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    //cmd.Parameters.AddWithValue("@nro_liq", nro_liq);
                    //cmd.Parameters.AddWithValue("@porcentaje", porcentaje);
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