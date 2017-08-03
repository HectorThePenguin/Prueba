namespace SIE.Services.Info.Info
{
    public partial class IvaInfo : BitacoraInfo
    {
        /// <summary>
        ///     Identificador Iva
        /// </summary>
        public int IvaID { get; set; }

        /// <summary>
        ///     Iva Description.
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        ///     Tasa del Iva
        /// </summary>
        public decimal TasaIva { get; set; }

        /// <summary>
        ///     Indicador iva a pagar
        /// </summary>
        public string IndicadorIvaPagar  { get; set; }

        /// <summary>
        ///     Cuenta iva a pagar
        /// </summary>
        public CuentaInfo CuentaPagar { get; set; }

        /// <summary>
        ///     Indicador a recuperar
        /// </summary>
        public string IndicadorIvaRecuperar { get; set; }

        /// <summary>
        ///     Cuenta de iva a recuperar
        /// </summary>
        public CuentaInfo CuentaRecuperar { get; set; }
    }
}
