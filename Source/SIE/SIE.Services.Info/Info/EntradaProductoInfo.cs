using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using SIE.Services.Info.Atributos;

namespace SIE.Services.Info.Info
{
    public class EntradaProductoInfo : INotifyPropertyChanged 
    {
        public EntradaProductoInfo()
        {
            FechaEmbarque = new DateTime(1900, 01, 01);
        }
        private int entradaProductoId;

        /// <summary>
        /// Es el identificador de la entrada de producto
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadEntradaProductoId", EncabezadoGrid = "Id",
            MetodoInvocacion = "ObtenerFoliosPorPaginaParaEntradaMateriaPrima", PopUp = true)]
        [AtributoInicializaPropiedad]
        public int EntradaProductoId
        {
            get { return entradaProductoId; }
            set
            {
                entradaProductoId = value;
                NotifyPropertyChanged("EntradaProductoId");
            }
        }

        /// <summary>
        /// Es el contrato al que pertenece la entrada de producto
        /// </summary>
        public ContratoInfo Contrato { get; set; }

        /// <summary>
        /// La organizacion de la entrada de producto
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary>
        /// El producto que tiene la entrada
        /// </summary>
        public ProductoInfo Producto { get; set; }

        /// <summary>
        /// El registro de vigilancia
        /// </summary>
        public RegistroVigilanciaInfo RegistroVigilancia { get; set; }

        /// <summary>
        /// Folio de la entrada
        /// </summary>
        public int Folio { get; set; }


        [AtributoAyuda(Nombre = "PropiedadFolio", EncabezadoGrid = "Folio", MetodoInvocacion = "ObtenerPorFolioPorEntradaMateriaPrima", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadFolio", EncabezadoGrid = "Folio", MetodoInvocacion = "ObtenerFoliosPorPaginaParaEntradaMateriaPrima", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadFolio", EncabezadoGrid = "Folio", MetodoInvocacion = "ObtenerPorFolioPorEntradaMateriaPrima", PopUp = false, EstaEnContenedor = true, NombreContenedor = "Producto")]
        [AtributoAyuda(Nombre = "PropiedadFolioEstatus", EncabezadoGrid = "Folio", MetodoInvocacion = "ObtenerPorFolioPorEntradaMateriaPrimaEstatus", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadFolioCancelacionEntradaCompra", EncabezadoGrid = "Folio", MetodoInvocacion = "ObtenerPorFolioPorEntradaCancelacion", PopUp = false)]
        public String FolioBusqueda { get { return Folio.ToString(CultureInfo.InvariantCulture); }
            set
            {
                int valor = 0;
                Folio = string.IsNullOrEmpty(value) ? 0 : int.TryParse(value, out valor) ? valor : -1;

                if (!int.TryParse(value, out valor))
                {
                    Folio = 0;
                    if (Producto != null)
                    {
                        Producto.ProductoDescripcion = value;
                    }
                    else
                    {
                        Producto = new ProductoInfo() {ProductoDescripcion = value};
                    }
                }
            }
        }

        /// <summary>
        /// Fecha de la entrada
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Fecha Destara
        /// </summary>
        public DateTime FechaDestara { get; set; }

        /// <summary>
        /// Observaciones
        /// </summary>
        public string Observaciones { get; set; }

        /// <summary>
        /// Operador Analista
        /// </summary>
        public OperadorInfo OperadorAnalista { get; set; }

        /// <summary>
        /// Peso en el Origen
        /// </summary>
        public int PesoOrigen { get; set; }

        /// <summary>
        /// Peso en el Origen Bonificacion
        /// </summary>
        public int PesoBonificacion { get; set; }

        /// <summary>
        /// Peso bruto
        /// </summary>
        public int PesoBruto { get; set; }

        /// <summary>
        /// Peso tara
        /// </summary>
        public int PesoTara { get; set; }

        /// <summary>
        /// Peso Neto
        /// </summary>
        public int PesoNetoAnalizado { get; set; }

        /// <summary>
        /// Piezas
        /// </summary>
        public int Piezas { get; set; }

        /// <summary>
        /// Tipo de contrato
        /// </summary>
        public TipoContratoInfo TipoContrato { get; set; }

        /// <summary>
        /// Estatus de la entrada
        /// </summary>
        public EstatusInfo Estatus { get; set; }

        /// <summary>
        /// Justificacion de la entrada
        /// </summary>
        public string Justificacion { get; set; }

        /// <summary>
        /// Operador de la bascula
        /// </summary>
        public OperadorInfo OperadorBascula { get; set; }

        /// <summary>
        /// Operador del almacen
        /// </summary>
        public OperadorInfo OperadorAlmacen { get; set; }

        /// <summary>
        /// Operador que autoriza
        /// </summary>
        public OperadorInfo OperadorAutoriza { get; set; }

        /// <summary>
        /// Fecha de inicio de descarga
        /// </summary>
        public DateTime FechaInicioDescarga { get; set; }

        /// <summary>
        /// Fecha fin de descarga
        /// </summary>
        public DateTime FechaFinDescarga { get; set; }

        /// <summary>
        /// Almacen inventario lote
        /// </summary>
        public AlmacenInventarioLoteInfo AlmacenInventarioLote { get; set; }

        /// <summary>
        /// Almacen movimiento
        /// </summary>
        public AlmacenMovimientoInfo AlmacenMovimiento { get; set; }

        /// <summary>
        /// Movimiento de almacen generado en la salida
        /// </summary>
        public AlmacenMovimientoInfo AlmacenMovimientoSalida { get; set; }

        /// <summary>
        /// Estatus
        /// </summary>
        public Enums.EstatusEnum Activo { get; set; }

        /// <summary>
        /// Fecha creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Usuario creacion
        /// </summary>
        public int UsuarioCreacionID { get; set; }

        /// <summary>
        /// Fecha modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        /// <summary>
        /// Usuario modificacion
        /// </summary>
        public int UsuarioModificacionID { get; set; }

        /// <summary>
        /// Obtiene las muestras que contiene el Indicador
        /// </summary>
        public List<EntradaProductoDetalleInfo> ProductoDetalle { get; set; }

        /// <summary>
        /// Obtiene la info si el producto es premezcla 
        /// </summary>
        public PremezclaInfo PremezclaInfo { get; set; }

        /// <summary>
        /// Indica el descuento aplicado
        /// </summary>
        public decimal? PesoDescuento { get; set; }

        public DatosOrigen datosOrigen { get; set; }

        /// <summary>
        /// Obtiene la descripcion del producto que se encuentra en la entrada de producto
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadDescripcionProducto", EncabezadoGrid = "Producto", MetodoInvocacion = "ObtenerFoliosPorPaginaParaEntradaMateriaPrima", PopUp = true, EstaEnContenedor = true, NombreContenedor = "Producto")]
        [AtributoAyuda(Nombre = "PropiedadDescripcionProductoEstatus", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerFoliosPorPaginaParaEntradaMateriaPrimaEstatus", PopUp = true, EstaEnContenedor = true, NombreContenedor = "Producto")]
        [AtributoAyuda(Nombre = "PropiedadDescripcionProductoCancelacionEntradaCompra", EncabezadoGrid = "Producto", MetodoInvocacion = "ObtenerFoliosPorPaginaParaCancelacionEntradaCompra", PopUp = true, EstaEnContenedor = true, NombreContenedor = "Producto")]
        [AtributoAyuda(Nombre = "PropiedadDescripcionProductoCancelacionEntradaTraspaso", EncabezadoGrid = "Producto", MetodoInvocacion = "ObtenerFoliosPorPaginaParaCancelacionEntradaTraspaso", PopUp = true, EstaEnContenedor = true, NombreContenedor = "Producto")]
        public string DescripcionProducto { get; set; }


        /// <summary>
        /// Gets or sets the fecha embarque.
        /// </summary>
        public DateTime FechaEmbarque { get; set; }

        /// <summary>
        /// Gets or sets the folio origen.
        /// </summary>
        public int FolioOrigen { get; set; }

        /// <summary>
        /// Gets or sets the es origen.
        /// </summary>
        public int EsOrigen { get; set; }

        /// <summary>
        /// Nota de venta
        /// </summary>
        public string NotaDeVenta { get; set; }

        #region Miembros de INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
