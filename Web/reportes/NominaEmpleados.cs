using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace web.reportes
{
    public class NominaEmpleados
    {
        public int legajo { get; set; }
        public string nombre { get; set; }
        public string cuil { get; set; }
        public string des_tipo_documento { get; set; }
        public string nro_documento { get; set; }
        public string fecha_nacimiento { get; set; }
        public string domicilio { get; set; }
        public string fecha_ingreso { get; set; }
        public string fecha_baja { get; set; }
        public string tarea { get; set; }
        public string des_tipo_liq { get; set; }
        public string desc_cargo { get; set; }
        public string des_clasif_per { get; set; }
        public string cod_banco { get; set; }
        public string nro_caja_ahorro { get; set; }
        public string nro_cbu { get; set; }
        public string email { get; set; }
        public int cod_categoria { get; set; }
        public decimal sueldo_basico { get; set; }
        public string secretaria { get; set; }
        public string direccion { get; set; }
        public string programa { get; set; }
        public string oficina { get; set; }
        public string revista { get; set; }
        public string fecha_revista { get; set; }
        public string activo { get; set; }

        public NominaEmpleados()
        {
            legajo = 0;
            nombre = "";
            cuil = "";
            des_tipo_documento = "";
            nro_documento = "";
            fecha_nacimiento = "";
            domicilio = "";
            fecha_ingreso = "";
            fecha_baja = string.Empty;
            tarea = "";
            des_tipo_liq = "";
            desc_cargo = "";
            des_clasif_per = "";
            cod_banco = "";
            nro_caja_ahorro = "";
            nro_cbu = "";
            email = "";
            cod_categoria = 0;
            sueldo_basico = 0;
            secretaria = string.Empty;
            direccion = string.Empty;
            programa = string.Empty;
            oficina = string.Empty;
            revista = string.Empty;
            fecha_revista = string.Empty;
            activo = string.Empty;
        }


        private static List<NominaEmpleados> mapeo(SqlDataReader dr)
        {
            DateTimeFormatInfo culturaFecArgentina = new CultureInfo("es-AR", false).DateTimeFormat;
            List<NominaEmpleados> lst = new List<NominaEmpleados>();
            NominaEmpleados obj;
            var ErrorLegajo = string.Empty;
            StringBuilder errorMessages = new StringBuilder();
            try
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        obj = new NominaEmpleados();
                        ErrorLegajo = Convert.ToString(dr.GetInt32(0));
                        if (ErrorLegajo == "190")
                            Console.Beep();
                        if (!dr.IsDBNull(0)) { obj.legajo = dr.GetInt32(0); }
                        if (!dr.IsDBNull(1)) { obj.nombre = dr.GetString(1).Trim(); }
                        if (!dr.IsDBNull(2)) { obj.cuil = dr.GetString(2); }
                        if (!dr.IsDBNull(3)) { obj.des_tipo_documento = dr.GetString(3); }
                        if (!dr.IsDBNull(4)) { obj.nro_documento = dr.GetString(4); }
                        if (!dr.IsDBNull(5)) { obj.fecha_nacimiento = dr.GetDateTime(5).ToShortDateString(); }
                        if (!dr.IsDBNull(6)) { obj.domicilio = dr.GetString(6); }
                        if (!dr.IsDBNull(7)) { obj.fecha_ingreso = dr.GetDateTime(7).ToShortDateString(); }
                        if (!dr.IsDBNull(8))
                        {
                            obj.fecha_baja = Convert.ToDateTime(dr.GetDateTime(8), culturaFecArgentina).ToString();
                            //dr.GetDateTime(8).ToShortDateString();
                        }
                        if (!dr.IsDBNull(9)) { obj.tarea = dr.GetString(9); }
                        if (!dr.IsDBNull(10)) { obj.des_tipo_liq = dr.GetString(10); }
                        if (!dr.IsDBNull(11)) { obj.desc_cargo = dr.GetString(11); }
                        if (!dr.IsDBNull(12)) { obj.des_clasif_per = dr.GetString(12); }
                        if (!dr.IsDBNull(13)) { obj.cod_banco = dr.GetInt32(13).ToString(); }
                        if (!dr.IsDBNull(14)) { obj.nro_caja_ahorro = dr.GetString(14); }
                        if (!dr.IsDBNull(15)) { obj.nro_cbu = dr.GetString(15); }
                        if (!dr.IsDBNull(16)) { obj.email = dr.GetString(16); }
                        //
                        if (!dr.IsDBNull(17)) { obj.cod_categoria = dr.GetInt32(17); }
                        if (!dr.IsDBNull(18)) { obj.sueldo_basico = dr.GetDecimal(18); }
                        if (!dr.IsDBNull(19)) { obj.secretaria = dr.GetString(19); }
                        if (!dr.IsDBNull(20)) { obj.direccion = dr.GetString(20); }
                        if (!dr.IsDBNull(21)) { obj.programa = dr.GetString(21); }
                        //
                        if (!dr.IsDBNull(22)) { obj.oficina = dr.GetString(22); }
                        if (!dr.IsDBNull(23)) { obj.revista = dr.GetString(23); }
                        if (!dr.IsDBNull(24))
                        {
                            obj.fecha_revista = Convert.ToDateTime(dr.GetDateTime(24), culturaFecArgentina).ToString();
                        }
                        if (!dr.IsDBNull(25))
                        {
                            if (dr.GetBoolean(25) == true)
                                obj.activo = "si";
                            else
                                obj.activo = "no";
                        }
                        lst.Add(obj);
                    }
                }
                return lst;
            }
            catch (SqlException ex)
            {
                //for (int i = 0; i < ex.Errors.Count; i++)
                //{
                //    errorMessages.Append("Index #" + i + "\n" +
                //                        "Message: " + ex.Errors[i].Message + "\n" +
                //                        "Error Number: " + ex.Errors[i].Number + "\n" +
                //                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                //                        "Source: " + ex.Errors[i].Source + "\n" +
                //                        "Procedure: " + ex.Errors[i].Procedure + "\n");
                //}
                //throw new Exception("Error en el proceso de ActualizarConceptoFamiliar!," + errorMessages.ToString(), ex);
                throw new Exception("Error en el Legajo : " + ErrorLegajo.ToString(), ex);
            }
        }

        public static List<NominaEmpleados> readNominaEmpleado()
        {
            try
            {
                string strSQL = @"SELECT a.legajo, a.nombre, a.cuil, b.des_tipo_documento, 
                a.nro_documento, a.fecha_nacimiento,
                domicilio = rtrim(ltrim(a.calle_domicilio)) + ' ' + 
                CONVERT(CHAR(5), ltrim(rtrim(a.nro_domicilio))) + ' ' + rtrim(ltrim(a.ciudad_domicilio)),
                a.fecha_ingreso, a.fecha_baja, a.tarea, t.des_tipo_liq, 
                c.desc_cargo, p.des_clasif_per, a.cod_banco, a.nro_caja_ahorro,
                a.nro_cbu, a.email, ca.cod_categoria, ca.sueldo_basico,
                des_secretaria=s.Descripcion,
                des_direccion=d.Descripcion,
                nom_programa=o.Programa, o1.nombre_oficina,
                rtrim(ltrim(srl.descripcion)) as situacion_revista,
                a.fecha_revista, a.activo
                FROM EMPLEADOS a
                JOIN TIPOS_DOCUMENTOS b on
                a.cod_tipo_documento = b.cod_tipo_documento
                JOIN TIPOS_LIQUIDACION t on
                a.cod_tipo_liq = t.cod_tipo_liq
                LEFT JOIN CARGOS c on
                c.cod_cargo = a.cod_cargo
                JOIN CLASIFICACIONES_PERSONAL p on
                p.cod_clasif_per = a.cod_clasif_per
                LEFT join CATEGORIAS ca on ca.cod_categoria=a.cod_categoria
                LEFT join SECRETARIA s on s.Id_secretaria=a.id_secretaria
                LEFT join DIRECCION d on d.Id_direccion=a.id_direccion
                LEFT join PROGRAMAS_PUBLICOS o on o.Id_programa=a.id_programa
                LEFT join OFICINAS o1 on o1.codigo_oficina=a.id_oficina     
                LEFT join situacion_revista_legajo srl on
                srl.id_revista = a.id_revista
                WHERE
                a.fecha_baja IS NULL
                ORDER BY a.legajo";
                List<NominaEmpleados> lst = new List<NominaEmpleados>();
                using (SqlConnection con = DAL.DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    //cmd.Parameters.AddWithValue("@anio", anio);
                    //cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    //cmd.Parameters.AddWithValue("@nro_liq", nro_liq);
                    //cmd.Parameters.AddWithValue("@porcentaje", porcentaje);
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    lst = mapeo(dr);
                    return lst;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en el proceso del Mapeo de la Nomina Empleados!", ex);
            }
        }

        public static List<NominaEmpleados> readNominaEmpleadoTodos()
        {
            try
            {
                string strSQL = @"SELECT a.legajo, a.nombre, a.cuil, b.des_tipo_documento, a.nro_documento, a.fecha_nacimiento,
                domicilio = rtrim(ltrim(a.calle_domicilio)) + ' ' + CONVERT(CHAR(5), ltrim(rtrim(a.nro_domicilio))) + ' ' + rtrim(ltrim(a.ciudad_domicilio)),
                a.fecha_ingreso, a.fecha_baja, a.tarea, t.des_tipo_liq, c.desc_cargo, p.des_clasif_per, a.cod_banco, a.nro_caja_ahorro,
                a.nro_cbu, a.email, ca.cod_categoria, ca.sueldo_basico,
                des_secretaria=s.Descripcion,
                des_direccion=d.Descripcion,
                nom_programa=o.Programa, o1.nombre_oficina,
                rtrim(ltrim(srl.descripcion)) as situacion_revista,
                a.fecha_revista, a.activo
                FROM EMPLEADOS a
                JOIN TIPOS_DOCUMENTOS b on
                a.cod_tipo_documento = b.cod_tipo_documento
                JOIN TIPOS_LIQUIDACION t on
                a.cod_tipo_liq = t.cod_tipo_liq
                LEFT JOIN CARGOS c on
                c.cod_cargo = a.cod_cargo
                JOIN CLASIFICACIONES_PERSONAL p on
                p.cod_clasif_per = a.cod_clasif_per
                LEFT join CATEGORIAS ca on ca.cod_categoria=a.cod_categoria
                LEFT join SECRETARIA s on s.Id_secretaria=a.id_secretaria
                LEFT join DIRECCION d on d.Id_direccion=a.id_direccion
                LEFT join PROGRAMAS_PUBLICOS o on o.Id_programa=a.id_programa
                LEFT join OFICINAS o1 on o1.codigo_oficina=a.id_oficina     
                LEFT join situacion_revista_legajo srl on
                srl.id_revista = a.id_revista
                --WHERE
                --a.fecha_baja IS NULL
                ORDER BY a.legajo";
                List<NominaEmpleados> lst = new List<NominaEmpleados>();
                using (SqlConnection con = DAL.DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    //cmd.Parameters.AddWithValue("@anio", anio);
                    //cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    //cmd.Parameters.AddWithValue("@nro_liq", nro_liq);
                    //cmd.Parameters.AddWithValue("@porcentaje", porcentaje);
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


    }
}