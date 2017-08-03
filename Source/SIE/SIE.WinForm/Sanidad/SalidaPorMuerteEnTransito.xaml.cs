using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using NPOI.SS.Formula.Functions;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.Services.Servicios.BL;
using Application = System.Windows.Application;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;

namespace SIE.WinForm.Sanidad
{
    /// <summary>
    /// Lógica de interacción para SalidaPorMuerteEnTransito.xaml
    /// </summary>
    public partial class SalidaPorMuerteEnTransito
    {
        private IList<CorralesPorOrganizacionInfo> corrales;
        
        private SalidaGanadoEnTransitoInfo Contexto
        {
            get
            {
                if (DataContext == null)//si no se recibio una alerta se inicializa una nueva
                {
                    InicializaContexto();
                }
                return (SalidaGanadoEnTransitoInfo)DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Inicializa el contexto
        /// </summary>
        private void InicializaContexto()
        {  
            Contexto = new SalidaGanadoEnTransitoInfo
            {
                LoteID=0,
                Cliente = new ClienteInfo(),
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario() 
            };
            
        }

        public SalidaPorMuerteEnTransito()
        {
            InicializaContexto();
            InitializeComponent();
        }

        /// <summary>
        /// Carga el comboBox de los corrales segun la organizacion del usuario que se logueo
        /// </summary>
        void CargarCorralesComboBox()
        {
            //cbCorrales.Items.Clear();
            InicializaContexto();
            //Contexto = new SalidaPorMuerteEnTransitoInfo {OrganizacionID = Contexto.OrganizacionID};
            corrales = CorralBL.ObtenerCorralesPorOrganizacionID(Contexto.OrganizacionID);
    
                //cargar combobox de corrales
            if (corrales.Count == 0) //en caso de no encontrarse ningun corral
            {
                //verificar si el parametroID de la tabla de organizacion del usuario logueado este registrado en la tabla parametros
                //SP: Parametro_ObtenerPorDescripcion con la descripcion; 'Corral para faltantes de compra directa' / 'Corral para faltantes de origen propio'

                //verificar que exista la configuracion del parametro corral  de faltantes para la organizacion
                //:SP ParametroOrganizacion_ObtenerPorParametroOrganizacionID ; con OrganizacionID del usuario logueado, ParametroID que corresponda al parametro de la primera validacion 

                //verificar que el corral tenga un lote activo (en caso de no hallar corrales)
                //LoteBL.ObtenerActivosPorCorral(AuxConfiguracion.ObtenerOrganizacionUsuarioID, int corralID);

                var parametroPl = new ParametroPL();
                ParametroInfo parametroFaltantesCompraDirecta = parametroPl.ObtenerPorDescripcion(
                    Properties.Resources.SalidaGanadoTransito_Parametro_Faltantes_Compra_Directa);

                ParametroInfo parametroFaltantesOrigenPropio = parametroPl.ObtenerPorDescripcion(
                    Properties.Resources.SalidaGanadoTransito_Parametro_Faltantes_Origen_Propio);

                if ((parametroFaltantesCompraDirecta != null &&
                     parametroFaltantesCompraDirecta.Activo == EstatusEnum.Activo) ||
                    (parametroFaltantesOrigenPropio != null &&
                     parametroFaltantesOrigenPropio.Activo == EstatusEnum.Activo))
                {
                    var parametroOrganizacionPl = new ParametroOrganizacionPL();
                    var parOrganizacionFaltantesCompraDirecta =
                        new ParametroOrganizacionInfo
                        {
                            Organizacion = new OrganizacionInfo(),
                            Parametro = new ParametroInfo()
                        };
                    parOrganizacionFaltantesCompraDirecta.Organizacion.OrganizacionID = Contexto.OrganizacionID;
                    if (parametroFaltantesCompraDirecta != null)
                        parOrganizacionFaltantesCompraDirecta.Parametro.ParametroID =
                            parametroFaltantesCompraDirecta.ParametroID;
                    parOrganizacionFaltantesCompraDirecta =
                        parametroOrganizacionPl.ObtenerPorParametroOrganizacionID(
                            parOrganizacionFaltantesCompraDirecta);

                    var parOrganizacionFaltantesOrigenPropio =
                        new ParametroOrganizacionInfo
                        {
                            Organizacion = new OrganizacionInfo(),
                            Parametro = new ParametroInfo()
                        };
                    parOrganizacionFaltantesOrigenPropio.Organizacion.OrganizacionID = Contexto.OrganizacionID;
                    parOrganizacionFaltantesOrigenPropio.Parametro.ParametroID =
                        parametroFaltantesOrigenPropio.ParametroID;
                    parOrganizacionFaltantesOrigenPropio =
                        parametroOrganizacionPl.ObtenerPorParametroOrganizacionID(
                            parOrganizacionFaltantesOrigenPropio);

                    if (parOrganizacionFaltantesCompraDirecta == null || parOrganizacionFaltantesOrigenPropio == null)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.SalidaGanadoTransito_MsgConfiguracionCorralNoExiste,
                            MessageBoxButton.OK, MessageImage.Warning);
                        BloquearPantalla();
                    }
                    else
                    {
                        if (!parOrganizacionFaltantesCompraDirecta.Activo.ValorBooleanoDesdeEnum() && !parOrganizacionFaltantesOrigenPropio.Activo.ValorBooleanoDesdeEnum())
                        {

                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.SalidaGanadoTransito_MsgConfiguracionCorralNoExiste,
                                MessageBoxButton.OK, MessageImage.Warning);
                            BloquearPantalla();
                        }
                        else 
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                 Properties.Resources.SalidaGanadoTransito_MsgCorralActivoNoEncontrado,
                                 MessageBoxButton.OK, MessageImage.Warning);
                            BloquearPantalla();   
                        }
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.SalidaGanadoTransito_MsgParametroCorralNoExiste, MessageBoxButton.OK,
                        MessageImage.Warning);
                    BloquearPantalla();
                    cbCorrales.IsEnabled = true;
                }
            }
            var seleccione = new CorralesPorOrganizacionInfo
                {
                    CorralID = 0,
                    Descripcion = "Seleccione"
                };
                corrales.Insert(0,seleccione);
                cbCorrales.ItemsSource = new List<CorralesPorOrganizacionInfo>();
                cbCorrales.ItemsSource = corrales;
                cbCorrales.SelectedValue = 0;
        }

        /// <summary>
        /// Evento load en el que se configura la ayuda y carga el comboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SalidaPorMuerteEnTransito_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //inicializa la ayuda de proveedores
               
                skAyudaClientes.ObjetoNegocio = new ClientePL();
                dtpFecha.DisplayDateStart=new DateTime(DateTime.Today.Year,DateTime.Today.Month,1);
                dtpFecha.DisplayDateEnd = DateTime.Today;
              //buscar todos los clientes del proveedor especificado
                //--
                InicializaContexto();
                CargarCorralesComboBox();
                //
            }
            catch
            {
            }
            
        }

        /// <summary>
        /// Al cambiar el corral seleccionado se cargan los costos activos del lote al que pertenece el corral seleccionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbCorrales_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           //carga cabezas,lote

            if (cbCorrales.SelectedIndex <= 0)
            {
                dgCostos.ItemsSource = null;
                InicializaContexto();
                txtLote.Clear();
                txtImporte.Clear();
                txtTotalCabezas.Clear();
                txtPesoPromedio.Clear();
                return;
            }

            foreach (CorralesPorOrganizacionInfo corral in corrales)
            {
                //al encontrar el corral:
                if (corral.CorralID == (int)cbCorrales.SelectedValue)
                {
                    if (corral.Activo == EstatusEnum.Inactivo)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.SalidaGanadoTransito_MsgCorralLoteNoActivo,
                            MessageBoxButton.OK, MessageImage.Warning);

                        Contexto.Costos = new List<CostoCorralInfo>();
                        dgCostos.ItemsSource = null;
                        Contexto.LoteID = 0;
                        txtTotalCabezas.Clear();
                        txtLote.Clear();
                        txtPesoPromedio.Clear();
                        BloquearPantalla();
                    }
                    else
                    {
                        if (cbCorrales.SelectedIndex==0 )//si no se selecciono un corral
                        {
                            txtLote.Clear();
                            txtTotalCabezas.Clear();
                            txtPesoPromedio.Clear();
                            Contexto.LoteID = 0;
                            dgCostos.ItemsSource = null;
                        }
                        else//si se selecciono un corral:
                        {
                            txtLote.Text = corral.LoteID.ToString(CultureInfo.InvariantCulture);
                            Contexto.LoteID = corral.LoteID;
                            txtTotalCabezas.Text = corral.Cabezas.ToString(CultureInfo.InvariantCulture);
                            txtPesoPromedio.Text = corral.PesoPromedio.ToString(CultureInfo.InvariantCulture);
                            DesbloquearPantalla();
                            Contexto.EntradaGanadoTransitoID = corral.EntradaGanadoTransitoID;

                            var costosTemp = CorralBL.ObtenerCostosCorralActivos(corral.EntradaGanadoTransitoID);
                            if (costosTemp == null)
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.SalidaGanadoTransito_MsgCostosCorralNoEncontrados, MessageBoxButton.OK, MessageImage.Warning);
                                Contexto.Costos = new List<CostoCorralInfo>();
                                dgCostos.ItemsSource = null;
                                BloquearPantalla();
                            }
                            else if (costosTemp.Count == 0)
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.SalidaGanadoTransito_MsgCostosCorralNoEncontrados, MessageBoxButton.OK, MessageImage.Warning);
                                Contexto.Costos = new List<CostoCorralInfo>();
                                dgCostos.ItemsSource = null; 
                                BloquearPantalla();
                            }
                            else
                            {
                                Contexto.Costos = null;
                                Contexto.Costos = costosTemp;
                            }
                        }
                       
                        if (dgCostos.Items.Count > 0)//si se hallaron costos se desbloquea la pantalla
                        {
                            DesbloquearPantalla();
                        }
                    }
                    break;
                }
            }
            if (Contexto.Costos!=null &&  Contexto.Costos.Count > 0)
            {
                dgCostos.ItemsSource = null;
                dgCostos.ItemsSource = Contexto.Costos;
                
            }
        }

        //al seleccionar la salida de ganado por venta
        private void RbVenta_OnChecked(object sender, RoutedEventArgs e)
        {
            txtImporte.IsEnabled = true;
            skAyudaClientes.IsEnabled = true;
        }

        //al seleccionar la salida de ganado por muerte
        private void RbMuerte_OnChecked(object sender, RoutedEventArgs e)
        {
            txtImporte.Clear();
            skAyudaClientes.LimpiarCampos();
            txtImporte.IsEnabled = false;
            skAyudaClientes.IsEnabled = false;
           Contexto.Cliente=new ClienteInfo();
            Contexto.Importe = 0;
        }

        //si se encontro o selecciono un cliente con la ayuda:
        private void SkAyudaClientes_OnAyudaConDatos(object sender, EventArgs e)
        {
            try
            {
                var clienteVentaPL=new ClientePL();
                var cliente = clienteVentaPL.Obtener_Nombre_CodigoSAP_PorID(Contexto.Cliente);

                Contexto.Cliente.CodigoSAP = cliente.CodigoSAP;
                skAyudaClientes.txtClave.Text = Contexto.Cliente.CodigoSAP;//:se carga el codigoSAP del proveedor seleccionado
                skAyudaClientes.txtDescripcion.Text = Contexto.Cliente.Descripcion;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                MessageBox.Show(ex.Message);
            }
           
        }

        private void DesbloquearPantalla()
        {
            if(RbVenta.IsChecked.HasValue && RbVenta.IsChecked.Value)
            {
                skAyudaClientes.IsEnabled = true;
            }
            btnGuardar.IsEnabled = true;
            dtpFecha.IsEnabled = true;
        } 

        private void BloquearPantalla()
        {

            skAyudaClientes.IsEnabled = false;
            btnGuardar.IsEnabled = false;
            dtpFecha.IsEnabled = false;
        }

        /// <summary>
        /// Valida que se hayan proporcionados todos los campos necesarios para el registro 
        /// </summary>
        /// <returns></returns>
        bool DatosObligatoriosProporcionados()
        {
            if (cbCorrales.SelectedIndex==0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.SalidaGanadoTransito_MsgCorralNoSeleccionado
                        , MessageBoxButton.OK, MessageImage.Warning);
                return false;   
            }
            if (Contexto.Muerte)
            {
                if (Contexto.NumCabezas == 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.SalidaGanadoTransito_MsgNumCabezasNoProporcionado
                      , MessageBoxButton.OK, MessageImage.Warning);
                    return false;
                }

                if (Contexto.NumCabezas > int.Parse(txtTotalCabezas.Text))
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.SalidaGanadoTransito_MsgNumCabezasExcedenCabezasLote
                        , MessageBoxButton.OK, MessageImage.Warning);
                    Contexto.NumCabezas = 0;
                    return false;
                }
                if (dtpFecha.Text == string.Empty)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.SalidaGanadoTransito_MsgFechaNoProporcionada
                        , MessageBoxButton.OK, MessageImage.Warning);
                    dtpFecha.IsEnabled = true;
                    return false;
                }
                if (String.IsNullOrEmpty(Contexto.Observaciones))
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.SalidaGanadoTransito_MsgObservacionesNoProporcionadas
                        , MessageBoxButton.OK, MessageImage.Warning);
                    return false;
                }
            }
            else if (Contexto.Venta)
            {
                if (Contexto.NumCabezas == 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.SalidaGanadoTransito_MsgNumcabezasNoProporcionadas
                      , MessageBoxButton.OK, MessageImage.Warning);
                    return false;
                }

                if (Contexto.NumCabezas > int.Parse(txtTotalCabezas.Text))
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.SalidaGanadoTransito_MsgNumCabezasExcedenCabezasLote, MessageBoxButton.OK, MessageImage.Warning);
                    Contexto.NumCabezas = 0;
                    return false;
                }
                if (Contexto.Importe == 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.SalidaGanadoTransito_MsgImporteNoProporcionado
                        , MessageBoxButton.OK, MessageImage.Warning);
                    return false;
                }
                if (Contexto.Cliente.ClienteID == 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.SalidaGanadoTransito_MsgClienteNoProporcionado
                        , MessageBoxButton.OK, MessageImage.Warning);
                    return false;
                }

                if (dtpFecha.Text == string.Empty)
                {

                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.SalidaGanadoTransito_MsgFechaNoProporcionada, MessageBoxButton.OK, MessageImage.Warning);
                    return false;
                }
                if (String.IsNullOrEmpty(Contexto.Observaciones))
                {
                    if (Contexto.NumCabezas > int.Parse(txtTotalCabezas.Text))
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.SalidaGanadoTransito_MsgNumCabezasExcedenCabezasLote, MessageBoxButton.OK, MessageImage.Warning);
                        return false;
                    }
                }
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.SalidaGanadoTransito_MsgVentaOMuerteNoProporcionado
                           , MessageBoxButton.OK, MessageImage.Warning);
                return false;
            }
            return true;
        }

        private void BtnGuardar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Contexto.Importe = txtImporte.Text == string.Empty ? 0 : int.Parse(txtImporte.Text);
                if (DatosObligatoriosProporcionados())//:si se proporcionaron los datos obligatorios
                {
                    ActualizarDetalles(Contexto,int.Parse(txtTotalCabezas.Text));//genera los importes para los costos  
                    Contexto.Kilos = (int.Parse(txtPesoPromedio.Text)*Contexto.NumCabezas);//calcula los kilos que se daran salida segun la cantidad de cabezas 
                    var salida = new SalidaPorMuerteEnTransitoBl();
                    bool t;
                    Contexto.CorralID = (int)cbCorrales.SelectedValue;
                    MemoryStream ms=salida.GuardarSalida(Contexto,out t);//memoryStream con la informacion que se guardara en el archivo PDF
                    if (t)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                          String.Format(Properties.Resources.SalidaGanadoTransito_MsgRegistroExito, Contexto.Folio), MessageBoxButton.OK, MessageImage.Correct);
                       
                        var poliza=new ExportarPoliza();
                        string pdfFile;
                        pdfFile = string.Format("{0} {1}", Contexto.Muerte ? "Poliza de Salida Ganado Transito por Muerte Folio " : "Poliza de Salida Ganado Transito por Venta Folio ", Contexto.Folio);
                        poliza.ImprimirPoliza(ms,pdfFile);//genera el archivo PDF con el nombre especificado

                        InicializaContexto();
                        CargarCorralesComboBox();

                        dtpFecha.Text = string.Empty;
                        txtLote.Clear();
                        txtTotalCabezas.Clear();
                        txtPesoPromedio.Clear();
                        cbCorrales.SelectedIndex = 0;
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.SalidaGanadoTransito_MsgErrorRegistrar
                          , MessageBoxButton.OK, MessageImage.Error);
                        InicializaContexto();
                        CargarCorralesComboBox();
                        dtpFecha.Text = string.Empty;
                        txtLote.Clear();
                        txtTotalCabezas.Clear();
                        txtPesoPromedio.Clear();
                        cbCorrales.SelectedIndex = 0;
                    }
                }
            }
            catch(Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                          ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        ///Calcula el importe para cada costo del corral
        /// </summary>
        /// <param name="totalCabezas"></param>
        public void ActualizarDetalles(SalidaGanadoEnTransitoInfo Contexto, int totalCabezas)
        {
            SalidaGanadoEnTransitoDetalleInfo detalle;

            try
            {
                Contexto.TotalCabezasDuranteMuerte = totalCabezas;
                Contexto.DetallesSalida = new List<SalidaGanadoEnTransitoDetalleInfo>();
                foreach (var costo in Contexto.Costos)
                {
                    detalle = new SalidaGanadoEnTransitoDetalleInfo
                    {
                        Activo = EstatusEnum.Activo,
                        CostoId = costo.CostoID,
                        //calcula el importe calculando el costo promedio unitario de la res y multiplicando por las cabezas que se daran salida:
                        ImporteCosto = Math.Round(((costo.Importe / totalCabezas) * Contexto.NumCabezas), 2),
                        SalidaGanadoTransitoId = Contexto.SalidaGanadoTransitoID,
                        UsuarioCreacionID = Contexto.UsuarioCreacionID
                    };
                    Contexto.DetallesSalida.Add(detalle);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        /// <summary>
        /// En caso de cancelar, se limpia la interfaz
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.SalidaGanadoTransito_MsgCancelar
                 , MessageBoxButton.YesNo, MessageImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                InicializaContexto();
                txtLote.Clear();
                txtTotalCabezas.Clear();
                txtNumCabezas.Clear();
                CargarCorralesComboBox();
                dtpFecha.Text = string.Empty;
            }
        }

        private void TxtImporte_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                  e.Handled = Extensor.ValidarSoloNumeros(txtImporte.Text);
                
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                e.Handled = false;
            }
        }

        private void TxtImporte_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                    e.Handled = Extensor.ValidarSoloNumeros(e.Text);
            
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                e.Handled = false;
            }
        }

        private void TxtNumCabezas_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                e.Handled = Extensor.ValidarSoloNumeros(txtImporte.Text);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                e.Handled = false;
            }
        }

        private void TxtNumCabezas_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                e.Handled = Extensor.ValidarSoloNumeros(e.Text);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                e.Handled = false;
            }
        }
    }
}
