using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public class Empleados : DALBase
    {
        public int legajo { get; set; }
        public DateTime fecha_alta_registro { get; set; }
        public string nombre { get; set; }
        public int cod_tipo_documento { get; set; }
        public string nro_documento { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public string sexo { get; set; }
        public string pais_domicilio { get; set; }
        public string provincia_domicilio { get; set; }
        public string ciudad_domicilio { get; set; }
        public string barrio_domicilio { get; set; }
        public string calle_domicilio { get; set; }
        public int nro_domicilio { get; set; }
        public string piso_domicilio { get; set; }
        public string dpto_domicilio { get; set; }
        public string monoblock_domicilio { get; set; }
        public string telefonos { get; set; }
        public string cod_postal { get; set; }
        public int cod_estado_civil { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public string tarea { get; set; }
        public int cod_seccion { get; set; }
        public int cod_categoria { get; set; }
        public int cod_cargo { get; set; }
        public int cod_banco { get; set; }
        public string nro_sucursal { get; set; }
        public string tipo_cuenta { get; set; }
        public string nro_caja_ahorro { get; set; }
        public string nro_cbu { get; set; }
        public string nro_ipam { get; set; }
        public string cuil { get; set; }
        public string nro_jubilacion { get; set; }
        public int antiguedad_ant { get; set; }
        public int antiguedad_actual { get; set; }
        public int cod_clasif_per { get; set; }
        public int cod_tipo_liq { get; set; }
        public int nro_ult_liq { get; set; }
        public int anio_ult_liq { get; set; }
        public string nro_cta_sb { get; set; }
        public string nro_cta_gastos { get; set; }
        public DateTime fecha_baja { get; set; }
        public int nro_contrato { get; set; }
        public DateTime fecha_inicio_contrato { get; set; }
        public DateTime fecha_fin_contrato { get; set; }
        public bool listar { get; set; }
        public Int16 id_regimen { get; set; }
        public int id_secretaria { get; set; }
        public int id_direccion { get; set; }
        public string nro_nombramiento { get; set; }
        public DateTime fecha_nombramiento { get; set; }
        public string usuario { get; set; }
        public int cod_escala_aumento { get; set; }
        public int cod_regimen_empleado { get; set; }
        public int id_oficina { get; set; }
        public string celular { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string passTemp { get; set; }
        public Int16 imprime_recibo { get; set; }
        public int id_programa { get; set; }
        public int id_revista { get; set; }
        public DateTime fecha_revista { get; set; }
        public bool activo { get; set; }
        public int licenciagenerada { get; set; }
        public int licenciadisponible { get; set; }
        public int licenciausadas { get; set; }
        public int razonesparticulares { get; set; }

        public string SECRETARIA { get; set; }
        public string DIRECCION { get; set; }
        public string OFICINA { get; set; }
        public string PROGRAMA { get; set; }

        public Empleados()
        {
            legajo = 0;
            fecha_alta_registro = DateTime.Now;
            nombre = string.Empty;
            cod_tipo_documento = 0;
            nro_documento = string.Empty;
            fecha_nacimiento = DateTime.Now;
            sexo = string.Empty;
            pais_domicilio = string.Empty;
            provincia_domicilio = string.Empty;
            ciudad_domicilio = string.Empty;
            barrio_domicilio = string.Empty;
            calle_domicilio = string.Empty;
            nro_domicilio = 0;
            piso_domicilio = string.Empty;
            dpto_domicilio = string.Empty;
            monoblock_domicilio = string.Empty;
            telefonos = string.Empty;
            cod_postal = string.Empty;
            cod_estado_civil = 0;
            fecha_ingreso = DateTime.Now;
            tarea = string.Empty;
            cod_seccion = 0;
            cod_categoria = 0;
            cod_cargo = 0;
            cod_banco = 0;
            nro_sucursal = string.Empty;
            tipo_cuenta = string.Empty;
            nro_caja_ahorro = string.Empty;
            nro_cbu = string.Empty;
            nro_ipam = string.Empty;
            cuil = string.Empty;
            nro_jubilacion = string.Empty;
            antiguedad_ant = 0;
            antiguedad_actual = 0;
            cod_clasif_per = 0;
            cod_tipo_liq = 0;
            nro_ult_liq = 0;
            anio_ult_liq = 0;
            nro_cta_sb = string.Empty;
            nro_cta_gastos = string.Empty;
            fecha_baja = DateTime.Now;
            nro_contrato = 0;
            fecha_inicio_contrato = DateTime.Now;
            fecha_fin_contrato = DateTime.Now;
            listar = false;
            id_regimen = 0;
            id_secretaria = 0;
            id_direccion = 0;
            nro_nombramiento = string.Empty;
            fecha_nombramiento = DateTime.Now;
            usuario = string.Empty;
            cod_escala_aumento = 0;
            cod_regimen_empleado = 0;
            id_oficina = 0;
            celular = string.Empty;
            email = string.Empty;
            password = string.Empty;
            passTemp = string.Empty;
            imprime_recibo = 0;
            id_programa = 0;
            id_revista = 0;
            fecha_revista = DateTime.Now;
            activo = false;
            licenciagenerada = 0;
            licenciadisponible = 0;
            licenciausadas = 0;
            razonesparticulares = 0;
        }

        private static List<Empleados> mapeo(SqlDataReader dr)
        {
            List<Empleados> lst = new List<Empleados>();
            Empleados obj;
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
                while (dr.Read())
                {
                    obj = new Empleados();
                    if (!dr.IsDBNull(legajo)) { obj.legajo = dr.GetInt32(legajo); }
                    if (!dr.IsDBNull(fecha_alta_registro)) { obj.fecha_alta_registro = dr.GetDateTime(fecha_alta_registro); }
                    if (!dr.IsDBNull(nombre)) { obj.nombre = dr.GetString(nombre); }
                    if (!dr.IsDBNull(cod_tipo_documento)) { obj.cod_tipo_documento = dr.GetInt32(cod_tipo_documento); }
                    if (!dr.IsDBNull(nro_documento)) { obj.nro_documento = dr.GetString(nro_documento); }
                    if (!dr.IsDBNull(fecha_nacimiento)) { obj.fecha_nacimiento = dr.GetDateTime(fecha_nacimiento); }
                    if (!dr.IsDBNull(sexo)) { obj.sexo = dr.GetString(sexo); }
                    if (!dr.IsDBNull(pais_domicilio)) { obj.pais_domicilio = dr.GetString(pais_domicilio); }
                    if (!dr.IsDBNull(provincia_domicilio)) { obj.provincia_domicilio = dr.GetString(provincia_domicilio); }
                    if (!dr.IsDBNull(ciudad_domicilio)) { obj.ciudad_domicilio = dr.GetString(ciudad_domicilio); }
                    if (!dr.IsDBNull(barrio_domicilio)) { obj.barrio_domicilio = dr.GetString(barrio_domicilio); }
                    if (!dr.IsDBNull(calle_domicilio)) { obj.calle_domicilio = dr.GetString(calle_domicilio); }
                    if (!dr.IsDBNull(nro_domicilio)) { obj.nro_domicilio = dr.GetInt32(nro_domicilio); }
                    if (!dr.IsDBNull(piso_domicilio)) { obj.piso_domicilio = dr.GetString(piso_domicilio); }
                    if (!dr.IsDBNull(dpto_domicilio)) { obj.dpto_domicilio = dr.GetString(dpto_domicilio); }
                    if (!dr.IsDBNull(monoblock_domicilio)) { obj.monoblock_domicilio = dr.GetString(monoblock_domicilio); }
                    if (!dr.IsDBNull(telefonos)) { obj.telefonos = dr.GetString(telefonos); }
                    if (!dr.IsDBNull(cod_postal)) { obj.cod_postal = dr.GetString(cod_postal); }
                    if (!dr.IsDBNull(cod_estado_civil)) { obj.cod_estado_civil = dr.GetInt32(cod_estado_civil); }
                    if (!dr.IsDBNull(fecha_ingreso)) { obj.fecha_ingreso = dr.GetDateTime(fecha_ingreso); }
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
                    if (!dr.IsDBNull(fecha_baja)) { obj.fecha_baja = dr.GetDateTime(fecha_baja); }
                    if (!dr.IsDBNull(nro_contrato)) { obj.nro_contrato = dr.GetInt32(nro_contrato); }
                    if (!dr.IsDBNull(fecha_inicio_contrato)) { obj.fecha_inicio_contrato = dr.GetDateTime(fecha_inicio_contrato); }
                    if (!dr.IsDBNull(fecha_fin_contrato)) { obj.fecha_fin_contrato = dr.GetDateTime(fecha_fin_contrato); }
                    if (!dr.IsDBNull(listar)) { obj.listar = dr.GetBoolean(listar); }
                    if (!dr.IsDBNull(id_regimen)) { obj.id_regimen = dr.GetInt16(id_regimen); }
                    if (!dr.IsDBNull(id_secretaria)) { obj.id_secretaria = dr.GetInt32(id_secretaria); }
                    if (!dr.IsDBNull(id_direccion)) { obj.id_direccion = dr.GetInt32(id_direccion); }
                    if (!dr.IsDBNull(nro_nombramiento)) { obj.nro_nombramiento = dr.GetString(nro_nombramiento); }
                    if (!dr.IsDBNull(fecha_nombramiento)) { obj.fecha_nombramiento = dr.GetDateTime(fecha_nombramiento); }
                    if (!dr.IsDBNull(usuario)) { obj.usuario = dr.GetString(usuario); }
                    if (!dr.IsDBNull(cod_escala_aumento)) { obj.cod_escala_aumento = dr.GetInt32(cod_escala_aumento); }
                    if (!dr.IsDBNull(cod_regimen_empleado)) { obj.cod_regimen_empleado = dr.GetInt32(cod_regimen_empleado); }
                    if (!dr.IsDBNull(id_oficina)) { obj.id_oficina = dr.GetInt32(id_oficina); }
                    if (!dr.IsDBNull(celular)) { obj.celular = dr.GetString(celular); }
                    if (!dr.IsDBNull(email)) { obj.email = dr.GetString(email); }
                    if (!dr.IsDBNull(password)) { obj.password = dr.GetString(password); }
                    if (!dr.IsDBNull(passTemp)) { obj.passTemp = dr.GetString(passTemp); }
                    if (!dr.IsDBNull(imprime_recibo)) { obj.imprime_recibo = dr.GetInt16(imprime_recibo); }
                    if (!dr.IsDBNull(id_programa)) { obj.id_programa = dr.GetInt32(id_programa); }
                    if (!dr.IsDBNull(id_revista)) { obj.id_revista = dr.GetInt32(id_revista); }
                    if (!dr.IsDBNull(fecha_revista)) { obj.fecha_revista = dr.GetDateTime(fecha_revista); }
                    if (!dr.IsDBNull(activo)) { obj.activo = dr.GetBoolean(activo); }
                    if (!dr.IsDBNull(licenciagenerada)) { obj.licenciagenerada = dr.GetInt32(licenciagenerada); }
                    if (!dr.IsDBNull(licenciadisponible)) { obj.licenciadisponible = dr.GetInt32(licenciadisponible); }
                    if (!dr.IsDBNull(licenciausadas)) { obj.licenciausadas = dr.GetInt32(licenciausadas); }
                    if (!dr.IsDBNull(razonesparticulares)) { obj.razonesparticulares = dr.GetInt32(razonesparticulares); }
                    lst.Add(obj);
                }
            }
            return lst;
        }

        public static List<Empleados> read()
        {
            try
            {
                List<Empleados> lst = new List<Empleados>();
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT *FROM Empleados";
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
        public static List<Empleados> getCumples(int mes, int dia)
        {
            try
            {
                List<Empleados> lst = new List<Empleados>();
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = 
                        @"SELECT A.*, B.Descripcion AS SECRETARIA, 
                        C.Descripcion AS DIRECCION, D.nombre_oficina AS OFICINA,
                        E.Programa AS PROGRAMA
                        FROM EMPLEADOS A
                        INNER JOIN SECRETARIA B ON A.id_secretaria=B.Id_secretaria
                        INNER JOIN DIRECCION C ON A.id_direccion=C.Id_direccion
                        INNER JOIN OFICINAS D ON A.id_oficina = D.codigo_oficina
                        INNER JOIN PROGRAMAS_PUBLICOS E ON A.id_programa=E.Id_programa
                        WHERE  MONTH(fecha_nacimiento)=@MES AND DAY(FECHA_NACIMIENTO)=@DIA
                        AND A.activo=1";
                    cmd.Parameters.AddWithValue("@MES", mes);
                    cmd.Parameters.AddWithValue("@DIA", dia);
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
        public static List<Empleados> getCumplesSecretaria(int mes, int dia, 
            int idSecretaria)
        {
            try
            {
                List<Empleados> lst = new List<Empleados>();
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        @"SELECT A.*, B.Descripcion AS SECRETARIA, 
                        C.Descripcion AS DIRECCION, D.nombre_oficina AS OFICINA,
                        E.Programa AS PROGRAMA
                        FROM EMPLEADOS A
                        INNER JOIN SECRETARIA B ON A.id_secretaria=B.Id_secretaria
                        INNER JOIN DIRECCION C ON A.id_direccion=C.Id_direccion
                        INNER JOIN OFICINAS D ON A.id_oficina = D.codigo_oficina
                        INNER JOIN PROGRAMAS_PUBLICOS E ON A.id_programa=E.Id_programa
                        WHERE  MONTH(fecha_nacimiento)=@MES AND DAY(FECHA_NACIMIENTO)=@DIA
                        AND A.activo=1 AND A.Id_secretaria=@Id_secretaria";
                    cmd.Parameters.AddWithValue("@MES", mes);
                    cmd.Parameters.AddWithValue("@DIA", dia);
                    cmd.Parameters.AddWithValue("@Id_secretaria", idSecretaria);
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
        public static List<Empleados> getCumplesDireccion(int mes, int dia,
    int idDireccion)
        {
            try
            {
                List<Empleados> lst = new List<Empleados>();
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        @"SELECT A.*, B.Descripcion AS SECRETARIA, 
                        C.Descripcion AS DIRECCION, D.nombre_oficina AS OFICINA,
                        E.Programa AS PROGRAMA
                        FROM EMPLEADOS A
                        INNER JOIN SECRETARIA B ON A.id_secretaria=B.Id_secretaria
                        INNER JOIN DIRECCION C ON A.id_direccion=C.Id_direccion
                        INNER JOIN OFICINAS D ON A.id_oficina = D.codigo_oficina
                        INNER JOIN PROGRAMAS_PUBLICOS E ON A.id_programa=E.Id_programa
                        WHERE  MONTH(fecha_nacimiento)=@MES AND DAY(FECHA_NACIMIENTO)=@DIA
                        AND A.activo=1 AND A.Id_direccion=@Id_direccion";
                    cmd.Parameters.AddWithValue("@MES", mes);
                    cmd.Parameters.AddWithValue("@DIA", dia);
                    cmd.Parameters.AddWithValue("@Id_direccion", idDireccion);
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
        public static Empleados getByPk(
        int legajo)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT *FROM Empleados WHERE");
                sql.AppendLine("legajo = @legajo");
                Empleados obj = null;
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@legajo", legajo);
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<Empleados> lst = mapeo(dr);
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
    }
}

