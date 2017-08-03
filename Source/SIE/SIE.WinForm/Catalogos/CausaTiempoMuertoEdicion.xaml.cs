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

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for CausaTiempoMuertoEdicion.xaml
    /// </summary>
    public partial class CausaTiempoMuertoEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private CausaTiempoMuertoInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (CausaTiempoMuertoInfo) DataContext;
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
        public CausaTiempoMuertoEdicion()
        {
           InitializeComponent();
           InicializaContexto();
        }

        /// <summary>
        /// Constructor para editar una entidad CausaTiempoMuerto Existente
        /// </summary>
        /// <param name="causaTiempoMuertoInfo"></param>
        public CausaTiempoMuertoEdicion(CausaTiempoMuertoInfo causaTiempoMuertoInfo)
        {
           InitializeComponent();
           causaTiempoMuertoInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
           Contexto = causaTiempoMuertoInfo;
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

        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new CausaTiempoMuertoInfo
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
                if (string.IsNullOrWhiteSpace(txtCausaTiempoMuertoID.Text) )
                {
                    resultado = false;
                    mensaje = Properties.Resources.CausaTiempoMuertoEdicion_MsgCausaTiempoMuertoIDRequerida;
                    txtCausaTiempoMuertoID.Focus();
                }
                else if (string.IsNullOrWhiteSpace(txtDescripcion.Text) || Contexto.Descripcion == string.Empty)
                {
                    resultado = false;
                    mensaje = Properties.Resources.CausaTiempoMuertoEdicion_MsgDescripcionRequerida;
                    txtDescripcion.Focus();
                }
                else if (string.IsNullOrWhiteSpace(txtTipoCausa.Text) || Contexto.TipoCausa == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.CausaTiempoMuertoEdicion_MsgTipoCausaRequerida;
                    txtTipoCausa.Focus();
                }
                else if (cmbActivo.SelectedItem == null )
                {
                    resultado = false;
                    mensaje = Properties.Resources.CausaTiempoMuertoEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    int causaTiempoMuertoId = Extensor.ValorEntero(txtCausaTiempoMuertoID.Text);
                    string descripcion = txtDescripcion.Text;

                    var causaTiempoMuertoBL = new CausaTiempoMuertoBL();
                    CausaTiempoMuertoInfo causaTiempoMuerto = causaTiempoMuertoBL.ObtenerPorDescripcion(descripcion);

                    if (causaTiempoMuerto != null && (causaTiempoMuertoId == 0 || causaTiempoMuertoId != causaTiempoMuerto.CausaTiempoMuertoID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.CausaTiempoMuertoEdicion_MsgDescripcionExistente, causaTiempoMuerto.CausaTiempoMuertoID);
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
                    var causaTiempoMuertoBL = new CausaTiempoMuertoBL();
                    causaTiempoMuertoBL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.CausaTiempoMuertoID != 0)
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
                    SkMessageBox.Show(this, Properties.Resources.CausaTiempoMuerto_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this,Properties.Resources.CausaTiempoMuerto_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        #endregion Métodos

    }
}

