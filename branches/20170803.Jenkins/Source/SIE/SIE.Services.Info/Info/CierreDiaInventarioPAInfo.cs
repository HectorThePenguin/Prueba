using System.Collections.Generic;
namespace SIE.Services.Info.Info
{
    public class CierreDiaInventarioPAInfo : BitacoraInfo
    {
        /// <summary>
        /// ID del Producto
        /// </summary>
        public int ProductoID { get; set; }

        /// <summary>
        ///  Descripción del Producto
        /// </summary>
        public string Producto { get; set; }

        /// <summary>
        /// Unidad de Medida del Producto
        /// </summary>
        public string UnidadMedicion { get; set; }

        /// <summary>
        /// Folio del Movimiento
        /// </summary>
        public int FolioMovimiento { get; set; }

        /// <summary>
        /// Folio del Movimiento
        /// </summary>
        public int AlmacenID { get; set; }

        /// <summary>
        /// Unidad de Medida del Producto
        /// </summary>
        public string Observaciones { get; set; }

        /// <summary>
        /// Lista con los detalles de los Inventario de los Productos
        /// </summary>
        public List<CierreDiaInventarioPADetalleInfo> ListaCierreDiaInventarioPADetalle { get; set; }

        /// <summary>
        /// Indica si el folio se va a cancelar en la funcionalidad Autorizar Cierre Dia Inventario PA
        /// </summary>
        public bool EsCancelacion { get; set; }

        /// <summary>
        /// SubFamilia del producto
        /// </summary>
        public int SubFamiliaID { get; set; }

        /// <summary>
        /// Identificador de la Organizacion
        /// </summary>
        public int OrganizacionID { get; set; }
    }
}
