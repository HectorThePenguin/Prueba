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
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using Xceed.Wpf.Toolkit;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for ConfiguracionEmbarqueEdicion.xaml
    /// </summary>
    public partial class ConfiguracionEmbarqueEdicion
    {
        #region Propiedades

        private ConfiguracionEmbarqueInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                   InicializaContexto();
                }
                return (ConfiguracionEmbarqueInfo) DataContext;
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
        private SKAyuda<OrganizacionInfo> skAyudaOrigen;

        /// <summary>
        /// Control para la ayuda de Proveedor
        /// </summary>
        private SKAyuda<OrganizacionInfo> skAyudaDestino;

        #endregion Propiedades

        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public ConfiguracionEmbarqueEdicion()
        {
            InitializeComponent();
            InicializaContexto();
            CargarAyudas();
        }

        /// <summary>
        /// Constructor para editar una entidad ConfiguracionEmbarque Existente
        /// </summary>
        /// <param name="configuracionEmbarqueInfo"></param>
        public ConfiguracionEmbarqueEdicion(ConfiguracionEmbarqueInfo configuracionEmbarqueInfo)
        {
            InitializeComponent();
            configuracionEmbarqueInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto = configuracionEmbarqueInfo;

            CargarAyudas();
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
            skAyudaOrigen.AsignarFoco();
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
        /// Evento para regresar a la pantalla anterior
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

        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new ConfiguracionEmbarqueInfo
                           {
                               OrganizacionOrigen = new OrganizacionInfo(),
                               OrganizacionDestino = new OrganizacionInfo(),
                               UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                               Activo = EstatusEnum.Activo
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
                if (string.IsNullOrWhiteSpace(skAyudaOrigen.Clave)
                    || string.IsNullOrWhiteSpace(skAyudaOrigen.Descripcion)
                    || skAyudaOrigen.Clave == "0")
                {
                    resultado = false;
                    mensaje = Properties.Resources.ConfiguracionEmbarqueEdicion_MsgOrganizacionOrigen;
                    skAyudaOrigen.AsignarFoco();
                }
                else if (string.IsNullOrWhiteSpace(skAyudaDestino.Clave)
                         || string.IsNullOrWhiteSpace(skAyudaDestino.Descripcion)
                         || skAyudaDestino.Clave == "0")
                {
                    resultado = false;
                    mensaje = Properties.Resources.ConfiguracionEmbarqueEdicion_MsgOrganizacionDestino;
                    skAyudaDestino.AsignarFoco();
                }
                else if (skAyudaOrigen.Clave == skAyudaDestino.Clave)
                {
                    resultado = false;
                    mensaje = Properties.Resources.ProgramacionEmbarque_DestinoIgualOrigen;
                    txtConfiguracionEmbarqueId.Focus();
                    var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                    txtConfiguracionEmbarqueId.MoveFocus(tRequest);
                }
                else if (!dtuKilometros.Value.HasValue || string.IsNullOrWhiteSpace(dtuKilometros.Text) ||
                         dtuKilometros.Text == "0")
                {
                    resultado = false;
                    mensaje = Properties.Resources.ConfiguracionEmbarqueEdicion_MsgKilometrosRequerida;
                    dtuKilometros.Focus();
                }
                else if (!dtuHoras.Value.HasValue || string.IsNullOrWhiteSpace(dtuHoras.Text) ||
                         dtuHoras.Text == "0.0")
                {
                    resultado = false;
                    mensaje = Properties.Resources.ConfiguracionEmbarqueEdicion_MsgHorasRequerida;
                    dtuHoras.Focus();
                }
                else
                {
                    int configuracionEmbarqueId = Extensor.ValorEntero(txtConfiguracionEmbarqueId.Text);
                    int organizacionOrigenId = Extensor.ValorEntero(skAyudaOrigen.Clave);
                    int organizacionDestinoId = Extensor.ValorEntero(skAyudaDestino.Clave);

                    var configuracionEmbarquePL = new ConfiguracionEmbarquePL();
                    ConfiguracionEmbarqueInfo configuracionEmbarque =
                        configuracionEmbarquePL.ObtenerPorOrganizacion(organizacionOrigenId,
                                                                       organizacionDestinoId);

                    if (configuracionEmbarque != null &&
                        (configuracionEmbarqueId == 0 ||
                         configuracionEmbarqueId != configuracionEmbarque.ConfiguracionEmbarqueID))
                    {
                        resultado = false;
                        mensaje =
                            string.Format(
                                Properties.Resources.ConfiguracionEmbarqueEdicion_MsgConfiguracionExistente,
                                configuracionEmbarque.ConfiguracionEmbarqueID);
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
                    var configuracionEmbarquePL = new ConfiguracionEmbarquePL();
                    configuracionEmbarquePL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK,
                                      MessageImage.Correct);
                    if (Contexto.ConfiguracionEmbarqueID != 0)
                    {
                        confirmaSalir = false;
                        Close();    
                    }
                    else
                    {
                        InicializaContexto();
                        skAyudaOrigen.AsignarFoco();
                    }                    
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.ConfiguracionEmbarque_ErrorGuardar, MessageBoxButton.OK,
                                      MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.ConfiguracionEmbarque_ErrorGuardar, MessageBoxButton.OK,
                                      MessageImage.Error);
                }
            }
        }

        /// <summary>
        /// Metodo que carga las Ayudas
        /// </summary>
        private void CargarAyudas()
        {
            AgregarAyudaOrganizacionOrigen();
            AgregarAyudaOrganizacionDestino();
        }

        /// <summary>
        /// Método para agregar el control ayuda organización origen
        /// </summary>
        private void AgregarAyudaOrganizacionOrigen()
        {
            skAyudaOrigen = new SKAyuda<OrganizacionInfo>(160, false, new OrganizacionInfo()
                                                          , "PropiedadClaveCatalogoAyudaOrigen"
                                                          , "PropiedadDescripcionCatalogoAyudaOrigen", true, 50, false)
                                {
                                    AyudaPL = new OrganizacionPL(),
                                    MensajeClaveInexistente = Properties.Resources.Origen_CodigoInvalido,
                                    MensajeBusquedaCerrar = Properties.Resources.Origen_SalirSinSeleccionar,
                                    MensajeBusqueda = Properties.Resources.Origen_Busqueda,
                                    MensajeAgregar = Properties.Resources.Origen_Seleccionar,
                                    TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaOrigen,
                                    TituloPantalla = Properties.Resources.BusquedaOrigen_Titulo,
                                };
            skAyudaOrigen.LlamadaMetodos += ValidaOrigenYdestino;
            skAyudaOrigen.ObtenerDatos += ValidaOrganizacionesIguales;
            skAyudaOrigen.AsignaTabIndex(1);
            stpOrigen.Children.Clear();
            stpOrigen.Children.Add(skAyudaOrigen);
        }

        /// <summary>
        /// Configura la ayuda para ligarlo con la organización origen
        /// </summary>
        private void AgregarAyudaOrganizacionDestino()
        {
            skAyudaDestino = new SKAyuda<OrganizacionInfo>(160, false, new OrganizacionInfo()
                , "PropiedadClaveCatalogoAyudaDestino"
                , "PropiedadDescripcionCatalogoAyudaDestino", true, 50, false)
            {
                AyudaPL = new OrganizacionPL(),
                MensajeClaveInexistente = Properties.Resources.Destino_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.Destino_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Destino_Busqueda,
                MensajeAgregar = Properties.Resources.Destino_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaDestino,
                TituloPantalla = Properties.Resources.BusquedaDestino_Titulo,
            };
            skAyudaDestino.LlamadaMetodos += ValidaOrigenYdestino;
            skAyudaDestino.ObtenerDatos += ValidaOrganizacionesIguales;

            skAyudaDestino.AsignaTabIndex(2);
            stpDestino.Children.Clear();
            stpDestino.Children.Add(skAyudaDestino);
        }

        /// <summary>
        /// Método que valida las reglas de negocio de las organizaciones de Destino
        /// </summary>
        private void ValidaOrigenYdestino()
        {
            if (string.IsNullOrWhiteSpace(skAyudaOrigen.Clave) || skAyudaOrigen.Clave == "0")
            {
                return;
            }

            if (skAyudaOrigen.Clave.Equals(skAyudaDestino.Clave))
            {
                SkMessageBox.Show(this, Properties.Resources.RegistroProgramacionEmbarque_DestinoDuplicado,
                                  MessageBoxButton.OK, MessageImage.Stop);

                skAyudaDestino.LimpiarCampos();
                var tRequest = new TraversalRequest(FocusNavigationDirection.Previous);
                skAyudaDestino.MoveFocus(tRequest);
            }
        }

        /// <summary>
        /// Carga la organización destino dependiendo del tipo de movimiento
        /// </summary>
        /// <returns></returns>
        private void ValidaOrganizacionesIguales(string clave)
        {
            if (string.IsNullOrWhiteSpace(skAyudaOrigen.Clave) || skAyudaOrigen.Clave == "0")
            {
                return;
            }

            bool destino = skAyudaOrigen.Clave == skAyudaDestino.Clave;

            if (destino)
            {
                SkMessageBox.Show(this, Properties.Resources.ProgramacionEmbarque_DestinoIgualOrigen, MessageBoxButton.OK,
                                  MessageImage.Stop);

                skAyudaDestino.LimpiarCampos();
                var tRequest = new TraversalRequest(FocusNavigationDirection.Previous);
                skAyudaDestino.MoveFocus(tRequest);
            }
        }

        #endregion Métodos
    }
}

