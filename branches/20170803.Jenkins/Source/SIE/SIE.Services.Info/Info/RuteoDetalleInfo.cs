using System;

namespace SIE.Services.Info.Info
{
    public class RuteoDetalleInfo : BitacoraInfo
    {
        /// <summary>
        /// Identificación del ruteo detalle
        /// </summary>
        public int RuteoDetalleID { get; set; }

        /// <summary>
        /// Ruteo al que pertenece el detalle de ruta
        /// </summary>
        public RuteoInfo Ruteo { get; set; }
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
        public double Horas { get; set; }

        /// <summary>
        /// Fecha de llegada al destino del ruteo.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Horas estimadas del trayecto entre el origen y el destino
        /// </summary>
        public string HorasString { get; set; }

      
    }
}
