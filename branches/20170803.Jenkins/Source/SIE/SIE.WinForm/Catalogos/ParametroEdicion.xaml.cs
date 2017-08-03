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
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for ParametroEdicion.xaml
    /// </summary>
    public partial class ParametroEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private ParametroInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ParametroInfo)DataContext;
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
        public ParametroEdicion()
        {
            InitializeComponent();
            CargaTipoParametro();
            InicializaContexto();            
        }

        /// <summary>
        /// Constructor para editar una entidad Parametro Existente
        /// </summary>
        /// <param name="parametroInfo"></param>
        public ParametroEdicion(ParametroInfo parametroInfo)
        {
            InitializeComponent();
            CargaTipoParametro();
            parametroInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto = parametroInfo;
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

        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new ParametroInfo
            {
                TipoParametro = new TipoParametroInfo(),
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
                if (string.IsNullOrWhiteSpace(txtDescripcion.Text) || Contexto.Descripcion == string.Empty)
                {
                    resultado = false;
                    mensaje = Properties.Resources.ParametroEdicion_MsgDescripcionRequerida;
                    txtDescripcion.Focus();
                }
                else if (cmbTipoParametro.SelectedItem == null || Contexto.TipoParametro.TipoParametroID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.ParametroEdicion_MsgTipoParametroIDRequerida;
                    cmbTipoParametro.Focus();
                }
                else if (string.IsNullOrWhiteSpace(txtClave.Text) || Contexto.Clave == string.Empty)
                {
                    resultado = false;
                    mensaje = Properties.Resources.ParametroEdicion_MsgClaveRequerida;
                    txtClave.Focus();
                }               
                else
                {
                    int parametroId = Extensor.ValorEntero(txtParametroID.Text);
                    string descripcion = txtDescripcion.Text.Trim();

                    var parametroPL = new ParametroPL();
                    ParametroInfo parametro = parametroPL.ObtenerPorDescripcion(descripcion);

                    if (parametro != null && (parametroId == 0 || parametroId != parametro.ParametroID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.ParametroEdicion_MsgDescripcionExistente, parametro.ParametroID);
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
                    var parametroPL = new ParametroPL();
                    parametroPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.ParametroID != 0)
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
                    SkMessageBox.Show(this, Properties.Resources.Parametro_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.Parametro_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }
        private void CargaTipoParametro()
        {
            var tipoParametroPL = new TipoParametroPL();
            var tipoParametroInfo = new TipoParametroInfo
            {
                TipoParametroID = 0,
                Descripcion = Properties.Resources.cbo_Seleccione,
            };
            IList<TipoParametroInfo> listaTipoParametro = tipoParametroPL.ObtenerTodos(EstatusEnum.Activo);
            listaTipoParametro.Insert(0, tipoParametroInfo);
            cmbTipoParametro.ItemsSource = listaTipoParametro;
            cmbTipoParametro.SelectedItem = tipoParametroInfo;
        }
        #endregion Métodos

    }
}

