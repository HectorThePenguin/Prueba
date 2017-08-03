using System;

namespace SIE.Services.Info.Info
{
    public class LoteReimplanteInfo : BitacoraInfo
    {
        ///<summary>
        /// Identificador lote reimplante
        /// </summary>
        public int LoteReimplanteID { get; set; }

        /// <summary>
        /// Identifica el Tipo de movimiento
        /// </summary>
        public int TipoMovimientoID { get; set; }

        /// <summary>
        /// Identifica el Folio de entrada
        /// </summary>
        public int FolioEntrada { get; set; }

        /// <summary>
        /// Identifica e numero de cabezas
        /// </summary>
        public int NumCabezas { get; set; }

        /// <summary>
        /// Identifica e numero de cabezas
        /// </summary>
        public int LoteProyeccionID { get; set; }

        /// <summary>
        /// Identifica e numero de cabezas
        /// </summary>
        public int NumeroReimplante { get; set; }

        /// <summary>
        /// Identifica e numero de cabezas
        /// </summary>
        public DateTime FechaProyectada { get; set; }

        /// <summary>
        /// Identifica e numero de cabezas
        /// </summary>
        public int PesoProyectado { get; set; }

        /// <summary>
        /// Identifica e numero de cabezas
        /// </summary>
        public DateTime FechaReal { get; set; }

        /// <summary>
        /// Identifica e numero de cabezas
        /// </summary>
        public int PesoReal { get; set; }

        /// <summary>
        /// Sirve para identificar la ganancia diaria de cada reimplante, Reporte Proyector
        /// </summary>
        public decimal GananciaDiaria { get; set; }
        /// <summary>
        /// Determina si el Reimplante se puede editar, en la pantalla Manejo/ConfiguracionReimplante
        /// </summary>
        public bool PermiteEditar { get; set; }
    }
}
