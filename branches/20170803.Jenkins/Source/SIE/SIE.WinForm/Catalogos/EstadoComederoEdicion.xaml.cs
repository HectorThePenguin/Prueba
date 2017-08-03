using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for EstadoComederoEdicion.xaml
    /// </summary>
    public partial class EstadoComederoEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private EstadoComederoInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (EstadoComederoInfo)DataContext;
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
        public EstadoComederoEdicion()
        {
            InitializeComponent();
            InicializaContexto();
        }

        /// <summary>
        /// Constructor para editar una entidad EstadoComedero Existente
        /// </summary>
        /// <param name="estadoComederoInfo"></param>
        public EstadoComederoEdicion(EstadoComederoInfo estadoComederoInfo)
        {
            InitializeComponent();
            estadoComederoInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto = estadoComederoInfo;
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
        /// Evento que guarda los datos de la entidad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Valida la entrada del control Decimal UpDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtuControl_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.OemPeriod)
                {
                    return;
                }

                if (e.Key == Key.Enter || e.Key == Key.Tab)
                {
                    cmbTendencia.Focus();
                    e.Handled = true;
                    return;
                }

                if ((int)e.Key > 43 || (int)e.Key < 34)
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(this, ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// utilizaremos el evento PreviewTextInput para validar letras y acentos
        /// </summary>
        /// <param name="sender">objeto que implementa el método</param>
        /// <param name="e">argumentos asociados</param>
        /// <returns></returns>
        private void TxtSoloLetrasPreviewTextInput(object sender, TextCompositionEventArgs e)
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
            Contexto = new EstadoComederoInfo
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
                if (string.IsNullOrWhiteSpace(Contexto.Descripcion))
                {
                    resultado = false;
                    mensaje = Properties.Resources.EstadoComederoEdicion_MsgDescripcionRequerida;
                    txtDescripcion.Focus();
                }
                else if (string.IsNullOrWhiteSpace(Contexto.DescripcionCorta))
                {
                    resultado = false;
                    mensaje = Properties.Resources.EstadoComederoEdicion_MsgDescripcionCortaRequerida;
                    txtDescripcionCorta.Focus();
                }
                else if (!dtuAjusteBase.Value.HasValue || Contexto.AjusteBase == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.EstadoComederoEdicion_MsgAjusteBaseRequerida;
                    dtuAjusteBase.Focus();
                }
                else if (cmbTendencia.SelectedItem == null)
                {
                    resultado = false;
                    mensaje = Properties.Resources.EstadoComederoEdicion_MsgTendenciaRequerida;
                    cmbTendencia.Focus();
                }
                else if (cmbActivo.SelectedItem == null)
                {
                    resultado = false;
                    mensaje = Properties.Resources.EstadoComederoEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    int estadoComederoId = Contexto.EstadoComederoID;

                    var estadoComederoPL = new EstadoComederoPL();
                    EstadoComederoInfo estadoComedero = estadoComederoPL.ObtenerPorDescripcion(Contexto.Descripcion);

                    if (estadoComedero != null 
                            && (estadoComederoId == 0 || estadoComederoId != estadoComedero.EstadoComederoID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.EstadoComederoEdicion_MsgDescripcionExistente, estadoComedero.EstadoComederoID);
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
                    var estadoComederoPL = new EstadoComederoPL();
                    estadoComederoPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.EstadoComederoID == 0)
                    {
                        InicializaContexto();
                        txtDescripcion.Focus();
                    }
                    else
                    {
                        confirmaSalir = false;
                        Close();                        
                    }
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.EstadoComedero_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.EstadoComedero_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }
        #endregion Métodos
    }
}
