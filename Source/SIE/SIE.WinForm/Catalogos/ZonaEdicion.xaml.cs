using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.Services.Servicios.BL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para ZonaEdicion.xaml
    /// </summary>
    public partial class ZonaEdicion
    {

        #region Propiedades

        private ZonaInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ZonaInfo)DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        #endregion Propiedades

        #region Constructores

        public ZonaEdicion()
        {
            InitializeComponent();
            InicializaContexto();
        }

        /// <summary>
        /// Constructor para editar una entidad Zona Existente
        /// </summary>
        /// <param name="zonaInfo"></param>
        public ZonaEdicion(ZonaInfo zonaInfo)
        {
            InitializeComponent();
            Contexto = zonaInfo;
        }

        #endregion Constructores

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto =
               new ZonaInfo
               {
                   UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                   Descripcion = string.Empty,
                   Pais = new PaisInfo()
               };
        }


        /// <summary>
        /// Evento de Carga de la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtDescripcion.Focus();
            skAyudaPais.ObjetoNegocio = new PaisPL();
            skAyudaPais.Contexto = Contexto.Pais;

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
        /// Evento para regresar a la pantalla anterior
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
                    var zonaBL = new ZonaBL();
                    zonaBL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK,
                                      MessageImage.Correct);
                    if (Contexto.ZonaID != 0)
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
                    SkMessageBox.Show(this, Properties.Resources.Zona_ErrorGuardar, MessageBoxButton.OK,
                                      MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.Zona_ErrorGuardar, MessageBoxButton.OK,
                                      MessageImage.Error);
                }
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
                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    resultado = false;
                    mensaje = Properties.Resources.ZonaEdicion_MsgDescripcionRequerida;
                    txtDescripcion.Focus();
                }
                else if (Contexto.Pais.PaisID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.ZonaEdicion_MsgPaisRequerida;
                    skAyudaPais.Focus();
                }
                else
                {
                    int zonaId = Extensor.ValorEntero(txtZonaId.Text);
                    string descripcion = txtDescripcion.Text.Trim();

                    var zonaBL = new ZonaBL();
                    ZonaInfo zona = zonaBL.ObtenerPorDescripcion(descripcion);

                    if (zona != null && (zonaId == 0 || zonaId != zona.ZonaID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.ZonaEdicion_MsgDescripcionExistente,
                                                zona.ZonaID);
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
        /// utilizaremos el evento PreviewTextInput para validar la entrada de solo números
        /// </summary>
        /// <param name="sender">objeto que implementa el método</param>
        /// <param name="e">argumentos asociados</param>
        /// <returns></returns>  
        private void TxtSoloNumerosPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }
        #endregion Métodos


    }
}
