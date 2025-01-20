using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace web.reportes
{
    public class NominaEmpxSeccion
    {
        public int cod_seccion { get; set; }
        public string des_seccion { get; set; }
        public int legajo { get; set; }
        public string nombre { get; set; }
        public int cod_categoria { get; set; }
        public decimal sueldo_basico { get; set; }
        public string tarea { get; set; }
        public string fecha_ingreso { get; set; }
        public int antiguedad_ant { get; set; }
        public string nro_cta_sb { get; set; }
        public string secretaria { get; set; }
        public string direccion { get; set; }

        public NominaEmpxSeccion()
        {
            cod_seccion = 0;
            des_seccion = "";
            legajo = 0;
            nombre = "";
            cod_categoria = 0;
            sueldo_basico = 0;
            tarea = "";
            fecha_ingreso = "";
            antiguedad_ant = 0;
            secretaria = "";
            direccion = "";
        }


        private static List<NominaEmpxSeccion> mapeo(SqlDataReader dr)
        {
            List<NominaEmpxSeccion> lst = new List<NominaEmpxSeccion>();
            NominaEmpxSeccion obj;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    obj = new NominaEmpxSeccion();
                    if (!dr.IsDBNull(0)) { obj.cod_seccion = dr.GetInt32(0); }
                    if (!dr.IsDBNull(1)) { obj.des_seccion = dr.GetString(1).Trim(); }
                    if (!dr.IsDBNull(2)) { obj.legajo = dr.GetInt32(2); }
                    if (!dr.IsDBNull(3)) { obj.nombre = dr.GetString(3); }
                    if (!dr.IsDBNull(4)) { obj.cod_categoria = dr.GetInt32(4); }
                    if (!dr.IsDBNull(5)) { obj.sueldo_basico = dr.GetDecimal(5); }
                    if (!dr.IsDBNull(6)) { obj.tarea = dr.GetString(6); }
                    if (!dr.IsDBNull(7)) { obj.fecha_ingreso = dr.GetDateTime(7).ToShortDateString(); }
                    if (!dr.IsDBNull(8)) { obj.antiguedad_ant = dr.GetInt32(8); }
                    if (!dr.IsDBNull(9)) { obj.nro_cta_sb = dr.GetString(9); }
                    if (!dr.IsDBNull(10)) { obj.secretaria = dr.GetString(10); }
                    if (!dr.IsDBNull(11)) { obj.direccion = dr.GetString(11); }
                    lst.Add(obj);
                }
            }
            return lst;
        }
        public static List<NominaEmpxSeccion> readNominaEmpleado()
        {
            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.AppendLine("SELECT distinct");
                strSQL.AppendLine("a.cod_seccion, a.des_seccion, b.legajo, b.nombre, c.cod_categoria, c.sueldo_basico,");
                strSQL.AppendLine("b.tarea, b.fecha_ingreso, b.antiguedad_ant, b.nro_cta_sb, s.descripcion as Secretaria,");
                strSQL.AppendLine("d.descripcion as Direccion");
                strSQL.AppendLine("FROM SECCIONES a");
                strSQL.AppendLine("JOIN EMPLEADOS b on");
                strSQL.AppendLine("a.cod_seccion = b.cod_seccion");
                strSQL.AppendLine("JOIN CATEGORIAS c on");
                strSQL.AppendLine("b.cod_categoria = c.cod_categoria");
                strSQL.AppendLine("JOIN DIRECCION_X_SECRETARIA dxs ON");
                strSQL.AppendLine("dxs.Id_secretaria = b.id_secretaria AND");
                strSQL.AppendLine("dxs.Id_direccion = b.id_direccion");
                strSQL.AppendLine("join secretaria s on");
                strSQL.AppendLine("s.id_secretaria = dxs.id_secretaria");
                strSQL.AppendLine("join direccion d on");
                strSQL.AppendLine("d.id_direccion = dxs.id_direccion");
                strSQL.AppendLine("WHERE");
                strSQL.AppendLine("b.fecha_baja IS Null");
                strSQL.AppendLine("ORDER BY a.cod_seccion, b.legajo");

                List<NominaEmpxSeccion> lst = new List<NominaEmpxSeccion>();
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