using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Atributos
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class AtributoImpresionSupervisionDetectores : Attribute
    {
        /// <summary>
        /// Id de la pregunta
        /// </summary>
        public int PreguntaID { get; set; }
    }
}
