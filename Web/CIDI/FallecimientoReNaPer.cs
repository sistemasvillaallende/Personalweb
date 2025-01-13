using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.CIDI
{
    public class FallecimientoReNaPer
    {
        /// <summary>
        ///Codigo   | Mensa                                                                                                                                 | Origen
        ///04       | Formulario F24 Digital, es la fuente más abundante de datos, es el sistema Nacional                                                   | Fallecido F24
        ///04       | Aviso de Fallecimiento, solo cuanta con el respaldo de un formulario escaneado escrito a mano en la delegación del Registro Civil     | Fallecido AF
        ///04       | Origen Histórico no respaldado por documentación                                                                                      | Fallecido
        ///06       | Con imagen de AF ilegible                                                                                                             | Fallecido con error AF
        ///05       | Con imagen F24 ilegible                                                                                                               | Fallecido con error F24
        ///08       | Origen Histórico no respaldado por documentación                                                                                      | Presunto Fallecido
        ///03       | Se presume con vida.                                                                                                                  | Sin Aviso Fallecimiento
        /// </summary>
        public int Codigo { get; set; }
        public String Mensaje { get; set; }
        public String Origen { get; set; }
        public String Fecha { get; set; }
    }
}