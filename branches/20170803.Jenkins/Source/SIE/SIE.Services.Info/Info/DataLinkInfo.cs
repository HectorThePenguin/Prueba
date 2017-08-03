

using System;

namespace SIE.Services.Info.Info
{
    public class DataLinkInfo
    {
        /// <summary>
        /// Tipo de servicio
        /// </summary>
        public int TipoServicio { get; set; }
        /// <summary>
        /// Codigo del corral
        /// </summary>
        public string CodigoCorral { get; set; }
        /// <summary>
        /// Clave de la formula
        /// </summary>
        public string ClaveFormula { get; set; }
        /// <summary>
        /// Kilos programados
        /// </summary>
        public int KilosProgramados { get; set; }
        /// <summary>
        /// Kilos servidos
        /// </summary>
        public int KilosServidos { get; set; }
        /// <summary>
        /// Hora del reparto
        /// </summary>
        public string Hora { get; set; }
        /// <summary>
        /// Fecha del reparto
        /// </summary>
        public string CadenaFechaReparto { get; set; }
        /// <summary>
        /// Fecha de reparto en formato date time
        /// </summary>
        public DateTime FechaReparto { get; set; }
        /// <summary>
        /// Numero del camion
        /// </summary>
        public string NumeroCamion { get; set; }
        /// <summary>
        /// Informacion de camion de reparto
        /// </summary>
        public CamionRepartoInfo CamionReparto { get; set; }
        /// <summary>
        /// Fecha del archivo
        /// </summary>
        public string FechaArchivo { get; set; }
        /// <summary>
        /// Identificador del usuario
        /// </summary>
        public int UsuarioID { get; set; }
        /// <summary>
        /// Formula servida
        /// </summary>
        public FormulaInfo FormulaServida { get; set; }
        /// <summary>
        /// Reparto
        /// </summary>
        public RepartoInfo Reparto { get; set; }
        /// <summary>
        /// Identificador de la organizacion
        /// </summary>
        public int OrganizacionID { get; set; }


    }
}
