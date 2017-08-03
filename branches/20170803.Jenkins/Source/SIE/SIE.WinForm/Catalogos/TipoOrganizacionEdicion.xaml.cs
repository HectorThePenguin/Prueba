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
    /// Interaction logic for TipoOrganizacionEdicion.xaml
    /// </summary>
    public partial class TipoOrganizacionEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private TipoOrganizacionInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (TipoOrganizacionInfo) DataContext;
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
        public TipoOrganizacionEdicion()
        {
           InitializeComponent();
           InicializaContexto();
            CargarCombos();
        }

        /// <summary>
        /// Constructor para editar una entidad TipoOrganizacion Existente
        /// </summary>
        /// <param name="tipoOrganizacionInfo"></param>
        public TipoOrganizacionEdicion(TipoOrganizacionInfo tipoOrganizacionInfo)
        {
           InitializeComponent();
           tipoOrganizacionInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
           Contexto = tipoOrganizacionInfo;
            CargarCombos();
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
            Contexto = new TipoOrganizacionInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                TipoProceso = new TipoProcesoInfo(), 
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
                if (string.IsNullOrWhiteSpace(txtTipoOrganizacionID.Text))
                {
                    resultado = false;
                    mensaje = Properties.Resources.TipoOrganizacionEdicion_MsgTipoOrganizacionIDRequerida;
                    txtTipoOrganizacionID.Focus();
                }
                else if (string.IsNullOrWhiteSpace(txtDescripcion.Text) || Contexto.Descripcion == string.Empty)
                {
                    resultado = false;
                    mensaje = Properties.Resources.TipoOrganizacionEdicion_MsgDescripcionRequerida;
                    txtDescripcion.Focus();
                }
                else if (cmbTipoProceso.SelectedItem == null || Contexto.TipoProceso.TipoProcesoID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.TipoOrganizacionEdicion_MsgTipoProcesoIDRequerida;
                    cmbTipoProceso.Focus();
                }
                else if (cmbActivo.SelectedItem == null)
                {
                    resultado = false;
                    mensaje = Properties.Resources.TipoOrganizacionEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    int tipoOrganizacionId = Extensor.ValorEntero(txtTipoOrganizacionID.Text);
                    string descripcion = txtDescripcion.Text.Trim();

                    var tipoOrganizacionPL = new TipoOrganizacionPL();
                    TipoOrganizacionInfo tipoOrganizacion = tipoOrganizacionPL.ObtenerPorDescripcion(descripcion);

                    if (tipoOrganizacion != null && (tipoOrganizacionId == 0 || tipoOrganizacionId != tipoOrganizacion.TipoOrganizacionID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.TipoOrganizacionEdicion_MsgDescripcionExistente, tipoOrganizacion.TipoOrganizacionID);
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
                    var tipoOrganizacionPL = new TipoOrganizacionPL();
                    tipoOrganizacionPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.TipoOrganizacionID != 0)
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
                    SkMessageBox.Show(this, Properties.Resources.TipoOrganizacion_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this,Properties.Resources.TipoOrganizacion_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        /// <summary>
        /// Método para cargar el combo tipos de organización
        /// </summary>
        private void CargarCombos()
        {
            CargarComboTiposProceso();
        }

        /// <summary>
        /// Método para cargar el combo tipos de organización
        /// </summary>
        private void CargarComboTiposProceso()
        {
            var tipoProcesoPL = new TipoProcesoPL();
            var tipoProceso = new TipoProcesoInfo
            {
                TipoProcesoID = 0,
                Descripcion = Properties.Resources.cbo_Seleccione,
            };
            IList<TipoProcesoInfo> listaTiposProceso = tipoProcesoPL.ObtenerTodos(EstatusEnum.Activo);
            listaTiposProceso.Insert(0, tipoProceso);
            cmbTipoProceso.ItemsSource = listaTiposProceso;
            cmbTipoProceso.SelectedItem = tipoProceso;
        }

        #endregion Métodos

    }
}

