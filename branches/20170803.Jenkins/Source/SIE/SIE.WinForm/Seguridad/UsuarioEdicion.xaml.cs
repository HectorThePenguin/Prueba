using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.WinForm.Auxiliar;

namespace SIE.WinForm.Seguridad
{
    /// <summary>
    /// Lógica de interacción para UsuarioEdicion.xaml
    /// </summary>
    public partial class UsuarioEdicion
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
                return (UsuarioInfo) DataContext;
            }
            set { DataContext = value; }
        }

        #endregion PROPIEDADES

        #region VARIABLES

        private SKAyuda<OrganizacionInfo> skAyudaOrganizacion;

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        #endregion VARIABLES

        #region CONSTRUCTORES

        public UsuarioEdicion()
        {
            InitializeComponent();
            InicializaContexto();
            AgregarAyudaOrganizacion();
            LlenarGridGrupos();
        }

        public UsuarioEdicion(UsuarioInfo usuarioInfo)
        {
            InitializeComponent();            
            Contexto = usuarioInfo;
            AgregarAyudaOrganizacion();
            LlenarGridGrupos();
        }

        #endregion CONSTRUCTORES

        #region EVENTOS

        /// <summary>
        /// Evento de Carga de la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtNombre.Focus();
        }

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
            else if (e.Key == Key.Escape)
            {
                Close();
            }
        }

        /// <summary>
        /// Cancela los cambios realizados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancelar_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Genera un nuevo Usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Guardar_OnClick(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        /// <summary>
        /// Evento que se ejecuta mientras se esta cerrando la ventana
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (confirmaSalir)
            {
                MessageBoxResult result = SkMessageBox.Show(this, Properties.Resources.Msg_CerrarSinGuardar
                                                          , MessageBoxButton.YesNo,
                                                            MessageImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Contexto = null;
                }
                else
                {
                    e.Cancel = true;
                }
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

        /// <summary>
        /// Valida la entrada de texto permitiendo letras y puntos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidarNumerosLetrasConPunto(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumerosConPunto(e.Text);
        }

        #endregion EVENTOS

        #region METODOS

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new UsuarioInfo
                           {
                               Nombre = string.Empty,
                               Activo = EstatusEnum.Activo,
                               UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                               UsuarioGrupo = new List<UsuarioGrupoInfo>(), 
                               NivelAcceso = NivelAccesoEnum.Engorda
                           };
            LlenarGridGrupos();
        }

        /// <summary>
        /// Agrega control de ayuda para organizaciones
        /// </summary>
        private void AgregarAyudaOrganizacion()
        {
            skAyudaOrganizacion = new SKAyuda<OrganizacionInfo>(160, false, Contexto.Organizacion
                                                                , "PropiedadClaveCatalogoAyuda"
                                                                , "PropiedadDescripcionCatalogoAyuda", true, true)
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
        /// Guarda un Costo Organizacion
        /// </summary>
        private void Guardar()
        {
            try
            {
                bool guardar = ValidaGuardar();
                if (guardar)
                {
                    var usuarioPL = new UsuarioPL();
                    usuarioPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK,
                                      MessageImage.Correct);
                    if (Contexto.UsuarioID == 0)
                    {
                        InicializaContexto();
                        txtNombre.Focus();
                    }
                    else
                    {
                        confirmaSalir = false;
                        Close();
                    }
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(this, Properties.Resources.Usuario_ErrorGuardar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.Usuario_ErrorGuardar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        /// <summary>
        /// Valida que el Contexto tenga datos validos
        /// </summary>
        /// <returns></returns>
        private bool ValidaGuardar()
        {
            bool guardar = true;

            var mensaje = string.Empty;
            if (string.IsNullOrWhiteSpace(Contexto.Nombre))
            {
                guardar = false;
                txtNombre.Focus();
                mensaje = Properties.Resources.UsuarioEdicion_MsgNombreRequerida;
            }
            else
            {
                if (Contexto.Organizacion.OrganizacionID == 0)
                {
                    guardar = false;
                    skAyudaOrganizacion.AsignarFoco();
                    mensaje = Properties.Resources.UsuarioEdicion_MsgOrganizacionIDRequerida;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(Contexto.UsuarioActiveDirectory))
                    {
                        guardar = false;
                        txtNombreActiveDirectory.Focus();
                        mensaje = Properties.Resources.UsuarioEdicion_MsgUsuarioActiveDirectoryRequerida;    
                    }
                }
            }
            if (guardar)
            {
                var usuarioPL = new UsuarioPL();
                UsuarioInfo usuario =
                    usuarioPL.ObtenerPorActiveDirectory(Contexto.UsuarioActiveDirectory);
                if (usuario != null && usuario.UsuarioID != Contexto.UsuarioID)
                {
                    guardar = false;
                    mensaje = string.Format(Properties.Resources.UsuarioEdicion_MsgDescripcionExistente,
                                            usuario.UsuarioID);
                    txtNombreActiveDirectory.Focus();
                }
            }
            if (!string.IsNullOrWhiteSpace(mensaje))
            {
                SkMessageBox.Show(this, mensaje, MessageBoxButton.OK, MessageImage.Warning);
            }

            return guardar;
        }

        /// <summary>
        /// Método para llenar el grid de grupos
        /// </summary>
        private void LlenarGridGrupos()
        {
            var usuarioGrupoPL = new UsuarioGrupoPL();
            var resultadoInfo = usuarioGrupoPL.ObtenerPorUsuarioID(Contexto.UsuarioID);

            resultadoInfo = resultadoInfo ?? new List<UsuarioGrupoInfo>();
            gridGrupos.ItemsSource = resultadoInfo;
            Contexto.UsuarioGrupo = resultadoInfo;
        }

        #endregion METODOS
    }
}
