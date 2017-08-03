using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class ComisionInfo
    {
        /// <summary>
        /// ID Comision
        /// </summary>
        public int ProveedorComisionID { get; set; }

        /// <summary>
        /// ID Proveedor
        /// </summary>
        public int ProveedorID { get; set; }

        /// <summary>
        /// ID del Tipo de Tarifa
        /// </summary>
        public int TipoComisionID { get; set; }

        /// <summary>
        /// Tarifa
        /// </summary>
        public decimal Tarifa { get; set; }

        /// <summary>
        /// Descripcion del tipo de tarifa
        /// </summary>
        public string DescripcionComision { get; set; }

        /// <summary>
        /// 1 : Elemento Nuevo, 2 : Actualizacion, 3 : Borrado
        /// </summary>
        public int Accion { get; set; }

        /// <summary>
        /// Usuario que realiza el cambio
        /// </summary>
        public int Usuario { get; set; }
    }
}
