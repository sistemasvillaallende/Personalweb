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
    public class Concepto_Liq_x_Emp_Mov : DALBase
    {

        private static List<Entities.ConceptoLiqxEmpMov> mapeo(SqlDataReader dr)
        {
            List<ConceptoLiqxEmpMov> lst = new List<ConceptoLiqxEmpMov>();
            ConceptoLiqxEmpMov obj;
            if (dr.HasRows)
            {
                int id = dr.GetOrdinal("id");
                int legajo = dr.GetOrdinal("legajo");
                int fecha_mov = dr.GetOrdinal("fecha_mov");
                int id_tipo_movimiento = dr.GetOrdinal("id_tipo_movimiento");
                int cod_concepto_liq = dr.GetOrdinal("cod_concepto_liq");
                int valor_concepto_liq = dr.GetOrdinal("valor_concepto_liq");
                int fecha_vto = dr.GetOrdinal("fecha_vto");
                int descripcion = dr.GetOrdinal("descripcion");
                int observacion = dr.GetOrdinal("observacion");
                int usuario = dr.GetOrdinal("usuario");
                while (dr.Read())
                {
                    obj = new ConceptoLiqxEmpMov();
                    if (!dr.IsDBNull(id)) { obj.id = dr.GetInt32(id); }
                    if (!dr.IsDBNull(legajo)) { obj.legajo = dr.GetInt32(legajo); }
                    if (!dr.IsDBNull(fecha_mov)) { obj.fecha_mov = dr.GetDateTime(fecha_mov); }
                    if (!dr.IsDBNull(id_tipo_movimiento)) { obj.id_tipo_movimiento = dr.GetInt32(id_tipo_movimiento); }
                    if (!dr.IsDBNull(cod_concepto_liq)) { obj.cod_concepto_liq = dr.GetInt32(cod_concepto_liq); }
                    if (!dr.IsDBNull(valor_concepto_liq)) { obj.valor_concepto_liq = dr.GetInt32(valor_concepto_liq); }
                    if (!dr.IsDBNull(fecha_vto)) { obj.fecha_vto = dr.GetDateTime(fecha_vto); }
                    if (!dr.IsDBNull(descripcion)) { obj.descripcion = dr.GetString(descripcion); }
                    if (!dr.IsDBNull(observacion)) { obj.observacion = dr.GetString(observacion); }
                    if (!dr.IsDBNull(usuario)) { obj.usuario = dr.GetString(usuario); }
                    lst.Add(obj);
                }
            }
            return lst;
        }

        public static List<Entities.ConceptoLiqxEmpMov> read()
        {
            try
            {
                List<ConceptoLiqxEmpMov> lst = new List<ConceptoLiqxEmpMov>();
                using (SqlConnection con = GetConnection("Siimva"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM Concep_liquid_x_empleado_mov";
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

        public static Entities.ConceptoLiqxEmpMov getByPk(int id)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT *FROM Concep_liquid_x_empleado_mov WHERE");
                sql.AppendLine("id = @id");
                ConceptoLiqxEmpMov obj = null;
                using (SqlConnection con = GetConnection("Siimva"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<ConceptoLiqxEmpMov> lst = mapeo(dr);
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

        public static List<Entities.ConceptoLiqxEmpMov> getAllByLegajo(int legajo)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT  * FROM Concep_liquid_x_empleado_mov WHERE");
                sql.AppendLine("legajo = @legajo");
                List<ConceptoLiqxEmpMov> lst;
                using (SqlConnection con = GetConnection("Siimva"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@legajo", legajo);
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    lst = mapeo(dr);
                   
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int insert(Entities.ConceptoLiqxEmpMov obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO Concep_liquid_x_empleado_mov(");
                sql.AppendLine("legajo");
                sql.AppendLine(", fecha_mov");
                sql.AppendLine(", id_tipo_movimiento");
                sql.AppendLine(", cod_concepto_liq");
                sql.AppendLine(", valor_concepto_liq");
                sql.AppendLine(", fecha_vto");
                sql.AppendLine(", descripcion");
                sql.AppendLine(", observacion");
                sql.AppendLine(", usuario");
                sql.AppendLine(")");
                sql.AppendLine("VALUES");
                sql.AppendLine("(");
                sql.AppendLine("@legajo");
                sql.AppendLine(", @fecha_mov");
                sql.AppendLine(", @id_tipo_movimiento");
                sql.AppendLine(", @cod_concepto_liq");
                sql.AppendLine(", @valor_concepto_liq");
                sql.AppendLine(", @fecha_vto");
                sql.AppendLine(", @descripcion");
                sql.AppendLine(", @observacion");
                sql.AppendLine(", @usuario");
                sql.AppendLine(")");
                sql.AppendLine("SELECT SCOPE_IDENTITY()");
                using (SqlConnection con = GetConnection("Siimva"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@legajo", obj.legajo);
                    cmd.Parameters.AddWithValue("@fecha_mov", obj.fecha_mov);
                    cmd.Parameters.AddWithValue("@id_tipo_movimiento", obj.id_tipo_movimiento);
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", obj.cod_concepto_liq);
                    cmd.Parameters.AddWithValue("@valor_concepto_liq", obj.valor_concepto_liq);
                    cmd.Parameters.AddWithValue("@fecha_vto", obj.fecha_vto);
                    cmd.Parameters.AddWithValue("@descripcion", obj.descripcion);
                    cmd.Parameters.AddWithValue("@observacion", obj.observacion);
                    cmd.Parameters.AddWithValue("@usuario", obj.usuario);
                    cmd.Connection.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void update(Entities.ConceptoLiqxEmpMov obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE  Concep_liquid_x_empleado_mov SET");
                sql.AppendLine("legajo=@legajo");
                sql.AppendLine(", fecha_mov=@fecha_mov");
                sql.AppendLine(", id_tipo_movimiento=@id_tipo_movimiento");
                sql.AppendLine(", cod_concepto_liq=@cod_concepto_liq");
                sql.AppendLine(", valor_concepto_liq=@valor_concepto_liq");
                sql.AppendLine(", fecha_vto=@fecha_vto");
                sql.AppendLine(", descripcion=@descripcion");
                sql.AppendLine(", observacion=@observacion");
                sql.AppendLine(", usuario=@usuario");
                sql.AppendLine("WHERE");
                sql.AppendLine("id=@id");
                using (SqlConnection con = GetConnection("Siimva"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@legajo", obj.legajo);
                    cmd.Parameters.AddWithValue("@fecha_mov", obj.fecha_mov);
                    cmd.Parameters.AddWithValue("@id_tipo_movimiento", obj.id_tipo_movimiento);
                    cmd.Parameters.AddWithValue("@cod_concepto_liq", obj.cod_concepto_liq);
                    cmd.Parameters.AddWithValue("@valor_concepto_liq", obj.valor_concepto_liq);
                    cmd.Parameters.AddWithValue("@fecha_vto", obj.fecha_vto);
                    cmd.Parameters.AddWithValue("@descripcion", obj.descripcion);
                    cmd.Parameters.AddWithValue("@observacion", obj.observacion);
                    cmd.Parameters.AddWithValue("@usuario", obj.usuario);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void delete(Entities.ConceptoLiqxEmpMov obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("DELETE  Concep_liquid_x_empleado_mov ");
                sql.AppendLine("WHERE");
                sql.AppendLine("id=@id");
                using (SqlConnection con = GetConnection("Siimva"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@id", obj.id);
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
