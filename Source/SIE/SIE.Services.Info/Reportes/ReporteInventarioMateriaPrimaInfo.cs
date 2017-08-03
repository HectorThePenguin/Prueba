using System;

namespace SIE.Services.Info.Reportes
{
    public class ReporteInventarioMateriaPrimaInfo
    {
        public string Producto { get; set; }
        public string Almacen { get; set; }
        public string UnidadMedida { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public int Lote { get; set; }
        public string CodigoMovimiento { get; set; }
        public string TipoMovimiento { get; set; }
        public string Folio { get; set; }
        public int PesoOrigen { get; set; }
        public decimal CantidadEntrada { get; set; }
        public decimal CantidadSalida { get; set; }
        public decimal ExistenciaInicial { get; set; }
        public decimal ExistenciaFinalMesAnterior { get; set; }
        public int Piezas { get; set; }
        public decimal Precio { get; set; }
        public long AlmacenMovimientoDetalleId { get; set; }
        public long AlmacenMovimientoCostoId { get; set; }
        public int CostoId { get; set; }
        public string Costo { get; set; }
        public DateTime FechaCosto { get; set; }
        public decimal ImporteCosto { get; set; }
        public decimal CantidadCosto { get; set; }
        public decimal CostoInicialInventario { get; set; }
        public decimal Cargos { get; set; }
        public decimal Abonos { get; set; }
        public int PiezasEntrada { get; set; }
        public int PiezasSalida { get; set; }
        public int ExistenciaPiezasInicial { get; set; }
        public int PiezasFinalMesAnterior { get; set; }
        public decimal CostoFinalMesAnterior { get; set; }
        public long AlmacenMovimientoID { get; set; }
        public long TipoMovimientoID { get; set; }

        //Datos del informe
        public string Titulo { get; set; }
        public string Organizacion {get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string FechaReporte
        {
            get
            {
                return String.Format("Del {0} al {1}", FechaInicio.ToString("dd/MM/yyyy"),
                    FechaFin.ToString("dd/MM/yyyy"));
            }
        }

        public decimal ImporteSubProductos { get; set; }

        public string DescripcionSubProductos { get; set; }
    }
}
