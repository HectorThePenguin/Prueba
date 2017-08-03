namespace SIE.Services.Info.Info
{
    public class CuentaValorInfo : BitacoraInfo
    {
        /// <summary>
        ///     Identificador CuentaValor
        /// </summary>
        public int CuentaValorID { get; set; }

        /// <summary>
        ///     Entidad Cuenta
        /// </summary>
        public CuentaInfo Cuenta { get; set; }

        /// <summary>
        ///     Entidad Organizacion
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary>
        ///     Valor de la cuenta
        /// </summary>
        public string Valor { get; set; }
    }
}