using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
  public class Sijcor
  {
    public string cuil { get; set; }
    public string apeynom { get; set; }
    public string cargo { get; set; }
    public string categoria { get; set; }
    public string adherente_vol { get; set; }
    public string adherente_obl { get; set; }
    public string cod_situacion { get; set; }
    public string cod_condicion { get; set; }
    public string cod_actividad { get; set; }
    public string cod_modalidad_contratacion { get; set; }
    public string cod_siniestro { get; set; }
    public string cod_departamento { get; set; }
    public string cod_delegacion { get; set; }
    public string cod_obra_social { get; set; }
    public string cod_situacion_1er_tramo { get; set; }
    public string cant_dias_1er_tramo { get; set; }
    public string cod_situacion_2do_tramo { get; set; }
    public string cant_dias_2do_tramo { get; set; }
    public string cod_situacion_3er_tramo { get; set; }
    public string cant_dias_3er_tramo { get; set; }
    public string cant_dias_trabajados { get; set; }
    public string sueldo { get; set; }
    public string importe_hs_extra { get; set; }
    public string zona_desfavorable { get; set; }
    public string conceptos_no_remunerativos { get; set; }
    public string retroactividades { get; set; }
    public string aguinaldo { get; set; }
    public string remuneracion_2 { get; set; }
    public string tipo_adicional_seg_vida { get; set; }
    //15/04/2013
    // Se agregaron 3 campos nuevos
    public string secuencia_cuil { get; set; }
    public string diferencia_x_jerarquia { get; set; }
    public string importe_adherente_voluntario { get; set; }

    public Sijcor()
    {

      cuil = "00000000000";
      apeynom = "";// Space(30)
      cargo = "";// Space(30)
      categoria = "";// Space(30)
      adherente_vol = "00";
      adherente_obl = "01";
      cod_situacion = "0001";
      cod_condicion = "0001";
      cod_actividad = "0001";
      cod_modalidad_contratacion = "0000";
      cod_siniestro = "0001";
      cod_departamento = "0016";
      cod_delegacion = "0000";
      cod_obra_social = "01";
      cod_situacion_1er_tramo = "0001";
      cant_dias_1er_tramo = "01";
      cod_situacion_2do_tramo = "0000";
      cant_dias_2do_tramo = "00";
      cod_situacion_3er_tramo = "0000";
      cant_dias_3er_tramo = "00";
      cant_dias_trabajados = "30";
      sueldo = "000000,00";
      importe_hs_extra = "000000,00";
      conceptos_no_remunerativos = "000000,00";
      zona_desfavorable = "000000,00";
      retroactividades = "000000,00";
      aguinaldo = "000000,00";
      remuneracion_2 = "000000,00";
      tipo_adicional_seg_vida = "1";
      secuencia_cuil = "01";
      diferencia_x_jerarquia = "000000,00";
      importe_adherente_voluntario = "000000,00";
    }


  }
}
