using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.Services.Info.Enums;
using Xceed.Wpf.Toolkit;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for PrecioGanadoEdicion.xaml
    /// </summary>
    public partial class PrecioGanadoEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private PrecioGanadoInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (PrecioGanadoInfo) DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        /// <summary>
        /// Control para la ayuda de Proveedor
        /// </summary>
        private SKAyuda<OrganizacionInfo> skAyudaOrganizacion;

        #endregion Propiedades

        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public PrecioGanadoEdicion()
        {
            InitializeComponent();
            InicializaContexto();
            CargarAyudas();
            CargaTiposGanado();
        }

        /// <summary>
        /// Constructor para editar una entidad PrecioGanado Existente
        /// </summary>
        /// <param name="precioGanadoInfo"></param>
        public PrecioGanadoEdicion(PrecioGanadoInfo precioGanadoInfo)
        {
           InitializeComponent();
           precioGanadoInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
           Contexto = precioGanadoInfo;
           CargarAyudas();
           CargaTiposGanado();
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
            skAyudaOrganizacion.AsignaTabIndex(0);
            skAyudaOrganizacion.AsignarFoco();

            //txtPrecioGanadoID.Focus();
            //var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
            //txtPrecioGanadoID.MoveFocus(tRequest);
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
        /// Evento que se ejecuta cuando se presiona una tecla en el Control de Importe
        /// </summary>
        private void DtuControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                //c.Focus();
                return;
            }

            string valorControl = ((DecimalUpDown)sender).Text ?? string.Empty;

            if (e.Key == Key.Decimal && valorControl.IndexOf('.') > 0)
            {
                e.Handled = true;
            }
            else if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.Decimal || e.Key == Key.OemPeriod)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se presiona una tecla en el control fechas
        /// </summary>
        private void DtpControl_PreviewKeyDown(object sender, KeyEventArgs e)
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
            Contexto =
                 new PrecioGanadoInfo
                 {
                     Organizacion = new OrganizacionInfo(),
                     TipoGanado = new TipoGanadoInfo(),
                     FechaVigencia = DateTime.Today,
                     UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
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
                if (string.IsNullOrWhiteSpace(skAyudaOrganizacion.Clave) ||
                        string.IsNullOrWhiteSpace(skAyudaOrganizacion.Descripcion))
                {
                    resultado = false;
                    mensaje = Properties.Resources.PrecioGanadoEdicion_MsgOrganizacionIDRequerida;
                    skAyudaOrganizacion.AsignarFoco();
                }
                else if (cmbTipoGanado.SelectedItem == null || Contexto.TipoGanado.TipoGanadoID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.PrecioGanadoEdicion_MsgTipoGanadoIDRequerida;
                    cmbTipoGanado.Focus();
                }

                else if (!dtuPrecio.Value.HasValue || Contexto.Precio == 0 || Validation.GetErrors(dtuPrecio).Count >0 ) //string.IsNullOrWhiteSpace(txtPrecio.Text) || txtPrecio.Text == "0")
                {                    
                    resultado = false;
                    mensaje = Validation.GetErrors(dtuPrecio).Count > 0
                                  ? Validation.GetErrors(dtuPrecio)[0].ErrorContent.ToString()
                                  : Properties.Resources.PrecioGanadoEdicion_MsgPrecioRequerida;
                    dtuPrecio.Focus();
                }
                else if (!dtpFechaVigencia.SelectedDate.HasValue)//string.IsNullOrWhiteSpace(txtFechaVigencia.Text) || txtFechaVigencia.Text == "0")
                {
                    resultado = false;
                    mensaje = Properties.Resources.PrecioGanadoEdicion_MsgFechaVigenciaRequerida;
                    dtpFechaVigencia.Focus();
                }
                else if (cmbActivo.SelectedItem == null)
                {
                    resultado = false;
                    mensaje = Properties.Resources.PrecioGanadoEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    int precioGanadoId = Extensor.ValorEntero(txtPrecioGanadoID.Text);

                    var precioGanadoPL = new PrecioGanadoPL();
                    PrecioGanadoInfo precioGanado = precioGanadoPL.ObtenerPorOrganizacionTipoGanado(Contexto);

                    if (precioGanado != null && (precioGanadoId == 0 || precioGanadoId != precioGanado.PrecioGanadoID))
                    {
                        resultado = false;
                        string organizacion = skAyudaOrganizacion.Descripcion;
                        string tipoGanado = ((TipoGanadoInfo) cmbTipoGanado.SelectedItem).Descripcion;
                        mensaje = string.Format(Properties.Resources.PrecioGanadoEdicion_MsgDescripcionExistente, organizacion, tipoGanado ,precioGanado.PrecioGanadoID);
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
                    var precioGanadoPL = new PrecioGanadoPL();
                    precioGanadoPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.PrecioGanadoID != 0)
                    {
                        confirmaSalir = false;
                        Close();                        
                    }
                    else
                    {
                        InicializaContexto();
                        skAyudaOrganizacion.AsignarFoco();
                    }
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.PrecioGanado_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this,Properties.Resources.PrecioGanado_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        /// <summary>
        /// Carga los Tipos de Ganado
        /// </summary>
        private void CargaTiposGanado()
        {
            var tipoGanadoPL = new TipoGanadoPL();
            var tipoGanado = new TipoGanadoInfo
                                 {
                                     TipoGanadoID = 0, 
                                     Descripcion = Properties.Resources.cbo_Seleccione,
                                 };
            IList<TipoGanadoInfo> listaTiposGanado = tipoGanadoPL.ObtenerTodos(EstatusEnum.Activo);
            listaTiposGanado.Insert(0, tipoGanado);
            cmbTipoGanado.ItemsSource = listaTiposGanado;
        }

        /// <summary>
        /// Metodo que carga las Ayudas
        /// </summary>
        private void CargarAyudas()
        {
            AgregarAyudaOrganizacion();
        }

        /// <summary>
        /// Método para agregar el control ayuda organización origen
        /// </summary>
        private void AgregarAyudaOrganizacion()
        {
            skAyudaOrganizacion = new SKAyuda<OrganizacionInfo>(200, false, new OrganizacionInfo()
                                                          , "PropiedadClaveCatalogoAyuda"
                                                          , "PropiedadDescripcionCatalogoAyuda", true, 50, true)
            {
                AyudaPL = new OrganizacionPL(),
                MensajeClaveInexistente = Properties.Resources.AyudaOrganizacion_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.AyudaOrganizacion_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.AyudaOrganizacion_Busqueda,
                MensajeAgregar = Properties.Resources.AyudaOrganizacion_Seleccionar,
                TituloEtiqueta = Properties.Resources.AyudaOrganizacion_LeyendaBusqueda,
                TituloPantalla = Properties.Resources.AyudaOrganizacion_Busqueda_Titulo,
            };
            skAyudaOrganizacion.AsignaTabIndex(0);
            SplAyudaOrganizacion.Children.Clear();
            SplAyudaOrganizacion.Children.Add(skAyudaOrganizacion);
        }

        #endregion Métodos
    }
}

