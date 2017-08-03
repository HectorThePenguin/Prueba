using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class AnimalDeteccionInfo
    {
        /// <summary>
        /// Identificador de la deteccion
        /// </summary>
        public int DeteccionID { get; set; }
        /// <summary>
        /// ruta de la foto del ganado enfermo
        /// </summary>
        public string RutaFotoDeteccion { get; set; }
        /// <summary>
        /// Sexo del animal enfermo
        /// </summary>
        public Sexo Sexo { get; set; }
        /// <summary>
        /// Lista de problemas
        /// </summary>
        public List<ProblemaInfo> Problemas { get; set; }

        public string DescripcionProblemas
        {
            get
            {
                string resultado = string.Empty;
                if (Problemas != null)
                {
                    resultado = Problemas.Aggregate(resultado, (current, problema) => current + (problema.Descripcion + ", "));
                    if (resultado.Length > 0)
                    {
                        resultado = resultado.Remove(resultado.Length - 2, 2);
                    }
                }
                return resultado;
            }
            
        }
        /// <summary>
        /// Grado de enfermedad
        /// </summary>
        public GradoInfo GradoEnfermedad { get; set; }
        /// <summary>
        /// Nombre del detector
        /// </summary>
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
        public DescripcionGanadoInfo DescripcionGanado { get; set; }
        /// <summary>
        /// Fecha de deteccion
        /// </summary>
        public DateTime FechaDeteccion { get; set; }
        /// <summary>
        /// Corral donde se encuentra la deteccion
        /// </summary>
        public EnfermeriaInfo EnfermeriaCorral { get; set; }
        /// <summary>
        /// Animal movimiento
        /// </summary>
        public AnimalMovimientoInfo AnimalMovimiento { get; set; }
        /// <summary>
        /// Usuario de para el registro de la deteccion
        /// </summary>
        public int UsuarioID { get; set; }
        /// <summary>
        /// Justificacion del analista
        /// </summary>
        public string Justificacion { get; set; }

        /// <summary>
        /// Indica si se realizo el diagnostico
        /// </summary>
        public int Diagnostico { get; set; }
        /// <summary>
        /// Indica el estatus de la deteccion 1=deteccion normal 0= deteccion temporal
        /// </summary>
        public int EstatusDeteccion { get; set; }

        /// <summary>
        /// Indica Si es necesario actualizar el arete en la deteccion
        /// </summary>
        public bool ActualizarAreteDeteccion { get; set; }
    }
}
