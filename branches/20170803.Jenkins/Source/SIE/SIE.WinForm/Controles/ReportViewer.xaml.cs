using System;
using System.Windows;
using CrystalDecisions.CrystalReports.Engine;
using SIE.Base.Log;

namespace SIE.WinForm.Controles
{
    /// <summary>
    /// Lógica de interacción para ReportViewer.xaml
    /// </summary>
    public partial class ReportViewer
    {
        private ReportDocument Reporte { get; set; }
        public string TituloReporte { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reporte">Reporte a cargar</param>
        /// <param name="tituloReporte">Titulo del reporte a mostrar</param>
        public ReportViewer(ReportDocument reporte, string tituloReporte)
        {
            InitializeComponent();

            Reporte = reporte;
            TituloReporte = tituloReporte;
        }

        /// <summary>
        /// Muestra el informe cargado
        /// </summary>
        public void MostrarReporte()
        {
            try
            {
                if (Reporte != null)
                {
                    rptReportViewerControl.ViewerCore.ReportSource = Reporte;
                    ucTitulo.TextoTitulo = TituloReporte.Trim();
                    Title = TituloReporte.Trim();
                    WindowState = WindowState.Maximized;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }
    }
}
