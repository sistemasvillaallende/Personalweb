using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Entities;
using System.Threading.Tasks;
using System.Transactions;
using System.Globalization;
using Newtonsoft.Json;

namespace DAL
{
    public class EmpleadoD : ConsultaEmpleadoD
    {

        #region Constructor
        /// <summary>
        /// Contructor de la clase sin parametro.
        /// </summary>


        public static List<Entities.LstEmpleados> GetLstEmpleados(SqlConnection cn, SqlTransaction trx)
        {
            StringBuilder strSQL = new StringBuilder();
            StringBuilder strCondicion = new StringBuilder();
            SqlCommand cmd = null;

            List<Entities.LstEmpleados> lst = new List<Entities.LstEmpleados>();
            Entities.LstEmpleados oEmp;


            strSQL.AppendLine("SELECT");
            strSQL.AppendLine("e.legajo, rtrim(ltrim(e.nombre)) as nombre, ");
            strSQL.AppendLine("convert(varchar(10), e.fecha_ingreso, 103) as fecha_ingreso , convert(varchar(10), e.fecha_nacimiento, 103) as fecha_nacimiento,");
            strSQL.AppendLine("e.cod_categoria, c.des_categoria, e.tarea, tl.des_tipo_liq,");
            strSQL.AppendLine("b.nom_banco, e.nro_caja_ahorro, e.nro_cbu,");
            strSQL.AppendLine("e.nro_documento, e.nro_cta_sb, e.nro_cta_gastos,");
            strSQL.AppendLine("rtrim(ltrim(s.descripcion)) as secretaria, rtrim(ltrim(d1.descripcion)) as direccion, ltrim(rtrim(o.nombre_oficina)) as oficina");
            strSQL.AppendLine(",rtrim(ltrim(p.programa)) as programa");
            strSQL.AppendLine(",rtrim(ltrim(srl.descripcion)) as situacion_revista");
            strSQL.AppendLine("FROM EMPLEADOS e");
            strSQL.AppendLine("inner join TIPOS_LIQUIDACION tl on");
            strSQL.AppendLine("tl.cod_tipo_liq = e.cod_tipo_liq");
            strSQL.AppendLine("inner join BANCOS b on");
            strSQL.AppendLine("b.cod_banco = e.cod_banco");
            strSQL.AppendLine("inner join CATEGORIAS c on");
            strSQL.AppendLine("e.cod_categoria = c.cod_categoria");
            strSQL.AppendLine("inner join secretaria s on");
            strSQL.AppendLine("s.id_secretaria = e.id_secretaria");
            strSQL.AppendLine("inner join direccion d1 on");
            strSQL.AppendLine("d1.id_direccion = e.id_direccion");
            strSQL.AppendLine("inner join oficinas o on");
            strSQL.AppendLine("o.codigo_oficina = e.id_oficina");
            strSQL.AppendLine("inner join programas_publicos p on");
            strSQL.AppendLine("p.id_programa = e.id_programa");
            strSQL.AppendLine("Left join situacion_revista_legajo srl on");
            strSQL.AppendLine("srl.id_revista = e.id_revista");
            strSQL.AppendLine("WHERE e.fecha_baja is null");
            strSQL.AppendLine("ORDER BY e.legajo");

            //using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            //{
            try
            {
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd.Connection = cn;
                cmd.Transaction = trx;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    int legajo = dr.GetOrdinal("legajo");
                    int nombre = dr.GetOrdinal("nombre");
                    int fecha_ingreso = dr.GetOrdinal("fecha_ingreso");
                    int fecha_nac = dr.GetOrdinal("fecha_nacimiento");
                    int cod_categoria = dr.GetOrdinal("cod_categoria");
                    int des_categoria = dr.GetOrdinal("des_categoria");
                    int tarea = dr.GetOrdinal("tarea");
                    int des_tipo_liq = dr.GetOrdinal("des_tipo_liq");
                    int nom_banco = dr.GetOrdinal("nom_banco");
                    int nro_caja_ahorro = dr.GetOrdinal("nro_caja_ahorro");
                    int nro_cbu = dr.GetOrdinal("nro_cbu");
                    int nro_documento = dr.GetOrdinal("nro_documento");
                    int nro_cta_sb = dr.GetOrdinal("nro_cta_sb");
                    int cod_seccion = dr.GetOrdinal("cod_seccion");
                    int secretaria = dr.GetOrdinal("secretaria");
                    int direccion = dr.GetOrdinal("direccion");
                    int oficina = dr.GetOrdinal("oficina");
                    //int usuario = dr.GetOrdinal("usuario");
                    int programa = dr.GetOrdinal("programa");
                    int situacion_revista = dr.GetOrdinal("situacion_revista");

                    while (dr.Read())
                    {
                        oEmp = new LstEmpleados();

                        if (!dr.IsDBNull(legajo)) oEmp.legajo = dr.GetInt32(legajo);
                        if (!dr.IsDBNull(nombre)) oEmp.nombre = dr.GetString(nombre);
                        if (!dr.IsDBNull(fecha_ingreso)) oEmp.fecha_ingreso = dr.GetString(fecha_ingreso);
                        if (!dr.IsDBNull(nro_documento)) oEmp.nro_documento = dr.GetString(nro_documento);
                        if (!dr.IsDBNull(fecha_nac)) oEmp.fecha_nacimiento = dr.GetString(fecha_nac);
                        if (!dr.IsDBNull(cod_categoria)) oEmp.cod_categoria = dr.GetInt16(cod_categoria);
                        if (!dr.IsDBNull(des_categoria)) oEmp.des_categoria = dr.GetString(des_categoria);
                        if (!dr.IsDBNull(tarea)) oEmp.tarea = dr.GetString(tarea);
                        if (!dr.IsDBNull(des_tipo_liq)) oEmp.des_tipo_liq = dr.GetString(des_tipo_liq);
                        if (!dr.IsDBNull(nom_banco)) oEmp.nom_banco = dr.GetString(nom_banco);
                        if (!dr.IsDBNull(nro_caja_ahorro)) oEmp.nro_caja_ahorro = dr.GetString(nro_caja_ahorro);
                        if (!dr.IsDBNull(nro_cbu)) oEmp.nro_cbu = dr.GetString(nro_cbu);
                        if (!dr.IsDBNull(nro_documento)) oEmp.nro_documento = dr.GetString(nro_documento);
                        if (!dr.IsDBNull(nro_cta_sb)) oEmp.nro_cta_sb = dr.GetString(nro_cta_sb);
                        if (!dr.IsDBNull(secretaria)) oEmp.secrectaria = dr.GetString(secretaria);
                        if (!dr.IsDBNull(direccion)) oEmp.direccion = dr.GetString(direccion);
                        if (!dr.IsDBNull(oficina)) oEmp.oficina = dr.GetString(oficina);
                        if (!dr.IsDBNull(programa)) oEmp.programa = dr.GetString(programa);
                        if (!dr.IsDBNull(situacion_revista)) oEmp.situacion_revista = dr.GetString(programa);
                        lst.Add(oEmp);
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            return lst;
        }

        public static List<Entities.LstEmpleados> GetEmpleadosAll()
        {
            StringBuilder strSQL = new StringBuilder();
            StringBuilder strCondicion = new StringBuilder();
            SqlCommand cmd = null;

            List<Entities.LstEmpleados> lst = new List<Entities.LstEmpleados>();
            Entities.LstEmpleados oEmp;


            strSQL.AppendLine("SELECT");
            strSQL.AppendLine("e.legajo, rtrim(ltrim(e.nombre)) as nombre, ");
            strSQL.AppendLine("convert(varchar(10), e.fecha_ingreso, 103) as fecha_ingreso , convert(varchar(10), e.fecha_nacimiento, 103) as fecha_nacimiento,");
            strSQL.AppendLine("e.cod_categoria, c.des_categoria, e.tarea, tl.des_tipo_liq,");
            strSQL.AppendLine("b.nom_banco, e.nro_caja_ahorro, e.nro_cbu,");
            strSQL.AppendLine("e.nro_documento, e.nro_cta_sb, e.nro_cta_gastos,");
            strSQL.AppendLine("rtrim(ltrim(s.descripcion)) as Secretaria, rtrim(ltrim(d1.descripcion)) as Direccion, ltrim(rtrim(o.nombre_oficina)) as Oficina,");
            strSQL.AppendLine("e.imprime_recibo, e.id_programa");
            strSQL.AppendLine(",rtrim(ltrim(p.programa)) as Programa");
            strSQL.AppendLine("FROM EMPLEADOS e");
            strSQL.AppendLine("join TIPOS_LIQUIDACION tl on");
            strSQL.AppendLine("tl.cod_tipo_liq = e.cod_tipo_liq");
            strSQL.AppendLine("join BANCOS b on");
            strSQL.AppendLine("b.cod_banco = e.cod_banco");
            strSQL.AppendLine("join CATEGORIAS c on");
            strSQL.AppendLine("e.cod_categoria = c.cod_categoria");
            strSQL.AppendLine("join secretaria s on");
            strSQL.AppendLine("s.id_secretaria = e.id_secretaria");
            strSQL.AppendLine("join direccion d1 on");
            strSQL.AppendLine("d1.id_direccion = e.id_direccion");
            strSQL.AppendLine("join oficinas o on");
            strSQL.AppendLine("o.codigo_oficina = e.id_oficina");
            strSQL.AppendLine("inner join programas_publicos p on");
            strSQL.AppendLine("p.id_programa = e.id_programa");
            //strSQL.AppendLine("WHERE e.fecha_baja is null");
            strSQL.AppendLine("ORDER BY e.legajo");

            using (SqlConnection cn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection = cn;
                    cmd.Connection.Open();
                    //cmd.Transaction = trx;
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        int legajo = dr.GetOrdinal("legajo");
                        int nombre = dr.GetOrdinal("nombre");
                        int fecha_ingreso = dr.GetOrdinal("fecha_ingreso");
                        int fecha_nac = dr.GetOrdinal("fecha_nacimiento");
                        int cod_categoria = dr.GetOrdinal("cod_categoria");
                        int des_categoria = dr.GetOrdinal("des_categoria");
                        int tarea = dr.GetOrdinal("tarea");
                        int des_tipo_liq = dr.GetOrdinal("des_tipo_liq");
                        int nom_banco = dr.GetOrdinal("nom_banco");
                        int nro_caja_ahorro = dr.GetOrdinal("nro_caja_ahorro");
                        int nro_cbu = dr.GetOrdinal("nro_cbu");
                        int nro_documento = dr.GetOrdinal("nro_documento");
                        int nro_cta_sb = dr.GetOrdinal("nro_cta_sb");
                        //int cod_seccion = dr.GetOrdinal("cod_seccion");
                        int secretaria = dr.GetOrdinal("secretaria");
                        int direccion = dr.GetOrdinal("direccion");
                        int oficina = dr.GetOrdinal("oficina");
                        int imprime_recibo = dr.GetOrdinal("imprime_recibo");
                        //int usuario = dr.GetOrdinal("usuario");
                        int programa = dr.GetOrdinal("programa");

                        while (dr.Read())
                        {
                            oEmp = new LstEmpleados();

                            if (!dr.IsDBNull(legajo)) oEmp.legajo = dr.GetInt32(legajo);
                            if (!dr.IsDBNull(nombre)) oEmp.nombre = dr.GetString(nombre);
                            if (!dr.IsDBNull(fecha_ingreso)) oEmp.fecha_ingreso = dr.GetString(fecha_ingreso);
                            if (!dr.IsDBNull(nro_documento)) oEmp.nro_documento = dr.GetString(nro_documento);
                            if (!dr.IsDBNull(fecha_nac)) oEmp.fecha_nacimiento = dr.GetString(fecha_nac);
                            if (!dr.IsDBNull(cod_categoria)) oEmp.cod_categoria = dr.GetInt32(cod_categoria);
                            if (!dr.IsDBNull(des_categoria)) oEmp.des_categoria = dr.GetString(des_categoria);
                            if (!dr.IsDBNull(tarea)) oEmp.tarea = dr.GetString(tarea);
                            if (!dr.IsDBNull(des_tipo_liq)) oEmp.des_tipo_liq = dr.GetString(des_tipo_liq);
                            if (!dr.IsDBNull(nom_banco)) oEmp.nom_banco = dr.GetString(nom_banco);
                            if (!dr.IsDBNull(nro_caja_ahorro)) oEmp.nro_caja_ahorro = dr.GetString(nro_caja_ahorro);
                            if (!dr.IsDBNull(nro_cbu)) oEmp.nro_cbu = dr.GetString(nro_cbu);
                            if (!dr.IsDBNull(nro_documento)) oEmp.nro_documento = dr.GetString(nro_documento);
                            if (!dr.IsDBNull(nro_cta_sb)) oEmp.nro_cta_sb = dr.GetString(nro_cta_sb);
                            if (!dr.IsDBNull(secretaria)) oEmp.secrectaria = dr.GetString(secretaria);
                            if (!dr.IsDBNull(direccion)) oEmp.direccion = dr.GetString(direccion);
                            if (!dr.IsDBNull(oficina)) oEmp.oficina = dr.GetString(oficina);
                            if (!dr.IsDBNull(imprime_recibo)) oEmp.imprime_recibo = dr.GetInt16(imprime_recibo);
                            if (!dr.IsDBNull(programa)) oEmp.programa = dr.GetString(programa);
                            lst.Add(oEmp);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error in query!" + e.ToString());
                    throw e;
                }
            }
            return lst;
        }

        public static List<Entities.LstEmpleados> GetLiqEmpleado(int anio, int cod_tipo_liq, int nro_liquidacion, SqlConnection cn, SqlTransaction trx)
        {

            List<Entities.LstEmpleados> lst = new List<Entities.LstEmpleados>();
            Entities.LstEmpleados oEmp;
            string strSQL = "";
            SqlCommand cmd = null;
            strSQL = @" SELECT A.legajo, A.antiguedad_ant, B.sueldo_basico,
                        CONVERT(varchar(10), A.fecha_ingreso, 103) as fecha_ingreso, 
                        A.cod_categoria, A.cod_cargo, A.tarea, A.nro_cta_sb, A.cod_clasif_per,
                        CP.des_clasif_per
                        FROM EMPLEADOS A WITH (NOLOCK)
                        JOIN  CATEGORIAS B ON 
                          A.cod_categoria=B.cod_categoria 
                        LEFT JOIN CLASIFICACIONES_PERSONAL cp on
                          cp.cod_clasif_per=A.cod_clasif_Per   
                        WHERE A.fecha_baja IS NULL
                          AND A.cod_tipo_liq = @cod_tipo_liq
                          --AND (A.anio_ult_liq <= @anio OR A.anio_ult_liq IS NULL)
                          --AND (A.nro_ult_liq <= @nro_liquidacion OR A.nro_ult_liq IS NULL) 
                        ORDER BY A.legajo";
            try
            {
                cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@anio", anio);
                cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                cmd.CommandText = strSQL.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Transaction = trx;
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    int legajo = dr.GetOrdinal("legajo");
                    int antiguedad_ant = dr.GetOrdinal("antiguedad_ant");
                    int sueldo_basico = dr.GetOrdinal("sueldo_basico");
                    int fecha_ingreso = dr.GetOrdinal("fecha_ingreso");
                    int cod_categoria = dr.GetOrdinal("cod_categoria");
                    int cod_cargo = dr.GetOrdinal("cod_cargo");
                    int tarea = dr.GetOrdinal("tarea");
                    int nro_cta_sb = dr.GetOrdinal("nro_cta_sb");
                    int cod_clasif_per = dr.GetOrdinal("cod_clasif_per");
                    int des_clasif_per = dr.GetOrdinal("des_clasif_per");


                    while (dr.Read())
                    {
                        oEmp = new LstEmpleados();
                        if (!dr.IsDBNull(legajo)) oEmp.legajo = dr.GetInt32(legajo);
                        if (!dr.IsDBNull(antiguedad_ant))
                            oEmp.antiguedad_ant = dr.GetInt32(antiguedad_ant);
                        if (!dr.IsDBNull(fecha_ingreso)) oEmp.fecha_ingreso = Convert.ToString(dr.GetString(fecha_ingreso));
                        if (!dr.IsDBNull(sueldo_basico)) oEmp.sueldo_basico = dr.GetDecimal(sueldo_basico);
                        if (!dr.IsDBNull(cod_categoria)) oEmp.cod_categoria = dr.GetInt32(cod_categoria);
                        if (!dr.IsDBNull(cod_cargo)) oEmp.cod_cargo = dr.GetInt32(cod_cargo);
                        if (!dr.IsDBNull(tarea)) oEmp.tarea = dr.GetString(tarea);
                        if (!dr.IsDBNull(nro_cta_sb)) oEmp.nro_cta_sb = dr.GetString(nro_cta_sb);
                        if (!dr.IsDBNull(cod_clasif_per)) oEmp.cod_clasif_per = dr.GetInt32(cod_clasif_per);
                        if (!dr.IsDBNull(des_clasif_per)) oEmp.des_clasif_per = dr.GetString(des_clasif_per);
                        lst.Add(oEmp);
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

        public static Entities.Empleado GetByPk(int legajo)
        {
            Entities.Empleado objEmp = new Empleado();
            SqlCommand cmd;
            SqlDataReader dr;
            StringBuilder strSQL = new StringBuilder();

            SqlConnection cn = DALBase.GetConnection("SIIMVA");

            strSQL.AppendLine("SELECT * ");
            strSQL.AppendLine("FROM EMPLEADOS (nolock) ");
            strSQL.AppendLine("WHERE legajo = @legajo");
            //strSQL.AppendLine(" AND fecha_baja is null");

            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@legajo", legajo);

            try
            {
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd.Connection.Open();
                dr = cmd.ExecuteReader();
                //
                int imprime_recibo = dr.GetOrdinal("imprime_recibo");
                while (dr.Read())
                {
                    objEmp = new Empleado();

                    if (!dr.IsDBNull(dr.GetOrdinal("legajo")))
                        objEmp.legajo = dr.GetInt32(dr.GetOrdinal("legajo"));

                    if (!dr.IsDBNull(dr.GetOrdinal("nombre")))
                        objEmp.nombre = dr.GetString(dr.GetOrdinal("nombre"));

                    if (!dr.IsDBNull(dr.GetOrdinal("fecha_alta_registro")))
                        objEmp.fecha_alta_registro = Convert.ToDateTime(dr["fecha_alta_registro"]).ToString("dd/MM/yyyy");

                    if (!dr.IsDBNull(dr.GetOrdinal("cod_tipo_documento")))
                        objEmp.cod_tipo_documento = dr.GetInt32(dr.GetOrdinal("cod_tipo_documento"));

                    if (!dr.IsDBNull(dr.GetOrdinal("nro_documento")))
                        objEmp.nro_documento = dr.GetString(dr.GetOrdinal("nro_documento"));
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_tipo_documento")))
                        objEmp.cod_tipo_documento = dr.GetInt32(dr.GetOrdinal("cod_tipo_documento"));

                    if (!dr.IsDBNull(dr.GetOrdinal("fecha_nacimiento")))
                        objEmp.fecha_nacimiento = Convert.ToDateTime(dr["fecha_nacimiento"]).ToString("dd/MM/yyyy");


                    if (!dr.IsDBNull(dr.GetOrdinal("pais_domicilio")))
                        objEmp.pais_domicilio = dr.GetString(dr.GetOrdinal("pais_domicilio"));

                    if (!dr.IsDBNull(dr.GetOrdinal("provincia_domicilio")))
                        objEmp.provincia_domicilio = dr.GetString(dr.GetOrdinal("provincia_domicilio"));

                    if (!dr.IsDBNull(dr.GetOrdinal("ciudad_domicilio")))
                        objEmp.ciudad_domicilio = dr.GetString(dr.GetOrdinal("ciudad_domicilio"));

                    if (!dr.IsDBNull(dr.GetOrdinal("barrio_domicilio")))
                        objEmp.barrio_domicilio = dr.GetString(dr.GetOrdinal("barrio_domicilio"));

                    if (!dr.IsDBNull(dr.GetOrdinal("calle_domicilio")))
                        objEmp.calle_domicilio = dr.GetString(dr.GetOrdinal("calle_domicilio"));

                    if (!dr.IsDBNull(dr.GetOrdinal("nro_domicilio")))
                        objEmp.nro_domicilio = Convert.ToString(dr.GetInt32(dr.GetOrdinal("nro_domicilio")));

                    if (!dr.IsDBNull(dr.GetOrdinal("piso_domicilio")))
                        objEmp.piso_domicilio = dr.GetString(dr.GetOrdinal("piso_domicilio"));

                    if (!dr.IsDBNull(dr.GetOrdinal("dpto_domicilio")))
                        objEmp.dpto_domicilio = dr.GetString(dr.GetOrdinal("dpto_domicilio"));

                    if (!dr.IsDBNull(dr.GetOrdinal("monoblock_domicilio")))
                        objEmp.monoblock_domicilio = dr.GetString(dr.GetOrdinal("monoblock_domicilio"));

                    if (!dr.IsDBNull(dr.GetOrdinal("telefonos")))
                        objEmp.telefonos = dr.GetString(dr.GetOrdinal("telefonos"));

                    if (!dr.IsDBNull(dr.GetOrdinal("celular")))
                        objEmp.celular = dr.GetString(dr.GetOrdinal("celular"));

                    if (!dr.IsDBNull(dr.GetOrdinal("cod_postal")))
                        objEmp.cod_postal = dr.GetString(dr.GetOrdinal("cod_postal"));

                    if (!dr.IsDBNull(dr.GetOrdinal("cod_estado_civil")))
                        objEmp.cod_estado_civil = dr.GetInt32(dr.GetOrdinal("cod_estado_civil"));

                    if (!dr.IsDBNull(dr.GetOrdinal("sexo")))
                        objEmp.sexo = dr.GetString(dr.GetOrdinal("sexo"));

                    if (!dr.IsDBNull(dr.GetOrdinal("fecha_ingreso")))
                        objEmp.fecha_ingreso = Convert.ToDateTime(dr["fecha_ingreso"]).ToString("dd/MM/yyyy");

                    if (!dr.IsDBNull(dr.GetOrdinal("tarea")))
                        objEmp.tarea = dr.GetString(dr.GetOrdinal("tarea"));

                    if (!dr.IsDBNull(dr.GetOrdinal("cod_seccion")))
                        objEmp.cod_seccion = dr.GetInt32(dr.GetOrdinal("cod_seccion"));

                    if (!dr.IsDBNull(dr.GetOrdinal("cod_categoria")))
                        objEmp.cod_categoria = dr.GetInt32(dr.GetOrdinal("cod_categoria"));

                    if (!dr.IsDBNull(dr.GetOrdinal("cod_cargo")))
                        objEmp.cod_cargo = dr.GetInt32(dr.GetOrdinal("cod_cargo"));

                    if (!dr.IsDBNull(dr.GetOrdinal("cod_banco")))
                        objEmp.cod_banco = dr.GetInt32(dr.GetOrdinal("cod_banco"));

                    if (!dr.IsDBNull(dr.GetOrdinal("nro_sucursal")))
                        objEmp.nro_sucursal = dr.GetString(dr.GetOrdinal("nro_sucursal"));

                    if (!dr.IsDBNull(dr.GetOrdinal("tipo_cuenta")))
                        objEmp.tipo_cuenta = dr.GetString(dr.GetOrdinal("tipo_cuenta"));

                    if (!dr.IsDBNull(dr.GetOrdinal("nro_caja_ahorro")))
                        objEmp.nro_caja_ahorro = dr.GetString(dr.GetOrdinal("nro_caja_ahorro"));

                    if (!dr.IsDBNull(dr.GetOrdinal("nro_cbu")))
                        objEmp.nro_cbu = dr.GetString(dr.GetOrdinal("nro_cbu"));

                    if (!dr.IsDBNull(dr.GetOrdinal("nro_ipam")))
                        objEmp.nro_ipam = dr.GetString(dr.GetOrdinal("nro_ipam"));

                    if (!dr.IsDBNull(dr.GetOrdinal("cuil")))
                        objEmp.cuil = dr.GetString(dr.GetOrdinal("cuil"));

                    if (!dr.IsDBNull(dr.GetOrdinal("nro_jubilacion")))
                        objEmp.nro_jubilacion = dr.GetString(dr.GetOrdinal("nro_jubilacion"));

                    if (!dr.IsDBNull(dr.GetOrdinal("nro_jubilacion")))
                        objEmp.nro_jubilacion = dr.GetString(dr.GetOrdinal("nro_jubilacion"));

                    if (!dr.IsDBNull(dr.GetOrdinal("antiguedad_ant")))
                        objEmp.antiguedad_ant = dr.GetInt32(dr.GetOrdinal("antiguedad_ant"));

                    if (!dr.IsDBNull(dr.GetOrdinal("antiguedad_actual")))
                        objEmp.antiguedad_actual = dr.GetInt32(dr.GetOrdinal("antiguedad_actual"));

                    if (!dr.IsDBNull(dr.GetOrdinal("cod_clasif_per")))
                        objEmp.cod_clasif_per = dr.GetInt32(dr.GetOrdinal("cod_clasif_per"));

                    if (!dr.IsDBNull(dr.GetOrdinal("cod_tipo_liq")))
                        objEmp.cod_tipo_liq = dr.GetInt32(dr.GetOrdinal("cod_tipo_liq"));

                    if (!dr.IsDBNull(dr.GetOrdinal("nro_ult_liq")))
                        objEmp.nro_ult_liq = dr.GetInt32(dr.GetOrdinal("nro_ult_liq"));

                    if (!dr.IsDBNull(dr.GetOrdinal("anio_ult_liq")))
                        objEmp.anio_ult_liq = dr.GetInt32(dr.GetOrdinal("anio_ult_liq"));

                    if (!dr.IsDBNull(dr.GetOrdinal("nro_cta_sb")))
                        objEmp.nro_cta_sb = dr.GetString(dr.GetOrdinal("nro_cta_sb"));

                    if (!dr.IsDBNull(dr.GetOrdinal("nro_cta_gastos")))
                        objEmp.nro_cta_gastos = dr.GetString(dr.GetOrdinal("nro_cta_gastos"));

                    if (!dr.IsDBNull(dr.GetOrdinal("fecha_baja")))
                        objEmp.fecha_baja = Convert.ToDateTime(dr["fecha_baja"]).ToString("dd/MM/yyyy");

                    if (!dr.IsDBNull(dr.GetOrdinal("nro_contrato")))
                        objEmp.nro_contrato = dr.GetInt32(dr.GetOrdinal("nro_contrato"));

                    if (!dr.IsDBNull(dr.GetOrdinal("fecha_inicio_contrato")))
                        objEmp.fecha_inicio_contrato = Convert.ToDateTime((dr["fecha_inicio_contrato"])).ToString("dd/MM/yyyy");

                    if (!dr.IsDBNull(dr.GetOrdinal("fecha_fin_contrato")))
                        objEmp.fecha_fin_contrato = Convert.ToDateTime((dr["fecha_fin_contrato"])).ToString("dd/MM/yyyy");

                    if (!dr.IsDBNull(dr.GetOrdinal("id_regimen")))
                        objEmp.id_regimen = dr.GetInt16(dr.GetOrdinal("id_regimen"));

                    if (!dr.IsDBNull(dr.GetOrdinal("id_secretaria")))
                        objEmp.id_secretaria = dr.GetInt32(dr.GetOrdinal("id_secretaria"));

                    if (!dr.IsDBNull(dr.GetOrdinal("id_direccion")))
                        objEmp.id_direccion = dr.GetInt32(dr.GetOrdinal("id_direccion"));

                    if (!dr.IsDBNull(dr.GetOrdinal("nro_nombramiento")))
                        objEmp.nro_nombramiento = dr.GetString(dr.GetOrdinal("nro_nombramiento"));

                    if (!dr.IsDBNull(dr.GetOrdinal("fecha_nombramiento")))
                        objEmp.fecha_nombramiento = Convert.ToDateTime(dr["fecha_nombramiento"]).ToString("dd/MM/yyyy"); ;

                    if (!dr.IsDBNull(dr.GetOrdinal("usuario")))
                        objEmp.usuario = dr.GetString(dr.GetOrdinal("usuario"));

                    if (!dr.IsDBNull(dr.GetOrdinal("cod_escala_aumento")))
                        objEmp.cod_escala_aumento = dr.GetInt32(dr.GetOrdinal("cod_escala_aumento"));

                    if (!dr.IsDBNull(dr.GetOrdinal("cod_regimen_empleado")))
                        objEmp.cod_regimen_empleado = dr.GetInt32(dr.GetOrdinal("cod_regimen_empleado"));

                    if (!dr.IsDBNull(dr.GetOrdinal("id_oficina")))
                        objEmp.id_oficina = dr.GetInt32(dr.GetOrdinal("id_oficina"));

                    if (!dr.IsDBNull(dr.GetOrdinal("email")))
                        objEmp.email = dr.GetString(dr.GetOrdinal("email"));

                    if (!dr.IsDBNull(imprime_recibo)) objEmp.imprime_recibo = dr.GetInt16(imprime_recibo);

                }
            }

            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            dr.Close();
            return objEmp;
        }

        public static List<LstEmpleados> GetLiqEmpleadoAguinaldo(int anio, int cod_tipo_liq, int nro_liquidacion, SqlConnection cn, SqlTransaction trx)
        {
            List<Entities.LstEmpleados> lst = new List<Entities.LstEmpleados>();
            Entities.LstEmpleados oEmp;
            string strSQL = string.Empty;
            SqlCommand cmd = null;
            DateTimeFormatInfo culturaFecArgentina = new CultureInfo("es-AR", false).DateTimeFormat;
            strSQL = @"SELECT A.legajo, A.antiguedad_ant, A.fecha_ingreso, B.sueldo_basico, 
                         A.cod_categoria, A.cod_cargo, A.tarea, A.nro_cta_sb, A.cod_clasif_per,
                         CP.des_clasif_per
                       FROM EMPLEADOS A WITH (NOLOCK)
                       JOIN CATEGORIAS B on A.cod_categoria=B.cod_categoria
                       LEFT JOIN CLASIFICACIONES_PERSONAL cp on
                          cp.cod_clasif_per=A.cod_clasif_Per   
                       WHERE A.fecha_baja IS NULL 
                       AND A.cod_tipo_liq=@cod_tipo_liq
                       AND (A.anio_ult_liq <= @anio OR A.anio_ult_liq IS NULL) 
                       AND (A.nro_ult_liq <=@nro_liquidacion OR A.nro_ult_liq IS NULL) 
                       ORDER BY A.legajo";
            try
            {
                cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@anio", anio);
                cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                cmd.CommandText = strSQL.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Transaction = trx;
                SqlDataReader dr = cmd.ExecuteReader();
                //
                if (dr.HasRows)
                {
                    int legajo = dr.GetOrdinal("legajo");
                    int antiguedad_ant = dr.GetOrdinal("antiguedad_ant");
                    int sueldo_basico = dr.GetOrdinal("sueldo_basico");
                    int fecha_ingreso = dr.GetOrdinal("fecha_ingreso");
                    int cod_categoria = dr.GetOrdinal("cod_categoria");
                    int cod_cargo = dr.GetOrdinal("cod_cargo");
                    int tarea = dr.GetOrdinal("tarea");
                    int nro_cta_sb = dr.GetOrdinal("nro_cta_sb");
                    int cod_clasif_per = dr.GetOrdinal("cod_clasif_per");
                    int des_clasif_per = dr.GetOrdinal("des_clasif_per");

                    while (dr.Read())
                    {
                        oEmp = new LstEmpleados();
                        if (!dr.IsDBNull(legajo)) oEmp.legajo = dr.GetInt32(legajo);
                        if (!dr.IsDBNull(antiguedad_ant))
                            oEmp.antiguedad_ant = dr.GetInt32(antiguedad_ant);
                        if (!dr.IsDBNull(fecha_ingreso)) oEmp.fecha_ingreso =
                                Convert.ToDateTime(dr.GetDateTime(fecha_ingreso), culturaFecArgentina).ToShortDateString();
                        //Convert.ToString(dr.GetString(fecha_ingreso);
                        if (!dr.IsDBNull(sueldo_basico)) oEmp.sueldo_basico = dr.GetDecimal(sueldo_basico);
                        if (!dr.IsDBNull(cod_categoria)) oEmp.cod_categoria = dr.GetInt32(cod_categoria);
                        if (!dr.IsDBNull(cod_cargo)) oEmp.cod_cargo = dr.GetInt32(cod_cargo);
                        if (!dr.IsDBNull(tarea)) oEmp.tarea = dr.GetString(tarea);
                        if (!dr.IsDBNull(nro_cta_sb)) oEmp.nro_cta_sb = dr.GetString(nro_cta_sb);
                        if (!dr.IsDBNull(cod_clasif_per)) oEmp.cod_clasif_per = dr.GetInt32(cod_clasif_per);
                        if (!dr.IsDBNull(des_clasif_per)) oEmp.des_clasif_per = dr.GetString(des_clasif_per);
                        lst.Add(oEmp);
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

        public static Entities.Empleado GetByPkTodos(int legajo)
        {
            Entities.Empleado objEmp = new Empleado();
            SqlCommand cmd;
            SqlDataReader dr;
            string strSQL;
            SqlConnection cn = DALBase.GetConnection("SIIMVA");
            DateTimeFormatInfo culturaFecArgentina = new CultureInfo("es-AR", false).DateTimeFormat;
            strSQL = @"SELECT * 
                        FROM EMPLEADOS (nolock)
                        WHERE legajo = @legajo";
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@legajo", legajo);
            try
            {
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd.Connection.Open();
                dr = cmd.ExecuteReader();
                //
                if (dr.HasRows)
                {
                    int imprime_recibo = dr.GetOrdinal("imprime_recibo");
                    int id_programa = dr.GetOrdinal("id_programa");
                    int id_revista = dr.GetOrdinal("id_revista");
                    int fecha_revista = dr.GetOrdinal("fecha_revista");
                    int activo = dr.GetOrdinal("activo");
                    while (dr.Read())
                    {
                        objEmp = new Empleado();

                        if (!dr.IsDBNull(dr.GetOrdinal("legajo")))
                            objEmp.legajo = dr.GetInt32(dr.GetOrdinal("legajo"));

                        if (!dr.IsDBNull(dr.GetOrdinal("nombre")))
                            objEmp.nombre = dr.GetString(dr.GetOrdinal("nombre"));

                        if (!dr.IsDBNull(dr.GetOrdinal("fecha_alta_registro")))
                            objEmp.fecha_alta_registro = Convert.ToDateTime(dr["fecha_alta_registro"]).ToString("dd/MM/yyyy");

                        if (!dr.IsDBNull(dr.GetOrdinal("cod_tipo_documento")))
                            objEmp.cod_tipo_documento = dr.GetInt32(dr.GetOrdinal("cod_tipo_documento"));

                        if (!dr.IsDBNull(dr.GetOrdinal("nro_documento")))
                            objEmp.nro_documento = dr.GetString(dr.GetOrdinal("nro_documento"));
                        if (!dr.IsDBNull(dr.GetOrdinal("cod_tipo_documento")))
                            objEmp.cod_tipo_documento = dr.GetInt32(dr.GetOrdinal("cod_tipo_documento"));

                        if (!dr.IsDBNull(dr.GetOrdinal("fecha_nacimiento")))
                            objEmp.fecha_nacimiento = Convert.ToDateTime(dr["fecha_nacimiento"]).ToString("dd/MM/yyyy");

                        if (!dr.IsDBNull(dr.GetOrdinal("pais_domicilio")))
                            objEmp.pais_domicilio = dr.GetString(dr.GetOrdinal("pais_domicilio"));

                        if (!dr.IsDBNull(dr.GetOrdinal("provincia_domicilio")))
                            objEmp.provincia_domicilio = dr.GetString(dr.GetOrdinal("provincia_domicilio"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ciudad_domicilio")))
                            objEmp.ciudad_domicilio = dr.GetString(dr.GetOrdinal("ciudad_domicilio"));

                        if (!dr.IsDBNull(dr.GetOrdinal("barrio_domicilio")))
                            objEmp.barrio_domicilio = dr.GetString(dr.GetOrdinal("barrio_domicilio"));

                        if (!dr.IsDBNull(dr.GetOrdinal("calle_domicilio")))
                            objEmp.calle_domicilio = dr.GetString(dr.GetOrdinal("calle_domicilio"));

                        if (!dr.IsDBNull(dr.GetOrdinal("nro_domicilio")))
                            objEmp.nro_domicilio = Convert.ToString(dr.GetInt32(dr.GetOrdinal("nro_domicilio")));

                        if (!dr.IsDBNull(dr.GetOrdinal("piso_domicilio")))
                            objEmp.piso_domicilio = dr.GetString(dr.GetOrdinal("piso_domicilio"));

                        if (!dr.IsDBNull(dr.GetOrdinal("dpto_domicilio")))
                            objEmp.dpto_domicilio = dr.GetString(dr.GetOrdinal("dpto_domicilio"));

                        if (!dr.IsDBNull(dr.GetOrdinal("monoblock_domicilio")))
                            objEmp.monoblock_domicilio = dr.GetString(dr.GetOrdinal("monoblock_domicilio"));

                        if (!dr.IsDBNull(dr.GetOrdinal("telefonos")))
                            objEmp.telefonos = dr.GetString(dr.GetOrdinal("telefonos"));

                        if (!dr.IsDBNull(dr.GetOrdinal("celular")))
                            objEmp.celular = dr.GetString(dr.GetOrdinal("celular"));

                        if (!dr.IsDBNull(dr.GetOrdinal("cod_postal")))
                            objEmp.cod_postal = dr.GetString(dr.GetOrdinal("cod_postal"));

                        if (!dr.IsDBNull(dr.GetOrdinal("cod_estado_civil")))
                            objEmp.cod_estado_civil = dr.GetInt32(dr.GetOrdinal("cod_estado_civil"));

                        if (!dr.IsDBNull(dr.GetOrdinal("sexo")))
                            objEmp.sexo = dr.GetString(dr.GetOrdinal("sexo"));

                        if (!dr.IsDBNull(dr.GetOrdinal("fecha_ingreso")))
                            objEmp.fecha_ingreso = Convert.ToDateTime(dr["fecha_ingreso"]).ToString("dd/MM/yyyy");

                        if (!dr.IsDBNull(dr.GetOrdinal("tarea")))
                            objEmp.tarea = dr.GetString(dr.GetOrdinal("tarea"));

                        if (!dr.IsDBNull(dr.GetOrdinal("cod_seccion")))
                            objEmp.cod_seccion = dr.GetInt32(dr.GetOrdinal("cod_seccion"));

                        if (!dr.IsDBNull(dr.GetOrdinal("cod_categoria")))
                            objEmp.cod_categoria = dr.GetInt32(dr.GetOrdinal("cod_categoria"));

                        if (!dr.IsDBNull(dr.GetOrdinal("cod_cargo")))
                            objEmp.cod_cargo = dr.GetInt32(dr.GetOrdinal("cod_cargo"));

                        if (!dr.IsDBNull(dr.GetOrdinal("cod_banco")))
                            objEmp.cod_banco = dr.GetInt32(dr.GetOrdinal("cod_banco"));

                        if (!dr.IsDBNull(dr.GetOrdinal("nro_sucursal")))
                            objEmp.nro_sucursal = dr.GetString(dr.GetOrdinal("nro_sucursal"));

                        if (!dr.IsDBNull(dr.GetOrdinal("tipo_cuenta")))
                            objEmp.tipo_cuenta = dr.GetString(dr.GetOrdinal("tipo_cuenta"));

                        if (!dr.IsDBNull(dr.GetOrdinal("nro_caja_ahorro")))
                            objEmp.nro_caja_ahorro = dr.GetString(dr.GetOrdinal("nro_caja_ahorro"));

                        if (!dr.IsDBNull(dr.GetOrdinal("nro_cbu")))
                            objEmp.nro_cbu = dr.GetString(dr.GetOrdinal("nro_cbu"));

                        if (!dr.IsDBNull(dr.GetOrdinal("nro_ipam")))
                            objEmp.nro_ipam = dr.GetString(dr.GetOrdinal("nro_ipam"));

                        if (!dr.IsDBNull(dr.GetOrdinal("cuil")))
                            objEmp.cuil = dr.GetString(dr.GetOrdinal("cuil"));

                        if (!dr.IsDBNull(dr.GetOrdinal("nro_jubilacion")))
                            objEmp.nro_jubilacion = dr.GetString(dr.GetOrdinal("nro_jubilacion"));

                        if (!dr.IsDBNull(dr.GetOrdinal("nro_jubilacion")))
                            objEmp.nro_jubilacion = dr.GetString(dr.GetOrdinal("nro_jubilacion"));

                        if (!dr.IsDBNull(dr.GetOrdinal("antiguedad_ant")))
                            objEmp.antiguedad_ant = dr.GetInt32(dr.GetOrdinal("antiguedad_ant"));

                        if (!dr.IsDBNull(dr.GetOrdinal("antiguedad_actual")))
                            objEmp.antiguedad_actual = dr.GetInt32(dr.GetOrdinal("antiguedad_actual"));

                        if (!dr.IsDBNull(dr.GetOrdinal("cod_clasif_per")))
                            objEmp.cod_clasif_per = dr.GetInt32(dr.GetOrdinal("cod_clasif_per"));

                        if (!dr.IsDBNull(dr.GetOrdinal("cod_tipo_liq")))
                            objEmp.cod_tipo_liq = dr.GetInt32(dr.GetOrdinal("cod_tipo_liq"));

                        if (!dr.IsDBNull(dr.GetOrdinal("nro_ult_liq")))
                            objEmp.nro_ult_liq = dr.GetInt32(dr.GetOrdinal("nro_ult_liq"));

                        if (!dr.IsDBNull(dr.GetOrdinal("anio_ult_liq")))
                            objEmp.anio_ult_liq = dr.GetInt32(dr.GetOrdinal("anio_ult_liq"));

                        if (!dr.IsDBNull(dr.GetOrdinal("nro_cta_sb")))
                            objEmp.nro_cta_sb = dr.GetString(dr.GetOrdinal("nro_cta_sb"));

                        if (!dr.IsDBNull(dr.GetOrdinal("nro_cta_gastos")))
                            objEmp.nro_cta_gastos = dr.GetString(dr.GetOrdinal("nro_cta_gastos"));

                        if (!dr.IsDBNull(dr.GetOrdinal("fecha_baja")))
                            objEmp.fecha_baja = Convert.ToDateTime(dr["fecha_baja"]).ToString("dd/MM/yyyy");

                        if (!dr.IsDBNull(dr.GetOrdinal("nro_contrato")))
                            objEmp.nro_contrato = dr.GetInt32(dr.GetOrdinal("nro_contrato"));

                        if (!dr.IsDBNull(dr.GetOrdinal("fecha_inicio_contrato")))
                            objEmp.fecha_inicio_contrato = Convert.ToDateTime((dr["fecha_inicio_contrato"])).ToString("dd/MM/yyyy");

                        if (!dr.IsDBNull(dr.GetOrdinal("fecha_fin_contrato")))
                            objEmp.fecha_fin_contrato = Convert.ToDateTime((dr["fecha_fin_contrato"])).ToString("dd/MM/yyyy");

                        if (!dr.IsDBNull(dr.GetOrdinal("id_regimen")))
                            objEmp.id_regimen = dr.GetInt16(dr.GetOrdinal("id_regimen"));

                        if (!dr.IsDBNull(dr.GetOrdinal("id_secretaria")))
                            objEmp.id_secretaria = dr.GetInt32(dr.GetOrdinal("id_secretaria"));

                        if (!dr.IsDBNull(dr.GetOrdinal("id_direccion")))
                            objEmp.id_direccion = dr.GetInt32(dr.GetOrdinal("id_direccion"));

                        if (!dr.IsDBNull(dr.GetOrdinal("nro_nombramiento")))
                            objEmp.nro_nombramiento = dr.GetString(dr.GetOrdinal("nro_nombramiento"));

                        if (!dr.IsDBNull(dr.GetOrdinal("fecha_nombramiento")))
                            objEmp.fecha_nombramiento = Convert.ToDateTime(dr["fecha_nombramiento"]).ToString("dd/MM/yyyy"); ;

                        if (!dr.IsDBNull(dr.GetOrdinal("usuario")))
                            objEmp.usuario = dr.GetString(dr.GetOrdinal("usuario"));

                        if (!dr.IsDBNull(dr.GetOrdinal("cod_escala_aumento")))
                            objEmp.cod_escala_aumento = dr.GetInt32(dr.GetOrdinal("cod_escala_aumento"));

                        if (!dr.IsDBNull(dr.GetOrdinal("cod_regimen_empleado")))
                            objEmp.cod_regimen_empleado = dr.GetInt32(dr.GetOrdinal("cod_regimen_empleado"));

                        if (!dr.IsDBNull(dr.GetOrdinal("id_oficina")))
                            objEmp.id_oficina = dr.GetInt32(dr.GetOrdinal("id_oficina"));

                        if (!dr.IsDBNull(dr.GetOrdinal("email")))
                            objEmp.email = dr.GetString(dr.GetOrdinal("email"));

                        if (!dr.IsDBNull(imprime_recibo)) objEmp.imprime_recibo = dr.GetInt16(imprime_recibo);
                        if (!dr.IsDBNull(id_programa)) objEmp.id_programa = dr.GetInt32(id_programa);
                        if (!dr.IsDBNull(id_revista)) objEmp.id_revista = dr.GetInt32(id_revista);
                        if (!dr.IsDBNull(fecha_revista))
                            objEmp.fecha_revista = Convert.ToDateTime(dr["fecha_revista"], culturaFecArgentina).ToString("dd/MM/yyyy");
                        if (!dr.IsDBNull(activo)) objEmp.activo = dr.GetBoolean(activo);
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            dr.Close();
            return objEmp;
        }

        public static Entities.Empleado GetByPk(int legajo, SqlConnection cn, SqlTransaction trx)
        {
            Entities.Empleado objEmp = new Empleado();
            SqlCommand cmd;
            SqlDataReader dr;
            string strSQL; ;
            //SqlConnection cn = DALBase.GetConnection("SIIMVA");
            strSQL = @"SELECT *
                        FROM EMPLEADOS (nolock) 
                        WHERE legajo = @legajo";
            DateTimeFormatInfo culturaFecArgentina = new CultureInfo("es-AR", false).DateTimeFormat;
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@legajo", legajo);
            try
            {
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                //cmd.Connection.Open();
                cmd.Transaction = trx;
                dr = cmd.ExecuteReader();
                //
                if (dr.HasRows)
                {
                    int imprime_recibo = dr.GetOrdinal("imprime_recibo");
                    int id_programa = dr.GetOrdinal("id_programa");
                    int id_revista = dr.GetOrdinal("id_revista");
                    int fecha_revista = dr.GetOrdinal("fecha_revista");
                    int activo = dr.GetOrdinal("activo");
                    while (dr.Read())
                    {
                        objEmp = new Empleado();

                        if (!dr.IsDBNull(dr.GetOrdinal("legajo")))
                            objEmp.legajo = dr.GetInt32(dr.GetOrdinal("legajo"));

                        if (!dr.IsDBNull(dr.GetOrdinal("nombre")))
                            objEmp.nombre = dr.GetString(dr.GetOrdinal("nombre"));

                        if (!dr.IsDBNull(dr.GetOrdinal("fecha_alta_registro")))
                            objEmp.fecha_alta_registro = Convert.ToDateTime(dr["fecha_alta_registro"], culturaFecArgentina).ToString("dd/MM/yyyy");

                        if (!dr.IsDBNull(dr.GetOrdinal("cod_tipo_documento")))
                            objEmp.cod_tipo_documento = dr.GetInt32(dr.GetOrdinal("cod_tipo_documento"));

                        if (!dr.IsDBNull(dr.GetOrdinal("nro_documento")))
                            objEmp.nro_documento = dr.GetString(dr.GetOrdinal("nro_documento"));
                        if (!dr.IsDBNull(dr.GetOrdinal("cod_tipo_documento")))
                            objEmp.cod_tipo_documento = dr.GetInt32(dr.GetOrdinal("cod_tipo_documento"));

                        if (!dr.IsDBNull(dr.GetOrdinal("fecha_nacimiento")))
                            objEmp.fecha_nacimiento = Convert.ToDateTime(dr["fecha_nacimiento"], culturaFecArgentina).ToString("dd/MM/yyyy");

                        if (!dr.IsDBNull(dr.GetOrdinal("pais_domicilio")))
                            objEmp.pais_domicilio = dr.GetString(dr.GetOrdinal("pais_domicilio"));

                        if (!dr.IsDBNull(dr.GetOrdinal("provincia_domicilio")))
                            objEmp.provincia_domicilio = dr.GetString(dr.GetOrdinal("provincia_domicilio"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ciudad_domicilio")))
                            objEmp.ciudad_domicilio = dr.GetString(dr.GetOrdinal("ciudad_domicilio"));

                        if (!dr.IsDBNull(dr.GetOrdinal("barrio_domicilio")))
                            objEmp.barrio_domicilio = dr.GetString(dr.GetOrdinal("barrio_domicilio"));

                        if (!dr.IsDBNull(dr.GetOrdinal("calle_domicilio")))
                            objEmp.calle_domicilio = dr.GetString(dr.GetOrdinal("calle_domicilio"));

                        if (!dr.IsDBNull(dr.GetOrdinal("nro_domicilio")))
                            objEmp.nro_domicilio = Convert.ToString(dr.GetInt32(dr.GetOrdinal("nro_domicilio")));

                        if (!dr.IsDBNull(dr.GetOrdinal("piso_domicilio")))
                            objEmp.piso_domicilio = dr.GetString(dr.GetOrdinal("piso_domicilio"));

                        if (!dr.IsDBNull(dr.GetOrdinal("dpto_domicilio")))
                            objEmp.dpto_domicilio = dr.GetString(dr.GetOrdinal("dpto_domicilio"));

                        if (!dr.IsDBNull(dr.GetOrdinal("monoblock_domicilio")))
                            objEmp.monoblock_domicilio = dr.GetString(dr.GetOrdinal("monoblock_domicilio"));

                        if (!dr.IsDBNull(dr.GetOrdinal("telefonos")))
                            objEmp.telefonos = dr.GetString(dr.GetOrdinal("telefonos"));

                        if (!dr.IsDBNull(dr.GetOrdinal("celular")))
                            objEmp.celular = dr.GetString(dr.GetOrdinal("celular"));

                        if (!dr.IsDBNull(dr.GetOrdinal("cod_postal")))
                            objEmp.cod_postal = dr.GetString(dr.GetOrdinal("cod_postal"));

                        if (!dr.IsDBNull(dr.GetOrdinal("cod_estado_civil")))
                            objEmp.cod_estado_civil = dr.GetInt32(dr.GetOrdinal("cod_estado_civil"));

                        if (!dr.IsDBNull(dr.GetOrdinal("sexo")))
                            objEmp.sexo = dr.GetString(dr.GetOrdinal("sexo"));

                        if (!dr.IsDBNull(dr.GetOrdinal("fecha_ingreso")))
                            objEmp.fecha_ingreso = Convert.ToDateTime(dr["fecha_ingreso"], culturaFecArgentina).ToString("dd/MM/yyyy");

                        if (!dr.IsDBNull(dr.GetOrdinal("tarea")))
                            objEmp.tarea = dr.GetString(dr.GetOrdinal("tarea"));

                        if (!dr.IsDBNull(dr.GetOrdinal("cod_seccion")))
                            objEmp.cod_seccion = dr.GetInt32(dr.GetOrdinal("cod_seccion"));

                        if (!dr.IsDBNull(dr.GetOrdinal("cod_categoria")))
                            objEmp.cod_categoria = dr.GetInt32(dr.GetOrdinal("cod_categoria"));

                        if (!dr.IsDBNull(dr.GetOrdinal("cod_cargo")))
                            objEmp.cod_cargo = dr.GetInt32(dr.GetOrdinal("cod_cargo"));

                        if (!dr.IsDBNull(dr.GetOrdinal("cod_banco")))
                            objEmp.cod_banco = dr.GetInt32(dr.GetOrdinal("cod_banco"));

                        if (!dr.IsDBNull(dr.GetOrdinal("nro_sucursal")))
                            objEmp.nro_sucursal = dr.GetString(dr.GetOrdinal("nro_sucursal"));

                        if (!dr.IsDBNull(dr.GetOrdinal("tipo_cuenta")))
                            objEmp.tipo_cuenta = dr.GetString(dr.GetOrdinal("tipo_cuenta"));

                        if (!dr.IsDBNull(dr.GetOrdinal("nro_caja_ahorro")))
                            objEmp.nro_caja_ahorro = dr.GetString(dr.GetOrdinal("nro_caja_ahorro"));

                        if (!dr.IsDBNull(dr.GetOrdinal("nro_cbu")))
                            objEmp.nro_cbu = dr.GetString(dr.GetOrdinal("nro_cbu"));

                        if (!dr.IsDBNull(dr.GetOrdinal("nro_ipam")))
                            objEmp.nro_ipam = dr.GetString(dr.GetOrdinal("nro_ipam"));

                        if (!dr.IsDBNull(dr.GetOrdinal("cuil")))
                            objEmp.cuil = dr.GetString(dr.GetOrdinal("cuil"));

                        //if (!dr.IsDBNull(dr.GetOrdinal("nro_jubilacion")))
                        //  objEmp.nro_jubilacion = dr.GetString((dr.GetOrdinal("nro_jubilacion"));

                        if (!dr.IsDBNull(dr.GetOrdinal("nro_jubilacion")))
                            objEmp.nro_jubilacion = dr.GetString(dr.GetOrdinal("nro_jubilacion"));

                        if (!dr.IsDBNull(dr.GetOrdinal("antiguedad_ant")))
                            objEmp.antiguedad_ant = dr.GetInt32(dr.GetOrdinal("antiguedad_ant"));

                        if (!dr.IsDBNull(dr.GetOrdinal("antiguedad_actual")))
                            objEmp.antiguedad_actual = dr.GetInt32(dr.GetOrdinal("antiguedad_actual"));

                        if (!dr.IsDBNull(dr.GetOrdinal("cod_clasif_per")))
                            objEmp.cod_clasif_per = dr.GetInt32(dr.GetOrdinal("cod_clasif_per"));

                        if (!dr.IsDBNull(dr.GetOrdinal("cod_tipo_liq")))
                            objEmp.cod_tipo_liq = dr.GetInt32(dr.GetOrdinal("cod_tipo_liq"));

                        if (!dr.IsDBNull(dr.GetOrdinal("nro_ult_liq")))
                            objEmp.nro_ult_liq = dr.GetInt32(dr.GetOrdinal("nro_ult_liq"));

                        if (!dr.IsDBNull(dr.GetOrdinal("anio_ult_liq")))
                            objEmp.anio_ult_liq = dr.GetInt32(dr.GetOrdinal("anio_ult_liq"));

                        if (!dr.IsDBNull(dr.GetOrdinal("nro_cta_sb")))
                            objEmp.nro_cta_sb = dr.GetString(dr.GetOrdinal("nro_cta_sb"));

                        if (!dr.IsDBNull(dr.GetOrdinal("nro_cta_gastos")))
                            objEmp.nro_cta_gastos = dr.GetString(dr.GetOrdinal("nro_cta_gastos"));

                        if (!dr.IsDBNull(dr.GetOrdinal("fecha_baja")))
                            objEmp.fecha_baja = Convert.ToDateTime(dr["fecha_baja"], culturaFecArgentina).ToString("dd/MM/yyyy");

                        if (!dr.IsDBNull(dr.GetOrdinal("nro_contrato")))
                            objEmp.nro_contrato = dr.GetInt32(dr.GetOrdinal("nro_contrato"));

                        if (!dr.IsDBNull(dr.GetOrdinal("fecha_inicio_contrato")))
                            objEmp.fecha_inicio_contrato = Convert.ToDateTime((dr["fecha_inicio_contrato"]), culturaFecArgentina).ToString("dd/MM/yyyy");

                        if (!dr.IsDBNull(dr.GetOrdinal("fecha_fin_contrato")))
                            objEmp.fecha_fin_contrato = Convert.ToDateTime((dr["fecha_fin_contrato"]), culturaFecArgentina).ToString("dd/MM/yyyy");

                        if (!dr.IsDBNull(dr.GetOrdinal("id_regimen")))
                            objEmp.id_regimen = dr.GetInt16(dr.GetOrdinal("id_regimen"));

                        if (!dr.IsDBNull(dr.GetOrdinal("id_secretaria")))
                            objEmp.id_secretaria = dr.GetInt32(dr.GetOrdinal("id_secretaria"));

                        if (!dr.IsDBNull(dr.GetOrdinal("id_direccion")))
                            objEmp.id_direccion = dr.GetInt32(dr.GetOrdinal("id_direccion"));

                        if (!dr.IsDBNull(dr.GetOrdinal("nro_nombramiento")))
                            objEmp.nro_nombramiento = dr.GetString(dr.GetOrdinal("nro_nombramiento"));

                        if (!dr.IsDBNull(dr.GetOrdinal("fecha_nombramiento")))
                            objEmp.fecha_nombramiento = Convert.ToDateTime(dr["fecha_nombramiento"], culturaFecArgentina).ToString("dd/MM/yyyy");

                        if (!dr.IsDBNull(dr.GetOrdinal("usuario")))
                            objEmp.usuario = dr.GetString(dr.GetOrdinal("usuario"));

                        if (!dr.IsDBNull(dr.GetOrdinal("cod_escala_aumento")))
                            objEmp.cod_escala_aumento = dr.GetInt32(dr.GetOrdinal("cod_escala_aumento"));

                        if (!dr.IsDBNull(dr.GetOrdinal("cod_regimen_empleado")))
                            objEmp.cod_regimen_empleado = dr.GetInt32(dr.GetOrdinal("cod_regimen_empleado"));

                        if (!dr.IsDBNull(dr.GetOrdinal("id_oficina")))
                            objEmp.id_oficina = dr.GetInt32(dr.GetOrdinal("id_oficina"));

                        if (!dr.IsDBNull(dr.GetOrdinal("email")))
                            objEmp.email = dr.GetString(dr.GetOrdinal("email"));

                        if (!dr.IsDBNull(imprime_recibo)) objEmp.imprime_recibo = dr.GetInt16(imprime_recibo);
                        if (!dr.IsDBNull(id_programa)) objEmp.id_programa = dr.GetInt32(id_programa);
                        if (!dr.IsDBNull(id_revista)) objEmp.id_revista = dr.GetInt32(id_revista);
                        if (!dr.IsDBNull(fecha_revista))
                            objEmp.fecha_revista = Convert.ToDateTime(dr["fecha_revista"], culturaFecArgentina).ToString("dd/MM/yyyy");
                        if (!dr.IsDBNull(activo)) objEmp.activo = dr.GetBoolean(activo);
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

            dr.Close();
            return objEmp;

        }

        public static Int32 InsertDatosEmpleado(Entities.Empleado oEmp, SqlConnection cn, SqlTransaction trx)
        {
            SqlCommand objCommand = null;
            SqlCommand cmdInsert = null;
            try
            {
                StringBuilder strSQL = new StringBuilder();
                if (oEmp.legajo == 0)
                {
                    string SQL = "SELECT isnull(max(legajo),0) FROM EMPLEADOS";
                    objCommand = new SqlCommand();
                    objCommand.Connection = cn;
                    objCommand.CommandType = CommandType.Text;
                    objCommand.Transaction = trx;
                    objCommand.CommandText = SQL;
                    oEmp.legajo = Convert.ToInt32(objCommand.ExecuteScalar()) + 1;
                }
                strSQL.AppendLine("INSERT INTO EMPLEADOS ");
                strSQL.AppendLine("(legajo,");
                strSQL.AppendLine("nombre,");
                strSQL.AppendLine("fecha_alta_registro,");
                strSQL.AppendLine("fecha_ingreso,");
                strSQL.AppendLine("cod_tipo_documento,");
                strSQL.AppendLine("nro_documento,");
                strSQL.AppendLine("cuil,");
                strSQL.AppendLine("tarea,");
                strSQL.AppendLine("cod_categoria,");
                strSQL.AppendLine("cod_cargo,");
                strSQL.AppendLine("cod_seccion,");
                strSQL.AppendLine("cod_clasif_per,");
                strSQL.AppendLine("cod_tipo_liq,");
                strSQL.AppendLine("id_secretaria,");
                strSQL.AppendLine("id_direccion,");
                strSQL.AppendLine("id_oficina,");
                strSQL.AppendLine("id_regimen,");
                strSQL.AppendLine("cod_regimen_empleado,");
                strSQL.AppendLine("cod_escala_aumento, listar,");
                strSQL.AppendLine("imprime_recibo, id_programa, nro_cta_sb,");
                strSQL.AppendLine("nro_cta_gastos, id_revista, fecha_revista, activo) ");
                //Asigno Valores 
                strSQL.AppendLine("VALUES ");
                strSQL.AppendLine("(@legajo,");
                strSQL.AppendLine("@nombre,");
                strSQL.AppendLine("@fecha_alta_registro,");
                strSQL.AppendLine("@fecha_ingreso,");
                strSQL.AppendLine("@cod_tipo_documento,");
                strSQL.AppendLine("@nro_documento,");
                strSQL.AppendLine("@cuil,");
                strSQL.AppendLine("@tarea,");
                strSQL.AppendLine("@cod_cargo,");
                strSQL.AppendLine("@cod_categoria,");
                strSQL.AppendLine("@cod_seccion,");
                strSQL.AppendLine("@cod_clasif_per,");
                strSQL.AppendLine("@cod_tipo_liq,");
                strSQL.AppendLine("@id_secretaria,");
                strSQL.AppendLine("@id_direccion,");
                strSQL.AppendLine("@id_oficina,");
                strSQL.AppendLine("@id_regimen,");
                strSQL.AppendLine("@cod_regimen_empleado,");
                strSQL.AppendLine("@cod_escala_aumento, @listar,");
                strSQL.AppendLine("@imprime_recibo, @id_programa, @nro_cta_sb,");
                strSQL.AppendLine("@nro_cta_gastos, @id_revista,");
                strSQL.AppendLine("@fecha_revista, @activo)");
                cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.CommandType = CommandType.Text;
                cmdInsert.Transaction = trx;
                cmdInsert.CommandText = strSQL.ToString();

                cmdInsert.Parameters.AddWithValue("@legajo", oEmp.legajo);
                cmdInsert.Parameters.AddWithValue("@nombre", oEmp.nombre);
                cmdInsert.Parameters.AddWithValue("@fecha_alta_registro", oEmp.fecha_alta_registro != null ? oEmp.fecha_alta_registro : null);
                cmdInsert.Parameters.AddWithValue("@fecha_ingreso", oEmp.fecha_ingreso);
                cmdInsert.Parameters.AddWithValue("@cod_tipo_documento", oEmp.cod_tipo_documento > 0 ? oEmp.cod_tipo_documento : 0);
                cmdInsert.Parameters.AddWithValue("@nro_documento", oEmp.nro_documento != null ? oEmp.nro_documento : null);
                cmdInsert.Parameters.AddWithValue("@cuil", oEmp.cuil != null ? oEmp.cuil : null);
                cmdInsert.Parameters.AddWithValue("@tarea", oEmp.tarea != null ? oEmp.tarea : null);
                cmdInsert.Parameters.AddWithValue("@cod_categoria", oEmp.cod_categoria > 0 ? oEmp.cod_categoria : 0);
                cmdInsert.Parameters.AddWithValue("@cod_cargo", oEmp.cod_cargo > 0 ? oEmp.cod_cargo : 0);
                cmdInsert.Parameters.AddWithValue("@cod_seccion", oEmp.cod_seccion > 0 ? oEmp.cod_seccion : 0);
                cmdInsert.Parameters.AddWithValue("@cod_clasif_per", oEmp.cod_clasif_per > 0 ? oEmp.cod_clasif_per : 0);
                cmdInsert.Parameters.AddWithValue("@cod_tipo_liq", oEmp.cod_tipo_liq > 0 ? oEmp.cod_tipo_liq : 0);
                cmdInsert.Parameters.AddWithValue("@id_secretaria", oEmp.id_secretaria > 0 ? oEmp.id_secretaria : 0);
                cmdInsert.Parameters.AddWithValue("@id_direccion", oEmp.id_direccion > 0 ? oEmp.id_direccion : 0);
                cmdInsert.Parameters.AddWithValue("@id_oficina", oEmp.id_oficina > 0 ? oEmp.id_oficina : 0);
                cmdInsert.Parameters.AddWithValue("@id_regimen", oEmp.id_regimen > 0 ? oEmp.id_regimen : 0);
                cmdInsert.Parameters.AddWithValue("@cod_regimen_empleado", oEmp.cod_regimen_empleado > 0 ? oEmp.cod_regimen_empleado : 0);
                cmdInsert.Parameters.AddWithValue("@cod_escala_aumento", oEmp.cod_escala_aumento > 0 ? oEmp.cod_escala_aumento : 0);
                cmdInsert.Parameters.AddWithValue("@listar", 1);
                cmdInsert.Parameters.AddWithValue("@imprime_recibo", oEmp.imprime_recibo);
                cmdInsert.Parameters.AddWithValue("@id_programa", oEmp.id_programa);
                cmdInsert.Parameters.AddWithValue("@nro_cta_sb", GetNro_cta_sb(oEmp.cod_cargo));
                cmdInsert.Parameters.AddWithValue("@nro_cta_gastos", oEmp.nro_cta_gastos != null ? oEmp.nro_cta_gastos : null);
                cmdInsert.Parameters.AddWithValue("@id_revista", oEmp.id_revista);
                cmdInsert.Parameters.AddWithValue("@fecha_revista", oEmp.fecha_revista != null ? oEmp.fecha_revista : null);
                cmdInsert.Parameters.AddWithValue("@activo", oEmp.activo);
                cmdInsert.ExecuteNonQuery();
                Inicializar_Conceptos_Fijos(oEmp, oEmp.usuario, cn, trx);
                //insertDetalle(op, objCommand);
                //insertAuditoria(op, objCommand, 0);
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmdInsert = null;
                objCommand = null;
            }
            return oEmp.legajo;
        }

        private static void Inicializar_Conceptos_Fijos(Entities.Empleado oEmp, string usuario, SqlConnection cn, SqlTransaction trx)
        {
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = trx;
                String strSQL = "";
                strSQL = @"INSERT INTO CONCEP_LIQUID_X_EMPLEADO
                         (legajo, cod_concepto_liq, fecha_alta_registro, valor_concepto_liq, fecha_vto, usuario)
                         VALUES
                         (@legajo, @cod_concepto_liq, @fecha_alta_registro, @valor_concepto_liq, @fecha_vto, @usuario)";
                cmd.CommandText = strSQL.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@legajo", 0);
                cmd.Parameters.AddWithValue("@cod_concepto_liq", 0);
                cmd.Parameters.AddWithValue("@fecha_alta_registro", DateTime.Today);
                cmd.Parameters.AddWithValue("@valor_concepto_liq", 0);
                cmd.Parameters.AddWithValue("@fecha_vto", DateTime.Today);
                cmd.Parameters.AddWithValue("@usuario", "");
                string codigos = "607,608,620,695";
                char[] delimit = new char[] { ',' };
                foreach (string item in codigos.Split(delimit))
                {
                    cmd.Parameters["@legajo"].Value = oEmp.legajo;
                    cmd.Parameters["@cod_concepto_liq"].Value = item;
                    cmd.Parameters["@fecha_alta_registro"].Value = DateTime.Today;
                    cmd.Parameters["@valor_concepto_liq"].Value = 1;
                    cmd.Parameters["@fecha_vto"].Value = DateTime.Today;
                    cmd.Parameters["@usuario"].Value = oEmp.usuario;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmd = null;
            }
        }

        public static Int32 UpdateDatosEmpleado(Entities.Empleado oEmp, string usuario,
            int id_tipo_auditoria, string des_tipo_auditoria, string obsauditoria, SqlConnection cn, SqlTransaction trx)
        {
            SqlCommand objCommand = null;
            SqlCommand objCategoria = null;
            int cod_categoria_ant = 0;
            //SqlConnection objConn = DALBase.GetConnection("SIIMVA");
            try
            {
                StringBuilder strSQL = new StringBuilder();
                StringBuilder strSQL1 = new StringBuilder();
                //objConn.Open();
                //Averiguo la Categoria del empleado para
                //guardarla como historial si hubo cambio de categoria
                string SQL = "";
                SQL = @"SELECT max(cod_categoria)
                        FROM Empleados where legajo=" + oEmp.legajo.ToString();
                objCategoria = new SqlCommand();
                objCategoria.Connection = cn;
                objCategoria.CommandType = CommandType.Text;
                objCategoria.CommandText = SQL;
                objCategoria.Transaction = trx;
                cod_categoria_ant = Convert.ToInt32(objCategoria.ExecuteScalar());
                //
                strSQL.AppendLine("UPDATE EMPLEADOS SET ");
                strSQL.AppendLine("nombre=@nombre,");
                strSQL.AppendLine("fecha_alta_registro=@fecha_alta_registro,");
                if (oEmp.fecha_ingreso.Length != 0)
                    strSQL.AppendLine("fecha_ingreso=@fecha_ingreso,");
                else
                    strSQL.AppendLine("fecha_ingreso=null,");
                strSQL.AppendLine("cod_tipo_documento=@cod_tipo_documento,");
                strSQL.AppendLine("nro_documento=@nro_documento,");
                strSQL.AppendLine("cuil=@cuil,");
                strSQL.AppendLine("tarea=@tarea,");
                strSQL.AppendLine("cod_categoria=@cod_categoria,");
                strSQL.AppendLine("cod_cargo=@cod_cargo,");
                strSQL.AppendLine("nro_cta_sb=@nro_cta_sb,");
                strSQL.AppendLine("nro_cta_gastos= @nro_cta_gastos,");
                strSQL.AppendLine("cod_seccion=@cod_seccion,");
                strSQL.AppendLine("cod_clasif_per=@cod_clasif_per,");
                strSQL.AppendLine("cod_tipo_liq=@cod_tipo_liq,");
                strSQL.AppendLine("id_secretaria=@id_secretaria,");
                strSQL.AppendLine("id_direccion=@id_direccion,");
                strSQL.AppendLine("id_oficina=@id_oficina,");
                strSQL.AppendLine("id_regimen=@id_regimen,");
                strSQL.AppendLine("cod_escala_aumento=@cod_escala_aumento ");
                if (oEmp.fecha_baja.Length != 0)
                    strSQL.AppendLine(",fecha_baja=@fecha_baja");
                else
                    strSQL.AppendLine(",fecha_baja=null");
                strSQL.AppendLine(",cod_regimen_empleado=@cod_regimen_empleado");
                strSQL.AppendLine(",imprime_recibo=@imprime_recibo");
                strSQL.AppendLine(",id_programa=@id_programa");
                strSQL.AppendLine(",id_revista=@id_revista");
                if (oEmp.fecha_revista.Length != 0)
                    strSQL.AppendLine(",fecha_revista=@fecha_revista");
                else
                    strSQL.AppendLine(",fecha_revista=null");
                strSQL.AppendLine(",activo=@activo");
                strSQL.AppendLine(" WHERE legajo=@legajo");
                //Asigno Valores 
                objCommand = new SqlCommand();
                objCommand.Connection = cn;
                objCommand.CommandType = CommandType.Text;
                objCommand.CommandText = strSQL.ToString();
                objCommand.Transaction = trx;
                objCommand.Parameters.AddWithValue("@legajo", oEmp.legajo);
                objCommand.Parameters.AddWithValue("@nombre", oEmp.nombre.Trim());
                objCommand.Parameters.AddWithValue("@fecha_alta_registro", oEmp.fecha_alta_registro.Length != 0 ? oEmp.fecha_alta_registro : "");
                if (oEmp.fecha_ingreso.Length != 0)
                    objCommand.Parameters.AddWithValue("@fecha_ingreso", oEmp.fecha_ingreso);
                objCommand.Parameters.AddWithValue("@cod_tipo_documento", oEmp.cod_tipo_documento > 0 ? oEmp.cod_tipo_documento : 0);
                objCommand.Parameters.AddWithValue("@nro_documento", oEmp.nro_documento != null ? oEmp.nro_documento : null);
                objCommand.Parameters.AddWithValue("@cuil", oEmp.cuil != null ? oEmp.cuil : null);
                objCommand.Parameters.AddWithValue("@tarea", oEmp.tarea.Trim() != null ? oEmp.tarea.Trim() : null);
                objCommand.Parameters.AddWithValue("@cod_cargo", oEmp.cod_cargo > 0 ? oEmp.cod_cargo : 0);
                objCommand.Parameters.AddWithValue("@cod_categoria", oEmp.cod_categoria > 0 ? oEmp.cod_categoria : 0);
                objCommand.Parameters.AddWithValue("@nro_cta_sb", oEmp.cod_cargo > 0 ? GetNro_cta_sb(oEmp.cod_cargo) : "");
                objCommand.Parameters.AddWithValue("@nro_cta_gastos", oEmp.nro_cta_gastos.Trim() != null ? oEmp.nro_cta_gastos.Trim() : null);
                objCommand.Parameters.AddWithValue("@cod_seccion", oEmp.cod_seccion > 0 ? oEmp.cod_seccion : 0);
                objCommand.Parameters.AddWithValue("@cod_clasif_per", oEmp.cod_clasif_per > 0 ? oEmp.cod_clasif_per : 0);
                objCommand.Parameters.AddWithValue("@cod_tipo_liq", oEmp.cod_tipo_liq > 0 ? oEmp.cod_tipo_liq : 0);
                objCommand.Parameters.AddWithValue("@id_secretaria", oEmp.id_secretaria > 0 ? oEmp.id_secretaria : 0);
                objCommand.Parameters.AddWithValue("@id_direccion", oEmp.id_direccion > 0 ? oEmp.id_direccion : 0);
                objCommand.Parameters.AddWithValue("@id_oficina", oEmp.id_oficina > 0 ? oEmp.id_oficina : 0);
                objCommand.Parameters.AddWithValue("@id_regimen", oEmp.id_regimen > 0 ? oEmp.id_regimen : 0);
                objCommand.Parameters.AddWithValue("@cod_escala_aumento", oEmp.cod_escala_aumento > 0 ? oEmp.cod_escala_aumento : 0);
                objCommand.Parameters.AddWithValue("@cod_regimen_empleado", oEmp.cod_regimen_empleado > 0 ? oEmp.cod_regimen_empleado : 0);
                if (oEmp.fecha_baja.Length != 0)
                    objCommand.Parameters.AddWithValue("@fecha_baja", oEmp.fecha_baja);
                objCommand.Parameters.AddWithValue("@imprime_recibo", oEmp.imprime_recibo);
                objCommand.Parameters.AddWithValue("@id_programa", oEmp.id_programa);
                objCommand.Parameters.AddWithValue("@id_revista", oEmp.id_revista);
                if (oEmp.fecha_revista.Length != 0)
                    objCommand.Parameters.AddWithValue("@fecha_revista", oEmp.fecha_revista);
                objCommand.Parameters.AddWithValue("@activo", oEmp.activo);
                //Antes de Actualizar
                //Guardo el estado anterior del Legajo
                Insert_cambios_empleados(oEmp.legajo, usuario, id_tipo_auditoria, "MODIFICA DATOS DEL EMPLEADO", obsauditoria, cn, trx);
                //
                objCommand.ExecuteNonQuery();
                if (cod_categoria_ant != oEmp.cod_categoria)
                {
                    Cambios_categoria_empleado(oEmp.legajo, cod_categoria_ant, usuario, "antes", "", cn, trx);
                    Cambios_categoria_empleado(oEmp.legajo, oEmp.cod_categoria, usuario, "nueva", "", cn, trx);
                }
                //insertDetalle(op, objCommand);
                //insertAuditoria(op, objCommand, 0);
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                objCommand = null;
                objCategoria = null;
            }
            return oEmp.legajo;
        }

        public static Int32 UpdateTab_Datos_Contrato(Entities.Empleado oEmp, string usuario, int id_tipo_auditoria, string des_tipo_auditoria, string obsauditoria,
            SqlConnection cn, SqlTransaction trx)
        {
            SqlCommand cmdInsert = null;
            string nro_cta = oEmp.nro_cta_sb;
            string sql = "";
            try
            {
                StringBuilder strSQL = new StringBuilder();
                StringBuilder strSQL1 = new StringBuilder();
                cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.Transaction = trx;
                //objConn.Open();

                strSQL.AppendLine("UPDATE EMPLEADOS set ");
                //strSQL.AppendLine("nro_cta_sb=@nro_cta_sb,");
                //strSQL.AppendLine("nro_cta_gastos=@nro_cta_gastos,");
                strSQL.AppendLine("nro_ipam=@nro_ipam,");
                strSQL.AppendLine("nro_jubilacion=@nro_jubilacion,");
                strSQL.AppendLine("antiguedad_ant=@antiguedad_ant,");
                strSQL.AppendLine("antiguedad_actual=@antiguedad_actual,");
                strSQL.AppendLine("nro_contrato=@nro_contrato,");

                if (oEmp.fecha_inicio_contrato.Length > 0)
                    strSQL.AppendLine("fecha_inicio_contrato=@fecha_inicio_contrato,");
                else
                    strSQL.AppendLine("fecha_inicio_contrato=null,");

                if (oEmp.fecha_fin_contrato.Length > 0)
                    strSQL.AppendLine("fecha_fin_contrato=@fecha_fin_contrato,");
                else
                    strSQL.AppendLine("fecha_fin_contrato=null,");

                if (oEmp.nro_nombramiento.Length > 0)
                    strSQL.AppendLine("nro_nombramiento=@nro_nombramiento,");
                else
                    strSQL.AppendLine("nro_nombramiento=null,");

                if (oEmp.fecha_nombramiento.Length > 0)
                    strSQL.AppendLine("fecha_nombramiento=@fecha_nombramiento,");
                else
                    strSQL.AppendLine("fecha_nombramiento=null,");

                char[] MyChar = { ',' };
                sql = strSQL.ToString();
                strSQL1.AppendLine(sql.Remove(sql.Trim().Length - 1, 1));
                //cmdInsert.Parameters.AddWithValue("@nro_cta_sb", oEmp.nro_cta_sb.Length > 0 ? oEmp.nro_cta_sb : "0");
                //cmdInsert.Parameters.AddWithValue("@nro_cta_gastos", oEmp.nro_cta_gastos.Length > 0 ? oEmp.nro_cta_gastos : "0");
                cmdInsert.Parameters.AddWithValue("@nro_ipam", oEmp.nro_ipam.Length > 0 ? oEmp.nro_ipam.Trim() : "0");
                cmdInsert.Parameters.AddWithValue("@nro_jubilacion", oEmp.nro_jubilacion.Length > 0 ? oEmp.nro_jubilacion.Trim() : "0");
                cmdInsert.Parameters.AddWithValue("@antiguedad_ant", oEmp.antiguedad_ant > 0 ? oEmp.antiguedad_ant : 0);
                cmdInsert.Parameters.AddWithValue("@antiguedad_actual", oEmp.antiguedad_actual > 0 ? oEmp.antiguedad_actual : 0);
                cmdInsert.Parameters.AddWithValue("@nro_contrato", oEmp.nro_contrato > 0 ? oEmp.nro_contrato : 0);

                if (oEmp.fecha_inicio_contrato.Length > 0)
                    cmdInsert.Parameters.AddWithValue("@fecha_inicio_contrato", oEmp.fecha_inicio_contrato);
                if (oEmp.fecha_fin_contrato.Length > 0)
                    cmdInsert.Parameters.AddWithValue("@fecha_fin_contrato", oEmp.fecha_fin_contrato);
                if (oEmp.nro_nombramiento.Length > 0)
                    cmdInsert.Parameters.AddWithValue("@nro_nombramiento", oEmp.nro_nombramiento.Trim());
                if (oEmp.fecha_nombramiento.Length > 0)
                    cmdInsert.Parameters.AddWithValue("@fecha_nombramiento", oEmp.fecha_nombramiento);


                strSQL1.AppendLine("WHERE legajo=@legajo");
                cmdInsert.Parameters.AddWithValue("@legajo", oEmp.legajo);

                cmdInsert.CommandType = CommandType.Text;
                cmdInsert.CommandText = strSQL1.ToString();
                cmdInsert.Transaction = trx;
                //Antes de Actualizar
                //Guardo el estado anterior del Legajo
                Insert_cambios_empleados(oEmp.legajo, usuario, 7, "MODIFICA DATOS CONTRATO", obsauditoria, cn, trx);
                cmdInsert.ExecuteNonQuery();

            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmdInsert = null;
            }
            return oEmp.legajo;
        }

        public static Int32 UpdateTab_Datos_Banco(Entities.Empleado oEmp, string usuario, int id_tipo_auditoria, string des_tipo_auditoria, string obsauditoria, SqlConnection cn, SqlTransaction trx)
        {
            SqlCommand cmdInsert = null;
            try
            {
                StringBuilder strSQL = new StringBuilder();
                //objConn.Open();
                cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                //
                strSQL.AppendLine("UPDATE EMPLEADOS SET ");
                strSQL.AppendLine("cod_banco=@cod_banco,");
                strSQL.AppendLine("tipo_cuenta=@tipo_cuenta,");
                strSQL.AppendLine("nro_sucursal=@nro_sucursal,");
                strSQL.AppendLine("nro_caja_ahorro=@nro_caja_ahorro,");
                strSQL.AppendLine("nro_cbu=@nro_cbu");
                strSQL.AppendLine("WHERE legajo=@legajo");
                //
                cmdInsert.Parameters.AddWithValue("@cod_banco", oEmp.cod_banco > 0 ? oEmp.cod_banco : 0);
                cmdInsert.Parameters.AddWithValue("@tipo_cuenta", oEmp.tipo_cuenta.Trim() != null ? oEmp.tipo_cuenta.Trim() : null);
                cmdInsert.Parameters.AddWithValue("@nro_sucursal", oEmp.nro_sucursal.Trim() != null ? oEmp.nro_sucursal.Trim() : null);
                cmdInsert.Parameters.AddWithValue("@nro_caja_ahorro", oEmp.nro_caja_ahorro.Trim() != null ? oEmp.nro_caja_ahorro.Trim() : null);
                cmdInsert.Parameters.AddWithValue("@nro_cbu", oEmp.nro_cbu.Trim() != null ? oEmp.nro_cbu.Trim() : null);
                cmdInsert.Parameters.AddWithValue("@legajo", oEmp.legajo);
                //
                cmdInsert.CommandType = CommandType.Text;
                cmdInsert.CommandText = strSQL.ToString();
                cmdInsert.Transaction = trx;
                //Antes de Actualizar
                //Guardo el estado anterior del Legajo
                Insert_cambios_empleados(oEmp.legajo, usuario, 8, "MODIFICA DATOS BANCO", obsauditoria, cn, trx);
                cmdInsert.ExecuteNonQuery();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cmdInsert = null;
            }
            return oEmp.legajo;
        }

        public static int UpdateTab_Datos_Particulares(Empleado oEmp, string usuario, int id_tipo_auditoria, string des_tipo_auditoria, string obsauditoria, SqlConnection cn, SqlTransaction trx)
        {
            SqlCommand objCommand = null;
            try
            {
                StringBuilder strSQL = new StringBuilder();
                //Hist_cambios_empleados(oEmp.legajo, usuario, "", "", cn, trx);
                //cn.Open();
                objCommand = new SqlCommand();
                objCommand.Connection = cn;

                strSQL.AppendLine("UPDATE EMPLEADOS set ");
                strSQL.AppendLine("fecha_nacimiento=@fecha_nacimiento,");
                strSQL.AppendLine("sexo=@sexo,");
                strSQL.AppendLine("cod_estado_civil=@cod_estado_civil,");
                strSQL.AppendLine("pais_domicilio=@pais_domicilio,");
                strSQL.AppendLine("provincia_domicilio=@provincia_domicilio,");
                strSQL.AppendLine("ciudad_domicilio=@ciudad_domicilio,");
                strSQL.AppendLine("barrio_domicilio=@barrio_domicilio,");
                strSQL.AppendLine("calle_domicilio=@calle_domicilio,");
                strSQL.AppendLine("nro_domicilio=@nro_domicilio,");
                strSQL.AppendLine("piso_domicilio=@piso_domicilio,");
                strSQL.AppendLine("dpto_domicilio=@dpto_domicilio,");
                strSQL.AppendLine("monoblock_domicilio=@monoblock_domicilio,");
                strSQL.AppendLine("cod_postal=@cod_postal,");
                strSQL.AppendLine("telefonos=@telefonos,");
                strSQL.AppendLine("celular=@celular, ");
                strSQL.AppendLine("email=@email");
                //strSQL.AppendLine("imprime_recibo=@imprime_recibo ");
                objCommand.Parameters.AddWithValue("@fecha_nacimiento", oEmp.fecha_nacimiento != null ? oEmp.fecha_nacimiento : null);
                objCommand.Parameters.AddWithValue("@sexo", oEmp.sexo != null ? oEmp.sexo : null);
                objCommand.Parameters.AddWithValue("@cod_estado_civil", oEmp.cod_estado_civil > 0 ? oEmp.cod_estado_civil : 0);
                objCommand.Parameters.AddWithValue("@pais_domicilio", oEmp.pais_domicilio.Trim() != null ? oEmp.pais_domicilio.Trim() : null);
                objCommand.Parameters.AddWithValue("@provincia_domicilio", oEmp.provincia_domicilio.Trim() != null ? oEmp.provincia_domicilio.Trim() : null);
                objCommand.Parameters.AddWithValue("@ciudad_domicilio", oEmp.ciudad_domicilio.Trim() != null ? oEmp.ciudad_domicilio.Trim() : null);
                objCommand.Parameters.AddWithValue("@barrio_domicilio", oEmp.barrio_domicilio.Trim() != null ? oEmp.barrio_domicilio.Trim() : null);
                objCommand.Parameters.AddWithValue("@calle_domicilio", oEmp.calle_domicilio.Trim() != null ? oEmp.calle_domicilio.Trim() : null);
                objCommand.Parameters.AddWithValue("@nro_domicilio", oEmp.nro_domicilio.Trim() != null ? oEmp.nro_domicilio.Trim() : null);
                objCommand.Parameters.AddWithValue("@piso_domicilio", oEmp.piso_domicilio.Trim() != null ? oEmp.piso_domicilio.Trim() : null);
                objCommand.Parameters.AddWithValue("@dpto_domicilio", oEmp.dpto_domicilio.Trim() != null ? oEmp.dpto_domicilio.Trim() : null);
                objCommand.Parameters.AddWithValue("@monoblock_domicilio", oEmp.monoblock_domicilio.Trim() != null ? oEmp.monoblock_domicilio.Trim() : null);
                objCommand.Parameters.AddWithValue("@cod_postal", oEmp.cod_postal.Trim() != null ? oEmp.cod_postal.Trim() : null);
                objCommand.Parameters.AddWithValue("@telefonos", oEmp.telefonos.Trim() != null ? oEmp.telefonos.Trim() : null);
                objCommand.Parameters.AddWithValue("@celular", oEmp.celular.Trim() != null ? oEmp.celular.Trim() : null);
                objCommand.Parameters.AddWithValue("@email", oEmp.email != null ? oEmp.email : null);
                //objCommand.Parameters.AddWithValue("@imprime_recibe", oEmp.imprime_recibo);
                //Con esto saco la ultima coma
                //string sql = strSQL.ToString();
                //StringBuilder strSQLAUX = new StringBuilder();
                //nro = sql.Length;
                //strSQLAUX.AppendLine(sql.Remove((nro - 3), 1);
                //strSQLAUX.AppendLine(" WHERE legajo=@legajo");
                strSQL.AppendLine(" WHERE legajo=@legajo");
                objCommand.Parameters.AddWithValue("@legajo", oEmp.legajo);
                objCommand.CommandType = CommandType.Text;
                objCommand.CommandText = strSQL.ToString();
                objCommand.Transaction = trx;
                //Antes de Actualizar
                //Guardo el estado anterior del Legajo
                Insert_cambios_empleados(oEmp.legajo, usuario, 9, "MODIFICA DATOS PARTICULARES", obsauditoria, cn, trx);
                objCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                objCommand = null;
            }
            return oEmp.legajo;
        }

        public static int Cambios_categoria_empleado(int legajo, int cod_categoria, string usuario, string operacion,
          string observacion, SqlConnection cnn, SqlTransaction trx)
        {

            SqlCommand cmd = null;
            SqlCommand cmdInsert = null;
            int item = 0;

            try
            {
                StringBuilder strSQL = new StringBuilder();
                string SQL = @"SELECT isnull(max(item),0)  As item
                               FROM Empleados_cambios_categoria (nolock)
                               Where legajo = @legajo";
                cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@legajo", legajo);
                cmd.CommandText = SQL;
                cmd.Transaction = trx;
                item = Convert.ToInt32(cmd.ExecuteScalar()) + 1;

                cmdInsert = new SqlCommand();
                cmdInsert.Connection = cnn;
                cmdInsert.Transaction = trx;

                strSQL.AppendLine("insert into Empleados_cambios_categoria ");
                strSQL.AppendLine("(legajo, item, fecha_movimiento, cod_categoria, observacion, operacion, usuario) ");
                strSQL.AppendLine("values ");
                strSQL.AppendLine("(@legajo,");
                strSQL.AppendLine("@item,");
                strSQL.AppendLine("@fecha_movimiento,");
                strSQL.AppendLine("@cod_categoria,");
                strSQL.AppendLine("@observacion,");
                strSQL.AppendLine("@operacion,");
                strSQL.AppendLine("@usuario ) ");

                cmdInsert.Parameters.AddWithValue("@legajo", legajo);
                cmdInsert.Parameters.AddWithValue("@item", item);
                cmdInsert.Parameters.AddWithValue("@fecha_movimiento", DateTime.Today.ToShortDateString());
                cmdInsert.Parameters.AddWithValue("@cod_categoria", cod_categoria);
                cmdInsert.Parameters.AddWithValue("@observacion", observacion);
                cmdInsert.Parameters.AddWithValue("@operacion", operacion);
                cmdInsert.Parameters.AddWithValue("@usuario", usuario);

                cmdInsert.CommandType = CommandType.Text;
                cmdInsert.CommandText = strSQL.ToString();
                cmdInsert.ExecuteNonQuery();

            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                cmdInsert = null;
                cmd = null;
            }
            return legajo;
        }

        public static int Cambios_tipo_personal(int legajo, int cod_clasif_per, string usuario, string operacion, string observacion, SqlConnection cnn, SqlTransaction trx)
        {

            SqlCommand objCommand = null;
            SqlCommand cmdInsert = null;

            int item = 0;

            try
            {
                StringBuilder strSQL = new StringBuilder();

                string SQL = "SELECT isnull(max(item),0)  As item";
                SQL = SQL + " FROM Empleados_cambios_tipo_personal (nolock)";
                SQL = SQL + " Where legajo = " + legajo;

                objCommand = new SqlCommand();
                objCommand.Connection = cnn;
                objCommand.CommandType = CommandType.Text;
                objCommand.CommandText = SQL;
                objCommand.Transaction = trx;
                item = Convert.ToInt32(objCommand.ExecuteScalar()) + 1;

                cmdInsert = new SqlCommand();
                cmdInsert.Connection = cnn;

                strSQL.AppendLine("insert into Empleados_cambios_categoria ");
                strSQL.AppendLine("(legajo, item, fecha_movimiento, cod_clasif_per, observacion, operacion, usuario) ");
                strSQL.AppendLine("values ");
                strSQL.AppendLine("(@legajo,");
                strSQL.AppendLine("@item,");
                strSQL.AppendLine("@fecha_movimiento,");
                strSQL.AppendLine("@cod_clasif_per,");
                strSQL.AppendLine("@observacion,");
                strSQL.AppendLine("@operacion,");
                strSQL.AppendLine("@usuario) ");


                cmdInsert.Parameters.AddWithValue("@legajo", legajo);
                cmdInsert.Parameters.AddWithValue("@item", item);
                cmdInsert.Parameters.AddWithValue("@fecha_movimiento", DateTime.Today.ToShortDateString());
                cmdInsert.Parameters.AddWithValue("@cod_clasif_per", cod_clasif_per);
                cmdInsert.Parameters.AddWithValue("@observacion", observacion);
                cmdInsert.Parameters.AddWithValue("@operacion", operacion);
                cmdInsert.Parameters.AddWithValue("@usuario", usuario);

                cmdInsert.CommandType = CommandType.Text;
                cmdInsert.CommandText = strSQL.ToString();
                cmdInsert.Transaction = trx;
                cmdInsert.ExecuteNonQuery();

            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                cmdInsert = null;
                objCommand = null;
            }
            return legajo;
        }

        public static int Insert_cambios_empleados(int legajo, string usuario, int id_tipo_auditoria, string des_tipo_auditoria, string obsauditoria, SqlConnection cn, SqlTransaction trx)
        {
            Entities.Empleado oEmp = new Empleado();
            SqlCommand cmd = null;
            SqlCommand cmdInsert = null;
            //SqlConnection cn = DALBase.GetConnection("SIIMVA");
            int nro_item = 0;
            try
            {
                StringBuilder strSQL = new StringBuilder();
                if (legajo > 0)
                {
                    string SQL = @"SELECT isnull(max(nro_item),0)  As item
                                   FROM HIST_CAMBIO_EMPLEADOS (nolock)
                                   WHERE legajo = @legajo";

                    cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SQL;
                    cmd.Transaction = trx;
                    cmd.Parameters.AddWithValue("@legajo", legajo);
                    nro_item = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
                }
                else
                    nro_item = 1;
                oEmp = GetByPk(legajo, cn, trx);
                cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.Transaction = trx;
                strSQL.AppendLine("INSERT INTO HIST_CAMBIO_EMPLEADOS ");
                strSQL.AppendLine("(legajo, nro_item, nombre, fecha_movimiento, fecha_ingreso, cod_tipo_documento, nro_documento,");
                strSQL.AppendLine("cuil, tarea, cod_categoria, cod_cargo, cod_seccion, cod_clasif_per, cod_tipo_liq, id_secretaria, id_direccion,");
                strSQL.AppendLine("id_oficina, id_regimen, cod_escala_aumento, fecha_nacimiento, sexo, cod_estado_civil, pais_domicilio,");
                strSQL.AppendLine("provincia_domicilio, ciudad_domicilio, barrio_domicilio, calle_domicilio, nro_domicilio, piso_domicilio,");
                strSQL.AppendLine("dpto_domicilio, monoblock_domicilio, cod_postal, telefonos, celular, email, nro_cta_sb, nro_cta_gastos,");
                strSQL.AppendLine("nro_ipam, nro_jubilacion, antiguedad_ant, antiguedad_actual, nro_contrato, fecha_inicio_contrato,");
                strSQL.AppendLine("fecha_fin_contrato, nro_nombramiento, fecha_nombramiento, cod_banco, tipo_cuenta, nro_sucursal, nro_caja_ahorro,");
                strSQL.AppendLine("nro_cbu, cod_regimen_empleado, imprime_recibo, usuario, id_programa, id_revista, id_tipo_auditoria, obsauditoria)");
                strSQL.AppendLine(" values ");
                strSQL.AppendLine("(@legajo, @nro_item, @nombre, @fecha_movimiento, @fecha_ingreso, @cod_tipo_documento, @nro_documento, @cuil,");
                strSQL.AppendLine("@tarea, @cod_categoria, @cod_cargo,@cod_seccion, @cod_clasif_per, @cod_tipo_liq, @id_secretaria, @id_direccion, @id_oficina,");
                strSQL.AppendLine("@id_regimen, @cod_escala_aumento, @fecha_nacimiento, @sexo, @cod_estado_civil, @pais_domicilio, @provincia_domicilio,");
                strSQL.AppendLine("@ciudad_domicilio, @barrio_domicilio, @calle_domicilio, @nro_domicilio, @piso_domicilio, @dpto_domicilio, @monoblock_domicilio,");
                strSQL.AppendLine("@cod_postal, @telefonos, @celular, @email, @nro_cta_sb, @nro_cta_gastos, @nro_ipam, @nro_jubilacion, @antiguedad_ant,");
                strSQL.AppendLine("@antiguedad_actual, @nro_contrato, @fecha_inicio_contrato, @fecha_fin_contrato, @nro_nombramiento, @fecha_nombramiento,");
                strSQL.AppendLine("@cod_banco, @tipo_cuenta, @nro_sucursal, @nro_caja_ahorro, @nro_cbu, @cod_regimen_empleado, @imprime_recibo,");
                strSQL.AppendLine("@usuario, @id_programa, @id_revista, @id_tipo_auditoria, @obsauditoria) ");
                //
                cmdInsert.Parameters.AddWithValue("@legajo", oEmp.legajo);
                cmdInsert.Parameters.AddWithValue("@nro_item", nro_item);
                cmdInsert.Parameters.AddWithValue("@nombre", oEmp.nombre.TrimEnd());
                cmdInsert.Parameters.AddWithValue("@fecha_movimiento", DateTime.Today.ToShortDateString());
                cmdInsert.Parameters.AddWithValue("@fecha_ingreso", oEmp.fecha_ingreso != null ? oEmp.fecha_ingreso : "");
                cmdInsert.Parameters.AddWithValue("@cod_tipo_documento", oEmp.cod_tipo_documento > 0 ? oEmp.cod_tipo_documento : 0);
                cmdInsert.Parameters.AddWithValue("@nro_documento", oEmp.nro_documento != null ? oEmp.nro_documento : "");
                cmdInsert.Parameters.AddWithValue("@cuil", oEmp.cuil != null ? oEmp.cuil : "");
                if (oEmp.tarea.Trim().Length > 0)
                    cmdInsert.Parameters.AddWithValue("@tarea", oEmp.tarea.Trim() != null ? oEmp.tarea.Trim() : "");
                else
                    cmdInsert.Parameters.AddWithValue("@tarea", string.Empty);
                cmdInsert.Parameters.AddWithValue("@cod_categoria", oEmp.cod_categoria > 0 ? oEmp.cod_categoria : 0);
                cmdInsert.Parameters.AddWithValue("@cod_cargo", oEmp.cod_cargo > 0 ? oEmp.cod_cargo : 0);
                cmdInsert.Parameters.AddWithValue("@cod_seccion", oEmp.cod_seccion > 0 ? oEmp.cod_seccion : 0);
                cmdInsert.Parameters.AddWithValue("@cod_clasif_per", oEmp.cod_clasif_per > 0 ? oEmp.cod_clasif_per : 0);
                cmdInsert.Parameters.AddWithValue("@cod_tipo_liq", oEmp.cod_tipo_liq > 0 ? oEmp.cod_tipo_liq : 0);
                cmdInsert.Parameters.AddWithValue("@id_secretaria", oEmp.id_secretaria > 0 ? oEmp.id_secretaria : 0);
                cmdInsert.Parameters.AddWithValue("@id_direccion", oEmp.id_direccion > 0 ? oEmp.id_direccion : 0);
                cmdInsert.Parameters.AddWithValue("@id_oficina", oEmp.id_oficina > 0 ? oEmp.id_oficina : 0);
                cmdInsert.Parameters.AddWithValue("@id_regimen", oEmp.id_regimen > 0 ? oEmp.id_regimen : 0);
                cmdInsert.Parameters.AddWithValue("@cod_escala_aumento", oEmp.cod_escala_aumento > 0 ? oEmp.cod_escala_aumento : 0);
                //
                cmdInsert.Parameters.AddWithValue("@fecha_nacimiento", oEmp.fecha_nacimiento != null ? oEmp.fecha_nacimiento : "");
                cmdInsert.Parameters.AddWithValue("@sexo", oEmp.sexo.Trim() != null ? oEmp.sexo.Trim() : "");
                cmdInsert.Parameters.AddWithValue("@cod_estado_civil", oEmp.cod_estado_civil > 0 ? oEmp.cod_estado_civil : 0);
                cmdInsert.Parameters.AddWithValue("@pais_domicilio", oEmp.pais_domicilio.Trim() != null ? oEmp.pais_domicilio.Trim() : "");
                cmdInsert.Parameters.AddWithValue("@provincia_domicilio", oEmp.provincia_domicilio.Trim() != null ? oEmp.provincia_domicilio.Trim() : "");
                cmdInsert.Parameters.AddWithValue("@ciudad_domicilio", oEmp.ciudad_domicilio.Trim() != null ? oEmp.ciudad_domicilio.Trim() : "");
                cmdInsert.Parameters.AddWithValue("@barrio_domicilio", oEmp.barrio_domicilio.Trim() != null ? oEmp.barrio_domicilio.Trim() : "");
                cmdInsert.Parameters.AddWithValue("@calle_domicilio", oEmp.calle_domicilio.Trim() != null ? oEmp.calle_domicilio.Trim() : "");
                cmdInsert.Parameters.AddWithValue("@nro_domicilio", oEmp.nro_domicilio.Trim() != null ? oEmp.nro_domicilio.Trim() : "");
                //
                cmdInsert.Parameters.AddWithValue("@piso_domicilio", oEmp.piso_domicilio.Trim() != null ? oEmp.piso_domicilio : "");
                cmdInsert.Parameters.AddWithValue("@dpto_domicilio", oEmp.dpto_domicilio != null ? oEmp.dpto_domicilio : "");
                cmdInsert.Parameters.AddWithValue("@monoblock_domicilio", oEmp.monoblock_domicilio != null ? oEmp.monoblock_domicilio : "");
                cmdInsert.Parameters.AddWithValue("@cod_postal", oEmp.cod_postal.Trim() != null ? oEmp.cod_postal.Trim() : "");
                cmdInsert.Parameters.AddWithValue("@telefonos", oEmp.telefonos.Trim() != null ? oEmp.telefonos.Trim() : "");
                cmdInsert.Parameters.AddWithValue("@celular", oEmp.celular.Trim() != null ? oEmp.celular.Trim() : "");
                cmdInsert.Parameters.AddWithValue("@email", oEmp.email != null ? oEmp.email : "");
                cmdInsert.Parameters.AddWithValue("@nro_cta_sb", oEmp.nro_cta_sb.Trim() != null ? oEmp.nro_cta_sb.Trim() : "");
                cmdInsert.Parameters.AddWithValue("@nro_cta_gastos", oEmp.nro_cta_gastos.Trim() != null ? oEmp.nro_cta_gastos.Trim() : "");
                cmdInsert.Parameters.AddWithValue("@nro_ipam", oEmp.nro_ipam.Trim() != null ? oEmp.nro_ipam.Trim() : "");
                cmdInsert.Parameters.AddWithValue("@nro_jubilacion", oEmp.nro_jubilacion.Trim() != null ? oEmp.nro_jubilacion.Trim() : "");
                cmdInsert.Parameters.AddWithValue("@antiguedad_ant", oEmp.antiguedad_ant > 0 ? oEmp.antiguedad_ant : 0);
                cmdInsert.Parameters.AddWithValue("@antiguedad_actual", oEmp.antiguedad_actual > 0 ? oEmp.antiguedad_actual : 0);
                cmdInsert.Parameters.AddWithValue("@nro_contrato", oEmp.nro_contrato > 0 ? oEmp.nro_contrato : 0);
                cmdInsert.Parameters.AddWithValue("@fecha_inicio_contrato", oEmp.fecha_inicio_contrato != null ? oEmp.fecha_inicio_contrato : "");
                cmdInsert.Parameters.AddWithValue("@fecha_fin_contrato", oEmp.fecha_fin_contrato != null ? oEmp.fecha_fin_contrato : "");
                cmdInsert.Parameters.AddWithValue("@nro_nombramiento", oEmp.nro_nombramiento != null ? oEmp.nro_nombramiento : "");
                cmdInsert.Parameters.AddWithValue("@fecha_nombramiento", oEmp.fecha_nombramiento != null ? oEmp.fecha_nombramiento : "");
                cmdInsert.Parameters.AddWithValue("@cod_banco", oEmp.cod_banco > 0 ? oEmp.cod_banco : 0);
                cmdInsert.Parameters.AddWithValue("@tipo_cuenta", oEmp.tipo_cuenta.Trim() != null ? oEmp.tipo_cuenta.Trim() : "");
                cmdInsert.Parameters.AddWithValue("@nro_sucursal", oEmp.nro_sucursal.Trim() != null ? oEmp.nro_sucursal.Trim() : "");
                cmdInsert.Parameters.AddWithValue("@nro_caja_ahorro", oEmp.nro_caja_ahorro != null ? oEmp.nro_caja_ahorro : "");
                cmdInsert.Parameters.AddWithValue("@nro_cbu", oEmp.nro_cbu.Trim() != null ? oEmp.nro_cbu.Trim() : "");
                cmdInsert.Parameters.AddWithValue("@cod_regimen_empleado", oEmp.cod_regimen_empleado > 0 ? oEmp.cod_regimen_empleado : 0);
                cmdInsert.Parameters.AddWithValue("@imprime_recibo", oEmp.imprime_recibo);
                cmdInsert.Parameters.AddWithValue("@usuario", usuario);
                cmdInsert.Parameters.AddWithValue("@id_programa", oEmp.id_programa);
                cmdInsert.Parameters.AddWithValue("@id_revista", oEmp.id_revista);
                cmdInsert.Parameters.AddWithValue("@id_tipo_auditoria", id_tipo_auditoria);
                cmdInsert.Parameters.AddWithValue("@obsauditoria", obsauditoria.Trim());
                cmdInsert.CommandType = CommandType.Text;
                cmdInsert.CommandText = strSQL.ToString();
                cmdInsert.ExecuteNonQuery();
                //Dsp audito el proceso
                Entities.Auditoria oAudita = new Auditoria();
                oAudita.id_auditoria = 0;
                oAudita.fecha_movimiento = DateTime.Now.ToString();
                oAudita.menu = "EMPLEADOS";
                oAudita.proceso = des_tipo_auditoria;
                oAudita.identificacion = oEmp.legajo.ToString();
                oAudita.autorizaciones = "";
                oAudita.observaciones = obsauditoria;
                oAudita.detalle = Newtonsoft.Json.JsonConvert.SerializeObject(oEmp);
                oAudita.usuario = usuario;
                DAL.AuditoriaD.Insert_movimiento(oAudita, cn, trx);

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
            return legajo;
        }

        public static string Certficaciones_Empleado(int legajo)
        {
            Entities.Empleado oEmp = new Empleado();
            DBHelper oLib = new DBHelper();
            string strXML = "";

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.AppendLine(" SELECT e.legajo, e.nombre, l.anio, l.periodo, l.des_liquidacion, a.desc_cargo, e.tarea, l1.importe");
                strSQL.AppendLine(" FROM LIQUIDACIONES l WITH (NOLOCK)");
                strSQL.AppendLine(" JOIN DET_LIQ_X_EMPLEADO l1 ON");
                strSQL.AppendLine(" l.anio=l1.anio AND");
                strSQL.AppendLine(" l.cod_tipo_liq=l1.cod_tipo_liq AND");
                strSQL.AppendLine(" l.nro_liquidacion = l1.nro_liquidacion AND");
                strSQL.AppendLine(" l1.cod_concepto_liq = 390");
                strSQL.AppendLine(" JOIN EMPLEADOS e ON");
                strSQL.AppendLine(" e.legajo = l1.legajo");
                //strSQL.AppendLine(" LEFT JOIN TIPOS_DOCUMENTOS t ON");
                //strSQL.AppendLine(" e.cod_tipo_documento = t.cod_tipo_documento");
                strSQL.AppendLine(" LEFT JOIN CATEGORIAS c ON");
                strSQL.AppendLine(" e.cod_categoria = c.cod_categoria");
                strSQL.AppendLine(" LEFT JOIN CARGOS a ON");
                strSQL.AppendLine(" e.cod_cargo = a.cod_cargo");
                strSQL.AppendLine(" LEFT JOIN clasificaciones_personal b ON");
                strSQL.AppendLine(" e.cod_clasif_per = b.cod_clasif_per");
                strSQL.AppendLine(" WHERE e.legajo=" + legajo.ToString());
                strSQL.AppendLine(" ORDER BY l.anio,l.cod_tipo_liq,l.nro_liquidacion,l.periodo");
                strXML = oLib.GetXML("Datos", "Dato", strSQL.ToString());
                return strXML;
            }

            catch (Exception ex)
            { throw ex; }
            finally
            {
                oLib = null;
                oEmp = null;
            }
        }

        public static List<Entities.Certificaciones> GetCertificaciones(int legajo)
        {
            List<Entities.Certificaciones> oLst = new List<Entities.Certificaciones>();
            Entities.Certificaciones oDetalle;

            SqlCommand objCommand = null;
            SqlConnection cnn = DALBase.GetConnection("SIIMVA");
            SqlDataReader dr;

            try
            {

                StringBuilder strSQL = new StringBuilder();

                strSQL.AppendLine(" SELECT e.legajo, e.nombre, l.anio, l.periodo, l.des_liquidacion, a.desc_cargo, e.tarea, l1.importe");
                strSQL.AppendLine(" FROM LIQUIDACIONES l WITH (NOLOCK)");
                strSQL.AppendLine(" JOIN DET_LIQ_X_EMPLEADO l1 ON");
                strSQL.AppendLine(" l.anio=l1.anio AND");
                strSQL.AppendLine(" l.cod_tipo_liq=l1.cod_tipo_liq AND");
                strSQL.AppendLine(" l.nro_liquidacion = l1.nro_liquidacion AND");
                strSQL.AppendLine(" l1.cod_concepto_liq = 390");
                strSQL.AppendLine(" JOIN EMPLEADOS e ON");
                strSQL.AppendLine(" e.legajo = l1.legajo");
                //strSQL.AppendLine(" JOIN TIPOS_DOCUMENTOS t ON");
                //strSQL.AppendLine(" e.cod_tipo_documento = t.cod_tipo_documento");
                strSQL.AppendLine(" LEFT JOIN CATEGORIAS c ON");
                strSQL.AppendLine(" e.cod_categoria = c.cod_categoria");
                strSQL.AppendLine(" Left JOIN CARGOS a ON");
                strSQL.AppendLine(" e.cod_cargo = a.cod_cargo");
                strSQL.AppendLine(" Left JOIN clasificaciones_personal b ON");
                strSQL.AppendLine(" e.cod_clasif_per = b.cod_clasif_per");
                strSQL.AppendLine(" WHERE e.legajo=@legajo");
                strSQL.AppendLine(" ORDER BY l.anio,l.cod_tipo_liq,l.nro_liquidacion,l.periodo");

                objCommand = new SqlCommand();
                objCommand.Parameters.AddWithValue("@legajo", legajo);
                objCommand.Connection = cnn;
                objCommand.CommandType = CommandType.Text;
                objCommand.CommandText = strSQL.ToString();
                objCommand.Connection.Open();
                dr = objCommand.ExecuteReader();


                while (dr.Read())
                {
                    oDetalle = new Certificaciones();

                    if (!dr.IsDBNull(dr.GetOrdinal("legajo")))
                        oDetalle.legajo = dr.GetInt32(dr.GetOrdinal("legajo"));

                    if (!dr.IsDBNull(dr.GetOrdinal("nombre")))
                        oDetalle.nombre = dr.GetString(dr.GetOrdinal("nombre"));

                    if (!dr.IsDBNull(dr.GetOrdinal("anio")))
                        oDetalle.anio = Convert.ToString(dr.GetInt32(dr.GetOrdinal("anio")));

                    if (!dr.IsDBNull(dr.GetOrdinal("periodo")))
                        oDetalle.periodo = dr.GetString(dr.GetOrdinal("periodo"));

                    oDetalle.des_liquidacion = dr.GetString(dr.GetOrdinal("des_liquidacion"));

                    if (!dr.IsDBNull(dr.GetOrdinal("desc_cargo")))
                        oDetalle.cargo = dr.GetString(dr.GetOrdinal("desc_cargo"));

                    if (!dr.IsDBNull(dr.GetOrdinal("tarea")))
                        oDetalle.tarea = dr.GetString(dr.GetOrdinal("tarea"));

                    if (!dr.IsDBNull(dr.GetOrdinal("importe")))
                        oDetalle.importe = dr.GetDecimal(dr.GetOrdinal("importe"));


                    oLst.Add(oDetalle);
                }

                return oLst;

            }

            catch (Exception ex)
            { throw ex; }
            finally
            {
                oLst = null;
                oDetalle = null;
            }
        }

        public static List<Entities.HistorialEmpleado> GetHistCambiosPersonal(int legajo, SqlConnection cn)
        {
            List<Entities.HistorialEmpleado> oLst = new List<Entities.HistorialEmpleado>();
            Entities.HistorialEmpleado oDetalle;

            SqlCommand cmd = null;
            SqlDataReader dr;

            try
            {

                //         public string des_clasif_per { get; set; }
                //public string des_tipo_liq { get; set; }
                //public string nro_cta_sb { get; set; }
                //public string nro_cta_gastos { get; set; }
                //public string fecha_baja { get; set; }
                //public int nro_contrato { get; set; }
                //public string fecha_inicio_contrato { get; set; }
                //public string fecha_fin_contrato { get; set; }
                //public int id_regimen { get; set; }
                //public string des_secretaria { get; set; }
                //public string des_direccion { get; set; }
                //public int cod_escala_aumento { get; set; }
                //public string email { get; set; }
                //public int cod_regimen_empleado { get; set; }
                //public string des_regimen_empleado { get; set; }
                //public string situacion_revista { get; set; }



                string strSQL = @"SELECT h.legajo,h.nro_item,convert(char(10),h.fecha_movimiento,103) as fecha_movimiento,
                    h.nombre,t.des_tipo_documento,h.nro_documento,convert(char(10),h.fecha_nacimiento,103) as fecha_nacimiento,
                    h.sexo,h.ciudad_domicilio,h.barrio_domicilio,h.calle_domicilio,h.nro_domicilio,
                    h.dpto_domicilio,h.piso_domicilio,h.monoblock_domicilio,h.telefonos,h.celular,h.cod_postal,
                    e.des_estado_civil, convert(char(10),h.fecha_ingreso,103) as fecha_ingreso,
                    h.tarea,s.des_seccion,h.cod_categoria, c.sueldo_basico,g.desc_cargo,b.nom_banco,h.nro_sucursal,
                    tc.des_tipo_cuenta,h.nro_caja_ahorro,h.nro_ipam,h.cuil,h.antiguedad_ant,h.antiguedad_actual,
                    cp.des_clasif_per , tl.des_tipo_liq, h.nro_cta_sb, h.nro_cta_gastos, h.fecha_baja, h.nro_contrato,
                    convert(char(10),h.fecha_inicio_contrato,103) as fecha_inicio_contrato, 
                    convert(char(10), h.fecha_fin_contrato,103) as fecha_fin_contrato, 
                    h.id_regimen, s1.descripcion as des_secretaria, 
                    d.descripcion as des_direccion, h.cod_escala_aumento, 
                    h.email, h.cod_regimen_empleado, er.descripcion as des_regimen_empleado , 
                    h.imprime_recibo,h.id_programa, rtrim(ltrim(p.programa)) as Programa,
                    rtrim(ltrim(srl.descripcion)) as situacion_revista,
                    ta.des_tipo_auditoria,
                    h.obsauditoria
                    FROM hist_cambio_empleados h
                    FULL join tipos_documentos t on
                      h.cod_tipo_documento = t.cod_tipo_documento
                    FULL join estados_civiles e on
                      h.cod_estado_civil = e.cod_estado_civil
                    FULL join secciones s on
                      h.cod_seccion = s.cod_seccion
                    FULL join categorias c on
                      h.cod_categoria = c.cod_categoria
                    FULL join cargos g on
                      h.cod_cargo = g.cod_cargo
                    FULL join bancos b on
                      h.cod_banco = b.cod_banco
                    FULL join tipos_cuentas tc on
                      h.tipo_cuenta = tc.cod_tipo_cuenta
                    FULL join clasificaciones_personal cp on
                      h.cod_clasif_per = cp.cod_clasif_per
                    FULL join tipos_liquidacion tl on
                      h.cod_tipo_liq = tl.cod_tipo_liq
                    FULL join secretaria s1 on
                      h.id_secretaria = s1.id_secretaria
                    FULL join direccion d on
                      h.id_direccion = d.id_direccion
                    FULL join programas_publicos p on
                      h.id_programa = p.id_programa
                    FULL join EMPLEADOS_REGIMEN er on
                      er.cod_regimen_empleado = h.cod_regimen_empleado
                    Left join situacion_revista_legajo srl on
                      srl.id_revista = h.id_revista
                    LEFT JOIN TIPO_AUDITORIA_SPW ta on
                      ta.id=h.id_tipo_auditoria
                    WHERE h.legajo=@legajo";
                cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@legajo", legajo);
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    oDetalle = new HistorialEmpleado();

                    if (!dr.IsDBNull(dr.GetOrdinal("legajo")))
                        oDetalle.legajo = dr.GetInt32(dr.GetOrdinal("legajo"));

                    if (!dr.IsDBNull(dr.GetOrdinal("nro_item")))
                        oDetalle.nro_item = dr.GetInt32(dr.GetOrdinal("nro_item"));

                    if (!dr.IsDBNull(dr.GetOrdinal("fecha_movimiento")))
                        oDetalle.fecha_movimiento = dr.GetString(dr.GetOrdinal("fecha_movimiento"));

                    if (!dr.IsDBNull(dr.GetOrdinal("nombre")))
                        oDetalle.nombre = dr.GetString(dr.GetOrdinal("nombre"));

                    if (!dr.IsDBNull(dr.GetOrdinal("des_tipo_documento")))
                        oDetalle.des_tipo_documento = dr.GetString(dr.GetOrdinal("des_tipo_documento"));

                    if (!dr.IsDBNull(dr.GetOrdinal("nro_documento")))
                        oDetalle.nro_documento = dr.GetString(dr.GetOrdinal("nro_documento"));

                    if (!dr.IsDBNull(dr.GetOrdinal("fecha_nacimiento")))
                        oDetalle.fecha_nacimiento = dr.GetString(dr.GetOrdinal("fecha_nacimiento"));

                    if (!dr.IsDBNull(dr.GetOrdinal("sexo")))
                        oDetalle.sexo = dr.GetString(dr.GetOrdinal("sexo"));

                    if (!dr.IsDBNull(dr.GetOrdinal("ciudad_domicilio")))
                        oDetalle.ciudad_domicilio = dr.GetString(dr.GetOrdinal("ciudad_domicilio"));

                    if (!dr.IsDBNull(dr.GetOrdinal("barrio_domicilio")))
                        oDetalle.barrio_domicilio = dr.GetString(dr.GetOrdinal("barrio_domicilio"));

                    if (!dr.IsDBNull(dr.GetOrdinal("calle_domicilio")))
                        oDetalle.calle_domicilio = dr.GetString(dr.GetOrdinal("calle_domicilio"));

                    if (!dr.IsDBNull(dr.GetOrdinal("nro_domicilio")))
                        oDetalle.nro_domicilio = Convert.ToString(dr.GetInt32(dr.GetOrdinal("nro_domicilio")));
                    if (!dr.IsDBNull(dr.GetOrdinal("dpto_domicilio")))
                        oDetalle.dpto_domicilio = dr.GetString(dr.GetOrdinal("dpto_domicilio"));
                    if (!dr.IsDBNull(dr.GetOrdinal("piso_domicilio")))
                        oDetalle.piso_domicilio = dr.GetString(dr.GetOrdinal("piso_domicilio"));
                    if (!dr.IsDBNull(dr.GetOrdinal("monoblock_domicilio")))
                        oDetalle.monoblock_domicilio = dr.GetString(dr.GetOrdinal("monoblock_domicilio"));
                    if (!dr.IsDBNull(dr.GetOrdinal("telefonos")))
                        oDetalle.telefonos = dr.GetString(dr.GetOrdinal("telefonos"));
                    if (!dr.IsDBNull(dr.GetOrdinal("celular")))
                        oDetalle.celular = dr.GetString(dr.GetOrdinal("celular"));
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_postal")))
                        oDetalle.cod_postal = dr.GetString(dr.GetOrdinal("cod_postal"));
                    if (!dr.IsDBNull(dr.GetOrdinal("des_estado_civil")))
                        oDetalle.des_estado_civil = dr.GetString(dr.GetOrdinal("des_estado_civil"));
                    if (!dr.IsDBNull(dr.GetOrdinal("fecha_ingreso")))
                        oDetalle.fecha_ingreso = dr.GetString(dr.GetOrdinal("fecha_ingreso"));
                    if (!dr.IsDBNull(dr.GetOrdinal("tarea")))
                        oDetalle.tarea = dr.GetString(dr.GetOrdinal("tarea"));
                    if (!dr.IsDBNull(dr.GetOrdinal("des_seccion")))
                        oDetalle.des_seccion = dr.GetString(dr.GetOrdinal("des_seccion"));
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_categoria")))
                        oDetalle.cod_categoria = dr.GetInt32(dr.GetOrdinal("cod_categoria"));

                    if (!dr.IsDBNull(dr.GetOrdinal("sueldo_basico")))
                        oDetalle.sueldo_basico = dr.GetDecimal(dr.GetOrdinal("sueldo_basico"));
                    if (!dr.IsDBNull(dr.GetOrdinal("desc_cargo")))
                        oDetalle.desc_cargo = dr.GetString(dr.GetOrdinal("desc_cargo"));
                    if (!dr.IsDBNull(dr.GetOrdinal("nom_banco")))
                        oDetalle.nom_banco = dr.GetString(dr.GetOrdinal("nom_banco"));
                    if (!dr.IsDBNull(dr.GetOrdinal("nro_sucursal")))
                        oDetalle.nro_sucursal = dr.GetString(dr.GetOrdinal("nro_sucursal"));

                    if (!dr.IsDBNull(dr.GetOrdinal("des_tipo_cuenta")))
                        oDetalle.des_tipo_cuenta = dr.GetString(dr.GetOrdinal("des_tipo_cuenta"));

                    if (!dr.IsDBNull(dr.GetOrdinal("nro_caja_ahorro")))
                        oDetalle.nro_caja_ahorro = dr.GetString(dr.GetOrdinal("nro_caja_ahorro"));

                    if (!dr.IsDBNull(dr.GetOrdinal("nro_ipam")))
                        oDetalle.nro_ipam = dr.GetString(dr.GetOrdinal("nro_ipam"));

                    if (!dr.IsDBNull(dr.GetOrdinal("cuil")))
                        oDetalle.cuil = dr.GetString(dr.GetOrdinal("cuil"));

                    if (!dr.IsDBNull(dr.GetOrdinal("antiguedad_ant")))
                        oDetalle.antiguedad_ant = dr.GetInt32(dr.GetOrdinal("antiguedad_ant"));

                    if (!dr.IsDBNull(dr.GetOrdinal("antiguedad_actual")))
                        oDetalle.antigudad_actual = dr.GetInt32(dr.GetOrdinal("antiguedad_actual"));

                    if (!dr.IsDBNull(dr.GetOrdinal("des_clasif_per")))
                        oDetalle.des_clasif_per = dr.GetString(dr.GetOrdinal("des_clasif_per"));
                    if (!dr.IsDBNull(dr.GetOrdinal("des_tipo_liq")))
                        oDetalle.des_tipo_liq = dr.GetString(dr.GetOrdinal("des_tipo_liq"));
                    if (!dr.IsDBNull(dr.GetOrdinal("nro_cta_sb")))
                        oDetalle.nro_cta_sb = dr.GetString(dr.GetOrdinal("nro_cta_sb"));
                    if (!dr.IsDBNull(dr.GetOrdinal("nro_cta_gastos")))
                        oDetalle.nro_cta_gastos = dr.GetString(dr.GetOrdinal("nro_cta_gastos"));
                    if (!dr.IsDBNull(dr.GetOrdinal("fecha_baja")))
                        oDetalle.fecha_baja = Convert.ToString(dr.GetOrdinal("fecha_baja"));

                    if (!dr.IsDBNull(dr.GetOrdinal("nro_contrato")))
                        oDetalle.nro_contrato = dr.GetInt32(dr.GetOrdinal("nro_contrato"));

                    if (!dr.IsDBNull(dr.GetOrdinal("fecha_inicio_contrato")))
                        oDetalle.fecha_inicio_contrato = dr.GetString(dr.GetOrdinal("fecha_inicio_contrato"));
                    if (!dr.IsDBNull(dr.GetOrdinal("fecha_fin_contrato")))
                        oDetalle.fecha_fin_contrato = dr.GetString(dr.GetOrdinal("fecha_fin_contrato"));
                    if (!dr.IsDBNull(dr.GetOrdinal("id_regimen")))
                        oDetalle.id_regimen = dr.GetInt32(dr.GetOrdinal("id_regimen"));
                    if (!dr.IsDBNull(dr.GetOrdinal("des_secretaria")))
                        oDetalle.des_secretaria = dr.GetString(dr.GetOrdinal("des_secretaria"));
                    if (!dr.IsDBNull(dr.GetOrdinal("des_direccion")))
                        oDetalle.des_direccion = dr.GetString(dr.GetOrdinal("des_direccion"));
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_escala_aumento")))
                        oDetalle.cod_escala_aumento = dr.GetInt32(dr.GetOrdinal("cod_escala_aumento"));
                    if (!dr.IsDBNull(dr.GetOrdinal("email")))
                        oDetalle.email = dr.GetString(dr.GetOrdinal("email"));
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_regimen_empleado")))
                        oDetalle.cod_regimen_empleado = dr.GetInt32(dr.GetOrdinal("cod_regimen_empleado"));
                    if (!dr.IsDBNull(dr.GetOrdinal("des_regimen_empleado")))
                        oDetalle.des_regimen_empleado = dr.GetString(dr.GetOrdinal("des_regimen_empleado"));

                    if (!dr.IsDBNull(dr.GetOrdinal("imprime_recibo")))
                        oDetalle.imprime_recibo = dr.GetInt16(dr.GetOrdinal("imprime_recibo"));

                    if (!dr.IsDBNull(dr.GetOrdinal("programa")))
                        oDetalle.programa = dr.GetString(dr.GetOrdinal("programa"));

                    if (!dr.IsDBNull(dr.GetOrdinal("situacion_revista")))
                        oDetalle.situacion_revista = dr.GetString(dr.GetOrdinal("situacion_revista"));


                    if (!dr.IsDBNull(dr.GetOrdinal("des_tipo_auditoria")))
                        oDetalle.des_tipo_auditoria = dr.GetString(dr.GetOrdinal("des_tipo_auditoria"));

                    if (!dr.IsDBNull(dr.GetOrdinal("obsauditoria")))
                        oDetalle.obsauditoria = dr.GetString(dr.GetOrdinal("obsauditoria"));

                    oLst.Add(oDetalle);
                }
                return oLst;
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                oLst = null;
                oDetalle = null;
            }
        }

        public static List<Entities.Tipo_auditoria> GetTipo_auditoria()
        {
            List<Entities.Tipo_auditoria> lst = new List<Entities.Tipo_auditoria>();
            Entities.Tipo_auditoria oTipo;
            string sql = @"SELECT * 
                            FROM TIPO_AUDITORIA_SPW
                            WHERE activo=1";
            try
            {
                using (SqlConnection cn = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {

                        int id = dr.GetOrdinal("id");
                        int des_tipo_auditoria = dr.GetOrdinal("des_tipo_auditoria");
                        while (dr.Read())
                        {
                            oTipo = new Entities.Tipo_auditoria();
                            if (!dr.IsDBNull(id)) oTipo.id =
                                    dr.GetInt32(id);
                            if (!dr.IsDBNull(des_tipo_auditoria)) oTipo.des_tipo_auditoria =
                                    Convert.ToString(dr.GetString(des_tipo_auditoria));
                            lst.Add(oTipo);
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            return lst;
        }
        public static int UpdateTab_Datos_Contrato(
  Empleado oEmp,
  string usuario,
  SqlConnection cn,
  SqlTransaction trx)
        {
            SqlCommand sqlCommand1 = (SqlCommand)null;
            string nroCtaSb = oEmp.nro_cta_sb;
            try
            {
                StringBuilder stringBuilder1 = new StringBuilder();
                StringBuilder stringBuilder2 = new StringBuilder();
                SqlCommand sqlCommand2 = new SqlCommand();
                sqlCommand2.Connection = cn;
                sqlCommand2.Transaction = trx;
                stringBuilder1.AppendLine("UPDATE EMPLEADOS set ");
                stringBuilder1.AppendLine("nro_ipam=@nro_ipam,");
                stringBuilder1.AppendLine("nro_jubilacion=@nro_jubilacion,");
                stringBuilder1.AppendLine("antiguedad_ant=@antiguedad_ant,");
                stringBuilder1.AppendLine("antiguedad_actual=@antiguedad_actual,");
                stringBuilder1.AppendLine("nro_contrato=@nro_contrato,");
                if (oEmp.fecha_inicio_contrato.Length > 0)
                    stringBuilder1.AppendLine("fecha_inicio_contrato=@fecha_inicio_contrato,");
                else
                    stringBuilder1.AppendLine("fecha_inicio_contrato=null,");
                if (oEmp.fecha_fin_contrato.Length > 0)
                    stringBuilder1.AppendLine("fecha_fin_contrato=@fecha_fin_contrato,");
                else
                    stringBuilder1.AppendLine("fecha_fin_contrato=null,");
                if (oEmp.nro_nombramiento.Length > 0)
                    stringBuilder1.AppendLine("nro_nombramiento=@nro_nombramiento,");
                else
                    stringBuilder1.AppendLine("nro_nombramiento=null,");
                if (oEmp.fecha_nombramiento.Length > 0)
                    stringBuilder1.AppendLine("fecha_nombramiento=@fecha_nombramiento,");
                else
                    stringBuilder1.AppendLine("fecha_nombramiento=null,");

                //new char[1][0] = ',';
                string str = stringBuilder1.ToString();
                stringBuilder2.AppendLine(str.Remove(str.Trim().Length - 1, 1));
                sqlCommand2.Parameters.Add(new SqlParameter("@nro_ipam", oEmp.nro_ipam.Length > 0 ? (object)oEmp.nro_ipam : (object)"0"));
                sqlCommand2.Parameters.Add(new SqlParameter("@nro_jubilacion", oEmp.nro_jubilacion.Length > 0 ? (object)oEmp.nro_jubilacion : (object)"0"));
                sqlCommand2.Parameters.Add(new SqlParameter("@antiguedad_ant", (object)(oEmp.antiguedad_ant > 0 ? oEmp.antiguedad_ant : 0)));
                sqlCommand2.Parameters.Add(new SqlParameter("@antiguedad_actual", (object)(oEmp.antiguedad_actual > 0 ? oEmp.antiguedad_actual : 0)));
                sqlCommand2.Parameters.Add(new SqlParameter("@nro_contrato", (object)(oEmp.nro_contrato > 0 ? oEmp.nro_contrato : 0)));
                if (oEmp.fecha_inicio_contrato.Length > 0)
                    sqlCommand2.Parameters.Add(new SqlParameter("@fecha_inicio_contrato", (object)oEmp.fecha_inicio_contrato));
                if (oEmp.fecha_fin_contrato.Length > 0)
                    sqlCommand2.Parameters.Add(new SqlParameter("@fecha_fin_contrato", (object)oEmp.fecha_fin_contrato));
                if (oEmp.nro_nombramiento.Length > 0)
                    sqlCommand2.Parameters.Add(new SqlParameter("@nro_nombramiento", (object)oEmp.nro_nombramiento));
                if (oEmp.fecha_nombramiento.Length > 0)
                    sqlCommand2.Parameters.Add(new SqlParameter("@fecha_nombramiento", (object)oEmp.fecha_nombramiento));
                stringBuilder2.AppendLine("WHERE legajo=@legajo");
                sqlCommand2.Parameters.Add(new SqlParameter("@legajo", (object)oEmp.legajo));
                sqlCommand2.CommandType = CommandType.Text;
                sqlCommand2.CommandText = stringBuilder2.ToString();
                sqlCommand2.Transaction = trx;
                EmpleadoD.Insert_cambios_empleados(oEmp.legajo, usuario, nameof(UpdateTab_Datos_Contrato), "", cn, trx);
                sqlCommand2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlCommand1 = (SqlCommand)null;
            }
            return oEmp.legajo;
        }
        public static int Insert_cambios_empleados(
          int legajo,
          string usuario,
          string operacion,
          string observacion,
          SqlConnection cn,
          SqlTransaction trx)
        {
            Empleado empleado = new Empleado();
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                int num;
                if (legajo > 0)
                {
                    string str = "SELECT isnull(max(nro_item),0)  As item\r\n                                   FROM HIST_CAMBIO_EMPLEADOS (nolock)\r\n                                   WHERE legajo = @legajo";
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = cn;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = str;
                    sqlCommand.Transaction = trx;
                    sqlCommand.Parameters.AddWithValue("@legajo", (object)legajo);
                    num = Convert.ToInt32(sqlCommand.ExecuteScalar()) + 1;
                }
                else
                    num = 1;
                Empleado byPk = EmpleadoD.GetByPk(legajo, cn, trx);
                SqlCommand sqlCommand1 = new SqlCommand();
                sqlCommand1.Connection = cn;
                stringBuilder.AppendLine("INSERT INTO HIST_CAMBIO_EMPLEADOS ");
                stringBuilder.AppendLine("(legajo, nro_item, nombre, fecha_movimiento, fecha_ingreso, cod_tipo_documento, nro_documento,");
                stringBuilder.AppendLine("cuil, tarea, cod_categoria, cod_cargo, cod_seccion, cod_clasif_per, cod_tipo_liq, id_secretaria, id_direccion,");
                stringBuilder.AppendLine("id_oficina, id_regimen, cod_escala_aumento, fecha_nacimiento, sexo, cod_estado_civil, pais_domicilio,");
                stringBuilder.AppendLine("provincia_domicilio, ciudad_domicilio, barrio_domicilio, calle_domicilio, nro_domicilio, piso_domicilio,");
                stringBuilder.AppendLine("dpto_domicilio, monoblock_domicilio, cod_postal, telefonos, celular, email, nro_cta_sb, nro_cta_gastos,");
                stringBuilder.AppendLine("nro_ipam, nro_jubilacion, antiguedad_ant, antiguedad_actual, nro_contrato, fecha_inicio_contrato,");
                stringBuilder.AppendLine("fecha_fin_contrato, nro_nombramiento, fecha_nombramiento, cod_banco, tipo_cuenta, nro_sucursal, nro_caja_ahorro,");
                stringBuilder.AppendLine("nro_cbu, cod_regimen_empleado, imprime_recibo, usuario, id_programa, id_revista)");
                stringBuilder.AppendLine(" values ");
                stringBuilder.AppendLine("(@legajo, @nro_item, @nombre, @fecha_movimiento, @fecha_ingreso, @cod_tipo_documento, @nro_documento, @cuil,");
                stringBuilder.AppendLine("@tarea, @cod_categoria, @cod_cargo,@cod_seccion, @cod_clasif_per, @cod_tipo_liq, @id_secretaria, @id_direccion, @id_oficina,");
                stringBuilder.AppendLine("@id_regimen, @cod_escala_aumento, @fecha_nacimiento, @sexo, @cod_estado_civil, @pais_domicilio, @provincia_domicilio,");
                stringBuilder.AppendLine("@ciudad_domicilio, @barrio_domicilio, @calle_domicilio, @nro_domicilio, @piso_domicilio, @dpto_domicilio, @monoblock_domicilio,");
                stringBuilder.AppendLine("@cod_postal, @telefonos, @celular, @email, @nro_cta_sb, @nro_cta_gastos, @nro_ipam, @nro_jubilacion, @antiguedad_ant,");
                stringBuilder.AppendLine("@antiguedad_actual, @nro_contrato, @fecha_inicio_contrato, @fecha_fin_contrato, @nro_nombramiento, @fecha_nombramiento,");
                stringBuilder.AppendLine("@cod_banco, @tipo_cuenta, @nro_sucursal, @nro_caja_ahorro, @nro_cbu, @cod_regimen_empleado, @imprime_recibo, @usuario, @id_programa, @id_revista) ");
                sqlCommand1.Parameters.Add(new SqlParameter("@legajo", (object)byPk.legajo));
                sqlCommand1.Parameters.Add(new SqlParameter("@nro_item", (object)num));
                sqlCommand1.Parameters.Add(new SqlParameter("@nombre", (object)byPk.nombre));
                sqlCommand1.Parameters.Add(new SqlParameter("@fecha_movimiento", (object)DateTime.Today.ToShortDateString()));
                sqlCommand1.Parameters.Add(new SqlParameter("@fecha_ingreso", byPk.fecha_ingreso != null ? (object)byPk.fecha_ingreso : (object)""));
                sqlCommand1.Parameters.Add(new SqlParameter("@cod_tipo_documento", (object)(byPk.cod_tipo_documento > 0 ? byPk.cod_tipo_documento : 0)));
                sqlCommand1.Parameters.Add(new SqlParameter("@nro_documento", byPk.nro_documento != null ? (object)byPk.nro_documento : (object)""));
                sqlCommand1.Parameters.Add(new SqlParameter("@cuil", byPk.cuil != null ? (object)byPk.cuil : (object)""));
                sqlCommand1.Parameters.Add(new SqlParameter("@tarea", byPk.tarea != null ? (object)byPk.tarea : (object)""));
                sqlCommand1.Parameters.Add(new SqlParameter("@cod_categoria", (object)(byPk.cod_categoria > 0 ? byPk.cod_categoria : 0)));
                sqlCommand1.Parameters.Add(new SqlParameter("@cod_cargo", (object)(byPk.cod_cargo > 0 ? byPk.cod_cargo : 0)));
                sqlCommand1.Parameters.Add(new SqlParameter("@cod_seccion", (object)(byPk.cod_seccion > 0 ? byPk.cod_seccion : 0)));
                sqlCommand1.Parameters.Add(new SqlParameter("@cod_clasif_per", (object)(byPk.cod_clasif_per > 0 ? byPk.cod_clasif_per : 0)));
                sqlCommand1.Parameters.Add(new SqlParameter("@cod_tipo_liq", (object)(byPk.cod_tipo_liq > 0 ? byPk.cod_tipo_liq : 0)));
                sqlCommand1.Parameters.Add(new SqlParameter("@id_secretaria", (object)(byPk.id_secretaria > 0 ? byPk.id_secretaria : 0)));
                sqlCommand1.Parameters.Add(new SqlParameter("@id_direccion", (object)(byPk.id_direccion > 0 ? byPk.id_direccion : 0)));
                sqlCommand1.Parameters.Add(new SqlParameter("@id_oficina", (object)(byPk.id_oficina > 0 ? byPk.id_oficina : 0)));
                sqlCommand1.Parameters.Add(new SqlParameter("@id_regimen", (object)(byPk.id_regimen > 0 ? byPk.id_regimen : 0)));
                sqlCommand1.Parameters.Add(new SqlParameter("@cod_escala_aumento", (object)(byPk.cod_escala_aumento > 0 ? byPk.cod_escala_aumento : 0)));
                sqlCommand1.Parameters.Add(new SqlParameter("@fecha_nacimiento", byPk.fecha_nacimiento != null ? (object)byPk.fecha_nacimiento : (object)""));
                sqlCommand1.Parameters.Add(new SqlParameter("@sexo", byPk.sexo != null ? (object)byPk.sexo : (object)""));
                sqlCommand1.Parameters.Add(new SqlParameter("@cod_estado_civil", (object)(byPk.cod_estado_civil > 0 ? byPk.cod_estado_civil : 0)));
                sqlCommand1.Parameters.Add(new SqlParameter("@pais_domicilio", byPk.pais_domicilio != null ? (object)byPk.pais_domicilio : (object)""));
                sqlCommand1.Parameters.Add(new SqlParameter("@provincia_domicilio", byPk.provincia_domicilio != null ? (object)byPk.provincia_domicilio : (object)""));
                sqlCommand1.Parameters.Add(new SqlParameter("@ciudad_domicilio", byPk.ciudad_domicilio != null ? (object)byPk.ciudad_domicilio : (object)""));
                sqlCommand1.Parameters.Add(new SqlParameter("@barrio_domicilio", byPk.barrio_domicilio != null ? (object)byPk.barrio_domicilio : (object)""));
                sqlCommand1.Parameters.Add(new SqlParameter("@calle_domicilio", byPk.calle_domicilio != null ? (object)byPk.calle_domicilio : (object)""));
                sqlCommand1.Parameters.Add(new SqlParameter("@nro_domicilio", byPk.nro_domicilio != null ? (object)byPk.nro_domicilio : (object)""));
                sqlCommand1.Parameters.Add(new SqlParameter("@piso_domicilio", byPk.piso_domicilio != null ? (object)byPk.piso_domicilio : (object)""));
                sqlCommand1.Parameters.Add(new SqlParameter("@dpto_domicilio", byPk.dpto_domicilio != null ? (object)byPk.dpto_domicilio : (object)""));
                sqlCommand1.Parameters.Add(new SqlParameter("@monoblock_domicilio", byPk.monoblock_domicilio != null ? (object)byPk.monoblock_domicilio : (object)""));
                sqlCommand1.Parameters.Add(new SqlParameter("@cod_postal", byPk.cod_postal != null ? (object)byPk.cod_postal : (object)""));
                sqlCommand1.Parameters.Add(new SqlParameter("@telefonos", byPk.telefonos != null ? (object)byPk.telefonos : (object)""));
                sqlCommand1.Parameters.Add(new SqlParameter("@celular", byPk.celular != null ? (object)byPk.celular : (object)""));
                sqlCommand1.Parameters.Add(new SqlParameter("@email", byPk.email != null ? (object)byPk.email : (object)""));
                sqlCommand1.Parameters.Add(new SqlParameter("@nro_cta_sb", byPk.nro_cta_sb != null ? (object)byPk.nro_cta_sb : (object)""));
                sqlCommand1.Parameters.Add(new SqlParameter("@nro_cta_gastos", byPk.nro_cta_gastos != null ? (object)byPk.nro_cta_gastos : (object)""));
                sqlCommand1.Parameters.Add(new SqlParameter("@nro_ipam", byPk.nro_ipam != null ? (object)byPk.nro_ipam : (object)""));
                sqlCommand1.Parameters.Add(new SqlParameter("@nro_jubilacion", byPk.nro_jubilacion != null ? (object)byPk.nro_jubilacion : (object)""));
                sqlCommand1.Parameters.Add(new SqlParameter("@antiguedad_ant", (object)(byPk.antiguedad_ant > 0 ? byPk.antiguedad_ant : 0)));
                sqlCommand1.Parameters.Add(new SqlParameter("@antiguedad_actual", (object)(byPk.antiguedad_actual > 0 ? byPk.antiguedad_actual : 0)));
                sqlCommand1.Parameters.Add(new SqlParameter("@nro_contrato", (object)(byPk.nro_contrato > 0 ? byPk.nro_contrato : 0)));
                sqlCommand1.Parameters.Add(new SqlParameter("@fecha_inicio_contrato", byPk.fecha_inicio_contrato != null ? (object)byPk.fecha_inicio_contrato : (object)""));
                sqlCommand1.Parameters.Add(new SqlParameter("@fecha_fin_contrato", byPk.fecha_fin_contrato != null ? (object)byPk.fecha_fin_contrato : (object)""));
                sqlCommand1.Parameters.Add(new SqlParameter("@nro_nombramiento", byPk.nro_nombramiento != null ? (object)byPk.nro_nombramiento : (object)""));
                sqlCommand1.Parameters.Add(new SqlParameter("@fecha_nombramiento", byPk.fecha_nombramiento != null ? (object)byPk.fecha_nombramiento : (object)""));
                sqlCommand1.Parameters.Add(new SqlParameter("@cod_banco", (object)(byPk.cod_banco > 0 ? byPk.cod_banco : 0)));
                sqlCommand1.Parameters.Add(new SqlParameter("@tipo_cuenta", byPk.tipo_cuenta != null ? (object)byPk.tipo_cuenta : (object)""));
                sqlCommand1.Parameters.Add(new SqlParameter("@nro_sucursal", byPk.nro_sucursal != null ? (object)byPk.nro_sucursal : (object)""));
                sqlCommand1.Parameters.Add(new SqlParameter("@nro_caja_ahorro", byPk.nro_caja_ahorro != null ? (object)byPk.nro_caja_ahorro : (object)""));
                sqlCommand1.Parameters.Add(new SqlParameter("@nro_cbu", byPk.nro_cbu != null ? (object)byPk.nro_cbu : (object)""));
                sqlCommand1.Parameters.Add(new SqlParameter("@cod_regimen_empleado", (object)(byPk.cod_regimen_empleado > 0 ? byPk.cod_regimen_empleado : 0)));
                sqlCommand1.Parameters.Add(new SqlParameter("@imprime_recibo", (object)byPk.imprime_recibo));
                sqlCommand1.Parameters.Add(new SqlParameter("@usuario", (object)usuario));
                sqlCommand1.Parameters.Add(new SqlParameter("@id_programa", (object)byPk.id_programa));
                sqlCommand1.Parameters.Add(new SqlParameter("@id_revista", (object)byPk.id_revista));
                sqlCommand1.CommandType = CommandType.Text;
                sqlCommand1.CommandText = stringBuilder.ToString();
                sqlCommand1.Transaction = trx;
                sqlCommand1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
            return legajo;
        }
        public static int UpdateDatosEmpleado(
  Empleado oEmp,
  string usuario,
  SqlConnection cn,
  SqlTransaction trx)
        {
            SqlCommand sqlCommand1 = (SqlCommand)null;
            try
            {
                StringBuilder stringBuilder1 = new StringBuilder();
                StringBuilder stringBuilder2 = new StringBuilder();
                string str = "SELECT max(cod_categoria)\r\n                        FROM Empleados where legajo=" + oEmp.legajo.ToString();
                SqlCommand sqlCommand2 = new SqlCommand();
                sqlCommand2.Connection = cn;
                sqlCommand2.CommandType = CommandType.Text;
                sqlCommand2.CommandText = str;
                sqlCommand2.Transaction = trx;
                int int32 = Convert.ToInt32(sqlCommand2.ExecuteScalar());
                stringBuilder1.AppendLine("UPDATE EMPLEADOS SET ");
                stringBuilder1.AppendLine("nombre=@nombre,");
                stringBuilder1.AppendLine("fecha_alta_registro=@fecha_alta_registro,");
                if (oEmp.fecha_ingreso.Length != 0)
                    stringBuilder1.AppendLine("fecha_ingreso=@fecha_ingreso,");
                else
                    stringBuilder1.AppendLine("fecha_ingreso=null,");
                stringBuilder1.AppendLine("cod_tipo_documento=@cod_tipo_documento,");
                stringBuilder1.AppendLine("nro_documento=@nro_documento,");
                stringBuilder1.AppendLine("cuil=@cuil,");
                stringBuilder1.AppendLine("tarea=@tarea,");
                stringBuilder1.AppendLine("cod_categoria=@cod_categoria,");
                stringBuilder1.AppendLine("cod_cargo=@cod_cargo,");
                stringBuilder1.AppendLine("nro_cta_sb=@nro_cta_sb,");
                stringBuilder1.AppendLine("nro_cta_gastos= @nro_cta_gastos,");
                stringBuilder1.AppendLine("cod_seccion=@cod_seccion,");
                stringBuilder1.AppendLine("cod_clasif_per=@cod_clasif_per,");
                stringBuilder1.AppendLine("cod_tipo_liq=@cod_tipo_liq,");
                stringBuilder1.AppendLine("id_secretaria=@id_secretaria,");
                stringBuilder1.AppendLine("id_direccion=@id_direccion,");
                stringBuilder1.AppendLine("id_oficina=@id_oficina,");
                stringBuilder1.AppendLine("id_regimen=@id_regimen,");
                stringBuilder1.AppendLine("cod_escala_aumento=@cod_escala_aumento ");
                if (oEmp.fecha_baja.Length != 0)
                    stringBuilder1.AppendLine(",fecha_baja=@fecha_baja");
                else
                    stringBuilder1.AppendLine(",fecha_baja=null");
                stringBuilder1.AppendLine(",cod_regimen_empleado=@cod_regimen_empleado");
                stringBuilder1.AppendLine(",imprime_recibo=@imprime_recibo");
                stringBuilder1.AppendLine(",id_programa=@id_programa");
                stringBuilder1.AppendLine(",id_revista=@id_revista");
                if (oEmp.fecha_revista.Length != 0)
                    stringBuilder1.AppendLine(",fecha_revista=@fecha_revista");
                else
                    stringBuilder1.AppendLine(",fecha_revista=null");
                stringBuilder1.AppendLine(",activo=@activo");
                stringBuilder1.AppendLine(" WHERE legajo=@legajo");
                SqlCommand sqlCommand3 = new SqlCommand();
                sqlCommand3.Connection = cn;
                sqlCommand3.CommandType = CommandType.Text;
                sqlCommand3.CommandText = stringBuilder1.ToString();
                sqlCommand3.Transaction = trx;
                sqlCommand3.Parameters.Add(new SqlParameter("@legajo", (object)oEmp.legajo));
                sqlCommand3.Parameters.Add(new SqlParameter("@nombre", (object)oEmp.nombre));
                sqlCommand3.Parameters.Add(new SqlParameter("@fecha_alta_registro", oEmp.fecha_alta_registro.Length != 0 ? (object)oEmp.fecha_alta_registro : (object)""));
                if (oEmp.fecha_ingreso.Length != 0)
                    sqlCommand3.Parameters.Add(new SqlParameter("@fecha_ingreso", (object)oEmp.fecha_ingreso));
                sqlCommand3.Parameters.Add(new SqlParameter("@cod_tipo_documento", (object)(oEmp.cod_tipo_documento > 0 ? oEmp.cod_tipo_documento : 0)));
                sqlCommand3.Parameters.Add(new SqlParameter("@nro_documento", oEmp.nro_documento != null ? (object)oEmp.nro_documento : (object)(string)null));
                sqlCommand3.Parameters.Add(new SqlParameter("@cuil", oEmp.cuil != null ? (object)oEmp.cuil : (object)(string)null));
                sqlCommand3.Parameters.Add(new SqlParameter("@tarea", oEmp.tarea != null ? (object)oEmp.tarea : (object)(string)null));
                sqlCommand3.Parameters.Add(new SqlParameter("@cod_cargo", (object)(oEmp.cod_cargo > 0 ? oEmp.cod_cargo : 0)));
                sqlCommand3.Parameters.Add(new SqlParameter("@cod_categoria", (object)(oEmp.cod_categoria > 0 ? oEmp.cod_categoria : 0)));
                sqlCommand3.Parameters.Add(new SqlParameter("@nro_cta_sb", oEmp.cod_cargo > 0 ? (object)ConsultaEmpleadoD.GetNro_cta_sb(oEmp.cod_cargo) : (object)""));
                sqlCommand3.Parameters.Add(new SqlParameter("@nro_cta_gastos", oEmp.nro_cta_gastos != null ? (object)oEmp.nro_cta_gastos : (object)(string)null));
                sqlCommand3.Parameters.Add(new SqlParameter("@cod_seccion", (object)(oEmp.cod_seccion > 0 ? oEmp.cod_seccion : 0)));
                sqlCommand3.Parameters.Add(new SqlParameter("@cod_clasif_per", (object)(oEmp.cod_clasif_per > 0 ? oEmp.cod_clasif_per : 0)));
                sqlCommand3.Parameters.Add(new SqlParameter("@cod_tipo_liq", (object)(oEmp.cod_tipo_liq > 0 ? oEmp.cod_tipo_liq : 0)));
                sqlCommand3.Parameters.Add(new SqlParameter("@id_secretaria", (object)(oEmp.id_secretaria > 0 ? oEmp.id_secretaria : 0)));
                sqlCommand3.Parameters.Add(new SqlParameter("@id_direccion", (object)(oEmp.id_direccion > 0 ? oEmp.id_direccion : 0)));
                sqlCommand3.Parameters.Add(new SqlParameter("@id_oficina", (object)(oEmp.id_oficina > 0 ? oEmp.id_oficina : 0)));
                sqlCommand3.Parameters.Add(new SqlParameter("@id_regimen", (object)(oEmp.id_regimen > 0 ? oEmp.id_regimen : 0)));
                sqlCommand3.Parameters.Add(new SqlParameter("@cod_escala_aumento", (object)(oEmp.cod_escala_aumento > 0 ? oEmp.cod_escala_aumento : 0)));
                sqlCommand3.Parameters.Add(new SqlParameter("@cod_regimen_empleado", (object)(oEmp.cod_regimen_empleado > 0 ? oEmp.cod_regimen_empleado : 0)));
                if (oEmp.fecha_baja.Length != 0)
                    sqlCommand3.Parameters.Add(new SqlParameter("@fecha_baja", (object)oEmp.fecha_baja));
                sqlCommand3.Parameters.Add(new SqlParameter("@imprime_recibo", (object)oEmp.imprime_recibo));
                sqlCommand3.Parameters.Add(new SqlParameter("@id_programa", (object)oEmp.id_programa));
                sqlCommand3.Parameters.Add(new SqlParameter("@id_revista", (object)oEmp.id_revista));
                if (oEmp.fecha_revista.Length != 0)
                    sqlCommand3.Parameters.Add(new SqlParameter("@fecha_revista", (object)oEmp.fecha_revista));
                sqlCommand3.Parameters.Add(new SqlParameter("@activo", (object)oEmp.activo));
                EmpleadoD.Insert_cambios_empleados(oEmp.legajo, usuario, nameof(UpdateDatosEmpleado), "", cn, trx);
                sqlCommand3.ExecuteNonQuery();
                if (int32 != oEmp.cod_categoria)
                {
                    EmpleadoD.Cambios_categoria_empleado(oEmp.legajo, int32, usuario, "antes", "", cn, trx);
                    EmpleadoD.Cambios_categoria_empleado(oEmp.legajo, oEmp.cod_categoria, usuario, "nueva", "", cn, trx);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlCommand1 = (SqlCommand)null;
            }
            return oEmp.legajo;
        }
        public static int UpdateTab_Datos_Particulares(
  Empleado oEmp,
  string usuario,
  SqlConnection cn,
  SqlTransaction trx)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = cn;
                stringBuilder.AppendLine("UPDATE EMPLEADOS set ");
                stringBuilder.AppendLine("fecha_nacimiento=@fecha_nacimiento,");
                stringBuilder.AppendLine("sexo=@sexo,");
                stringBuilder.AppendLine("cod_estado_civil=@cod_estado_civil,");
                stringBuilder.AppendLine("pais_domicilio=@pais_domicilio,");
                stringBuilder.AppendLine("provincia_domicilio=@provincia_domicilio,");
                stringBuilder.AppendLine("ciudad_domicilio=@ciudad_domicilio,");
                stringBuilder.AppendLine("barrio_domicilio=@barrio_domicilio,");
                stringBuilder.AppendLine("calle_domicilio=@calle_domicilio,");
                stringBuilder.AppendLine("nro_domicilio=@nro_domicilio,");
                stringBuilder.AppendLine("piso_domicilio=@piso_domicilio,");
                stringBuilder.AppendLine("dpto_domicilio=@dpto_domicilio,");
                stringBuilder.AppendLine("monoblock_domicilio=@monoblock_domicilio,");
                stringBuilder.AppendLine("cod_postal=@cod_postal,");
                stringBuilder.AppendLine("telefonos=@telefonos,");
                stringBuilder.AppendLine("celular=@celular, ");
                stringBuilder.AppendLine("email=@email");
                sqlCommand.Parameters.Add(new SqlParameter("@fecha_nacimiento", oEmp.fecha_nacimiento != null ? (object)oEmp.fecha_nacimiento : (object)(string)null));
                sqlCommand.Parameters.Add(new SqlParameter("@sexo", oEmp.sexo != null ? (object)oEmp.sexo : (object)(string)null));
                sqlCommand.Parameters.Add(new SqlParameter("@cod_estado_civil", (object)(oEmp.cod_estado_civil > 0 ? oEmp.cod_estado_civil : 0)));
                sqlCommand.Parameters.Add(new SqlParameter("@pais_domicilio", oEmp.pais_domicilio != null ? (object)oEmp.pais_domicilio : (object)(string)null));
                sqlCommand.Parameters.Add(new SqlParameter("@provincia_domicilio", oEmp.provincia_domicilio != null ? (object)oEmp.provincia_domicilio : (object)(string)null));
                sqlCommand.Parameters.Add(new SqlParameter("@ciudad_domicilio", oEmp.ciudad_domicilio != null ? (object)oEmp.ciudad_domicilio : (object)(string)null));
                sqlCommand.Parameters.Add(new SqlParameter("@barrio_domicilio", oEmp.barrio_domicilio != null ? (object)oEmp.barrio_domicilio : (object)(string)null));
                sqlCommand.Parameters.Add(new SqlParameter("@calle_domicilio", oEmp.calle_domicilio != null ? (object)oEmp.calle_domicilio : (object)(string)null));
                sqlCommand.Parameters.Add(new SqlParameter("@nro_domicilio", oEmp.nro_domicilio != null ? (object)oEmp.nro_domicilio : (object)(string)null));
                sqlCommand.Parameters.Add(new SqlParameter("@piso_domicilio", oEmp.piso_domicilio != null ? (object)oEmp.piso_domicilio : (object)(string)null));
                sqlCommand.Parameters.Add(new SqlParameter("@dpto_domicilio", oEmp.dpto_domicilio != null ? (object)oEmp.dpto_domicilio : (object)(string)null));
                sqlCommand.Parameters.Add(new SqlParameter("@monoblock_domicilio", oEmp.monoblock_domicilio != null ? (object)oEmp.monoblock_domicilio : (object)(string)null));
                sqlCommand.Parameters.Add(new SqlParameter("@cod_postal", oEmp.cod_postal != null ? (object)oEmp.cod_postal : (object)(string)null));
                sqlCommand.Parameters.Add(new SqlParameter("@telefonos", oEmp.telefonos != null ? (object)oEmp.telefonos : (object)(string)null));
                sqlCommand.Parameters.Add(new SqlParameter("@celular", oEmp.celular != null ? (object)oEmp.celular : (object)(string)null));
                sqlCommand.Parameters.Add(new SqlParameter("@email", oEmp.email != null ? (object)oEmp.email : (object)(string)null));
                stringBuilder.AppendLine(" WHERE legajo=@legajo");
                sqlCommand.Parameters.Add(new SqlParameter("@legajo", (object)oEmp.legajo));
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = stringBuilder.ToString();
                sqlCommand.Transaction = trx;
                EmpleadoD.Insert_cambios_empleados(oEmp.legajo, usuario, nameof(UpdateTab_Datos_Particulares), "", cn, trx);
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
            return oEmp.legajo;
        }
        public static int UpdateTab_Datos_Banco(
  Empleado oEmp,
  string usuario,
  SqlConnection cn,
  SqlTransaction trx)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = cn;
                stringBuilder.AppendLine("UPDATE EMPLEADOS set ");
                stringBuilder.AppendLine("cod_banco=@cod_banco,");
                stringBuilder.AppendLine("tipo_cuenta=@tipo_cuenta,");
                stringBuilder.AppendLine("nro_sucursal=@nro_sucursal,");
                stringBuilder.AppendLine("nro_caja_ahorro=@nro_caja_ahorro,");
                stringBuilder.AppendLine("nro_cbu=@nro_cbu");
                sqlCommand.Parameters.Add(new SqlParameter("@cod_banco", (object)(oEmp.cod_banco > 0 ? oEmp.cod_banco : 0)));
                sqlCommand.Parameters.Add(new SqlParameter("@tipo_cuenta", oEmp.tipo_cuenta != null ? (object)oEmp.tipo_cuenta : (object)(string)null));
                sqlCommand.Parameters.Add(new SqlParameter("@nro_sucursal", oEmp.nro_sucursal != null ? (object)oEmp.nro_sucursal : (object)(string)null));
                sqlCommand.Parameters.Add(new SqlParameter("@nro_caja_ahorro", oEmp.nro_caja_ahorro != null ? (object)oEmp.nro_caja_ahorro : (object)(string)null));
                sqlCommand.Parameters.Add(new SqlParameter("@nro_cbu", oEmp.nro_cbu != null ? (object)oEmp.nro_cbu : (object)(string)null));
                stringBuilder.AppendLine(" WHERE legajo=@legajo");
                sqlCommand.Parameters.Add(new SqlParameter("@legajo", (object)oEmp.legajo));
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = stringBuilder.ToString();
                sqlCommand.Transaction = trx;
                EmpleadoD.Insert_cambios_empleados(oEmp.legajo, usuario, nameof(UpdateTab_Datos_Banco), "", cn, trx);
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
            return oEmp.legajo;
        }

    }

}



#endregion


