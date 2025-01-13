using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class LstEmpleados
    {
        public int legajo { get; set; }

        public string nombre { get; set; }

        public string fecha_ingreso { get; set; }

        public string fecha_nacimiento { get; set; }

        public int cod_categoria { get; set; }

        public string des_categoria { get; set; }

        public int cod_cargo { get; set; }

        public string tarea { get; set; }

        public string des_tipo_liq { get; set; }

        public string nom_banco { get; set; }

        public string nro_caja_ahorro { get; set; }

        public string nro_cbu { get; set; }

        public string nro_documento { get; set; }

        public string nro_cta_sb { get; set; }

        public string nro_cta_gastos { get; set; }

        public string secrectaria { get; set; }

        public string direccion { get; set; }

        public string oficina { get; set; }

        public int antiguedad_ant { get; set; }

        public Decimal sueldo_basico { get; set; }

        public short imprime_recibo { get; set; }

        public string programa { get; set; }

        public Decimal sueldo_bruto { get; set; }

        public int cod_clasif_per { get; set; }

        public string des_clasif_per { get; set; }

        public int id_revista { get; set; }

        public string situacion_revista { get; set; }

        public Decimal dias_trabajados { get; set; }

        public Decimal hs_trabajados { get; set; }

        public string celular { get; set; }

        public string telefonos { get; set; }

        public string email { get; set; }

        public string passTemp { get; set; }

        public string estadoEvaluacion { get; set; }

        public int idEstadoEvaluacion { get; set; }

        public int id_direccion { get; set; }

        public LstEmpleados()
        {
            this.legajo = 0;
            this.nombre = "";
            this.fecha_ingreso = "";
            this.fecha_nacimiento = "";
            this.cod_categoria = 0;
            this.des_categoria = "";
            this.cod_cargo = 0;
            this.tarea = "";
            this.des_tipo_liq = "";
            this.nom_banco = "";
            this.nro_caja_ahorro = "";
            this.nro_cbu = "";
            this.nro_documento = "";
            this.nro_cta_sb = "";
            this.nro_cta_gastos = "";
            this.secrectaria = "";
            this.direccion = "";
            this.oficina = "";
            this.antiguedad_ant = 0;
            this.sueldo_basico = 0M;
            this.imprime_recibo = (short)1;
            this.programa = "";
            this.sueldo_bruto = 0M;
            this.cod_clasif_per = 0;
            this.des_clasif_per = string.Empty;
            this.id_revista = 0;
            this.situacion_revista = string.Empty;
            this.dias_trabajados = 0M;
            this.hs_trabajados = 0M;
            this.celular = string.Empty;
            this.telefonos = string.Empty;
            this.email = string.Empty;
            this.passTemp = string.Empty;
            this.estadoEvaluacion = string.Empty;
            this.idEstadoEvaluacion = 0;
            this.id_direccion = 0;
        }
    }
}
