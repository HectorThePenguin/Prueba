using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace SIE.Services.Info.Info
{
    public class EnvioAlimentoInfo : BitacoraInfo
    {
        #region Atributos

        /// <summary>
        /// Identificados del envio de alimento
        /// </summary>
        private int envioId;

        /// <summary>
        /// Organizacion de origen del envio
        /// </summary>
        private OrganizacionInfo _origen;

        /// <summary>
        /// Organizacion de destino del envio
        /// </summary>
        private OrganizacionInfo _destino;

        /// <summary>
        /// Alimento que se envia
        /// </summary>
        private ProductoInfo _producto;

        /// <summary>
        /// Almacen de origen del que se realiza el envio
        /// </summary>
        private AlmacenInfo _almacen;

        /// <summary>
        /// Inventario del producto en el almacen 
        /// </summary>
        private AlmacenInventarioInfo _almacenInventario;

        /// <summary>
        /// Cuenta sap del producto en el almacen
        /// </summary>
        private ClaseCostoProductoInfo _costoProduto;

        /// <summary>
        /// Cantidad del envio
        /// </summary>
        private decimal _cantidad;

        /// <summary>
        /// Importe del envio
        /// </summary>
        private decimal _importe;
        
        /// <summary>
        /// Numero de piezas del envio 
        /// </summary>
        private int _piezas;

        /// <summary>
        /// Folio del envio
        /// </summary>
        private long _folio;

        /// <summary>
        /// Fecha de envio
        /// </summary>
        private DateTime _fechaEnvio;

        /// <summary>
        /// Folio del envio
        /// </summary>
        private long _trasferenciaId;

        /// <summary>
        /// ID del movimiento de almacen del envio
        /// </summary>
        private long _almacenMovimientoId;

        /// <summary>
        /// Poliza generada en el envio
        /// </summary>
        private MemoryStream _poliza;

        #endregion

        #region Propiedades

        /// <summary>
        /// Infomración de la organización de donde sale el producto
        /// </summary>
        public OrganizacionInfo Origen
        {
            get { return _origen; }
            set { _origen = value; }
        }

        /// <summary>
        /// Informacion de la orgniazacion destino del producto
        /// </summary>
        public OrganizacionInfo Destino
        {
            get { return _destino; }
            set { _destino = value; }
        }

        /// <summary>
        /// Informacion del producto a enviar
        /// </summary>
        public ProductoInfo Producto
        {
            get { return _producto; }
            set { _producto = value; }
        }

        /// <summary>
        /// Infomración del almacen donde sale el producto
        /// </summary>
        public AlmacenInfo Almacen
        {
            get { return _almacen; }
            set { _almacen = value; }
        }

        /// <summary>
        /// Infomracion del invenario del producto
        /// </summary>
        public AlmacenInventarioInfo AlmacenInventario
        {
            get { return _almacenInventario; }
            set { _almacenInventario = value; }
        }

        /// <summary>
        /// Cantidad de producto a enviar
        /// </summary>
        public decimal Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        /// <summary>
        /// Importe de los productos enviados
        /// </summary>
        public decimal Importe
        {
            get { return _importe; }
            set { _importe = value; }
        }

        /// <summary>
        /// Piezas enviadas
        /// </summary>
        public int Piezas
        {
            get { return _piezas; }
            set { _piezas = value; }
        }

        /// <summary>
        /// Información del costo del producto
        /// </summary>
        public ClaseCostoProductoInfo CostoProduto
        {
            get { return _costoProduto; }
            set { _costoProduto = value; }
        }

        /// <summary>
        /// Folio del envío del producto geenrado
        /// </summary>
        public long Folio
        {
            get { return _folio; }
            set { _folio = value; }
        }

        /// <summary>
        /// Fecha de envío del producto
        /// </summary>
        public DateTime FechaEnvio
        {
            get { return _fechaEnvio; }
            set { _fechaEnvio = value; }
        }

        /// <summary>
        /// Identificador de la transferencia generada
        /// </summary>
        public long TransferenciaId
        {
            get { return _trasferenciaId; }
            set { _trasferenciaId = value; }
        }

        /// <summary>
        /// Identificador del envío
        /// </summary>
        public int EnvioId
        {
            get { return envioId; }
            set { envioId = value; }
        }

        /// <summary>
        /// Identificador del movimiento de almacen generado por el envío
        /// </summary>
        public long AlmacenMovimientoId
        {
            get { return _almacenMovimientoId; }
            set { _almacenMovimientoId = value; }
        }

        /// <summary>
        /// Folio del movimiento del almacen
        /// </summary>
        public long FolioMovimientoAlmacen
        {
            get
            {
                return long.Parse(this._folio.ToString().Substring(this._folio.ToString().Length - 6));
            }
        }

        /// <summary>
        /// Información para la impresión de la póliza
        /// </summary>
        public MemoryStream Poliza
        {
            get { return _poliza; }
            set { _poliza = value; }
        }

        #endregion

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
