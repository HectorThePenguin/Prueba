using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using System.Linq;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for CentroCostoEdicion.xaml
    /// </summary>
    public partial class CentroCostoEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private CentroCostoInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (CentroCostoInfo)DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        #endregion Propiedades

        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public CentroCostoEdicion()
        {
            InitializeComponent();
            InicializaContexto();
        }

        /// <summary>
        /// Constructor para editar una entidad CentroCosto Existente
        /// </summary>
        /// <param name="centroCostoInfo"></param>
        public CentroCostoEdicion(CentroCostoInfo centroCostoInfo)
        {
            InitializeComponent();
            centroCostoInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto = centroCostoInfo;
        }

        #endregion Constructores

        #region Eventos
        /// <summary>
        /// Evento de Carga de la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var centroCostoUsuarioBL = new CentroCostoUsuarioBL();
            var lista = centroCostoUsuarioBL.ObtenerPorCentroCostoID(Contexto.CentroCostoID);
            if (lista != null && lista.Any())
            {
                Contexto.ListaCentroCostoUsuario = lista;
                gridDatosUsuario.ItemsSource = lista;
            }
            txtDescripcion.Focus();
        }

        /// <summary>
        /// Evento que se ejecuta mientras se esta cerrando la ventana
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (confirmaSalir)
            {
                MessageBoxResult result = SkMessageBox.Show(this, Properties.Resources.Msg_CerrarSinGuardar, MessageBoxButton.YesNo,
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
        /// Evento que guarda los datos de la entidad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        /// <summary>
        /// Evento para cerrar la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new CentroCostoInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
            };
        }

        /// <summary>
        /// Metodo que valida los datos para guardar
        /// </summary>
        /// <returns></returns>
        private bool ValidaGuardar()
        {
            bool resultado = true;
            string mensaje = string.Empty;
            try
            {
                if (string.IsNullOrWhiteSpace(txtDescripcion.Text) || Contexto.Descripcion == string.Empty)
                {
                    resultado = false;
                    mensaje = Properties.Resources.CentroCostoEdicion_MsgDescripcionRequerida;
                    txtDescripcion.Focus();
                }
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            if (!string.IsNullOrWhiteSpace(mensaje))
            {
                SkMessageBox.Show(this, mensaje, MessageBoxButton.OK, MessageImage.Warning);
            }
            return resultado;
        }


        /// <summary>
        /// Método para guardar los valores del contexto
        /// </summary>
        private void Guardar()
        {
            bool guardar = ValidaGuardar();

            if (guardar)
            {
                try
                {
                    var centroCostoBL = new CentroCostoUsuarioBL();
                    Contexto.ListaCentroCostoUsuario.ForEach(centro =>
                        {
                            centro.UsuarioID = centro.Usuario.UsuarioID;
                            centro.CentroCostoID = Contexto.CentroCostoID;
                        });
                    centroCostoBL.GuardarCentroCostoUsuarioLista(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.CentroCostoID != 0)
                    {
                        confirmaSalir = false;
                        Close();
                    }
                    else
                    {
                        InicializaContexto();
                    }
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.CentroCosto_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.CentroCosto_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        #endregion Métodos

        private void BotonNuevoUsuario_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var centroCostoUsuario = new CentroCostoUsuarioInfo
                {
                   Usuario = new UsuarioInfo()
                };
                var centroCostoEdicionUsuario =
                    new CentroCostoEdicionUsuario(centroCostoUsuario)
                    {
                        ucTitulo = { TextoTitulo = Properties.Resources.CentroCostoUsuario_Nuevo }
                    };
                MostrarCentrado(centroCostoEdicionUsuario);

                if (centroCostoEdicionUsuario.ConfirmaSalir)
                {
                    return;
                }
                if (centroCostoUsuario.Usuario != null)
                {
                    var usuarioRepetido =
                        Contexto.ListaCentroCostoUsuario.FirstOrDefault(
                            pro => pro.Usuario.UsuarioID == centroCostoUsuario.Usuario.UsuarioID);
                    if (usuarioRepetido != null)
                    {
                        SkMessageBox.Show(this, Properties.Resources.CentroCostoEdicion_UsuarioRepetido, MessageBoxButton.OK, MessageImage.Warning);
                        return;
                    }
                    centroCostoUsuario.TieneCambios = true;
                    centroCostoUsuario.UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
                    Contexto.ListaCentroCostoUsuario.Add(centroCostoUsuario);
                    gridDatosUsuario.ItemsSource = null;
                    gridDatosUsuario.ItemsSource = Contexto.ListaCentroCostoUsuario;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.CentroCostoEdicion_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void BotonEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var btn = (Button)e.Source;
                var centroCostoUsuarioEditar = (CentroCostoUsuarioInfo)btn.CommandParameter;
                var centroCostoUsuarioOriginal = centroCostoUsuarioEditar.Clone();
                var centroEdicionUsuario =
                    new CentroCostoEdicionUsuario(centroCostoUsuarioEditar)
                    {
                        ucTitulo = { TextoTitulo = Properties.Resources.CentroCostoEdicionUsuario_Edicion }
                    };
                MostrarCentrado(centroEdicionUsuario);

                if (centroEdicionUsuario.ConfirmaSalir)
                {
                    var centroCostoUsuarioModificado =
                        Contexto.ListaCentroCostoUsuario.FirstOrDefault(
                            pro => pro.CentroCostoUsuarioID == centroCostoUsuarioOriginal.CentroCostoUsuarioID);
                    if (centroCostoUsuarioModificado == null)
                    {
                        return;
                    }
                    centroCostoUsuarioModificado.Activo = centroCostoUsuarioOriginal.Activo;
                    centroCostoUsuarioModificado.Autoriza = centroCostoUsuarioOriginal.Autoriza;
                    centroCostoUsuarioModificado.Usuario = centroCostoUsuarioOriginal.Usuario;
                    centroCostoUsuarioEditar.TieneCambios = false;
                    gridDatosUsuario.ItemsSource = null;
                    gridDatosUsuario.ItemsSource = Contexto.ListaCentroCostoUsuario;
                }
                else
                {
                    centroCostoUsuarioEditar.TieneCambios = true;
                    centroCostoUsuarioEditar.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.TratamiendoEdicion_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }
    }
}

