using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Servicios.PL;
using SIE.Services.Info.Info;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.MessageBox;
using SuKarne.Controls.Enum;

namespace SIE.WinForm.Manejo
{
    /// <summary>
    /// Interaction logic for ProgramacionCorteGanadoDialogo.xaml
    /// </summary>
    public partial class ProgramacionCorteGanadoDialogo
    {
        private string _nombreImpresora;
        private IList<ProgramacionCorteInfo> listaProgramacionCorte;
        public bool guardado { get; set; }
        private int totalHembras, totalMachos, totalRechazos, totalRecibidas;
        private ImpresionProgramacionCorteInfo datosReporte;
        public ProgramacionCorteGanadoDialogo()
        {
            InitializeComponent();
        }

        public ProgramacionCorteGanadoDialogo(IList<ProgramacionCorteInfo> programacionCorte)
        {

            guardado = false;
            InitializeComponent();
            listaProgramacionCorte = programacionCorte;

            totalHembras = programacionCorte.Sum(t => t.Hembras);
            totalRecibidas = programacionCorte.Sum(t => t.CabezasRecibidas);
            totalMachos = programacionCorte.Sum(t => t.Machos);
            totalRechazos = programacionCorte.Sum(t => t.Rechazos);
            
            LblTotalCabezasTrabajarValor.Content = totalRecibidas;
            lblTotalHembrasValor.Content = totalHembras;
            LblTotalMachosValor.Content = totalMachos;
            LblTotalRechazados.Content = totalRechazos;

            dgCorteGanado.ItemsSource = programacionCorte;

            datosReporte = new ImpresionProgramacionCorteInfo();
            datosReporte.TotalHembras = totalHembras;
            datosReporte.TotalMachos = totalMachos;
            datosReporte.TotalRecibidas = totalRecibidas;

            datosReporte.FechaProgramacion = DateTime.Now.AddDays(1);
            datosReporte.ProgramacionCorte = programacionCorte;

            /* Obtener la impresora de la configuracion xml */
            ConfiguracionInfo configuracion = AuxConfiguracion.ObtenerConfiguracion();
            _nombreImpresora = configuracion.ImpresoraRecepcionGanado;
        }

        /// <summary>
        /// Imprimir el reporte de corte de ganado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                var pl = new ProgramacionCortePL();
                if (datosReporte != null)
                {
                    var regresoImpresion = pl.ImprimirProgramacionCorte(datosReporte);

                    if (regresoImpresion)
                    {
                        var programacionCortePl = new ProgramacionCortePL();

                        if (programacionCortePl.GuardarProgramacionCorte(listaProgramacionCorte) == 0)
                        {
                            guardado = true;
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.ProgramacionCorteGanado_MensajeGuardado,
                                MessageBoxButton.OK, MessageImage.Correct);
                            Close();

                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.ProgramacionCorteGanado_MensajeGuardadoError,
                                MessageBoxButton.OK, MessageImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                      Properties.Resources.ProgramacionCorte_ErrorImpresora + "-> " + ex.Message, MessageBoxButton.OK,
                          MessageImage.Error);
                
            }

            
                    
           
        }

    }
}
