using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace web.reportes
{
    public class Conceptos_fijosRep
    {
        public int cod_concepto_liq { get; set; }
        public string des_concepto_liq { get; set; }
        public int legajo { get; set; }
        public string nombre { get; set; }
        public int cod_categoria { get; set; }
        public int cod_seccion { get; set; }
        public decimal importe { get; set; }
        public string des_liquidacion { get; set; }
        public int unidades { get; set; }

        public Conceptos_fijosRep()
        {
            cod_concepto_liq = 0;
            des_concepto_liq = "";
            legajo = 0;
            nombre = "";
            cod_categoria = 0;
            cod_seccion = 0;
            importe = 0;
            des_liquidacion = "";
            unidades = 0;
        }
        private static List<Conceptos_fijosRep> mapeo(SqlDataReader dr)
        {
            List<Conceptos_fijosRep> lst = new List<Conceptos_fijosRep>();
            Conceptos_fijosRep obj;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    obj = new Conceptos_fijosRep();
                    if (!dr.IsDBNull(0)) { obj.cod_concepto_liq = dr.GetInt32(0); }
                    if (!dr.IsDBNull(1)) { obj.des_concepto_liq = dr.GetString(1).Trim(); }
                    if (!dr.IsDBNull(2)) { obj.legajo = dr.GetInt32(2); }
                    if (!dr.IsDBNull(3)) { obj.nombre = dr.GetString(3); }
                    if (!dr.IsDBNull(4)) { obj.cod_categoria = dr.GetInt32(4); }
                    if (!dr.IsDBNull(5)) { obj.cod_seccion = dr.GetInt32(5); }
                    if (!dr.IsDBNull(6)) { obj.importe = dr.GetDecimal(6); }
                    if (!dr.IsDBNull(7)) { obj.des_liquidacion = dr.GetString(7); }
                    if (!dr.IsDBNull(8)) { obj.unidades = Convert.ToInt32(dr[8]); }
                    lst.Add(obj);
                }
            }
            return lst;
        }
        public static List<Conceptos_fijosRep> readResumen_cptos(int anio, int cod_tipo_liq, int nro_liq, int fijo)
        {
            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.AppendLine("select");
                strSQL.AppendLine("a.cod_concepto_liq,");
                strSQL.AppendLine("b.des_concepto_liq,");
                strSQL.AppendLine("e.legajo,");
                strSQL.AppendLine("e.nombre,");
                strSQL.AppendLine("e.cod_categoria,");
                strSQL.AppendLine("e.cod_seccion,");
                strSQL.AppendLine("a.importe,");
                strSQL.AppendLine("l1.des_liquidacion,");
                strSQL.AppendLine("a.unidades");
                strSQL.AppendLine("FROM DET_LIQ_X_EMPLEADO a WITH(NOLOCK)");
                strSQL.AppendLine("JOIN EMPLEADOS e on");
                strSQL.AppendLine("a.legajo = e.legajo");
                strSQL.AppendLine("JOIN CONCEPTOS_LIQUIDACION b on");
                strSQL.AppendLine("a.cod_concepto_liq = b.cod_concepto_liq ");
                strSQL.AppendLine("AND");
                if (fijo == 0)
                    strSQL.AppendLine("NOT EXISTS");
                else
                    strSQL.AppendLine("EXISTS");
                strSQL.AppendLine("(SELECT cod_concepto_liq");
                strSQL.AppendLine("FROM PAR_X_DET_LIQ_X_EMPLEADO P");
                strSQL.AppendLine("WHERE");
                strSQL.AppendLine("a.anio = p.anio AND");
                strSQL.AppendLine("a.cod_tipo_liq = p.cod_tipo_liq AND");
                strSQL.AppendLine("a.nro_liquidacion = p.nro_liquidacion AND");
                strSQL.AppendLine("a.cod_concepto_liq = p.cod_concepto_liq AND");
                strSQL.AppendLine("a.legajo = p.legajo)");
                strSQL.AppendLine("JOIN CATEGORIAS c on");
                strSQL.AppendLine("e.cod_categoria = c.cod_categoria");
                strSQL.AppendLine("JOIN SECCIONES d on");
                strSQL.AppendLine("e.cod_seccion = d.cod_seccion");
                strSQL.AppendLine("JOIN LIQUIDACIONES l1 ON");
                strSQL.AppendLine("a.anio = l1.anio AND");
                strSQL.AppendLine("a.cod_tipo_liq = l1.cod_tipo_liq AND");
                strSQL.AppendLine("a.nro_liquidacion = l1.nro_liquidacion");
                strSQL.AppendLine("WHERE");
                strSQL.AppendLine("a.anio=@anio AND");
                strSQL.AppendLine("a.cod_tipo_liq=@cod_tipo_liq AND");
                strSQL.AppendLine("a.nro_liquidacion=@nro_liq");
                strSQL.AppendLine("ORDER by");
                strSQL.AppendLine("a.anio, a.nro_liquidacion, a.cod_concepto_liq, e.legajo");


                List<Conceptos_fijosRep> lst = new List<Conceptos_fijosRep>();
                using (SqlConnection con = DAL.DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liq", nro_liq);
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

        public static List<Conceptos_fijosRep> readResumen_cptos_opedido(int anio, int cod_tipo_liq, int nro_liq, int fijo)
        {
            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.AppendLine("select");
                strSQL.AppendLine("a.cod_concepto_liq,");
                strSQL.AppendLine("b.des_concepto_liq,");
                strSQL.AppendLine("e.legajo,");
                strSQL.AppendLine("e.nombre,");
                strSQL.AppendLine("e.cod_categoria,");
                strSQL.AppendLine("e.cod_seccion,");
                strSQL.AppendLine("a.importe,");
                strSQL.AppendLine("l1.des_liquidacion,");
                strSQL.AppendLine("a.unidades");
                strSQL.AppendLine("FROM DET_LIQ_X_EMPLEADO a WITH(NOLOCK)");
                strSQL.AppendLine("JOIN EMPLEADOS e on");
                strSQL.AppendLine("a.legajo = e.legajo");
                strSQL.AppendLine("JOIN CONCEPTOS_LIQUIDACION b on");
                strSQL.AppendLine("a.cod_concepto_liq = b.cod_concepto_liq ");
                strSQL.AppendLine("AND");
                if (fijo == 0)
                    strSQL.AppendLine("NOT EXISTS");
                else
                    strSQL.AppendLine("EXISTS");
                strSQL.AppendLine("(SELECT cod_concepto_liq");
                strSQL.AppendLine("FROM PAR_X_DET_LIQ_X_EMPLEADO P");
                strSQL.AppendLine("WHERE");
                strSQL.AppendLine("a.anio = p.anio AND");
                strSQL.AppendLine("a.cod_tipo_liq = p.cod_tipo_liq AND");
                strSQL.AppendLine("a.nro_liquidacion = p.nro_liquidacion AND");
                strSQL.AppendLine("a.cod_concepto_liq = p.cod_concepto_liq AND");
                strSQL.AppendLine("a.legajo = p.legajo)");
                strSQL.AppendLine("JOIN CATEGORIAS c on");
                strSQL.AppendLine("e.cod_categoria = c.cod_categoria");
                strSQL.AppendLine("JOIN SECCIONES d on");
                strSQL.AppendLine("e.cod_seccion = d.cod_seccion");
                strSQL.AppendLine("JOIN LIQUIDACIONES l1 ON");
                strSQL.AppendLine("a.anio = l1.anio AND");
                strSQL.AppendLine("a.cod_tipo_liq = l1.cod_tipo_liq AND");
                strSQL.AppendLine("a.nro_liquidacion = l1.nro_liquidacion");
                strSQL.AppendLine("WHERE");
                strSQL.AppendLine("a.anio=@anio AND");
                strSQL.AppendLine("a.cod_tipo_liq=@cod_tipo_liq AND");
                strSQL.AppendLine("a.nro_liquidacion=@nro_liq AND");
                strSQL.AppendLine("a.cod_concepto_liq in (609,635,650,695,611,624,627,660,680,681,693,810,819,824,827,828,833,835)");
                strSQL.AppendLine("ORDER by");
                strSQL.AppendLine("a.anio, a.nro_liquidacion, a.cod_concepto_liq, e.legajo");

                List<Conceptos_fijosRep> lst = new List<Conceptos_fijosRep>();
                using (SqlConnection con = DAL.DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liq", nro_liq);
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

        public static List<Conceptos_fijosRep> readResumen_cptos(int anio, int cod_tipo_liq, int nro_liq)
        {
            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.AppendLine("select");
                strSQL.AppendLine("a.cod_concepto_liq,");
                strSQL.AppendLine("b.des_concepto_liq,");
                strSQL.AppendLine("e.legajo,");
                strSQL.AppendLine("e.nombre,");
                strSQL.AppendLine("e.cod_categoria,");
                strSQL.AppendLine("e.cod_seccion,");
                strSQL.AppendLine("a.importe,");
                strSQL.AppendLine("l.des_liquidacion,");
                strSQL.AppendLine("a.unidades");
                strSQL.AppendLine("FROM DET_LIQ_X_EMPLEADO a WITH(NOLOCK)");
                strSQL.AppendLine("JOIN EMPLEADOS e on");
                strSQL.AppendLine("a.legajo = e.legajo");
                //strSQL.AppendLine("JOIN CONCEP_LIQUID_X_EMPLEADO cle on");
                //strSQL.AppendLine("a.cod_concepto_liq = cle.cod_concepto_liq ");
                strSQL.AppendLine("JOIN CONCEPTOS_LIQUIDACION b on");
                strSQL.AppendLine("a.cod_concepto_liq = b.cod_concepto_liq ");
                strSQL.AppendLine("join LIQUIDACIONES l on");
                strSQL.AppendLine(" l.anio=a.anio AND");
                strSQL.AppendLine(" l.cod_tipo_liq=a.cod_tipo_liq AND");
                strSQL.AppendLine(" l.nro_liquidacion = a.nro_liquidacion ");
                strSQL.AppendLine("WHERE");
                strSQL.AppendLine("a.anio=@anio AND");
                strSQL.AppendLine("a.cod_tipo_liq=@cod_tipo_liq AND");
                strSQL.AppendLine("a.nro_liquidacion=@nro_liq");
                strSQL.AppendLine("ORDER by");
                strSQL.AppendLine("a.anio, a.nro_liquidacion, a.cod_concepto_liq, e.legajo");
                List<Conceptos_fijosRep> lst = new List<Conceptos_fijosRep>();
                using (SqlConnection con = DAL.DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liq", nro_liq);
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

        public static List<Conceptos_fijosRep> readResumen_cptosAnio_Tipo_Nro_liq(int anio, int cod_tipo_liq, int nro_liq)
        {
            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.AppendLine("select");
                strSQL.AppendLine("a.cod_concepto_liq,");
                strSQL.AppendLine("b.des_concepto_liq,");
                strSQL.AppendLine("e.legajo,");
                strSQL.AppendLine("e.nombre,");
                strSQL.AppendLine("e.cod_categoria,");
                strSQL.AppendLine("e.cod_seccion,");
                strSQL.AppendLine("a.importe,");
                strSQL.AppendLine("l.des_liquidacion,");
                strSQL.AppendLine("a.unidades");
                strSQL.AppendLine("FROM DET_LIQ_X_EMPLEADO a WITH(NOLOCK)");
                strSQL.AppendLine("JOIN EMPLEADOS e on");
                strSQL.AppendLine("a.legajo = e.legajo");
                strSQL.AppendLine("JOIN CONCEPTOS_LIQUIDACION b on");
                strSQL.AppendLine("a.cod_concepto_liq = b.cod_concepto_liq ");
                strSQL.AppendLine("join LIQUIDACIONES l on");
                strSQL.AppendLine(" l.anio=a.anio AND");
                strSQL.AppendLine(" l.cod_tipo_liq=a.cod_tipo_liq AND");
                strSQL.AppendLine(" l.nro_liquidacion = a.nro_liquidacion ");
                strSQL.AppendLine("WHERE");
                strSQL.AppendLine("a.anio=@anio AND");
                strSQL.AppendLine("a.cod_tipo_liq=@cod_tipo_liq AND");
                strSQL.AppendLine("a.nro_liquidacion=@nro_liq");
                strSQL.AppendLine("ORDER by");
                strSQL.AppendLine("a.anio, a.nro_liquidacion, a.cod_concepto_liq, e.legajo");
                List<Conceptos_fijosRep> lst = new List<Conceptos_fijosRep>();
                using (SqlConnection con = DAL.DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liq", nro_liq);
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
        public static List<Conceptos_fijosRep> readResumen_cptosAnio_Tipo_Anio_Desde_Hasta(int anio_desde, int anio_hasta, int cod_tipo_liq, int nro_liq)
        {
            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.AppendLine("select");
                strSQL.AppendLine("a.cod_concepto_liq,");
                strSQL.AppendLine("b.des_concepto_liq,");
                strSQL.AppendLine("e.legajo,");
                strSQL.AppendLine("e.nombre,");
                strSQL.AppendLine("e.cod_categoria,");
                strSQL.AppendLine("e.cod_seccion,");
                strSQL.AppendLine("a.importe,");
                strSQL.AppendLine("l.des_liquidacion,");
                strSQL.AppendLine("a.unidades");
                strSQL.AppendLine("FROM DET_LIQ_X_EMPLEADO a WITH(NOLOCK)");
                strSQL.AppendLine("JOIN EMPLEADOS e on");
                strSQL.AppendLine("a.legajo = e.legajo");
                strSQL.AppendLine("JOIN CONCEPTOS_LIQUIDACION b on");
                strSQL.AppendLine("a.cod_concepto_liq = b.cod_concepto_liq ");
                strSQL.AppendLine("join LIQUIDACIONES l on");
                strSQL.AppendLine(" l.anio=a.anio AND");
                strSQL.AppendLine(" l.cod_tipo_liq=a.cod_tipo_liq AND");
                strSQL.AppendLine(" l.nro_liquidacion = a.nro_liquidacion ");
                strSQL.AppendLine("WHERE");
                strSQL.AppendLine("a.anio between @anio_desde AND @anio_hasta AND");
                strSQL.AppendLine("a.cod_tipo_liq=@cod_tipo_liq");
                //strSQL.AppendLine("AND a.nro_liquidacion=@nro_liq");
                strSQL.AppendLine("ORDER by");
                strSQL.AppendLine("a.anio, a.nro_liquidacion, a.cod_concepto_liq, e.legajo");
                List<Conceptos_fijosRep> lst = new List<Conceptos_fijosRep>();
                using (SqlConnection con = DAL.DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Parameters.AddWithValue("@anio_desde", anio_desde);
                    cmd.Parameters.AddWithValue("@anio_hasta", anio_hasta);

                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
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

        public static List<Conceptos_fijosRep> readResumen_cptosAnio_Tipo_liq(int anio, int cod_tipo_liq)
        {
            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.AppendLine("select");
                strSQL.AppendLine("a.cod_concepto_liq,");
                strSQL.AppendLine("b.des_concepto_liq,");
                strSQL.AppendLine("e.legajo,");
                strSQL.AppendLine("e.nombre,");
                strSQL.AppendLine("e.cod_categoria,");
                strSQL.AppendLine("e.cod_seccion,");
                strSQL.AppendLine("a.importe,");
                strSQL.AppendLine("l.des_liquidacion,");
                strSQL.AppendLine("a.unidades");
                strSQL.AppendLine("FROM DET_LIQ_X_EMPLEADO a WITH(NOLOCK)");
                strSQL.AppendLine("JOIN EMPLEADOS e on");
                strSQL.AppendLine("a.legajo = e.legajo");
                strSQL.AppendLine("JOIN CONCEPTOS_LIQUIDACION b on");
                strSQL.AppendLine("a.cod_concepto_liq = b.cod_concepto_liq ");
                strSQL.AppendLine("join LIQUIDACIONES l on");
                strSQL.AppendLine(" l.anio=a.anio AND");
                strSQL.AppendLine(" l.cod_tipo_liq=a.cod_tipo_liq AND");
                strSQL.AppendLine(" l.nro_liquidacion = a.nro_liquidacion ");
                strSQL.AppendLine("WHERE");
                strSQL.AppendLine("a.anio=@anio AND");
                strSQL.AppendLine("a.cod_tipo_liq=@cod_tipo_liq");
                //strSQL.AppendLine("a.nro_liquidacion=@nro_liq");
                strSQL.AppendLine("ORDER by");
                strSQL.AppendLine("a.anio, a.nro_liquidacion, a.cod_concepto_liq, e.legajo");
                List<Conceptos_fijosRep> lst = new List<Conceptos_fijosRep>();
                using (SqlConnection con = DAL.DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
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
        public static List<Conceptos_fijosRep> readResumen_cptosAnio_desde_hasta(int anio_desde, int anio_hasta, int cod_tipo_liq)
        {
            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.AppendLine("select");
                strSQL.AppendLine("a.cod_concepto_liq,");
                strSQL.AppendLine("b.des_concepto_liq,");
                strSQL.AppendLine("e.legajo,");
                strSQL.AppendLine("e.nombre,");
                strSQL.AppendLine("e.cod_categoria,");
                strSQL.AppendLine("e.cod_seccion,");
                strSQL.AppendLine("a.importe,");
                strSQL.AppendLine("l.des_liquidacion,");
                strSQL.AppendLine("a.unidades");
                strSQL.AppendLine("FROM DET_LIQ_X_EMPLEADO a WITH(NOLOCK)");
                strSQL.AppendLine("JOIN EMPLEADOS e on");
                strSQL.AppendLine("a.legajo = e.legajo");
                strSQL.AppendLine("JOIN CONCEPTOS_LIQUIDACION b on");
                strSQL.AppendLine("a.cod_concepto_liq = b.cod_concepto_liq ");
                strSQL.AppendLine("join LIQUIDACIONES l on");
                strSQL.AppendLine(" l.anio=a.anio AND");
                strSQL.AppendLine(" l.cod_tipo_liq=a.cod_tipo_liq AND");
                strSQL.AppendLine(" l.nro_liquidacion = a.nro_liquidacion ");
                strSQL.AppendLine("WHERE");
                strSQL.AppendLine("a.anio between @anio_desde AND @anio_hasta AND");
                strSQL.AppendLine("a.cod_tipo_liq=@cod_tipo_liq");
                //strSQL.AppendLine("a.nro_liquidacion=@nro_liq");
                strSQL.AppendLine("ORDER by");
                strSQL.AppendLine("a.anio, a.nro_liquidacion, a.cod_concepto_liq, e.legajo");
                List<Conceptos_fijosRep> lst = new List<Conceptos_fijosRep>();
                using (SqlConnection con = DAL.DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Parameters.AddWithValue("@anio_desde", anio_desde);
                    cmd.Parameters.AddWithValue("@anio_hasta", anio_hasta);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
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

        //public static List<Conceptos_fijosRep> readResumen_cptos_variables(int anio, int cod_tipo_liq, int nro_liq, bool fijo)
        //{
        //    try
        //    {
        //        StringBuilder strSQL = new StringBuilder();
        //        strSQL.AppendLine("select");
        //        strSQL.AppendLine("a.cod_concepto_liq,");
        //        strSQL.AppendLine("b.des_concepto_liq,");
        //        strSQL.AppendLine("e.legajo,");
        //        strSQL.AppendLine("e.nombre,");
        //        strSQL.AppendLine("e.cod_categoria,");
        //        strSQL.AppendLine("e.cod_seccion,");
        //        strSQL.AppendLine("a.importe,");
        //        strSQL.AppendLine("l1.des_liquidacion,");
        //        strSQL.AppendLine("a.unidades");
        //        strSQL.AppendLine("FROM DET_LIQ_X_EMPLEADO a WITH(NOLOCK)");
        //        strSQL.AppendLine("JOIN EMPLEADOS e on");
        //        strSQL.AppendLine("a.legajo = e.legajo");
        //        strSQL.AppendLine("JOIN CONCEPTOS_LIQUIDACION b on");
        //        strSQL.AppendLine("a.cod_concepto_liq = b.cod_concepto_liq ");
        //        strSQL.AppendLine("AND");
        //        strSQL.AppendLine("NOT EXISTS");
        //        strSQL.AppendLine("(SELECT cod_concepto_liq");
        //        strSQL.AppendLine("FROM PAR_X_DET_LIQ_X_EMPLEADO P");
        //        strSQL.AppendLine("WHERE");
        //        strSQL.AppendLine("a.anio = p.anio AND");
        //        strSQL.AppendLine("a.cod_tipo_liq = p.cod_tipo_liq AND");
        //        strSQL.AppendLine("a.nro_liquidacion = p.nro_liquidacion AND");
        //        strSQL.AppendLine("a.cod_concepto_liq = p.cod_concepto_liq AND");
        //        strSQL.AppendLine("a.legajo = p.legajo)");
        //        strSQL.AppendLine("JOIN CATEGORIAS c on");
        //        strSQL.AppendLine("e.cod_categoria = c.cod_categoria");
        //        strSQL.AppendLine("JOIN SECCIONES d on");
        //        strSQL.AppendLine("e.cod_seccion = d.cod_seccion");
        //        strSQL.AppendLine("JOIN LIQUIDACIONES l1 ON");
        //        strSQL.AppendLine("a.anio = l1.anio AND");
        //        strSQL.AppendLine("a.cod_tipo_liq = l1.cod_tipo_liq AND");
        //        strSQL.AppendLine("a.nro_liquidacion = l1.nro_liquidacion");
        //        strSQL.AppendLine("WHERE");
        //        strSQL.AppendLine("a.anio=@anio AND");
        //        strSQL.AppendLine("a.cod_tipo_liq=@cod_tipo_liq AND");
        //        strSQL.AppendLine("a.nro_liquidacion=@nro_liq");
        //        strSQL.AppendLine("ORDER by");
        //        strSQL.AppendLine("a.cod_concepto_liq, e.legajo");


        //        List<Conceptos_fijosRep> lst = new List<Conceptos_fijosRep>();
        //        using (SqlConnection con = DAL.DALBase.GetConnection("SIIMVA"))
        //        {
        //            SqlCommand cmd = con.CreateCommand();
        //            cmd.CommandType = CommandType.Text;
        //            cmd.CommandText = strSQL.ToString();
        //            cmd.Parameters.AddWithValue("@anio", anio);
        //            cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
        //            cmd.Parameters.AddWithValue("@nro_liq", nro_liq);
        //            //cmd.Parameters.AddWithValue("@porcentaje", porcentaje);
        //            cmd.Connection.Open();
        //            SqlDataReader dr = cmd.ExecuteReader();
        //            lst = mapeo(dr);
        //            return lst;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

    }
}