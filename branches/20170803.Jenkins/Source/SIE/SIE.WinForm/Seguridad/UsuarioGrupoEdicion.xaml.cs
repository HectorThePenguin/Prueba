using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Seguridad
{
    /// <summary>
    /// Interaction logic for UsuarioGrupoEdicion.xaml
    /// </summary>
    public partial class UsuarioGrupoEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
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

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        #endregion Propiedades

        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public UsuarioGrupoEdicion()
        {
           InitializeComponent();
           InicializaContexto();
            LlenarGridGrupos();
        }

        /// <summary>
        /// Constructor para editar una entidad UsuarioGrupo Existente
        /// </summary>
        /// <param name="usuarioGrupoInfo"></param>
        public UsuarioGrupoEdicion(UsuarioInfo usuarioGrupoInfo)
        {
            InitializeComponent();
            //usuarioGrupoInfo.UsuarioGrupo = Obtener
            //usuarioGrupoInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto = usuarioGrupoInfo;
            LlenarGridGrupos();
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
             //txtDescripcion.Focus();
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
            Contexto = new UsuarioInfo
            {
                UsuarioGrupo = new List<UsuarioGrupoInfo>(),
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
                //if (string.IsNullOrWhiteSpace(txtUsuarioGrupoID.Text) )
                //{
                //    resultado = false;
                //    mensaje = Properties.Resources.UsuarioGrupoEdicion_MsgUsuarioGrupoIDRequerida;
                //    txtUsuarioGrupoID.Focus();
                //}
                //else if (cmbUsuario.SelectedItem == null || Contexto.Usuario.UsuarioID == 0 )
                //{
                //    resultado = false;
                //    mensaje = Properties.Resources.UsuarioGrupoEdicion_MsgUsuarioIDRequerida;
                //    cmbUsuario.Focus();
                //}
                //else if (cmbGrupo.SelectedItem == null || Contexto.Grupo.GrupoID == 0 )
                //{
                //    resultado = false;
                //    mensaje = Properties.Resources.UsuarioGrupoEdicion_MsgGrupoIDRequerida;
                //    cmbGrupo.Focus();
                //}
                //else
                //{
                //    int usuarioGrupoId = Extensor.ValorEntero(txtUsuarioGrupoID.Text);
                //    string descripcion = txtDescripcion.Text;

                //    var usuarioGrupoPL = new UsuarioGrupoPL();
                //    UsuarioGrupoInfo usuarioGrupo = usuarioGrupoPL.ObtenerPorDescripcion(descripcion);

                //    if (usuarioGrupo != null && (usuarioGrupoId == 0 || usuarioGrupoId != usuarioGrupo.UsuarioGrupoID))
                //    {
                //        resultado = false;
                //        mensaje = string.Format(Properties.Resources.UsuarioGrupoEdicion_MsgDescripcionExistente, usuarioGrupo.UsuarioGrupoID);
                //    }
                //}
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
            bool guardar = true;// ValidaGuardar();

            if (guardar)
            {
                try
                {
                    var usuarioGrupoPL = new UsuarioGrupoPL();
                    usuarioGrupoPL.Guardar(Contexto.UsuarioGrupo);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.UsuarioID != 0)
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
                    SkMessageBox.Show(this, Properties.Resources.UsuarioGrupo_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this,Properties.Resources.UsuarioGrupo_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        /// <summary>
        /// Método para llenar el grid de grupos
        /// </summary>
        private void LlenarGridGrupos()
        {
            var usuarioGrupoPL = new UsuarioGrupoPL();
            var resultadoInfo = usuarioGrupoPL.ObtenerPorUsuarioID(Contexto.UsuarioID);

            if (resultadoInfo != null && resultadoInfo.Count > 0)
            {
                gridGrupos.ItemsSource = resultadoInfo;
                Contexto.UsuarioGrupo = resultadoInfo;
            }
            else
            {
                gridGrupos.ItemsSource = new List<UsuarioGrupoInfo>();
            }
        }
        
        #endregion Métodos

    }
}

