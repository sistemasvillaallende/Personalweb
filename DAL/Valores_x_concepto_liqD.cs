using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DAL
{
    public class Valores_x_concepto_liqD
    {
        public static List<Valores_x_concepto_liq> GetByPk(int cod_concepto_liq, object nro_valor)
        {
            List<Entities.Valores_x_concepto_liq> oList = new List<Entities.Valores_x_concepto_liq>();
            Entities.Valores_x_concepto_liq obj = null; //new Entities.Ctas_x_concepto_liq();
            SqlCommand cmd;
            SqlDataReader dr;
            StringBuilder strSQL = new StringBuilder();



            strSQL.AppendLine("select a.cod_concepto_liq, a.nro_valor, a.fecha_alta_registro, valor");
            strSQL.AppendLine("from Valores_x_concepto_liq a");
            strSQL.AppendLine("WHERE a.cod_concepto_liq = @cod_concepto_liq and nro_valor=@nro_valor");

            cmd = new SqlCommand();

            cmd.Parameters.Add(new SqlParameter("@cod_concepto_liq", cod_concepto_liq));
            cmd.Parameters.Add(new SqlParameter("@nro_valor", nro_valor));
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
                        obj = new Entities.Valores_x_concepto_liq();

                        if (!dr.IsDBNull(dr.GetOrdinal("cod_concepto_liq")))
                            obj.cod_concepto_liq = dr.GetInt32((dr.GetOrdinal("cod_concepto_liq")));

                        if (!dr.IsDBNull(dr.GetOrdinal("nro_valor")))
                            obj.nro_valor = dr.GetInt32((dr.GetOrdinal("nro_valor")));

                        if (!dr.IsDBNull(dr.GetOrdinal("valor")))
                            obj.valor = dr.GetDecimal((dr.GetOrdinal("valor")));

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

        public static List<Valores_x_concepto_liq> GetValores(int cod_concepto_liq)
        {
            Entities.Valores_x_concepto_liq obj = null; //new Entities.Ctas_x_concepto_liq();
            List<Entities.Valores_x_concepto_liq> lst = new List<Entities.Valores_x_concepto_liq>();
            SqlCommand cmd;
            SqlDataReader dr;
            StringBuilder strSQL = new StringBuilder();

            strSQL.AppendLine("select c.nro_valor, c.valor");
            strSQL.AppendLine("from Valores_x_concepto_liq c");
            strSQL.AppendLine("WHERE c.cod_concepto_liq = @cod_concepto_liq");
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
                        obj = new Entities.Valores_x_concepto_liq();

                        if (!dr.IsDBNull(dr.GetOrdinal("nro_valor")))
                            obj.nro_valor = dr.GetInt32((dr.GetOrdinal("nro_valor")));

                        if (!dr.IsDBNull(dr.GetOrdinal("valor")))
                            obj.valor = dr.GetDecimal((dr.GetOrdinal("valor")));

                        obj.nro_valor_des = string.Format("{0} - {1}", obj.nro_valor.ToString(), obj.valor.ToString());

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


        public static void NuevoValor(Entities.Valores_x_concepto_liq oVal)
        {
            SqlCommand cmd = null;
            SqlCommand cmd1 = null;
            SqlConnection cn = DALBase.GetConnection("Siimva");
            StringBuilder strSQL = new StringBuilder();
            cn.Open();
            try
            {
                if (oVal.nro_valor == 0)
                {
                    StringBuilder SQL = new StringBuilder();
                    SQL.AppendLine("SELECT isnull(max(nro_valor),0) FROM Valores_x_concepto_liq a");
                    SQL.AppendLine("WHERE  a.cod_concepto_liq = @cod_concepto_liq");
                    cmd1 = new SqlCommand();
                    cmd1.Parameters.AddWithValue("@cod_concepto_liq", oVal.cod_concepto_liq);
                    cmd1.Connection = cn;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = SQL.ToString();
                    //cmd1.Connection.Open();
                    oVal.nro_valor = Convert.ToInt32(cmd1.ExecuteScalar()) + 1;
                }
                strSQL.AppendLine("INSERT into Valores_x_concepto_liq");
                strSQL.AppendLine("(cod_concepto_liq,");
                strSQL.AppendLine("nro_valor,");
                strSQL.AppendLine("fecha_alta_registro,");
                strSQL.AppendLine("valor)");
                strSQL.AppendLine("VALUES");
                strSQL.AppendLine("(@cod_concepto_liq,");
                strSQL.AppendLine("@nro_valor,");
                strSQL.AppendLine("@fecha_alta_registro,");
                strSQL.AppendLine("@valor)");

                cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@cod_concepto_liq", oVal.cod_concepto_liq);
                cmd.Parameters.AddWithValue("@nro_valor", oVal.nro_valor);
                cmd.Parameters.AddWithValue("@valor", oVal.valor);
                cmd.Parameters.AddWithValue("@fecha_alta_registro", oVal.fecha_alta_registro);
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                //cmd.Connection.Open();
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


        public static void ModificaValor(Valores_x_concepto_liq oVal)
        {
            SqlCommand cmd = null;
            SqlConnection cn = DALBase.GetConnection("Siimva");
            StringBuilder strSQL = new StringBuilder();
            try
            {

                strSQL.AppendLine("UPDATE Valores_x_concepto_liq SET");
                strSQL.AppendLine("valor=@valor");
                strSQL.AppendLine("WHERE cod_concepto_liq=@cod_concepto_liq AND");
                strSQL.AppendLine("nro_valor=@nro_valor");
                cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@cod_concepto_liq", oVal.cod_concepto_liq);
                cmd.Parameters.AddWithValue("@nro_valor", oVal.nro_valor);
                cmd.Parameters.AddWithValue("@valor", oVal.valor);
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


        public static void EliminaValor(Valores_x_concepto_liq oVal)
        {
            SqlCommand cmd = null;
            SqlConnection cn = DALBase.GetConnection("Siimva");
            StringBuilder strSQL = new StringBuilder();
            try
            {

                strSQL.AppendLine("DELETE FROM Valores_x_concepto_liq");
                strSQL.AppendLine("WHERE cod_concepto_liq=@cod_concepto_liq AND");
                strSQL.AppendLine("nro_valor=@nro_valor");
                cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@cod_concepto_liq", oVal.cod_concepto_liq);
                cmd.Parameters.AddWithValue("@nro_valor", oVal.nro_valor);
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

    }
}