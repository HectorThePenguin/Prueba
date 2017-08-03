using System;

namespace SIE.Services.Info.Info
{
    public class ProveedorAlmacenInfo : BitacoraInfo
    {
        /// <summary>
        /// Identificador del Proveedor Almacen
        /// </summary>
        public int ProveedorAlmacenId { get; set; }

        /// <summary>
        /// Identificador del Proveedor
        /// </summary>
        public int ProveedorId { get; set; }

        /// <summary>
        /// Identificador del Almacen
        /// </summary>
        public int AlmacenId { get; set; }

        /// <summary> 
        ///	ProveedorID  
        /// </summary> 
        public ProveedorInfo Proveedor { get; set; }

        /// <summary> 
        ///	AlmacenID  
        /// </summary> 
        public AlmacenInfo Almacen { get; set; }

        /// <summary>
        /// Fecha de creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
    }
}
