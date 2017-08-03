using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Bascula;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.MateriaPrima
{
    /// <summary>
    /// Lógica de interacción para CapturaHumedad.xaml
    /// </summary>
    public partial class CapturaHumedad
    {
        #region PROPIEDADES
        private SerialPortManager spManager;
        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private RegistroVigilanciaHumedadInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (RegistroVigilanciaHumedadInfo)DataContext;
            }
            set { DataContext = value; }
        }

        public List<int> ProductosHumedad;

        #endregion PROPIEDADES

        #region CONSTRUCTORES

        public CapturaHumedad()
        {
            InitializeComponent();
        }

        #endregion CONSTRUCTORES

        #region EVENTOS

        private void CapturaHumedad_OnLoaded(object sender, RoutedEventArgs e)
        {
            InicializarDickeyJohn();
            InicializaContexto();
            CargarProductosValidosHumedad();
        }

        private void IudFolio_OnLostFocus(object sender, RoutedEventArgs e)
        {
            ConsultarVigilanciaPorFolio();
        }

        private void BtnBuscarFolioVigilancia_OnClick(object sender, RoutedEventArgs e)
        {
            AyudaFolioRegistroVigilancia();
        }

        private void BtnGuardar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidaGuardar())
                {
                    var registroVigilanciaHumedadBL = new RegistroVigilanciaHumedadBL();
                    registroVigilanciaHumedadBL.Guardar(Contexto);

                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.CapturaHumedad_GuardadoExito, MessageBoxButton.OK,
                                  MessageImage.Correct);
                    InicializaContexto();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.CapturaHumedad_ErrorGuardar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CapturaHumedad_Cancelar
                                                            , MessageBoxButton.YesNo, MessageImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                InicializaContexto();
            }
        }

        #endregion EVENTOS

        #region METODOS

        /// <summary>
        /// Carga el Parametro por Organizacion de los productos que aplican para capturar muestra de Humedad
        /// </summary>
        private void CargarProductosValidosHumedad()
        {
            var parametroOrganizacionPL = new ParametroOrganizacionPL();

            var parametroOrganizacionInfo = new ParametroOrganizacionInfo
            {
                Parametro = new ParametroInfo
                {
                    Clave =
                        ParametrosEnum.ProductosMuestraHumedad.
                        ToString()
                },
                Organizacion = new OrganizacionInfo
                {
                    OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario()
                }
            };

            ParametroOrganizacionInfo parametro =
                parametroOrganizacionPL.ObtenerPorOrganizacionIDClaveParametro(parametroOrganizacionInfo.Organizacion.OrganizacionID, parametroOrganizacionInfo.Parametro.Clave);

            if (parametro == null)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CapturaHumedadBusquedaFolio_SinParametro, MessageBoxButton.OK, MessageImage.Warning);

                return;
            }
            if (parametro.Valor.Contains('|'))
            {
                ProductosHumedad = (from tipos in parametro.Valor.Split('|')
                                    select Convert.ToInt32(tipos)).ToList();
            }
            else
            {
                int producto = Convert.ToInt32(parametro.Valor);
                ProductosHumedad.Add(producto);
            }
        }

        private void ConsultarVigilanciaPorFolio()
        {
            try
            {
                if (iudFolio.Value.HasValue && iudFolio.Value.Value != 0)
                {
                    var registroVigilanciaPL = new RegistroVigilanciaPL();
                    var fechaPL = new FechaPL();
                    FechaInfo fechaServidor = fechaPL.ObtenerFechaActual();
                    Contexto.RegistroVigilancia =
                        registroVigilanciaPL.ObtenerRegistroVigilanciaPorFolioTurno(Contexto.RegistroVigilancia);
                    if (Contexto.RegistroVigilancia != null)
                    {
                        if (Contexto.RegistroVigilancia.FechaLlegada < (fechaServidor.FechaActual.AddDays(-3)))
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                              Properties.Resources.CapturaHumedad_FechaInvalida, MessageBoxButton.OK,
                                              MessageImage.Warning);
                            InicializaContexto();
                            return;
                        }
                        if (!ProductosHumedad.Contains(Contexto.RegistroVigilancia.Producto.ProductoId))
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                              Properties.Resources.CapturaHumedad_ProductoNoValido, MessageBoxButton.OK,
                                              MessageImage.Warning);
                            InicializaContexto();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.CapturaHumedad_ErrorConsultarFolio, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        /// <summary>
        /// Llama ventana para ayuda de folio de traspaso
        /// </summary>
        private void AyudaFolioRegistroVigilancia()
        {
            try
            {
                var capturaHumedadBusquedaFolio = new CapturaHumedadBusquedaFolio();
                capturaHumedadBusquedaFolio.InicializaPaginador();
                capturaHumedadBusquedaFolio.Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal];
                MostrarCentrado(capturaHumedadBusquedaFolio);

                if (capturaHumedadBusquedaFolio.RegistroVigilancia != null)
                {
                    Contexto.RegistroVigilancia = capturaHumedadBusquedaFolio.RegistroVigilancia;
                    iudFolio.Value = Convert.ToInt32(Contexto.RegistroVigilancia.FolioTurno);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.CapturaHumedad_ErrorConsultarFolio, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            try
            {
                Contexto = new RegistroVigilanciaHumedadInfo
                {
                    RegistroVigilancia = new RegistroVigilanciaInfo
                                             {
                                                 Organizacion = new OrganizacionInfo
                                                                    {
                                                                        OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario()
                                                                    }
                                             },
                    UsuarioCreacionID = Auxiliar.AuxConfiguracion.ObtenerUsuarioLogueado(),
                    NumeroMuestra = 1 //TODO valor por default en espera de la definicion
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.CapturaHumedad_ErrorValoresIniciales, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        private bool ValidaGuardar()
        {
            try
            {
                if (Contexto == null || Contexto.RegistroVigilancia == null
                    || Contexto.RegistroVigilancia.FolioTurno == 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                 Properties.Resources.CapturaHumedad_FolioRegistroVigilancia, MessageBoxButton.OK,
                                 MessageImage.Warning);
                    return false;
                }

                if (Contexto.Humedad == 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                 Properties.Resources.CapturaHumedad_CapturarHumedad, MessageBoxButton.OK,
                                 MessageImage.Warning);
                    return false;
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.CapturaHumedad_ErrorGuardar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
            return true;
        }

        #endregion METODOS

        #region DICKEY JHON
        /// <summary>
        /// Método que inicializa los datos de la bascula
        /// </summary>
        private void InicializarDickeyJohn()
        {
            try
            {
                spManager = new SerialPortManager();
                spManager.NewSerialDataRecieved += (SpManagerNewSerialDataRecieved);

                if (spManager != null)
                {

                    spManager.StartListening(AuxConfiguracion.ObtenerConfiguracion().PuertoDickey,
                        int.Parse(AuxConfiguracion.ObtenerConfiguracion().DickeyBaudrate),
                        ObtenerParidad(AuxConfiguracion.ObtenerConfiguracion().DickeyParidad),
                        int.Parse(AuxConfiguracion.ObtenerConfiguracion().DickeyDataBits),
                        ObtenerStopBits(AuxConfiguracion.ObtenerConfiguracion().DickeyBitStop));
                    dudHumedad.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                dudHumedad.IsEnabled = true;
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CapturaHumedad_MsgErrorDickey,
                    MessageBoxButton.OK, MessageImage.Warning);
            }
        }

        /// <summary>
        /// Cambia la variable string en una entidad Parity
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        private Parity ObtenerParidad(string parametro)
        {
            Parity paridad;

            switch (parametro)
            {
                case "Even":
                    paridad = Parity.Even;
                    break;
                case "Mark":
                    paridad = Parity.Mark;
                    break;
                case "None":
                    paridad = Parity.None;
                    break;
                case "Odd":
                    paridad = Parity.Odd;
                    break;
                case "Space":
                    paridad = Parity.Space;
                    break;
                default:
                    paridad = Parity.None;
                    break;
            }
            return paridad;
        }

        /// <summary>
        /// Cambia la variable string en una entidad StopBit
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        private StopBits ObtenerStopBits(string parametro)
        {
            StopBits stopBit;

            switch (parametro)
            {
                case "None":
                    stopBit = StopBits.None;
                    break;
                case "One":
                    stopBit = StopBits.One;
                    break;
                case "OnePointFive":
                    stopBit = StopBits.OnePointFive;
                    break;
                case "Two":
                    stopBit = StopBits.Two;
                    break;
                default:
                    stopBit = StopBits.One;
                    break;
            }
            return stopBit;
        }
        /// <summary>
        /// Event Handler que permite recibir los datos reportados por el SerialPortManager para su utilizacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SpManagerNewSerialDataRecieved(object sender, SerialDataEventArgs e)
        {
            try
            {
                string str = Encoding.ASCII.GetString(e.Data);
                if (!string.IsNullOrWhiteSpace(str))
                {
                    var valores = str.Split(',');
                    //Se agrega validacion, ya que recibe 2 llamada de datos, una con los datos de la humedad, y otro con datos de control
                    if(valores.Count() > 7)
                    {
                        var humedad = valores[3];
                        //Aquie es para que se este reflejando la humedad del Dickey John
                        Dispatcher.BeginInvoke(new Action(() =>
                                                              {
                                                                  dudHumedad.Value = Convert.ToDecimal(humedad);
                                                                  dudHumedad.IsEnabled = false;
                                                              }), null);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        #endregion DICKEY JHON

        private void CapturaHumedad_OnUnloaded(object sender, RoutedEventArgs e)
        {
            if (spManager != null)
            {
                spManager.Dispose();
            }
        }
    }
}
