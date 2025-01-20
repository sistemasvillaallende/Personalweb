using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace web.reportes
{
    public class Planilla_SueldosRep
    {
        public int legajo { get; set; }
        public string cuil { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public string tarea { get; set; }
        public string cargo { get; set; }
        public string sexo { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public string nombre { get; set; }
        public int cod_categoria { get; set; }
        public int cod_seccion { get; set; }
        public decimal sueldo_basico { get; set; }
        public decimal sueldo_neto { get; set; }
        public decimal sueldo_bruto { get; set; }
        public int cod_banco { get; set; }
        public string nro_caja_ahorro { get; set; }
        public int anio { get; set; }
        public int cod_tipo_liq { get; set; }
        public int nro_liquidacion { get; set; }

        //public List<web.reportes.Planilla_Sueldos_DetalleRep> lstDetalle { get; set; }

        public int cod_concepto_liq { get; set; }
        public string desc_concepto_liq { get; set; }
        public bool suma { get; set; }
        public bool sujeto_a_desc { get; set; }
        public decimal unidades { get; set; }
        public decimal importe { get; set; }
        public decimal hab_con_descuento { get; set; }
        public decimal hab_sin_descuento { get; set; }
        public decimal descuentos { get; set; }
        public string tipo_personal { get; set; }
        public string nro_documento { get; set; }
        public string secretaria { get; set; }
        public string direccion { get; set; }
        public string oficina { get; set; }
        public int id_secretaria { get; set; }
        public int id_direccion { get; set; }
        public int id_oficina { get; set; }

        public Planilla_SueldosRep()
        {
            legajo = 0;
            cuil = string.Empty;
            fecha_nacimiento = DateTime.Now;
            tarea = string.Empty;
            tarea = string.Empty;
            sexo = string.Empty;
            fecha_ingreso = DateTime.Now;
            nombre = string.Empty;
            cod_categoria = 0;
            cod_seccion = 0;
            sueldo_basico = 0;
            sueldo_neto = 0;
            sueldo_bruto = 0;
            cod_banco = 0;
            nro_caja_ahorro = string.Empty;
            anio = 0;
            cod_tipo_liq = 0;
            nro_liquidacion = 0;
            cod_concepto_liq = 0;
            desc_concepto_liq = "";
            suma = false;
            sujeto_a_desc = false;
            unidades = 0;
            importe = 0;
            hab_sin_descuento = 0;
            hab_con_descuento = 0;
            descuentos = 0;
            tipo_personal = "";
            nro_documento = "";
            secretaria = string.Empty;
            direccion = string.Empty;
            oficina = string.Empty;
            id_secretaria = 0;
            id_direccion = 0;
            id_oficina = 0;
        }

        private static List<Planilla_SueldosRep> mapeo(SqlDataReader dr)
        {
            List<Planilla_SueldosRep> lst = new List<Planilla_SueldosRep>();
            Planilla_SueldosRep obj;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    obj = new Planilla_SueldosRep();
                    if (!dr.IsDBNull(0)) { obj.legajo = dr.GetInt32(0); }
                    if (!dr.IsDBNull(1)) { obj.cuil = dr.GetString(1); }
                    if (!dr.IsDBNull(2)) { obj.fecha_nacimiento = dr.GetDateTime(2); }
                    if (!dr.IsDBNull(3)) { obj.tarea = dr.GetString(3); }
                    if (!dr.IsDBNull(4)) { obj.cargo = dr.GetString(4); }
                    if (!dr.IsDBNull(5)) { obj.sexo = dr.GetString(5); }
                    if (!dr.IsDBNull(6)) { obj.fecha_ingreso = dr.GetDateTime(6); }
                    if (!dr.IsDBNull(7)) { obj.nombre = dr.GetString(7); }
                    if (!dr.IsDBNull(8)) { obj.cod_categoria = dr.GetInt32(8); }
                    if (!dr.IsDBNull(9)) { obj.cod_seccion = dr.GetInt32(9); }
                    if (!dr.IsDBNull(10)) { obj.sueldo_basico = dr.GetDecimal(10); }
                    if (!dr.IsDBNull(11)) { obj.sueldo_neto = dr.GetDecimal(11); }
                    if (!dr.IsDBNull(12)) { obj.sueldo_bruto = dr.GetDecimal(12); }
                    if (!dr.IsDBNull(13)) { obj.cod_banco = dr.GetInt32(13); }
                    if (!dr.IsDBNull(14)) { obj.nro_caja_ahorro = dr.GetString(14); }
                    if (!dr.IsDBNull(15)) { obj.anio = dr.GetInt32(15); }
                    if (!dr.IsDBNull(16)) { obj.cod_tipo_liq = dr.GetInt32(16); }
                    if (!dr.IsDBNull(17)) { obj.nro_liquidacion = dr.GetInt32(17); }
                    //obj.lstDetalle = Planilla_Sueldos_DetalleRep.read(obj.anio, obj.cod_tipo_liq, obj.nro_liquidacion, obj.legajo);
                    if (!dr.IsDBNull(18)) { obj.cod_concepto_liq = dr.GetInt32(18); }
                    if (!dr.IsDBNull(19)) { obj.desc_concepto_liq = dr.GetString(19); }
                    if (!dr.IsDBNull(20)) { obj.suma = dr.GetBoolean(20); }
                    if (!dr.IsDBNull(21)) { obj.sujeto_a_desc = dr.GetBoolean(21); }
                    //22-->sac
                    if (!dr.IsDBNull(23)) { obj.unidades = Convert.ToDecimal(dr[23]); }
                    if (!dr.IsDBNull(24)) { obj.importe = dr.GetDecimal(24); }

                    if (obj.suma)
                    {
                        if (obj.sujeto_a_desc)
                            obj.hab_con_descuento = obj.importe;
                        else
                            obj.hab_sin_descuento = obj.importe;
                    }
                    else
                    {
                        obj.descuentos = obj.importe;
                    }
                    if (!dr.IsDBNull(25)) { obj.tipo_personal = dr.GetString(25); }
                    if (!dr.IsDBNull(26)) { obj.nro_documento = Convert.ToString(dr[26]); }
                    if (!dr.IsDBNull(27)) { obj.secretaria = dr.GetString(27); }
                    if (!dr.IsDBNull(28)) { obj.direccion = Convert.ToString(dr[28]); }
                    if (!dr.IsDBNull(29)) { obj.oficina = Convert.ToString(dr[29]); }
                    //if (item2.suma)
                    //    if (item2.sujeto_a_desc)
                    //    {
                    //        objDetalle.hab_con_descuento = item2.importe;
                    //        tot_hab_con_descuento += item2.importe;
                    //    }
                    //    else
                    //    {
                    //        objDetalle.hab_sin_descuento = item2.importe;
                    //        tot_sin_con_descuento += item2.importe;
                    //    }
                    //else
                    //{
                    //    objDetalle.descuentos = item2.importe;
                    //    tot_descuento += item2.importe;
                    //}
                    //if (!dr.IsDBNull(30)) { obj.id_secretaria = dr.GetString(27); }
                    //if (!dr.IsDBNull(31)) { obj.id_direccion = Convert.ToString(dr[28]); }
                    //if (!dr.IsDBNull(32)) { obj.id_direccion = Convert.ToString(dr[29]); }
                    lst.Add(obj);

                    //cod_concepto_liq = 0;
                    //desc_concepto_liq = "";
                    //unidades = 0;
                    //hab_sin_descuento = 0;
                    //hab_con_descuento = 0;
                    //descuentos = 0;
                    //c1.cod_concepto_liq,");
                    //c1.des_concepto_liq,");
                    //c1.suma,");
                    //c1.sujeto_a_desc,");
                    //c1.sac,");
                    //d.unidades,");
                    //d.importe");

                }
            }
            return lst;
        }

        public static List<Planilla_SueldosRep> read(int anio, int cod_tipo_liq, int nro_liq)
        {
            try
            {
                StringBuilder strSQL = new StringBuilder();

                strSQL.AppendLine("SELECT");
                strSQL.AppendLine("e.legajo,");
                strSQL.AppendLine("e.cuil,");
                strSQL.AppendLine("e.fecha_nacimiento,");
                strSQL.AppendLine("e.tarea,");
                strSQL.AppendLine("ca.desc_cargo as cargo,");
                strSQL.AppendLine("e.sexo,");
                strSQL.AppendLine("e.fecha_ingreso,  ");
                strSQL.AppendLine("e.nombre,");
                strSQL.AppendLine("e.cod_categoria,");
                strSQL.AppendLine("e.cod_seccion,");
                strSQL.AppendLine("c.sueldo_basico,");
                strSQL.AppendLine("l.sueldo_neto,");
                strSQL.AppendLine("sueldo_bruto = (Select importe From Det_Liq_x_Empleado");
                strSQL.AppendLine("Where anio = l.anio AND");
                strSQL.AppendLine("cod_tipo_liq = l.cod_tipo_liq AND");
                strSQL.AppendLine("nro_liquidacion = l.nro_liquidacion AND");
                strSQL.AppendLine("cod_concepto_liq = 390 AND");
                strSQL.AppendLine("legajo = l.legajo),");
                strSQL.AppendLine("e.cod_banco,");
                strSQL.AppendLine("e.nro_caja_ahorro, l.anio, l.cod_tipo_liq, l.nro_liquidacion");
                strSQL.AppendLine(",");
                //
                strSQL.AppendLine("c1.cod_concepto_liq,");
                strSQL.AppendLine("c1.des_concepto_liq,");
                strSQL.AppendLine("c1.suma,");
                strSQL.AppendLine("c1.sujeto_a_desc,");
                strSQL.AppendLine("c1.sac,");
                strSQL.AppendLine("d.unidades,");
                strSQL.AppendLine("d.importe, cp.des_clasif_per as tipo_personal, e.nro_documento, ");
                //
                strSQL.AppendLine(" '' as Secretaria, '' as Direccion, ");
                strSQL.AppendLine(" '0' as id_secretaria, '0' as id_direccion");
                //
                strSQL.AppendLine("FROM Liq_x_Empleado l");
                strSQL.AppendLine("INNER JOIN EMPLEADOS e on");
                strSQL.AppendLine("e.legajo = l.legajo");
                strSQL.AppendLine("INNER JOIN CATEGORIAS c on");
                strSQL.AppendLine("e.cod_categoria = c.cod_categoria");
                strSQL.AppendLine("INNER JOIN CARGOS ca on");
                strSQL.AppendLine("e.cod_cargo = ca.cod_cargo");
                //
                //strSQL.AppendLine("SELECT");
                //strSQL.AppendLine("d.anio,");
                //strSQL.AppendLine("d.cod_tipo_liq,");
                //strSQL.AppendLine("d.nro_liquidacion,");
                //strSQL.AppendLine("d.legajo,");
                //strSQL.AppendLine("c.cod_concepto_liq,");
                //strSQL.AppendLine("c.des_concepto_liq,");
                //strSQL.AppendLine("c.suma,");
                //strSQL.AppendLine("c.sujeto_a_desc,");
                //strSQL.AppendLine("c.sac,");
                //strSQL.AppendLine("d.unidades,");
                //strSQL.AppendLine("d.importe,");
                //strSQL.AppendLine("d.nro_orden");
                strSQL.AppendLine("INNER JOIN Det_Liq_x_Empleado d on");
                strSQL.AppendLine("d.cod_concepto_liq <> 390 AND");
                strSQL.AppendLine("d.anio = l.anio AND");
                strSQL.AppendLine("d.cod_tipo_liq = l.cod_tipo_liq AND");
                strSQL.AppendLine("d.nro_liquidacion = l.nro_liquidacion AND");
                strSQL.AppendLine("d.legajo = l.legajo");
                strSQL.AppendLine("INNER JOIN Conceptos_Liquidacion c1 ON");
                strSQL.AppendLine("d.cod_concepto_liq = c1.cod_concepto_liq");
                //strSQL.AppendLine("WHERE");
                //strSQL.AppendLine("d.cod_concepto_liq <> 390 AND");
                //strSQL.AppendLine("d.anio = @anio AND");
                //strSQL.AppendLine("d.cod_tipo_liq = @cod_tipo_liq AND");
                //strSQL.AppendLine("d.nro_liquidacion = @nro_liq AND");
                //strSQL.AppendLine("d.legajo = @legajo");
                //strSQL.AppendLine("order by");
                //strSQL.AppendLine("d.cod_concepto_liq");
                strSQL.AppendLine("INNER JOIN CLASIFICACIONES_PERSONAL cp ON");
                strSQL.AppendLine("cp.cod_clasif_per = e.cod_clasif_per");
                //
                strSQL.AppendLine("WHERE");
                strSQL.AppendLine("l.anio=@anio and");
                strSQL.AppendLine("l.cod_tipo_liq=@cod_tipo_liq and");
                strSQL.AppendLine("l.nro_liquidacion=@nro_liq");
                strSQL.AppendLine("ORDER BY e.legajo, d.cod_concepto_liq");
                List<Planilla_SueldosRep> lst = new List<Planilla_SueldosRep>();
                using (SqlConnection con = DAL.DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liq", nro_liq);
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


        public static List<Planilla_SueldosRep> readxSecretaria(int anio, int cod_tipo_liq, int nro_liq)
        {
            try
            {
                //ltrim(rtrim(o.nombre_oficina)) as Oficina,
                //e.id_secretaria, e.id_direccion, e.id_oficina
                string strSQL;
                strSQL = @" SELECT 
                                   e.legajo, e.cuil, e.fecha_nacimiento, e.tarea, ca.desc_cargo as cargo,
                                   e.sexo, e.fecha_ingreso, e.nombre, e.cod_categoria, e.cod_seccion,
                                   c.sueldo_basico, l.sueldo_neto,
                                    sueldo_bruto = (Select importe From Det_Liq_x_Empleado
                                    Where anio = l.anio AND
                                    cod_tipo_liq = l.cod_tipo_liq AND
                                    nro_liquidacion = l.nro_liquidacion AND
                                    cod_concepto_liq = 390 AND
                                    legajo = l.legajo),
                                    e.cod_banco,
                                    e.nro_caja_ahorro, l.anio, l.cod_tipo_liq, l.nro_liquidacion,
                                    c1.cod_concepto_liq, c1.des_concepto_liq, c1.suma,
                                    c1.sujeto_a_desc, c1.sac, d.unidades,
                                    d.importe, cp.des_clasif_per as tipo_personal, e.nro_documento,
                rtrim(ltrim(s.descripcion)) as Secretaria, rtrim(ltrim(d1.descripcion)) as Direccion, 
                e.id_secretaria, e.id_direccion
                FROM Liq_x_Empleado l
                INNER JOIN EMPLEADOS e on
                e.legajo = l.legajo
                inner join secretaria s on          
                s.id_secretaria=e.id_secretaria
                inner join direccion d1 on
                d1.id_direccion=e.id_direccion    
                inner join oficinas o on
                o.codigo_oficina=e.id_oficina
                INNER JOIN CATEGORIAS c on
                e.cod_categoria = c.cod_categoria
                INNER JOIN CARGOS ca on
                e.cod_cargo = ca.cod_cargo
                INNER JOIN Det_Liq_x_Empleado d on
                d.cod_concepto_liq <> 390 AND
                d.anio = l.anio AND
                d.cod_tipo_liq = l.cod_tipo_liq AND
                d.nro_liquidacion = l.nro_liquidacion AND
                d.legajo = l.legajo
                INNER JOIN Conceptos_Liquidacion c1 ON
                d.cod_concepto_liq = c1.cod_concepto_liq
                INNER JOIN CLASIFICACIONES_PERSONAL cp ON
                cp.cod_clasif_per = e.cod_clasif_per
                WHERE
                l.anio=@anio and
                l.cod_tipo_liq=@cod_tipo_liq and
                l.nro_liquidacion=@nro_liq
                ORDER BY e.id_secretaria, e.id_direccion, e.legajo, d.cod_concepto_liq";
                List<Planilla_SueldosRep> lst = new List<Planilla_SueldosRep>();
                using (SqlConnection con = DAL.DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL;
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liq", nro_liq);
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