using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class Resultado_General_por_Competencia:DAL.DALBase
    {
        public string Pregunta { get; set; }
        public long[] Respuestas { get; set; }
        public string[] Etiquetas { get; set; }
        
        
            
        public Resultado_General_por_Competencia()
        {
            Pregunta = string.Empty;
            Respuestas = new long[4];
            Etiquetas = new string[4];
        }
    }
}