using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using Application = System.Windows.Application;

namespace SIE.WinForm.PlantaAlimentos
{
    /// <summary>
    /// Lógica de interacción para AplicacionConsumoAlimento.xaml
    /// </summary>
    public partial class AplicacionConsumoAlimento
    {

        #region PROPIEDADES
        /// <summary>
        /// Contexto
        /// </summary>
        private AplicacionConsumoModel Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (AplicacionConsumoModel)DataContext;
            }
            set { DataContext = value; }
        }

        private void InicializaContexto()
        {
            Contexto = new AplicacionConsumoModel
                           {
                               Organizacion = new OrganizacionInfo(),
                               FechaConsumo = DateTime.Now
                           };
        }

        #endregion PROPIEDADES

        public AplicacionConsumoAlimento()
        {
            InitializeComponent();
            InicializaContexto();
            CargarOrganizaciones();
        }

        private void ConsultarConsumo()
        {
            try
            {
                if (dpFecha.SelectedDate == null || dpFecha.SelectedDate.Value == DateTime.MinValue)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AplicacionConsumoAlimento_FechaInvalida, MessageBoxButton.OK, MessageImage.Warning);
                    return;
                }
                var repartoPL = new RepartoPL();
                Contexto.ListaFormulas = repartoPL.ObtenerConsumoPendiente(Contexto);
                if (Contexto.ListaFormulas == null || !Contexto.ListaFormulas.Any())
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AplicacionConsumoAlimento_SinConsumos, MessageBoxButton.OK, MessageImage.Warning);
                    return;
                }
                dgConsumos.ItemsSource = Contexto.ListaFormulas;
                var consumoNegativo = Contexto.ListaFormulas.FirstOrDefault(rep => rep.CantidadDiferencia < 0);
                if (consumoNegativo == null)
                {
                    dpFecha.IsEnabled = false;
                    btnEjecutar.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AplicacionConsumoAlimento_ErrorConsultarConsumos, MessageBoxButton.OK, MessageImage.Error);
            }
        }


        private void btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            ConsultarConsumo();
        }

        private void btnEjecutar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ProcesoConsumo = new Thread(delegate(object ctx)
                                                    {
                                                        Dispatcher.BeginInvoke((Action) delegate
                                                                                            {
                                                                                                btnEjecutar.IsEnabled = false;
                                                                                                imgloading.Visibility = Visibility.Visible;
                                                                                            },
                                                                               DispatcherPriority.Background, null);
                                                        try
                                                        {
                                                            var context = ctx as AplicacionConsumoModel;
                                                            var consumoAlimentoPL = new ConsumoAlimentoPL();
                                                            consumoAlimentoPL.GenerarConsumoAlimento(
                                                                context.Organizacion.OrganizacionID, context.FechaConsumo);
                                                            Dispatcher.BeginInvoke((Action)delegate
                                                            {
                                                                SkMessageBox.Show(
                                                                    Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                                    Properties.Resources.AplicacionConsumoAlimento_EjecutadoExito,
                                                                    MessageBoxButton.OK,
                                                                    MessageImage.Correct);
                                                                Limpiar();
                                                            },
                                                                                   DispatcherPriority.Background, null);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            Logger.Error(ex);
                                                            Dispatcher.BeginInvoke((Action) delegate
                                                                                                {
                                                                                                    SkMessageBox.Show(
                                                                                                            Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                                                                            Properties.Resources.AplicacionConsumoAlimento_ErrorEjecutarConsumos,
                                                                                                            MessageBoxButton.OK,MessageImage.Error);
                                                                                                    btnEjecutar.IsEnabled = true;
                                                                                                    imgloading.Visibility = Visibility.Hidden;
                                                                                                },
                                                                                   DispatcherPriority.Background, null);
                                                        }
                                                    });
                ProcesoConsumo.IsBackground = true;
                ProcesoConsumo.Start(Contexto);
            }
            catch (Exception ex)
            {
                btnEjecutar.IsEnabled = true;
                imgloading.Visibility = Visibility.Hidden;
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.AplicacionConsumoAlimento_ErrorEjecutarConsumos, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            dpFecha.IsEnabled = true;
            btnEjecutar.IsEnabled = false;
            dgConsumos.ItemsSource = null;
            imgloading.Visibility = Visibility.Hidden;
            InicializaContexto();
            CargarOrganizaciones();
        }

        /// <summary>
        /// Carga las organizaciones
        /// </summary>
        private void CargarOrganizaciones()
        {
            try
            {
                bool usuarioCorporativo = AuxConfiguracion.ObtenerUsuarioCorporativo();
                //Obtener la organizacion del usuario
                int organizacionId = AuxConfiguracion.ObtenerOrganizacionUsuario();
                var organizacionPl = new OrganizacionPL();

                var organizacion = organizacionPl.ObtenerPorID(organizacionId);
                //int tipoOrganizacion = organizacion.TipoOrganizacion.TipoOrganizacionID;
                var nombreOrganizacion = organizacion != null ? organizacion.Descripcion : String.Empty;

                var organizacionesPl = new OrganizacionPL();
                IList<OrganizacionInfo> listaorganizaciones = organizacionesPl.ObtenerTipoGanaderas();
                if (usuarioCorporativo)
                {

                    var organizacion0 =
                        new OrganizacionInfo
                        {
                            OrganizacionID = 0,
                            Descripcion = Properties.Resources.ReporteConsumoProgramadovsServido_cmbSeleccione,
                        };
                    listaorganizaciones.Insert(0, organizacion0);
                    cmbOrganizacion.ItemsSource = listaorganizaciones;
                    cmbOrganizacion.SelectedIndex = 0;
                    cmbOrganizacion.IsEnabled = true;

                }
                else
                {
                    var organizacion0 =
                        new OrganizacionInfo
                        {
                            OrganizacionID = organizacionId,
                            Descripcion = nombreOrganizacion
                        };
                    listaorganizaciones.Insert(0, organizacion0);
                    cmbOrganizacion.ItemsSource = listaorganizaciones;
                    cmbOrganizacion.SelectedIndex = 0;
                    cmbOrganizacion.IsEnabled = false;
                    Contexto.Organizacion = organizacion0;
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.AplicacionConsumoAlimento_ErrorCargarOrganizaciones,
                    MessageBoxButton.OK, MessageImage.Error);

            }
        }
    }
}
