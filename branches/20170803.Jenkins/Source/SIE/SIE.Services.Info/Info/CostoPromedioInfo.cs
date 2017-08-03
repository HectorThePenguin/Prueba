namespace SIE.Services.Info.Info 
{
    public class CostoPromedioInfo : BitacoraInfo
    {
        /// <summary>
        ///     Identificador CostoOrganizacion
        /// </summary>
        public int CostoPromedioID { get; set; }

        /// <summary>
        ///    Organizacion
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary>
        ///     Costo
        /// </summary>
        public CostoInfo Costo { get; set; }

        /// <summary>
        ///     Automatico
        /// </summary>
        public decimal Importe { get; set; }
    }
}

