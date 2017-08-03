using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.Services.Servicios.BL;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para CuentaSAPEdicion.xaml
    /// </summary>
    public partial class CuentaSAPEdicion
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



        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        #endregion Propiedades

        #region Constructores

        public CuentaSAPEdicion(CuentaSAPInfo cuentaSAPInfo)
        {
            InitializeComponent();
            CargaComboTipoCuenta();
            cuentaSAPInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto = cuentaSAPInfo;
        }

        #endregion Constructores

        #region Eventos

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


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbTipoCuenta.Focus();
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion Eventos

        #region Metodos

        private bool ValidaGuardar()
        {
            if (Contexto.TipoCuenta == null || Contexto.TipoCuenta.TipoCuentaID == 0)
            {
                SkMessageBox.Show(this, Properties.Resources.CuentaSAPEdicion_SeleccioneTipoCuenta, MessageBoxButton.OK,
                                                       MessageImage.Warning);
                return false;
            }
            if(string.IsNullOrWhiteSpace(Contexto.CuentaSAP))
            {
                SkMessageBox.Show(this, Properties.Resources.CuentaSAPEdicion_CaptureCuenta, MessageBoxButton.OK,
                                                      MessageImage.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new CuentaSAPInfo
            {
                UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                TipoCuenta = new TipoCuentaInfo()
            };
        }

        /// <summary>
        /// Metodo para guardar la Cuenta SAP
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
                SkMessageBox.Show(this, Properties.Resources.CuentaSAPEdicion_ErrorGuardar, MessageBoxButton.OK,
                                                            MessageImage.Error);
            }
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
