using System;
using System.Collections.Generic;
using System.Globalization;

namespace SIE.Services.Info.Reportes
{
    public class ReporteTabularDisponibilidadSemanaInfo
    {
        public int SemanaAnio { get; set; }
        public int NumeroSemana { get; set; }

        public int TotalCabezas { get; set; }
        public int TotalCorrales {
            get
            {
                int nCorrales = 0;
                if (Corrales != null)
                    nCorrales = Corrales.Count;

                return nCorrales;
            }
        }

        public DateTime FechaInicioSemana { get; set; }
        public IList<ReporteTabularDisponibilidadInfo> Corrales { get; set; }
    }
}
