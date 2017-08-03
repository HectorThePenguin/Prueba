namespace SIE.Services.Info.Info
{
    public class ConfiguracionEmbarqueInfo : BitacoraInfo
    {
        /// <summary>
        /// Identificación de la configuración
        /// </summary>
        public int ConfiguracionEmbarqueID { get; set; }

        /// <summary>
        /// Organización origen de la configuración
        /// </summary>
        public OrganizacionInfo OrganizacionOrigen { get; set; }

        /// <summary>
        /// Organización destino de la configuración
        /// </summary>
        public OrganizacionInfo OrganizacionDestino { get; set; }

        /// <summary>
        /// Kolometros que existen entre el origen y el destino
        /// </summary>
        public decimal Kilometros { get; set; }

        /// <summary>
        /// Horas estimadas del trayecto entre el origen y el destino
        /// </summary>
        public decimal Horas { get; set; }

        public ConfiguracionEmbarqueDetalleInfo ConfiguracionEmbarqueDetalle { get; set; }
    }
}