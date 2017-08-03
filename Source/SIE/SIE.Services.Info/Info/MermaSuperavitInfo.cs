using System;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class MermaSuperavitInfo : BitacoraInfo
    {
        /// <summary>
        /// Id del registro
        /// </summary>
        public int MermaSuperavitId { get; set; }
        /// <summary>
        /// Almacen correspondiente al registro
        /// </summary>
        public AlmacenInfo Almacen { get; set; }
        /// <summary>
        /// Producto correspondiente al registro
        /// </summary>
        public ProductoInfo Producto { get; set; }
        /// <summary>
        /// Merma del registro
        /// </summary>
        public decimal Merma { get; set; }
        /// <summary>
        /// Superavit del registro
        /// </summary>
        public decimal Superavit { get; set; }
    }
}
