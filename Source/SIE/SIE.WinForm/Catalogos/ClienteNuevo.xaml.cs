using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para ClienteNuevo.xaml
    /// </summary>
    public partial class ClienteNuevo
    {
        #region Propiedades
        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private ClienteInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ClienteInfo)DataContext;
            }
            set { DataContext = value; }
        }

        private const string SociedadDefault = "212";

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        #endregion Propiedades

        #region Constructor

        public ClienteNuevo()
        {
            InitializeComponent();
            InicializaContexto();
            CargaComboMetodoPago();
        }

        #endregion Constructor

        #region Eventos

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtCliente.Focus();
        }

        private void txtCliente_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        private void txtCliente_KeyDown(object sender, KeyEventArgs e)
        {
            btnGuardar.IsEnabled = false;
            if(e.Key == Key.Back)
            {
                btnGuardar.IsEnabled = false;    
            }
            if (e.Key == Key.Enter)
            {
                Buscar();
            }
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Buscar();
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
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

        private void TxtCliente_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                btnGuardar.IsEnabled = false;
            }
        }

        #endregion Eventos

        #region Metodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new ClienteInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                Sociedad = SociedadDefault,
                MetodoPago = new MetodoPagoInfo()
            };
        }

        /// <summary>
        /// Carga los datos de la entidad Tipo Cuenta 
        /// </summary>
        private void CargaComboMetodoPago()
        {
            try
            {
                var metodoPagoBL = new MetodoPagoBL();
                var metodoPago = new MetodoPagoInfo
                {
                    MetodoPagoID = 0,
                    Descripcion = Properties.Resources.cbo_Seleccione,
                };
                IList<MetodoPagoInfo> listaMetodoPago = metodoPagoBL.ObtenerTodos(EstatusEnum.Activo);
                listaMetodoPago.Insert(0, metodoPago);
                cmbMetodoPago.ItemsSource = listaMetodoPago;
                cmbMetodoPago.SelectedItem = metodoPago;
                if (Contexto.MetodoPago == null || Contexto.MetodoPago.MetodoPagoID == 0)
                {
                    cmbMetodoPago.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.ClienteNuevo_ErrorMetodoPago, MessageBoxButton.OK,
                                                            MessageImage.Error);
            }
        }

        private void Guardar()
        {
            try
            {
                if (ValidaGuardar())
                {
                    var clientePL = new ClientePL();
                    clientePL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK,
                                                           MessageImage.Correct);
                    confirmaSalir = false;
                    Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.ClienteNuevo_ErrorGuardar, MessageBoxButton.OK,
                                                            MessageImage.Error);
            }
        }

        private bool ValidaGuardar()
        {
            if(string.IsNullOrWhiteSpace(Contexto.CodigoSAP))
            {
                SkMessageBox.Show(this, Properties.Resources.ClienteNuevo_CapturaCodigoSAP, MessageBoxButton.OK,
                                                           MessageImage.Warning);
                return false;
            }
            if(string.IsNullOrWhiteSpace(Contexto.DiasPago))
            {
                SkMessageBox.Show(this, Properties.Resources.ClienteNuevo_CapturaDiasPago, MessageBoxButton.OK,
                                                         MessageImage.Warning);
                return false;
            }
            return true;
        }

        private void Buscar()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Contexto.Sociedad) && !string.IsNullOrWhiteSpace(Contexto.CodigoSAP))
                {
                    Contexto.CodigoSAP = Contexto.CodigoSAP.PadLeft(10, '0');
                    var clientePL = new ClientePL();

                    ClienteInfo clienteExiste = clientePL.ObtenerClientePorCliente(Contexto);
                    MessageBoxResult resultado = MessageBoxResult.No;
                    if(clienteExiste != null && clienteExiste.ClienteID > 0)
                    {
                        resultado = SkMessageBox.Show(this, Properties.Resources.ClienteNuevo_ClienteExiste, MessageBoxButton.YesNo,
                                                           MessageImage.Question);
                        if (resultado == MessageBoxResult.No)
                        {
                            return;
                        }
                    }

                    Contexto = clientePL.ObtenerClienteSAP(Contexto);
                    if(Contexto == null || string.IsNullOrWhiteSpace(Contexto.CodigoSAP))
                    {
                        SkMessageBox.Show(this, Properties.Resources.ClienteNuevo_ClienteNoEncontrado, MessageBoxButton.OK,
                                                            MessageImage.Warning);
                        return;
                    }
                    if(Contexto.Bloqueado)
                    {
                        SkMessageBox.Show(this, Properties.Resources.ClienteNuevo_ClienteBloqueado, MessageBoxButton.OK,
                                                            MessageImage.Warning);
                        return;
                    }
                    if (resultado == MessageBoxResult.Yes && clienteExiste != null)
                    {
                        Contexto.ClienteID = clienteExiste.ClienteID;
                        Contexto.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
                    }
                    btnGuardar.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.ClienteNuevo_ErrorBuscar, MessageBoxButton.OK,
                                                            MessageImage.Error);
            }
        }

        #endregion Metodos

        
    }
}

