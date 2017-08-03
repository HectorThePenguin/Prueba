using System.Collections.Generic;

namespace SIE.Services.Facturas
{
    public class FacturaInfo
    {
        private string _nombreFactura;
        private string _indicadorTipoArchivo;
        private string _serie;
        private string _trxNumber;

        public FacturaInfo()
        {
            TipoCliente = "1";
            TipoMoneda = "MXN";
            EstatusFactura = "1";
            TasaDeCambio = "1";
            _indicadorTipoArchivo = "1";
            _serie = "";
            _trxNumber = "";
        }

        /// <summary>
        /// Corresponde a un identificador para el archivo generado (DEFAULT = 1)
        /// </summary>
        public string NombreFactura
        {
            get { return _nombreFactura; }
            set { _nombreFactura = value; }
        }
        /// <summary>
        /// Corresponde a un identificador para el archivo generado (DEFAULT = 1)
        /// </summary>
        public string IndicadorTipoArchivo
        {
            get { return _indicadorTipoArchivo; }
            set
            {
                _indicadorTipoArchivo = value;
                _nombreFactura = string.Format("{0}{1}{2}.xml", _serie, _indicadorTipoArchivo, _trxNumber.PadLeft(6, '0'));
            }
        }
        /// <summary>
        /// Letra correspondiente a la ganadera
        /// </summary>
        public string Serie
        {
            get { return _serie; }
            set
            {
                _serie = value;
                _nombreFactura = string.Format("{0}{1}{2}.xml", _serie, _indicadorTipoArchivo, _trxNumber.PadLeft(6, '0'));
            }
        }
        /// <summary>
        /// TRX_NUMBER.- Folio Correspondiente a la ganadera
        /// </summary>
        public string TrxNumber
        {
            get { return _trxNumber; }
            set
            {
                _trxNumber = value;
                _nombreFactura = string.Format("{0}{1}{2}.xml", _serie, _indicadorTipoArchivo, _trxNumber.PadLeft(6, '0'));
            }
        }
        /// <summary>
        /// CUST_TRX_TYPE_ID.- Hace referencia al tipo de cliente
        /// </summary>
        public string TipoCliente { get; set; }
        /// <summary>
        /// TRX_DATE.- Hace referencia a la fecha de la factura
        /// </summary>
        public string FechaFactura { get; set; }
        /// <summary>
        /// TIPO_MONEDA.- Muestra el tipo de moneda (Default = "MXN")
        /// </summary>
        public string TipoMoneda { get; set; }
        /// <summary>
        /// TASA_DE_CAMBIO.- Tasa de cambio (Default = 1)
        /// </summary>
        public string TasaDeCambio { get; set; }
        /// <summary>
        /// SELLER_ID.- Numero identificador del vendedor Mostrar Campo SELLER_ID de la tabla
        /// </summary>
        public string SellerId { get; set; }
        /// <summary>
        /// BUYER_ID.- Mostrar el valor igual que campo PARTY_ID
        /// </summary>
        public string BuyerId { get; set; }
        /// <summary>
        /// SHIP_FROM.- Se refiere a quien enviara el producto. Mostrar el campo SHIP_FROM de la tabla
        /// </summary>
        public string ShipFrom { get; set; }
        /// <summary>
        /// SHIP_TO.- Se refiere al identificador del comprador. Mostrar el valor igual que campo PARTY_ID
        /// </summary>
        public string ShipTo { get; set; }
        /// <summary>
        /// ACCOUNT_NUMBER.- Numero de cuenta
        /// </summary>
        public string NumeroDeCuenta { get; set; }
        /// <summary>
        /// STATUS_FACTURA.- Estatus de la Factura (Default = 1)
        /// </summary>
        public string EstatusFactura { get; set; }
        /// <summary>
        /// Datos del cliente para la facturación
        /// </summary>
        public DatosCliente DatosCliente { get; set; }
        /// <summary>
        /// Datos del envio para la facturación
        /// </summary>
        public DatosDeEnvio DatosDeEnvio { get; set; }
        /// <summary>
        /// Listado de Productos que llevara la factura.
        /// </summary>
        public List<ItemsFactura> ItemsFactura { get; set; } 
    }
}
