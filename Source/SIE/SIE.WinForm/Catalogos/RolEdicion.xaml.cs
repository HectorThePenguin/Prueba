using System;
using System.Collections.Generic;
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
    /// Interaction logic for RolEdicion.xaml
    /// </summary>
    public partial class RolEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private RolInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (RolInfo) DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool _confirmaSalir = true;

        #endregion Propiedades

        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public RolEdicion()
        {
            InitializeComponent();
            InicializaContexto();
            CargaComboNivelAlerta();
        }

        /// <summary>
        /// Constructor para editar una entidad Rol Existente
        /// </summary>
        /// <param name="rolInfo"></param>
        public RolEdicion(RolInfo rolInfo)
        {
            InitializeComponent();
            CargaComboNivelAlerta();
            rolInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto = rolInfo;
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
            if (_confirmaSalir)
            {
                MessageBoxResult result = SkMessageBox.Show(this, Properties.Resources.Msg_CerrarSinGuardar,
                                                            MessageBoxButton.YesNo,
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
            e.Handled = Extensor.ValidarLetrasConAcentos(e.Text);
        }

        /// <summary>
        /// utilizaremos el evento PreviewTextInput para validar números
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
            Contexto = new RolInfo
                           {
                               UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                               NivelAlerta = new NivelAlertaInfo()
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
                if (string.IsNullOrWhiteSpace(txtRolID.Text))
                {
                    resultado = false;
                    mensaje = Properties.Resources.RolEdicion_MsgRolIDRequerida;
                    txtRolID.Focus();
                }
                else if (string.IsNullOrWhiteSpace(txtDescripcion.Text) || Contexto.Descripcion == string.Empty)
                {
                    resultado = false;
                    mensaje = Properties.Resources.RolEdicion_MsgDescripcionRequerida;
                    txtDescripcion.Focus();
                }
                else if (cmbActivo.SelectedItem == null)
                {
                    resultado = false;
                    mensaje = Properties.Resources.RolEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    int rolId = Extensor.ValorEntero(txtRolID.Text);
                    string descripcion = txtDescripcion.Text;

                    var rolPl = new RolPL();
                    RolInfo rol = rolPl.ObtenerPorDescripcion(descripcion);

                    if (rol != null && (rolId == 0 || rolId != rol.RolID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.RolEdicion_MsgDescripcionExistente, rol.RolID);
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
                    var rolPl = new RolPL();
                    rolPl.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK,
                                      MessageImage.Correct);
                    if (Contexto.RolID != 0)
                    {
                        _confirmaSalir = false;
                        Close();
                    }
                    else
                    {
                        InicializaContexto();
                    }
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.Rol_ErrorGuardar, MessageBoxButton.OK,
                                      MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.Rol_ErrorGuardar, MessageBoxButton.OK,
                                      MessageImage.Error);
                }
            }
        }

        /// <summary>
        /// Metodo para cargar el comboBox de Nivel de Alertas
        /// </summary>
        private void CargaComboNivelAlerta()
        {
            try
            {
                var nivelAlertaPl = new RolPL();
                var nivelAlerta = new NivelAlertaInfo
                {
                    NivelAlertaId = 0,
                    Descripcion = Properties.Resources.cbo_Seleccione,
                };
                IList<NivelAlertaInfo> listaNivelAlerta = nivelAlertaPl.NivelAlertaInfo();
                listaNivelAlerta.Insert(0, nivelAlerta);
                cmbNivel.ItemsSource = listaNivelAlerta;
                cmbNivel.SelectedItem = nivelAlerta;
                if (Contexto.NivelAlerta.Descripcion == null || Contexto.NivelAlerta.NivelAlertaId == 0)
                {
                    Contexto.NivelAlerta = nivelAlerta;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.ClienteNuevo_ErrorMetodoPago, MessageBoxButton.OK,
                                                            MessageImage.Error);
            }
        }
        #endregion Métodos
    }
}

