using System;
using System.Collections.Generic;
using System.ComponentModel;
using SIE.Services.Info.Atributos;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class ContratoInfo : INotifyPropertyChanged 
    {
        private int contratoId;
        private int folio;
        private string pesoNegociar;

        /// <summary>
        /// Identificador del contrato
        /// </summary>
        /// 
        [AtributoAyuda(Nombre = "PropiedadClaveCatalogoAyudaFolioContrato",
            EncabezadoGrid = "Clave",
            MetodoInvocacion = "ObtenerPorFolioContrato",
            PopUp = false,
            EstaEnContenedor = true,
            NombreContenedor = "Contrato")]
        [AtributoInicializaPropiedad]
        public int ContratoId
        {
            set
            {
                contratoId = value;
                NotifyPropertyChanged("ContratoId");
            }
            get { return contratoId; }
        }

        /// <summary>
        /// Identificador de la organizacion a la que pertenece el contrato
        /// </summary>
        public OrganizacionInfo Organizacion { set; get; }

        /// <summary>
        /// Folio
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadDescripcionCatalogoAyudaTipoOrganizacion",
            EncabezadoGrid = "Folio",
            MetodoInvocacion = "ObtenerPorPaginaTipoOrganizacion",
            PopUp = true,
            EstaEnContenedor = true,
            NombreContenedor = "Contrato")]
        [AtributoAyuda(Nombre = "PropiedadFolioContratoFlete",
            EncabezadoGrid = "Folio",
            MetodoInvocacion = "ObtenerPorFolioContrato",
            PopUp = false,
            EstaEnContenedor = true,
            NombreContenedor = "Contrato")]
        [AtributoInicializaPropiedad]
        public int Folio
        {
            set
            {
                folio = value;
                NotifyPropertyChanged("Folio");
            }
            get { return folio; }
        }

        public string FolioCadena { get; set; }

        /// <summary>
        /// Identificador del Producto 
        /// </summary>
        public ProductoInfo Producto { set; get; }
        /// <summary>
        /// Identificador del tipo de contrato
        /// </summary>
        public TipoContratoInfo TipoContrato { set; get; }

        /// <summary>
        /// Cuenta del contrato
        /// </summary>
        public CuentaSAPInfo Cuenta { set; get; }
        /// <summary>
        /// Indicador si esta activo o no el registro.
        /// </summary>
        public CompraParcialEnum Parcial { set; get; }
        /// <summary>
        /// Identificador del tipo de flete
        /// </summary>
        public TipoFleteInfo TipoFlete { set; get; }
        /// <summary>
        /// Identificador del proveedor
        /// </summary>
        public ProveedorInfo Proveedor { set; get; }
        /// <summary>
        /// Precio
        /// </summary>
        public decimal Precio { set; get; }

        /// <summary>
        /// Precio convertido al tipo cambio de cuando se genero el contrato
        /// </summary>
        public decimal PrecioConvertido { get; set; }

        /// <summary>
        /// Identificador del tipo de cambio
        /// </summary>
        public TipoCambioInfo TipoCambio { set; get; }

        /// <summary>
        /// Cantidad de producto
        /// </summary>
        public decimal Cantidad { set; get; }
        /// <summary>
        /// Cantidad de merma del producto
        /// </summary>
        public decimal Merma { set; get; }

        /// <summary>
        /// Tolerancia permitida
        /// </summary>
        public decimal Tolerancia { set; get; }

        /// <summary>
        /// Peso a negociar
        /// </summary>
        public string PesoNegociar
        {
            set
            {
                pesoNegociar = value;
                NotifyPropertyChanged("PesoNegociar");
            }
            get { return pesoNegociar; }
        }

        /// <summary>
        /// Fecha del contrato
        /// </summary>
        public DateTime Fecha { set; get; }
        /// <summary>
        /// Fecha de vigencia del contrato
        /// </summary>
        public DateTime FechaVigencia { set; get; }

        /// <summary>
        /// Indicador si esta activo o no el registro.
        /// </summary>
        public Enums.EstatusEnum Activo { set; get; }
        /// <summary>
        /// Usuario de creacion del contrato
        /// </summary>
        public int UsuarioCreacionId { set; get; }
        /// <summary>
        /// Lista que contiene el total de contrato detalle por contrato
        /// </summary>
        public List<ContratoDetalleInfo> ListaContratoDetalleInfo { set; get; }
        /// <summary>
        /// Usuario de modificacion del contrato
        /// </summary>
        public int UsuarioModificacionId { set; get; }

        /// <summary>
        /// Lista para manejar el parámetro de los productos
        /// </summary>
        public IList<ProductoInfo> ListaProductos { set; get; }

        /// <summary>
        /// Lista para manejar el parámetro de los proveedores
        /// </summary>
        public IList<ProveedorInfo> ListaProveedores { set; get; }

        /// <summary>
        /// Lista para manejar el parámetro de los tipo contrato
        /// </summary>
        public IList<TipoContratoInfo> ListaTipoContrato { set; get; }

        /// <summary>
        /// Lista para manejar el parámetro de los tipo flete
        /// </summary>
        public IList<TipoFleteInfo> ListaTipoFlete { set; get; }

        /// <summary>
        /// Lista para manejar el parámetro de los tipo flete
        /// </summary>
        public IList<EstatusInfo> ListaEstatusContrato { set; get; }

        /// <summary>
        /// Lista de parcialidades capturadas para el contrato
        /// </summary>
        public List<ContratoParcialInfo> ListaContratoParcial { set; get; }

        /// <summary>
        /// Lista de humedades
        /// </summary>
        public List<ContratoHumedadInfo> ListaContratoHumedad { set; get; } 

        /// <summary>
        /// Cantidad de producto en toneladas
        /// </summary>
        public decimal CantidadToneladas { set; get; }

        /// <summary>
        /// Precio de producto en toneladas
        /// </summary>
        public decimal PrecioToneladas { set; get; }

        /// <summary>
        /// Indica si es un contrato nuevo o un contrato previamente guardado
        /// </summary>
        public bool Guardado { set; get; }

        /// <summary>
        /// Id del estatus
        /// </summary>
        public EstatusInfo Estatus { set; get; }

        /// <summary>
        /// Para Saber el Tipo del Contrato
        /// </summary>
        public string tipocontratodescripcion { set; get; }

        /// <summary>
        /// Indica la cantidad en kilos que se ha surtido de ese contrato
        /// </summary>
        public int CantidadSurtida { set; get; }

        /// <summary>
        /// Indica la cantidad en toneladas que se ha surtido de ese contrato
        /// </summary>
        public decimal ToneladasSurtidas { set; get; }

        /// <summary>
        /// Folio del contrato aserca
        /// </summary>
        public string FolioAserca { set; get; }

        /// <summary>
        /// Folio de la cobertura
        /// </summary>
        public int FolioCobertura { set; get; }

        /// <summary>
        /// se selecciona destino se debe guardar en 0. Si se selecciona orgien se debe guardar en 1.
        /// </summary>
        public int CalidadOrigen { set; get; }

        /// <summary>
        /// Se debe guardar en 1 en caso de que se seleccione el campo “Aplica Costo de Secado”. En caso contrario debe quedar en 0.
        /// </summary>
        public int CostoSecado { get; set; }

        /// <summary>
        /// Se debe guardar en 1 en caso de que se seleccione el campo “Aplica Descuento”. En caso contrario debe quedar en 0.
        /// </summary>
        public int AplicaDescuento { set; get; }

        #region Miembros de INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        [AtributoAyuda(Nombre = "PropiedadProductoContratoFlete",
            EncabezadoGrid = "Producto",
            MetodoInvocacion = "ObtenerPorPaginaSinProgramacion",
            PopUp = true,
            EstaEnContenedor = true,
            NombreContenedor = "ProductoDescripcion")]
        [AtributoAyuda(Nombre = "PropiedadProductoContratoFlete",
            EncabezadoGrid = "Producto",
            MetodoInvocacion = "ObtenerPorPaginaSinProgramacion",
            PopUp = false,
            EstaEnContenedor = true,
            NombreContenedor = "ProductoDescripcion")]
        public string ProductoDescripcion { get; set; }

        #endregion
    }
}
