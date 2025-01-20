using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EstadisticaSueldos : DALBase
    {
        public string nombre { get; set; }
        public int[] valores { get; set; }

        EstadisticaSueldos()
        {
            nombre = string.Empty;
            valores = null;
        }
        public static object readSueldosPlanta(int mes, int anio)
        {
            try
            {
                string m = string.Empty;
                if (mes < 10)
                    m = string.Format("0{0}", mes);
                else
                    m = mes.ToString();

                string periodo = string.Format("{0}{1}", anio, m);
                List<int> lstBasePlanta = new List<int>();
                List<int> lstBaseContratados = new List<int>();
                List<int> lstBasePlantaPolitica = new List<int>();
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                    @"SELECT cod_clasif_per, CONVERT(INT, ROUND((sueldo_bruto / 10000),0) * 10000)
                    FROM LIQ_X_EMPLEADO
                    WHERE anio=@anio AND nro_liquidacion=(
                    SELECT nro_liquidacion FROM LIQUIDACIONES
                    WHERE anio=@anio AND periodo=@periodo
                    AND publica=1 AND cod_tipo_liq=1)

                    UNION

                    SELECT cod_clasif_per, CONVERT(INT, ROUND((sueldo_bruto / 10000),0) * 10000)
                    FROM LIQ_X_EMPLEADO
                    WHERE anio=@anio AND nro_liquidacion = (
	                    SELECT nro_liquidacion FROM LIQUIDACIONES
	                    WHERE anio=@anio AND periodo=@periodo
	                    AND publica=1 AND cod_tipo_liq=8)
                    AND cod_tipo_liq=8
                    ORDER BY cod_clasif_per, 
                    CONVERT(INT, ROUND((sueldo_bruto / 10000),0) * 10000) ASC";

                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@periodo", periodo);

                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            if (dr.GetInt32(0) == 1)
                                lstBasePlanta.Add(dr.GetInt32(1));
                            if (dr.GetInt32(0) == 2)
                                lstBaseContratados.Add(dr.GetInt32(1));
                            if (dr.GetInt32(0) == 10)
                                lstBasePlantaPolitica.Add(dr.GetInt32(1));
                        }
                    }

                    int maxPlanta = lstBasePlanta.Count() - 1;
                    int maxContrato = lstBaseContratados.Count() - 1;
                    int maxPlantaPolitica = lstBasePlantaPolitica.Count() - 1;

                    List<DiccionarioDonut> lstPlanta = new List<DiccionarioDonut>();
                    lstPlanta.Add(new DiccionarioDonut(
                        "Minimo Planta", lstBasePlanta[0]));
                    lstPlanta.Add(new DiccionarioDonut(
                        "Media Planta", Convert.ToInt32(lstBasePlanta.Average())));
                    lstPlanta.Add(new DiccionarioDonut(
                        "Mediana Planta", mediana(lstBasePlanta)));
                    lstPlanta.Add(new DiccionarioDonut(
                        "Moda Planta", moda(lstBasePlanta)));
                    lstPlanta.Add(new DiccionarioDonut(
                        "Maximo Planta", lstBasePlanta[maxPlanta]));

                    lstPlanta.OrderBy(x=>x.valor);

                    List<DiccionarioDonut> lstContrato = new List<DiccionarioDonut>();
                    lstContrato.Add(new DiccionarioDonut(
                        "Minimo Planta", lstBaseContratados[0]));
                    lstContrato.Add(new DiccionarioDonut(
                        "Media Planta", Convert.ToInt32(lstBaseContratados.Average())));
                    lstContrato.Add(new DiccionarioDonut(
                        "Mediana Planta", mediana(lstBaseContratados)));
                    lstContrato.Add(new DiccionarioDonut(
                        "Moda Planta", moda(lstBaseContratados)));
                    lstContrato.Add(new DiccionarioDonut(
                        "Maximo Planta", lstBaseContratados[maxContrato]));

                    lstPlanta.OrderBy(x => x.valor);


                    List<DiccionarioDonut> lstPlantaPolitica = new List<DiccionarioDonut>();
                    lstPlantaPolitica.Add(new DiccionarioDonut(
                        "Minimo Planta", lstBasePlantaPolitica[0]));
                    lstPlantaPolitica.Add(new DiccionarioDonut(
                        "Media Planta", Convert.ToInt32(lstBasePlantaPolitica.Average())));
                    lstPlantaPolitica.Add(new DiccionarioDonut(
                        "Mediana Planta", mediana(lstBasePlantaPolitica)));
                    lstPlantaPolitica.Add(new DiccionarioDonut(
                        "Moda Planta", moda(lstBasePlantaPolitica)));
                    lstPlantaPolitica.Add(new DiccionarioDonut(
                        "Maximo Planta", lstBasePlantaPolitica[maxPlantaPolitica]));

                    lstPlantaPolitica.OrderBy(x => x.valor);

                    List<DiccionarioDonut> lstSerie = new List<DiccionarioDonut>();
                    lstSerie.AddRange(lstPlanta);
                    lstSerie.AddRange(lstContrato);
                    lstSerie.AddRange(lstPlantaPolitica);

                    lstSerie.Sort((a, b) => a.valor.CompareTo(b.valor));

                    var datos = new List<object[]>
                    {
                        new object[] {"Planta Permanente", new[]
                            {
                                lstPlanta[0].valor,
                                lstPlanta[1].valor,
                                lstPlanta[2].valor,
                                lstPlanta[3].valor,
                                lstPlanta[4].valor
                            }
                        },
                        new object[] {"Personal Contratado", new[]
                            {
                                lstContrato[0].valor,
                                lstContrato[1].valor,
                                lstContrato[2].valor,
                                lstContrato[3].valor,
                                lstContrato[4].valor
                            }
                        },
                        new object[] {"Planta Politica", new[]
                            {
                                lstPlantaPolitica[0].valor,
                                lstPlantaPolitica[1].valor,
                                lstPlantaPolitica[2].valor,
                                lstPlantaPolitica[3].valor,
                                lstPlantaPolitica[4].valor
                            }
                        }
                    };
                    return datos;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<EstadisticaSueldos> readSueldosPlantaDireccion(
int mes, int anio, int idDireccion)
        {
            try
            {
                string m = string.Empty;
                if (mes < 10)
                    m = string.Format("0{0}", mes);
                else
                    m = mes.ToString();

                string periodo = string.Format("{0}{1}", anio, m);
                List<int> lstBasePlanta = new List<int>();
                List<int> lstBaseContratados = new List<int>();
                List<int> lstReturn = new List<int>();
                List<EstadisticaSueldos> lstRet = new List<EstadisticaSueldos>();
                using (SqlConnection con = GetConnection("SIIMVA"))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        @"SELECT b.cod_clasif_per, CONVERT(INT, ROUND(sueldo_bruto, 0, 1)) 
                        FROM LIQ_X_EMPLEADO A
                        INNER JOIN EMPLEADOS B ON A.legajo=B.legajo
                        WHERE A.anio=@anio AND A.nro_liquidacion=(
                        SELECT nro_liquidacion FROM LIQUIDACIONES
                        WHERE anio=@anio AND periodo=@periodo
                        AND publica=1 AND cod_tipo_liq=1)
                        AND B.id_direccion=@id_direccion
                        ORDER BY sueldo_bruto ASC";

                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@periodo", periodo);
                    cmd.Parameters.AddWithValue("@id_direccion", idDireccion);

                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            if (dr.GetInt32(0) == 1)
                                lstBasePlanta.Add(dr.GetInt32(1));
                            if (dr.GetInt32(0) == 2)
                                lstBaseContratados.Add(dr.GetInt32(1));
                        }
                    }
                    int max = 0;
                    //PERSONAL PLANTA
                    if (lstBasePlanta.Count() > 0)
                    {
                        max = lstBasePlanta.Count() - 1;
                        lstReturn.Add(lstBasePlanta[0]);
                        lstReturn.Add(Convert.ToInt32(lstBasePlanta.Average()));
                        lstReturn.Add(mediana(lstBasePlanta));
                        lstReturn.Add(moda(lstBasePlanta));
                        lstReturn.Add(lstBasePlanta[max]);
                    }
                    EstadisticaSueldos objEst = new EstadisticaSueldos();
                    objEst.nombre = "Personal Contratado";
                    objEst.valores = lstReturn.ToArray();
                    lstRet.Add(objEst);
                    //PERSONAL CONTRATADO
                    if (lstBaseContratados.Count() > 0)
                    {
                        //lstBaseContratados.Clear();
                        max = lstBaseContratados.Count() - 1;
                        lstReturn.Add(lstBaseContratados[0]);
                        lstReturn.Add(Convert.ToInt32(lstBaseContratados.Average()));
                        lstReturn.Add(mediana(lstBaseContratados));
                        lstReturn.Add(moda(lstBaseContratados));
                        lstReturn.Add(lstBaseContratados[max]);
                    }
                    objEst = new EstadisticaSueldos();
                    objEst.nombre = "Personal Contratado";
                    objEst.valores = lstReturn.ToArray();
                    lstRet.Add(objEst);
                }
                return lstRet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private static int mediana(List<int> lst)
        {
            int n = lst.Count;
            List<int> lstRound = new List<int>();
            double mediana = n % 2 == 0
                ? (lst[(n / 2) - 1] + lst[n / 2]) / 2.0
                : lst[n / 2];

            return Convert.ToInt32(mediana);
        }
        private static int RedondearA50K(int numero)
        {
            // Dividir el número por 50,000, redondear al entero más cercano y luego multiplicar por 50,000
            return (int)Math.Round(numero / 50000.0) * 50000;
        }
        private static int moda(List<int> lst)
        {
            //List<int> lstRound = new List<int>();
            //foreach (var item in lst)
            //{
            //    lstRound.Add(RedondearA50K(item));
            //}
            var grupos = lst.GroupBy(x => x);
            var moda = grupos.OrderByDescending(g => g.Count()).First().Key;
            return Convert.ToInt32(moda);
        }


    }
}
