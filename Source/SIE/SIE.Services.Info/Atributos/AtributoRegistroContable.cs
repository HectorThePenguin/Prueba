using System;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Atributos
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class AtributoRegistroContable : Attribute
    {
        /// <summary>
        /// Tipo de poliza que se generara
        /// </summary>
        public TipoPoliza TipoPoliza { get; set; }
        /// <summary>
        /// Orden en que se generaran las columnas
        /// </summary>
        public int Orden { get; set; }
        /// <summary>
        /// Indica la alineacion del campo
        /// </summary>
        public string Alineacion { get; set; }
    }
}
