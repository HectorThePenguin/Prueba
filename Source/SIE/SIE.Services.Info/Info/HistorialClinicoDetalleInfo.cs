using System;
using System.Collections.Generic;
namespace SIE.Services.Info.Info
{
    public class HistorialClinicoDetalleInfo
    {

        /// <summary>
        /// Problema
        /// </summary>
        public string Problema { get; set; }
        /// <summary>
        /// Tratamiento
        /// </summary>
        public string Tratamiento { get; set; }
        /// <summary>
        /// Costo
        /// </summary>
        public decimal Costo { get; set; }
        /// <summary>
        /// TratamientoID
        /// </summary>
        public int TratamientoId { get; set; }
        /// <summary>
        /// Descripcion de producto
        /// </summary>
        public string DescripcionProducto { get; set; }

    }
}
