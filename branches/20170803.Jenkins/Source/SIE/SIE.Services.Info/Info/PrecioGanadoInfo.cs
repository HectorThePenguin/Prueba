using System;

namespace SIE.Services.Info.Info
{
    public class PrecioGanadoInfo : BitacoraInfo
    {
        /// <summary>
        ///     Identificador CostoOrganizacion
        /// </summary>
        public int PrecioGanadoID { get; set; }

        /// <summary>
        ///    Organizacion
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary>
        ///     Tipo de Ganado
        /// </summary>
        public TipoGanadoInfo TipoGanado { get; set; }

        /// <summary>
        ///     Preio
        /// </summary>
        public decimal Precio { get; set; }

        /// <summary>
        ///     Fecha de Vigencia
        /// </summary>
        public DateTime FechaVigencia { get; set; }
    }
}

