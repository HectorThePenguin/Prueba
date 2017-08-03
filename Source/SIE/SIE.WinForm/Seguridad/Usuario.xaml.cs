using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Seguridad
{
    /// <summary>
    /// Lógica de interacción para Usuario.xaml
    /// </summary>
    public partial class Usuario
    {
        #region PROPIEDADES

        private UsuarioInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (UsuarioInfo)DataContext;
            }
            set { DataContext = value; }
        }

        #endregion PROPIEADES

        #region VARIABLES

        private SKAyuda<OrganizacionInfo> skAyudaOrganizacion;

        #endregion VARIABLES
        
        #region CONSTRUCTORES

        public Usuario()
        {
            InitializeComponent();
            InicializaContexto();
            CargarCboEstatus();
            AgregarAyudaOrganizacion();
        }

        #endregion CONSTRUCTORES

        #region EVENTOS

        /// <summary>
        /// evento cuando se preciona cualquier tecla en el formulario 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AvanzarSiguienteControl(e);
            }
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
                ucPaginacion.DatosDelegado += ObtenerUsuarios;
                ucPaginacion.AsignarValoresIniciales();
                ucPaginacion.Contexto = Contexto;
                Buscar();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Usuario_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Usuario_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Se ejecuta al presionar el boton Buscar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBuscar_OnClick(object sender, RoutedEventArgs e)
        {
            Buscar();
        }

        /// <summary>
        /// Llama la ventana para generar un nuevo usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNuevo_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var usuarioInfo = new UsuarioInfo
                {
                    UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                    Activo = EstatusEnum.Activo,
                };
                var usuarioEdicion = new UsuarioEdicion(usuarioInfo)
                {
                    ucTitulo = { TextoTitulo = Properties.Resources.Usuario_Nuevo_Titulo }
                };
                usuarioEdicion.Left = (ActualWidth - usuarioEdicion.Width) / 2;
                usuarioEdicion.Top = ((ActualHeight - usuarioEdicion.Height) / 2) + 132;
                usuarioEdicion.Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal];
                usuarioEdicion.ShowDialog();
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Usuario_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Llama la ventana de edicion de Usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Editar_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = e.Source as Button;
            try
            {
                var usuarioSelecionado = btn.CommandParameter as UsuarioInfo;
                if (usuarioSelecionado != null)
                {
                    var usuarioInfo = Extensor.ClonarInfo(usuarioSelecionado) as UsuarioInfo;
                    if (usuarioInfo != null)
                    {
                        usuarioInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();

                        var usuarioEdicion = new UsuarioEdicion(usuarioInfo)
                                                 {
                                                     ucTitulo =
                                                         {
                                                             TextoTitulo =
                                                                 Properties.Resources.
                                                                 Usuario_Editar_Titulo
                                                         }
                                                 };
                        usuarioEdicion.Left = (ActualWidth - usuarioEdicion.Width)/2;
                        usuarioEdicion.Top = ((ActualHeight - usuarioEdicion.Height)/2) + 132;
                        usuarioEdicion.Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal];
                        usuarioEdicion.ShowDialog();
                        ReiniciarValoresPaginador();
                        Buscar();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Usuario_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Valida que solo se puedan agregar letras
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidarLetrasConAncento(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarLetrasConAcentos(e.Text);
        }

        #endregion EVENTOS

        #region METODOS

        /// <summary>
        /// Inicializa el contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new UsuarioInfo{ Activo = EstatusEnum.Activo};
        }

        private void ReiniciarValoresPaginador()
        {
            ucPaginacion.Inicio = 1;
        }

        /// <summary>
        /// Agrega control de ayuda para organizaciones
        /// </summary>
        private void AgregarAyudaOrganizacion()
        {
            skAyudaOrganizacion = new SKAyuda<OrganizacionInfo>(160, false, Contexto.Organizacion
                                                                , "PropiedadClaveCatalogoAyuda"
                                                                , "PropiedadDescripcionCatalogoAyuda", true)
            {
                AyudaPL = new OrganizacionPL(),
                MensajeClaveInexistente = Properties.Resources.AyudaOrganizacion_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.AyudaOrganizacion_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.AyudaOrganizacion_Busqueda,
                MensajeAgregar = Properties.Resources.AyudaOrganizacion_Seleccionar,
                TituloEtiqueta = Properties.Resources.AyudaOrganizacion_LeyendaBusqueda,
                TituloPantalla = Properties.Resources.AyudaOrganizacion_Busqueda_Titulo,
            };
            skAyudaOrganizacion.AsignaTabIndex(1);
            skAyudaOrganizacion.IsTabStop = false;

            stpOrganizacion.Children.Clear();
            stpOrganizacion.Children.Add(skAyudaOrganizacion);
        }

        /// <summary>
        /// Avanza el foco al siguiente control
        /// </summary>
        /// <param name="e"></param>
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

        /// <summary>
        /// Buscar una lista de productos
        /// </summary>
        public void Buscar()
        {
            ObtenerUsuarios(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene una Lista de Costos por Organizacion
        /// </summary>
        /// <param name="inicio"></param>
        /// <param name="limite"></param>
        private void ObtenerUsuarios(int inicio, int limite)
        {
            try
            {
                if (ucPaginacion.ContextoAnterior != null)
                {
                    bool contextosIguales = ucPaginacion.CompararObjetos(Contexto, ucPaginacion.ContextoAnterior);
                    if (!contextosIguales)
                    {
                        ucPaginacion.Inicio = 1;
                        inicio = 1;
                    }
                }
                var costoOrganizacionPL = new UsuarioPL();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<UsuarioInfo> resultadoInfo = costoOrganizacionPL.ObtenerPorPagina(pagina, Contexto);
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
                    gridDatos.ItemsSource = new List<UsuarioInfo>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Usuario_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Usuario_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Carga los valores del combo de estatus
        /// </summary>
        private void CargarCboEstatus()
        {
            try
            {
                IList<EstatusEnum> estatusEnums = Enum.GetValues(typeof(EstatusEnum)).Cast<EstatusEnum>().ToList();
                cboEstatus.ItemsSource = estatusEnums;
                cboEstatus.SelectedItem = EstatusEnum.Activo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        #endregion METODOS
    }
}
