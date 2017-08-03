using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
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
    /// Interaction logic for TipoServicioEdicion.xaml
    /// </summary>
    public partial class TipoServicioEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private TipoServicioInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (TipoServicioInfo) DataContext;
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
        public TipoServicioEdicion()
        {
           InitializeComponent();
           InicializaContexto();
        }

        /// <summary>
        /// Constructor para editar una entidad TipoServicio Existente
        /// </summary>
        /// <param name="tipoServicioInfo"></param>
        public TipoServicioEdicion(TipoServicioInfo tipoServicioInfo)
        {
           InitializeComponent();
           tipoServicioInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
           Contexto = tipoServicioInfo;
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

        /// <summary>
        /// utilizaremos el evento PreviewTextInput para validarnúmeros
        /// </summary>
        /// <param name="sender">objeto que implementa el método</param>
        /// <param name="e">argumentos asociados</param>
        /// <returns></returns>
        private void TxtValidarSoloNumerosPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new TipoServicioInfo
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
                    mensaje = Properties.Resources.TipoServicioEdicion_MsgDescripcionRequerida;
                    txtDescripcion.Focus();                    
                }
                else if (string.IsNullOrWhiteSpace(Contexto.HoraInicio)) 
                {
                    resultado = false;
                    mensaje = Properties.Resources.TipoServicioEdicion_MsgHoraInicioRequerida;
                    DtuHoraInicio.Focus();                    
                }
                else if (string.IsNullOrWhiteSpace(Contexto.HoraFin))
                {
                    resultado = false;
                    mensaje = Properties.Resources.TipoServicioEdicion_MsgHoraFinRequerida;
                    DtuHoraFin.Focus();
                }
                else if (cmbActivo.SelectedItem == null )
                {
                    resultado = false;
                    mensaje = Properties.Resources.TipoServicioEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    int tipoServicioId = Contexto.TipoServicioId;
                    string descripcion = Contexto.Descripcion;

                    using(var tipoServicioBL = new TipoServicioBL())
                    {
                        TipoServicioInfo tipoServicio = tipoServicioBL.ObtenerPorDescripcion(descripcion);

                        if (tipoServicio != null && (tipoServicioId == 0 || tipoServicioId != tipoServicio.TipoServicioId))
                        {
                            resultado = false;
                            mensaje = string.Format(Properties.Resources.TipoServicioEdicion_MsgDescripcionExistente,
                                                    tipoServicio.TipoServicioId);
                        }
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
                    using(var tipoServicioBL = new TipoServicioBL())
                    {
                        int tipoServicioID = Contexto.TipoServicioId;
                        tipoServicioBL.Guardar(Contexto);
                        SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                        if (tipoServicioID != 0)
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
                    SkMessageBox.Show(this, Properties.Resources.TipoServicio_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this,Properties.Resources.TipoServicio_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        #endregion Métodos

        private void DtuHoraSalidaKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.OemPeriod)
                {
                    return;
                }

                if (e.Key == Key.Enter || e.Key == Key.Tab)
                {
                    return;
                }

                if ((int)e.Key == 58)
                {
                    e.Handled = false;
                }
                else if ((int)e.Key > 43 || (int)e.Key < 34)
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(this, ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }        
    }
}

