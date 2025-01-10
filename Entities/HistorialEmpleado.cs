using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class HistorialEmpleado
    {
        public int legajo { get; set; }
        public int nro_item { get; set; }
        public string fecha_movimiento { get; set; }
        public string nombre { get; set; }
        public string des_tipo_documento { get; set; }
        public string nro_documento { get; set; }
        public string fecha_nacimiento { get; set; }
        public string sexo { get; set; }
        public string ciudad_domicilio { get; set; }
        public string barrio_domicilio { get; set; }
        public string calle_domicilio { get; set; }
        public string nro_domicilio { get; set; }
        public string dpto_domicilio { get; set; }
        public string piso_domicilio { get; set; }
        public string monoblock_domicilio { get; set; }
        public string telefonos { get; set; }
        public string celular { get; set; }
        public string cod_postal { get; set; }
        public string des_estado_civil { get; set; }
        public string fecha_ingreso { get; set; }
        public string tarea { get; set; }
        public string des_seccion { get; set; }
        public int cod_categoria { get; set; }
        public decimal sueldo_basico { get; set; }
        public string desc_cargo { get; set; }
        public string nom_banco { get; set; }
        public string nro_sucursal { get; set; }
        public string des_tipo_cuenta { get; set; }
        public string nro_caja_ahorro { get; set; }
        public string nro_ipam { get; set; }
        public string cuil { get; set; }
        public int antiguedad_ant { get; set; }
        public int antigudad_actual { get; set; }
        public string des_clasif_per { get; set; }
        public string des_tipo_liq { get; set; }
        public string nro_cta_sb { get; set; }
        public string nro_cta_gastos { get; set; }
        public string fecha_baja { get; set; }
        public int nro_contrato { get; set; }
        public string fecha_inicio_contrato { get; set; }
        public string fecha_fin_contrato { get; set; }
        public int id_regimen { get; set; }
        public string des_secretaria { get; set; }
        public string des_direccion { get; set; }
        public int cod_escala_aumento { get; set; }
        public string email { get; set; }
        public int cod_regimen_empleado { get; set; }
        public string des_regimen_empleado { get; set; }
        public Int16 imprime_recibo { get; set; }
        public string programa { get; set; }
        public string situacion_revista { get; set; }
        public string des_tipo_auditoria { get; set; }
        public string obsauditoria { get; set; }

        public HistorialEmpleado()
        {
            legajo = 0;
            nombre = "";
        }


    }
}

