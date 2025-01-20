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
    public class Acred_bco_macroD : DALBase
    {


        private static List<Entities.Acred_bco_macro> mapeo(SqlDataReader dr)
        {
            List<Entities.Acred_bco_macro> lst = new List<Entities.Acred_bco_macro>();
            Entities.Acred_bco_macro obj;
            //string[] words = text.Split(delimiterChars);
            string nombre;
            bool si = false;
            int i, j;

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    obj = new Entities.Acred_bco_macro();
                    //if (!dr.IsDBNull(0)) { obj.codigo_empresa = dr.GetString(0); }
                    //12/05/2020, Le saque dni y le puse valor por defecto 00
                    //if (!dr.IsDBNull(0)) { obj.tipo_dni = Convert.ToString(dr.GetInt32(0)).PadLeft(2, Convert.ToChar("0")); }
                    obj.tipo_dni = "00";
                    if (!dr.IsDBNull(1)) { obj.nro_documento = dr.GetString(1).Trim().PadLeft(8, Convert.ToChar("0")); }
                    if (!dr.IsDBNull(2))
                    {
                        nombre = dr.GetString(2).Trim();
                        si = nombre.Contains(",");
                        if (si)
                        {
                            i = nombre.IndexOf(",");
                            j = nombre.Length - nombre.IndexOf(",");
                        }
                        //else
                        //{
                        //    i = nombre.Count();
                        //    j = 0;
                        //}
                        else
                        {
                            si = nombre.Contains(" ");
                            if (si)
                            {
                                i = nombre.IndexOf(" ");
                                j = nombre.Length - nombre.IndexOf(" ");
                            }
                            else
                            {
                                i = nombre.Count();
                                j = 0;
                            }
                        }


                        obj.apellido_beneficiario = nombre.Substring(0, i).Trim().PadRight(20, Convert.ToChar(" ")).Substring(0, 20);
                        obj.nombre_beneficiario = nombre.Substring(i, j).Replace(",", "").Trim().PadRight(20, Convert.ToChar(" ")).Substring(0, 20);
                    }
                    else
                    {
                        obj.apellido_beneficiario = string.Empty.PadRight(20, Convert.ToChar(" "));
                        obj.nombre_beneficiario = string.Empty.PadRight(20, Convert.ToChar(" "));
                    }

                    //If InStr(rs1("nombre").Value, ",") Then
                    //i = InStr(rs1("nombre").Value, ",") - 1
                    //j = Len(rs1("nombre").Value) - InStr(rs1("nombre").Value, ",")
                    //Else
                    //i = 30
                    //j = 0
                    //End If
                    //RS2("apellido_beneficiario").Value = UCase(Format$(Trim(Left(rs1("nombre").Value, i)), Space(30)))
                    //RS2("nombre_beneficiario").Value = UCase(Format$(Trim(Right(rs1("nombre").Value, j)), Space(30)))


                    if (!dr.IsDBNull(3)) { obj.tipo_cuenta = dr.GetString(3); }
                    if (obj.tipo_cuenta == "1")
                        //Cuenta corriente
                        obj.tipo_cuenta = "3";
                    else
                        //Caja de ahorro
                        obj.tipo_cuenta = "4";
                    if (!dr.IsDBNull(4)) { obj.nro_cbu = Convert.ToString(dr.GetString(4)).PadLeft(22, Convert.ToChar("0")); }
                    if (!dr.IsDBNull(5))
                    {
                        obj.sueldo_neto = Convert.ToString(dr.GetDecimal(5)).Replace(".", "").PadLeft(12, Convert.ToChar("0"));
                    }
                    //if (!dr.IsDBNull(8)) { obj.salto_de_fila = dr.GetString(8); }
                    lst.Add(obj);
                }
            }
            return lst;
        }

        private static List<Entities.Pago_sueldo_macro> mapeo2(SqlDataReader dr)
        {
            List<Entities.Pago_sueldo_macro> lst = new List<Entities.Pago_sueldo_macro>();
            Entities.Pago_sueldo_macro obj;
            string importe;
            bool si = false;
            int i, j;

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    obj = new Entities.Pago_sueldo_macro();
                    if (!dr.IsDBNull(0)) { obj.legajo = Convert.ToString(dr.GetInt32(0)).PadLeft(7, '0'); }
                    if (!dr.IsDBNull(1)) { obj.cuil = dr.GetString(1).Replace("-", "").PadLeft(11, '0'); }
                    if (!dr.IsDBNull(2)) { obj.apeynom = dr.GetString(2).Replace(",", "").Trim().PadRight(19, ' '); }
                    if (!dr.IsDBNull(3))
                    {
                        if (dr.GetString(3).Trim().Length == 15)
                            obj.cuenta = dr.GetString(3).Replace("-", "").Replace("/", "").PadRight(15, ' ');
                        else
                            //obj.cuenta = " ".ToString().PadRight(15, ' ');
                            obj.cuenta = string.Empty.PadRight(15, ' ');
                    }
                    if (!dr.IsDBNull(4)) { obj.cbu = dr.GetString(4).PadRight(22, ' '); }
                    if (!dr.IsDBNull(5))
                    {
                        importe = Convert.ToString(dr.GetDecimal(5));
                        si = importe.Contains(".");
                        if (si)
                        {
                            i = importe.IndexOf(".");
                            j = importe.Length - importe.IndexOf(".");
                            obj.importe = importe.Substring(0, i).Trim().PadRight(7, Convert.ToChar(" ")).Substring(0, 7);
                        }
                        else
                        {
                            obj.importe = Convert.ToString(dr.GetDecimal(5)).PadRight(7, ' ');
                        }
                    }
                    if (!dr.IsDBNull(6)) { obj.comprobante = Convert.ToString(dr.GetInt32(6)); }
                    lst.Add(obj);
                }
            }
            return lst;
        }

        public static List<Pago_sueldo_macro> GetAcred_bco_macro_nvo_formato(int anio, int cod_tipo_liq, int nro_liquidacion, decimal porcentaje)
        {
            try
            {
                List<Entities.Pago_sueldo_macro> lst = new List<Entities.Pago_sueldo_macro>();
                StringBuilder strSQL = new StringBuilder();

                strSQL.AppendLine("SELECT");
                strSQL.AppendLine("e.legajo,");
                strSQL.AppendLine("e.cuil,");
                //strSQL.AppendLine("e.nombre,");
                strSQL.AppendLine("SUBSTRING(nombre, 0, 19) as nombre,");
                strSQL.AppendLine("e.nro_caja_ahorro as cuenta,");
                //strSQL.AppendLine("'0' as cuenta,");
                strSQL.AppendLine("e.nro_cbu,");
                strSQL.AppendLine("Convert(decimal(15, 2), Round((l.sueldo_neto * 100 / 100), 0, 1)) as importe,");
                strSQL.AppendLine("l.nro_orden");
                strSQL.AppendLine("FROM liq_x_empleado l with(nolock)");
                strSQL.AppendLine("join Empleados e on");
                strSQL.AppendLine("l.legajo = e.legajo and e.cod_banco = 255");
                strSQL.AppendLine("join liquidaciones l1 on  l.anio = l1.anio and");
                strSQL.AppendLine("l.cod_tipo_liq = l1.cod_tipo_liq and");
                strSQL.AppendLine("l.nro_liquidacion = l1.nro_liquidacion");
                strSQL.AppendLine("WHERE");
                //strSQL.AppendLine("e.listar = 1 and
                strSQL.AppendLine("l.anio=@anio and");
                strSQL.AppendLine("l.cod_tipo_liq=@cod_tipo_liq and");
                strSQL.AppendLine("l.nro_liquidacion=@nro_liquidacion");
                strSQL.AppendLine("ORDER by l.legajo");
                using (SqlConnection con = GetConnection("Siimva"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                    cmd.Parameters.AddWithValue("@porcentaje", porcentaje);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    lst = mapeo2(dr);
                    return lst;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static List<Entities.Acred_bco_macro> GetAcred_bco_macro(int anio, int cod_tipo_liq, int nro_liquidacion, decimal porcentaje)
        {
            try
            {
                List<Entities.Acred_bco_macro> lst = new List<Entities.Acred_bco_macro>();
                StringBuilder strSQL = new StringBuilder();

                strSQL.AppendLine("SELECT e.cod_tipo_documento, e.nro_documento, e.nombre,");
                strSQL.AppendLine("e.tipo_cuenta, e.nro_cbu,");
                //strSQL.AppendLine("l.sueldo_neto ");
                strSQL.AppendLine("Convert(decimal(15,2),Round((l.sueldo_neto * @porcentaje / 100 ),0,1)) as sueldo_neto");
                strSQL.AppendLine("FROM liq_x_empleado l with(nolock)");
                strSQL.AppendLine("join Empleados e on");
                //strSQL.AppendLine("e.listar = 1 and l.legajo = e.legajo and e.cod_banco = 255");
                //strSQL.AppendLine("e.cod_clasif_per<>10 and");
                strSQL.AppendLine("l.legajo = e.legajo and e.cod_banco = 255");
                strSQL.AppendLine("join liquidaciones l1 on  l.anio = l1.anio and");
                strSQL.AppendLine("l.cod_tipo_liq = l1.cod_tipo_liq and");
                strSQL.AppendLine("l.nro_liquidacion = l1.nro_liquidacion");
                strSQL.AppendLine("WHERE e.listar=1 and");
                strSQL.AppendLine("l.anio=@anio and");
                strSQL.AppendLine("l.cod_tipo_liq=@cod_tipo_liq and");
                strSQL.AppendLine("l.nro_liquidacion=@nro_liquidacion");
                strSQL.AppendLine("ORDER by l.legajo");


                using (SqlConnection con = GetConnection("Siimva"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liquidacion);
                    cmd.Parameters.AddWithValue("@porcentaje", porcentaje);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();

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
        public static List<Entities.Acred_bco_macro> read()
        {
            try
            {
                List<Entities.Acred_bco_macro> lst = new List<Entities.Acred_bco_macro>();
                using (SqlConnection con = GetConnection("Siimva"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM ACRED_BCO_MACRO";
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

        public static Entities.Acred_bco_macro getByPk()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT * FROM ACRED_BCO_MACRO WHERE");
                Entities.Acred_bco_macro obj = null;
                using (SqlConnection con = GetConnection("Siimva"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<Entities.Acred_bco_macro> lst = mapeo(dr);
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

        public static int insert(Entities.Acred_bco_macro obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO ACRED_BCO_MACRO(");
                sql.AppendLine("codigo_empresa");
                sql.AppendLine(", tipo_dni");
                sql.AppendLine(", nro_documento");
                sql.AppendLine(", nombre_beneficiario");
                sql.AppendLine(", apellido_beneficiario");
                sql.AppendLine(", tipo_cuenta");
                sql.AppendLine(", nro_cbu");
                sql.AppendLine(", sueldo_neto");
                sql.AppendLine(", salto_de_fila");
                sql.AppendLine(")");
                sql.AppendLine("VALUES");
                sql.AppendLine("(");
                sql.AppendLine("@codigo_empresa");
                sql.AppendLine(", @tipo_dni");
                sql.AppendLine(", @nro_documento");
                sql.AppendLine(", @nombre_beneficiario");
                sql.AppendLine(", @apellido_beneficiario");
                sql.AppendLine(", @tipo_cuenta");
                sql.AppendLine(", @nro_cbu");
                sql.AppendLine(", @sueldo_neto");
                sql.AppendLine(", @salto_de_fila");
                sql.AppendLine(")");
                sql.AppendLine("SELECT SCOPE_IDENTITY()");
                using (SqlConnection con = GetConnection("Siimva"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@codigo_empresa", obj.codigo_empresa);
                    cmd.Parameters.AddWithValue("@tipo_dni", obj.tipo_dni);
                    cmd.Parameters.AddWithValue("@nro_documento", obj.nro_documento);
                    cmd.Parameters.AddWithValue("@nombre_beneficiario", obj.nombre_beneficiario);
                    cmd.Parameters.AddWithValue("@apellido_beneficiario", obj.apellido_beneficiario);
                    cmd.Parameters.AddWithValue("@tipo_cuenta", obj.tipo_cuenta);
                    cmd.Parameters.AddWithValue("@nro_cbu", obj.nro_cbu);
                    cmd.Parameters.AddWithValue("@sueldo_neto", obj.sueldo_neto);
                    cmd.Parameters.AddWithValue("@salto_de_fila", obj.salto_de_fila);
                    cmd.Connection.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void update(Entities.Acred_bco_macro obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE  ACRED_BCO_MACRO SET");
                sql.AppendLine("codigo_empresa=@codigo_empresa");
                sql.AppendLine(", tipo_dni=@tipo_dni");
                sql.AppendLine(", nro_documento=@nro_documento");
                sql.AppendLine(", nombre_beneficiario=@nombre_beneficiario");
                sql.AppendLine(", apellido_beneficiario=@apellido_beneficiario");
                sql.AppendLine(", tipo_cuenta=@tipo_cuenta");
                sql.AppendLine(", nro_cbu=@nro_cbu");
                sql.AppendLine(", sueldo_neto=@sueldo_neto");
                sql.AppendLine(", salto_de_fila=@salto_de_fila");
                sql.AppendLine("WHERE");
                using (SqlConnection con = GetConnection("Siimva"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@codigo_empresa", obj.codigo_empresa);
                    cmd.Parameters.AddWithValue("@tipo_dni", obj.tipo_dni);
                    cmd.Parameters.AddWithValue("@nro_documento", obj.nro_documento);
                    cmd.Parameters.AddWithValue("@nombre_beneficiario", obj.nombre_beneficiario);
                    cmd.Parameters.AddWithValue("@apellido_beneficiario", obj.apellido_beneficiario);
                    cmd.Parameters.AddWithValue("@tipo_cuenta", obj.tipo_cuenta);
                    cmd.Parameters.AddWithValue("@nro_cbu", obj.nro_cbu);
                    cmd.Parameters.AddWithValue("@sueldo_neto", obj.sueldo_neto);
                    cmd.Parameters.AddWithValue("@salto_de_fila", obj.salto_de_fila);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void delete(Entities.Acred_bco_macro obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("DELETE  ACRED_BCO_MACRO ");
                sql.AppendLine("WHERE");
                using (SqlConnection con = GetConnection("Siimva"))
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

//nombreyapellido = dr.GetString(2).Split(Convert.ToChar(","));
//                        if (nombreyapellido.Count() >= 1)
//                        {
//                            obj.apellido_beneficiario = nombreyapellido[0].Trim().PadRight(20, Convert.ToChar(" "));
//                            obj.nombre_beneficiario = nombreyapellido[1].Trim().PadRight(20, Convert.ToChar(" "));
//                        }
//                        else
//                        {
//                            nombreyapellido = dr.GetString(2).Split(Convert.ToChar(" "));
//                            obj.apellido_beneficiario = nombreyapellido[0].Trim().PadRight(20, Convert.ToChar(" "));
//                            obj.nombre_beneficiario = nombreyapellido[1].Trim().PadRight(20, Convert.ToChar(" "));
//                        }