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
    public partial class MesesPorVencer
    {
       #region CONSTRUCTORES
       public MesesPorVencer()
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
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.MesesPorVencer_ErrorInicial, MessageBoxButton.OK, MessageImage.Error);
            }
        }
       #endregion CONSTRUCTORES

       #region PROPIEDADES
       private PlazoCreditoInfo PlazoCreditoInfo
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (PlazoCreditoInfo)DataContext;
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
            PlazoCreditoInfo = new PlazoCreditoInfo
            {
                Activo = EstatusEnum.Activo,
                Descripcion= string.Empty
            };
        }

        public void Buscar()
        {
            ObtenerListaPlazos(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        private void CargarComboEstatus()
        {
            IList<EstatusEnum> estatusEnums = Enum.GetValues(typeof(EstatusEnum)).Cast<EstatusEnum>().ToList();
            cmbEstatus.ItemsSource = estatusEnums;
            cmbEstatus.SelectedItem = EstatusEnum.Activo;
        }

        private void ObtenerListaPlazos(int inicio, int limite)
        {
            try
            {
                if (ucPaginacion.ContextoAnterior != null)
                {
                    bool contextosIguales = ucPaginacion.CompararObjetos(PlazoCreditoInfo, ucPaginacion.ContextoAnterior);
                    if (!contextosIguales)
                    {
                        ucPaginacion.Inicio = 1;
                        inicio = 1;
                    }
                }
                var pl = new PlazoCreditoPL();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                var resultadoInfo = pl.PlazoCredito_ObtenerPlazosCreditoPorFiltro(pagina, PlazoCreditoInfo);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridDatos.ItemsSource = new List<PlazoCreditoInfo>();
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.MesesPorVencer_SinInformacion, MessageBoxButton.OK, MessageImage.Warning);
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.MesesPorVencer_ErrorConsultar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.MesesPorVencer_ErrorConsultar, MessageBoxButton.OK, MessageImage.Error);
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
                ucPaginacion.DatosDelegado += ObtenerListaPlazos;
                ucPaginacion.AsignarValoresIniciales();
                ucPaginacion.Contexto = PlazoCreditoInfo;
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.MesesPorVencer_ErrorInicial, MessageBoxButton.OK, MessageImage.Error);
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
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.MesesPorVencer_ErrorConsultar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var edicion =
                    new MesesPorVencerEdicion
                    {
                        ucTitulo = { TextoTitulo = Properties.Resources.MesesPorVencer_Nuevo_Titulo }
                    };
                MostrarCentrado(edicion);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.MesesPorVencer_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void BotonEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var btn = (Button)e.Source;
                var editar = (PlazoCreditoInfo)btn.CommandParameter;
                var edicion =
                    new MesesPorVencerEdicion(editar)
                    {
                        ucTitulo = { TextoTitulo = Properties.Resources.MesesPorVencer_Edicion_Titulo }
                    };
                MostrarCentrado(edicion);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.MesesPorVencer_ErrorModificacion, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion EVENTOS
    }
}
