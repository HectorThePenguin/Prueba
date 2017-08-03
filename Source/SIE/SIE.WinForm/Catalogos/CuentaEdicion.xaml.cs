using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using SIE.Base.Exepciones;
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
    /// Interaction logic for CuentaEdicion.xaml
    /// </summary>
    public partial class CuentaEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private CuentaInfo Contexto
        {
             get
              {
                  if (DataContext == null)
                  {
                     InicializaContexto();
                  }
                  return (CuentaInfo) DataContext;
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
        public CuentaEdicion()
        {
           InitializeComponent();
           InicializaContexto();
           CargaComboTipoCuenta();
        }

        /// <summary>
        /// Constructor para editar una entidad Cuenta Existente
        /// </summary>
        /// <param name="cuentaInfo"></param>
        public CuentaEdicion(CuentaInfo cuentaInfo)
        {
           InitializeComponent();
           CargaComboTipoCuenta();
           cuentaInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
           Contexto = cuentaInfo;
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
        /// Evento para cerrar la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion Eventos

        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new CuentaInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                TipoCuenta = new TipoCuentaInfo()
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
                if (string.IsNullOrWhiteSpace(txtCuentaID.Text) )
                {
                    resultado = false;
                    mensaje = Properties.Resources.CuentaEdicion_MsgCuentaIDRequerida;
                    txtCuentaID.Focus();
                }
                else if (string.IsNullOrWhiteSpace(txtDescripcion.Text) || Contexto.Descripcion == string.Empty)
                {
                    resultado = false;
                    mensaje = Properties.Resources.CuentaEdicion_MsgDescripcionRequerida;
                    txtDescripcion.Focus();
                }
                else if (cmbTipoCuenta.SelectedItem == null || Contexto.TipoCuenta.TipoCuentaID == 0 )
                {
                    resultado = false;
                    mensaje = Properties.Resources.CuentaEdicion_MsgTipoCuentaIDRequerida;
                    cmbTipoCuenta.Focus();
                }
                else if (string.IsNullOrWhiteSpace(txtClaveCuenta.Text) || Contexto.ClaveCuenta == string.Empty)
                {
                    resultado = false;
                    mensaje = Properties.Resources.CuentaEdicion_MsgClaveCuentaRequerida;
                    txtClaveCuenta.Focus();
                }
                else if (cmbActivo.SelectedItem == null )
                {
                    resultado = false;
                    mensaje = Properties.Resources.CuentaEdicion_MsgActivoRequerida;
                    cmbActivo.Focus();
                }
                else
                {
                    int cuentaId = Extensor.ValorEntero(txtCuentaID.Text);
                    string descripcion = txtDescripcion.Text;

                    var cuentaPL = new CuentaPL();
                    CuentaInfo cuenta = cuentaPL.ObtenerPorDescripcion(descripcion);

                    if (cuenta != null && (cuentaId == 0 || cuentaId != cuenta.CuentaID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.CuentaEdicion_MsgDescripcionExistente, cuenta.CuentaID);
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
                    var cuentaPL = new CuentaPL();
                    cuentaPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.CuentaID != 0)
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
                    SkMessageBox.Show(this, Properties.Resources.Cuenta_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this,Properties.Resources.Cuenta_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        /// <summary>
        /// Carga los datos de la entidad Tipo Cuenta 
        /// </summary>
        private void CargaComboTipoCuenta()
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
            if(Contexto.TipoCuenta == null || Contexto.TipoCuenta.TipoCuentaID == 0)
            {
                cmbTipoCuenta.SelectedIndex = 0;
            }
        }

        #endregion Métodos

    }
}

