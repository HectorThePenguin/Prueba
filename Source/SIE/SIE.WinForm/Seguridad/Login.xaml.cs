using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using System.Xml.Linq;
using Application = System.Windows.Application;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace SIE.WinForm.Seguridad
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login
    {
        public Login()
        {
            try
            {
                InitializeComponent();
                CargarConfiguracion();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, "Error al Cargar la Configuración", MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Metodo para cargar la configuracion del Sistema SIAP
        /// </summary>
        private static void CargarConfiguracion()
        {
            var rutaEjecutable = System.Reflection.Assembly.GetExecutingAssembly().Location.Substring(0, System.Reflection.Assembly.GetExecutingAssembly().Location.LastIndexOf("\\", StringComparison.Ordinal));
            var sourcePath = String.Format("{0}{1}", rutaEjecutable, ConfigurationManager.AppSettings["CarpetaConfiguracion"]);
            var targetPath = ConfigurationManager.AppSettings["RutaConfiguracion"];
            CopiarArchivos(sourcePath, targetPath);
            var nombreArchivo = ConfigurationManager.AppSettings["ArchivoConfiguracion"];
            var archivoConfiguracion = string.Format("{0}{1}", targetPath, nombreArchivo);

            CompararArchivosConfiguracion(sourcePath, targetPath, nombreArchivo);

            XDocument configuracion = XDocument.Load(archivoConfiguracion);

            if (configuracion.Root == null)
            {
                return;
            }

            foreach (var atributos in configuracion.Root.Elements().SelectMany(elemento => elemento.Elements()))
            {
                Application.Current.Properties[atributos.Name.LocalName] = atributos.Value;
            }
        }

        private static void CompararArchivosConfiguracion(string sourcePath, string targetPath, string nombreArchivo)
        {
            var rutaArchivoLocal = string.Format("{0}{1}", targetPath, nombreArchivo);
            var rutaArchivoSistema = string.Format("{0}{1}", sourcePath, nombreArchivo);

            var configuracionLocal = XDocument.Load(rutaArchivoLocal);
            var configuracionSistema = XDocument.Load(rutaArchivoSistema);

            if (configuracionLocal.Root == null || configuracionSistema.Root == null)
            {
                return;
            }
            var nodoConfiguracion = configuracionLocal.Root.Elements().FirstOrDefault();
            if (nodoConfiguracion != null)
            {
                var configuracion = nodoConfiguracion.Elements();
                foreach (var configSistema in configuracionSistema.Root.Elements().SelectMany(config => config.Elements()))
                {
                    if (configuracion.Any(config => config.Name.Equals(configSistema.Name)))
                    {
                        continue;
                    }
                    nodoConfiguracion.Add(configSistema);
                }
            }
            configuracionLocal.Save(rutaArchivoLocal);
        }

        private static void CopiarArchivos(string sourcePath, string targetPath)
        {
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }

            if (!Directory.Exists(sourcePath))
            {
                return;
            }
            var files = Directory.GetFiles(sourcePath);

            // Copy the files and overwrite destination files if they already exist.
            foreach (var s in files)
            {
                // Use static Path methods to extract only the file name from the path.
                string archivoSource = Path.GetFileName(s);
                if (archivoSource != null)
                {
                    var archivoTarget = Path.Combine(targetPath, archivoSource);
                    if (!File.Exists(archivoTarget))
                    {
                        File.Copy(s, archivoTarget, true);
                    }
                }
            }
        }

        /// <summary>
        /// Evento para entrar al menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            btnAceptar.IsEnabled = false;
            imgloading.Visibility = Visibility.Visible;
            Dispatcher.BeginInvoke((Action)(IniciarSesion), DispatcherPriority.Background);
            //new Thread(Work).Start();
            //new Thread(Work).Start();
        }

        private void IniciarSesion()
        {
            try
            {
                //btnAceptar.IsEnabled = false;
                if (ValidarCampos())
                {
                    if (ValidaUsuario())
                    {
                        var usuarioPL = new UsuarioPL();
                        UsuarioInfo usuarioInfo = usuarioPL.ObtenerPorActiveDirectory(txtUsuario.Text.Trim());

                        if (usuarioInfo != null)
                        {
                            Application.Current.Properties["UsuarioActiveDirectory"] = txtUsuario.Text.Trim();
                            Application.Current.Properties["UsuarioID"] = usuarioInfo.UsuarioID;
                            Application.Current.Properties["Nombre"] = usuarioInfo.Nombre;
                            Application.Current.Properties["OrganizacionID"] = usuarioInfo.Organizacion.OrganizacionID;
                            Application.Current.Properties["Corporativo"] = usuarioInfo.Corporativo;

                            var formaPrincipal = new Principal();
                            Hide();
                            formaPrincipal.Show();
                        }
                        else
                        {
                            SkMessageBox.Show(this, Properties.Resources.Login_ErrorUsuarioLocal, MessageBoxButton.OK, MessageImage.Error);
                            btnAceptar.IsEnabled = true;
                            imgloading.Visibility = Visibility.Hidden;
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(this, Properties.Resources.Login_Error, MessageBoxButton.OK, MessageImage.Error);
                        txtUsuario.Focus();
                        btnAceptar.IsEnabled = true;
                        imgloading.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    SkMessageBox.Show(this, Properties.Resources.Login_ErrorCampos, MessageBoxButton.OK, MessageImage.Error);
                    txtUsuario.Focus();
                    btnAceptar.IsEnabled = true;
                    imgloading.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.Login_ErrorValidarUsuario, MessageBoxButton.OK,
                                  MessageImage.Error);
                btnAceptar.IsEnabled = true;
                imgloading.Visibility = Visibility.Hidden;
            }

        }

        /// <summary>
        /// Metodo que valida el usuario en el  active directory 
        /// </summary>
        private bool ValidaUsuario()
        {
            bool esValido = false;
            var usuarioPL = new UsuarioPL();
            ConfiguracionInfo configuracion = AuxConfiguracion.ObtenerConfiguracion();
            UsuarioInfo usuario = usuarioPL.ValidarUsuario(txtUsuario.Text, txtContra.Password, configuracion);

            if (usuario != null)
            {
                esValido = true;
            }
            return esValido;
        }

        /// <summary>
        /// Evento para cerrar la pantalla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Evento cargar forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUsuario.Focus();
        }

        /// <summary>
        /// Metodo que valida campos vacios 
        /// </summary>
        /// <returns></returns>
        private bool ValidarCampos()
        {
            return !(txtUsuario.Text.Trim() == string.Empty || txtContra.Password.Trim() == string.Empty);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void TxtContra_OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                btnAceptar.IsEnabled = false;
                imgloading.Visibility = Visibility.Visible;
                Dispatcher.BeginInvoke((Action)(IniciarSesion), DispatcherPriority.Background);
            }
        }
    }
}
