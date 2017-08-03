using System;

namespace SIE.Services.Info.Reportes
{
    public class ReporteCronicosRecuperacionEnfermeria
    {
        /// <summary>
        /// Fecha de entrada a enfermeria
        /// </summary>
        public DateTime Fecha { get; set; }
        /// <summary>
        /// Nombre de la enfermeria
        /// </summary>
        public string Enfermeria { get; set; }
        /// <summary>
        /// Arete del animal
        /// </summary>
        public string Arete { get; set; }
        /// <summary>
        /// Genero del animal
        /// </summary>
        public char Sexo { get; set; }
        /// <summary>
        /// Causa de su entrada a enfermeria
        /// </summary>
        public string Causa { get; set; }
        /// <summary>
        /// Operador detector
        /// </summary>
        public string Detector { get; set; }
        /// <summary>
        /// Corral de enfermeria
        /// </summary>
        public string CorralEnfermeria { get; set; }
        /// <summary>
        /// Identificador del Animal
        /// </summary>
        public int AnimalID { get; set; }
        /// <summary>
        /// Identificador del movimiento del animal
        /// </summary>
        public long AnimalMovimientoID { get; set; }

        /// <summary>
        /// Fecha en que sale el animal de enfermería
        /// </summary>
        public DateTime? FechaAlta { get; set; }
    }
}
