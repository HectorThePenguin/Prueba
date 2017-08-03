using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.MessageBox;
using System.Collections.Generic;
using SuKarne.Controls.Enum;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Lógica de interacción para Cje.xaml
    /// </summary>
    public partial class CuentaGastosEdicion
    {
        #region propiedades
        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private CuentaGastosInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (CuentaGastosInfo)DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CuentaGastosEdicion()
        {
            InitializeComponent();
            InicializaContexto();
            
        }

        public CuentaGastosEdicion(CuentaGastosInfo cuentaGastosInfoSelecionado)
        {
            InitializeComponent();
            cuentaGastosInfoSelecionado.UsuarioModificaID = AuxConfiguracion.ObtenerUsuarioLogueado();
            Contexto = cuentaGastosInfoSelecionado;
            skAyudaOrganizacion.ObjetoNegocio = new OrganizacionPL();
            skAyudaCuentaSAP.ObjetoNegocio = new CuentaSAPPL();
            skAyudaCosto.ObjetoNegocio = new CostoPL();

            var listaTipoCuenta = new List<TipoCuentaInfo>();
            listaTipoCuenta.Add(new TipoCuentaInfo() { TipoCuentaID = SIE.Services.Info.Enums.TipoCuenta.Gastos.GetHashCode() });
            listaTipoCuenta.Add(new TipoCuentaInfo() { TipoCuentaID = SIE.Services.Info.Enums.TipoCuenta.Inventario.GetHashCode() });
            listaTipoCuenta.Add(new TipoCuentaInfo() { TipoCuentaID = SIE.Services.Info.Enums.TipoCuenta.InventarioEnTransito.GetHashCode() });
            listaTipoCuenta.Add(new TipoCuentaInfo() { TipoCuentaID = SIE.Services.Info.Enums.TipoCuenta.Iva.GetHashCode() });
            listaTipoCuenta.Add(new TipoCuentaInfo() { TipoCuentaID = SIE.Services.Info.Enums.TipoCuenta.MateriaPrima.GetHashCode() });
            listaTipoCuenta.Add(new TipoCuentaInfo() { TipoCuentaID = SIE.Services.Info.Enums.TipoCuenta.Producto.GetHashCode() });
            listaTipoCuenta.Add(new TipoCuentaInfo() { TipoCuentaID = SIE.Services.Info.Enums.TipoCuenta.Provision.GetHashCode() });

            Contexto.CuentaSAP.ListaTiposCuenta = listaTipoCuenta;
            
            if (Contexto.CuentaSAP.CuentaSAP == null)
            {
                Contexto.CuentaSAP.CuentaSAP = "";
                Contexto.Costos.ClaveContable = "";
            }
        }
        #endregion
        #region Eventos

        /// <summary>
        /// Evento para cerrar la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
        /// Boton guardar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            var guardar = validarGuardar();

            if (guardar)
            {
                try
                {
                    var cuentaGastosPL = new CuentaGastosPL();
                    cuentaGastosPL.Guardar(Contexto);
                    
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK, MessageImage.Correct);
                    InicializaContexto();
                    
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.CuentasGasto_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.CuentasGasto_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
        }

        #endregion
        #region Metodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            var listaTipoCuenta = new List<TipoCuentaInfo>();
            listaTipoCuenta.Add(new TipoCuentaInfo(){TipoCuentaID = SIE.Services.Info.Enums.TipoCuenta.Gastos.GetHashCode()});
            listaTipoCuenta.Add(new TipoCuentaInfo(){TipoCuentaID = SIE.Services.Info.Enums.TipoCuenta.Inventario.GetHashCode()});
            listaTipoCuenta.Add(new TipoCuentaInfo(){TipoCuentaID = SIE.Services.Info.Enums.TipoCuenta.InventarioEnTransito.GetHashCode()});
            listaTipoCuenta.Add(new TipoCuentaInfo(){TipoCuentaID = SIE.Services.Info.Enums.TipoCuenta.Iva.GetHashCode()});
            listaTipoCuenta.Add(new TipoCuentaInfo(){TipoCuentaID = SIE.Services.Info.Enums.TipoCuenta.MateriaPrima.GetHashCode()});
            listaTipoCuenta.Add(new TipoCuentaInfo(){TipoCuentaID = SIE.Services.Info.Enums.TipoCuenta.Producto.GetHashCode()});
            listaTipoCuenta.Add(new TipoCuentaInfo(){TipoCuentaID = SIE.Services.Info.Enums.TipoCuenta.Provision.GetHashCode()});
            Contexto = new CuentaGastosInfo
            {
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                Organizacion = new OrganizacionInfo(),
                CuentaSAP = new CuentaSAPInfo()
                {
                    ListaTiposCuenta = listaTipoCuenta,
                    CuentaSAP = ""
                },
                Costos = new CostoInfo(),
                Activo = EstatusEnum.Activo
            };


            skAyudaOrganizacion.ObjetoNegocio = new OrganizacionPL();
            skAyudaCuentaSAP.ObjetoNegocio = new CuentaSAPPL();
            skAyudaCuentaSAP.AyudaConDatos += (sender, args) =>
            {
                Contexto.CuentaSAP.ListaTiposCuenta = listaTipoCuenta;
            };
            skAyudaCosto.ObjetoNegocio = new CostoPL();
        }

        /// <summary>
        /// Valida guardar
        /// </summary>
        /// <returns></returns>
        public bool validarGuardar()
        {

            bool resultado = true;
            string mensaje = string.Empty;
            try
            {
                if (Contexto.Organizacion == null || Contexto.Organizacion.OrganizacionID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.CuentasGasto_msgOrganizacionRequerido;
                    skAyudaOrganizacion.AsignarFoco();
                }
                else if (Contexto.CuentaSAP == null || Contexto.CuentaSAP.CuentaSAP == string.Empty)
                {
                    resultado = false;
                    mensaje = Properties.Resources.CuentasGasto_msgCuentaRequerido;
                    skAyudaCuentaSAP.AsignarFoco();
                }
                else if (Contexto.Costos == null || Contexto.Costos.CostoID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.CuentasGasto_msgCostoRequerido;
                    skAyudaCosto.AsignarFoco();
                }
                else if (cmbActivo.SelectedItem == null)
                {
                    resultado = false;
                    mensaje = Properties.Resources.CuentasGasto_msgEstatusRequerido;
                    cmbActivo.Focus();
                }
                else 
                {
                    var cuentaGastosPl = new CuentaGastosPL();
                    List<CuentaGastosInfo> listaCuentaGastos = cuentaGastosPl.ObtenerTodos();

                    if (listaCuentaGastos != null)
                    {
                        if (
                            listaCuentaGastos.Any(
                                registro =>
                                    registro.Organizacion.OrganizacionID == Contexto.Organizacion.OrganizacionID &&
                                    registro.Costos.CostoID == Contexto.Costos.CostoID &&
                                    registro.CuentaGastoID != Contexto.CuentaGastoID))
                        {
                            resultado = false;
                            mensaje = Properties.Resources.CuentasGasto_msgCostoOrganizacionRegistrado;
                            skAyudaCosto.AsignarFoco();
                        }
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
        #endregion
 
    }
    
}
