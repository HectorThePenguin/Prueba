using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;


namespace SIE.WinForm.Sanidad
{
   
    /// <summary>
    /// Lógica de interacción para EvaluacionPartida.xaml
    /// </summary>
    public partial class EvaluacionPartida
    { 
        private int organizacionID;

        public EvaluacionPartida()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento de Carga de la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dtpFecha.SelectedDate = DateTime.Now;
                organizacionID = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
                ucPaginacion.DatosDelegado += ObtenerListaEvaluacion;
                ucPaginacion.AsignarValoresIniciales();
                Buscar();
                dtpFecha.Focus();
                
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.EvaluacionPartida_ErrorCargar, 
                                  MessageBoxButton.OK, 
                                  MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], 
                                  Properties.Resources.EvaluacionPartida_ErrorCargar, 
                                  MessageBoxButton.OK, 
                                  MessageImage.Error);
            }
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void Buscar()
        {
            ObtenerListaEvaluacion(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Evento que se ejecuta cuando se presiona una tecla en el Control de Importe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Fechas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                var keyboardFocus = Keyboard.FocusedElement as UIElement;

                if (keyboardFocus != null)
                {
                    keyboardFocus.MoveFocus(tRequest);
                }
                e.Handled = true;
            }
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// LostFocus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpFecha_LostFocus(object sender, RoutedEventArgs e)
        {
            bool isValid = ValidaFechaInicial();

            if (isValid)
            {
                return;
            }
            var tRequest = new TraversalRequest(FocusNavigationDirection.Previous);
            dtpFecha.MoveFocus(tRequest);
            MostrarMensajeFechaInicialMayorFechaActual();
            e.Handled = true;
        }

        /// <summary>
        /// Método para validar que la fecha inicial no sea mayor a la actual.
        /// </summary>
        private bool ValidaFechaInicial()
        {
            bool result = true;
            DateTime? fecha = dtpFecha.SelectedDate.HasValue
                                  ? dtpFecha.SelectedDate
                                  : null;
            //var fecha = new DateTime(fecha.Value.Date);
            if (fecha != null && fecha.Value.Date > DateTime.Today)
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Método para indicarle al usuario que capture una fecha válida.
        /// </summary>
        private void MostrarMensajeFechaInicialMayorFechaActual()
        {
            string mensaje = Properties.Resources.EvaluacionPartida_MsgFechaEvaluacionMayorFechaActual;

            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                              MessageBoxButton.OK, MessageImage.Warning);
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaEvaluacion(int inicio, int limite)
        {
            try
            {
                var evaluacionPL = new EvaluacionCorralPL();
                EvaluacionCorralInfo filtros = ObtenerFiltros();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<EvaluacionCorralInfo> resultadoInfo = evaluacionPL.ObtenerPorPagina(pagina, filtros);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    ucPaginacion.AsignarValoresIniciales();
                    gridDatos.ItemsSource = new List<EvaluacionCorralInfo>();

                    string mensaje = Properties.Resources.EvaluacionPartida_MsgNoHayRegistros;

                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                                      MessageBoxButton.OK, MessageImage.Warning);
                    dtpFecha.Focus();
                }
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private EvaluacionCorralInfo ObtenerFiltros()
        {
            EvaluacionCorralInfo filtro = null;
            try
            {
                DateTime fechaEvaluacion = Convert.ToDateTime(dtpFecha.SelectedDate);
                filtro = new EvaluacionCorralInfo
                {
                    FechaEvaluacion = fechaEvaluacion,
                    Organizacion = new OrganizacionInfo()
                    {
                        OrganizacionID = organizacionID
                    },
                    /*Activo = (bool)EstatusEnum.Activo*/
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                //throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return filtro;
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = ValidaFechaInicial();

            if (!isValid)
            {
                MostrarMensajeFechaInicialMayorFechaActual();
                return;
            }
            ucPaginacion.AsignarValoresIniciales();
            Buscar();
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {

            ucPaginacion.AsignarValoresIniciales();
            dtpFecha.SelectedDate = DateTime.Now;
            Buscar();

        }


        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)e.Source;
            var evaluacionCorralInfoSelecionado = (EvaluacionCorralInfo)btn.CommandParameter;

            try
            {
                if (evaluacionCorralInfoSelecionado != null)
                {
                    var evaluacionCorralPL = new EvaluacionCorralPL();
                    evaluacionCorralPL.ImprimirEvaluacionPartida(evaluacionCorralInfoSelecionado,false);
                }
            }
            catch (ExcepcionDesconocida ex)
            {
                Logger.Error(ex);
                //Favor de seleccionar una organizacion
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    (ex.InnerException == null ? ex.Message : ex.InnerException.Message),
                    MessageBoxButton.OK,
                    MessageImage.Stop);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    (ex.InnerException == null ? ex.Message : ex.InnerException.Message),
                    MessageBoxButton.OK,
                    MessageImage.Stop);
            }
        }
    }
}
