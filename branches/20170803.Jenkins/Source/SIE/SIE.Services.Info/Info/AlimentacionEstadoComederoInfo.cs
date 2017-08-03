using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class AlimentacionEstadoComederoInfo
    {
        /// <summary>
        /// código del corral de todos los corrales que cuenten con programación de reparto para el día actual.
        /// </summary>
        public string Corral { get; set; }
        /// <summary>
        /// lote correspondiente al corral
        /// </summary>
        public string Lote { get; set; }
        /// <summary>
        /// tipo de ganado que le corresponde al corral
        /// </summary>
        public string TipoGanado { get; set; }
        /// <summary>
        /// cantidad de cabezas con las que cuenta el lote hasta el momento de generar el reporte
        /// </summary>
        public int Cabezas { get; set; }
        /// <summary>
        /// cantidad promedio de días de engorda con los que cuenta el lote según la cantidad de animales
        /// </summary>
        public int DiasEngorda { get; set; }
        /// <summary>
        /// total de peso promedio de animales por lote correspondiente al corral
        /// </summary>
        public int PesoProyectado { get; set; }
        /// <summary>
        /// cantidad de días que tiene el corral sirviéndose la última fórmula.
        /// </summary>
        public int DiasUltimaFormula { get; set; }
        /// <summary>
        /// cantidad promedio de consumo en los últimos 5 días
        /// </summary>
        public decimal Promedio5Dias { get; set; }
        /// <summary>
        /// último Estado de Comedero registrado hasta el día actual
        /// </summary>
        public int EstadoComederoHoy { get; set; }
        /// <summary>
        /// cantidad de alimento programado a servir en el servicio matutino del día actual
        /// </summary>
        public int AlimentacionProgramadaMatutinaHoy { get; set; }
        /// <summary>
        /// fórmula de alimento programada a servir en el servicio matutino del día actual
        /// </summary>
        public string FormulaProgramadaMatutinaHoy { get; set; }
        /// <summary>
        /// cantidad de alimento programado a servir en el servicio vespertino del día actual
        /// </summary>
        public int AlimentacionProgramadaVespertinaHoy { get; set; }
        /// <summary>
        /// fórmula de alimento programada a servir en el servicio vespertino del día actual
        /// </summary>
        public string FormulaProgramadaVespertinaHoy { get; set; }
        /// <summary>
        /// cantidad de alimento programado para servir en el día actua
        /// </summary>
        public int TotalProgramadoHoy { get; set; }
        /// <summary>
        /// Estado de Comedero registrado el día anterior al actual, es decir el día de ayer
        /// </summary>
        public int EstadoComederoRealAyer { get; set; }
        /// <summary>
        /// cantidad de alimento servido en el servicio matutino del día anterior
        /// </summary>
        public int AlimentacionRealMatutinaAyer { get; set; }
        /// <summary>
        /// fórmula de alimento servido en el servicio matutino del día anterior
        /// </summary>
        public string FormulaRealMatutinaAyer { get; set; }
        /// <summary>
        /// cantidad de alimento servido en el servicio vespertino del día anterior
        /// </summary>
        public int AlimentacionRealVespertinoAyer { get; set; }
        /// <summary>
        /// fórmula de alimento servida en el servicio vespertino del día anterior
        /// </summary>
        public string FormulaRealVespertinoAyer { get; set; }
        /// <summary>
        /// cantidad de alimento servido en el día anterior
        /// </summary>
        public int TotalRealAyer { get; set; }
        /// <summary>
        /// cantidad de kilogramos de alimento servido en el día 3 hacia atrás de la fecha actual
        /// </summary>
        public int Kilogramos3Dias { get; set; }
        /// <summary>
        /// consumo de alimento por cabeza en el día 3 hacia atrás empezando a contar de la fecha actual
        /// </summary>
        public decimal ConsumoCabeza3Dias { get; set; }
        /// <summary>
        /// Estado de Comedero que tuvo el corral el día 3 hacia atrás empezando a contar de la fecha actual
        /// </summary>
        public int EstadoComedero3Dias { get; set; }
        /// <summary>
        /// cantidad de kilogramos de alimento servido en el día 4 hacia atrás de la fecha actual
        /// </summary>
        public int Kilogramos4Dias { get; set; }
        /// <summary>
        /// consumo de alimento por cabeza en el día 4 hacia atrás empezando a contar de la fecha actual
        /// </summary>
        public decimal ConsumoCabeza4Dias { get; set; }
        /// <summary>
        /// Estado de Comedero que tuvo el corral el día 4 hacia atrás empezando a contar de la fecha actual
        /// </summary>
        public int EstadoComedero4Dias { get; set; }
        /// <summary>
        /// cantidad de kilogramos de alimento servido en el día 5 hacia atrás de la fecha actual
        /// </summary>
        public int Kilogramos5Dias { get; set; }
        /// <summary>
        /// consumo de alimento por cabeza en el día 5 hacia atrás empezando a contar de la fecha actual
        /// </summary>
        public decimal ConsumoCabeza5Dias { get; set; }
        /// <summary>
        /// Estado de Comedero que tuvo el corral el día 5 hacia atrás empezando a contar de la fecha actual
        /// </summary>
        public int EstadoComedero5Dias { get; set; }
        /// <summary>
        /// cantidad de kilogramos de alimento servido en el día 6 hacia atrás de la fecha actual
        /// </summary>
        public int Kilogramos6Dias { get; set; }
        /// <summary>
        /// consumo de alimento por cabeza en el día 6 hacia atrás empezando a contar de la fecha actual
        /// </summary>
        public decimal ConsumoCabeza6Dias { get; set; }
        /// <summary>
        /// Estado de Comedero que tuvo el corral el día 6 hacia atrás empezando a contar de la fecha actual
        /// </summary>
        public int EstadoComedero6Dias { get; set; }
        /// <summary>
        /// cantidad de kilogramos de alimento servido en el día 7 hacia atrás de la fecha actual
        /// </summary>
        public int Kilogramos7Dias { get; set; }
        /// <summary>
        /// consumo de alimento por cabeza en el día 7 hacia atrás empezando a contar de la fecha actual
        /// </summary>
        public decimal ConsumoCabeza7Dias { get; set; }
        /// <summary>
        /// Estado de Comedero que tuvo el corral el día 7 hacia atrás empezando a contar de la fecha actual
        /// </summary>
        public int EstadoComedero7Dias { get; set; }
    }
}
