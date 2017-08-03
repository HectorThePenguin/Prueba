using System;
namespace SIE.Services.Info.Reportes
{
    public class ReporteVentaMuerteInfo
    {
        /// <summary>
        /// Fecha en que se etiqueto como cronico
        /// </summary>
        public string Fecha { get; set; }
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
        /// Primer tratamiento suministrado
        /// </summary>
        public string PrimerTratamiento { get; set; }
        /// <summary>
        /// Fecha del primer tratamiento
        /// suministrado
        /// </summary>
        public string FechaPrimerTratamiento { get; set; }
        /// <summary>
        /// Segundo tratamiento suministrado
        /// </summary>
        public string SegundoTratamiento { get; set; }
        /// <summary>
        /// Fecha del segundo tratamiento
        /// suministrado
        /// </summary>
        public string FechaSegundoTratamiento { get; set; }
        /// <summary>
        /// Tercer tratamiento suministrado
        /// </summary>
        public string TercerTratamiento { get; set; }
        /// <summary>
        /// Fecha del tercer tratamiento suministrado
        /// </summary>
        public string FechaTercerTratamiento { get; set; }
        /// <summary>
        /// Corral en donde se detecto el animal
        /// </summary>
        public string LugarGeneracion { get; set; }
        /// <summary>
        /// Partida a la que pertenece
        /// </summary>
        public long Partida { get; set; }
        /// <summary>
        /// Organizacion que proviene el animal
        /// </summary>
        public string OrganizacionOrigen { get; set; }
        /// <summary>
        /// Dias que paso en engorda
        /// </summary>
        public int DiasEngorda { get; set; }
        /// <summary>
        /// Organizacion que se mostrara en el rpt
        /// </summary>
        public string Organizacion { get; set; }
        /// <summary>
        /// Titulo que tendra el RPT
        /// </summary>
        public string Titulo { get; set; }
        /// <summary>
        /// rango de fechas en que se genero el RPT
        /// </summary>
        public string RangoFechas { get; set; }

    }
}
