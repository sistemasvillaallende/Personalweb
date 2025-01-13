using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.CIDI
{
    public class Domicilio
    {
        public String IdPais { get; set; }
        public String Pais { get; set; }
        public String IdProvincia { get; set; }
        public String Provincia { get; set; }
        public int IdDepartamento { get; set; }
        public String Departamento { get; set; }
        public int IdLocalidad { get; set; }
        public String Localidad { get; set; }
        public int IdBarrio { get; set; }
        public String Barrio { get; set; }
        public String Calle { get; set; }
        public String Altura { get; set; }
        public String CodigoPostal { get; set; }
        public String Piso { get; set; }
        public String Depto { get; set; }
        public String Torre { get; set; }
        public String Manzana { get; set; }
        public String Lote { get; set; }
    }

    public class DomicilioNTB
    {
        public String Calle { get; set; }
        public String Numero { get; set; }
        public String Piso { get; set; }
        public String Depto { get; set; }
        public Departamento Departamento { get; set; }
        public List<Departamento> DepartamentosList { get; set; }
        public Respuesta Respuesta { get; set; }

        public DomicilioNTB()
        {
            Departamento = new Departamento();
            DepartamentosList = new List<Departamento>();
            Respuesta = new Respuesta();
        }
    }

    public class Departamento
    {
        public int IdDepartamento { get; set; }
        public String NombreDepartamento { get; set; }
        public Localidad Localidad { get; set; }
        public List<Localidad> LocalidadesList { get; set; }

        public Departamento()
        {
            Localidad = new Localidad();
            LocalidadesList = new List<Localidad>();
        }
    }

    public class Localidad
    {
        public int IdLocalidad { get; set; }
        public String NombreLocalidad { get; set; }
        public Barrio Barrio { get; set; }
        public List<Barrio> BarriosList { get; set; }

        public Localidad()
        {
            Barrio = new Barrio();
            BarriosList = new List<Barrio>();
        }
    }

    public class Barrio
    {
        public int IdBarrio { get; set; }
        public String NombreBarrio { get; set; }
    }

    public class DomicilioReNaPer
    {
        public String Pais { get; set; }
        public String Provincia { get; set; }
        public String Departamento { get; set; }
        public String Localidad { get; set; }
        public String Barrio { get; set; }
        public String Calle { get; set; }
        public String Altura { get; set; }
        public String CodigoPostal { get; set; }
        public String Piso { get; set; }
        public String Depto { get; set; }
        public String Torre { get; set; }
        public String Manzana { get; set; }
        public String Lote { get; set; }
    }
}