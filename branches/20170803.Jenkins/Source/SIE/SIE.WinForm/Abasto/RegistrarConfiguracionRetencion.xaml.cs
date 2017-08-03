using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Abasto
{
    public partial class RegistrarConfiguracionRetencion
    {
       #region CONSTRUCTORES
        public RegistrarConfiguracionRetencion()
        {
            try
            {
                InitializeComponent();
                InicializaContexto();
                CargarComboEstatus();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ConfigurarRetencion_ErrorInicial, MessageBoxButton.OK, MessageImage.Error);
            }
        }
       #endregion CONSTRUCTORES

       #region PROPIEDADES
        private ConfiguracionCreditoInfo ConfiguracionCreditoInfo
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ConfiguracionCreditoInfo)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }
       #endregion PROPIEDADES

       #region VARIABLES

       #endregion VARIABLES

       #region METODOS

        private void InicializaContexto()
        {
            ConfiguracionCreditoInfo = new ConfiguracionCreditoInfo
            {
                Activo = EstatusEnum.Activo,
                TipoCredito = new TipoCreditoInfo { Descripcion = string.Empty, TipoCreditoID = 0 },
                PlazoCredito = new PlazoCreditoInfo { Descripcion = string.Empty, PlazoCreditoID = 0}
            };
        }

        public void Buscar()
        {
            ObtenerListaConfiguracionCredito(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        private void CargarComboEstatus()
        {
            IList<EstatusEnum> estatusEnums = Enum.GetValues(typeof(EstatusEnum)).Cast<EstatusEnum>().ToList();
            cmbEstatus.ItemsSource = estatusEnums;
            cmbEstatus.SelectedItem = EstatusEnum.Activo;
        }

        private void ObtenerListaConfiguracionCredito(int inicio, int limite)
        {
            try
            {
                if (ucPaginacion.ContextoAnterior != null)
                {
                    bool contextosIguales = ucPaginacion.CompararObjetos(ConfiguracionCreditoInfo, ucPaginacion.ContextoAnterior);
                    if (!contextosIguales)
                    {
                        ucPaginacion.Inicio = 1;
                        inicio = 1;
                    }
                }
                var pl = new ConfiguracionCreditoPL();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                var resultadoInfo = pl.ConfiguracionCredito_ObtenerConfiguracionCreditoPorFiltro(pagina, ConfiguracionCreditoInfo);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridDatos.ItemsSource = new List<ConfiguracionCreditoInfo>();
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ConfigurarRetencion_SinInformacion, MessageBoxButton.OK, MessageImage.Warning);
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ConfigurarRetencion_ErrorConsultar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ConfigurarRetencion_ErrorConsultar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void AvanzarSiguienteControl(KeyEventArgs e)
        {
            var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
            var keyboardFocus = Keyboard.FocusedElement as UIElement;

            if (keyboardFocus != null)
            {
                keyboardFocus.MoveFocus(tRequest);
            }
            e.Handled = true;
        }

        private void ReiniciarValoresPaginador()
        {
            ucPaginacion.Inicio = 1;
        }

        #endregion METODOS

       #region EVENTOS
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ucPaginacion.DatosDelegado += ObtenerListaConfiguracionCredito;
                ucPaginacion.AsignarValoresIniciales();
                ucPaginacion.Contexto = ConfiguracionCreditoInfo;
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ConfigurarRetencion_ErrorInicial, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AvanzarSiguienteControl(e);
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ConfigurarRetencion_ErrorConsultar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var edicion =
                    new RegistrarConfiguracionRetencionEdicion
                    {
                        ucTitulo = { TextoTitulo = Properties.Resources.ConfigurarRetencion_Nuevo_Titulo }
                    };
                MostrarCentrado(edicion);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ConfigurarRetencion_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void BotonEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var btn = (Button)e.Source;
                var editar = (ConfiguracionCreditoInfo)btn.CommandParameter;
                var edicion =
                    new RegistrarConfiguracionRetencionEdicion(editar)
                    {
                        ucTitulo = { TextoTitulo = Properties.Resources.ConfigurarRetencion_Edicion_Titulo }
                    };
                MostrarCentrado(edicion);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.ConfigurarRetencion_ErrorModificacion, MessageBoxButton.OK, MessageImage.Error);
            }
        }

    #endregion EVENTOS
    }
}
