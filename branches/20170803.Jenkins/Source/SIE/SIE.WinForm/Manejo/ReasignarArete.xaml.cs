using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Info.Enums;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SuKarne.Controls.Bascula;
using System.Collections.Generic;
using System.Windows.Threading;

namespace SIE.WinForm.Manejo
{
    /// <summary>
    /// Interaction logic for ReasignarArete.xaml
    /// </summary>
    public partial class ReasignarArete : Window
    {

        #region Atributos
        private int organizacionID;
        private int usuarioLogueadoID;
        private bool guardado;
        private bool CtrlPegar;
        private string TextoAnterior;
        private string rfidCapturadoGlobal;

        public int TrampaID { get; set; }

        private SerialPortManager spManagerRFID;
        private BasculaCorteSection configRFID;
        private bool rfidConectado = false;
        private bool CapturaObligatoriaRFID = false;
        public string RfidNuevo {get; set;}
        #endregion
       
        #region Constructor
        public ReasignarArete()
        {
            InitializeComponent();
            organizacionID = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
            usuarioLogueadoID = Extensor.ValorEntero(Application.Current.Properties["UsuarioID"].ToString());
            Limpiar(true);
            txtNumeroIndividual.Focus();
            guardado = false;
            CtrlPegar = false;
            TextoAnterior = string.Empty;
        }
        #endregion

        #region Eventos


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var parametrosPL = new ConfiguracionParametrosPL();
            var parametroSolicitado = new ConfiguracionParametrosInfo
            {
                TipoParametro = (int)TiposParametrosEnum.DispositivoRFID,
                OrganizacionID = organizacionID
            };

            var parametroSolicitadoCaptura = new ConfiguracionParametrosInfo
            {
                Clave = ParametrosEnum.CapturaObligatoriaRFID.ToString(),
                TipoParametro = (int)TiposParametrosEnum.ObligatorioAreteRFID,
                OrganizacionID = organizacionID
            };

            var parametro = parametrosPL.ObtenerPorOrganizacionTipoParametroClave(parametroSolicitadoCaptura);
            if (parametro != null)
                CapturaObligatoriaRFID = bool.Parse(parametro.Valor);

            var parametros = parametrosPL.ParametroObtenerPorTrampaTipoParametro(parametroSolicitado, TrampaID);
            configRFID = ObtenerParametroDispositivo(parametros);

            spManagerRFID = new SerialPortManager();

            spManagerRFID.NewSerialDataRecieved += (spManager_NewSerialDataRecievedRFID);

            if (!rfidConectado) InicializarLectorRFID();


        }
 
        /// <summary>
        /// Event Handler que permite recibir los datos reportados por el SerialPortManager para su utilizacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void spManager_NewSerialDataRecievedRFID(object sender, SerialDataEventArgs e)
        {
            try
            {
                string strEnd = spManagerRFID.ObtenerLeturaRFID(e.Data);
                if (strEnd.Trim() == string.Empty) return;

                rfidCapturadoGlobal = strEnd;
                //Aquie es para que se este reflejando la lectura en el display
                Dispatcher.BeginInvoke(new Action(CapturarAreteRFID),
                    DispatcherPriority.Background);
            }
            catch (FormatException ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.CorteGanado_ErrorCapturaRFID,
                                      MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.CorteGanado_ErrorCapturaRFID,
                                      MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Metodo que se ejecuta cuando el lector RFID obtiene una lectura de arete
        /// </summary>
        private void CapturarAreteRFID()
        {

            int index = -1;
            int posision = 0;
            foreach (Window ventana in Application.Current.Windows)
            {
                if (ventana.Name.IndexOf("ReasignarAreteWindow") >= 0)
                {
                    index = posision;
                   
                }
                posision++;
            }

            if (index >= 0 && Application.Current.Windows[index].IsActive)
            {
                txtAreteMetalico.Text = rfidCapturadoGlobal;
                ValidarAreteTestigoRegistrado();
            }
        }

        /// <summary>
        /// Evento closing de Corte de ganado 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, RoutedEventArgs e)
        {
            try
            {
                if (rfidConectado)
                    spManagerRFID.Dispose();
            }
            catch (Exception)
            {
            }
        }


        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ReasignarAreteMetalico(1))
                {
                    spManagerRFID.Dispose();
                    Close();
                } spManagerRFID.Dispose();
            }
            catch (Exception)
            {
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (guardado == false)
            {

                if (String.IsNullOrEmpty(txtAreteMetalico.Text) == false ||
                    String.IsNullOrEmpty(txtNumeroIndividual.Text) == false)
                {
                    if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.Cancelarcaptura_ReasignacionArete,
                        MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
                    {
                        Limpiar(true);
                        spManagerRFID.Dispose();
                        Close();
                    }
                }
                else
                {
                    Limpiar(true);
                    spManagerRFID.Dispose();
                    Close();
                }
            }
            else
            {
                Limpiar(true);
                spManagerRFID.Dispose();
                Close();
            }
           
        }

        private void btnReasignar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar(false);
            ReasignarAreteMetalico(0);
            
        }

        private void txtNumeroIndividual_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != Key.Enter && e.Key != Key.Tab) return;
            if (!string.IsNullOrEmpty(txtNumeroIndividual.Text))
            {
                txtAreteMetalico.Focus();
            }

        }

        private void txtAreteMetalico_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter && e.Key != Key.Tab) return;
            if (!String.IsNullOrEmpty(txtAreteMetalico.Text))
            {
                btnReasignar.Focus();
            }

        }
        #endregion

        #region Metodos
        private void Limpiar(Boolean todo)
        {
            if (todo)
            {
                txtNumeroIndividual.Text = "";
                txtAreteMetalico.Text = "";
            }
           
            txtCorralOrigen.Text = "";
            txtCorralOrigen.IsReadOnly = true;
            txtPesoCorte.Text = "";
            txtPesoCorte.IsReadOnly = true;
            txtOrigen.Text = "";
            txtOrigen.IsReadOnly = true;
            txtProveedor.Text = "";
            txtProveedor.IsReadOnly = true;
            txtTipoAnimal.Text = "";
            txtTipoAnimal.IsReadOnly = true;
            dtpFechaInicio.SelectedDate = DateTime.Now;
            dtpFechaInicio.IsEnabled = false;
            LimpiarBordes();
            guardado = false;
        }

        private void LimpiarBordes()
        {
            txtNumeroIndividual.BorderBrush = txtProveedor.BorderBrush;
            txtAreteMetalico.BorderBrush = txtProveedor.BorderBrush;
        }
        private void MostrarDatos(AnimalInfo animalInfo, DatosCompra datosCompra)
        {
            //txtNumeroIndividual.Text = animalInfo.Arete;
            //txtAreteMetalico.Text = animalInfo.AreteMetalico;
            txtCorralOrigen.Text = animalInfo.Corral;
            txtPesoCorte.Text = animalInfo.PesoAlCorte.ToString();
            if (datosCompra != null)
            {
                dtpFechaInicio.SelectedDate = datosCompra.FechaInicio;
                txtOrigen.Text = datosCompra.Origen;
                txtProveedor.Text = datosCompra.Proveedor;
                txtTipoAnimal.Text = datosCompra.TipoAnimal;
            }
            LimpiarBordes();
        }

       

        private bool ValidaGuardar()
        {
            bool resultado = true;
            var linearBrush = new LinearGradientBrush();
            try
            {
                linearBrush.GradientStops.Add(new GradientStop(Colors.Red, 0.0));
                if (txtNumeroIndividual.Text == string.Empty)
                {
                    resultado = false;
                    txtNumeroIndividual.BorderBrush = linearBrush;
                }
                if (txtAreteMetalico.Text == string.Empty)
                {
                    resultado = false;
                    txtAreteMetalico.BorderBrush = linearBrush;
                }
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        private bool ReasignarAreteMetalico(int banderaGuardar)
        {
            LimpiarBordes();
            bool guardar = ValidaGuardar();
            RfidNuevo = "";
            if (guardar)
            {
                try
                {
                    if (banderaGuardar > 0)
                    {
                        bool areteYaExiste = ValidarAreteTestigoRegistrado();
                        if (areteYaExiste)
                        {
                            return false;
                        }
                    }

                    var animalInfo = new AnimalInfo();
                    animalInfo.Arete = txtNumeroIndividual.Text;
                    animalInfo.AreteMetalico = txtAreteMetalico.Text;
                    animalInfo.OrganizacionIDEntrada = organizacionID;
                    animalInfo.UsuarioModificacionID = usuarioLogueadoID;
                    var reimplantePL = new ReimplantePL();
                    var datosCompra = new DatosCompra();
                    animalInfo = reimplantePL.ReasignarAreteMetalico(animalInfo, banderaGuardar);
                    if (animalInfo != null)
                    {
                        datosCompra = reimplantePL.ObtenerDatosCompra(animalInfo);
                        MostrarDatos(animalInfo, datosCompra);
                        if (banderaGuardar == 1)
                        {
                            RfidNuevo = txtAreteMetalico.Text;
                            SkMessageBox.Show(this, Properties.Resources.ReasignacionArete_GuardadoExito, MessageBoxButton.OK, MessageImage.Correct);
                            guardado = true;
                            Limpiar(true);
                        }
                        else
                        {
                            btnGuardar.Focus();
                        }
                        
                    }
                    else
                    {
                        SkMessageBox.Show(this, Properties.Resources.ReimplanteGanado_ReasignarAreteDatosIncorrectos, MessageBoxButton.OK, MessageImage.Warning);
                    }


                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.ReimplanteGanado_Error_ReasignarArete, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.ReimplanteGanado_Error_ReasignarArete, MessageBoxButton.OK, MessageImage.Error);
                }
            }
            else
            {
                SkMessageBox.Show(this, Properties.Resources.Reimplante_DatosEnBlanco, MessageBoxButton.OK, MessageImage.Warning);
            }
            return guardar;
        }

        /// <summary>
        /// Inicializa el lector RFID cuando el formulario esta listo para captura
        /// </summary>
        private void InicializarLectorRFID()
        {
            try
            {

                if (spManagerRFID != null)
                {
                    txtAreteMetalico.IsEnabled = false;
                    spManagerRFID.StartListening(configRFID.Puerto,
                        configRFID.Baudrate,
                        configRFID.Paridad,
                        configRFID.Databits,
                        configRFID.BitStop);
                    rfidConectado = true;
                }
                else
                {
                    rfidConectado = false;
                    txtAreteMetalico.IsEnabled = true;
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            VerificarPermisosCapturaManual();
        }

        /// <summary>
        /// Verifica si el equipo no tiene lector conectado, si es asi y tiene permisos de captura manual activa el txtAreteMetalico
        /// </summary>
        private void VerificarPermisosCapturaManual()
        {
            //Si el Lector RFID estan desconectados se verifica si se tiene permiso para capturar manualmente
            if (rfidConectado == false)
            {
                if (configRFID == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteTransferenciaGanado_PermisoCapturaManualRFID,
                    MessageBoxButton.OK,
                    MessageImage.Warning);
                }
                else
                {

                    txtAreteMetalico.IsEnabled = true;
                    if (!configRFID.CapturaManual)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.CorteTransferenciaGanado_PermisoCapturaManualRFID,
                        MessageBoxButton.OK,
                        MessageImage.Warning);
                    }
                }
            }
        }

        /// <summary>
        /// Funciona para mapear la seccion de parametros de configuracion del dispositivo
        /// </summary>
        /// <param name="parametros"></param>
        /// <returns></returns>
        private BasculaCorteSection ObtenerParametroDispositivo(IList<ConfiguracionParametrosInfo> parametros)
        {
            try
            {
                var parametroResultado = new BasculaCorteSection();
                foreach (var parametro in parametros)
                {
                    ParametrosDispositivosEnum enumTemporal;
                    var res = ParametrosDispositivosEnum.TryParse(parametro.Clave, out enumTemporal);
                    if (res)
                    {
                        switch (enumTemporal)
                        {
                            case ParametrosDispositivosEnum.puerto:
                                parametroResultado.Puerto = parametro.Valor;
                                break;
                            case ParametrosDispositivosEnum.baudrate:
                                parametroResultado.Baudrate = int.Parse(parametro.Valor);
                                break;
                            case ParametrosDispositivosEnum.databits:
                                parametroResultado.Databits = int.Parse(parametro.Valor);
                                break;
                            case ParametrosDispositivosEnum.paridad:
                                parametroResultado.cambiarParidad(parametro.Valor);
                                break;
                            case ParametrosDispositivosEnum.bitstop:
                                parametroResultado.cambiarBitStop(parametro.Valor);
                                break;
                            case ParametrosDispositivosEnum.espera:
                                parametroResultado.Espera = int.Parse(parametro.Valor);
                                break;
                            case ParametrosDispositivosEnum.CapturaManual:
                                parametroResultado.CapturaManual = bool.Parse(parametro.Valor);
                                break;
                        }
                    }
                }
                return parametroResultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// Metodo para validar si un Arete Metalico ya se encuentra registrado en la organizacion
        /// </summary>
        /// <returns></returns>
        private bool ValidarAreteTestigoRegistrado()
        {
            var resultadoValidacion = ComprobarAreteRegistrado("", txtAreteMetalico.Text);
            if (resultadoValidacion.Resultado)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.CorteGanado_AreteRFIDRegistrado, MessageBoxButton.OK, MessageImage.Stop);
                txtAreteMetalico.Text = "";
                return true;
            }
            return false;
        }

        /// <summary>
        /// Comprueba si el arete o arete metalico no se encuentra registrado.
        /// </summary>
        /// <param name="arete"></param>
        /// <param name="areteMetalico"></param>
        /// <returns></returns>
        private ResultadoValidacion ComprobarAreteRegistrado(string arete, string areteMetalico)
        {
            var animalPl = new AnimalPL();
            var resultado = new ResultadoValidacion();
            try
            {
                resultado.Resultado = animalPl.VerificarExistenciaArete(arete, areteMetalico, organizacionID);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return resultado;
        }

        #endregion

        private void txtNumeroIndividual_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                CtrlPegar = true;
                TextoAnterior = txtNumeroIndividual.Text;
            }
            if (e.Key == Key.Insert && Keyboard.Modifiers == ModifierKeys.Shift)
            {
                CtrlPegar = true;
                TextoAnterior = txtNumeroIndividual.Text;
            }
        }

        private void txtNumeroIndividual_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (Extensor.ValidarNumeroYletras(txtNumeroIndividual.Text))
            {
                CtrlPegar = false;
                txtNumeroIndividual.Text = txtNumeroIndividual.Text.Replace(" ", "");
            }
            else
            {
                if (CtrlPegar)
                {
                    txtNumeroIndividual.Text = TextoAnterior;
                    CtrlPegar = false;
                }
            }
 
        }

        private void txtAreteMetalico_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                CtrlPegar = true;
                TextoAnterior = txtAreteMetalico.Text;
            }
            if (e.Key == Key.Insert && Keyboard.Modifiers == ModifierKeys.Shift)
            {
                CtrlPegar = true;
                TextoAnterior = txtAreteMetalico.Text;
            }
        }

        private void txtAreteMetalico_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (Extensor.ValidarNumeroYletras(txtAreteMetalico.Text))
            {
                CtrlPegar = false;
                txtAreteMetalico.Text = txtAreteMetalico.Text.Replace(" ", "");
            }
            else
            {
                if (CtrlPegar)
                {
                    txtAreteMetalico.Text = TextoAnterior;
                    CtrlPegar = false;
                }
            }

        }

        private void txtNumeroIndividual_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumeros(e.Text);
        }

        private void txtAreteMetalico_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumeros(e.Text);
        }

        

    }
}
