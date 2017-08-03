using SIE.Services.Info.Enums;
namespace SIE.Services.Info.Info
{
    public class CostoOrganizacionInfo : BitacoraInfo
    {
        public CostoOrganizacionInfo()
        {
            TipoOrganizacion = new TipoOrganizacionInfo();
            Costo = new CostoInfo();
        }

        /// <summary>
        ///     Identificador CostoOrganizacion
        /// </summary>
        public int CostoOrganizacionID { get; set; }

        /// <summary>
        ///     Tipo de Organizacion
        /// </summary>
        public TipoOrganizacionInfo TipoOrganizacion { get; set; }

        /// <summary>
        ///     Costo
        /// </summary>
        public CostoInfo Costo { get; set; }

        /// <summary>
        ///     Automatico
        /// </summary>
        public Automatico Automatico { get; set; }

        /// <summary>
        ///  Propiedad que indica el Importe del Costo Automatico
        /// </summary>
        public decimal Importe { get; set; }
    }
}
