using System;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Atributos
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class AtributoDetallePoliza : Attribute
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
        /// <summary>
        /// Numero de Columnas que se desplazara
        /// </summary>
        public int Desplazamiento { get; set; }
        /// <summary>
        /// Indica si se aplicara el desplazamiento
        /// </summary>
        public bool AplicarDesplazamiento { get; set; }
    }
}
