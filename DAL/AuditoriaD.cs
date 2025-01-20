using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AuditoriaD
    {
        public static void Insert_movimiento(string fecha_movimiento, string usuario, string menu,
            string proceso, string identificacion, string autorizaciones, string observaciones, string detalle)
        {
            Entities.Auditoria oAudita = new Entities.Auditoria();
            int id = 0;
            try
            {
                string SQLAudita = @"INSERT INTO MOVIMIENTOS_SPW
                                    (id_auditoria, fecha_movimiento,
                                    usuario, menu, proceso,
                                    identificacion, autorizaciones,
                                    observaciones, detalle)
                                    VALUES
                                    (@id_auditoria, @fecha_movimiento,
                                    @usuario, @menu, @proceso,
                                    @identificacion, @autorizaciones, @detalle)";
                using (SqlConnection cn = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmdInsert = null;
                    SqlCommand cmd = null;

                    string SQL = @"SELECT isnull(max(id_auditoria),0)  As id
                                FROM MOVIMIENTOS_SPW  (nolock)";
                    cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SQL.ToString();
                    cmd.Connection.Open();
                    id = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
                    //
                    cmdInsert = cn.CreateCommand();
                    cmdInsert.CommandType = CommandType.Text;
                    cmdInsert.CommandText = SQLAudita;
                    cmdInsert.Parameters.AddWithValue("@id_auditoria", id);
                    cmdInsert.Parameters.AddWithValue("@fecha_movimiento", fecha_movimiento);
                    cmdInsert.Parameters.AddWithValue("@usuario", usuario);
                    cmdInsert.Parameters.AddWithValue("@proceso", proceso);
                    cmdInsert.Parameters.AddWithValue("@identificacion", identificacion);
                    cmdInsert.Parameters.AddWithValue("@autorizaciones", autorizaciones);
                    cmdInsert.Parameters.AddWithValue("@observaciones", observaciones);
                    cmdInsert.Parameters.AddWithValue("@detalle", detalle);
                    cmdInsert.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Insert_movimiento(Entities.Auditoria oAudita)
        {
            int id = 0;
            DateTimeFormatInfo culturaFecArgentina = new CultureInfo("es-AR", false).DateTimeFormat;
            DateTime fecha;
            using (SqlConnection cn = DALBase.GetConnection("SIIMVA"))
            {
                try
                {
                    string SQL = @"SELECT isnull(max(id_auditoria),0)  As id
                                   FROM MOVIMIENTOS_SPW  (nolock)";
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SQL.ToString();
                    cmd.Connection.Open();
                    id = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
                    string SQLAudita = @"INSERT INTO MOVIMIENTOS_SPW
                                    (id_auditoria, fecha_movimiento,
                                    usuario, menu, proceso,
                                    identificacion, autorizaciones,
                                    observaciones, detalle)
                                    VALUES
                                    (@id_auditoria, @fecha_movimiento,
                                    @usuario, @menu, @proceso,
                                    @identificacion, @autorizaciones, @observaciones, @detalle)";
                    fecha = Convert.ToDateTime(DateTime.Now, culturaFecArgentina);
                    SqlCommand cmdInsert = cn.CreateCommand();
                    cmdInsert.CommandType = CommandType.Text;
                    cmdInsert.CommandText = SQLAudita;
                    cmdInsert.Parameters.AddWithValue("@id_auditoria", id);
                    cmdInsert.Parameters.AddWithValue("@fecha_movimiento", Convert.ToDateTime(fecha, culturaFecArgentina));
                    cmdInsert.Parameters.AddWithValue("@usuario", oAudita.usuario);
                    cmdInsert.Parameters.AddWithValue("@menu", oAudita.menu);
                    cmdInsert.Parameters.AddWithValue("@proceso", oAudita.proceso);
                    cmdInsert.Parameters.AddWithValue("@identificacion", oAudita.identificacion);
                    cmdInsert.Parameters.AddWithValue("@autorizaciones", oAudita.autorizaciones);
                    cmdInsert.Parameters.AddWithValue("@observaciones", oAudita.observaciones);
                    cmdInsert.Parameters.AddWithValue("@detalle", oAudita.detalle);
                    cmdInsert.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static void Insert_movimiento(Entities.Auditoria oAudita, SqlConnection cn, SqlTransaction trx)
        {
            int id = 0;

            SqlCommand cmdInsert = null;
            SqlCommand cmd = null;
            DateTimeFormatInfo culturaFecArgentina = new CultureInfo("es-AR", false).DateTimeFormat;
            try
            {

                string SQL = @"SELECT isnull(max(id_auditoria),0)  As id
                               FROM MOVIMIENTOS_SPW  (nolock)";
                cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SQL.ToString();
                cmd.Transaction = trx;
                id = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
                string SQLAudita = @"
                                    INSERT INTO MOVIMIENTOS_SPW
                                    (id_auditoria, fecha_movimiento,
                                    usuario, menu, proceso,
                                    identificacion, autorizaciones,
                                    observaciones, detalle)
                                    VALUES
                                    (@id_auditoria, @fecha_movimiento,
                                    @usuario, @menu, @proceso,
                                    @identificacion, @autorizaciones, @observaciones, @detalle)";
                cmdInsert = cn.CreateCommand();
                cmdInsert.CommandType = CommandType.Text;
                cmdInsert.CommandText = SQLAudita;
                cmdInsert.Transaction = trx;
                cmdInsert.Parameters.AddWithValue("@id_auditoria", id);
                cmdInsert.Parameters.AddWithValue("@fecha_movimiento", Convert.ToDateTime(oAudita.fecha_movimiento, culturaFecArgentina));
                cmdInsert.Parameters.AddWithValue("@usuario", oAudita.usuario);
                cmdInsert.Parameters.AddWithValue("@menu", oAudita.menu);
                cmdInsert.Parameters.AddWithValue("@proceso", oAudita.proceso);
                cmdInsert.Parameters.AddWithValue("@identificacion", oAudita.identificacion);
                cmdInsert.Parameters.AddWithValue("@autorizaciones", oAudita.autorizaciones);
                cmdInsert.Parameters.AddWithValue("@observaciones", oAudita.observaciones);
                cmdInsert.Parameters.AddWithValue("@detalle", oAudita.detalle);
                cmdInsert.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd = null;
                cmdInsert = null;
            }
        }


    }
}
