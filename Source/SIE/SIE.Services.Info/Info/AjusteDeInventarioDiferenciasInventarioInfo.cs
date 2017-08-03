using System;

namespace SIE.Services.Info.Info
{
    public class AjusteDeInventarioDiferenciasInventarioInfo
    {
        public bool Seleccionado { get; set; }
        /// <summary>
        /// ProductoID
        /// </summary>
        public int ProductoID { get; set; }

        /// <summary>
        /// Descripcion
        /// </summary>
        public String Descripcion { get; set; }

        /// <summary>
        /// LoteAlmacenado
        /// </summary>
        public int LoteAlmacenado { get; set; }

        /// <summary>
        /// UnidadMedida
        /// </summary>
        public String UnidadMedida { get; set; }

        /// <summary>
        /// Cantidad
        /// </summary>
        public decimal Cantidad { get; set; }

        /// <summary>
        /// Precio
        /// </summary>
        public decimal Precio { get; set; }

        /// <summary>
        /// Importe
        /// </summary>
        public decimal Importe { get; set; }

        /// <summary>
        /// CantidadInventarioFisico
        /// </summary>
        public decimal CantidadInventarioFisico { get; set; }

        /// <summary>
        /// PrecioInventarioFisico
        /// </summary>
        public decimal PrecioInventarioFisico { get; set; }

        /// <summary>
        /// CantidadInventarioTeorico
        /// </summary>
        public decimal CantidadInventarioTeorico { get; set; }

        /// <summary>
        /// PrecioInventarioFisico
        /// </summary>
        public decimal PrecioInventarioTeorico { get; set; }

        /// <summary>
        /// AlmacenInventarioID
        /// </summary>
        public int AlmacenInventarioID { get; set; }

        /// <summary>
        /// AlmacenMovimientoDetalleID
        /// </summary>
        public long AlmacenMovimientoDetalleID { get; set; }
    }
}
