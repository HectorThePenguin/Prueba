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
    /// Interaction logic for CalidadGanadoEdicion.xaml
    /// </summary>
    public partial class CalidadGanadoEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private CalidadGanadoInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (CalidadGanadoInfo) DataContext;
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
        public CalidadGanadoEdicion()
        {
            InitializeComponent();
            InicializaContexto();
        }

        /// <summary>
        /// Constructor para editar una entidad CalidadGanado Existente
        /// </summary>
        /// <param name="calidadGanadoInfo"></param>
        public CalidadGanadoEdicion(CalidadGanadoInfo calidadGanadoInfo)
        {
           InitializeComponent();
           calidadGanadoInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
           Contexto = calidadGanadoInfo;
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
            Contexto =
                new CalidadGanadoInfo
                {
                    CalidadGanadoID = 0,
                    UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                    Sexo = Sexo.Hembra,
                    Activo = EstatusEnum.Activo,
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
                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    resultado = false;
                    mensaje = Properties.Resources.CalidadGanadoEdicion_MsgDescripcionRequerida;
                    txtDescripcion.Focus();
                }else if(cmbSexo.SelectedItem == null)
                {
                    resultado = false;
                    mensaje = Properties.Resources.CalidadGanadoEdicion_MsgSexoRequerida;
                    cmbSexo.Focus();
                }
                else if (string.IsNullOrWhiteSpace(txtCalidad.Text))
                {
                    resultado = false;
                    mensaje = Properties.Resources.CalidadGanadoEdicion_MsgCalidadRequerida;
                    txtCalidad.Focus();
                }
                else
                {
                    int calidadGanadoId = Extensor.ValorEntero(txtCalidadGanadoId.Text);
                    string descripcion = txtDescripcion.Text;
                    var sexo = (Sexo) cmbSexo.SelectedItem;

                    var calidadGanadoPL = new CalidadGanadoPL();
                    CalidadGanadoInfo calidadGanado = calidadGanadoPL.ObtenerPorDescripcion(descripcion, sexo);

                    if (calidadGanado != null && (calidadGanadoId == 0 || calidadGanadoId != calidadGanado.CalidadGanadoID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.CalidadGanadoEdicion_MsgDescripcionExistente, calidadGanado.CalidadGanadoID);
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
                    var calidadGanadoPL = new CalidadGanadoPL();
                    calidadGanadoPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.CalidadGanadoID != 0)
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
                    SkMessageBox.Show(this, Properties.Resources.CalidadGanado_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this,Properties.Resources.CalidadGanado_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }
        #endregion Métodos

    }
}

