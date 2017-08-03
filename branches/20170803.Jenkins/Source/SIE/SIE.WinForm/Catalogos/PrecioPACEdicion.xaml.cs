using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using Xceed.Wpf.Toolkit;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for PrecioPACEdicion.xaml
    /// </summary>
    public partial class PrecioPACEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private PrecioPACInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (PrecioPACInfo) DataContext;
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
        public PrecioPACEdicion()
        {
           InitializeComponent();
           InicializaContexto();
           CargaComboOrganizacion();
           CargaComboTipo();
        }

        /// <summary>
        /// Constructor para editar una entidad PrecioPAC Existente
        /// </summary>
        /// <param name="precioPACInfo"></param>
        public PrecioPACEdicion(PrecioPACInfo precioPACInfo)
        {
           InitializeComponent();
           CargaComboOrganizacion();
           CargaComboTipo();
           precioPACInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
           Contexto = precioPACInfo;
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
            cmbOrganizacion.Focus();
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

        private void TxtPrecioPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string valor = ((DecimalUpDown)sender).Value.HasValue
                              ? ((DecimalUpDown)sender).Value.ToString()
                              : string.Empty;
            e.Handled = Extensor.ValidarSoloNumerosDecimales(e.Text, valor);
        }

        /// <summary>
        /// Evento que se ejecuta cuando se presiona una tecla en el control fechas
        /// </summary>
        private void DtpControlPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                var keyboardFocus = Keyboard.FocusedElement as UIElement;

                if (keyboardFocus != null)
                {
                    keyboardFocus.MoveFocus(tRequest);
                }
                e.Handled = true;
            }
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new PrecioPACInfo
                           {
                               UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                               OrganizacionID = 0,
                               Organizacion = new OrganizacionInfo(),
                               Precio = 0,
                               PrecioViscera = 0,
                               TipoPAC = new TipoPACInfo(),
                               TipoPACID = 0,
                               FechaInicio = DateTime.Now
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
                if (Contexto.Organizacion.OrganizacionID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.PrecioPACEdicion_MsgOrganizacionIDRequerida;
                    cmbOrganizacion.Focus();
                }
                else if (Contexto.TipoPAC.TipoPACID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.PrecioPACEdicion_MsgTipoPACIDRequerida;
                    cmbTipoPAC.Focus();
                }
                else if (Contexto.Precio == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.PrecioPACEdicion_MsgPrecioRequerida;
                    dtuPrecio.Focus();
                }
                else if (Contexto.PrecioViscera == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.PrecioPACEdicion_MsgPrecioRequerida;
                    dtuPrecio.Focus();
                }
                else if (!dtpFechaInicio.SelectedDate.HasValue)
                {
                    resultado = false;
                    mensaje = Properties.Resources.PrecioPACEdicion_MsgFechaInicioRequerida;
                    dtpFechaInicio.Focus();
                }
                else if (cmbActivo.SelectedItem == null)
                {
                    resultado = false;
                    mensaje = Properties.Resources.PrecioPACEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    int precioPACId = Contexto.PrecioPACID;
                    int organizaionID = Contexto.Organizacion.OrganizacionID;
                    int tipoPACID = Contexto.TipoPAC.TipoPACID;
                    DateTime fecha = Contexto.FechaInicio;

                    PrecioPACInfo precioPAC;
                    using (var precioPACBL = new PrecioPACBL())
                    {
                        precioPAC = precioPACBL.ObtenerPorOrganizacionFecha(organizaionID, tipoPACID, fecha);
                    }
                    if (precioPAC != null && (precioPACId == 0 || precioPACId != precioPAC.PrecioPACID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.PrecioPACEdicion_MsgDescripcionExistente,
                                                precioPAC.PrecioPACID);
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
                    int precioPACId = Contexto.PrecioPACID;
                    using (var precioPACBL = new PrecioPACBL())
                    {
                        precioPACBL.Guardar(Contexto);
                    }
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (precioPACId != 0)
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
                    SkMessageBox.Show(this, Properties.Resources.PrecioPAC_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this,Properties.Resources.PrecioPAC_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }
        /// <summary>
        /// Carga los datos de la entidad Organización 
        /// </summary>
        private void CargaComboOrganizacion()
        {
            var organizacionPL = new OrganizacionPL();
            var organizacion = new OrganizacionInfo
                                   {
                                       OrganizacionID = 0,
                                       Descripcion = Properties.Resources.cbo_Seleccione,
                                   };
            IList<OrganizacionInfo> listaOrganizacion = organizacionPL.ObtenerTodos(EstatusEnum.Activo);
            listaOrganizacion =
                listaOrganizacion.Where(
                    ganadera =>
                    ganadera.TipoOrganizacion.TipoOrganizacionID ==
                    Services.Info.Enums.TipoOrganizacion.Ganadera.GetHashCode()).ToList();
            listaOrganizacion.Insert(0, organizacion);
            cmbOrganizacion.ItemsSource = listaOrganizacion;
            cmbOrganizacion.SelectedItem = organizacion;
        }

        /// <summary>
        /// Carga los datos de la entidad Tipo 
        /// </summary>
        private void CargaComboTipo()
        {
            var tipoPL = new TipoPACBL();
            var tipo = new TipoPACInfo
            {
                TipoPACID = 0,
                Descripcion = Properties.Resources.cbo_Seleccione,
            };
            IList<TipoPACInfo> listaTipo = tipoPL.ObtenerTodos(EstatusEnum.Activo);
            listaTipo.Insert(0, tipo);
            cmbTipoPAC.ItemsSource = listaTipo;
            cmbTipoPAC.SelectedItem = tipo;
        }

        #endregion Métodos

    }
}
