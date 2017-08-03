using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class AnimalEnfermeriaInfo
    {
        /// <summary>
        /// ruta de la foto del ganado enfermo
        /// </summary>
        public string RutaFotoDeteccion { get; set; }
        /// <summary>
        /// Sexo del animal enfermo
        /// </summary>
        public Sexo Sexo { get; set; }
        /// <summary>
        /// Problema detectado
        /// </summary>
        public string Problema { get; set; }
        /// <summary>
        /// Grado de enfermedad
        /// </summary>
        public string GradoEnfermedad { get; set; }

        public string NombreDetector { get; set; }
        /// <summary>
        /// operador que realizo la deteccion
        /// </summary>
        public OperadorInfo Detector { get; set; }
        /// <summary>
        /// Animal Enfermo
        /// </summary>
        public AnimalInfo Animal { get; set; }
        /// <summary>
        /// Descripcion del ganado al momento de la descripcion
        /// </summary>
        public string DescripcionGanado { get; set; }
        /// <summary>
        /// Fecha de deteccion
        /// </summary>
        public DateTime FechaDeteccion { get; set; }
    }
}
