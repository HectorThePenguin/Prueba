using System;
using System.ComponentModel;
using SIE.Services.Info.Atributos;
using SIE.Services.Info.Enums;
using System.Collections.Generic;

namespace SIE.Services.Info.Info
{
    public class SalidaProductoInfo : INotifyPropertyChanged
    {
        private int salidaProductoId;
        private int folioSalida;
        private string descripcion;

        /// <summary>
        /// Identificador de la salida del producto
        /// </summary>
        [AtributoInicializaPropiedad]
        public int SalidaProductoId
        {
            get { return salidaProductoId; }
            set
            {
                salidaProductoId = value;
                NotifyPropertyChanged("SalidaProductoId");
            }
        }

        /// <summary>
        /// Organizacion de la salida
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary>
        /// Organizacion del destino
        /// </summary>
        public OrganizacionInfo OrganizacionDestino { get; set; }

        /// <summary>
        /// Tipo de movimiento de la salida
        /// </summary>
        public TipoMovimientoInfo TipoMovimiento { get; set; }
       
        /// <summary>
        /// 
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadDescripcionSalidaProducto", EncabezadoGrid = "Salida", MetodoInvocacion = "ObtenerPorFolioSalida", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionSalidaProducto", EncabezadoGrid = "Salida", MetodoInvocacion = "ObtenerFoliosPorPaginaParaSalidaProducto", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionCancelacion", EncabezadoGrid = "Salida", MetodoInvocacion = "ObtenerFoliosPorPaginaParaCancelacion", PopUp = true)]
        public String DescripcionMovimiento {
            get
            {
                if (TipoMovimiento != null)
                {
                    return TipoMovimiento.Descripcion;
                }
                return string.Empty;
            }
            set
            {
                int valorNumero = 0;
                if (int.TryParse(value, out valorNumero))
                {
                    FolioSalida = valorNumero;
                }
                else
                {
                    FolioSalida = 0;
                    TipoMovimiento = new TipoMovimientoInfo
                    {
                        Descripcion = value
                    };
                }
            } 
        }

        /// <summary>
        /// Folio de la salida
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadClaveSalidaProducto", EncabezadoGrid = "Folio Salida",
            MetodoInvocacion = "ObtenerFoliosPorPaginaParaSalidaProducto", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadClaveSalidaProducto", EncabezadoGrid = "Folio Salida",
            MetodoInvocacion = "ObtenerPorFolioSalida", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadClaveCancelacion", EncabezadoGrid = "Folio",
            MetodoInvocacion = "ObtenerPorFolioSalida", PopUp = false)]
        public int FolioSalida
        {
            get { return folioSalida; }
            set
            {
                folioSalida = value;
                NotifyPropertyChanged("FolioSalida");
            }
        }

        /// <summary>
        /// Almacen de la salida
        /// </summary>
        public AlmacenInfo Almacen { get; set; }

        /// <summary>
        /// Almacen inventario lote de la salida
        /// </summary>
        public AlmacenInventarioLoteInfo AlmacenInventarioLote { get; set; }

        /// <summary>
        /// Cliente de la salida
        /// </summary>
        public ClienteInfo Cliente { get; set; }

        /// <summary>
        /// Cuenta SAP de la salida
        /// </summary>
        public CuentaSAPInfo CuentaSAP { get; set; }

        /// <summary>
        /// Observaciones de la salida
        /// </summary>
        public string Observaciones { get; set; }

        /// <summary>
        /// Observaciones de la salida
        /// </summary>
        public string Justificacion { get; set; }

        /// <summary>
        /// Costo de venta de la salida
        /// </summary>
        public decimal Precio { get; set; }
        /// <summary>
        /// Importe
        /// </summary>
        public decimal Importe { get; set; }
        /// <summary>
        /// Almacen movimiento
        /// </summary>
        public AlmacenMovimientoInfo AlmacenMovimiento { get; set; }

        /// <summary>
        /// Peso tara
        /// </summary>
        public int PesoTara { get; set; }

        /// <summary>
        /// Peso bruto
        /// </summary>
        public int PesoBruto { get; set; }

        /// <summary>
        /// Piezas de la salida
        /// </summary>
        public int Piezas { get; set; }

        /// <summary>
        /// Fecha Salida
        /// </summary>
        public DateTime FechaSalida { get; set; }
        /// <summary>
        /// Chofer que trasportara
        /// </summary>
        public ChoferInfo Chofer { get; set; }

        /// <summary>
        /// Camion que trasportara
        /// </summary>
        public CamionInfo Camion { get; set; }
        /// <summary>
        /// Producto
        /// </summary>
        public ProductoInfo Producto { get; set; }

        /// <summary>
        /// Estado del registro
        /// </summary>
        public EstatusEnum Activo { get; set; }

        /// <summary>
        /// Fecha en que se creo el registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Usuario que registro
        /// </summary>
        public int UsuarioCreacionId { get; set; }

        /// <summary>
        /// Fecha en que se modifico
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        /// <summary>
        /// Usuario que modifica
        /// </summary>
        public int UsuarioModificacionId { get; set; }

        /// <summary>
        /// Descripcion del Movimiento
        /// </summary>
        public string Descripcion
        {
            get { return descripcion; }
            set
            {
                descripcion = value;
                NotifyPropertyChanged("Descripcion");
            }
        }

        public List<TipoMovimientoInfo> listaTipoMovimiento { get; set; }

        /// <summary>
        /// Usuario que registro
        /// </summary>
        public int IDSolicitud { get; set; }

        /// <summary>
        /// Fecha en que se modifico
        /// </summary>
        public int EstatusSolicitud { get; set; }

        /// <summary>
        /// Usuario que modifica
        /// </summary>
        public double PrecioSolicitud { get; set; }

        /// <summary>
        /// Indica si la salida genera factura 
        /// </summary>
        public bool GeneraFactura { get; set; }

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
