//--*********** Info *************
using System;

namespace SIE.Services.Info.Reportes
{
    public class ReporteInventarioPorLotesInfo
    {
        public int AlmacenId { get; set; }
        public string Codigoalmacen { get; set; }
        public int Lote { get; set; }
        public DateTime FechaInicio { get; set; }
        public decimal PrecioPromedio { get; set; }
        public decimal CostoPromedio { get; set; }
        public decimal CantidadInventario { get; set; }
        public int FamiliaId { get; set; }
        public string Familia { get; set; }
        public int TipoAlmancenId { get; set; }
        public string TipoAlmacen { get; set; }
        public int SubFamiliaId { get; set; }
        public string SubFamilia { get; set; }
        public int ProductoId { get; set; }
        public string Producto { get; set; }
        public decimal UnidadEntrada { get; set; }
        public decimal UnidadSalida { get; set; }
        public decimal TamanioLote { get; set; }

        /// <summary>
        /// Titulo
        /// </summary>
        public string Titulo { get; set; }
        /// <summary>
        /// Organizacion
        /// </summary>
        public string Organizacion { get; set; }
        /// <summary>
        /// Fecha
        /// </summary>
        public DateTime Fecha { get; set; }
    }
}