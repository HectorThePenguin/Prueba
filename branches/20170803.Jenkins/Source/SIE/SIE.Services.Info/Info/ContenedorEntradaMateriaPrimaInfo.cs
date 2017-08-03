
using System.Collections.ObjectModel;

namespace SIE.Services.Info.Info
{
    public class ContenedorEntradaMateriaPrimaInfo
    {
        public ObservableCollection<CostoEntradaMateriaPrimaInfo> ListaCostoEntradaMateriaPrima { get; set; }

        public EntradaProductoInfo EntradaProducto { get; set; }

        public ProductoInfo Producto { get; set; }

        public ContratoInfo Contrato { get; set; }

        public string TipoEntrada { get; set; }

        public string Observaciones { get; set; }

        public int UsuarioId { get; set; }

        public decimal CostoProductosPremezcla { get; set; }

        public ContenedorEntradaMateriaPrimaInfo()
        {
            ListaCostoEntradaMateriaPrima = new ObservableCollection<CostoEntradaMateriaPrimaInfo>();
        }

        public bool aplicaRestriccionDescuento { get; set; }

        public decimal PorcentajeRestriccionDescuento { get; set; }
    }
}
