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
    public partial class TipoCredito
    {
       #region CONSTRUCTORES
        public TipoCredito()
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
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.TipoCredito_ErrorInicial, MessageBoxButton.OK, MessageImage.Error);
            }
        }
       #endregion CONSTRUCTORES

       #region PROPIEDADES
        private TipoCreditoInfo TipoCreditoInfo
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (TipoCreditoInfo)DataContext;
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
            TipoCreditoInfo = new TipoCreditoInfo
            {
                Activo = EstatusEnum.Activo,
                Descripcion= string.Empty
            };
        }

        public void Buscar()
        {
            ObtenerListaTiposCredito(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        private void CargarComboEstatus()
        {
            IList<EstatusEnum> estatusEnums = Enum.GetValues(typeof(EstatusEnum)).Cast<EstatusEnum>().ToList();
            cmbEstatus.ItemsSource = estatusEnums;
            cmbEstatus.SelectedItem = EstatusEnum.Activo;
        }

        private void ObtenerListaTiposCredito(int inicio, int limite)
        {
            try
            {
                if (ucPaginacion.ContextoAnterior != null)
                {
                    bool contextosIguales = ucPaginacion.CompararObjetos(TipoCreditoInfo, ucPaginacion.ContextoAnterior);
                    if (!contextosIguales)
                    {
                        ucPaginacion.Inicio = 1;
                        inicio = 1;
                    }
                }
                var pl = new TipoCreditoPL();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                var resultadoInfo = pl.TipoCredito_ObtenerPlazosCreditoPorFiltro(pagina, TipoCreditoInfo);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacion.TotalRegistros = 0;
                    gridDatos.ItemsSource = new List<TipoCreditoInfo>();
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.TipoCredito_SinInformacion, MessageBoxButton.OK, MessageImage.Warning);
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.TipoCredito_ErrorConsultar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.TipoCredito_ErrorConsultar, MessageBoxButton.OK, MessageImage.Error);
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
                ucPaginacion.DatosDelegado += ObtenerListaTiposCredito;
                ucPaginacion.AsignarValoresIniciales();
                ucPaginacion.Contexto = TipoCreditoInfo;
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.TipoCredito_ErrorInicial, MessageBoxButton.OK, MessageImage.Error);
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
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.TipoCredito_ErrorConsultar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var edicion =
                    new TipoCreditoEdicion
                    {
                        ucTitulo = { TextoTitulo = Properties.Resources.TipoCredito_Nuevo_Titulo }
                    };
                MostrarCentrado(edicion);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.TipoCredito_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void BotonEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var btn = (Button)e.Source;
                var editar = (TipoCreditoInfo)btn.CommandParameter;
                var edicion =
                    new TipoCreditoEdicion(editar)
                    {
                        ucTitulo = { TextoTitulo = Properties.Resources.TipoCredito_Edicion_Titulo }
                    };
                MostrarCentrado(edicion);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.TipoCredito_ErrorModificacion, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion EVENTOS
    }
}

