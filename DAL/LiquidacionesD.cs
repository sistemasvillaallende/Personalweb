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
    public class LiquidacionesD
    {
        public static Entities.Liquidacion getByPk(int anio, int cod_tipo_liq, int nro_liquidacion, SqlConnection cn)
        {

            Entities.Liquidacion oLiq = null;
            //List<Entities.Liquidacion> lst = new List<Entities.Liquidacion>();
            SqlCommand cmd;
            SqlDataReader dr;
            StringBuilder strSQL = new StringBuilder();
            //
            strSQL.AppendLine("select a.anio, b.des_tipo_liq, a.cod_tipo_liq, a.nro_liquidacion, a.periodo, a.des_liquidacion,");
            strSQL.AppendLine("convert(varchar(10), a.fecha_alta_registro, 103) as fecha_alta , a.aguinaldo,");
            strSQL.AppendLine("convert(varchar(10), a.fecha_liquidacion, 103) as fecha_liquidacion,");
            strSQL.AppendLine("convert(varchar(10), a.fecha_pago, 103) as fecha_pago, a.per_ult_dep, convert(varchar(10),a.fecha_ult_dep,103) as fecha_ult_dep,");
            strSQL.AppendLine("c.cod_semestre, c.descripcion as semestre, a.usuario,a.operacion, a.cod_banco_ult_dep,");
            strSQL.AppendLine("convert(varchar(10), a.fecha_modificacion, 103) as fecha_modificacion, a.publica, a.cerrada, a.fecha_cierre_liq,");
            strSQL.AppendLine("a.usuario_cierre, a.prueba ");
            strSQL.AppendLine("from LIQUIDACIONES a");
            strSQL.AppendLine("join TIPOS_LIQUIDACION b on");
            strSQL.AppendLine("a.cod_tipo_liq = b.cod_tipo_liq");
            strSQL.AppendLine("join semestres c on");
            strSQL.AppendLine("a.cod_semestre = c.cod_semestre");
            strSQL.AppendLine("where a.anio=@anio and a.cod_tipo_liq=@cod_tipo_liq and nro_liquidacion=@nro_liquidacion");
            strSQL.AppendLine("order by a.anio desc, a.cod_tipo_liq, a.nro_liquidacion desc");
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@anio", anio);
            cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
            cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
            try
            {
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                //cmd.Connection.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    oLiq = new Entities.Liquidacion();
                    int des_tipo_liq = dr.GetOrdinal("des_tipo_liq");
                    int periodo = dr.GetOrdinal("periodo");
                    int des_liquidacion = dr.GetOrdinal("des_liquidacion");
                    int fecha_alta = dr.GetOrdinal("fecha_alta");
                    int fecha_liquidacion = dr.GetOrdinal("fecha_liquidacion");
                    int aguinaldo = dr.GetOrdinal("aguinaldo");
                    int fecha_pago = dr.GetOrdinal("fecha_pago");
                    int per_ult_dep = dr.GetOrdinal("per_ult_dep");
                    int fecha_ult_dep = dr.GetOrdinal("fecha_ult_dep");
                    int cod_semestre = dr.GetOrdinal("cod_semestre");
                    int semestre = dr.GetOrdinal("semestre");
                    int usuario = dr.GetOrdinal("usuario"); ;
                    int operacion = dr.GetOrdinal("operacion");
                    int fecha_modificacion = dr.GetOrdinal("fecha_modificacion");
                    int cod_banco_ult_dep = dr.GetOrdinal("cod_banco_ult_dep");
                    int publica = dr.GetOrdinal("publica");
                    int cerrada = dr.GetOrdinal("cerrada");
                    int fecha_cierre_liq = dr.GetOrdinal("fecha_cierre_liq");
                    int usuario_cierre = dr.GetOrdinal("usuario_cierre");
                    int prueba = dr.GetOrdinal("prueba");

                    oLiq.anio = anio;
                    oLiq.cod_tipo_liq = cod_tipo_liq;
                    oLiq.nro_liquidacion = nro_liquidacion;


                    if (!dr.IsDBNull(des_tipo_liq)) oLiq.des_tipo_liq = dr.GetString(des_tipo_liq);
                    if (!dr.IsDBNull(des_liquidacion)) oLiq.des_liquidacion = dr.GetString(des_liquidacion);
                    if (!dr.IsDBNull(periodo)) oLiq.periodo = dr.GetString(periodo);
                    if (!dr.IsDBNull(fecha_alta)) oLiq.fecha_alta = dr.GetString(fecha_alta);
                    if (!dr.IsDBNull(fecha_liquidacion)) oLiq.fecha_liquidacion = dr.GetString(fecha_liquidacion);
                    if (!dr.IsDBNull(aguinaldo)) oLiq.aguinaldo = dr.GetBoolean(aguinaldo);
                    if (!dr.IsDBNull(fecha_pago)) oLiq.fecha_pago = dr.GetString(fecha_pago);
                    if (!dr.IsDBNull(per_ult_dep)) oLiq.per_ult_dep = dr.GetString(per_ult_dep);
                    if (!dr.IsDBNull(cod_semestre)) oLiq.cod_semestre = dr.GetInt32(cod_semestre);
                    if (!dr.IsDBNull(semestre)) oLiq.semestre = dr.GetString(semestre);
                    if (!dr.IsDBNull(usuario)) oLiq.usuario = dr.GetString(usuario);
                    if (!dr.IsDBNull(operacion)) oLiq.operacion = dr.GetString(operacion);
                    if (!dr.IsDBNull(fecha_modificacion))
                        oLiq.fecha_modificacion = dr.GetString(fecha_modificacion);
                    if (!dr.IsDBNull(cod_banco_ult_dep))
                        oLiq.cod_banco_ult_dep = dr.GetInt32(cod_banco_ult_dep);
                    if (!dr.IsDBNull(fecha_ult_dep)) oLiq.fecha_ult_dep = dr.GetString(fecha_ult_dep);
                    oLiq.publica = dr.GetBoolean(publica);
                    //
                    if (!dr.IsDBNull(cerrada)) oLiq.cerrada = dr.GetBoolean(cerrada);
                    if (!dr.IsDBNull(fecha_cierre_liq)) oLiq.fecha_cierre_liq = Convert.ToString(dr.GetDateTime(fecha_cierre_liq));
                    if (!dr.IsDBNull(usuario_cierre)) oLiq.usuario_cierre = dr.GetString(usuario_cierre);
                    if (!dr.IsDBNull(prueba)) oLiq.prueba = dr.GetInt16(prueba);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            finally
            { cn.Close(); }
            return oLiq;
        }

        public static List<Liquidacion> getLstLiq(SqlConnection cn)
        {
            Entities.Liquidacion oLiq = null;
            List<Entities.Liquidacion> lst = new List<Entities.Liquidacion>();

            SqlCommand cmd;
            SqlDataReader dr;
            StringBuilder strSQL = new StringBuilder();

            strSQL.AppendLine("select a.anio, b.des_tipo_liq, a.cod_tipo_liq, a.nro_liquidacion, a.periodo, a.des_liquidacion,");
            strSQL.AppendLine("convert(varchar(10), a.fecha_alta_registro, 103) as fecha_alta , a.aguinaldo,");
            strSQL.AppendLine("convert(varchar(10), a.fecha_liquidacion, 103) as fecha_liquidacion,");
            strSQL.AppendLine("convert(varchar(10), a.fecha_pago, 103) as fecha_pago, a.per_ult_dep, a.fecha_ult_dep,");
            strSQL.AppendLine("c.cod_semestre, c.descripcion as semestre, a.usuario,a.operacion, a.cod_banco_ult_dep,");
            strSQL.AppendLine("convert(varchar(10), a.fecha_modificacion, 103) as fecha_modificacion, a.publica, a.cerrada, a.fecha_cierre_liq, a.usuario_cierre,");
            strSQL.AppendLine("a.prueba");
            strSQL.AppendLine("from LIQUIDACIONES a");
            strSQL.AppendLine("join TIPOS_LIQUIDACION b on");
            strSQL.AppendLine("a.cod_tipo_liq = b.cod_tipo_liq");
            strSQL.AppendLine("left join semestres c on");
            strSQL.AppendLine("a.cod_semestre = c.cod_semestre");
            strSQL.AppendLine("order by a.anio desc, a.cod_tipo_liq, a.nro_liquidacion desc");

            cmd = new SqlCommand();

            try
            {
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                //cmd.Connection.Open();

                dr = cmd.ExecuteReader();


                if (dr.HasRows)
                {
                    int anio = dr.GetOrdinal("anio");
                    int cod_tipo_liq = dr.GetOrdinal("cod_tipo_liq");
                    int des_tipo_liq = dr.GetOrdinal("des_tipo_liq");
                    int nro_liquidacion = dr.GetOrdinal("nro_liquidacion");
                    int periodo = dr.GetOrdinal("periodo");
                    int des_liquidacion = dr.GetOrdinal("des_liquidacion");
                    int fecha_alta = dr.GetOrdinal("fecha_alta");
                    int fecha_liquidacion = dr.GetOrdinal("fecha_liquidacion");
                    int aguinaldo = dr.GetOrdinal("aguinaldo");
                    int fecha_pago = dr.GetOrdinal("fecha_pago");
                    int per_ult_dep = dr.GetOrdinal("per_ult_dep");
                    int fecha_ult_dep = dr.GetOrdinal("fecha_ult_dep");
                    int cod_semestre = dr.GetOrdinal("cod_semestre");
                    int semestre = dr.GetOrdinal("semestre");
                    int usuario = dr.GetOrdinal("usuario"); ;
                    int operacion = dr.GetOrdinal("operacion");
                    int fecha_modificacion = dr.GetOrdinal("fecha_modificacion");
                    int cod_banco_ult_dep = dr.GetOrdinal("cod_banco_ult_dep");
                    int publica = dr.GetOrdinal("publica");
                    int cerrada = dr.GetOrdinal("cerrada");
                    int fecha_cierre_liq = dr.GetOrdinal("fecha_cierre_liq");
                    int usuario_cierre = dr.GetOrdinal("usuario_cierre");

                    while (dr.Read())
                    {
                        oLiq = new Entities.Liquidacion();

                        if (!dr.IsDBNull(anio)) oLiq.anio = dr.GetInt32(anio);
                        if (!dr.IsDBNull(cod_tipo_liq)) oLiq.cod_tipo_liq = dr.GetInt32(cod_tipo_liq);
                        if (!dr.IsDBNull(nro_liquidacion)) oLiq.nro_liquidacion = dr.GetInt32(nro_liquidacion);
                        if (!dr.IsDBNull(des_tipo_liq)) oLiq.des_tipo_liq = dr.GetString(des_tipo_liq);
                        if (!dr.IsDBNull(des_liquidacion)) oLiq.des_liquidacion = dr.GetString(des_liquidacion);
                        if (!dr.IsDBNull(periodo)) oLiq.periodo = dr.GetString(periodo);
                        if (!dr.IsDBNull(fecha_alta)) oLiq.fecha_alta = dr.GetString(fecha_alta);
                        if (!dr.IsDBNull(fecha_liquidacion)) oLiq.fecha_liquidacion = dr.GetString(fecha_liquidacion);
                        if (!dr.IsDBNull(aguinaldo)) oLiq.aguinaldo = dr.GetBoolean(aguinaldo);
                        if (!dr.IsDBNull(fecha_pago)) oLiq.fecha_pago = dr.GetString(fecha_pago);
                        if (!dr.IsDBNull(per_ult_dep)) oLiq.per_ult_dep = dr.GetString(per_ult_dep);
                        if (!dr.IsDBNull(cod_semestre)) oLiq.cod_semestre = dr.GetInt32(cod_semestre);
                        if (!dr.IsDBNull(semestre)) oLiq.semestre = dr.GetString(semestre);
                        if (!dr.IsDBNull(usuario)) oLiq.usuario = dr.GetString(usuario);
                        if (!dr.IsDBNull(operacion))
                            oLiq.operacion = dr.GetString(operacion);
                        if (!dr.IsDBNull(fecha_modificacion))
                            oLiq.fecha_modificacion = dr.GetString(fecha_modificacion);
                        if (!dr.IsDBNull(cod_banco_ult_dep))
                            oLiq.cod_banco_ult_dep = dr.GetInt32(cod_banco_ult_dep);
                        oLiq.publica = dr.GetBoolean(publica);
                        if (!dr.IsDBNull(cerrada)) oLiq.cerrada = dr.GetBoolean(cerrada);
                        if (!dr.IsDBNull(fecha_cierre_liq)) oLiq.fecha_cierre_liq = Convert.ToString(dr.GetDateTime(fecha_cierre_liq));
                        if (!dr.IsDBNull(usuario_cierre)) oLiq.usuario_cierre = dr.GetString(usuario_cierre);
                        lst.Add(oLiq);
                    }
                }
                dr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            finally
            { cmd = null; }
            return lst;
        }

        public static void insert(Liquidacion oLiq, SqlConnection cn, SqlTransaction trx)
        {
            SqlCommand cmd;
            SqlCommand cmd1;
            StringBuilder strSQL = new StringBuilder();
            if (oLiq.nro_liquidacion == 0)
            {
                StringBuilder SQL = new StringBuilder();
                SQL.AppendLine("SELECT isnull(max(nro_liquidacion),0) FROM Liquidaciones");
                SQL.AppendLine("WHERE anio=@anio");
                SQL.AppendLine("AND cod_tipo_liq=@cod_tipo_liq");
                //SQL.AppendLine("AND nro_liquidacion=@nro_liquidacion");
                cmd1 = new SqlCommand();
                cmd1.Parameters.AddWithValue("@anio", oLiq.anio);
                cmd1.Parameters.AddWithValue("@cod_tipo_liq", oLiq.cod_tipo_liq);
                //cmd1.Parameters.AddWithValue("@nro_liquidacion", 0);
                cmd1.Connection = cn;
                cmd1.CommandType = CommandType.Text;
                cmd1.Transaction = trx;
                cmd1.CommandText = SQL.ToString();
                oLiq.nro_liquidacion = Convert.ToInt32(cmd1.ExecuteScalar()) + 1;
            }
            strSQL.AppendLine("INSERT INTO LIQUIDACIONES");
            strSQL.AppendLine("(anio,");
            strSQL.AppendLine("cod_tipo_liq,");
            strSQL.AppendLine("nro_liquidacion,");
            strSQL.AppendLine("fecha_alta_registro,");
            strSQL.AppendLine("aguinaldo,");
            strSQL.AppendLine("des_liquidacion,");
            strSQL.AppendLine("periodo,");
            if (oLiq.fecha_liquidacion.Length != 0)
                strSQL.AppendLine("fecha_liquidacion,");
            if (oLiq.fecha_pago.Length != 0)
                strSQL.AppendLine("fecha_pago,");
            if (oLiq.per_ult_dep.Length != 0)
                strSQL.AppendLine("per_ult_dep,");
            if (oLiq.cod_banco_ult_dep != 0)
                strSQL.AppendLine("cod_banco_ult_dep,");
            if (oLiq.fecha_ult_dep.Length != 0)
                strSQL.AppendLine("fecha_ult_dep,");
            strSQL.AppendLine("cod_semestre,");
            strSQL.AppendLine("usuario,");
            strSQL.AppendLine("operacion,");
            strSQL.AppendLine("publica,");
            strSQL.AppendLine("prueba)");

            //if (oLiq.fecha_modificacion.Length != 0)
            //  strSQL.AppendLine("fecha_modificacion)");

            strSQL.AppendLine("VALUES");
            strSQL.AppendLine("(@anio,");
            strSQL.AppendLine("@cod_tipo_liq,");
            strSQL.AppendLine("@nro_liquidacion,");

            strSQL.AppendLine("@fecha_alta_registro,");
            strSQL.AppendLine("@aguinaldo,");
            strSQL.AppendLine("@des_liquidacion,");
            strSQL.AppendLine("@periodo,");
            if (oLiq.fecha_liquidacion.Length != 0)
                strSQL.AppendLine("@fecha_liquidacion,");
            if (oLiq.fecha_pago.Length != 0)
                strSQL.AppendLine("@fecha_pago,");
            if (oLiq.per_ult_dep.Length != 0)
                strSQL.AppendLine("@per_ult_dep,");
            if (oLiq.cod_banco_ult_dep != 0)
                strSQL.AppendLine("@cod_banco_ult_dep,");
            if (oLiq.fecha_ult_dep.Length != 0)
                strSQL.AppendLine("@fecha_ult_dep,");
            strSQL.AppendLine("@cod_semestre,");
            strSQL.AppendLine("@usuario,");
            strSQL.AppendLine("@operacion,");
            //strSQL.AppendLine("@fecha_modificacion,");
            strSQL.AppendLine("@publica,");
            strSQL.AppendLine("@prueba)");
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@anio", oLiq.anio);
            cmd.Parameters.AddWithValue("@cod_tipo_liq", oLiq.cod_tipo_liq);
            cmd.Parameters.AddWithValue("@nro_liquidacion", oLiq.nro_liquidacion);
            cmd.Parameters.AddWithValue("@fecha_alta_registro", DateTime.Today.ToString("dd/MM/yyyy"));
            cmd.Parameters.AddWithValue("@aguinaldo", oLiq.aguinaldo);
            cmd.Parameters.AddWithValue("@des_liquidacion", oLiq.des_liquidacion);
            cmd.Parameters.AddWithValue("@periodo", oLiq.periodo);
            if (oLiq.fecha_liquidacion.Length != 0)
                cmd.Parameters.AddWithValue("@fecha_liquidacion", oLiq.fecha_liquidacion);
            if (oLiq.fecha_pago.Length != 0)
                cmd.Parameters.AddWithValue("@fecha_pago", oLiq.fecha_pago);
            if (oLiq.per_ult_dep.Length != 0)
                cmd.Parameters.AddWithValue("@per_ult_dep", oLiq.per_ult_dep);
            if (oLiq.cod_banco_ult_dep != 0)
                cmd.Parameters.AddWithValue("@cod_banco_ult_dep", oLiq.cod_banco_ult_dep);
            if (oLiq.fecha_ult_dep.Length != 0)
                cmd.Parameters.AddWithValue("@fecha_ult_dep", oLiq.fecha_ult_dep);
            cmd.Parameters.AddWithValue("@cod_semestre", oLiq.cod_semestre);
            cmd.Parameters.AddWithValue("@usuario", oLiq.usuario);
            cmd.Parameters.AddWithValue("@operacion", oLiq.operacion);
            //cmd.Parameters.AddWithValue("@fecha_moficiacion", oLiq.fecha_modificacion);
            cmd.Parameters.AddWithValue("@publica", oLiq.publica);
            cmd.Parameters.AddWithValue("@prueba", oLiq.prueba);
            try
            {
                cmd.Connection = cn;
                cmd.Transaction = trx;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                //cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            finally
            {
                cmd = null;
                cmd1 = null;
            }
        }

        public static void update(Liquidacion oLiq, SqlConnection cn, SqlTransaction trx)
        {
            SqlCommand cmd;
            StringBuilder strSQL = new StringBuilder();
            strSQL.AppendLine("set dateformat dmy; update LIQUIDACIONES SET");
            strSQL.AppendLine("aguinaldo=@aguinaldo,");
            strSQL.AppendLine("des_liquidacion=@des_liquidacion,");
            strSQL.AppendLine("periodo=@periodo,");
            strSQL.AppendLine("fecha_liquidacion=@fecha_liquidacion,");
            if (oLiq.fecha_pago.Length != 0)
                strSQL.AppendLine("fecha_pago=@fecha_pago,");
            else
                strSQL.AppendLine("fecha_pago=null,");
            if (oLiq.per_ult_dep.Length != 0)
                strSQL.AppendLine("per_ult_dep=@per_ult_dep,");
            else
                strSQL.AppendLine("per_ult_dep=null,");
            if (oLiq.cod_banco_ult_dep != 0)
                strSQL.AppendLine("cod_banco_ult_dep=@cod_banco_ult_dep,");
            else
                strSQL.AppendLine("cod_banco_ult_dep=null,");
            if (oLiq.fecha_ult_dep.Length != 0)
                strSQL.AppendLine("fecha_ult_dep=@fecha_ult_dep,");
            else
                strSQL.AppendLine("fecha_ult_dep=null,");
            
            strSQL.AppendLine("cod_semestre=@cod_semestre,");
            strSQL.AppendLine("usuario=@usuario,");
            strSQL.AppendLine("operacion=@operacion,");
            strSQL.AppendLine("fecha_modificacion=@fecha_modificacion,");
            strSQL.AppendLine("publica=@publica,");
            strSQL.AppendLine("prueba=@prueba");
            strSQL.AppendLine("WHERE anio=@anio and cod_tipo_liq=@cod_tipo_liq and nro_liquidacion=@nro_liquidacion");

            cmd = new SqlCommand();

            cmd.Parameters.AddWithValue("@aguinaldo", oLiq.aguinaldo);
            cmd.Parameters.AddWithValue("@des_liquidacion", oLiq.des_liquidacion);
            cmd.Parameters.AddWithValue("@periodo", oLiq.periodo);
            if (oLiq.fecha_liquidacion.Length != 0)
                cmd.Parameters.AddWithValue("@fecha_liquidacion", oLiq.fecha_liquidacion);
            if (oLiq.fecha_pago.Length != 0)
                cmd.Parameters.AddWithValue("@fecha_pago", oLiq.fecha_pago);
            if (oLiq.per_ult_dep.Length != 0)
                cmd.Parameters.AddWithValue("@per_ult_dep", oLiq.per_ult_dep);
            if (oLiq.cod_banco_ult_dep != 0)
                cmd.Parameters.AddWithValue("@cod_banco_ult_dep", oLiq.cod_banco_ult_dep);
            if (oLiq.fecha_ult_dep.Length != 0)
                cmd.Parameters.AddWithValue("@fecha_ult_dep", oLiq.fecha_ult_dep);
            cmd.Parameters.AddWithValue("@cod_semestre", oLiq.cod_semestre);
            cmd.Parameters.AddWithValue("@usuario", oLiq.usuario);
            cmd.Parameters.AddWithValue("@operacion", oLiq.operacion);
            cmd.Parameters.AddWithValue("@fecha_modificacion", DateTime.Today.ToString("dd/MM/yyyy"));
            cmd.Parameters.AddWithValue("@anio", oLiq.anio);
            cmd.Parameters.AddWithValue("@cod_tipo_liq", oLiq.cod_tipo_liq);
            cmd.Parameters.AddWithValue("@nro_liquidacion", oLiq.nro_liquidacion);
            cmd.Parameters.AddWithValue("@publica", oLiq.publica);
            cmd.Parameters.AddWithValue("@prueba", oLiq.prueba);
            try
            {
                cmd.Connection = cn;
                cmd.Transaction = trx;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                //cmd.Connection.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            finally
            {
                cmd = null;
            }
        }

        public static void Liquidar(int anio, int cod_tipo_liq, int nro_liquidacion, Entities.Liquidacion oLiq, SqlConnection cn, SqlTransaction trx)
        {
            bool control = false;
            try
            {
                control = ExisteDetalleLiquidacion(anio, cod_tipo_liq, nro_liquidacion, cn, trx);
                if (control == true)
                {
                    DeleteLiquidacion(anio, cod_tipo_liq, nro_liquidacion, oLiq, cn, trx);
                    //Si se Elimina la liquidacion es pq estoy reliquidando
                    //Entonces
                    //Primero audito
                    Entities.Auditoria oAudita = new Auditoria();
                    oAudita.id_auditoria = 0;
                    oAudita.fecha_movimiento = DateTime.Now.ToString();
                    oAudita.menu = "Liquidaciones";
                    oAudita.proceso = "Liquidaciones de Sueldos";
                    oAudita.identificacion = anio.ToString() + cod_tipo_liq.ToString() + nro_liquidacion.ToString();
                    oAudita.autorizaciones = "";
                    oAudita.observaciones = "";
                    oAudita.detalle = "Se Elimino la Liquidación correspondiente al Año: " +
                        anio.ToString() + " Tipo Liq: " + cod_tipo_liq.ToString() + " Nro Liq: " + nro_liquidacion.ToString() +
                        " y luego se Reliquido nuevamente!";
                    oAudita.usuario = oLiq.usuario;
                    DAL.AuditoriaD.Insert_movimiento(oAudita);
                    oLiq.operacion = "Reliquidacion Sueldo";
                    ActualizarLiquidacion(oLiq, cn, trx);
                    //Y dsp sigo el proceso logico
                }
                LiquidacionD.Calcula_SueldoxdiasTrabajados(anio, cod_tipo_liq, nro_liquidacion, oLiq.fecha_liquidacion, cn, trx);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        public static void Aguinaldo(int anio, int cod_tipo_liq, int nro_liquidacion, int cod_semestre, Entities.Liquidacion oLiq, SqlConnection cn, SqlTransaction trx)
        {
            bool control = false;
            try
            {
                control = ExisteDetalleLiquidacion(anio, cod_tipo_liq, nro_liquidacion, cn, trx);
                if (control == true)
                {
                    DeleteLiquidacion(anio, cod_tipo_liq, nro_liquidacion, oLiq, cn, trx);
                    //Si se eLimina la liquidacion es pq estoy reliquidando
                    Entities.Auditoria oAudita = new Auditoria();
                    oAudita.id_auditoria = 0;
                    oAudita.fecha_movimiento = DateTime.Now.ToString();
                    oAudita.menu = "Liquidaciones";
                    oAudita.proceso = "Liquidaciones de Aguinaldo";
                    oAudita.identificacion = anio.ToString() + cod_tipo_liq.ToString() + nro_liquidacion.ToString();
                    oAudita.autorizaciones = "";
                    oAudita.observaciones = "";
                    oAudita.detalle = "Se Elimino el Aguinaldo correspondiente al Año: " +
                        anio.ToString() + " Tipo Liq: " + cod_tipo_liq.ToString() + " Nro Liq: " + nro_liquidacion.ToString() +
                        " y luego se Reliquido nuevamente!";
                    oAudita.usuario = oLiq.usuario;
                    DAL.AuditoriaD.Insert_movimiento(oAudita);
                    oLiq.operacion = "Reliquidacion Aguinaldo";
                    ActualizarLiquidacion(oLiq, cn, trx);

                }
                LiquidacionD.Calcula_Aguinaldo(anio, cod_tipo_liq, nro_liquidacion, cod_semestre, oLiq.fecha_liquidacion, cn, trx);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { }
        }

        protected static void DeleteLiquidacion(int anio, int cod_tipo_liq, int nro_liquidacion, Entities.Liquidacion oLiq, SqlConnection cn, SqlTransaction trx)
        {

            SqlCommand cmd, cmd1;
            StringBuilder strSQL = new StringBuilder();
            StringBuilder strSQL1 = new StringBuilder();

            strSQL.AppendLine("DELETE FROM LIQ_X_EMPLEADO WHERE");
            strSQL.AppendLine("anio=@anio");
            strSQL.AppendLine("AND cod_tipo_liq=@cod_tipo_liq");
            strSQL.AppendLine("AND nro_liquidacion=@nro_liquidacion");
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@anio", anio);
            cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
            cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);

            strSQL1.AppendLine("DELETE FROM DET_LIQ_X_EMPLEADO WHERE");
            strSQL1.AppendLine("anio=@anio");
            strSQL1.AppendLine("AND cod_tipo_liq=@cod_tipo_liq");
            strSQL1.AppendLine("AND nro_liquidacion=@nro_liquidacion");
            cmd1 = new SqlCommand();
            cmd1.Parameters.AddWithValue("@anio", anio);
            cmd1.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
            cmd1.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
            try
            {
                cmd.Connection = cn;
                cmd.Transaction = trx;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd1.Connection = cn;
                cmd1.Transaction = trx;
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = strSQL1.ToString();
                cmd.ExecuteNonQuery();
                cmd1.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            { }
        }

        protected static void EliminaLiquidacion(int anio, int cod_tipo_liq, int nro_liquidacion, Entities.Liquidacion oLiq, SqlConnection cn, SqlTransaction trx)
        {

            SqlCommand cmd, cmd1;
            StringBuilder strSQL = new StringBuilder();
            StringBuilder strSQL1 = new StringBuilder();
            int retorno = 0;

            strSQL.AppendLine("DELETE FROM LIQ_X_EMPLEADO WHERE");
            strSQL.AppendLine("anio=@anio");
            strSQL.AppendLine("AND cod_tipo_liq=@cod_tipo_liq");
            strSQL.AppendLine("AND nro_liquidacion=@nro_liquidacion");
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@anio", anio);
            cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
            cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);

            strSQL1.AppendLine("DELETE FROM DET_LIQ_X_EMPLEADO WHERE");
            strSQL1.AppendLine("anio=@anio");
            strSQL1.AppendLine("AND cod_tipo_liq=@cod_tipo_liq");
            strSQL1.AppendLine("AND nro_liquidacion=@nro_liquidacion");
            cmd1 = new SqlCommand();
            cmd1.Parameters.AddWithValue("@anio", anio);
            cmd1.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
            cmd1.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
            try
            {
                cmd.Connection = cn;
                cmd.Transaction = trx;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                //
                cmd1.Connection = cn;
                cmd1.Transaction = trx;
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = strSQL1.ToString();
                cmd.ExecuteNonQuery();
                retorno = cmd1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                cmd = null;
                cmd1 = null;
                throw ex;
            }
            finally
            { }
        }

        public static bool ExisteDetalleLiquidacion(int anio, int cod_tipo_liq, int nro_liquidacion, SqlConnection cn, SqlTransaction trx)
        {
            SqlCommand cmd;
            SqlDataReader dr;
            bool si = false;
            string strSQL = @"select isnull(count(*),0) 
                              from Det_liq_x_empleado with (nolock)
                              where
                              anio=@anio and
                              cod_tipo_liq=@cod_tipo_liq and
                              nro_liquidacion=@nro_liquidacion";
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@anio", anio);
            cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
            cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
            try
            {
                cmd.Connection = cn;
                cmd.Transaction = trx;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    si = true;
                }
                dr.Close();
                return si;
            }
            catch (Exception ex)
            {
                cmd = null;
                throw ex;
            }
            finally
            { }
        }

        public static void Publicar_liquidacion(int anio, int cod_tipo_liq, int nro_liquidacion, string usuario, string operacion, bool publicar, SqlConnection cn)
        {
            SqlCommand cmd;
            StringBuilder strSQL = new StringBuilder();
            DateTimeFormatInfo culturaFecArgentina = new CultureInfo("es-AR", false).DateTimeFormat;
            strSQL.AppendLine("set dateformat dmy; update LIQUIDACIONES SET");
            strSQL.AppendLine("publica=@publica, usuario=@usuario, operacion=@operacion, fecha_modificacion=@fecha_modificacion");
            strSQL.AppendLine("WHERE anio=@anio and cod_tipo_liq=@cod_tipo_liq and nro_liquidacion=@nro_liquidacion");
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@usuario", usuario);
            cmd.Parameters.AddWithValue("@operacion", operacion);
            cmd.Parameters.AddWithValue("@fecha_modificacion", Convert.ToDateTime(DateTime.Now.Date, culturaFecArgentina));
            cmd.Parameters.AddWithValue("@anio", anio);
            cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
            cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
            cmd.Parameters.AddWithValue("@publica", publicar);
            try
            {
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            finally
            {
                cmd = null;
            }
        }

        public static void Cerrar_liquidacion(int anio, int cod_tipo_liq, int nro_liquidacion, string usuario_cierre, string operacion,
            bool cerrada, string fecha_cierre,
            SqlConnection cn)
        {
            SqlCommand cmd;
            StringBuilder strSQL = new StringBuilder();
            DateTimeFormatInfo culturaFecArgentina = new CultureInfo("es-AR", false).DateTimeFormat;
            strSQL.AppendLine("set dateformat dmy; update LIQUIDACIONES SET");
            strSQL.AppendLine("fecha_cierre_liq=@fecha_cierre, usuario_cierre=@usuario_cierre, cerrada=@cerrada");
            strSQL.AppendLine("WHERE anio=@anio and cod_tipo_liq=@cod_tipo_liq and nro_liquidacion=@nro_liquidacion");
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@usuario_cierre", usuario_cierre);
            cmd.Parameters.AddWithValue("@operacion", operacion);
            cmd.Parameters.AddWithValue("@cerrada", cerrada);
            cmd.Parameters.AddWithValue("@fecha_cierre", Convert.ToDateTime(DateTime.Now, culturaFecArgentina));
            cmd.Parameters.AddWithValue("@anio", anio);
            cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
            cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
            try
            {
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            finally
            {
                cmd = null;
            }
        }

        public static void ActualizarLiquidacion(Liquidacion oLiq, SqlConnection cn, SqlTransaction trx)
        {
            SqlCommand cmd;
            string strSQL = @"Set dateformat dmy; 
                              UPDATE LIQUIDACIONES 
                                SET usuario=@usuario,
                                fecha_modificacion=@fecha_modificacion,
                                operacion=@operacion
                              WHERE anio = @anio and cod_tipo_liq = @cod_tipo_liq and 
                                nro_liquidacion = @nro_liquidacion";
            DateTimeFormatInfo culturaFecArgentina = new CultureInfo("es-AR", false).DateTimeFormat;
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@usuario", oLiq.usuario);
            cmd.Parameters.AddWithValue("@operacion", oLiq.operacion);
            cmd.Parameters.AddWithValue("@fecha_modificacion", Convert.ToDateTime(DateTime.Now, culturaFecArgentina));
            cmd.Parameters.AddWithValue("@anio", oLiq.anio);
            cmd.Parameters.AddWithValue("@cod_tipo_liq", oLiq.cod_tipo_liq);
            cmd.Parameters.AddWithValue("@nro_liquidacion", oLiq.nro_liquidacion);
            try
            {
                cmd.Connection = cn;
                cmd.Transaction = trx;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                //cmd.Connection.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            finally
            {
                cmd = null;
            }
        }

    }
}
