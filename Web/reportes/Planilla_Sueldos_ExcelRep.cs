using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace web.reportes
{
    public class Planilla_Sueldos_ExcelRep
    {
        public int legajo { get; set; }
        public string cuil { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public string tarea { get; set; }
        public string sexo { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public string nombre { get; set; }
        public int cod_categoria { get; set; }
        public int cod_seccion { get; set; }
        public decimal sueldo_basico { get; set; }
        public decimal sueldo_neto { get; set; }
        public decimal sueldo_bruto { get; set; }
        public int cod_banco { get; set; }
        public string nro_caja_ahorro { get; set; }
        public string clasificacion_personal { get; set; }

        public Planilla_Sueldos_ExcelRep()
        {
            legajo = 0;
            cuil = string.Empty;
            fecha_nacimiento = DateTime.Now;
            tarea = string.Empty;
            sexo = string.Empty;
            fecha_ingreso = DateTime.Now;
            nombre = string.Empty;
            cod_categoria = 0;
            cod_seccion = 0;
            sueldo_basico = 0;
            sueldo_neto = 0;
            sueldo_bruto = 0;
            cod_banco = 0;
            nro_caja_ahorro = string.Empty;
            clasificacion_personal = string.Empty;
        }

        private static List<Planilla_Sueldos_ExcelRep> mapeo(SqlDataReader dr)
        {
            List<Planilla_Sueldos_ExcelRep> lst = new List<Planilla_Sueldos_ExcelRep>();
            Planilla_Sueldos_ExcelRep obj;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    obj = new Planilla_Sueldos_ExcelRep();
                    if (!dr.IsDBNull(0)) { obj.legajo = dr.GetInt32(0); }
                    if (!dr.IsDBNull(1)) { obj.cuil = dr.GetString(1); }
                    if (!dr.IsDBNull(2)) { obj.fecha_nacimiento = dr.GetDateTime(2); }
                    if (!dr.IsDBNull(3)) { obj.tarea = dr.GetString(3); }
                    if (!dr.IsDBNull(4)) { obj.sexo = dr.GetString(4); }
                    if (!dr.IsDBNull(5)) { obj.fecha_ingreso = dr.GetDateTime(5); }
                    if (!dr.IsDBNull(6)) { obj.nombre = dr.GetString(6); }
                    if (!dr.IsDBNull(7)) { obj.cod_categoria = dr.GetInt32(7); }
                    if (!dr.IsDBNull(8)) { obj.cod_seccion = dr.GetInt32(8); }
                    if (!dr.IsDBNull(9)) { obj.sueldo_basico = dr.GetDecimal(9); }
                    if (!dr.IsDBNull(10)) { obj.sueldo_neto = dr.GetDecimal(10); }
                    if (!dr.IsDBNull(11)) { obj.sueldo_bruto = dr.GetDecimal(11); }
                    if (!dr.IsDBNull(12)) { obj.cod_banco = dr.GetInt32(12); }
                    if (!dr.IsDBNull(13)) { obj.nro_caja_ahorro = dr.GetString(13); }
                    if (!dr.IsDBNull(14)) { obj.clasificacion_personal = dr.GetString(14); }
                    lst.Add(obj);
                }
            }
            return lst;
        }

        public static List<Planilla_Sueldos_ExcelRep> read(int anio, int cod_tipo_liq, int nro_liq)
        {
            try
            {
                StringBuilder strSQL = new StringBuilder();

                strSQL.AppendLine("SELECT");
                strSQL.AppendLine("e.legajo,");
                strSQL.AppendLine("e.cuil,");
                strSQL.AppendLine("e.fecha_nacimiento,");
                strSQL.AppendLine("e.tarea,");
                strSQL.AppendLine("e.sexo,");
                strSQL.AppendLine("e.fecha_ingreso,  ");
                strSQL.AppendLine("e.nombre,");
                strSQL.AppendLine("e.cod_categoria,");
                strSQL.AppendLine("e.cod_seccion,");
                strSQL.AppendLine("c.sueldo_basico,");
                strSQL.AppendLine("l.sueldo_neto,");
                strSQL.AppendLine("sueldo_bruto = (Select importe From Det_Liq_x_Empleado");
                strSQL.AppendLine("WHERE anio = l.anio AND");
                strSQL.AppendLine("cod_tipo_liq = l.cod_tipo_liq AND");
                strSQL.AppendLine("nro_liquidacion = l.nro_liquidacion AND");
                strSQL.AppendLine("cod_concepto_liq = 390 AND");
                strSQL.AppendLine("legajo = l.legajo),");
                strSQL.AppendLine("e.cod_banco,");
                strSQL.AppendLine("e.nro_caja_ahorro,");
                strSQL.AppendLine("cp.des_clasif_per as clasificacion_per");
                strSQL.AppendLine("FROM Liq_x_Empleado l");
                strSQL.AppendLine("INNER JOIN EMPLEADOS e on");
                strSQL.AppendLine("e.legajo = l.legajo");
                strSQL.AppendLine("INNER JOIN CATEGORIAS c on");
                strSQL.AppendLine("e.cod_categoria = c.cod_categoria");
                strSQL.AppendLine("INNER JOIN CLASIFICACIONES_PERSONAL cp on");
                strSQL.AppendLine("e.cod_clasif_per = cp.cod_clasif_per");
                strSQL.AppendLine("WHERE");
                strSQL.AppendLine("l.anio=@anio and");
                strSQL.AppendLine("l.cod_tipo_liq=@cod_tipo_liq and");
                strSQL.AppendLine("l.nro_liquidacion=@nro_liq");
                strSQL.AppendLine("ORDER BY e.legajo");
                List <Planilla_Sueldos_ExcelRep> lst = new List<Planilla_Sueldos_ExcelRep>();
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

