using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using System.Windows.Input;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for GrupoCorralEdicion.xaml
    /// </summary>
    public partial class GrupoCorralEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private GrupoCorralInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (GrupoCorralInfo) DataContext;
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
        public GrupoCorralEdicion()
        {
           InitializeComponent();
           InicializaContexto();
        }

        /// <summary>
        /// Constructor para editar una entidad GrupoCorral Existente
        /// </summary>
        /// <param name="grupoCorralInfo"></param>
        public GrupoCorralEdicion(GrupoCorralInfo grupoCorralInfo)
        {
           InitializeComponent();
           grupoCorralInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
           Contexto = grupoCorralInfo;
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

        /// <summary>
        /// utilizaremos el evento PreviewTextInput para validar letras y acentos
        /// </summary>
        /// <param name="sender">objeto que implementa el método</param>
        /// <param name="e">argumentos asociados</param>
        /// <returns></returns>
        private void TxtValidarLetrasConAcentosPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarNumerosLetrasSinAcentos(e.Text);
        }

        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new GrupoCorralInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
            };
            txtDescripcion.Focus();
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
                if (string.IsNullOrWhiteSpace(Contexto.Descripcion))
                {
                    resultado = false;
                    mensaje = Properties.Resources.GrupoCorralEdicion_MsgDescripcionRequerida;
                    txtDescripcion.Focus();
                }
                else if (cmbActivo.SelectedItem == null)
                {
                    resultado = false;
                    mensaje = Properties.Resources.GrupoCorralEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    int grupoCorralId = Contexto.GrupoCorralID;
                    string descripcion = Contexto.Descripcion;

                    using (var grupoCorralBL = new GrupoCorralBL())
                    {
                        GrupoCorralInfo grupoCorral = grupoCorralBL.ObtenerPorDescripcion(descripcion);

                        if (grupoCorral != null && (grupoCorralId == 0 || grupoCorralId != grupoCorral.GrupoCorralID))
                        {
                            resultado = false;
                            mensaje = string.Format(Properties.Resources.GrupoCorralEdicion_MsgDescripcionExistente,
                                                    grupoCorral.GrupoCorralID);
                        }
                    }
                }
                if (resultado && Contexto.GrupoCorralID > 0)
                {
                    var tipoCorralPL = new TipoCorralPL();
                    bool tieneTiposAsignados = tipoCorralPL.TieneAsignadoGruposCorral(Contexto.GrupoCorralID);
                    if (tieneTiposAsignados)
                    {
                        resultado = false;
                        cmbActivo.Focus();
                        mensaje = Properties.Resources.GrupoCorralEdicion_MsgGrupoCorralAsignatoTipoCorral;
                    }
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
                    using (var grupoCorralBL = new GrupoCorralBL())
                    {
                        int grupoCorralID = Contexto.GrupoCorralID;
                        grupoCorralBL.Guardar(Contexto);
                        SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                        if (grupoCorralID != 0)
                        {
                            confirmaSalir = false;
                            Close();
                        }
                        else
                        {
                            InicializaContexto();
                        }
                    }
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.GrupoCorral_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this,Properties.Resources.GrupoCorral_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        #endregion Métodos

    }
}

