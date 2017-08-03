using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for AlmacenUsuarioEdicion.xaml
    /// </summary>
    public partial class AlmacenUsuarioEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private AlmacenInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (AlmacenInfo)DataContext;
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
        /// Constructor para editar una entidad AlmacenUsuario Existente
        /// </summary>
        /// <param name="almacenInfo"></param>
        public AlmacenUsuarioEdicion(AlmacenInfo almacenInfo)
        {
           InitializeComponent();
           almacenInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
           Contexto = almacenInfo;
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
            var almacenUsuarioBL = new AlmacenUsuarioBL();
            List<AlmacenUsuarioInfo> listaUsuarios = almacenUsuarioBL.ObtenerPorAlmacenID(Contexto);
            if(listaUsuarios != null && listaUsuarios.Any())
            {
                Contexto.ListaAlmacenUsuario = listaUsuarios;
                gridDatosUsuario.ItemsSource = Contexto.ListaAlmacenUsuario;
            }
            else
            {
                Contexto.ListaAlmacenUsuario = new List<AlmacenUsuarioInfo>();
            }
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

        private void BotonEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var btn = (Button)e.Source;
                var usuarioEditar = (AlmacenUsuarioInfo)btn.CommandParameter;
                var usuarioOriginal = usuarioEditar.Clone();
                var almacenUsuarioEdicionUsuario =
                    new AlmacenUsuarioEdicionUsuario(usuarioEditar)
                    {
                        ucTitulo = { TextoTitulo = Properties.Resources.CentroCostoEdicionUsuario_Edicion }
                    };
                MostrarCentrado(almacenUsuarioEdicionUsuario);

                if (almacenUsuarioEdicionUsuario.ConfirmaSalir)
                {
                    var usuarioModificado =
                        Contexto.ListaAlmacenUsuario.FirstOrDefault(
                            pro => pro.AlmacenUsuarioID == usuarioOriginal.AlmacenUsuarioID);
                    if (usuarioModificado == null)
                    {
                        return;
                    }
                    usuarioModificado.Activo = usuarioOriginal.Activo;
                    //usuarioModificado.Autoriza = usuarioOriginal.Autoriza;
                    //usuarioModificado.Usuario = usuarioOriginal.Usuario;
                    usuarioEditar.TieneCambios = false;
                    gridDatosUsuario.ItemsSource = null;
                    gridDatosUsuario.ItemsSource = Contexto.ListaAlmacenUsuario;
                }
                else
                {
                    usuarioEditar.TieneCambios = true;
                    usuarioEditar.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.TratamiendoEdicion_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void BotonNuevoUsuario_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var almacenUsuarioInfo = new AlmacenUsuarioInfo
                {
                    Usuario = new UsuarioInfo()
                };

                var almacenUsuarioEdicionUsuario =
                    new AlmacenUsuarioEdicionUsuario(almacenUsuarioInfo)
                    {
                        ucTitulo = { TextoTitulo = Properties.Resources.CentroCostoUsuario_Nuevo }
                    };
                MostrarCentrado(almacenUsuarioEdicionUsuario);

                if (almacenUsuarioEdicionUsuario.ConfirmaSalir)
                {
                    return;
                }
                if (almacenUsuarioInfo.Usuario != null && almacenUsuarioInfo.Usuario.UsuarioID != 0)
                {
                    var usuarioRepetido =
                        Contexto.ListaAlmacenUsuario.FirstOrDefault(
                            pro => pro.Usuario.UsuarioID == almacenUsuarioInfo.Usuario.UsuarioID);
                    if (usuarioRepetido != null)
                    {
                        SkMessageBox.Show(this, Properties.Resources.CentroCostoEdicion_UsuarioRepetido, MessageBoxButton.OK, MessageImage.Warning);
                        return;
                    }
                    almacenUsuarioInfo.TieneCambios = true;
                    almacenUsuarioInfo.UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
                    Contexto.ListaAlmacenUsuario.Add(almacenUsuarioInfo);
                    gridDatosUsuario.ItemsSource = null;
                    gridDatosUsuario.ItemsSource = Contexto.ListaAlmacenUsuario;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.CentroCostoEdicion_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new AlmacenInfo
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
                if (Contexto.ListaAlmacenUsuario == null || !Contexto.ListaAlmacenUsuario.Any())
                {
                    resultado = false;
                    mensaje = Properties.Resources.AlmacenUsuarioEdicion_MsgAlmacenUsuarioLista;
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
                    var almacenUsuarioBL = new AlmacenUsuarioBL();
                    Contexto.ListaAlmacenUsuario.ForEach(alm =>
                        {
                            alm.Almacen = Contexto;
                            if(alm.AlmacenUsuarioID == 0)
                            {
                                alm.UsuarioCreacionID = Contexto.UsuarioModificacionID.HasValue
                                                            ? Contexto.UsuarioModificacionID.Value
                                                            : 0;
                            }
                            else
                            {
                                alm.UsuarioModificacionID = Contexto.UsuarioModificacionID.HasValue
                                                                ? Contexto.UsuarioModificacionID.Value
                                                                : 0;
                            }
                        });
                    almacenUsuarioBL.GuardarXML(Contexto.ListaAlmacenUsuario);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.AlmacenID != 0)
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
                    SkMessageBox.Show(this, Properties.Resources.AlmacenUsuario_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this,Properties.Resources.AlmacenUsuario_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        #endregion Métodos

        
    }
}

