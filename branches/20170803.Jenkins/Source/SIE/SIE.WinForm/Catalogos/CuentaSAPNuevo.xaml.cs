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
    /// Lógica de interacción para CuentaSAPNuevo.xaml
    /// </summary>
    public partial class CuentaSAPNuevo
    {
        #region Propiedades
        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private CuentaSAPInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (CuentaSAPInfo)DataContext;
            }
            set { DataContext = value; }
        }

        private const string PlanCuenta = "GVIZ";
        private const string SociedadDefault = "212";

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        #endregion Propiedades

        #region Constructor

        public CuentaSAPNuevo()
        {
            InitializeComponent();
            CargaComboTipoCuenta();
            InicializaContexto();
        }

        #endregion Constructor

        #region Eventos

        private void txtIdCuentaSAP_KeyDown(object sender, KeyEventArgs e)
        {
            btnGuardar.IsEnabled = false;
            if(e.Key == Key.Enter)
            {
                Buscar();
            }
        }

        private void txtCuentaSAP_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Buscar();
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtCuentaSAP.Focus();
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

        private void TxtCuentaSAP_OnKeyUp(object sender, KeyEventArgs e)
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
            Contexto = new CuentaSAPInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                Sociedad = SociedadDefault,
                TipoCuenta = new TipoCuentaInfo()
            };
        }

        /// <summary>
        /// Metodo para buscar la Cuenta en SAP
        /// </summary>
        private void Buscar()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Contexto.Sociedad) && !string.IsNullOrWhiteSpace(Contexto.CuentaSAP))
                {
                    var cuentaSAPPL = new CuentaSAPPL();
                    Contexto.CuentaSAP = Contexto.CuentaSAP.PadLeft(10, '0');

                    CuentaSAPInfo cuentaSAPExiste = cuentaSAPPL.ObtenerPorCuentaSAP(Contexto);

                    if (cuentaSAPExiste != null && cuentaSAPExiste.CuentaSAPID > 0)
                    {
                        SkMessageBox.Show(this, Properties.Resources.CuentaSAPNuevo_CuentaExiste, MessageBoxButton.OK,
                                          MessageImage.Warning);
                        return;
                    }
                    Contexto.PlanCuenta = PlanCuenta;
                    Contexto = cuentaSAPPL.ObtenerCuentaSAPInterfaz(Contexto);

                    if (Contexto == null)
                    {
                        SkMessageBox.Show(this, Properties.Resources.CuentaSAPNuevo_CuentaNoExiste, MessageBoxButton.OK,
                                          MessageImage.Warning);
                        InicializaContexto();
                        return;
                    }
                    if (Contexto.Bloqueada)
                    {
                        SkMessageBox.Show(this, Properties.Resources.CuentaSAPNuevo_CuentaBloqueada, MessageBoxButton.OK,
                                          MessageImage.Warning);
                        return;
                    }
                    btnGuardar.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.CuentaSAPNuevo_ErrorBuscar, MessageBoxButton.OK,
                                                            MessageImage.Error);
            }
        }
        /// <summary>
        /// Metodo para guardar la Cuenta de SAP
        /// </summary>
        private void Guardar()
        {
            try
            {
                if (ValidaGuardar())
                {
                    var cuentaSAPPL = new CuentaSAPPL();
                    cuentaSAPPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK,
                                      MessageImage.Correct);
                    confirmaSalir = false;
                    Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.CuentaSAPNuevo_ErrorGuardar, MessageBoxButton.OK,
                                                            MessageImage.Error);
            }
        }

        private bool ValidaGuardar()
        {
            if (string.IsNullOrWhiteSpace(Contexto.CuentaSAP))
            {
                SkMessageBox.Show(this, Properties.Resources.CuentaSAPNuevo_CapturarCuenta, MessageBoxButton.OK,
                                                       MessageImage.Warning);
                return false;
            }
            if(Contexto.TipoCuenta == null || Contexto.TipoCuenta.TipoCuentaID == 0)
            {
                SkMessageBox.Show(this, Properties.Resources.CuentaSAPNuevo_SeleccioneTipoCuenta, MessageBoxButton.OK,
                                                       MessageImage.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Carga los datos de la entidad Tipo Cuenta 
        /// </summary>
        private void CargaComboTipoCuenta()
        {
            try
            {
                var tipoCuentaPL = new TipoCuentaBL();
                var tipoCuenta = new TipoCuentaInfo
                {
                    TipoCuentaID = 0,
                    Descripcion = Properties.Resources.cbo_Seleccione,
                };
                IList<TipoCuentaInfo> listaTipoCuenta = tipoCuentaPL.ObtenerTodos(EstatusEnum.Activo);
                listaTipoCuenta.Insert(0, tipoCuenta);
                cmbTipoCuenta.ItemsSource = listaTipoCuenta;
                cmbTipoCuenta.SelectedItem = tipoCuenta;
                if (Contexto.TipoCuenta == null || Contexto.TipoCuenta.TipoCuentaID == 0)
                {
                    cmbTipoCuenta.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        #endregion Metodos

      
    }
}

