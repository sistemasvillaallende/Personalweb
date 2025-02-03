using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Empleados_categoria_cantD
    {
        public static int CantidadEmpleadosByCategoria(int cod_categoria)
        {
            int cantEmpleados;

            try
            {
                using (SqlConnection cn = DALBase.GetConnection("Siimva"))
                {
                    if (cn.State != ConnectionState.Open)
                    {
                        cn.Open();
                    }

                    using (SqlCommand cmd1 = new SqlCommand())
                    {
                        StringBuilder SQL = new StringBuilder();
                        SQL.AppendLine(" SELECT COUNT(*) AS cantidad_empleados");
                        SQL.AppendLine(" FROM EMPLEADOS e ");
                        SQL.AppendLine(" WHERE e.fecha_baja IS NULL ");
                        SQL.AppendLine(" AND e.cod_categoria = @cod_categoria");
                        SQL.AppendLine(" AND e.legajo IS NOT NULL ");

                        cmd1.Parameters.AddWithValue("@cod_categoria", cod_categoria);
                        cmd1.Connection = cn;
                        cmd1.CommandType = CommandType.Text;
                        cmd1.CommandText = SQL.ToString();

                        cantEmpleados = Convert.ToInt32(cmd1.ExecuteScalar());
                        return cantEmpleados;
                    }

                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static Entities.Empleados_categoria_cant GetEmpleadosByCategoria(int codCategoria)
        {

            Entities.Empleados_categoria_cant objCantidad;

            StringBuilder strSQL = new StringBuilder();
            strSQL.AppendLine(" SELECT * FROM EMPLEADOS e ");
            strSQL.AppendLine(" WHERE e.fecha_baja IS NULL ");
            strSQL.AppendLine(" AND e.cod_categoria = @cod_categoria");
            strSQL.AppendLine(" AND e.legajo IS NOT NULL;");

            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Parameters.AddWithValue("@cod_categoria", codCategoria);
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    List<Entities.Empleado> lst = new List<Entities.Empleado>();
                    Entities.Empleado obj;
                    if (dr.HasRows)
                    {
                        int legajo = dr.GetOrdinal("legajo");
                        int fecha_alta_registro = dr.GetOrdinal("fecha_alta_registro");
                        int nombre = dr.GetOrdinal("nombre");
                        int cod_tipo_documento = dr.GetOrdinal("cod_tipo_documento");
                        int nro_documento = dr.GetOrdinal("nro_documento");
                        int fecha_nacimiento = dr.GetOrdinal("fecha_nacimiento");
                        int sexo = dr.GetOrdinal("sexo");
                        int pais_domicilio = dr.GetOrdinal("pais_domicilio");
                        int provincia_domicilio = dr.GetOrdinal("provincia_domicilio");
                        int ciudad_domicilio = dr.GetOrdinal("ciudad_domicilio");
                        int barrio_domicilio = dr.GetOrdinal("barrio_domicilio");
                        int calle_domicilio = dr.GetOrdinal("calle_domicilio");
                        int nro_domicilio = dr.GetOrdinal("nro_domicilio");
                        int piso_domicilio = dr.GetOrdinal("piso_domicilio");
                        int dpto_domicilio = dr.GetOrdinal("dpto_domicilio");
                        int monoblock_domicilio = dr.GetOrdinal("monoblock_domicilio");
                        int telefonos = dr.GetOrdinal("telefonos");
                        int cod_postal = dr.GetOrdinal("cod_postal");
                        int cod_estado_civil = dr.GetOrdinal("cod_estado_civil");
                        int fecha_ingreso = dr.GetOrdinal("fecha_ingreso");
                        int tarea = dr.GetOrdinal("tarea");
                        int cod_seccion = dr.GetOrdinal("cod_seccion");
                        int cod_categoria = dr.GetOrdinal("cod_categoria");
                        int cod_cargo = dr.GetOrdinal("cod_cargo");
                        int cod_banco = dr.GetOrdinal("cod_banco");
                        int nro_sucursal = dr.GetOrdinal("nro_sucursal");
                        int tipo_cuenta = dr.GetOrdinal("tipo_cuenta");
                        int nro_caja_ahorro = dr.GetOrdinal("nro_caja_ahorro");
                        int nro_cbu = dr.GetOrdinal("nro_cbu");
                        int nro_ipam = dr.GetOrdinal("nro_ipam");
                        int cuil = dr.GetOrdinal("cuil");
                        int nro_jubilacion = dr.GetOrdinal("nro_jubilacion");
                        int antiguedad_ant = dr.GetOrdinal("antiguedad_ant");
                        int antiguedad_actual = dr.GetOrdinal("antiguedad_actual");
                        int cod_clasif_per = dr.GetOrdinal("cod_clasif_per");
                        int cod_tipo_liq = dr.GetOrdinal("cod_tipo_liq");
                        int nro_ult_liq = dr.GetOrdinal("nro_ult_liq");
                        int anio_ult_liq = dr.GetOrdinal("anio_ult_liq");
                        int nro_cta_sb = dr.GetOrdinal("nro_cta_sb");
                        int nro_cta_gastos = dr.GetOrdinal("nro_cta_gastos");
                        int fecha_baja = dr.GetOrdinal("fecha_baja");
                        int nro_contrato = dr.GetOrdinal("nro_contrato");
                        int fecha_inicio_contrato = dr.GetOrdinal("fecha_inicio_contrato");
                        int fecha_fin_contrato = dr.GetOrdinal("fecha_fin_contrato");
                        int listar = dr.GetOrdinal("listar");
                        int id_regimen = dr.GetOrdinal("id_regimen");
                        int id_secretaria = dr.GetOrdinal("id_secretaria");
                        int id_direccion = dr.GetOrdinal("id_direccion");
                        int nro_nombramiento = dr.GetOrdinal("nro_nombramiento");
                        int fecha_nombramiento = dr.GetOrdinal("fecha_nombramiento");
                        int usuario = dr.GetOrdinal("usuario");
                        int cod_escala_aumento = dr.GetOrdinal("cod_escala_aumento");
                        int cod_regimen_empleado = dr.GetOrdinal("cod_regimen_empleado");
                        int id_oficina = dr.GetOrdinal("id_oficina");
                        int celular = dr.GetOrdinal("celular");
                        int email = dr.GetOrdinal("email");
                        int password = dr.GetOrdinal("password");
                        int passTemp = dr.GetOrdinal("passTemp");
                        int imprime_recibo = dr.GetOrdinal("imprime_recibo");
                        int id_programa = dr.GetOrdinal("id_programa");
                        int id_revista = dr.GetOrdinal("id_revista");
                        int fecha_revista = dr.GetOrdinal("fecha_revista");
                        int activo = dr.GetOrdinal("activo");
                        int licenciagenerada = dr.GetOrdinal("licenciagenerada");
                        int licenciadisponible = dr.GetOrdinal("licenciadisponible");
                        int licenciausadas = dr.GetOrdinal("licenciausadas");
                        int razonesparticulares = dr.GetOrdinal("razonesparticulares");
                        int id_profesional_monotributo = dr.GetOrdinal("id_profesional_monotributo");

                        // falta aca
                        while (dr.Read())
                        {
                            obj = new Entities.Empleado();
                            if (!dr.IsDBNull(legajo)) { obj.legajo = dr.GetInt32(legajo); }
                            if (!dr.IsDBNull(fecha_alta_registro)) { obj.fecha_alta_registro = dr.GetDateTime(fecha_alta_registro).ToString(); }
                            if (!dr.IsDBNull(nombre)) { obj.nombre = dr.GetString(nombre); }
                            if (!dr.IsDBNull(cod_tipo_documento)) { obj.cod_tipo_documento = dr.GetInt32(cod_tipo_documento); }
                            if (!dr.IsDBNull(nro_documento)) { obj.nro_documento = dr.GetString(nro_documento); }
                            if (!dr.IsDBNull(fecha_nacimiento)) { obj.fecha_nacimiento = dr.GetDateTime(fecha_nacimiento).ToString(); }
                            if (!dr.IsDBNull(sexo)) { obj.sexo = dr.GetString(sexo); }
                            if (!dr.IsDBNull(pais_domicilio)) { obj.pais_domicilio = dr.GetString(pais_domicilio); }
                            if (!dr.IsDBNull(provincia_domicilio)) { obj.provincia_domicilio = dr.GetString(provincia_domicilio); }
                            if (!dr.IsDBNull(ciudad_domicilio)) { obj.ciudad_domicilio = dr.GetString(ciudad_domicilio); }
                            if (!dr.IsDBNull(barrio_domicilio)) { obj.barrio_domicilio = dr.GetString(barrio_domicilio); }
                            if (!dr.IsDBNull(calle_domicilio)) { obj.calle_domicilio = dr.GetString(calle_domicilio); }
                            if (!dr.IsDBNull(nro_domicilio)) { obj.nro_domicilio = dr.GetInt32(nro_domicilio).ToString(); }
                            if (!dr.IsDBNull(piso_domicilio)) { obj.piso_domicilio = dr.GetString(piso_domicilio); }
                            if (!dr.IsDBNull(dpto_domicilio)) { obj.dpto_domicilio = dr.GetString(dpto_domicilio); }
                            if (!dr.IsDBNull(monoblock_domicilio)) { obj.monoblock_domicilio = dr.GetString(monoblock_domicilio); }
                            if (!dr.IsDBNull(telefonos)) { obj.telefonos = dr.GetString(telefonos); }
                            if (!dr.IsDBNull(cod_postal)) { obj.cod_postal = dr.GetString(cod_postal); }
                            if (!dr.IsDBNull(cod_estado_civil)) { obj.cod_estado_civil = dr.GetInt32(cod_estado_civil); }
                            if (!dr.IsDBNull(fecha_ingreso)) { obj.fecha_ingreso = dr.GetDateTime(fecha_ingreso).ToString(); }
                            if (!dr.IsDBNull(tarea)) { obj.tarea = dr.GetString(tarea); }
                            if (!dr.IsDBNull(cod_seccion)) { obj.cod_seccion = dr.GetInt32(cod_seccion); }
                            if (!dr.IsDBNull(cod_categoria)) { obj.cod_categoria = dr.GetInt32(cod_categoria); }
                            if (!dr.IsDBNull(cod_cargo)) { obj.cod_cargo = dr.GetInt32(cod_cargo); }
                            if (!dr.IsDBNull(cod_banco)) { obj.cod_banco = dr.GetInt32(cod_banco); }
                            if (!dr.IsDBNull(nro_sucursal)) { obj.nro_sucursal = dr.GetString(nro_sucursal); }
                            if (!dr.IsDBNull(tipo_cuenta)) { obj.tipo_cuenta = dr.GetString(tipo_cuenta); }
                            if (!dr.IsDBNull(nro_caja_ahorro)) { obj.nro_caja_ahorro = dr.GetString(nro_caja_ahorro); }
                            if (!dr.IsDBNull(nro_cbu)) { obj.nro_cbu = dr.GetString(nro_cbu); }
                            if (!dr.IsDBNull(nro_ipam)) { obj.nro_ipam = dr.GetString(nro_ipam); }
                            if (!dr.IsDBNull(cuil)) { obj.cuil = dr.GetString(cuil); }
                            if (!dr.IsDBNull(nro_jubilacion)) { obj.nro_jubilacion = dr.GetString(nro_jubilacion); }
                            if (!dr.IsDBNull(antiguedad_ant)) { obj.antiguedad_ant = dr.GetInt32(antiguedad_ant); }
                            if (!dr.IsDBNull(antiguedad_actual)) { obj.antiguedad_actual = dr.GetInt32(antiguedad_actual); }
                            if (!dr.IsDBNull(cod_clasif_per)) { obj.cod_clasif_per = dr.GetInt32(cod_clasif_per); }
                            if (!dr.IsDBNull(cod_tipo_liq)) { obj.cod_tipo_liq = dr.GetInt32(cod_tipo_liq); }
                            if (!dr.IsDBNull(nro_ult_liq)) { obj.nro_ult_liq = dr.GetInt32(nro_ult_liq); }
                            if (!dr.IsDBNull(anio_ult_liq)) { obj.anio_ult_liq = dr.GetInt32(anio_ult_liq); }
                            if (!dr.IsDBNull(nro_cta_sb)) { obj.nro_cta_sb = dr.GetString(nro_cta_sb); }
                            if (!dr.IsDBNull(nro_cta_gastos)) { obj.nro_cta_gastos = dr.GetString(nro_cta_gastos); }
                            if (!dr.IsDBNull(fecha_baja)) { obj.fecha_baja = dr.GetDateTime(fecha_baja).ToString(); }
                            if (!dr.IsDBNull(nro_contrato)) { obj.nro_contrato = dr.GetInt32(nro_contrato); }
                            if (!dr.IsDBNull(fecha_inicio_contrato)) { obj.fecha_inicio_contrato = dr.GetDateTime(fecha_inicio_contrato).ToString(); }
                            if (!dr.IsDBNull(fecha_fin_contrato)) { obj.fecha_fin_contrato = dr.GetDateTime(fecha_fin_contrato).ToString(); }
                            if (!dr.IsDBNull(listar)) { obj.listar = dr.GetBoolean(listar); }
                            if (!dr.IsDBNull(id_regimen)) { obj.id_regimen = dr.GetInt16(id_regimen); }
                            if (!dr.IsDBNull(id_secretaria)) { obj.id_secretaria = dr.GetInt32(id_secretaria); }
                            if (!dr.IsDBNull(id_direccion)) { obj.id_direccion = dr.GetInt32(id_direccion); }
                            if (!dr.IsDBNull(nro_nombramiento)) { obj.nro_nombramiento = dr.GetString(nro_nombramiento); }
                            if (!dr.IsDBNull(fecha_nombramiento)) { obj.fecha_nombramiento = dr.GetDateTime(fecha_nombramiento).ToString(); }
                            if (!dr.IsDBNull(usuario)) { obj.usuario = dr.GetString(usuario); }
                            if (!dr.IsDBNull(cod_escala_aumento)) { obj.cod_escala_aumento = dr.GetInt32(cod_escala_aumento); }
                            if (!dr.IsDBNull(cod_regimen_empleado)) { obj.cod_regimen_empleado = dr.GetInt32(cod_regimen_empleado); }
                            if (!dr.IsDBNull(id_oficina)) { obj.id_oficina = dr.GetInt32(id_oficina); }
                            if (!dr.IsDBNull(celular)) { obj.celular = dr.GetString(celular); }
                            if (!dr.IsDBNull(email)) { obj.email = dr.GetString(email); }
                            if (!dr.IsDBNull(imprime_recibo)) { obj.imprime_recibo = dr.GetInt16(imprime_recibo); }
                            if (!dr.IsDBNull(id_programa)) { obj.id_programa = dr.GetInt32(id_programa); }
                            if (!dr.IsDBNull(id_revista)) { obj.id_revista = dr.GetInt32(id_revista); }
                            if (!dr.IsDBNull(fecha_revista)) { obj.fecha_revista = dr.GetDateTime(fecha_revista).ToString(); }
                            if (!dr.IsDBNull(activo)) { obj.activo = dr.GetBoolean(activo); }
                            if (!dr.IsDBNull(licenciagenerada)) { obj.licenciagenerada = dr.GetInt32(licenciagenerada); }
                            if (!dr.IsDBNull(licenciadisponible)) { obj.licenciadisponible = dr.GetInt32(licenciadisponible); }
                            if (!dr.IsDBNull(licenciausadas)) { obj.licenciausadas = dr.GetInt32(licenciausadas); }
                            if (!dr.IsDBNull(razonesparticulares)) { obj.razonesparticulares = dr.GetInt32(razonesparticulares); }
                            lst.Add(obj);
                        }
                        dr.Close();
                    }

                    objCantidad = new Entities.Empleados_categoria_cant();

                    objCantidad.cantidad_empleados = CantidadEmpleadosByCategoria(codCategoria);
                    objCantidad.Empleados = lst;
                    return objCantidad;

                }
                catch (Exception e)
                {
                    Console.WriteLine("Error in query!" + e.ToString());
                    throw e;
                }
            }

        }

    }
}
