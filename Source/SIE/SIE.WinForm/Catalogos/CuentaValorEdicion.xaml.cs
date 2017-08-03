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
    /// Interaction logic for CuentaValorEdicion.xaml
    /// </summary>
    public partial class CuentaValorEdicion
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private CuentaValorInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (CuentaValorInfo)DataContext;
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
        public CuentaValorEdicion()
        {
            InitializeComponent();
            InicializaContexto();
            //CargaComboCuenta();
            //CargaComboOrganizacion();
            //CargaComboUuarioModificacion();
        }

        /// <summary>
        /// Constructor para editar una entidad CuentaValor Existente
        /// </summary>
        /// <param name="cuentaValorInfo"></param>
        public CuentaValorEdicion(CuentaValorInfo cuentaValorInfo)
        {
            InitializeComponent();
            //CargaComboCuenta();
            //CargaComboOrganizacion();
            //CargaComboUuarioModificacion();
            cuentaValorInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto = cuentaValorInfo;
            skAyudaOrganizacion.IsEnabled = false;
            skAyudaCuenta.IsEnabled = false;
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
            skAyudaOrganizacion.ObjetoNegocio = new OrganizacionPL();
            skAyudaCuenta.ObjetoNegocio = new CuentaPL();
            //txtDescripcion.Focus();
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
            Contexto = new CuentaValorInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                Cuenta = new CuentaInfo(),
                Organizacion = new OrganizacionInfo()
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
                if (string.IsNullOrWhiteSpace(txtCuentaValorID.Text))
                {
                    resultado = false;
                    mensaje = Properties.Resources.CuentaValorEdicion_MsgCuentaValorIDRequerida;
                    txtCuentaValorID.Focus();
                }
                else if (Contexto.Cuenta == null || Contexto.Cuenta.CuentaID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.CuentaValorEdicion_MsgCuentaIDRequerida;
                    skAyudaCuenta.AsignarFoco();
                }
                else if (Contexto.Organizacion == null || Contexto.Organizacion.OrganizacionID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.CuentaValorEdicion_MsgOrganizacionIDRequerida;
                    skAyudaOrganizacion.AsignarFoco();
                }
                else if (string.IsNullOrWhiteSpace(txtValor.Text) || Contexto.Valor == string.Empty)
                {
                    resultado = false;
                    mensaje = Properties.Resources.CuentaValorEdicion_MsgValorRequerida;
                    txtValor.Focus();
                }
                else
                {
                    int cuentaValorId = Extensor.ValorEntero(txtCuentaValorID.Text);

                    var cuentaValorFiltro = new CuentaValorInfo
                        {
                            Organizacion = Contexto.Organizacion,
                            Cuenta = Contexto.Cuenta,
                            Activo = EstatusEnum.Activo
                        };

                    var cuentaValorPL = new CuentaValorBL();
                    CuentaValorInfo cuentaValor = cuentaValorPL.ObtenerPorFiltros(cuentaValorFiltro);

                    if (cuentaValor != null && (cuentaValorId == 0 || cuentaValorId != cuentaValor.CuentaValorID))
                    {
                        resultado = false;
                        mensaje = string.Format(Properties.Resources.CuentaValorEdicion_MsgDescripcionExistente, cuentaValor.CuentaValorID);
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
                    var cuentaValorPL = new CuentaValorBL();
                    cuentaValorPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    if (Contexto.CuentaValorID != 0)
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
                    SkMessageBox.Show(this, Properties.Resources.CuentaValor_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.CuentaValor_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        ///// <summary>
        ///// Carga los datos de la entidad Cuenta 
        ///// </summary>
        //private void CargaComboCuenta()
        //{
        //    var cuentaPL = new CuentaPL();
        //    var cuenta = new CuentaInfo
        //    {
        //        CuentaID = 0,
        //        Descripcion = Properties.Resources.cbo_Seleccione,
        //    };
        //    IList<CuentaInfo> listaCuenta = cuentaPL.ObtenerTodos(EstatusEnum.Activo);
        //    listaCuenta.Insert(0, cuenta);
        //    cmbCuenta.ItemsSource = listaCuenta;
        //    cmbCuenta.SelectedItem = cuenta;
        //}

        ///// <summary>
        ///// Carga los datos de la entidad Organización 
        ///// </summary>
        //private void CargaComboOrganizacion()
        //{
        //    var organizacionPL = new OrganizacionPL();
        //    var organizacion = new OrganizacionInfo
        //    {
        //        OrganizacionID = 0,
        //        Descripcion = Properties.Resources.cbo_Seleccione,
        //    };
        //    IList<OrganizacionInfo> listaOrganizacion = organizacionPL.ObtenerTodos(EstatusEnum.Activo);
        //    listaOrganizacion.Insert(0, organizacion);
        //    cmbOrganizacion.ItemsSource = listaOrganizacion;
        //    cmbOrganizacion.SelectedItem = organizacion;
        //}

        ///// <summary>
        ///// Carga los datos de la entidad Uuario Modificación 
        ///// </summary>
        //private void CargaComboUuarioModificacion()
        //{
        //    var uuarioModificacionPL = new UuarioModificacionPL();
        //    var uuarioModificacion = new UuarioModificacionInfo
        //    {
        //        UuarioModificacionID = 0,
        //        Descripcion = Properties.Resources.cbo_Seleccione,
        //    };
        //    IList<UsuarioInfo> listaUuarioModificacion = uuarioModificacionPL.ObtenerTodos(EstatusEnum.Activo);
        //    listaUuarioModificacion.Insert(0, uuarioModificacion);
        //    cmbUuarioModificacion.ItemsSource = listaUuarioModificacion;
        //    cmbUuarioModificacion.SelectedItem = uuarioModificacion;
        //}

        #endregion Métodos

    }
}

