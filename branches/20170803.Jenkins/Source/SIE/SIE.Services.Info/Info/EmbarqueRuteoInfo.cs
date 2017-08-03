using System;

namespace SIE.Services.Info.Info 
{
    public class EmbarqueRuteoInfo : BitacoraInfo
    {
        /// <summary>
        /// id de embarque ruteo
        /// </summary>
        public long EmbarqueRuteoID { get; set; }

        /// <summary>
        /// organizacion de embarque ruteo
        /// </summary>
        public OrganizacionInfo OrganizacionOrigen { get; set; }
        
        /// <summary>
        /// fecha programada de ruteo
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// horas de ruteo
        /// </summary>
        public TimeSpan Horas { get; set; }

        /// <summary>
        /// Horas estimadas del trayecto entre el origen y el destino
        /// </summary>
        public string HorasString { get; set; }

        /// <summary>
        /// Kilometros de ruteo
        /// </summary>
        public int Kilometros { get; set; }

        /// <summary>
        /// Campo que almacena los datos del ruteo para el embarque.
        /// </summary>
        public RuteoInfo Ruteo { get; set; }
    }
}
