using System;
using SIE.Services.Info.Info;
using System.IO;

namespace SIE.Services.Info.Reportes
{
    public class ReporteLlegadaLogisticaDatos
    {
        public int FolioEmbarque { get; set; }
        public string TipoEmbarque { get; set; }
        public string Proveedor { get; set; }
        public string Chofer { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public string Estatus { get; set; }
        public string Descripcion { get; set; }
        public decimal Importe { get; set; }
        public int FolioEntrada { get; set; }
        public DateTime FechaEntrada { get; set; }
        public ReporteEncabezadoInfo Encabezado;
        public string TituloPeriodo { get; set; }
        public string Titulo { get; set; }
        public string Organizacion { get; set; }
        public DateTime FechaEmbarque { get; set; }
        /// <summary>
        /// Fecha Entrada con formato
        /// </summary>
        public string FechaEntradaConFormato
        {
            get
            {
                if (FechaEntrada == new DateTime(1900, 1, 1))
                    return string.Empty;
                return FechaEntrada.ToString("dd/MM/yyyy");
            }

        }
        /// <summary>
        /// Fecha Embarque con formato
        /// </summary>
        public string FechaEmbarqueConFormato
        {
            get
            {
                if (FechaEmbarque == new DateTime(1900, 1, 1))
                    return string.Empty;
                else
                    return FechaEmbarque.ToString("dd/MM/yyyy");
            }

        }
        /// <summary>
        /// Folio Entrada validado
        /// </summary>
        public string FolioEntradaConFormato
        {
            get
            {
                if (FolioEntrada == 0)
                    return string.Empty;
                else
                    return string.Format("{0}",FolioEntrada);
            }
        }
    }
}
