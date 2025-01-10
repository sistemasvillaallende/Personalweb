using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace web.reportes
{
    public class Etiquetas
    {

        public int legajo1 { get; set; }
        public string nombre1 { get; set; }
        public string banco1 { get; set; }
        public string nro_caja_ahorro1 { get; set; }
        public decimal sueldo_neto1 { get; set; }

        public int legajo2 { get; set; }
        public string nombre2 { get; set; }
        public string banco2 { get; set; }
        public string nro_caja_ahorro2 { get; set; }
        public decimal sueldo_neto2 { get; set; }

        public int legajo3 { get; set; }
        public string nombre3 { get; set; }
        public string banco3 { get; set; }
        public string nro_caja_ahorro3 { get; set; }
        public decimal sueldo_neto3 { get; set; }


        public Etiquetas()
        {
            legajo1 = 0;
            nombre1 = string.Empty;
            banco1 = string.Empty;
            nro_caja_ahorro1 = string.Empty;
            sueldo_neto1 = 0;

            legajo2 = 0;
            nombre2 = string.Empty;
            banco2 = string.Empty;
            nro_caja_ahorro2 = string.Empty;
            sueldo_neto2 = 0;

            legajo3 = 0;
            nombre3 = string.Empty;
            banco3 = string.Empty;
            nro_caja_ahorro3 = string.Empty;
            sueldo_neto3 = 0;
        }

        private static List<Etiquetas> mapeo(SqlDataReader dr)
        {
            List<Etiquetas> lst = new List<Etiquetas>();
            Etiquetas obj;
            obj = new Etiquetas();
            int etiqueta = 0;

            if (dr.Read())
            {
                var loop = true;

                while (loop)
                {
                    if (loop)
                    {
                        switch (etiqueta)
                        {
                            case 0:
                                if (!dr.IsDBNull(0)) { obj.legajo1 = dr.GetInt32(0); }
                                if (!dr.IsDBNull(1)) { obj.nombre1 = dr.GetString(1); }
                                if (!dr.IsDBNull(2)) { obj.banco1 = dr.GetString(2); }
                                if (!dr.IsDBNull(3)) { obj.nro_caja_ahorro1 = dr.GetString(3); }
                                if (!dr.IsDBNull(4)) { obj.sueldo_neto1 = dr.GetDecimal(4); }
                                break;
                            case 1:
                                if (!dr.IsDBNull(0)) { obj.legajo2 = dr.GetInt32(0); }
                                if (!dr.IsDBNull(1)) { obj.nombre2 = dr.GetString(1); }
                                if (!dr.IsDBNull(2)) { obj.banco2 = dr.GetString(2); }
                                if (!dr.IsDBNull(3)) { obj.nro_caja_ahorro2 = dr.GetString(3); }
                                if (!dr.IsDBNull(4)) { obj.sueldo_neto2 = dr.GetDecimal(4); }
                                break;
                            case 2:
                                if (!dr.IsDBNull(0)) { obj.legajo3 = dr.GetInt32(0); }
                                if (!dr.IsDBNull(1)) { obj.nombre3 = dr.GetString(1); }
                                if (!dr.IsDBNull(2)) { obj.banco3 = dr.GetString(2); }
                                if (!dr.IsDBNull(3)) { obj.nro_caja_ahorro3 = dr.GetString(3); }
                                if (!dr.IsDBNull(4)) { obj.sueldo_neto3 = dr.GetDecimal(4); }
                                break;
                            default:
                                break;
                        }
                        etiqueta++;
                        if (etiqueta == 3)
                        {
                            lst.Add(obj);
                            obj = new Etiquetas();
                            etiqueta = 0;
                        }
                        loop = dr.Read();
                        if (!loop)
                            lst.Add(obj);


                    }
                }
            }
            return lst;

        }
        public static List<Etiquetas> read(int anio, int cod_tipo_liq, int nro_liq)
        {
            try
            {
                StringBuilder strSQL = new StringBuilder();

                strSQL.AppendLine("SELECT");
                strSQL.AppendLine("e.legajo,");
                strSQL.AppendLine("e.nombre,");
                strSQL.AppendLine("b.nom_banco,");
                strSQL.AppendLine("e.nro_caja_ahorro,");
                strSQL.AppendLine("importe_total = l1.sueldo_neto");
                strSQL.AppendLine("FROM LIQUIDACIONES l WITH(NOLOCK)");
                strSQL.AppendLine("JOIN LIQ_X_EMPLEADO l1 ON");
                strSQL.AppendLine("l.anio = l1.anio AND");
                strSQL.AppendLine("l.cod_tipo_liq = l1.cod_tipo_liq AND");
                strSQL.AppendLine("l.nro_liquidacion = l1.nro_liquidacion");
                strSQL.AppendLine("JOIN EMPLEADOS e ON");
                strSQL.AppendLine("l1.legajo = e.legajo");
                strSQL.AppendLine("JOIN Bancos b ON");
                strSQL.AppendLine("b.cod_banco = e.cod_banco");
                strSQL.AppendLine("WHERE");
                strSQL.AppendLine("l.anio=@anio and");
                strSQL.AppendLine("l.cod_tipo_liq=@cod_tipo_liq and");
                strSQL.AppendLine("l.nro_liquidacion=@nro_liq");
                strSQL.AppendLine("ORDER BY e.legajo");
                List<Etiquetas> lst = new List<Etiquetas>();
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

    }
}