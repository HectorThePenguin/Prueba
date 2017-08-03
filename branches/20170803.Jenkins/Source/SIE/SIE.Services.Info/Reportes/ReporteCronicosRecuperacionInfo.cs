using System;

namespace SIE.Services.Info.Reportes
{
    public class ReporteCronicosRecuperacionInfo
    {
        /// <summary>
        /// Fecha en que se etiqueto como cronico
        /// </summary>
        public DateTime FechaGenerado { get; set; }
        /// <summary>
        /// Nombre de la enfermeria
        /// </summary>
        public string Enfermeria { get; set; }
        /// <summary>
        /// Corral de Produccion del que proviene
        /// </summary>
        public string CorralOrigen { get; set; }
        /// <summary>
        /// Arete que tiene el Animal
        /// </summary>
        public string Arete { get; set; }
        /// <summary>
        /// Genero del Animal
        /// </summary>
        public string Sexo { get; set; }
        /// <summary>
        /// Causa por la cual entro a enfermeria
        /// </summary>
        public string Causa { get; set; }
        /// <summary>
        /// Operador que detecto el problema
        /// </summary>
        public string Detector { get; set; }
        /// <summary>
        /// Corral en donde se detecto el animal
        /// </summary>
        public string LugarGeneracion { get; set; }

        /// <summary>
        /// Corral en donde se encuentra el animal
        /// </summary>
        public string LugarDestino { get; set; }
        /// <summary>
        /// Primer tratamiento suministrado
        /// </summary>
        public string PrimerTratamiento { get; set; }
        /// <summary>
        /// Fecha del primer tratamiento
        /// suministrado
        /// </summary>
        public string DatePrimerTratamiento { get; set; }
        /// <summary>
        /// Segundo tratamiento suministrado
        /// </summary>
        public string SegundoTratamiento { get; set; }
        /// <summary>
        /// Fecha del segundo tratamiento
        /// suministrado
        /// </summary>
       public string DateSegundoTratamiento { get; set; }
        /// <summary>
        /// Tercer tratamiento suministrado
        /// </summary>
        public string TercerTratamiento { get; set; }
        /// <summary>
        /// Fecha del tercer tratamiento suministrado
        /// </summary>
        public string DateTercerTratamiento { get; set; }
      
        /// <summary>
        /// Partida a la que pertenece
        /// </summary>
        public long Partida { get; set; }

        /// <summary>
        /// Fecha en la que sale de enfermería
        /// </summary>
        public string DateAlta { get; set; }

        /// <summary>
        /// Número del mes en que entra a enfermería
        /// </summary>
        public int Mes { get; set; }

        /// <summary>
        /// Número de la Semana en que entra a enfermería
        /// </summary>
        public int Semana { get; set; }

        /// <summary>
        /// Fecha en la que sale de enfermería
        /// </summary>
        public string DateLlegada { get; set; }

        /// <summary>
        /// Número de la Semana en que entra a enfermería
        /// </summary>
        public int DiasEngorda { get; set; }

        public string Titulo { get; set; }
        public string Organizacion { get; set; }
        public string RangoFechas { get; set; }

        
        
        
        
        
    }
}
