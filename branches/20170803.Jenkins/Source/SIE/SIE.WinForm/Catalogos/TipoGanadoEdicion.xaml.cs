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
    /// Interaction logic for TipoGanadoEdicion.xaml
    /// </summary>
    public partial class TipoGanadoEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private TipoGanadoInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (TipoGanadoInfo) DataContext;
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
        public TipoGanadoEdicion()
        {
            InitializeComponent();
            InicializaContexto();
        }

        /// <summary>
        /// Constructor para editar una entidad TipoGanado Existente
        /// </summary>
        /// <param name="tipoGanadoInfo"></param>
        public TipoGanadoEdicion(TipoGanadoInfo tipoGanadoInfo)
        {
           InitializeComponent();
           tipoGanadoInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
           Contexto = tipoGanadoInfo;
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
        /// utilizaremos el evento PreviewTextInput para validar la entrada de solo números
        /// </summary>
        /// <param name="sender">objeto que implementa el método</param>
        /// <param name="e">argumentos asociados</param>
        /// <returns></returns>  
        private void TxtSoloNumerosPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        private void TxtDescripcionPreviewTextInput(object sender, TextCompositionEventArgs e)
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
            Contexto = new TipoGanadoInfo
            {
                Sexo = Sexo.Hembra,
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
                if (string.IsNullOrWhiteSpace(txtDescripcion.Text) || txtDescripcion.Text == "0")
                {
                    resultado = false;
                    mensaje = Properties.Resources.TipoGanadoEdicion_MsgDescripcionRequerida;
                    txtDescripcion.Focus();
                }
                else if (cmbSexo.SelectedItem == null)
                {
                    resultado = false;
                    mensaje = Properties.Resources.TipoGanadoEdicion_MsgSexoRequerida;
                    cmbSexo.Focus();
                }
                else if (string.IsNullOrWhiteSpace(txtPesoMinimo.Text) || txtPesoMinimo.Text == "0")
                {
                    resultado = false;
                    mensaje = Properties.Resources.TipoGanadoEdicion_MsgPesoMinimoRequerida;
                    txtPesoMinimo.Focus();
                }
                else if (string.IsNullOrWhiteSpace(txtPesoMaximo.Text) || txtPesoMaximo.Text == "0")
                {
                    resultado = false;
                    mensaje = Properties.Resources.TipoGanadoEdicion_MsgPesoMaximoRequerida;
                    txtPesoMaximo.Focus();
                }
                else if (!iudPesoSalida.Value.HasValue || iudPesoSalida.Value.Value == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.TipoGanadoEdicion_MsgPesoSalidaRequerida;
                    iudPesoSalida.Focus();
                }
                else if (Contexto.PesoMaximo < Contexto.PesoMinimo)
                {
                    resultado = false;
                    mensaje = Properties.Resources.TipoGanadoEdicion_MsgPesoMaximoMenorPesoMinimoRequerida;
                    txtPesoMaximo.Focus();
                }
                else if (cmbActivo.SelectedItem == null)
                {
                    resultado = false;
                    mensaje = Properties.Resources.TipoGanadoEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    int tipoGanadoId = Extensor.ValorEntero(txtTipoGanadoID.Text);
                    string descripcion = txtDescripcion.Text.Trim();

                    var tipoGanadoPL = new TipoGanadoPL();
                    TipoGanadoInfo tipoGanado = tipoGanadoPL.ObtenerPorDescripcion(descripcion);

                    if (tipoGanado != null && (tipoGanadoId == 0 || tipoGanadoId != tipoGanado.TipoGanadoID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.TipoGanadoEdicion_MsgDescripcionExistente, tipoGanado.TipoGanadoID);
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
                    var tipoGanadoPL = new TipoGanadoPL();
                    tipoGanadoPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.TipoGanadoID != 0)
                    {
                        confirmaSalir = false;
                        Close();    
                    }
                    else
                    {
                        InicializaContexto();
                        txtDescripcion.Focus();
                    }                    
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.TipoGanado_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this,Properties.Resources.TipoGanado_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }
        #endregion Métodos
        
    }
}

