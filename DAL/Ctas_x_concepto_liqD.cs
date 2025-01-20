using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DAL
{
    public class Ctas_x_concepto_liqD
    {


        public static List<Entities.Ctas_x_concepto_liq> GetByPk(int cod_concepto_liq)
        {
            List<Entities.Ctas_x_concepto_liq> oList = new List<Entities.Ctas_x_concepto_liq>();
            Entities.Ctas_x_concepto_liq obj = null; //new Entities.Ctas_x_concepto_liq();
            SqlCommand cmd;
            SqlDataReader dr;
            StringBuilder strSQL = new StringBuilder();

            strSQL.AppendLine("select a.cod_concepto_liq, a.cod_tipo_liq, a.cod_clasif_per, a.nro_cta_contable, p.nom_cta, a.fecha_alta_registro,");
            strSQL.AppendLine("b.des_concepto_liq");
            //strSQL.AppendLine("d.des_tipo_liq, c.des_clasif_per,"); 
            strSQL.AppendLine("from Ctas_x_concepto_liq a");
            strSQL.AppendLine("join CONCEPTOS_LIQUIDACION b on");
            strSQL.AppendLine("a.cod_concepto_liq=b.cod_concepto_liq");
            strSQL.AppendLine("join Plan_ctas_egreso p on ");
            strSQL.AppendLine("p.nro_cta = a.nro_cta_contable ");
            //strSQL.AppendLine("join CLASIFICACIONES_PERSONAL c on");
            //strSQL.AppendLine("a.cod_clasif_per=c.cod_clasif_per");
            //strSQL.AppendLine("join TIPOS_LIQUIDACION D on");
            //strSQL.AppendLine("a.cod_tipo_liq=d.cod_tipo_liq");
            strSQL.AppendLine("WHERE a.cod_concepto_liq = @cod_concepto_liq");

            cmd = new SqlCommand();

            cmd.Parameters.Add(new SqlParameter("@cod_concepto_liq", cod_concepto_liq));

            try
            {
                using (SqlConnection conn = DALBase.GetConnection("Siimva"))
                {
                    try
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = strSQL.ToString();
                        cmd.Connection.Open();
                        dr = cmd.ExecuteReader();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    while (dr.Read())
                    {
                        obj = new Entities.Ctas_x_concepto_liq();

                        if (!dr.IsDBNull(dr.GetOrdinal("cod_concepto_liq")))
                            obj.cod_concepto_liq = dr.GetInt32((dr.GetOrdinal("cod_concepto_liq")));

                        if (!dr.IsDBNull(dr.GetOrdinal("des_concepto_liq")))
                            obj.des_concepto_liq = dr.GetString((dr.GetOrdinal("des_concepto_liq")));

                        if (!dr.IsDBNull(dr.GetOrdinal("cod_tipo_liq")))
                            obj.cod_tipo_liq = dr.GetInt32((dr.GetOrdinal("cod_tipo_liq")));

                        //if (!dr.IsDBNull(dr.GetOrdinal("des_tipo_liq")))
                        //  obj.des_tipo_liq = dr.GetString((dr.GetOrdinal("des_tipo_liq")));

                        if (!dr.IsDBNull(dr.GetOrdinal("cod_clasif_per")))
                            obj.cod_clasif_per = dr.GetInt32((dr.GetOrdinal("cod_clasif_per")));

                        if (!dr.IsDBNull(dr.GetOrdinal("nom_cta")))
                            obj.nom_cta = dr.GetString((dr.GetOrdinal("nom_cta")));

                        if (!dr.IsDBNull(dr.GetOrdinal("nro_cta_contable")))
                            obj.nro_cta = dr.GetString((dr.GetOrdinal("nro_cta_contable")));

                        if (!dr.IsDBNull(dr.GetOrdinal("fecha_alta_registro")))
                            obj.fecha_alta_registro = Convert.ToString((dr.GetOrdinal("fecha_alta_registro")));

                        oList.Add(obj);
                    }
                    dr.Close();
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            finally
            { cmd = null; strSQL = null; }

            return oList;
        }

        public static void NuevaCuenta(Ctas_x_concepto_liq oCta)
        {
            SqlCommand cmd = null;
            SqlConnection cn = DALBase.GetConnection("Siimva");
            StringBuilder strSQL = new StringBuilder();
            try
            {

                strSQL.AppendLine("INSERT into Ctas_x_concepto_liq");
                strSQL.AppendLine("(cod_concepto_liq,");
                strSQL.AppendLine("cod_tipo_liq,");
                strSQL.AppendLine("cod_clasif_per,");
                strSQL.AppendLine("nro_cta_contable,");
                strSQL.AppendLine("fecha_alta_registro)");
                strSQL.AppendLine("VALUES");
                strSQL.AppendLine("(@cod_concepto_liq,");
                strSQL.AppendLine("@cod_tipo_liq,");
                strSQL.AppendLine("@cod_clasif_per,");
                strSQL.AppendLine("@nro_cta_contable,");
                strSQL.AppendLine("@fecha_alta_registro)");

                cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@cod_concepto_liq", oCta.cod_concepto_liq);
                cmd.Parameters.AddWithValue("@cod_tipo_liq", oCta.cod_tipo_liq);
                cmd.Parameters.AddWithValue("@cod_clasif_per", oCta.cod_clasif_per);
                cmd.Parameters.AddWithValue("@nro_cta_contable", oCta.nro_cta);
                cmd.Parameters.AddWithValue("@fecha_alta_registro", oCta.fecha_alta_registro);
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw e;
            }

            finally
            {
                cmd = null;
                cn.Close();
            }
        }


        public static void ModificaCuenta(Ctas_x_concepto_liq oCta)
        {
            SqlCommand cmd = null;
            SqlConnection cn = DALBase.GetConnection("Siimva");
            StringBuilder strSQL = new StringBuilder();
            try
            {

                strSQL.AppendLine("UPDATE Ctas_x_concepto_liq ");
                strSQL.AppendLine(" Set nro_cta_contable=@nro_cta_contable ");
                strSQL.AppendLine("WHERE cod_concepto_liq=@cod_concepto_liq AND");
                strSQL.AppendLine("cod_tipo_liq=@cod_tipo_liq AND");
                strSQL.AppendLine("cod_clasif_per=@cod_clasif_per ");
                //strSQL.AppendLine("nro_cta_contable=@nro_cta_contable");

                cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@cod_concepto_liq", oCta.cod_concepto_liq);
                cmd.Parameters.AddWithValue("@cod_tipo_liq", oCta.cod_tipo_liq);
                cmd.Parameters.AddWithValue("@cod_clasif_per", oCta.cod_clasif_per);
                cmd.Parameters.AddWithValue("@nro_cta_contable", oCta.nro_cta);
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw e;
            }

            finally
            {
                cmd = null;
                cn.Close();
            }
        }


        public static void EliminaCuenta(Ctas_x_concepto_liq oCta)
        {
            SqlCommand cmd = null;
            SqlConnection cn = DALBase.GetConnection("Siimva");
            StringBuilder strSQL = new StringBuilder();
            try
            {
                strSQL.AppendLine("delete from Ctas_x_concepto_liq WHERE ");
                strSQL.AppendLine("cod_tipo_liq=@cod_tipo_liq AND");
                strSQL.AppendLine("cod_clasif_per=@cod_clasif_per AND");
                strSQL.AppendLine("nro_cta_contable=@nro_cta_contable AND");
                strSQL.AppendLine("cod_concepto_liq=@cod_concepto_liq");
                cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@cod_concepto_liq", oCta.cod_concepto_liq);
                cmd.Parameters.AddWithValue("@cod_tipo_liq", oCta.cod_tipo_liq);
                cmd.Parameters.AddWithValue("@cod_clasif_per", oCta.cod_clasif_per);
                cmd.Parameters.AddWithValue("@nro_cta_contable", oCta.nro_cta);
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                cmd = null;
                cn.Close();
            }
        }


        public static List<Entities.Ctas_x_concepto_liq> GetPlan_cta_egreso(int cod_concepto_liq, int cod_clasif_per, int cod_tipo_liq)
        {
            Entities.Ctas_x_concepto_liq obj = null; //new Entities.Ctas_x_concepto_liq();
            List<Entities.Ctas_x_concepto_liq> lst = new List<Entities.Ctas_x_concepto_liq>();
            SqlCommand cmd;
            SqlDataReader dr;
            StringBuilder strSQL = new StringBuilder();

            strSQL.AppendLine("select c.nro_cta, c.nom_cta");
            //strSQL.AppendLine("d.des_tipo_liq, c.des_clasif_per,"); 
            strSQL.AppendLine("from Ctas_x_concepto_liq a");
            //strSQL.AppendLine("join CONCEPTOS_LIQUIDACION b on");
            //strSQL.AppendLine("a.cod_concepto_liq=b.cod_concepto_liq");
            strSQL.AppendLine("join PLAN_CTAS_EGRESO c on");
            strSQL.AppendLine("c.nro_cta=a.nro_cta_contable");
            //strSQL.AppendLine("join TIPOS_LIQUIDACION D on");
            //strSQL.AppendLine("a.cod_tipo_liq=d.cod_tipo_liq");
            strSQL.AppendLine("WHERE a.cod_concepto_liq = @cod_concepto_liq and ");
            strSQL.AppendLine(" a.cod_clasif_per = @cod_clasif_per and ");
            strSQL.AppendLine(" a.cod_tipo_liq = @cod_tipo_liq");
            strSQL.AppendLine(" ORDER BY c.nro_cta");
            cmd = new SqlCommand();

            cmd.Parameters.Add(new SqlParameter("@cod_concepto_liq", cod_concepto_liq));
            cmd.Parameters.Add(new SqlParameter("@cod_clasif_per", cod_clasif_per));
            cmd.Parameters.Add(new SqlParameter("@cod_tipo_liq", cod_tipo_liq));
            try
            {
                using (SqlConnection conn = DALBase.GetConnection("Siimva"))
                {
                    try
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = strSQL.ToString();
                        cmd.Connection.Open();
                        dr = cmd.ExecuteReader();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    while (dr.Read())
                    {
                        obj = new Entities.Ctas_x_concepto_liq();

                        if (!dr.IsDBNull(dr.GetOrdinal("nro_cta")))
                            obj.nro_cta = dr.GetString((dr.GetOrdinal("nro_cta")));

                        if (!dr.IsDBNull(dr.GetOrdinal("nom_cta")))
                            obj.nom_cta = dr.GetString((dr.GetOrdinal("nom_cta")));

                        obj.nro_con_des = string.Format("{0} - {1}", obj.nro_cta, obj.nom_cta);

                        lst.Add(obj);

                    }
                    dr.Close();
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            finally
            { cmd = null; strSQL = null; }

            return lst;
        }




        public static List<Entities.Ctas_x_concepto_liq> GetPlan_cta_egreso()
        {
            Entities.Ctas_x_concepto_liq obj = null; //new Entities.Ctas_x_concepto_liq();
            List<Entities.Ctas_x_concepto_liq> lst = new List<Entities.Ctas_x_concepto_liq>();
            SqlCommand cmd;
            SqlDataReader dr;
            StringBuilder strSQL = new StringBuilder();

            strSQL.AppendLine("SELECT c.nro_cta, c.nom_cta");
            strSQL.AppendLine("FROM PLAN_CTAS_EGRESO c");
            strSQL.AppendLine("ORDER BY c.nro_cta");
            cmd = new SqlCommand();
            //cmd.Parameters.Add(new SqlParameter("@cod_concepto_liq", cod_concepto_liq));
            //cmd.Parameters.Add(new SqlParameter("@cod_clasif_per", cod_clasif_per));
            //cmd.Parameters.Add(new SqlParameter("@cod_tipo_liq", cod_tipo_liq));
            try
            {
                using (SqlConnection conn = DALBase.GetConnection("Siimva"))
                {
                    try
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = strSQL.ToString();
                        cmd.Connection.Open();
                        dr = cmd.ExecuteReader();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    while (dr.Read())
                    {
                        obj = new Entities.Ctas_x_concepto_liq();

                        if (!dr.IsDBNull(dr.GetOrdinal("nro_cta")))
                            obj.nro_cta = dr.GetString((dr.GetOrdinal("nro_cta")));

                        if (!dr.IsDBNull(dr.GetOrdinal("nom_cta")))
                            obj.nom_cta = dr.GetString((dr.GetOrdinal("nom_cta")));

                        obj.nro_con_des = string.Format("{0} - {1}", obj.nro_cta, obj.nom_cta);

                        lst.Add(obj);

                    }
                    dr.Close();
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            finally
            { cmd = null; strSQL = null; }

            return lst;
        }


        public static List<Entities.LstCtas_x_concepto> GetCtas_x_conceptos()
        {
            List<Entities.LstCtas_x_concepto> lst = new List<Entities.LstCtas_x_concepto>();
            SqlCommand cmd;
            SqlDataReader dr;
            string strSQL;
            strSQL = @"Select 
                          a.cod_concepto_liq,
                          b.des_concepto_liq,
                          a.fecha_alta_registro,
                          t.cod_tipo_liq,
                          t.des_tipo_liq,
                          c.cod_clasif_per,
                          c.des_clasif_per,
                          p.nom_cta,
                          p.nro_cta
                        from CTAS_X_CONCEPTO_LIQ a
                        left join CONCEPTOS_LIQUIDACION b on
                        a.cod_concepto_liq=b.cod_concepto_liq
                        left join CLASIFICACIONES_PERSONAL c on
                        a.cod_clasif_per=c.cod_clasif_per
                        left join TIPOS_LIQUIDACION t on
                        a.cod_tipo_liq=t.cod_tipo_liq
                        left join PLAN_CTAS_EGRESO p on
                        a.nro_cta_contable=p.nro_cta
                        order by 1,9 ";
            cmd = new SqlCommand();
            try
            {
                using (SqlConnection conn = DALBase.GetConnection("Siimva"))
                {
                    try
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = strSQL.ToString();
                        cmd.Connection.Open();
                        dr = cmd.ExecuteReader();
                        lst = mapeoCtas_x_concepto(dr);
                        return lst;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            finally
            { cmd = null; strSQL = null; }
        }


        private static List<LstCtas_x_concepto> mapeoCtas_x_concepto(SqlDataReader dr)
        {
            DateTimeFormatInfo culturaFecArgentina = new CultureInfo("es-AR", false).DateTimeFormat;
            List<LstCtas_x_concepto> lst = new List<LstCtas_x_concepto>();
            LstCtas_x_concepto obj;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    obj = new LstCtas_x_concepto();
                    if (!dr.IsDBNull(0)) { obj.cod_concepto_liq = dr.GetInt32(0); }
                    if (!dr.IsDBNull(1)) { obj.des_concepto_liq = dr.GetString(1); }
                    if (!dr.IsDBNull(2)) { obj.fecha_alta_registro = Convert.ToDateTime(dr.GetDateTime(2), culturaFecArgentina).ToShortDateString(); }
                    if (!dr.IsDBNull(3)) { obj.cod_tipo_liq = dr.GetInt32(3); }
                    if (!dr.IsDBNull(4)) { obj.des_tipo_liq = dr.GetString(4); }
                    if (!dr.IsDBNull(5)) { obj.cod_clasif_per = dr.GetInt32(5); }
                    if (!dr.IsDBNull(6)) { obj.des_concepto_liq = dr.GetString(6); }
                    if (!dr.IsDBNull(7)) { obj.nom_cta = dr.GetString(7); }
                    if (!dr.IsDBNull(8)) { obj.nro_cta = dr.GetString(8); }                    
                    lst.Add(obj);
                }
            }
            return lst;
        }

    }
}
