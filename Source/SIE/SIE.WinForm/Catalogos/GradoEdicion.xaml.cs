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
using System.Windows.Input;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for GradoEdicion.xaml
    /// </summary>
    public partial class GradoEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private GradoInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (GradoInfo) DataContext;
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
        public GradoEdicion()
        {
            InitializeComponent();
            InicializaContexto();
        }

        /// <summary>
        /// Constructor para editar una entidad Grado Existente
        /// </summary>
        /// <param name="gradoInfo"></param>
        public GradoEdicion(GradoInfo gradoInfo)
        {
            InitializeComponent();
            gradoInfo.NivelesGravedad = GenerarNivelGravedad();
            gradoInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto = gradoInfo;
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
            if (Contexto.GradoID == 0)
            {
                cboNivelGravedad.SelectedIndex = 0;
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
            Contexto = new GradoInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                NivelesGravedad = GenerarNivelGravedad(),
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
                var nivelesGravedadValidos = new []{'G', 'L'};
                if (string.IsNullOrWhiteSpace(Contexto.Descripcion))
                {
                    resultado = false;
                    mensaje = Properties.Resources.GradoEdicion_MsgDescripcionRequerida;
                    txtDescripcion.Focus();
                }
                else if (Contexto.NivelGravedad.IndexOfAny(nivelesGravedadValidos) < 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.GradoEdicion_MsgNivelGravedadRequerida;
                    cboNivelGravedad.Focus();
                }
                else if (cmbActivo.SelectedItem == null)
                {
                    resultado = false;
                    mensaje = Properties.Resources.GradoEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    int gradoId = Contexto.GradoID;
                    string descripcion = Contexto.Descripcion;

                    var gradoPL = new GradoPL();
                    GradoInfo grado = gradoPL.ObtenerPorDescripcion(descripcion);

                    if (grado != null && (gradoId == 0 || gradoId != grado.GradoID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.GradoEdicion_MsgDescripcionExistente, grado.GradoID);
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
                    var gradoPL = new GradoPL();
                    gradoPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.GradoID != 0)
                    {
                        confirmaSalir = false;
                        Close();
                    }
                    else
                    {
                        InicializaContexto();
                        cboNivelGravedad.SelectedIndex = 0;
                    }
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.Grado_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this,Properties.Resources.Grado_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        /// <summary>
        /// Genera los niveles de gravedad
        /// </summary>
        private List<string> GenerarNivelGravedad()
        {
            return new List<string> {Properties.Resources.cbo_Seleccione, "G", "L"};
        }

        #endregion Métodos

    }
}

