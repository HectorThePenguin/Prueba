using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for CausaSalidaEdicion.xaml
    /// </summary>
    public partial class CausaSalidaEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private CausaSalidaInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (CausaSalidaInfo)DataContext;
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
        public CausaSalidaEdicion()
        {
            InitializeComponent();
            InicializaContexto();
        }

        /// <summary>
        /// Constructor para editar una entidad CausaSalida Existente
        /// </summary>
        /// <param name="causaSalidaInfo"></param>
        public CausaSalidaEdicion(CausaSalidaInfo causaSalidaInfo)
        {
            InitializeComponent();
            causaSalidaInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto = causaSalidaInfo;
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
            CargarTiposMovimientos();
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
        /// utilizaremos el evento PreviewTextInput para validar números letras sin acentos
        /// </summary>
        /// <param name="sender">objeto que implementa el método</param>
        /// <param name="e">argumentos asociados</param>
        /// <returns></returns>  
        private void TxtValidarNumerosLetrasSinAcentosPreviewTextInput(object sender, TextCompositionEventArgs e)
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
            Contexto = new CausaSalidaInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                TipoMovimiento = new TipoMovimientoInfo()
            };
        }

        /// <summary>
        /// Metodo que carga el combo con los Tipos de Movimiento
        /// </summary>
        /// <returns></returns>
        private void CargarTiposMovimientos()
        {
            try
            {
                var tipoMovimientoDefault = new TipoMovimientoInfo
                    {
                        TipoMovimientoID = 0,
                        Descripcion = Properties.Resources.cbo_Seleccione
                    };

                var tipoMovimientoPL = new TipoMovimientoPL();
                var tiposMovimiento = tipoMovimientoPL.ObtenerTodos(EstatusEnum.Activo);
                tiposMovimiento.Insert(0, tipoMovimientoDefault);
                cmbTipoMovimiento.ItemsSource = tiposMovimiento;
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);   
            }
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
                    mensaje = Properties.Resources.CausaSalidaEdicion_MsgDescripcionRequerida;
                    txtDescripcion.Focus();
                }
                else if (Contexto.TipoMovimiento == null || Contexto.TipoMovimiento.TipoMovimientoID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.CausaSalidaEdicion_MsgTipoMovimientoIDRequerida;
                    cmbTipoMovimiento.Focus();
                }
                else
                {

                    int causaSalidaId = Contexto.CausaSalidaID;
                 
                        var causaSalidaPL = new CausaSalidaPL();
                        CausaSalidaInfo causaSalida = causaSalidaPL.ObtenerPorTipoMovimientoDescripcion(Contexto);

                        if (causaSalida != null && (causaSalidaId == 0 || causaSalidaId != causaSalida.CausaSalidaID))
                        {
                            resultado = false;
                            mensaje = string.Format(Properties.Resources.CausaSalidaEdicion_MsgDescripcionExistente,
                                                    causaSalida.CausaSalidaID);
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
                    var causaSalidaPL = new CausaSalidaPL();
                    causaSalidaPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    confirmaSalir = false;
                    Close();
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.CausaSalida_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.CausaSalida_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }
        #endregion Métodos

    }
}

