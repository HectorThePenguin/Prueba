namespace SIE.Services.Info.Info
{
    public class ProductoINFORInfo
    {
        /// <summary>
        /// Descripción TRA_CODE
        /// </summary>
        public string CodigoTransaccion { get; set; }

        /// <summary>
        /// Descripción TRA_ADVICE
        /// </summary>
        public string Referencia { get; set; }

        /// <summary>
        /// Descripción TRL_QTY
        /// </summary>
        public decimal Cantidad { get; set; }

        /// <summary>
        /// Descripción TRL_PRICE
        /// </summary>
        public decimal PrecioPromedio { get; set; }

        /// <summary>
        /// Descripción PAR_CODE
        /// </summary>
        public string CodigoParte { get; set; }

        /// <summary>
        /// Descripción PAR_DESC
        /// </summary>
        public string DescripcionParte { get; set; }

        /// <summary>
        /// Descripción TRL_COSTCODE
        /// </summary>
        public string CodigoCosto { get; set; }

        /// <summary>
        /// Descripción PAR_UDFCHAR02
        /// </summary>
        public string ParUdfchar02 { get; set; }

    }
}

