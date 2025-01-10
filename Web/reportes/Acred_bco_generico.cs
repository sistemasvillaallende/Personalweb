using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace web.reportes
{
    public class Acred_bco_generico
    {


        public string codigo_empresa { get; set; }
        public string tipo_dni { get; set; }
        public string nro_documento { get; set; }
        public string nombre_beneficiario { get; set; }
        public string apellido_beneficiario { get; set; }
        public string tipo_cuenta { get; set; }
        public string nro_cbu { get; set; }
        public decimal sueldo_neto { get; set; }
        public string salto_de_fila { get; set; }

        public Acred_bco_generico()
        {
            codigo_empresa = "00000";
            tipo_dni = string.Empty;
            nro_documento = string.Empty;
            nombre_beneficiario = string.Empty;
            apellido_beneficiario = string.Empty;
            tipo_cuenta = string.Empty;
            nro_cbu = string.Empty;
            sueldo_neto = 0;
            salto_de_fila = string.Empty;
        }


        private static List<Acred_bco_generico> mapeo(SqlDataReader dr)
        {
            List<Acred_bco_generico> lst = new List<Acred_bco_generico>();
            Acred_bco_generico obj;
            string nombre;
            bool si = false;
            int i, j;

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    obj = new Acred_bco_generico();
                    //if (!dr.IsDBNull(0)) { obj.codigo_empresa = dr.GetString(0); }
                    if (!dr.IsDBNull(0)) { obj.tipo_dni = Convert.ToString(dr.GetInt32(0)).PadLeft(2, Convert.ToChar("0")); }
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
                        else
                        {
                            i = nombre.Count();
                            j = 0;
                        }
                        obj.apellido_beneficiario = nombre.Substring(0, i).Trim().PadRight(20, Convert.ToChar(" "));
                        obj.nombre_beneficiario = nombre.Substring(i, j).Replace(",", "").Trim().PadRight(20, Convert.ToChar(" "));
                    }
                    else
                    {
                        obj.apellido_beneficiario = string.Empty.PadRight(20, Convert.ToChar(" "));
                        obj.nombre_beneficiario = string.Empty.PadRight(20, Convert.ToChar(" "));
                    }

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
                        obj.sueldo_neto = dr.GetDecimal(5);
                    }
                    //if (!dr.IsDBNull(8)) { obj.salto_de_fila = dr.GetString(8); }
                    lst.Add(obj);
                }
            }
            return lst;
        }

        public static List<Acred_bco_generico> read(int anio, int cod_tipo_liq, int nro_liq, decimal porcentaje)
        {
            try
            {
                StringBuilder strSQL = new StringBuilder();

                strSQL.AppendLine("SELECT e.cod_tipo_documento, e.nro_documento, e.nombre,");
                strSQL.AppendLine("e.tipo_cuenta, e.nro_cbu,");
                //strSQL.AppendLine("l.sueldo_neto ");
                strSQL.AppendLine("Convert(decimal(15,2),Round((l.sueldo_neto * @porcentaje / 100 ),0,1)) as sueldo_neto");
                strSQL.AppendLine("FROM liq_x_empleado l with(nolock)");
                strSQL.AppendLine("join Empleados e on");
                strSQL.AppendLine("l.legajo = e.legajo and e.listar = 1 and e.cod_banco = 22");
                strSQL.AppendLine("join liquidaciones l1 on  l.anio = l1.anio and");
                strSQL.AppendLine("l.cod_tipo_liq = l1.cod_tipo_liq and");
                strSQL.AppendLine("l.nro_liquidacion = l1.nro_liquidacion");
                strSQL.AppendLine("WHERE");
                strSQL.AppendLine("l.anio=@anio and");
                strSQL.AppendLine("l.cod_tipo_liq=@cod_tipo_liq and");
                strSQL.AppendLine("l.nro_liquidacion=@nro_liquidacion");
                List<Acred_bco_generico> lst = new List<Acred_bco_generico>();
                using (SqlConnection con = DAL.DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@cod_tipo_liq", cod_tipo_liq);
                    cmd.Parameters.AddWithValue("@nro_liquidacion", nro_liq);
                    cmd.Parameters.AddWithValue("@porcentaje", porcentaje);
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
