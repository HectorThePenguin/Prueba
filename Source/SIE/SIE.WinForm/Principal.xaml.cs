using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using AvalonDock.Layout;
using Microsoft.Windows.Controls.Ribbon;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles;
using SuKarne.Controls.MessageBox;
using System.Linq;

namespace SIE.WinForm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Principal
    {
        System.Timers.Timer tmr;
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Principal()
        {
            InitializeComponent();
            Title = string.Format(Properties.Resources.Principal_Titulo, AuxConfiguracion.ObtenerVersionAplicacion());
            tmr = new System.Timers.Timer(TimeSpan.FromMinutes(60).TotalMilliseconds);
            tmr.Elapsed += tmr_Elapsed;
            tmr.Start();
        }

        void tmr_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            tmr.Stop();

            InstallUpdateSyncWithInfo();
            //MessageBox.Show("Ejecutando mensaje");

            tmr.Start();
        }

        private void InstallUpdateSyncWithInfo()
        {
            Logger.Info();
            UpdateCheckInfo info = null;

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

                try
                {
                    info = ad.CheckForDetailedUpdate();
                }
                catch (DeploymentDownloadException dde)
                {
                    MessageBox.Show("Ocurrio un error al validar la versión del Sistema. Error: " + dde.Message);
                    return;
                }
                catch (InvalidDeploymentException ide)
                {
                    MessageBox.Show("Ocurrio un problema al validar la de ClickOnce. Error: " + ide.Message);
                    return;
                }
                catch (InvalidOperationException ioe)
                {
                    MessageBox.Show("Imposible actualizar la aplicación. Error: " + ioe.Message);
                    return;
                }

                if (info.UpdateAvailable)
                {
                    Boolean doUpdate = true;

                    MessageBoxResult respuesta = MessageBox.Show(
                        Properties.Resources.Principal_ActualizacionDisponible, "Actualización", MessageBoxButton.YesNo,
                        MessageBoxImage.Question);
                    if (respuesta == MessageBoxResult.No)
                    {
                        doUpdate = false;
                    }

                    //if (!info.IsUpdateRequired)
                    //{

                    //}
                    //else
                    //{
                    //    // Display a message that the app MUST reboot. Display the minimum required version.
                    //    MessageBox.Show("This application has detected a mandatory update from your current " +
                    //        "version to version " + info.MinimumRequiredVersion.ToString() +
                    //        ". The application will now install the update and restart.",
                    //        "Update Available", MessageBoxButtons.OK,
                    //        MessageBoxIcon.Information);
                    //}

                    if (doUpdate)
                    {
                        try
                        {
                            ad.Update();
                            var resultado = MessageBox.Show(Properties.Resources.Principal_ActualizadaExito);
                            if (resultado == MessageBoxResult.OK)
                            {
                                Environment.Exit(0);
                            }
                            //System.Windows.Forms.Application.Restart();
                        }
                        catch (DeploymentDownloadException dde)
                        {
                            MessageBox.Show(Properties.Resources.Principal_ErrorActualizar);
                            Logger.Error(dde);
                            //MessageBox.Show("Cannot install the latest version of the application. \n\nPlease check your network connection, or try again later. Error: " + dde);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Evento para abrir el formulario correspondiente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var menu = (MenuInfo)((RibbonButton)sender).Tag;
                string ensamblado = string.Format("{0}.{1}.{2}", ConstantesVista.AssemblyBase, menu.Control,
                                                  menu.WinForm);
                Type tipo = Type.GetType(ensamblado);
                if (tipo != null)
                {
                    var control = (UserControl)Activator.CreateInstance(tipo);
                    LayoutDocument layout = ObtenerLayout(control);
                    if (layout == null)
                    {
                        layout = new LayoutDocument { Title = menu.Formulario, Content = control, CanFloat = false };
                        Contenedor.Children.Add(layout);
                    }
                    Contenedor.SelectedContentIndex = Contenedor.IndexOf(layout);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar formulario" + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene si una pestaña si se encuentra abierta
        /// </summary>
        /// <param name="controlFormulario"></param>
        /// <returns></returns>
        private LayoutDocument ObtenerLayout(UserControl controlFormulario)
        {
            LayoutDocument layout = null;

            foreach (LayoutDocument layoutExistente in Contenedor.Children)
            {
                if (layoutExistente.Content.GetType() == controlFormulario.GetType())
                {
                    layout = layoutExistente;
                    break;
                }
            }
            return layout;
        }

        /// <summary>
        /// Evento de carga de Forma 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                CargarMenu();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show("Ocurrio un error al cargar el menu");
            }
        }

        /// <summary>
        /// Metodo que carga el menu al que tiene permisos el usuario
        /// </summary>
        private void CargarMenu()
        {
            try
            {
                string usuario = Application.Current.Properties["UsuarioActiveDirectory"].ToString();
                IList<MenuInfo> menuLista = ObtenerOpciones(usuario);
                if (menuLista != null && menuLista.Count > 0)
                {
                    AsignaOpciones(menuLista);
                }
                else
                {
                    SkMessageBox.Show("El usuario no tiene asignado ningun grupo.", MessageBoxButton.OK);
                    Close();
                }
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo que muestras las opciones a las que tiene acceso el usuario
        /// </summary>
        /// <param name="menuLista"></param>
        private void AsignaOpciones(IList<MenuInfo> menuLista)
        {
            try
            {
                int moduloActual = 0;
                int? padreActual = null;
                RibbonTab tabMenu = new RibbonTab();
                string ensamblado = ObtenerEnsamblado();
                Uri ruta = null;
                RibbonGroup grupoMenu = null;

                menuLista.ToList().ForEach(menu =>
                    {
                        if (menu.PadreID == null && string.IsNullOrWhiteSpace(menu.Padre))
                        {
                            menu.PadreID = 0;
                            menu.Padre = "Seleccione";
                        }
                    });

                foreach (MenuInfo menuOpcion in menuLista.OrderBy(menu => menu.OrdenModulo).ThenBy(menu=> menu.PadreID))
                {
                    if (moduloActual != menuOpcion.ModuloID)
                    {
                        padreActual = null;
                        moduloActual = menuOpcion.ModuloID;
                        tabMenu = new RibbonTab { Header = menuOpcion.Modulo };
                        rbMenu.Items.Add(tabMenu);
                    }
                    if (padreActual != menuOpcion.PadreID)
                    {
                        if (menuOpcion.PadreID != null && padreActual != menuOpcion.PadreID)
                        {
                            padreActual = menuOpcion.PadreID;
                            grupoMenu = new RibbonGroup { Header = menuOpcion.Padre };
                            if (padreActual > 0)
                            {
                                grupoMenu.GroupSizeDefinitions.Add(new RibbonGroupSizeDefinition {IsCollapsed = true});
                            }

                            ruta = new Uri(string.Format("/{1};component/Imagenes/{0}", menuOpcion.ImagenPadre, ensamblado),
                                           UriKind.Relative);
                            var imagen = new BitmapImage(ruta);
                            grupoMenu.LargeImageSource = imagen;
                            //grupoMenu.SmallImageSource = imagen;

                            tabMenu.Items.Add(grupoMenu);
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(tabMenu.Header.ToString()))
                    {
                        var boton = new RibbonButton { Label = menuOpcion.Formulario, Tag = menuOpcion };
                        boton.Width = double.NaN;
                        boton.Click += button_Click;
                        ruta = new Uri(string.Format("/{1};component/Imagenes/{0}", menuOpcion.Imagen, ensamblado),
                                           UriKind.Relative);
                        var imagen = new BitmapImage(ruta);
                        if (padreActual > 0)
                        {
                            boton.SmallImageSource = imagen;
                        }
                        else
                        {
                            boton.LargeImageSource = imagen;
                        }
                        if (grupoMenu != null)
                        {
                            grupoMenu.Items.Add(boton);
                        }
                    
                        //var style = (Style) Application.Current.FindResource("botonMenu");

                        //boton.Style = style;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private string ObtenerEnsamblado()
        {
            return Assembly.GetExecutingAssembly().FullName.Split(',')[0];
        }

        /// <summary>
        /// Metodo que obtiene las opciones que tiene permisos el usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        private IList<MenuInfo> ObtenerOpciones(string usuario)
        {
            IList<MenuInfo> menuLista;
            try
            {
                var menu = new MenuPL();
                menuLista = menu.ObtenerPorUsuario(usuario, false);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return menuLista;
        }

        private void rbMenu_Loaded(object sender, RoutedEventArgs e)
        {
            var child = VisualTreeHelper.GetChild((DependencyObject)sender, 0) as Grid;
            if (child != null)
            {
                child.RowDefinitions[0].Height = new GridLength(0);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}