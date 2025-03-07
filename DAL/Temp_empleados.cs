using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public class Temp_empleados : DALBase
    {
        public string NOMBRE { get; set; }
        public DateTime FECHA_NACIMIENTO { get; set; }
        public string SEXO { get; set; }
        public string ESTADO_CIVIL { get; set; }
        public string CUIT { get; set; }
        public string CALLE { get; set; }
        public int NRO { get; set; }
        public string BARRIO { get; set; }
        public string CIUDAD { get; set; }
        public string PROVINCIA { get; set; }
        public string CP { get; set; }
        public string TELEFONOS { get; set; }
        public string CELULAR { get; set; }
        public string EMAIL { get; set; }
        public string NRO_AFILIADO_OS { get; set; }
        public DateTime FECHA_INGRESO { get; set; }
        public int LEGAJO { get; set; }
        public string TAREA { get; set; }
        public string CARGO { get; set; }
        public string SECCION { get; set; }
        public int CATEGORIA { get; set; }
        public string CLASIFICACION_PERSONAL { get; set; }
        public string TIPO_LIQUIDACION { get; set; }
        public string OFICINA { get; set; }
        public string PROGRAMA { get; set; }
        public string DIRECCION { get; set; }
        public string SECRETARIA { get; set; }
        public string REGIMEN { get; set; }
        public string ESCALA_AUMENTO { get; set; }
        public string REVISTA { get; set; }
        public DateTime FECHA_REVISTA { get; set; }
        public string ACTIVO { get; set; }
        public DateTime FECHA_BAJA { get; set; }
        public string IMPRIME_RECIBO { get; set; }
        public string NRO_SUELDO_BASICO { get; set; }
        public string NRO_CUENTA_GASTOS { get; set; }
        public string NRO_NOMRAMIENTO { get; set; }
        public DateTime FECHA_NOMBRAMIENTO { get; set; }
        public string BANCO { get; set; }
        public string SUCURSAL { get; set; }
        public string CAJA_AHORRO { get; set; }
        public string CBU { get; set; }
        public int NRO_CONTRATO { get; set; }
        public DateTime FECHA_INICIO_CONTRATO { get; set; }
        public DateTime FECHA_FIN_CONTRATO { get; set; }
        public string NRO_JUBILACION { get; set; }
        public int ANTIGUEDD_ACTUAL { get; set; }
        public int ANTIGUEDAD_ANTERIOR { get; set; }

        public Temp_empleados()
        {
            NOMBRE = " - ";
            FECHA_NACIMIENTO = DateTime.Now;
            SEXO = " - ";
            ESTADO_CIVIL = " - ";
            CUIT = " - ";
            CALLE = " - ";
            NRO = 0;
            BARRIO = " - ";
            CIUDAD = " - ";
            PROVINCIA = " - ";
            CP = " - ";
            TELEFONOS = " - ";
            CELULAR = " - ";
            EMAIL = " - ";
            NRO_AFILIADO_OS = " - ";
            FECHA_INGRESO = DateTime.Now;
            LEGAJO = 0;
            TAREA = " - ";
            CARGO = " - ";
            SECCION = " - ";
            CATEGORIA = 0;
            CLASIFICACION_PERSONAL = " - ";
            TIPO_LIQUIDACION = " - ";
            OFICINA = " - ";
            PROGRAMA = " - ";
            DIRECCION = " - ";
            SECRETARIA = " - ";
            REGIMEN = " - ";
            ESCALA_AUMENTO = " - ";
            REVISTA = " - ";
            FECHA_REVISTA = DateTime.Now;
            ACTIVO = " - ";
            FECHA_BAJA = DateTime.Now;
            IMPRIME_RECIBO = " - ";
            NRO_SUELDO_BASICO = " 0 ";
            NRO_CUENTA_GASTOS = " 0 ";
            NRO_NOMRAMIENTO = " - ";
            FECHA_NOMBRAMIENTO = DateTime.Now;
            BANCO = " - ";
            SUCURSAL = " - ";
            CAJA_AHORRO = " - ";
            CBU = " - ";
            NRO_CONTRATO = 0;
            FECHA_INICIO_CONTRATO = DateTime.Now;
            FECHA_FIN_CONTRATO = DateTime.Now;
            NRO_JUBILACION = " - ";
            ANTIGUEDD_ACTUAL = 0;
            ANTIGUEDAD_ANTERIOR = 0;
        }

        private static List<Temp_empleados> mapeo(SqlDataReader dr)
        {
            List<Temp_empleados> lst = new List<Temp_empleados>();
            Temp_empleados obj;
            if (dr.HasRows)
            {
                int NOMBRE = dr.GetOrdinal("NOMBRE");
                int FECHA_NACIMIENTO = dr.GetOrdinal("FECHA_NACIMIENTO");
                int SEXO = dr.GetOrdinal("SEXO");
                int ESTADO_CIVIL = dr.GetOrdinal("ESTADO_CIVIL");
                int CUIT = dr.GetOrdinal("CUIT");
                int CALLE = dr.GetOrdinal("CALLE");
                int NRO = dr.GetOrdinal("NRO");
                int BARRIO = dr.GetOrdinal("BARRIO");
                int CIUDAD = dr.GetOrdinal("CIUDAD");
                int PROVINCIA = dr.GetOrdinal("PROVINCIA");
                int CP = dr.GetOrdinal("CP");
                int TELEFONOS = dr.GetOrdinal("TELEFONOS");
                int CELULAR = dr.GetOrdinal("CELULAR");
                int EMAIL = dr.GetOrdinal("EMAIL");
                int NRO_AFILIADO_OS = dr.GetOrdinal("NRO_AFILIADO_OS");
                int FECHA_INGRESO = dr.GetOrdinal("FECHA_INGRESO");
                int LEGAJO = dr.GetOrdinal("LEGAJO");
                int TAREA = dr.GetOrdinal("TAREA");
                int CARGO = dr.GetOrdinal("CARGO");
                int SECCION = dr.GetOrdinal("SECCION");
                int CATEGORIA = dr.GetOrdinal("CATEGORIA");
                int CLASIFICACION_PERSONAL = dr.GetOrdinal("CLASIFICACION_PERSONAL");
                int TIPO_LIQUIDACION = dr.GetOrdinal("TIPO_LIQUIDACION");
                int OFICINA = dr.GetOrdinal("OFICINA");
                int PROGRAMA = dr.GetOrdinal("PROGRAMA");
                int DIRECCION = dr.GetOrdinal("DIRECCION");
                int SECRETARIA = dr.GetOrdinal("SECRETARIA");
                int REGIMEN = dr.GetOrdinal("REGIMEN");
                int ESCALA_AUMENTO = dr.GetOrdinal("ESCALA_AUMENTO");
                int REVISTA = dr.GetOrdinal("REVISTA");
                int FECHA_REVISTA = dr.GetOrdinal("FECHA_REVISTA");
                int ACTIVO = dr.GetOrdinal("ACTIVO");
                int FECHA_BAJA = dr.GetOrdinal("FECHA_BAJA");
                int IMPRIME_RECIBO = dr.GetOrdinal("IMPRIME_RECIBO");
                int NRO_SUELDO_BASICO = dr.GetOrdinal("NRO_SUELDO_BASICO");
                int NRO_CUENTA_GASTOS = dr.GetOrdinal("NRO_CUENTA_GASTOS");
                int NRO_NOMRAMIENTO = dr.GetOrdinal("NRO_NOMRAMIENTO");
                int FECHA_NOMBRAMIENTO = dr.GetOrdinal("FECHA_NOMBRAMIENTO");
                int BANCO = dr.GetOrdinal("BANCO");
                int SUCURSAL = dr.GetOrdinal("SUCURSAL");
                int CAJA_AHORRO = dr.GetOrdinal("CAJA_AHORRO");
                int CBU = dr.GetOrdinal("CBU");
                int NRO_CONTRATO = dr.GetOrdinal("NRO_CONTRATO");
                int FECHA_INICIO_CONTRATO = dr.GetOrdinal("FECHA_INICIO_CONTRATO");
                int FECHA_FIN_CONTRATO = dr.GetOrdinal("FECHA_FIN_CONTRATO");
                int NRO_JUBILACION = dr.GetOrdinal("NRO_JUBILACION");
                int ANTIGUEDD_ACTUAL = dr.GetOrdinal("ANTIGUEDD_ACTUAL");
                int ANTIGUEDAD_ANTERIOR = dr.GetOrdinal("ANTIGUEDAD_ANTERIOR");
                while (dr.Read())
                {
                    obj = new Temp_empleados();
                    if (!dr.IsDBNull(NOMBRE)) { obj.NOMBRE = dr.GetString(NOMBRE); }
                    if (!dr.IsDBNull(FECHA_NACIMIENTO)) { obj.FECHA_NACIMIENTO = dr.GetDateTime(FECHA_NACIMIENTO); }
                    if (!dr.IsDBNull(SEXO)) { obj.SEXO = dr.GetString(SEXO); }
                    if (!dr.IsDBNull(ESTADO_CIVIL)) { obj.ESTADO_CIVIL = dr.GetString(ESTADO_CIVIL); }
                    if (!dr.IsDBNull(CUIT)) { obj.CUIT = dr.GetString(CUIT); }
                    if (!dr.IsDBNull(CALLE)) { obj.CALLE = dr.GetString(CALLE); }
                    if (!dr.IsDBNull(NRO)) { obj.NRO = dr.GetInt32(NRO); }
                    if (!dr.IsDBNull(BARRIO)) { obj.BARRIO = dr.GetString(BARRIO); }
                    if (!dr.IsDBNull(CIUDAD)) { obj.CIUDAD = dr.GetString(CIUDAD); }
                    if (!dr.IsDBNull(PROVINCIA)) { obj.PROVINCIA = dr.GetString(PROVINCIA); }
                    if (!dr.IsDBNull(CP)) { obj.CP = dr.GetString(CP); }
                    if (!dr.IsDBNull(TELEFONOS)) { obj.TELEFONOS = dr.GetString(TELEFONOS); }
                    if (!dr.IsDBNull(CELULAR)) { obj.CELULAR = dr.GetString(CELULAR); }
                    if (!dr.IsDBNull(EMAIL)) { obj.EMAIL = dr.GetString(EMAIL); }
                    if (!dr.IsDBNull(NRO_AFILIADO_OS)) { obj.NRO_AFILIADO_OS = dr.GetString(NRO_AFILIADO_OS); }
                    if (!dr.IsDBNull(FECHA_INGRESO)) { obj.FECHA_INGRESO = dr.GetDateTime(FECHA_INGRESO); }
                    if (!dr.IsDBNull(LEGAJO)) { obj.LEGAJO = dr.GetInt32(LEGAJO); }
                    if (!dr.IsDBNull(TAREA)) { obj.TAREA = dr.GetString(TAREA); }
                    if (!dr.IsDBNull(CARGO)) { obj.CARGO = dr.GetString(CARGO); }
                    if (!dr.IsDBNull(SECCION)) { obj.SECCION = dr.GetString(SECCION); }
                    if (!dr.IsDBNull(CATEGORIA)) { obj.CATEGORIA = dr.GetInt32(CATEGORIA); }
                    if (!dr.IsDBNull(CLASIFICACION_PERSONAL)) { obj.CLASIFICACION_PERSONAL = dr.GetString(CLASIFICACION_PERSONAL); }
                    if (!dr.IsDBNull(TIPO_LIQUIDACION)) { obj.TIPO_LIQUIDACION = dr.GetString(TIPO_LIQUIDACION); }
                    if (!dr.IsDBNull(OFICINA)) { obj.OFICINA = dr.GetString(OFICINA); }
                    if (!dr.IsDBNull(PROGRAMA)) { obj.PROGRAMA = dr.GetString(PROGRAMA); }
                    if (!dr.IsDBNull(DIRECCION)) { obj.DIRECCION = dr.GetString(DIRECCION); }
                    if (!dr.IsDBNull(SECRETARIA)) { obj.SECRETARIA = dr.GetString(SECRETARIA); }
                    if (!dr.IsDBNull(REGIMEN)) { obj.REGIMEN = dr.GetString(REGIMEN); }
                    if (!dr.IsDBNull(ESCALA_AUMENTO)) { obj.ESCALA_AUMENTO = dr.GetString(ESCALA_AUMENTO); }
                    if (!dr.IsDBNull(REVISTA)) { obj.REVISTA = dr.GetString(REVISTA); }
                    if (!dr.IsDBNull(FECHA_REVISTA)) { obj.FECHA_REVISTA = dr.GetDateTime(FECHA_REVISTA); }
                    if (!dr.IsDBNull(ACTIVO)) { obj.ACTIVO = dr.GetString(ACTIVO); }
                    if (!dr.IsDBNull(FECHA_BAJA)) { obj.FECHA_BAJA = dr.GetDateTime(FECHA_BAJA); }
                    if (!dr.IsDBNull(IMPRIME_RECIBO)) { obj.IMPRIME_RECIBO = dr.GetString(IMPRIME_RECIBO); }
                    if (!dr.IsDBNull(NRO_SUELDO_BASICO)) { obj.NRO_SUELDO_BASICO = dr.GetString(NRO_SUELDO_BASICO); }
                    if (!dr.IsDBNull(NRO_CUENTA_GASTOS)) { obj.NRO_CUENTA_GASTOS = dr.GetString(NRO_CUENTA_GASTOS); }
                    if (!dr.IsDBNull(NRO_NOMRAMIENTO)) { obj.NRO_NOMRAMIENTO = dr.GetString(NRO_NOMRAMIENTO); }
                    if (!dr.IsDBNull(FECHA_NOMBRAMIENTO)) { obj.FECHA_NOMBRAMIENTO = dr.GetDateTime(FECHA_NOMBRAMIENTO); }
                    if (!dr.IsDBNull(BANCO)) { obj.BANCO = dr.GetString(BANCO); }
                    if (!dr.IsDBNull(SUCURSAL)) { obj.SUCURSAL = dr.GetString(SUCURSAL); }
                    if (!dr.IsDBNull(CAJA_AHORRO)) { obj.CAJA_AHORRO = dr.GetString(CAJA_AHORRO); }
                    if (!dr.IsDBNull(CBU)) { obj.CBU = dr.GetString(CBU); }
                    if (!dr.IsDBNull(NRO_CONTRATO)) { obj.NRO_CONTRATO = dr.GetInt32(NRO_CONTRATO); }
                    if (!dr.IsDBNull(FECHA_INICIO_CONTRATO)) { obj.FECHA_INICIO_CONTRATO = dr.GetDateTime(FECHA_INICIO_CONTRATO); }
                    if (!dr.IsDBNull(FECHA_FIN_CONTRATO)) { obj.FECHA_FIN_CONTRATO = dr.GetDateTime(FECHA_FIN_CONTRATO); }
                    if (!dr.IsDBNull(NRO_JUBILACION)) { obj.NRO_JUBILACION = dr.GetString(NRO_JUBILACION); }
                    if (!dr.IsDBNull(ANTIGUEDD_ACTUAL)) { obj.ANTIGUEDD_ACTUAL = dr.GetInt32(ANTIGUEDD_ACTUAL); }
                    if (!dr.IsDBNull(ANTIGUEDAD_ANTERIOR)) { obj.ANTIGUEDAD_ANTERIOR = dr.GetInt32(ANTIGUEDAD_ANTERIOR); }
                    lst.Add(obj);
                }
            }
            return lst;
        }

        public static List<Temp_empleados> read()
        {
            try
            {
                List<Temp_empleados> lst = new List<Temp_empleados>();
                using (SqlConnection con = GetConnection("siimva"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT *FROM Temp_empleados";
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

        public static Temp_empleados getByPk(int lejago)
        {
            try
            {                
                Temp_empleados obj = null;
                using (SqlConnection con = GetConnection("siimva"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        @"SELECT
	                        A.nombre AS 'NOMBRE',
	                        A.fecha_nacimiento AS 'FECHA_NACIMIENTO',
	                        CASE A.SEXO 
		                        WHEN 'M' THEN 'Masculino'
		                        WHEN 'F' THEN 'Femenino'
	                        END AS 'SEXO',
	                        B.des_estado_civil 'ESTADO_CIVIL',
	                        A.cuil AS 'CUIT',
	                        A.calle_domicilio AS 'CALLE',
	                        A.nro_domicilio AS 'NRO', 
	                        A.barrio_domicilio AS 'BARRIO',
	                        A.ciudad_domicilio AS 'CIUDAD',
	                        A.provincia_domicilio AS 'PROVINCIA',
	                        A.cod_postal AS 'CP',
	                        A.telefonos AS 'TELEFONOS',
	                        A.celular AS 'CELULAR',
	                        A.email AS 'EMAIL',
	                        A.nro_ipam AS 'NRO_AFILIADO_OS',
	                        A.fecha_ingreso AS 'FECHA_INGRESO',
	                        A.legajo AS 'LEGAJO',
	                        A.tarea AS 'TAREA',
	                        C.desc_cargo AS 'CARGO',
	                        D.des_seccion AS 'SECCION',
	                        A.cod_categoria AS 'CATEGORIA',
	                        E.des_clasif_per AS 'CLASIFICACION_PERSONAL',
	                        F.des_tipo_liq AS 'TIPO_LIQUIDACION',
	                        H.nombre_oficina AS 'OFICINA',
	                        G.Programa AS 'PROGRAMA',
	                        I.Descripcion AS 'DIRECCION',
	                        J.Descripcion AS 'SECRETARIA',
	                        k.descripcion AS 'REGIMEN',
	                        L.Descripcion AS 'ESCALA_AUMENTO',
	                        M.descripcion AS 'REVISTA',
	                        A.fecha_revista AS 'FECHA_REVISTA',
	                        CASE A.activo
		                        WHEN 1 THEN 'Si'
		                        ELSE 'No'
	                        END AS 'ACTIVO',
	                        A.fecha_baja AS 'FECHA_BAJA',
	                        CASE A.imprime_recibo
		                        WHEN 1 THEN 'SI'
		                        ELSE 'NO'
	                        END AS 'IMPRIME_RECIBO',
	                        A.nro_cta_sb AS 'NRO_SUELDO_BASICO',
	                        A.nro_cta_gastos AS 'NRO_CUENTA_GASTOS',
	                        a.nro_nombramiento AS 'NRO_NOMRAMIENTO',
	                        a.fecha_nombramiento AS 'FECHA_NOMBRAMIENTO',
	                        N.nom_banco AS 'BANCO',
	                        A.nro_sucursal AS 'SUCURSAL',
	                        A.nro_caja_ahorro 'CAJA_AHORRO',
	                        A.nro_cbu AS 'CBU',	
	                        A.nro_contrato AS 'NRO_CONTRATO',
	                        A.fecha_inicio_contrato AS 'FECHA_INICIO_CONTRATO',
	                        A.fecha_fin_contrato AS 'FECHA_FIN_CONTRATO',
	                        A.nro_jubilacion AS 'NRO_JUBILACION',
	                        A.antiguedad_actual AS 'ANTIGUEDD_ACTUAL',
	                        A.antiguedad_ant AS 'ANTIGUEDAD_ANTERIOR'
                        FROM EMPLEADOS A 
                            LEFT JOIN ESTADOS_CIVILES B ON A.cod_estado_civil=B.cod_estado_civil
                            LEFT JOIN CARGOS C ON  A.cod_cargo=C.cod_cargo
                            LEFT JOIN SECCIONES D ON A.cod_seccion=D.cod_seccion
                            LEFT JOIN CLASIFICACIONES_PERSONAL E ON E.cod_clasif_per = A.cod_clasif_per
                            LEFT JOIN TIPOS_LIQUIDACION F ON A.cod_tipo_liq=F.cod_tipo_liq
                            LEFT JOIN PROGRAMAS_PUBLICOS G ON A.id_programa=G.Id_programa
                            LEFT JOIN OFICINAS H ON H.codigo_oficina=A.id_oficina
                            LEFT JOIN DIRECCION I ON A.id_direccion=I.Id_direccion
                            LEFT JOIN SECRETARIA J ON J.Id_secretaria = A.ID_SECRETARIA 
                            AND J.ejercicio=YEAR(GETDATE())
                            LEFT JOIN EMPLEADOS_REGIMEN K ON A.cod_regimen_empleado=K.cod_regimen_empleado
                            LEFT JOIN ESCALA_AUMENTOS L ON L.Cod_escala_aumento=A.cod_escala_aumento  
                            LEFT JOIN SITUACION_REVISTA_LEGAJO M ON M.id_revista=A.id_revista
                            LEFT JOIN BANCOS N ON N.cod_banco=A.cod_banco
                        WHERE A.legajo=@legajo";
                    cmd.Parameters.AddWithValue("@legajo", lejago);
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<Temp_empleados> lst = mapeo(dr);
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

        public static int insert(Temp_empleados obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO Temp_empleados(");
                sql.AppendLine("NOMBRE");
                sql.AppendLine(", FECHA_NACIMIENTO");
                sql.AppendLine(", SEXO");
                sql.AppendLine(", ESTADO_CIVIL");
                sql.AppendLine(", CUIT");
                sql.AppendLine(", CALLE");
                sql.AppendLine(", NRO");
                sql.AppendLine(", BARRIO");
                sql.AppendLine(", CIUDAD");
                sql.AppendLine(", PROVINCIA");
                sql.AppendLine(", CP");
                sql.AppendLine(", TELEFONOS");
                sql.AppendLine(", CELULAR");
                sql.AppendLine(", EMAIL");
                sql.AppendLine(", NRO_AFILIADO_OS");
                sql.AppendLine(", FECHA_INGRESO");
                sql.AppendLine(", LEGAJO");
                sql.AppendLine(", TAREA");
                sql.AppendLine(", CARGO");
                sql.AppendLine(", SECCION");
                sql.AppendLine(", CATEGORIA");
                sql.AppendLine(", CLASIFICACION_PERSONAL");
                sql.AppendLine(", TIPO_LIQUIDACION");
                sql.AppendLine(", OFICINA");
                sql.AppendLine(", PROGRAMA");
                sql.AppendLine(", DIRECCION");
                sql.AppendLine(", SECRETARIA");
                sql.AppendLine(", REGIMEN");
                sql.AppendLine(", ESCALA_AUMENTO");
                sql.AppendLine(", REVISTA");
                sql.AppendLine(", FECHA_REVISTA");
                sql.AppendLine(", ACTIVO");
                sql.AppendLine(", FECHA_BAJA");
                sql.AppendLine(", IMPRIME_RECIBO");
                sql.AppendLine(", NRO_SUELDO_BASICO");
                sql.AppendLine(", NRO_CUENTA_GASTOS");
                sql.AppendLine(", NRO_NOMRAMIENTO");
                sql.AppendLine(", FECHA_NOMBRAMIENTO");
                sql.AppendLine(", BANCO");
                sql.AppendLine(", SUCURSAL");
                sql.AppendLine(", CAJA_AHORRO");
                sql.AppendLine(", CBU");
                sql.AppendLine(", NRO_CONTRATO");
                sql.AppendLine(", FECHA_INICIO_CONTRATO");
                sql.AppendLine(", FECHA_FIN_CONTRATO");
                sql.AppendLine(", NRO_JUBILACION");
                sql.AppendLine(", ANTIGUEDD_ACTUAL");
                sql.AppendLine(", ANTIGUEDAD_ANTERIOR");
                sql.AppendLine(")");
                sql.AppendLine("VALUES");
                sql.AppendLine("(");
                sql.AppendLine("@NOMBRE");
                sql.AppendLine(", @FECHA_NACIMIENTO");
                sql.AppendLine(", @SEXO");
                sql.AppendLine(", @ESTADO_CIVIL");
                sql.AppendLine(", @CUIT");
                sql.AppendLine(", @CALLE");
                sql.AppendLine(", @NRO");
                sql.AppendLine(", @BARRIO");
                sql.AppendLine(", @CIUDAD");
                sql.AppendLine(", @PROVINCIA");
                sql.AppendLine(", @CP");
                sql.AppendLine(", @TELEFONOS");
                sql.AppendLine(", @CELULAR");
                sql.AppendLine(", @EMAIL");
                sql.AppendLine(", @NRO_AFILIADO_OS");
                sql.AppendLine(", @FECHA_INGRESO");
                sql.AppendLine(", @LEGAJO");
                sql.AppendLine(", @TAREA");
                sql.AppendLine(", @CARGO");
                sql.AppendLine(", @SECCION");
                sql.AppendLine(", @CATEGORIA");
                sql.AppendLine(", @CLASIFICACION_PERSONAL");
                sql.AppendLine(", @TIPO_LIQUIDACION");
                sql.AppendLine(", @OFICINA");
                sql.AppendLine(", @PROGRAMA");
                sql.AppendLine(", @DIRECCION");
                sql.AppendLine(", @SECRETARIA");
                sql.AppendLine(", @REGIMEN");
                sql.AppendLine(", @ESCALA_AUMENTO");
                sql.AppendLine(", @REVISTA");
                sql.AppendLine(", @FECHA_REVISTA");
                sql.AppendLine(", @ACTIVO");
                sql.AppendLine(", @FECHA_BAJA");
                sql.AppendLine(", @IMPRIME_RECIBO");
                sql.AppendLine(", @NRO_SUELDO_BASICO");
                sql.AppendLine(", @NRO_CUENTA_GASTOS");
                sql.AppendLine(", @NRO_NOMRAMIENTO");
                sql.AppendLine(", @FECHA_NOMBRAMIENTO");
                sql.AppendLine(", @BANCO");
                sql.AppendLine(", @SUCURSAL");
                sql.AppendLine(", @CAJA_AHORRO");
                sql.AppendLine(", @CBU");
                sql.AppendLine(", @NRO_CONTRATO");
                sql.AppendLine(", @FECHA_INICIO_CONTRATO");
                sql.AppendLine(", @FECHA_FIN_CONTRATO");
                sql.AppendLine(", @NRO_JUBILACION");
                sql.AppendLine(", @ANTIGUEDD_ACTUAL");
                sql.AppendLine(", @ANTIGUEDAD_ANTERIOR");
                sql.AppendLine(")");
                using (SqlConnection con = GetConnection("siimva"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@NOMBRE", obj.NOMBRE);
                    cmd.Parameters.AddWithValue("@FECHA_NACIMIENTO", obj.FECHA_NACIMIENTO);
                    cmd.Parameters.AddWithValue("@SEXO", obj.SEXO);
                    cmd.Parameters.AddWithValue("@ESTADO_CIVIL", obj.ESTADO_CIVIL);
                    cmd.Parameters.AddWithValue("@CUIT", obj.CUIT);
                    cmd.Parameters.AddWithValue("@CALLE", obj.CALLE);
                    cmd.Parameters.AddWithValue("@NRO", obj.NRO);
                    cmd.Parameters.AddWithValue("@BARRIO", obj.BARRIO);
                    cmd.Parameters.AddWithValue("@CIUDAD", obj.CIUDAD);
                    cmd.Parameters.AddWithValue("@PROVINCIA", obj.PROVINCIA);
                    cmd.Parameters.AddWithValue("@CP", obj.CP);
                    cmd.Parameters.AddWithValue("@TELEFONOS", obj.TELEFONOS);
                    cmd.Parameters.AddWithValue("@CELULAR", obj.CELULAR);
                    cmd.Parameters.AddWithValue("@EMAIL", obj.EMAIL);
                    cmd.Parameters.AddWithValue("@NRO_AFILIADO_OS", obj.NRO_AFILIADO_OS);
                    cmd.Parameters.AddWithValue("@FECHA_INGRESO", obj.FECHA_INGRESO);
                    cmd.Parameters.AddWithValue("@LEGAJO", obj.LEGAJO);
                    cmd.Parameters.AddWithValue("@TAREA", obj.TAREA);
                    cmd.Parameters.AddWithValue("@CARGO", obj.CARGO);
                    cmd.Parameters.AddWithValue("@SECCION", obj.SECCION);
                    cmd.Parameters.AddWithValue("@CATEGORIA", obj.CATEGORIA);
                    cmd.Parameters.AddWithValue("@CLASIFICACION_PERSONAL", obj.CLASIFICACION_PERSONAL);
                    cmd.Parameters.AddWithValue("@TIPO_LIQUIDACION", obj.TIPO_LIQUIDACION);
                    cmd.Parameters.AddWithValue("@OFICINA", obj.OFICINA);
                    cmd.Parameters.AddWithValue("@PROGRAMA", obj.PROGRAMA);
                    cmd.Parameters.AddWithValue("@DIRECCION", obj.DIRECCION);
                    cmd.Parameters.AddWithValue("@SECRETARIA", obj.SECRETARIA);
                    cmd.Parameters.AddWithValue("@REGIMEN", obj.REGIMEN);
                    cmd.Parameters.AddWithValue("@ESCALA_AUMENTO", obj.ESCALA_AUMENTO);
                    cmd.Parameters.AddWithValue("@REVISTA", obj.REVISTA);
                    cmd.Parameters.AddWithValue("@FECHA_REVISTA", obj.FECHA_REVISTA);
                    cmd.Parameters.AddWithValue("@ACTIVO", obj.ACTIVO);
                    cmd.Parameters.AddWithValue("@FECHA_BAJA", obj.FECHA_BAJA);
                    cmd.Parameters.AddWithValue("@IMPRIME_RECIBO", obj.IMPRIME_RECIBO);
                    cmd.Parameters.AddWithValue("@NRO_SUELDO_BASICO", obj.NRO_SUELDO_BASICO);
                    cmd.Parameters.AddWithValue("@NRO_CUENTA_GASTOS", obj.NRO_CUENTA_GASTOS);
                    cmd.Parameters.AddWithValue("@NRO_NOMRAMIENTO", obj.NRO_NOMRAMIENTO);
                    cmd.Parameters.AddWithValue("@FECHA_NOMBRAMIENTO", obj.FECHA_NOMBRAMIENTO);
                    cmd.Parameters.AddWithValue("@BANCO", obj.BANCO);
                    cmd.Parameters.AddWithValue("@SUCURSAL", obj.SUCURSAL);
                    cmd.Parameters.AddWithValue("@CAJA_AHORRO", obj.CAJA_AHORRO);
                    cmd.Parameters.AddWithValue("@CBU", obj.CBU);
                    cmd.Parameters.AddWithValue("@NRO_CONTRATO", obj.NRO_CONTRATO);
                    cmd.Parameters.AddWithValue("@FECHA_INICIO_CONTRATO", obj.FECHA_INICIO_CONTRATO);
                    cmd.Parameters.AddWithValue("@FECHA_FIN_CONTRATO", obj.FECHA_FIN_CONTRATO);
                    cmd.Parameters.AddWithValue("@NRO_JUBILACION", obj.NRO_JUBILACION);
                    cmd.Parameters.AddWithValue("@ANTIGUEDD_ACTUAL", obj.ANTIGUEDD_ACTUAL);
                    cmd.Parameters.AddWithValue("@ANTIGUEDAD_ANTERIOR", obj.ANTIGUEDAD_ANTERIOR);
                    cmd.Connection.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void update(Temp_empleados obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE  Temp_empleados SET");
                sql.AppendLine("NOMBRE=@NOMBRE");
                sql.AppendLine(", FECHA_NACIMIENTO=@FECHA_NACIMIENTO");
                sql.AppendLine(", SEXO=@SEXO");
                sql.AppendLine(", ESTADO_CIVIL=@ESTADO_CIVIL");
                sql.AppendLine(", CUIT=@CUIT");
                sql.AppendLine(", CALLE=@CALLE");
                sql.AppendLine(", NRO=@NRO");
                sql.AppendLine(", BARRIO=@BARRIO");
                sql.AppendLine(", CIUDAD=@CIUDAD");
                sql.AppendLine(", PROVINCIA=@PROVINCIA");
                sql.AppendLine(", CP=@CP");
                sql.AppendLine(", TELEFONOS=@TELEFONOS");
                sql.AppendLine(", CELULAR=@CELULAR");
                sql.AppendLine(", EMAIL=@EMAIL");
                sql.AppendLine(", NRO_AFILIADO_OS=@NRO_AFILIADO_OS");
                sql.AppendLine(", FECHA_INGRESO=@FECHA_INGRESO");
                sql.AppendLine(", LEGAJO=@LEGAJO");
                sql.AppendLine(", TAREA=@TAREA");
                sql.AppendLine(", CARGO=@CARGO");
                sql.AppendLine(", SECCION=@SECCION");
                sql.AppendLine(", CATEGORIA=@CATEGORIA");
                sql.AppendLine(", CLASIFICACION_PERSONAL=@CLASIFICACION_PERSONAL");
                sql.AppendLine(", TIPO_LIQUIDACION=@TIPO_LIQUIDACION");
                sql.AppendLine(", OFICINA=@OFICINA");
                sql.AppendLine(", PROGRAMA=@PROGRAMA");
                sql.AppendLine(", DIRECCION=@DIRECCION");
                sql.AppendLine(", SECRETARIA=@SECRETARIA");
                sql.AppendLine(", REGIMEN=@REGIMEN");
                sql.AppendLine(", ESCALA_AUMENTO=@ESCALA_AUMENTO");
                sql.AppendLine(", REVISTA=@REVISTA");
                sql.AppendLine(", FECHA_REVISTA=@FECHA_REVISTA");
                sql.AppendLine(", ACTIVO=@ACTIVO");
                sql.AppendLine(", FECHA_BAJA=@FECHA_BAJA");
                sql.AppendLine(", IMPRIME_RECIBO=@IMPRIME_RECIBO");
                sql.AppendLine(", NRO_SUELDO_BASICO=@NRO_SUELDO_BASICO");
                sql.AppendLine(", NRO_CUENTA_GASTOS=@NRO_CUENTA_GASTOS");
                sql.AppendLine(", NRO_NOMRAMIENTO=@NRO_NOMRAMIENTO");
                sql.AppendLine(", FECHA_NOMBRAMIENTO=@FECHA_NOMBRAMIENTO");
                sql.AppendLine(", BANCO=@BANCO");
                sql.AppendLine(", SUCURSAL=@SUCURSAL");
                sql.AppendLine(", CAJA_AHORRO=@CAJA_AHORRO");
                sql.AppendLine(", CBU=@CBU");
                sql.AppendLine(", NRO_CONTRATO=@NRO_CONTRATO");
                sql.AppendLine(", FECHA_INICIO_CONTRATO=@FECHA_INICIO_CONTRATO");
                sql.AppendLine(", FECHA_FIN_CONTRATO=@FECHA_FIN_CONTRATO");
                sql.AppendLine(", NRO_JUBILACION=@NRO_JUBILACION");
                sql.AppendLine(", ANTIGUEDD_ACTUAL=@ANTIGUEDD_ACTUAL");
                sql.AppendLine(", ANTIGUEDAD_ANTERIOR=@ANTIGUEDAD_ANTERIOR");
                sql.AppendLine("WHERE");
                using (SqlConnection con = GetConnection("siimva"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@NOMBRE", obj.NOMBRE);
                    cmd.Parameters.AddWithValue("@FECHA_NACIMIENTO", obj.FECHA_NACIMIENTO);
                    cmd.Parameters.AddWithValue("@SEXO", obj.SEXO);
                    cmd.Parameters.AddWithValue("@ESTADO_CIVIL", obj.ESTADO_CIVIL);
                    cmd.Parameters.AddWithValue("@CUIT", obj.CUIT);
                    cmd.Parameters.AddWithValue("@CALLE", obj.CALLE);
                    cmd.Parameters.AddWithValue("@NRO", obj.NRO);
                    cmd.Parameters.AddWithValue("@BARRIO", obj.BARRIO);
                    cmd.Parameters.AddWithValue("@CIUDAD", obj.CIUDAD);
                    cmd.Parameters.AddWithValue("@PROVINCIA", obj.PROVINCIA);
                    cmd.Parameters.AddWithValue("@CP", obj.CP);
                    cmd.Parameters.AddWithValue("@TELEFONOS", obj.TELEFONOS);
                    cmd.Parameters.AddWithValue("@CELULAR", obj.CELULAR);
                    cmd.Parameters.AddWithValue("@EMAIL", obj.EMAIL);
                    cmd.Parameters.AddWithValue("@NRO_AFILIADO_OS", obj.NRO_AFILIADO_OS);
                    cmd.Parameters.AddWithValue("@FECHA_INGRESO", obj.FECHA_INGRESO);
                    cmd.Parameters.AddWithValue("@LEGAJO", obj.LEGAJO);
                    cmd.Parameters.AddWithValue("@TAREA", obj.TAREA);
                    cmd.Parameters.AddWithValue("@CARGO", obj.CARGO);
                    cmd.Parameters.AddWithValue("@SECCION", obj.SECCION);
                    cmd.Parameters.AddWithValue("@CATEGORIA", obj.CATEGORIA);
                    cmd.Parameters.AddWithValue("@CLASIFICACION_PERSONAL", obj.CLASIFICACION_PERSONAL);
                    cmd.Parameters.AddWithValue("@TIPO_LIQUIDACION", obj.TIPO_LIQUIDACION);
                    cmd.Parameters.AddWithValue("@OFICINA", obj.OFICINA);
                    cmd.Parameters.AddWithValue("@PROGRAMA", obj.PROGRAMA);
                    cmd.Parameters.AddWithValue("@DIRECCION", obj.DIRECCION);
                    cmd.Parameters.AddWithValue("@SECRETARIA", obj.SECRETARIA);
                    cmd.Parameters.AddWithValue("@REGIMEN", obj.REGIMEN);
                    cmd.Parameters.AddWithValue("@ESCALA_AUMENTO", obj.ESCALA_AUMENTO);
                    cmd.Parameters.AddWithValue("@REVISTA", obj.REVISTA);
                    cmd.Parameters.AddWithValue("@FECHA_REVISTA", obj.FECHA_REVISTA);
                    cmd.Parameters.AddWithValue("@ACTIVO", obj.ACTIVO);
                    cmd.Parameters.AddWithValue("@FECHA_BAJA", obj.FECHA_BAJA);
                    cmd.Parameters.AddWithValue("@IMPRIME_RECIBO", obj.IMPRIME_RECIBO);
                    cmd.Parameters.AddWithValue("@NRO_SUELDO_BASICO", obj.NRO_SUELDO_BASICO);
                    cmd.Parameters.AddWithValue("@NRO_CUENTA_GASTOS", obj.NRO_CUENTA_GASTOS);
                    cmd.Parameters.AddWithValue("@NRO_NOMRAMIENTO", obj.NRO_NOMRAMIENTO);
                    cmd.Parameters.AddWithValue("@FECHA_NOMBRAMIENTO", obj.FECHA_NOMBRAMIENTO);
                    cmd.Parameters.AddWithValue("@BANCO", obj.BANCO);
                    cmd.Parameters.AddWithValue("@SUCURSAL", obj.SUCURSAL);
                    cmd.Parameters.AddWithValue("@CAJA_AHORRO", obj.CAJA_AHORRO);
                    cmd.Parameters.AddWithValue("@CBU", obj.CBU);
                    cmd.Parameters.AddWithValue("@NRO_CONTRATO", obj.NRO_CONTRATO);
                    cmd.Parameters.AddWithValue("@FECHA_INICIO_CONTRATO", obj.FECHA_INICIO_CONTRATO);
                    cmd.Parameters.AddWithValue("@FECHA_FIN_CONTRATO", obj.FECHA_FIN_CONTRATO);
                    cmd.Parameters.AddWithValue("@NRO_JUBILACION", obj.NRO_JUBILACION);
                    cmd.Parameters.AddWithValue("@ANTIGUEDD_ACTUAL", obj.ANTIGUEDD_ACTUAL);
                    cmd.Parameters.AddWithValue("@ANTIGUEDAD_ANTERIOR", obj.ANTIGUEDAD_ANTERIOR);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void delete(Temp_empleados obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("DELETE  Temp_empleados ");
                sql.AppendLine("WHERE");
                using (SqlConnection con = GetConnection("siimva"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
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

