using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for PreguntaEdicion.xaml
    /// </summary>
    public partial class PreguntaEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private PreguntaInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (PreguntaInfo) DataContext;
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
        public PreguntaEdicion()
        {
           InitializeComponent();
           InicializaContexto();
           CargaComboTipoPregunta();
        }

        /// <summary>
        /// Constructor para editar una entidad Pregunta Existente
        /// </summary>
        /// <param name="preguntaInfo"></param>
        public PreguntaEdicion(PreguntaInfo preguntaInfo)
        {
           InitializeComponent();
           CargaComboTipoPregunta();
           Contexto = preguntaInfo;
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
             cmbTipoPregunta.Focus();
            if(Contexto.TipoPregunta == null && Contexto.TipoPreguntaID == 0)
            {
                cmbTipoPregunta.SelectedIndex = 0;
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

        private void TxtValidarLetrasConAcentosPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //e.Handled = Extensor.ValidarNumerosLetrasSinAcentos(e.Text);
        }

        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new PreguntaInfo
            {
                Estatus = EstatusEnum.Activo,
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
                if (string.IsNullOrWhiteSpace(txtPreguntaID.Text) )
                {
                    resultado = false;
                    mensaje = Properties.Resources.PreguntaEdicion_MsgPreguntaIDRequerida;
                    txtPreguntaID.Focus();
                }
                else if (cmbTipoPregunta.SelectedItem == null || Contexto.TipoPreguntaID == 0 )
                {
                    resultado = false;
                    mensaje = Properties.Resources.PreguntaEdicion_MsgTipoPreguntaIDRequerida;
                    cmbTipoPregunta.Focus();
                }
                else if (string.IsNullOrWhiteSpace(txtDescripcion.Text) || Contexto.Descripcion == string.Empty)
                {
                    resultado = false;
                    mensaje = Properties.Resources.PreguntaEdicion_MsgDescripcionRequerida;
                    txtDescripcion.Focus();
                }
                else if (cmbActivo.SelectedItem == null )
                {
                    resultado = false;
                    mensaje = Properties.Resources.PreguntaEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    int preguntaId = Extensor.ValorEntero(txtPreguntaID.Text);
                    string descripcion = txtDescripcion.Text;

                    var preguntaPL = new PreguntaBL();
                    PreguntaInfo pregunta = preguntaPL.ObtenerPorTipoPreguntaDescripcion(Contexto.TipoPreguntaID, descripcion);

                    if (pregunta != null && (preguntaId == 0 || preguntaId != pregunta.PreguntaID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.PreguntaEdicion_MsgDescripcionExistente, pregunta.PreguntaID);
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
                    var preguntaPL = new PreguntaBL();
                    if(Contexto.PreguntaID > 0)
                    {
                        Contexto.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
                    }
                    int preguntaID = Contexto.PreguntaID;
                    preguntaPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (preguntaID != 0)
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
                    SkMessageBox.Show(this, Properties.Resources.Pregunta_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this,Properties.Resources.Pregunta_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        /// <summary>
        /// Carga los datos de la entidad Tipo Pregunta 
        /// </summary>
        private void CargaComboTipoPregunta()
        {
            var tipoPreguntaPL = new TipoPreguntaBL();
            var tipoPregunta = new TipoPreguntaInfo
            {
                TipoPreguntaID = 0,
                Descripcion = Properties.Resources.cbo_Seleccione,
            };
            IList<TipoPreguntaInfo> listaTipoPregunta = tipoPreguntaPL.ObtenerTodos(EstatusEnum.Activo);
            listaTipoPregunta.Insert(0, tipoPregunta);
            cmbTipoPregunta.ItemsSource = listaTipoPregunta;
            cmbTipoPregunta.SelectedItem = tipoPregunta;
        }

        #endregion Métodos
    }
}

