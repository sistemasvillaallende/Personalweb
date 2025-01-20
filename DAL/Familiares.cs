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
    public class Familiares : DALBase
    {
        public int legajo { get; set; }
        public int nro_familiar { get; set; }
        public DateTime? fecha_alta_registro { get; set; }
        public string nombre { get; set; }
        public int cod_tipo_documento { get; set; }
        public string nro_documento { get; set; }
        public DateTime? fecha_nacimiento { get; set; }
        public string parentezco { get; set; }
        public int sexo { get; set; }
        public bool salario_familiar { get; set; }
        public bool incapacitado { get; set; }
        public int id_parentezco { get; set; }
        public int opcion { get; set; }

        public Familiares()
        {
            legajo = 0;
            nro_familiar = 0;
            fecha_alta_registro = DateTime.Now;
            nombre = string.Empty;
            cod_tipo_documento = 0;
            nro_documento = string.Empty;
            fecha_nacimiento = null;
            parentezco = string.Empty;
            sexo = 0;
            salario_familiar = false;
            incapacitado = false;
            id_parentezco = 0;
            opcion = 0; //0: normal, 1: alta, 2:edicion, 3:elimina
        }

        private static List<Familiares> mapeo(SqlDataReader dr)
        {
            List<Familiares> lst = new List<Familiares>();
            Familiares obj;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    obj = new Familiares();
                    if (!dr.IsDBNull(0)) { obj.legajo = dr.GetInt32(0); }
                    if (!dr.IsDBNull(1)) { obj.nro_familiar = dr.GetInt32(1); }
                    if (!dr.IsDBNull(2)) { obj.fecha_alta_registro = dr.GetDateTime(2); }
                    if (!dr.IsDBNull(3)) { obj.nombre = dr.GetString(3); }
                    if (!dr.IsDBNull(4)) { obj.cod_tipo_documento = dr.GetInt32(4); }
                    if (!dr.IsDBNull(5)) { obj.nro_documento = dr.GetString(5); }
                    if (!dr.IsDBNull(6)) { obj.fecha_nacimiento = dr.GetDateTime(6); }
                    if (!dr.IsDBNull(7)) { obj.parentezco = dr.GetString(7); }
                    if (!dr.IsDBNull(8)) { obj.sexo = dr.GetInt32(8); }
                    if (!dr.IsDBNull(9)) { obj.salario_familiar = dr.GetBoolean(9); }
                    if (!dr.IsDBNull(10)) { obj.incapacitado = dr.GetBoolean(10); }
                    if (!dr.IsDBNull(11)) { obj.id_parentezco = dr.GetInt32(11); }
                    obj.opcion = 0;
                    lst.Add(obj);
                }
            }
            return lst;
        }

        public static List<Familiares> read()
        {
            try
            {
                List<Familiares> lst = new List<Familiares>();
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM FAMILIARES";
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

        public static List<Familiares> read(int legajo)
        {
            try
            {
                List<Familiares> lst = new List<Familiares>();
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM FAMILIARES WHERE LEGAJO=@LEGAJO";
                    cmd.Parameters.AddWithValue("@LEGAJO", legajo);
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

        public static Familiares getByPk(int legajo, int nro_familiar)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT * FROM FAMILIARES WHERE");
                sql.AppendLine("legajo = @legajo");
                sql.AppendLine("AND nro_familiar = @nro_familiar");
                Familiares obj = null;
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@legajo", legajo);
                    cmd.Parameters.AddWithValue("@nro_familiar", nro_familiar);
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<Familiares> lst = mapeo(dr);
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

        public static void insert(Familiares obj)
        {
            int nro_familiar = 0;
            SqlTransaction trx = null;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO FAMILIARES(");
                sql.AppendLine("legajo");
                sql.AppendLine(", nro_familiar");
                sql.AppendLine(", fecha_alta_registro");
                sql.AppendLine(", nombre");
                sql.AppendLine(", cod_tipo_documento");
                sql.AppendLine(", nro_documento");
                sql.AppendLine(", fecha_nacimiento");
                sql.AppendLine(", parentezco");
                sql.AppendLine(", sexo");
                sql.AppendLine(", salario_familiar");
                sql.AppendLine(", incapacitado");
                sql.AppendLine(", id_parentezco");
                sql.AppendLine(")");
                sql.AppendLine("VALUES");
                sql.AppendLine("(");
                sql.AppendLine("@legajo");
                sql.AppendLine(", @nro_familiar");
                sql.AppendLine(", @fecha_alta_registro");
                sql.AppendLine(", @nombre");
                sql.AppendLine(", @cod_tipo_documento");
                sql.AppendLine(", @nro_documento");
                sql.AppendLine(", @fecha_nacimiento");
                sql.AppendLine(", @parentezco");
                sql.AppendLine(", @sexo");
                sql.AppendLine(", @salario_familiar");
                sql.AppendLine(", @incapacitado");
                sql.AppendLine(", @id_parentezco");
                sql.AppendLine(")");
                //sql.AppendLine("SELECT SCOPE_IDENTITY()");

                StringBuilder sqlMax = new StringBuilder();
                sqlMax.AppendLine("SELECT isnull(max(nro_familiar),0) ");
                sqlMax.AppendLine("FROM FAMILIARES");
                sqlMax.AppendLine("WHERE legajo=@legajo");

                using (SqlConnection con = GetConnection("SIIMVA"))
                {

                    SqlCommand cmd = con.CreateCommand();

                    cmd.Connection.Open();
                    trx = con.BeginTransaction();
                    cmd.Transaction = trx;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sqlMax.ToString();
                    cmd.Parameters.AddWithValue("@legajo", obj.legajo);
                    nro_familiar = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
                    //
                    SqlCommand cmdFam = con.CreateCommand();
                    cmdFam.CommandType = CommandType.Text;
                    cmdFam.CommandText = sql.ToString();
                    cmdFam.Transaction = trx;
                    cmdFam.Parameters.AddWithValue("@legajo", obj.legajo);
                    cmdFam.Parameters.AddWithValue("@nro_familiar", nro_familiar);
                    cmdFam.Parameters.AddWithValue("@fecha_alta_registro", obj.fecha_alta_registro);
                    cmdFam.Parameters.AddWithValue("@nombre", obj.nombre);
                    cmdFam.Parameters.AddWithValue("@cod_tipo_documento", obj.cod_tipo_documento);
                    cmdFam.Parameters.AddWithValue("@nro_documento", obj.nro_documento);
                    cmdFam.Parameters.AddWithValue("@fecha_nacimiento", obj.fecha_nacimiento);
                    cmdFam.Parameters.AddWithValue("@parentezco", obj.parentezco);
                    cmdFam.Parameters.AddWithValue("@sexo", obj.sexo);
                    cmdFam.Parameters.AddWithValue("@salario_familiar", obj.salario_familiar);
                    cmdFam.Parameters.AddWithValue("@incapacitado", obj.incapacitado);
                    cmdFam.Parameters.AddWithValue("@id_parentezco", obj.id_parentezco);
                    cmdFam.ExecuteNonQuery();
                    trx.Commit();
                }
            }
            catch (Exception ex)
            {
                trx.Rollback();
                throw ex;
            }
        }

        public static void update(Familiares obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE  FAMILIARES SET");
                sql.AppendLine("fecha_alta_registro=@fecha_alta_registro");
                sql.AppendLine(", nombre=@nombre");
                sql.AppendLine(", cod_tipo_documento=@cod_tipo_documento");
                sql.AppendLine(", nro_documento=@nro_documento");
                sql.AppendLine(", fecha_nacimiento=@fecha_nacimiento");
                sql.AppendLine(", parentezco=@parentezco");
                sql.AppendLine(", sexo=@sexo");
                sql.AppendLine(", salario_familiar=@salario_familiar");
                sql.AppendLine(", incapacitado=@incapacitado");
                sql.AppendLine(", id_parentezco=@id_parentezco");
                sql.AppendLine("WHERE");
                sql.AppendLine("legajo=@legajo");
                sql.AppendLine("AND nro_familiar=@nro_familiar");
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@legajo", obj.legajo);
                    cmd.Parameters.AddWithValue("@nro_familiar", obj.nro_familiar);
                    cmd.Parameters.AddWithValue("@fecha_alta_registro", obj.fecha_alta_registro);
                    cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("@cod_tipo_documento", obj.cod_tipo_documento);
                    cmd.Parameters.AddWithValue("@nro_documento", obj.nro_documento);
                    cmd.Parameters.AddWithValue("@fecha_nacimiento", obj.fecha_nacimiento);
                    cmd.Parameters.AddWithValue("@parentezco", obj.parentezco);
                    cmd.Parameters.AddWithValue("@sexo", obj.sexo);
                    cmd.Parameters.AddWithValue("@salario_familiar", obj.salario_familiar);
                    cmd.Parameters.AddWithValue("@incapacitado", obj.incapacitado);
                    cmd.Parameters.AddWithValue("@id_parentezco", obj.id_parentezco);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void delete(Familiares obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("DELETE  FAMILIARES ");
                sql.AppendLine("WHERE");
                sql.AppendLine("legajo=@legajo");
                sql.AppendLine("AND nro_familiar=@nro_familiar");
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@legajo", obj.legajo);
                    cmd.Parameters.AddWithValue("@nro_familiar", obj.nro_familiar);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        ///////////
        ///
        public static List<Familiares> GetHijos(int legajo)
        {
            try
            {
                DateTimeFormatInfo culturaFecArgentina = new System.Globalization.CultureInfo("es-AR", false).DateTimeFormat;
                string strSQL = @"SELECT 
                                    a.legajo, a.nro_familiar, a.fecha_nacimiento
                                  FROM FAMILIARES a 
                                  JOIN EMPLEADOS b on
                                     a.legajo = b.legajo AND
                                     b.fecha_baja is null
                                  WHERE
                                     a.legajo=@legajo AND 
                                     a.id_parentezco=1 AND
                                     a.salario_familiar=1 AND
                                     a.incapacitado=0";
                List<Familiares> lst = new List<Familiares>();
                Familiares obj;
                using (SqlConnection cn = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL;
                    cmd.Parameters.AddWithValue("@legajo", legajo);
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        int legajo2 = dr.GetOrdinal("legajo");
                        int nro_familiar = dr.GetOrdinal("nro_familiar");
                        int fecha_nacimiento = dr.GetOrdinal("fecha_nacimiento");
                        while (dr.Read())
                        {
                            if (!dr.IsDBNull(fecha_nacimiento))
                            {
                                obj = new Familiares();
                                obj.legajo = dr.GetInt32(legajo2);
                                obj.nro_familiar = dr.GetInt32(nro_familiar);
                                obj.fecha_nacimiento = Convert.ToDateTime(dr.GetDateTime(fecha_nacimiento), culturaFecArgentina);
                                lst.Add(obj);
                            }
                        }
                    }
                    return lst;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<Familiares> GetFamiliaresToExcel(int legajo)
        {
            try
            {
                DateTimeFormatInfo culturaFecArgentina = new System.Globalization.CultureInfo("es-AR", false).DateTimeFormat;
                string strSQL = @"SELECT a.*
                                  FROM FAMILIARES a 
                                  JOIN EMPLEADOS b on 
                                     a.legajo = b.legajo AND
                                     b.fecha_baja is null
                                  WHERE
                                     a.legajo=@legajo AND
                                     a.id_parentezco=1";
                List<Familiares> lst = new List<Familiares>();
                //Familiares obj;
                using (SqlConnection cn = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL;
                    cmd.Parameters.AddWithValue("@legajo", legajo);
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    lst = mapeo(dr);
                    return lst;
                    //SqlDataReader dr = cmd.ExecuteReader();
                    //if (dr.HasRows)
                    //{
                    //    int legajo2 = dr.GetOrdinal("legajo");
                    //    int nro_familiar = dr.GetOrdinal("nro_familiar");
                    //    int fecha_nacimiento = dr.GetOrdinal("fecha_nacimiento");
                    //    while (dr.Read())
                    //    {
                    //        obj = new Familiares();
                    //        obj.legajo = dr.GetInt32(legajo2);
                    //        obj.nro_familiar = dr.GetInt32(nro_familiar);
                    //        obj.fecha_nacimiento = Convert.ToDateTime(dr.GetDateTime(fecha_nacimiento), culturaFecArgentina);
                    //        lst.Add(obj);
                    //    }
                    //}
                    //return lst;
                }

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}

