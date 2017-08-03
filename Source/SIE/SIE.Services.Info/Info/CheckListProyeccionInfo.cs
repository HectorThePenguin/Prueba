using System;

namespace SIE.Services.Info.Info
{
    public class CheckListProyeccionInfo
    {
        /// <summary>
        ///  Número del primer reimplante
        /// </summary>
        public int PrimerReimplante { get; set; }

        /// <summary>
        ///  Fecha proyectada del primer reimplante
        /// </summary>
        public DateTime FechaProyectada1 { get; set; }

        /// <summary>
        ///  Peso proyectado del primer reimplante
        /// </summary>
        public int PesoProyectado1 { get; set; }

        /// <summary>
        /// Número del segundo reimplante
        /// </summary>
        public int SegundoReimplante { get; set; }

        /// <summary>
        /// Fecha proyectada del segundo reimplante
        /// </summary>
        public DateTime FechaProyectada2 { get; set; }

        /// <summary>
        /// Peso proyectado del segundo reimplante
        /// </summary>
        public int PesoProyectado2 { get; set; }
    }
}
