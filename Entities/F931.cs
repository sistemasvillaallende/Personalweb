using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class F931
    {
        public string cuil { get; set; }
        public string apeynom { get; set; }
        public string conyuge { get; set; }
        public string cant_hijos { get; set; }
        public string cod_situacion { get; set; }
        public string cod_condicion { get; set; }
        public string cod_actividad { get; set; }
        public string cod_zona { get; set; }
        public string por_aporte_adic_ss { get; set; }
        public string cod_modalidad_contratacion { get; set; }
        //
        public string cod_obra_social { get; set; }
        public string cant_adherente { get; set; }
        public string remuneracion_total{ get; set; }
        //
        public string remuneracion_imponible_1{ get; set; }
        public string asig_familiares_pagadas { get; set; }
        public string importe_aporte_vol { get; set; }
        public string importe_adicional_os { get; set; }
        public string importe_excendente_aporte_ss { get; set; }
        public string importe_excendente_aporte_os { get; set; }
        public string provincia_localidad { get; set; }
        public string remuneracion_imponible_2 { get; set; }
        public string remuneracion_imponible_3 { get; set; }
        public string remuneracion_imponible_4 { get; set; }
        public string cod_siniestrado { get; set; }
        public string marca_reduccion { get; set; }
        public string capital_recomposicion_lrt { get; set; }
        public string tipo_empresa { get; set; }
        public string aporte_adicional_os { get; set; }
        public string regimen { get; set; }
        public string situacion_revista_1 { get; set; }
        //
        //
        public string dia_inicio_situacion_revista_1 { get; set; }
        public string situacion_revista_2 { get; set; }
        public string dia_inicio_situacion_revista_2 { get; set; }
        public string situacion_revista_3 { get; set; }
        public string dia_inicio_situacion_revista_3 { get; set; }
        //15/04/2013
        // Se agregaron 3 campos nuevos
        public string sueldo_mas_adicionales { get; set; }
        public string sac { get; set; }
        public string horas_extras { get; set; }
        public string zona_desfavorable { get; set; }
        public string vacaciones { get; set; }
        public string cantidad_dias_trabajos { get; set; }
        public string remuneracion_imponible_5 { get; set; }
        public string trabajador_convencionado { get; set; }
        public string remuneracion_imponible_6 { get; set; }
        public string tipo_operacion { get; set; }
        public string adiciones { get; set; }
        public string premios { get; set; }
        public string rem_dto_78805_rem_8 { get; set; }
        public string remuneracion_imponible_7 { get; set; }
        public string cantidad_horas_extras { get; set; }
        public string conceptos_no_remunerativos { get; set; }
        public string maternidad { get; set; }
        public string rectificacion_remuneracion { get; set; }
        public string remuneracion_imponible_9 { get; set; }
        public string contribucion_tarea_diferencial_porc { get; set; }
        public string horas_trabajadas { get; set; }
        public string seguro_colectivo_vida_obligatorio { get; set; }
        public string importe_detraccion_ley27430 { get; set; }
        public string incremento_salarial { get; set; }
        public string remuneracion_imponible_11 { get; set; }

        public F931()
        {

            cuil = "00000000000";
            apeynom = "".ToString().PadLeft(30).Substring(0, 30);
            conyuge= "0";//0=no, 1=si
            cant_hijos = "00";
            cod_situacion = "00";
            cod_condicion = "00";
            cod_actividad = "00";
            cod_zona = "00";
            por_aporte_adic_ss = "00000";
            cod_modalidad_contratacion = "000";
            cod_obra_social = "000000";
            cant_adherente = "00";
            remuneracion_total = "000000000000";
            remuneracion_imponible_1= "000000000000";
            asig_familiares_pagadas = "000000000";
            importe_aporte_vol = "000000000";
            importe_adicional_os = "000000000";
            importe_excendente_aporte_ss= "000000000";
            importe_excendente_aporte_os = "000000000";
            provincia_localidad = "".ToString().PadLeft(50).Substring(0, 50);
            //
            remuneracion_imponible_2 = "000000000000";
            remuneracion_imponible_3 = "000000000000";
            remuneracion_imponible_4 = "000000000000";
            cod_siniestrado = "00";
            marca_reduccion = "0";
            capital_recomposicion_lrt = "000000000";
            tipo_empresa = "0";
            aporte_adicional_os = "000000000";
            regimen = "0";
            situacion_revista_1 = "00";
            dia_inicio_situacion_revista_1 = "00";
            situacion_revista_2 = "00";
            dia_inicio_situacion_revista_2 = "00";
            situacion_revista_3 = "00";
            dia_inicio_situacion_revista_3 = "00";
            sueldo_mas_adicionales = "000000000000";
            sac = "000000000000";
            horas_extras = "000000000000";
            zona_desfavorable = "000000000000";
            vacaciones = "000000000000";
            cantidad_dias_trabajos = "000000000";
            remuneracion_imponible_5 = "000000000000";
            trabajador_convencionado = "0";
            remuneracion_imponible_6 = "000000000000";
            tipo_operacion = "0";
            adiciones = "000000000000";
            premios = "000000000000";
            rem_dto_78805_rem_8 = "000000000000";
            remuneracion_imponible_7 = "000000000000";
            cantidad_horas_extras = "000";
            conceptos_no_remunerativos = "000000000000";
            maternidad = "000000000000";
            rectificacion_remuneracion = "000000000";
            remuneracion_imponible_9 = "000000000000";
            contribucion_tarea_diferencial_porc = "000000000";
            horas_trabajadas = "000";
            seguro_colectivo_vida_obligatorio = "0";
            importe_detraccion_ley27430 = "000000000000";
            incremento_salarial = "000000000000";
            remuneracion_imponible_11 = "000000000000";
        }


    }
}
