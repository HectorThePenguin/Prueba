using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Servicios.PL;
using SIE.Services.Info.Info;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.WinForm.Auxiliar;
using System.Windows.Controls;

namespace SIE.WinForm.Abasto
{
    /// <summary>
    /// Lógica de interacción para RelacionClienteProveedor.xaml
    /// </summary>
    public partial class RelacionClienteProveedor
    {
        #region  CONSTRUCTOR
        public RelacionClienteProveedor()
        {
            InitializeComponent();
            InicializaContexto();
            ObtenerGanadera();
        }
        #endregion

        #region  PROPIEDADES
        private RelacionClienteProveedorInfo Contexto
        { 
            get 
            {
                if(DataContext == null)
                {
                    InicializaContexto();
                }
                return (RelacionClienteProveedorInfo)DataContext;
            }
            set { DataContext = value; }
        }
        public IList<RelacionClienteProveedorInfo> ListaRelacionClienteProveedor;
        #endregion

        #region  EVENTOS

        private void skAyudaCentroAcopio_LostFocus(object sender, RoutedEventArgs e)
        {
            if (skAyudaCentroAcopio.Descripcion.Trim().Equals(string.Empty) || skAyudaCentroAcopio.Clave.Trim().Equals(string.Empty) || skAyudaCentroAcopio.Clave.Trim().Equals("0"))
            {
                skAyudaCentroAcopio.LimpiarCampos();
            }
            else {
                Contexto.ContextoProveedor.OrganizacionID = Contexto.CentroAcopio.OrganizacionID;
            }
        }

        private void cboGadera_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            skAyudaCentroAcopio.LimpiarCampos();
            SplAyudaProveedor.LimpiarCampos();
            skAyudaClienteCreditoExcel.LimpiarCampos();
            gridDatos3.ItemsSource = null;
            ListaRelacionClienteProveedor = new List<RelacionClienteProveedorInfo>();
            if (cboGadera.SelectedIndex > 0)
            {
                var division = (OrganizacionInfo)cboGadera.SelectedItem;
                Contexto.CentroAcopio.Division = division.Division;
            }
            else
            {
                Contexto.CentroAcopio.Division = ".";
                Contexto.ClienteCreditoExcel = new ClienteCreditoExcelInfo();
                Contexto.Proveedor = new ProveedorInfo() { OrganizacionID = -1 };
                gridDatos3.ItemsSource = null;
                ListaRelacionClienteProveedor = new List<RelacionClienteProveedorInfo>();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cboGadera.SelectedIndex = 0;
                skAyudaClienteCreditoExcel.ObjetoNegocio = new ClienteCreditoExcelPL();
                SplAyudaProveedor.ObjetoNegocio = new ProveedorPL();
                skAyudaCentroAcopio.ObjetoNegocio = new OrganizacionPL();
                skAyudaCentroAcopio.AyudaConDatos += (sender2, args) =>
                {
                    gridDatos3.ItemsSource = null;
                    ListaRelacionClienteProveedor = new List<RelacionClienteProveedorInfo>();
                    var division = (OrganizacionInfo)cboGadera.SelectedItem;
                    if (cboGadera.SelectedIndex > 0)
                    {
                        Contexto.CentroAcopio.Division = division.Division;
                    }
                    else
                    {
                        Contexto.CentroAcopio.Division = ".";
                    }
                    SplAyudaProveedor.LimpiarCampos();
                    Contexto.ContextoProveedor.OrganizacionID = Contexto.CentroAcopio.OrganizacionID;
                };
                skAyudaCentroAcopio.AyudaLimpia += (sender2, args) =>
                {
                    gridDatos3.ItemsSource = null;
                    ListaRelacionClienteProveedor = new List<RelacionClienteProveedorInfo>();
                    SplAyudaProveedor.LimpiarCampos();
                    Contexto.ContextoProveedor = new ProveedorInfo { ProveedorID = 0, OrganizacionID = -1};
                };
                SplAyudaProveedor.AyudaLimpia += (sender2, args) =>
                {
                    gridDatos3.ItemsSource = null;
                    ListaRelacionClienteProveedor = new List<RelacionClienteProveedorInfo>();
                    Contexto.ContextoProveedor.OrganizacionID = Contexto.CentroAcopio.OrganizacionID == 0 ? -1 : Contexto.CentroAcopio.OrganizacionID;
                };
                SplAyudaProveedor.AyudaConDatos += (sender2, args) =>
                {
                    MostrarCreditosPorProveedor();
                };

                skAyudaClienteCreditoExcel.txtClave.Focus();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.RelacionClienteProveedor_ErrorTipoRetencion, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.RelacionClienteProveedor_ErrorTipoRetencion, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidaDatos(true))
            {
                Guardar();
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidaDatos(false))
            {
                AgregarCredito();
                HabilitarControles(false);
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.RCP_ConfirmarCancelar, MessageBoxButton.YesNo, MessageImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Limpiar();
            }
        }

        #endregion

        #region  METODOS
        private void InicializaContexto()
        {
            Contexto = new RelacionClienteProveedorInfo()
            {
                ClienteCreditoExcel = new ClienteCreditoExcelInfo(),
                ContextoProveedor = new ProveedorInfo(){OrganizacionID = -1},
                CentroAcopio = new OrganizacionInfo(){
                    TipoOrganizacion = new TipoOrganizacionInfo{TipoOrganizacionID = TipoOrganizacion.Centro.GetHashCode()},
                    Division = "."
                }
            };
        }

        private void ObtenerCentros()
        {
            try
            {
                var organizacionPL = new OrganizacionPL();
                IList<OrganizacionInfo> listaCentros = organizacionPL.ObtenerTipoCentros();

                if (listaCentros != null && listaCentros.Any())
                {
                    AgregarElementoInicialCentros(listaCentros);
                    Contexto.ListaCentros = listaCentros;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.RelacionClienteProveedor_ErrorObtenerCentros, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void ObtenerGanadera()
        {
            try
            {
                var organizacionPL = new OrganizacionPL();
                IList<OrganizacionInfo> listOrganizacion = organizacionPL.ObtenerTipoGanaderas();
                if (listOrganizacion != null && listOrganizacion.Where(item => item.Activo.GetHashCode() == 1).Any())
                {
                    AgregarElementoInicialGanadera(listOrganizacion);
                    Contexto.ListaOrganizacion = listOrganizacion;
                }
                else {
                    AgregarElementoInicialGanadera(listOrganizacion);
                    Contexto.ListaOrganizacion = listOrganizacion;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.RelacionClienteProveedor_SinGanaderas, MessageBoxButton.OK, MessageImage.Warning);
                    HabilitarTodo(false);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.RelacionClienteProveedor_ErrorObtenerGanadera, MessageBoxButton.OK, MessageImage.Error);
            }

        }

        private void AgregarElementoInicialGanadera(IList<OrganizacionInfo> listOrganizasion)
        {
            var listOrganizasionInicial = new OrganizacionInfo { OrganizacionID = 0, Descripcion = Properties.Resources.cbo_Seleccione };
            if (!listOrganizasion.Contains(listOrganizasionInicial))
            {
                listOrganizasion.Insert(0, listOrganizasionInicial);
            }
        }

        private void AgregarElementoInicialCentros(IList<OrganizacionInfo> listCentros)
        {
            var listOrganizasionInicial = new OrganizacionInfo { OrganizacionID = 0, Descripcion = Properties.Resources.cbo_Seleccione };
            if (!listCentros.Contains(listOrganizasionInicial))
            {
                listCentros.Insert(0, listOrganizasionInicial);
            }
        }

        private void MostrarCreditosPorProveedor()
        {
            var rcp = new RelacionClienteproveedorPL();
            gridDatos3.ItemsSource = null;
            ListaRelacionClienteProveedor = rcp.ObtenerPorProveedorID(Contexto.ContextoProveedor.ProveedorID, Contexto.CentroAcopio.OrganizacionID);
            if (ListaRelacionClienteProveedor != null)
            {
                gridDatos3.ItemsSource = ListaRelacionClienteProveedor;
            }
        }

        private void AgregarCredito()
        {
            try
            {
                var pl = new ImportarSaldosSOFOMPL();
                var info = pl.CreditoSOFOM_ObtenerPorID(Convert.ToInt32(skAyudaClienteCreditoExcel.Clave.Trim()));
                var credito = new RelacionClienteProveedorInfo
                {
                    Editable = true,
                    CreditoID = Convert.ToInt32(skAyudaClienteCreditoExcel.Clave.Trim()),
                    Credito = new ImportarSaldosSOFOMInfo
                    {
                        CreditoID = Convert.ToInt32(skAyudaClienteCreditoExcel.Clave.Trim()),
                        Saldo = info.Saldo,
                        TipoCredito = new TipoCreditoInfo { 
                            Descripcion = info.TipoCredito.Descripcion,
                            TipoCreditoID = info.TipoCredito.TipoCreditoID
                        },                        
                    },
                    Ganadera = (OrganizacionInfo)cboGadera.SelectedItem,
                    Centro = (OrganizacionInfo)skAyudaCentroAcopio.Contexto,
                    UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                    Proveedor = new ProveedorInfo{
                        ProveedorID = Convert.ToInt32(SplAyudaProveedor.Clave.Trim()),
                        Descripcion = SplAyudaProveedor.Descripcion.Trim()
                    }                    
                };

                ListaRelacionClienteProveedor.Add(credito);
                gridDatos3.ItemsSource = null;   
                gridDatos3.ItemsSource = ListaRelacionClienteProveedor;
                skAyudaClienteCreditoExcel.LimpiarCampos();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.RelacionClienteProveedor_ErrorAgregarCredito, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void Guardar()
        {
            try
            {
                var rcp = new RelacionClienteproveedorPL();
                if (rcp.Guardar(ListaRelacionClienteProveedor.Where(item => item.Editable == true).ToList()))
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.RelacionClienteProveedor_Guardar, MessageBoxButton.OK, MessageImage.Correct);
                    Limpiar();
                }
                else {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.RelacionClienteProveedor_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.RelacionClienteProveedor_ErrorGuardar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void Limpiar()
        {
            cboGadera.SelectedIndex = 0;
            Contexto.ClienteCreditoExcel = new ClienteCreditoExcelInfo();
            Contexto.ContextoProveedor = new ProveedorInfo { OrganizacionID = -1};
            gridDatos3.ItemsSource = null;
            ListaRelacionClienteProveedor = new List<RelacionClienteProveedorInfo>();
            HabilitarControles(true);
        }

        private bool ValidaDatos(bool esGuardar)
        {
            if (esGuardar)
            {
                if (ListaRelacionClienteProveedor == null || !ListaRelacionClienteProveedor.Where(item => item.Editable == true).Any())
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.RelacionClienteProveedor_RegistrosNuevos, MessageBoxButton.OK, MessageImage.Stop);
                    skAyudaClienteCreditoExcel.AsignarFoco();
                    return false;
                }
            }
            else {
                if (Convert.ToInt32(cboGadera.SelectedValue) <= 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.RCP_ValidaGanadera,MessageBoxButton.OK,MessageImage.Stop);
                    cboGadera.Focus();
                    return false;
                }

                if (Convert.ToInt32(skAyudaCentroAcopio.Clave) <= 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.RCP_ValidaCentro,MessageBoxButton.OK,MessageImage.Stop);
                    skAyudaCentroAcopio.AsignarFoco();
                    return false;
                }

                if (Contexto.ContextoProveedor.ProveedorID <= 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],Properties.Resources.RCP_ValidaProveedor,MessageBoxButton.OK,MessageImage.Stop);
                    SplAyudaProveedor.AsignarFoco();
                    return false;
                }

                if (Convert.ToInt32(skAyudaClienteCreditoExcel.Clave) <= 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.RCP_ValidaCredito, MessageBoxButton.OK, MessageImage.Stop);
                    skAyudaClienteCreditoExcel.AsignarFoco();
                    return false;
                }

                if (ListaRelacionClienteProveedor.Where(item => item.CreditoID == Convert.ToInt32(skAyudaClienteCreditoExcel.Clave)).Any())
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.RelacionClienteProveedor_CreditoRelacionado, MessageBoxButton.OK, MessageImage.Stop);
                    skAyudaClienteCreditoExcel.AsignarFoco();
                    return false;
                }
            }

            return true;
        }

        private void HabilitarControles(bool habilitar)
        {
            cboGadera.IsEnabled = habilitar;
            skAyudaCentroAcopio.IsEnabled = habilitar;
            SplAyudaProveedor.IsEnabled = habilitar;        
        }

        private void HabilitarTodo(bool habilitar)
        {
            cboGadera.IsEnabled = habilitar;
            skAyudaCentroAcopio.IsEnabled = habilitar;
            SplAyudaProveedor.IsEnabled = habilitar;
            skAyudaClienteCreditoExcel.IsEnabled = habilitar;
            btnAgregar.IsEnabled = habilitar;
            btnLimpiar.IsEnabled = habilitar;
            btnGuardar.IsEnabled = habilitar;
            btnCancelar.IsEnabled = habilitar;
        }


        #endregion
      
    }
}