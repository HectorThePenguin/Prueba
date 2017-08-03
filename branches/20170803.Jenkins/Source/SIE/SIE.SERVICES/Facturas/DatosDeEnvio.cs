namespace SIE.Services.Facturas
{
    public class DatosDeEnvio
    {
        private string _codigoCliente;
        private string _codigoSucursal;
        public DatosDeEnvio()
        {
            PartyType = "CUSTOMER";
            _codigoSucursal = "00001";
        }

        /// <summary>
        /// CODIGO_CLIENTE.- CodigoSAP del cliente
        /// </summary>
        public string CodigoCliente {
            get
            {
                return _codigoCliente;
            } 
            set 
            { 
                _codigoCliente = value;
                PartyId = value + _codigoSucursal;
            }
        }
        /// <summary>
        /// PARTY_ID.- Se refiere al CodigoSap del cliente
        /// </summary>
        public string PartyId { get; set; }
        /// <summary>
        /// PARTY_NAME.- Nombre del cliente
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// PARTY_TYPE.- Se refiere al tipo de grupo (Default = CUSTOMER)
        /// </summary>
        public string PartyType { get; set; }
        /// <summary>
        /// JGZZ_FISCAL_CODE.- Codigo Fiscal Se refiere al RFC del cliente
        /// </summary>
        public string CodigoFiscal { get; set; }
        /// <summary>
        /// ADDRESS1.- 
        /// </summary>
        public string Direccion1 { get; set; }
        /// <summary>
        /// ADDRESS2
        /// </summary>
        public string Direccion2 { get; set; }
        /// <summary>
        /// ADDRESS3
        /// </summary>
        public string Direccion3 { get; set; }
        /// <summary>
        /// ADDRESS4
        /// </summary>
        public string Direccion4 { get; set; }
        /// <summary>
        /// Codigo Postal
        /// </summary>
        public string CodigoPostal { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        /// <summary>
        /// Este dato es fijo en el contructor con 00001
        /// </summary>
        public string CodigoSucursal
        {
            get
            {
                return _codigoSucursal;
            }
            set
            {
                _codigoSucursal = value;
            }
        }
    }
}
