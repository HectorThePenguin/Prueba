using System;
using System.ComponentModel;
using System.Globalization;
using SIE.Services.Info.Atributos;

namespace SIE.Services.Info.Info
{
    public class AlmacenInventarioLoteInfo : INotifyPropertyChanged
    {
        private string descripcionLote;
        private string _cantidadCadena;
        private decimal _cantidad;
        private int _lote;
        private decimal precioPromedio;

        /// <summary>
        /// Identificador del almacen inventario lote.
        /// </summary>
        public int AlmacenInventarioLoteId { set; get; }
        /// <summary>
        /// Identificador del almacen inventario
        /// </summary>
        public AlmacenInventarioInfo AlmacenInventario { set; get; }

        /// <summary>
        /// Lote asignado al almacen inventario lote
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadLote", EncabezadoGrid = "Lote", MetodoInvocacion = "ObtenerPorOrganizacionTipoAlmacenProductoFamiliaPaginado", PopUp = true, EstaEnContenedor = true, NombreContenedor = "Lote")]
        [AtributoAyuda(Nombre = "PropiedadLote", EncabezadoGrid = "Lote", MetodoInvocacion = "ObtenerAlmacenInventarioLotePorFolio", PopUp = false, EstaEnContenedor = true, NombreContenedor = "Lote")]
        [AtributoInicializaPropiedad]
        public int Lote { set { _lote = value; NotifyPropertyChanged("Lote"); } get { return _lote; } }

        /// <summary>
        /// Cantidad que tiene el almacen
        /// </summary>
        public decimal Cantidad {
            set
            {
                _cantidadCadena = value.ToString(CultureInfo.InvariantCulture);
                _cantidad = value;
                NotifyPropertyChanged("Cantidad");
            }
            get { return _cantidad; }
        }

        /// <summary>
        /// Cantidad en cadena
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadCantidad",EncabezadoGrid = "Cantidad",
            MetodoInvocacion = "ObtenerPorOrganizacionTipoAlmacenProductoFamiliaPaginado", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadObtenerLoteCantidadMayorACero", EncabezadoGrid = "Cantidad", MetodoInvocacion = "ObtenerPorOrganizacionTipoAlmacenProductoFamiliaCantidadPaginado", PopUp = true, EstaEnContenedor = true, NombreContenedor = "Lote")]
        public string CantidadCadena
        {
            get { return _cantidadCadena; }
            set
            {
                if (!(string.IsNullOrEmpty(value)))
                {
                    int punto = value.IndexOf('.');
                    if (punto < 0)
                    {
                        punto = value.Length;
                    }
                    _cantidadCadena = String.Format(CultureInfo.InvariantCulture, "{0,0:N0}",
                                                    int.Parse(value.Substring(0, punto).Replace(",", ""),
                                                              NumberStyles.AllowDecimalPoint |
                                                              NumberStyles.AllowLeadingSign,
                                                              CultureInfo.InvariantCulture));
                }
            }
        }
        /// <summary>
        /// Precio promedio del inventario del almacen
        /// </summary>
        public decimal PrecioPromedio
        {
            set
            {
                precioPromedio = value;
                NotifyPropertyChanged("PrecioPromedio");
            }
            get { return precioPromedio; }
        }
        /// <summary>
        /// Piezas totales que se encuentran en el lote
        /// </summary>
        public int Piezas { set; get; }
        /// <summary>
        /// Importe al que haciende el lote
        /// </summary>
        public decimal Importe { set; get; }
        /// <summary>
        /// Fecha en que se creo el Lote
        /// </summary>
        public DateTime FechaInicio { set; get; }
        /// <summary>
        /// Fecha en que se cerro el lote
        /// </summary>
        public DateTime FechaFin { set; get; }
        /// <summary>
        /// Estatus del lote.
        /// </summary>
        public Enums.EstatusEnum Activo { set; get; }
        /// <summary>
        /// Id del usuario que crea
        /// </summary>
        public int UsuarioCreacionId { set; get; }
        /// <summary>
        /// Id del usuario que modifica
        /// </summary>
        public int UsuarioModificacionId { set; get; }

        /// <summary>
        /// Cantidad de producto que se programo para ser entregado.
        /// </summary>
        public decimal CantidadProgramada { get; set; }

        /// <summary>
        /// Descripcion del Lote
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public string DescripcionLote
        {
            get { return string.IsNullOrEmpty(descripcionLote) ? string.Empty : descripcionLote.Trim(); }
            set
            {
                string valor = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
                if (valor.Trim() != descripcionLote)
                {
                    descripcionLote = value;
                    NotifyPropertyChanged("DescripcionLote");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Organizacion del almaceninventariolote
        /// </summary>
        public int OrganizacionId { get; set; }

        /// <summary>
        /// Producto del almaceninventariolote
        /// </summary>
        public int ProductoId { get; set; }

        /// <summary>
        /// Tipo de AlmacenID
        /// </summary>
        public int TipoAlmacenId { get; set; }

        public string LoteCombo
        {
            get
            {
                if (_lote == 0)
                {
                    return "Seleccione";
                }
                return _lote.ToString(CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Fecha de Producción de la Formula
        /// </summary>
        public DateTime FechaProduccionFormula { get; set; }

        /// <summary>
        /// Folio de la formula que se produjo, (flujo de polizas)
        /// </summary>
        public int FolioFormula { get; set; }

        /// <summary>
        /// Identifica si el moviemiento es entrada para descontar o sumar al Inventario la cantidad
        /// </summary>
        public bool EsEntrada { get; set; }
    }
}
