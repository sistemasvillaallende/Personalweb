using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public class Acred_bco_provD : DALBase
    {
        private static List<Entities.Acred_bco_prov> mapeo(SqlDataReader dr)
        {
            List<Entities.Acred_bco_prov> lst = new List<Entities.Acred_bco_prov>();
            Entities.Acred_bco_prov obj;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    obj = new Entities.Acred_bco_prov();
                    if (!dr.IsDBNull(0)) { obj.tipo_reg = Convert.ToString(dr.GetInt32(0)); }
                    if (!dr.IsDBNull(1)) { obj.nro_suc = dr.GetString(1); }
                    if (!dr.IsDBNull(2)) { obj.nro_reparto = dr.GetString(2); }
                    if (!dr.IsDBNull(3)) { obj.legajo = string.Format("{0:0000}", Convert.ToString(dr.GetInt32(3))); }
                    if (!dr.IsDBNull(4)) { obj.tipo_doc = Convert.ToString(dr.GetInt32(4)); }
                    if (!dr.IsDBNull(5)) { obj.nro_doc = string.Format("{0:000000000000}", dr.GetString(5)); }
                    if (!dr.IsDBNull(6)) { obj.tipo_cuenta = Convert.ToString(dr.GetInt32(6)); }
                    if (!dr.IsDBNull(7)) { obj.nro_cuenta = string.Format("{0:000000000}", dr.GetString(7)).Replace("/", ""); }
                    if (!dr.IsDBNull(8)) { obj.monto_sueldo = string.Format("{0:0000000000000000.00}", Convert.ToString(dr.GetDecimal(8))); }
                    if (!dr.IsDBNull(9)) { obj.importe_pesos = string.Format("{0:0000000000000000.00}", Convert.ToString(dr.GetDecimal(9))); }
                    if (!dr.IsDBNull(10)) { obj.importe_cecor = string.Format("{0:0000000000000000.00}", "0"); } //Convert.ToString(dr.GetInt32(10)); }
                    if (!dr.IsDBNull(11)) { obj.fecha_acreditacion = Convert.ToString((dr.GetDateTime(11)).ToString("yyyyMMdd")); }
                    //Convert.ToDateTime(dr.GetString(fecha_acta)).ToString("dd/MM/yy");
                    if (!dr.IsDBNull(12)) { obj.nro_empresa = Convert.ToString(dr.GetInt32(12)); }
                    if (!dr.IsDBNull(13)) { obj.nombre = dr.GetString(13).PadRight(30).Substring(0, 30); }
                    if (!dr.IsDBNull(14)) { obj.direccion = dr.GetString(14).PadRight(30).Substring(0, 30); }
                    if (!dr.IsDBNull(15)) { obj.localidad = dr.GetString(15).PadRight(30).Substring(0, 30); }
                    if (!dr.IsDBNull(16)) { obj.cod_postal = string.Format("{0:00000}", Convert.ToString(dr.GetString(16))); }
                    if (!dr.IsDBNull(17)) { obj.telefono = string.Format("{0:0000000000}", Convert.ToString(dr.GetInt32(17))); }
                    if (!dr.IsDBNull(18)) { obj.fecha_nacimiento = Convert.ToString((dr.GetDateTime(18)).ToString("yyyyMMdd")); }
                    if (!dr.IsDBNull(19)) { obj.salto_de_fila = Convert.ToString(dr.GetInt32(19)); }
                    lst.Add(obj);
                }
            }
            return lst;
        }
        public static List<Entities.Acred_bco_prov> GetAcred_bco_prov(int anio, int cod_tipo_liq, int nro_liquidacion, decimal porcentaje)
        {
            List<Entities.Acred_bco_prov> lst = new List<Entities.Acred_bco_prov>();
            StringBuilder strSQL = new StringBuilder();
            SqlCommand cmd;
            SqlConnection cn = DALBase.GetConnection("Siimva");
            strSQL.AppendLine("SELECT ");
            strSQL.AppendLine("tipo_reg=0,nro_suc='392',nro_reparto='000',legajo=e.legajo,");
            strSQL.AppendLine("tipo_doc=e.cod_tipo_documento,nro_doc=e.nro_documento,");
            strSQL.AppendLine("tipo_cuenta=1,nro_cuenta=e.nro_caja_ahorro,l.sueldo_neto,");
            strSQL.AppendLine("importe_pesos=Round((l.sueldo_neto * @porcentaje / 100 ),0,1),");
            strSQL.AppendLine("importe_cecor='0',");
            strSQL.AppendLine("fecha_acreditacion=l1.fecha_pago,nro_empresa=2173,");
            strSQL.AppendLine("nombre=e.nombre,");
            strSQL.AppendLine("direccion=CONVERT(CHAR(30),(CONVERT(CHAR(30),e.calle_domicilio)) + ' ' + CONVERT(CHAR(5),e.nro_domicilio) +");
            strSQL.AppendLine("' ' + e.barrio_domicilio), cod_postal=e.cod_postal, ");
            strSQL.AppendLine("localidad=e.ciudad_domicilio,telefono=0,");
            strSQL.AppendLine("fecha_nacimiento = e.fecha_nacimiento, salto_de_fila=0 ");
            strSQL.AppendLine("FROM liq_x_empleado l with (nolock) ");
            strSQL.AppendLine("join Empleados e on ");
            strSQL.AppendLine("l.legajo = e.legajo and ");
            strSQL.AppendLine("e.listar = 1 and ");
            strSQL.AppendLine("e.cod_banco = 20 ");
            strSQL.AppendLine("join liquidaciones l1 on ");
            strSQL.AppendLine("l.anio=l1.anio and ");
            strSQL.AppendLine("l.cod_tipo_liq=l1.cod_tipo_liq and ");
            strSQL.AppendLine("l.nro_liquidacion = l1.nro_liquidacion ");
            strSQL.AppendLine("Where ");
            strSQL.AppendLine("l.anio=@anio and");
            strSQL.AppendLine("l.cod_tipo_liq=@cod_tipo_liq and");
            strSQL.AppendLine("l.nro_liquidacion=@nro_liquidacion");

            SqlDataReader dr;
            try
            {
                cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@anio", anio);
                cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                cmd.Parameters.AddWithValue("@porcentaje", porcentaje);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd.Connection = cn;
                cmd.Connection.Open();
                dr = cmd.ExecuteReader();
                lst = mapeo(dr);
                return lst;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<Entities.Acred_bco_prov> read()
        {
            try
            {
                List<Entities.Acred_bco_prov> lst = new List<Entities.Acred_bco_prov>();
                using (SqlConnection con = GetConnection("Siimva"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM ACRED_BCO_PROV";
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

        public static Entities.Acred_bco_prov getByPk()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT *FROM ACRED_BCO_PROV WHERE");
                Entities.Acred_bco_prov obj = null;
                using (SqlConnection con = GetConnection("Siimva"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<Entities.Acred_bco_prov> lst = mapeo(dr);
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

        public static int insert(Entities.Acred_bco_prov obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO ACRED_BCO_PROV(");
                sql.AppendLine("tipo_reg");
                sql.AppendLine(", nro_suc");
                sql.AppendLine(", nro_reparto");
                sql.AppendLine(", legajo");
                sql.AppendLine(", tipo_doc");
                sql.AppendLine(", nro_doc");
                sql.AppendLine(", tipo_cuenta");
                sql.AppendLine(", nro_cuenta");
                sql.AppendLine(", monto_sueldo");
                sql.AppendLine(", importe_pesos");
                sql.AppendLine(", importe_cecor");
                sql.AppendLine(", fecha_acreditacion");
                sql.AppendLine(", nro_empresa");
                sql.AppendLine(", nombre");
                sql.AppendLine(", direccion");
                sql.AppendLine(", localidad");
                sql.AppendLine(", cod_postal");
                sql.AppendLine(", telefono");
                sql.AppendLine(", fecha_nacimiento");
                sql.AppendLine(", salto_de_fila");
                sql.AppendLine(")");
                sql.AppendLine("VALUES");
                sql.AppendLine("(");
                sql.AppendLine("@tipo_reg");
                sql.AppendLine(", @nro_suc");
                sql.AppendLine(", @nro_reparto");
                sql.AppendLine(", @legajo");
                sql.AppendLine(", @tipo_doc");
                sql.AppendLine(", @nro_doc");
                sql.AppendLine(", @tipo_cuenta");
                sql.AppendLine(", @nro_cuenta");
                sql.AppendLine(", @monto_sueldo");
                sql.AppendLine(", @importe_pesos");
                sql.AppendLine(", @importe_cecor");
                sql.AppendLine(", @fecha_acreditacion");
                sql.AppendLine(", @nro_empresa");
                sql.AppendLine(", @nombre");
                sql.AppendLine(", @direccion");
                sql.AppendLine(", @localidad");
                sql.AppendLine(", @cod_postal");
                sql.AppendLine(", @telefono");
                sql.AppendLine(", @fecha_nacimiento");
                sql.AppendLine(", @salto_de_fila");
                sql.AppendLine(")");
                sql.AppendLine("SELECT SCOPE_IDENTITY()");
                using (SqlConnection con = GetConnection("Siimva"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@tipo_reg", obj.tipo_reg);
                    cmd.Parameters.AddWithValue("@nro_suc", obj.nro_suc);
                    cmd.Parameters.AddWithValue("@nro_reparto", obj.nro_reparto);
                    cmd.Parameters.AddWithValue("@legajo", obj.legajo);
                    cmd.Parameters.AddWithValue("@tipo_doc", obj.tipo_doc);
                    cmd.Parameters.AddWithValue("@nro_doc", obj.nro_doc);
                    cmd.Parameters.AddWithValue("@tipo_cuenta", obj.tipo_cuenta);
                    cmd.Parameters.AddWithValue("@nro_cuenta", obj.nro_cuenta);
                    cmd.Parameters.AddWithValue("@monto_sueldo", obj.monto_sueldo);
                    cmd.Parameters.AddWithValue("@importe_pesos", obj.importe_pesos);
                    cmd.Parameters.AddWithValue("@importe_cecor", obj.importe_cecor);
                    cmd.Parameters.AddWithValue("@fecha_acreditacion", obj.fecha_acreditacion);
                    cmd.Parameters.AddWithValue("@nro_empresa", obj.nro_empresa);
                    cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("@direccion", obj.direccion);
                    cmd.Parameters.AddWithValue("@localidad", obj.localidad);
                    cmd.Parameters.AddWithValue("@cod_postal", obj.cod_postal);
                    cmd.Parameters.AddWithValue("@telefono", obj.telefono);
                    cmd.Parameters.AddWithValue("@fecha_nacimiento", obj.fecha_nacimiento);
                    cmd.Parameters.AddWithValue("@salto_de_fila", obj.salto_de_fila);
                    cmd.Connection.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void update(Entities.Acred_bco_prov obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE  ACRED_BCO_PROV SET");
                sql.AppendLine("tipo_reg=@tipo_reg");
                sql.AppendLine(", nro_suc=@nro_suc");
                sql.AppendLine(", nro_reparto=@nro_reparto");
                sql.AppendLine(", legajo=@legajo");
                sql.AppendLine(", tipo_doc=@tipo_doc");
                sql.AppendLine(", nro_doc=@nro_doc");
                sql.AppendLine(", tipo_cuenta=@tipo_cuenta");
                sql.AppendLine(", nro_cuenta=@nro_cuenta");
                sql.AppendLine(", monto_sueldo=@monto_sueldo");
                sql.AppendLine(", importe_pesos=@importe_pesos");
                sql.AppendLine(", importe_cecor=@importe_cecor");
                sql.AppendLine(", fecha_acreditacion=@fecha_acreditacion");
                sql.AppendLine(", nro_empresa=@nro_empresa");
                sql.AppendLine(", nombre=@nombre");
                sql.AppendLine(", direccion=@direccion");
                sql.AppendLine(", localidad=@localidad");
                sql.AppendLine(", cod_postal=@cod_postal");
                sql.AppendLine(", telefono=@telefono");
                sql.AppendLine(", fecha_nacimiento=@fecha_nacimiento");
                sql.AppendLine(", salto_de_fila=@salto_de_fila");
                sql.AppendLine("WHERE");
                using (SqlConnection con = GetConnection("Siimva"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@tipo_reg", obj.tipo_reg);
                    cmd.Parameters.AddWithValue("@nro_suc", obj.nro_suc);
                    cmd.Parameters.AddWithValue("@nro_reparto", obj.nro_reparto);
                    cmd.Parameters.AddWithValue("@legajo", obj.legajo);
                    cmd.Parameters.AddWithValue("@tipo_doc", obj.tipo_doc);
                    cmd.Parameters.AddWithValue("@nro_doc", obj.nro_doc);
                    cmd.Parameters.AddWithValue("@tipo_cuenta", obj.tipo_cuenta);
                    cmd.Parameters.AddWithValue("@nro_cuenta", obj.nro_cuenta);
                    cmd.Parameters.AddWithValue("@monto_sueldo", obj.monto_sueldo);
                    cmd.Parameters.AddWithValue("@importe_pesos", obj.importe_pesos);
                    cmd.Parameters.AddWithValue("@importe_cecor", obj.importe_cecor);
                    cmd.Parameters.AddWithValue("@fecha_acreditacion", obj.fecha_acreditacion);
                    cmd.Parameters.AddWithValue("@nro_empresa", obj.nro_empresa);
                    cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("@direccion", obj.direccion);
                    cmd.Parameters.AddWithValue("@localidad", obj.localidad);
                    cmd.Parameters.AddWithValue("@cod_postal", obj.cod_postal);
                    cmd.Parameters.AddWithValue("@telefono", obj.telefono);
                    cmd.Parameters.AddWithValue("@fecha_nacimiento", obj.fecha_nacimiento);
                    cmd.Parameters.AddWithValue("@salto_de_fila", obj.salto_de_fila);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void delete(Acred_bco_provD obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("DELETE ACRED_BCO_PROV ");
                //sql.AppendLine("WHERE");
                using (SqlConnection con = GetConnection("Siimva"))
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

