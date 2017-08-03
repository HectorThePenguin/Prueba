//--*********** Info *************
using System;
using SIE.Services.Info.Info;
namespace SIE.Services.Info.Reportes
{
    public class
        ReporteConsumoProgramadovsServidoInfo
    {
        /// <summary>
        /// Codigo
        /// </summary>
        public string Codigo { get; set; }
        /// <summary>
        /// Cabezas
        /// </summary>
        public int Cabezas { get; set; }
        /// <summary>
        /// PesoProyectado
        /// </summary>
        public int PesoProyectado { get; set; }
        /// <summary>
        /// DiasEngorda
        /// </summary>
        public int DiasEngorda { get; set; }
        /// <summary>
        /// FormulaIDServida
        /// </summary>
        public int FormulaIDServida { get; set; }
        /// <summary>
        /// Descripcion
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// CantidadProgramada
        /// </summary>
        public int CantidadProgramada { get; set; }
        /// <summary>
        /// CantidadServida
        /// </summary>
        public int CantidadServida { get; set; }
        /// <summary>
        /// Diferencia
        /// </summary>
        public int Diferencia { get; set; }
        /// <summary>
        /// Consumo promedio
        /// </summary>
        public decimal ConsumoPromedio { get; set; }
        /// <summary>
        /// CPV
        /// </summary>
        public decimal PorcentajeCPV { get; set; }

        public int TotalPesoProyectado {
            get { return PesoProyectado*Cabezas; }
        }

        public int TotalDiasEngorda {
            get { return DiasEngorda*Cabezas; }
        }

        public decimal TotalConsumoPromedio {
            get { return ConsumoPromedio*Cabezas; }
        }

        public decimal TotalPorcentajeCPV {
            get { return PorcentajeCPV*Cabezas; }
        }

        /// <summary>
        /// Fecha
        /// </summary>
        public DateTime Fecha { get; set; }
        /// <summary>
        /// TipoServicioID
        /// </summary>
        public int TipoServicioID { get; set; }

        public string Titulo { get; set; }
        public string Organizacion { get; set; }
    }
}
