using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Empleado
    {
        public int legajo { get; set; }
        public string nombre { get; set; }
        public string fecha_alta_registro { get; set; }

        public int cod_tipo_documento { get; set; }
        public string nro_documento { get; set; }
        public string fecha_nacimiento { get; set; }
        public string sexo { get; set; }
        public string pais_domicilio { get; set; }
        public string provincia_domicilio { get; set; }
        public string ciudad_domicilio { get; set; }
        public string barrio_domicilio { get; set; }
        public string calle_domicilio { get; set; }
        public string nro_domicilio { get; set; }
        public string piso_domicilio { get; set; }
        public string dpto_domicilio { get; set; }
        public string monoblock_domicilio { get; set; }
        public string telefonos { get; set; }
        public string celular { get; set; }
        public string cod_postal { get; set; }
        public int cod_estado_civil { get; set; }
        public string fecha_ingreso { get; set; }
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
        public string fecha_baja { get; set; }
        public int nro_contrato { get; set; }
        public string fecha_inicio_contrato { get; set; }
        public string fecha_fin_contrato { get; set; }
        public bool listar { get; set; }
        public int id_regimen { get; set; }
        public int id_secretaria { get; set; }
        public int id_direccion { get; set; }
        public string nro_nombramiento { get; set; }
        public string fecha_nombramiento { get; set; }
        public string usuario { get; set; }
        public int cod_escala_aumento { get; set; }
        public int cod_regimen_empleado { get; set; }
        public int id_oficina { get; set; }
        public string email { get; set; }
        public short imprime_recibo { get; set; }
        public int id_programa { get; set; }
        public int id_revista { get; set; }
        public string fecha_revista { get; set; }
        public bool activo { get; set; }


        public Empleado()
        {
            legajo = 0;
            fecha_alta_registro = DateTime.Today.ToString("dd/MM/yyyy");
            nombre = "";
            cod_tipo_documento = 0;
            nro_documento = "";
            fecha_nacimiento = DateTime.Today.ToString("dd/MM/yyyy");
            sexo = "";
            pais_domicilio = "";
            provincia_domicilio = "";
            ciudad_domicilio = "";
            barrio_domicilio = "";
            calle_domicilio = "";
            nro_domicilio = "";
            piso_domicilio = "";
            dpto_domicilio = "";
            monoblock_domicilio = "";
            telefonos = "";
            celular = "";
            cod_postal = "";
            cod_estado_civil = 0;
            fecha_ingreso = "";
            tarea = "";
            cod_seccion = 0;
            cod_categoria = 0;
            cod_cargo = 0;

            cod_banco = 0;
            nro_sucursal = "";
            tipo_cuenta = "";
            nro_caja_ahorro = "";
            nro_cbu = "";
            nro_ipam = "";
            cuil = "";

            nro_jubilacion = "";
            antiguedad_ant = 0;
            antiguedad_actual = 0;
            cod_clasif_per = 0;
            cod_tipo_liq = 0;
            nro_ult_liq = 0;
            anio_ult_liq = 0;
            nro_cta_sb = "";
            nro_cta_gastos = "";
            fecha_baja = "";
            nro_contrato = 0;
            cod_regimen_empleado = 0;
            fecha_inicio_contrato = "";
            fecha_fin_contrato = "";
            listar = false;
            id_regimen = 0;
            id_secretaria = 0;
            id_direccion = 0;
            imprime_recibo = 1;
            id_programa = 0;
            id_revista = 1;
            fecha_revista = string.Empty;
            activo = true;
        }

    }
}

