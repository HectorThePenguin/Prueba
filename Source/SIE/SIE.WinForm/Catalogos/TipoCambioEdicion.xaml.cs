using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for TipoCambioEdicion.xaml
    /// </summary>
    public partial class TipoCambioEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private TipoCambioInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (TipoCambioInfo) DataContext;
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
        public TipoCambioEdicion()
        {
           InitializeComponent();
            CargarComboMoneda();
           InicializaContexto();
        }

        /// <summary>
        /// Constructor para editar una entidad TipoCambio Existente
        /// </summary>
        /// <param name="tipoCambioInfo"></param>
        public TipoCambioEdicion(TipoCambioInfo tipoCambioInfo)
        {
           InitializeComponent();
           CargarComboMoneda();
           tipoCambioInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
           Contexto = tipoCambioInfo;
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
            cmbMoneda.Focus();
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
            Contexto = new TipoCambioInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                Fecha = DateTime.Now,
                Moneda = new MonedaInfo()
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
                if (string.IsNullOrWhiteSpace(txtTipoCambioID.Text) )
                {
                    resultado = false;
                    mensaje = Properties.Resources.TipoCambioEdicion_MsgTipoCambioIDRequerida;
                    txtTipoCambioID.Focus();
                }
                else if (Contexto.Moneda == null || Contexto.Moneda.MonedaID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.TipoCambioEdicion_MsgDescripcionRequerida;
                    cmbMoneda.Focus();
                }
                else if (!dtuCambio.Value.HasValue || Contexto.Cambio == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.TipoCambioEdicion_MsgCambioRequerida;
                    dtuCambio.Focus();
                }
                else if (!dtpFecha.SelectedDate.HasValue)
                {
                    resultado = false;
                    mensaje = Properties.Resources.TipoCambioEdicion_MsgFechaRequerida;
                    dtpFecha.Focus();
                }
                else if (cmbActivo.SelectedItem == null )
                {
                    resultado = false;
                    mensaje = Properties.Resources.TipoCambioEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    int tipoCambioId = Extensor.ValorEntero(txtTipoCambioID.Text);
                    string descripcion = Contexto.Moneda.Descripcion;

                    var tipoCambioPL = new TipoCambioPL();
                    TipoCambioInfo tipoCambio = tipoCambioPL.ObtenerPorDescripcionFecha(descripcion, Contexto.Fecha);

                    if (tipoCambio != null && (tipoCambioId == 0 || tipoCambioId != tipoCambio.TipoCambioId))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.TipoCambioEdicion_MsgDescripcionExistente, tipoCambio.TipoCambioId);
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
                    var tipoCambioPL = new TipoCambioPL();
                    tipoCambioPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.TipoCambioId != 0)
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
                    SkMessageBox.Show(this, Properties.Resources.TipoCambio_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this,Properties.Resources.TipoCambio_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        private void CargarComboMoneda()
        {
            var monedaBL = new MonedaBL();
            var listaMoneda = monedaBL.ObtenerTodos(EstatusEnum.Activo);
            var monedaDefault = new MonedaInfo
            {
                MonedaID = 0,
                Descripcion = Properties.Resources.cbo_Seleccione
            };
            listaMoneda.Insert(0, monedaDefault);
            cmbMoneda.ItemsSource = listaMoneda;
        }

        #endregion Métodos

    }
}

