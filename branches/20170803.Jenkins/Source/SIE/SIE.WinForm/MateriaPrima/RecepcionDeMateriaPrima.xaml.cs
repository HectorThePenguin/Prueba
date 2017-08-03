using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Windows.Threading;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using System;
using System.Windows;
using System.Windows.Input;
using SuKarne.Controls.Bascula;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.WinForm.Auxiliar;
using SIE.Services.Servicios.BL;

namespace SIE.WinForm.MateriaPrima
{
    /// <summary>
    /// Lógica de interacción para RecepcionDeMateriaPrima.xaml
    /// </summary>
    public partial class RecepcionDeMateriaPrima
    {
        private int organizacionId;
        private int usuario;
        private int operadorId;
        private string nombreImpresora;
        private bool basculaConectada;
        private SerialPortManager spManager;
        private bool pnaOrigenAutomatico;
        private string productoConRestriccion;
        private string porcentajeDescuento;
        private bool aplicarValidacionRestriccionDesc;

        EntradaProductoInfo entradaProductoPorFolio;

        public RecepcionDeMateriaPrima()
        {
            InitializeComponent();
            basculaConectada = false;
        }

        #region Bascula

        /// <summary>
        /// Método que inicializa los datos de la bascula
        /// </summary>
        private void InicializarBascula()
        {
            try
            {
                spManager = new SerialPortManager();
                spManager.NewSerialDataRecieved += (spManager_NewSerialDataRecieved);

                if (spManager != null)
                {

                    spManager.StartListening(AuxConfiguracion.ObtenerConfiguracion().PuertoBascula,
                        int.Parse(AuxConfiguracion.ObtenerConfiguracion().BasculaBaudrate),
                        ObtenerParidad(AuxConfiguracion.ObtenerConfiguracion().BasculaParidad),
                        int.Parse(AuxConfiguracion.ObtenerConfiguracion().BasculaDataBits),
                        ObtenerStopBits(AuxConfiguracion.ObtenerConfiguracion().BasculaBitStop));

                    basculaConectada = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.BasculaMateriaPrima_MsgErrorBascula,
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
        void spManager_NewSerialDataRecieved(object sender, SerialDataEventArgs e)
        {
            string strEnd = "", peso = "";
            double val;
            try
            {
                strEnd = spManager.ObtenerLetura(e.Data);
                if (strEnd.Trim() != "")
                {
                    val = double.Parse(strEnd.Replace(',', '.'), CultureInfo.InvariantCulture);

                    // damos formato al valor peso para presentarlo
                    peso = String.Format(CultureInfo.CurrentCulture, "{0:0}", val).Replace(",", ".");

                    //Aquie es para que se este reflejando la bascula en el display
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        txtDisplayPeso.Text = peso;
                    }), null);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        #endregion

        #region Métodos
        /// <summary>
        /// Método para inicializar los campos
        /// </summary>
        private void InicializarDatos()
        {
            if (obtenerParametrosRestriccionDescuentoProductoSK())
            {
                imgBuscar.IsEnabled = true;
                txtProducto.Text = string.Empty;
                txtContrato.Text = string.Empty;
                txtProveedor.Text = string.Empty;
                txtPlacas.Text = string.Empty;
                txtChofer.Text = string.Empty;
                txtPesoOrigen.Value = 0;
                txtPesoOrigenBonificacion.Value = 0;
                txtPesoBruto.Value = 0;
                txtPesoTara.Value = 0;
                txtPesoNeto.Value = 0;
                txtDescuentos.Value = 0;
                txtNetoAnalizado.Value = 0;
                imgAyudaPenalizaciones.Visibility = Visibility.Hidden;
                btnImprimir.IsEnabled = false;
                txtPesoTara.IsEnabled = false;
                txtPesoBruto.IsEnabled = false;
                txtPesoOrigen.IsEnabled = true;

                txtFecha.Text = DateTime.Now.ToShortDateString();
                entradaProductoPorFolio = new EntradaProductoInfo();
                pnaOrigenAutomatico = false;
                aplicarValidacionRestriccionDesc = false;
                lblPorcentajeDesc.Content = string.Empty;
                txtNotaVenta.Text = string.Empty;
                txtNotaVenta.IsEnabled = false;
            }
            else
            {
                txtFolio.IsEnabled = false;
                btnImprimir.IsEnabled = false;
                btnGuardar.IsEnabled = false;
                txtPesoTara.IsEnabled = false;
                txtPesoBruto.IsEnabled = false;
                txtPesoOrigen.IsEnabled = false;
                txtNotaVenta.IsEnabled = false;
                txtNetoAnalizado.IsEnabled = false;
                txtPesoOrigenBonificacion.IsEnabled = false;
                imgBuscar.IsEnabled = false;
                
            }
        }

        /// <summary>
        /// Valida que el peso tara sea valido
        /// </summary>
        /// <returns></returns>
        private bool ValidarPesoTara()
        {
            var retorno = false;

            try
            {
                if (txtPesoTara.Value > 0)
                {
                    var pesoBruto = txtPesoBruto.Value;
                    var pesoTara = txtPesoTara.Value;

                    if (pesoBruto > pesoTara)
                    {
                        retorno = true;
                        if (!basculaConectada)
                        {
                            CalcularDescuento();
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.SalidaIndividualGanado_MensajePesoBrutoMayorAPesoTara,
                            MessageBoxButton.OK, MessageImage.Warning);
                        if (!basculaConectada)
                        {
                            txtPesoTara.Focus();
                        }
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.BasculaMateriaPrima_MsgPesoTaraMayorCero,
                        MessageBoxButton.OK, MessageImage.Warning);
                    if (!basculaConectada)
                    {
                        txtPesoTara.Focus();
                    }
                }
            }
            catch (Exception)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.BasculaMateriaPrima_MsgPesoTaraMayorCero,
                        MessageBoxButton.OK, MessageImage.Warning);
                if (!basculaConectada)
                {
                    txtPesoTara.Focus();
                }
            }

            return retorno;
        }

        /// <summary>
        /// Valida que el peso bruto sea valido
        /// </summary>
        /// <returns></returns>
        private bool ValidarPesoBruto()
        {
            var retorno = false;
            try
            {
                if (txtPesoBruto.Value > 0)
                {
                    retorno = true;
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.BasculaMateriaPrima_MsgPesoBrutoMayorCero,
                        MessageBoxButton.OK, MessageImage.Warning);
                    if (!basculaConectada)
                    {
                        txtPesoBruto.Focus();
                    }
                }
            }
            catch (Exception)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.BasculaMateriaPrima_MsgPesoBrutoMayorCero,
                        MessageBoxButton.OK, MessageImage.Warning);
                if (!basculaConectada)
                {
                    txtPesoBruto.Focus();
                }
            }

            return retorno;
        }

        /// <summary>
        /// Valida que el peso origen sea valido
        /// </summary>
        /// <returns></returns>
        private bool ValidarPesoOrigen()
        {
            var retorno = false;
            try
            {

                if ((entradaProductoPorFolio.Producto.SubFamilia.SubFamiliaID == (int)SubFamiliasEnum.MicroIngredientes) ||
                    (entradaProductoPorFolio.Contrato.PesoNegociar == PesoNegociarEnum.Origen.ToString() && txtPesoOrigen.Value > 0) ||
                    (entradaProductoPorFolio.Contrato.PesoNegociar == PesoNegociarEnum.Destino.ToString() &&
                     entradaProductoPorFolio.Contrato.TipoFlete.TipoFleteId == (int)TipoFleteEnum.PagoenGanadera &&
                     txtPesoOrigen.Value > 0) ||
                    (entradaProductoPorFolio.Contrato.PesoNegociar == PesoNegociarEnum.Destino.ToString() &&
                     entradaProductoPorFolio.Contrato.TipoFlete.TipoFleteId == (int)TipoFleteEnum.LibreAbordo &&
                     txtPesoOrigen.Value >= 0))
                {
                    if (pnaOrigenAutomatico)
                    {
                        txtPesoOrigenBonificacion.Value = txtPesoOrigen.Value;
                    }
                    else
                    {
                        // Realizar los calculos aqui en esta seccion
                        ValidarPnaOrigen();
                    }
                    retorno = true;
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.BasculaMateriaPrima_MsgPesoOrigenMayorCero,
                        MessageBoxButton.OK, MessageImage.Warning);
                    if (!basculaConectada)
                    {
                        txtPesoOrigen.Focus();
                    }
                }
            }
            catch (Exception)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.BasculaMateriaPrima_MsgPesoOrigenMayorCero,
                        MessageBoxButton.OK, MessageImage.Warning);
                if (!basculaConectada)
                {
                    txtPesoOrigen.Focus();
                }
            }

            return retorno;
        }

        /// <summary>
        /// Método con el cual se valida el folio ingresado
        /// </summary>
        private bool ValidarFolio()
        {
            int folio = 0, contadorMuestras = 0;
            try
            {
                if (txtFolio.Text.Trim() != string.Empty)
                {
                    folio = int.Parse(txtFolio.Text.Trim());
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.BasculaMateriaPrima_MsgCapturarFolio, MessageBoxButton.OK, MessageImage.Warning);
                    InicializarDatos();
                    txtFolio.Text = string.Empty;
                    txtFolio.Focus();
                }
            }
            catch (Exception)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.BasculaMateriaPrima_MsgFolioFueraDeRango, MessageBoxButton.OK, MessageImage.Warning);
                InicializarDatos();
                txtFolio.Text = string.Empty;
                txtFolio.Focus();
            }

            try
            {
                if (folio > 0)
                {
                    txtNotaVenta.Text = string.Empty;
                    txtNotaVenta.IsEnabled = false;
                    var entradaProductoPl = new EntradaProductoPL();
                    entradaProductoPorFolio = entradaProductoPl.ObtenerEntradaProductoPorFolio(folio, organizacionId);
                    if (entradaProductoPorFolio != null)
                    {
                        // Se contabilizan las muestras para validar el segundo pesaje de los forrajes.
                        if (entradaProductoPorFolio.Estatus.EstatusId == (int)Estatus.PendienteAutorizar &&
                                            entradaProductoPorFolio.Producto.Familia.FamiliaID == (int)FamiliasEnum.MateriaPrimas &&
                                            entradaProductoPorFolio.Producto.SubFamilia.SubFamiliaID == (int)SubFamiliasEnum.Forrajes)
                        {
                            if (entradaProductoPorFolio.ProductoDetalle != null)
                            {
                                foreach (var entradaProductoDetalleInfo in entradaProductoPorFolio.ProductoDetalle)
                                {
                                    contadorMuestras = entradaProductoDetalleInfo.ProductoMuestras.Where(t => t.EsOrigen == EsOrigenEnum.Destino).ToList().Count;
                                }
                            }
                        }

                        if (entradaProductoPorFolio.Activo == EstatusEnum.Activo)
                        {
                            if (entradaProductoPorFolio.TipoContrato.TipoContratoId != (int)TipoContratoEnum.BodegaExterna)
                            {
                                if (entradaProductoPorFolio.PesoTara == 0)
                                {
                                    if (entradaProductoPorFolio.Estatus.EstatusId == (int)Estatus.Aprobado ||
                                        entradaProductoPorFolio.Estatus.EstatusId == (int)Estatus.Autorizado ||
                                        (entradaProductoPorFolio.Estatus.EstatusId == (int)Estatus.PendienteAutorizar &&
                                            entradaProductoPorFolio.Producto.Familia.FamiliaID == (int)FamiliasEnum.MateriaPrimas &&
                                            entradaProductoPorFolio.Producto.SubFamilia.SubFamiliaID == (int)SubFamiliasEnum.Forrajes && contadorMuestras > 15))
                                    {
                                        if (entradaProductoPorFolio.PesoBruto == 0 ||
                                            (entradaProductoPorFolio.PesoBruto > 0 &&
                                             entradaProductoPorFolio.AlmacenInventarioLote.AlmacenInventarioLoteId != 0 &&
                                             entradaProductoPorFolio.FechaFinDescarga.Year != 1 &&
                                             entradaProductoPorFolio.FechaInicioDescarga.Year != 1))
                                        {
                                            txtNotaVenta.Text = entradaProductoPorFolio.NotaDeVenta;
                                            txtProducto.Text = entradaProductoPorFolio.Producto.ProductoDescripcion;
                                            txtProveedor.Text =
                                                entradaProductoPorFolio.RegistroVigilancia.ProveedorMateriasPrimas.Descripcion;
                                            txtContrato.Text =
                                                entradaProductoPorFolio.Contrato.Folio.ToString(
                                                    CultureInfo.InvariantCulture);

                                            if (entradaProductoPorFolio.Contrato.PesoNegociar == PesoNegociarEnum.Destino.ToString() &&
                                                entradaProductoPorFolio.Producto.Descripcion != "MELAZA")
                                            {
                                                txtPesoOrigenBonificacion.Value = 0;
                                            }

                                            ValidarPnaOrigen();

                                            if (entradaProductoPorFolio.Contrato.TipoFlete != null &&
                                                entradaProductoPorFolio.Contrato.TipoFlete.TipoFleteId == (int)TipoFleteEnum.LibreAbordo)
                                            {
                                                txtPlacas.Text =
                                                    entradaProductoPorFolio.RegistroVigilancia.CamionCadena;
                                                txtChofer.Text =
                                                    entradaProductoPorFolio.RegistroVigilancia.Chofer;
                                            }
                                            else
                                            {
                                                txtPlacas.Text =
                                                    entradaProductoPorFolio.RegistroVigilancia.Camion.PlacaCamion;
                                                txtChofer.Text = String.Format("{0} {1} {2}",
                                                   entradaProductoPorFolio.RegistroVigilancia.ProveedorChofer.Chofer.Nombre,
                                                   entradaProductoPorFolio.RegistroVigilancia.ProveedorChofer.Chofer
                                                       .ApellidoPaterno,
                                                   entradaProductoPorFolio.RegistroVigilancia.ProveedorChofer.Chofer
                                                       .ApellidoMaterno);
                                            }


                                            if (entradaProductoPorFolio.PesoBruto > 0)
                                            {
                                                if (!basculaConectada)
                                                {
                                                    txtPesoTara.IsEnabled = true;
                                                    txtPesoTara.Focus();
                                                }
                                                else
                                                {
                                                    if (!String.IsNullOrEmpty(txtPesoTara.Text))
                                                    {
                                                        try
                                                        {
                                                            txtPesoTara.Value = int.Parse(txtDisplayPeso.Text.Trim().Replace(",", ""));
                                                            btnGuardar.Focus();
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            Logger.Error(ex);
                                                            txtPesoTara.Text = "0";
                                                        }

                                                    }

                                                }

                                                txtPesoOrigen.IsEnabled = false;

                                                txtPesoOrigen.Value = entradaProductoPorFolio.PesoOrigen;
                                                txtPesoOrigenBonificacion.Value = entradaProductoPorFolio.PesoBonificacion;
                                                txtPesoBruto.Value = entradaProductoPorFolio.PesoBruto;

                                                if (!txtPesoTara.IsEnabled)
                                                {
                                                    CalcularDescuento();
                                                }
                                            }
                                            else
                                            {
                                                if (!basculaConectada)
                                                {
                                                    txtPesoBruto.IsEnabled = true;
                                                }
                                                else
                                                {
                                                    if (!String.IsNullOrEmpty(txtDisplayPeso.Text))
                                                    {
                                                        try
                                                        {
                                                            txtPesoBruto.Value = int.Parse(txtDisplayPeso.Text.Trim().Replace(",", ""));
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            Logger.Error(ex);
                                                            txtPesoBruto.Text = "0";
                                                        }

                                                    }

                                                }
                                                txtPesoOrigenBonificacion.Focus();
                                            }
                                            aplicarValidacionRestriccionDesc = validarRestriccionDescuento(entradaProductoPorFolio.Producto.ProductoId.ToString(), entradaProductoPorFolio.RegistroVigilancia.ProveedorMateriasPrimas.Descripcion);
                                            if (productoConRestriccion.Length == 0)
                                            {
                                                SkMessageBox.Show(
                                                                            Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                                            Properties.Resources.EntradaMateriaPrima_MsgSinConfiguracionProductoRestriccion,
                                                                            MessageBoxButton.OK,
                                                                            MessageImage.Stop);
                                                //EntradaMateriaPrima_MsgSinConfiguracionProductoRestriccion
                                                InicializarDatos();
                                                txtFolio.Text = string.Empty;
                                                txtFolio.Focus();
                                            }
                                            else if (porcentajeDescuento.Length == 0)
                                            {
                                                SkMessageBox.Show(
                                                                            Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                                            Properties.Resources.EntradaMateriaPrima_MsgSinConfiguracionPorcentajeDescuento,
                                                                            MessageBoxButton.OK,
                                                                            MessageImage.Stop);
                                                InicializarDatos();
                                                txtFolio.Text = string.Empty;
                                                txtFolio.Focus();
                                            }
                                            else if (aplicarValidacionRestriccionDesc)
                                            {
                                                txtNotaVenta.IsEnabled = true;
                                                txtNotaVenta.Focus();
                                            }

                                        }
                                        else
                                        {
                                            SkMessageBox.Show(
                                                Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                Properties.Resources.BasculaMateriaPrima_MsgFolioNoTieneFechaDescarga,
                                                MessageBoxButton.OK, MessageImage.Warning);
                                            InicializarDatos();
                                            txtFolio.Text = string.Empty;
                                            txtFolio.Focus();
                                        }
                                    }
                                    else
                                    {
                                        SkMessageBox.Show(
                                            Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                            Properties.Resources.BasculaMateriaPrima_MsgFolioNoHaSidoAprobado,
                                            MessageBoxButton.OK, MessageImage.Warning);
                                        InicializarDatos();
                                        txtFolio.Text = string.Empty;
                                        txtFolio.Focus();
                                    }
                                }
                                else
                                {
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.BasculaMateriaPrima_MsgFolioYaSeLeDioEntrada, MessageBoxButton.OK, MessageImage.Warning);
                                    InicializarDatos();
                                    txtFolio.Text = string.Empty;
                                    txtFolio.Focus();
                                }
                            }
                            else
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.BasculaMateriaPrima_MsgFolioEsDeBodegaExterna, MessageBoxButton.OK, MessageImage.Warning);
                                InicializarDatos();
                                txtFolio.Text = string.Empty;
                                txtFolio.Focus();
                            }
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.BasculaMateriaPrima_MsgFolioInactivo, MessageBoxButton.OK, MessageImage.Warning);
                            InicializarDatos();
                            txtFolio.Text = string.Empty;
                            txtFolio.Focus();
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.BasculaMateriaPrima_MsgFolioInvalido, MessageBoxButton.OK, MessageImage.Warning);
                        InicializarDatos();
                        txtFolio.Text = string.Empty;
                        txtFolio.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.BasculaMateriaPrima_MsgErrorInesperado,
                    MessageBoxButton.OK,
                    MessageImage.Stop);
                InicializarDatos();
                txtFolio.Text = string.Empty;
                txtFolio.Focus();
            }

            return false;
        }


        public bool validarRestriccionDescuento(string idProducto, string proveedorMateriaPrima)
        {
            bool resultado = false;
            lblPorcentajeDesc.Content = "";
            string[] productos = null; 

            
            if(proveedorMateriaPrima.ToUpper().Trim() == "SUKARNE SA DE CV")
            {
                if (productoConRestriccion.Trim().Length > 0)
                {
                    //if (descripcionProducto == productoConRestriccion)
                    productos = productoConRestriccion.Split('|');
                    if (productos.Contains(idProducto))
                    {
                        lblPorcentajeDesc.Content = string.Format( "{0} %", porcentajeDescuento.ToString());
                        lblPorcentajeDesc.Visibility = System.Windows.Visibility.Hidden;
                        resultado = true;
                    }
                }
            }
            
            return resultado;
        }

        private void ObtenerRestriccionDescuento()
        {
            float porcentajeDescuentoPermitido = 0.0f;
            float pnaOrigen = 0.0f;
            float porcentaje = 0.0f;
            float pesoOrigen = 0.0f;
            float pesoNeto = 0.0f;
            float pnaDestino = 0.0f;

            float.TryParse(porcentajeDescuento, out porcentaje);
            float.TryParse(txtPesoOrigenBonificacion.Text, out pnaOrigen);
            float.TryParse(txtPesoOrigen.Text, out pesoOrigen);
            float.TryParse(txtPesoNeto.Text, out pesoNeto);

            porcentajeDescuentoPermitido = ((porcentaje/100) * pnaOrigen);
            pnaDestino = pnaOrigen - porcentajeDescuentoPermitido;
            
            if (pesoNeto < Math.Abs(pesoOrigen - porcentajeDescuentoPermitido) )
            {
                txtDescuentos.Text = porcentajeDescuentoPermitido.ToString();
                txtNetoAnalizado.Text = pnaDestino.ToString();
                lblPorcentajeDesc.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                txtDescuentos.Text = "0";
                txtNetoAnalizado.Text = pnaOrigen.ToString();
                lblPorcentajeDesc.Visibility = System.Windows.Visibility.Hidden;
            }
            
        }

        public bool obtenerParametrosRestriccionDescuentoProductoSK()
        {
            ParametroGeneralBL parametroGenBL = new ParametroGeneralBL();
            ParametroGeneralInfo parametroGeneral;
            parametroGeneral = parametroGenBL.ObtenerPorClaveParametroActivo(ParametrosEnum.ProductoConRestriccionDescSK.ToString());
            if (parametroGeneral != null)
            {
                productoConRestriccion = parametroGeneral.Valor;
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.BasculaMateriaPrima_MsgErrorParametroProductoRestriccion, MessageBoxButton.OK, MessageImage.Error);
                return false;
            }
            parametroGeneral = parametroGenBL.ObtenerPorClaveParametroActivo(ParametrosEnum.PorcentajeMaxDesctoProductoSK.ToString());
            if (parametroGeneral != null)
            {
                try
                {
                    porcentajeDescuento = decimal.Parse(parametroGeneral.Valor).ToString();
                }
                catch (Exception)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.BasculaMateriaPrima_MsgErrorParametroPorcentajeDesc, MessageBoxButton.OK, MessageImage.Error);
                    return false;
                }
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.BasculaMateriaPrima_MsgErrorParametroPorcentajeDesc, MessageBoxButton.OK, MessageImage.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Método que sirve para calcular el descuento
        /// </summary>
        private void CalcularDescuento()
        {
            //if (txtPesoNeto.Value == 0)
            //{
            int pesoBruto = txtPesoBruto.Value.HasValue ? Convert.ToInt32(txtPesoBruto.Value.Value) : 0;
            int pesoTara = txtPesoTara.Value.HasValue ? Convert.ToInt32(txtPesoTara.Value.Value) : 0;

            int pesoNeto;
            if (entradaProductoPorFolio.Contrato.CalidadOrigen == EsOrigenEnum.Origen.GetHashCode())
            {
                pesoNeto = txtPesoOrigen.Value.HasValue ? Convert.ToInt32(txtPesoOrigen.Value.Value) : 0;
            }
            else
            {
                pesoNeto = pesoBruto - pesoTara;
            }

            txtPesoNeto.Value = pesoBruto - pesoTara;

            if (aplicarValidacionRestriccionDesc && entradaProductoPorFolio.Contrato.PesoNegociar == PesoNegociarEnum.Origen.ToString())
            {
                ObtenerRestriccionDescuento();
            }
            else
            {

                if (entradaProductoPorFolio.ProductoDetalle != null)
                {
                    decimal muestras = decimal.Zero;
                    decimal descuento = decimal.Zero;

                    if (entradaProductoPorFolio.Contrato.AplicaDescuento == 1)
                    {
                        if (entradaProductoPorFolio.Producto.SubFamilia.SubFamiliaID == (int)SubFamiliasEnum.Granos)
                        {
                            foreach (var entradaProductoDetalle in entradaProductoPorFolio.ProductoDetalle
                                .Where(t => t.Indicador.IndicadorId == (int)IndicadoresEnum.Humedad ||
                                            t.Indicador.IndicadorId == (int)IndicadoresEnum.Impurezas ||
                                            t.Indicador.IndicadorId == (int)IndicadoresEnum.Danostotales))
                            {
                                foreach (var productoMuestra in entradaProductoDetalle.ProductoMuestras)
                                {
                                    muestras += productoMuestra.Descuento;
                                }
                            }
                        }
                        else if (entradaProductoPorFolio.Producto.SubFamilia.SubFamiliaID ==
                                 (int)SubFamiliasEnum.Forrajes)
                        {
                            foreach (var entradaProductoDetalle in entradaProductoPorFolio.ProductoDetalle)
                            {
                                foreach (var productoMuestra in entradaProductoDetalle.ProductoMuestras)
                                {
                                    muestras += productoMuestra.Descuento;
                                    break;
                                }
                                break;
                            }
                        }
                        else
                        // Si es otra subfamilia se hace la validacion que se hacia antes del cambio de la calidad origen.
                        {
                            foreach (var entradaProductoDetalle in entradaProductoPorFolio.ProductoDetalle)
                            {
                                foreach (var productoMuestra in entradaProductoDetalle.ProductoMuestras)
                                {
                                    muestras += productoMuestra.Descuento;
                                }
                            }
                        }
                    }

                    descuento = (muestras / 100) * pesoNeto;

                    txtDescuentos.Value = decimal.Round(descuento, 0);

                    if (descuento > 0)
                    {
                        imgAyudaPenalizaciones.Visibility = Visibility.Visible;
                    }

                    //var pesoNetoAnalizado = int.Parse(txtPesoNeto.Text.Trim()) - int.Parse(txtDescuentos.Text.Trim());

                    if (descuento > 0)
                    {
                        int pesoNetoAnalizado = pesoNeto - Convert.ToInt32(decimal.Round(descuento, 0));

                        if (entradaProductoPorFolio.Contrato.CalidadOrigen == EsOrigenEnum.Origen.GetHashCode())
                        {
                            txtPesoOrigenBonificacion.Value = pesoNetoAnalizado;
                        }
                        else
                        {
                            txtNetoAnalizado.Value = pesoNetoAnalizado;
                        }
                    }
                    if (descuento == 0)
                    {

                        if (entradaProductoPorFolio.Contrato.CalidadOrigen == EsOrigenEnum.Origen.GetHashCode() && (!txtPesoOrigenBonificacion.Value.HasValue || txtPesoOrigenBonificacion.Value.Value == 0))
                        {
                            txtPesoOrigenBonificacion.Value = pesoNeto;
                        }
                        else
                            if (entradaProductoPorFolio.Contrato.CalidadOrigen == EsOrigenEnum.Destino.GetHashCode() && (!txtNetoAnalizado.Value.HasValue || txtNetoAnalizado.Value.Value == 0))
                            {
                                txtNetoAnalizado.Value = pesoNeto;
                            }
                    }

                }
            }
            //}
        }
        /// <summary>
        /// Método que manda imprimir los ticket
        /// </summary>
        private void Imprimir()
        {
            try
            {
                var print = new PrintDocument();
                print.PrintPage += print_Page;
                print.PrinterSettings.PrinterName = nombreImpresora;
                print.PrintController = new StandardPrintController();
                print.Print();
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.BasculaMateriaPrima_MsgErrorImprimir, MessageBoxButton.OK, MessageImage.Warning);
                Logger.Error(ex);

            }
        }
        /// <summary>
        /// Método que guarda la entrada de materia prima
        /// </summary>
        void Guardar()
        {
            int folio = 0;
            try
            {
                if (txtFolio.Text.Trim() != string.Empty)
                {
                    folio = int.Parse(txtFolio.Text.Trim());
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.BasculaMateriaPrima_MsgCapturarFolio, MessageBoxButton.OK, MessageImage.Warning);
                    InicializarDatos();
                    txtFolio.Text = string.Empty;
                    txtFolio.Focus();
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                           Properties.Resources.BasculaMateriaPrima_MsgFolioFueraDeRango, MessageBoxButton.OK, MessageImage.Warning);
                InicializarDatos();
                txtFolio.Text = string.Empty;
                txtFolio.Focus();
                Logger.Error(ex);
            }

            if (folio > 0)
            {
                var entradaProductoPl = new EntradaProductoPL();

                if (entradaProductoPorFolio.PesoBruto > 0)
                {
                    if (ValidarPesoTara())
                    {
                        try
                        {
                            entradaProductoPorFolio.PesoTara = (int)txtPesoTara.Value;
                            var operadorPl = new OperadorPL();

                            entradaProductoPorFolio.OperadorBascula = entradaProductoPorFolio.OperadorBascula ?? operadorPl.ObtenerPorID(operadorId);
                            entradaProductoPorFolio.PesoNetoAnalizado = (int)txtNetoAnalizado.Value;
                            entradaProductoPorFolio.PesoDescuento = txtDescuentos.Value;
                            entradaProductoPorFolio.NotaDeVenta = txtNotaVenta.Text.Trim();
                            if (entradaProductoPl.ActualizarEntradaProductoLlegada(entradaProductoPorFolio))
                            {
                                Imprimir();
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.BasculaMateriaPrima_MsgGuardadoOk, MessageBoxButton.OK,
                                    MessageImage.Correct);
                                btnImprimir.IsEnabled = true;
                            }
                            else
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.BasculaMateriaPrima_MsgErrorAlGuardar, MessageBoxButton.OK,
                                    MessageImage.Warning);
                                txtFolio.Focus();
                            }
                        }
                        catch
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.BasculaMateriaPrima_MsgErrorAlGuardar, MessageBoxButton.OK,
                                    MessageImage.Warning);
                            txtFolio.Focus();
                        }
                    }
                }
                else
                {
                    if (ValidarPesoOrigen() && ValidarPesoBruto())
                    {
                        try
                        {
                            entradaProductoPorFolio.PesoBruto = (int)txtPesoBruto.Value;
                            entradaProductoPorFolio.PesoOrigen = (int)txtPesoOrigen.Value;
                            entradaProductoPorFolio.PesoBonificacion = (int)txtPesoOrigenBonificacion.Value;
                            entradaProductoPorFolio.OperadorBascula = new OperadorInfo { OperadorID = operadorId };
                            entradaProductoPorFolio.PesoDescuento = txtDescuentos.Value;
                            entradaProductoPorFolio.NotaDeVenta = txtNotaVenta.Text.Trim();
                            if (entradaProductoPl.ActualizarEntradaProductoLlegada(entradaProductoPorFolio))
                            {
                                Imprimir();
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.BasculaMateriaPrima_MsgGuardadoOk, MessageBoxButton.OK,
                                    MessageImage.Correct);
                                InicializarDatos();
                                txtFolio.Text = string.Empty;
                                txtFolio.Focus();
                            }
                            else
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.BasculaMateriaPrima_MsgErrorAlGuardar, MessageBoxButton.OK,
                                    MessageImage.Warning);
                                txtFolio.Focus();
                            }
                        }
                        catch
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.BasculaMateriaPrima_MsgErrorAlGuardar, MessageBoxButton.OK,
                                    MessageImage.Warning);
                            txtFolio.Focus();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Carga la pantalla de penalizaciones.
        /// </summary>
        void CargarPantallaPenalizaciones()
        {
            var listaPenalizacion = new List<PenalizacionesInfo>();
            var pantallaPenalizaciones = new Penalizaciones();

            var esOrigen = EsOrigenEnum.Origen;
            if (entradaProductoPorFolio.Contrato.CalidadOrigen == 0)
            {
                esOrigen = EsOrigenEnum.Destino;
            }

            foreach (var entradaProductoDetalle in entradaProductoPorFolio.ProductoDetalle)
            {
                decimal muestras = 0, porcentaje = 0;
                var penalizacion = new PenalizacionesInfo();
                penalizacion.Indicador = entradaProductoDetalle.Indicador.Descripcion;

                if (entradaProductoPorFolio.Producto.SubFamilia.SubFamiliaID == (int)SubFamiliasEnum.Forrajes)
                {
                    EntradaProductoMuestraInfo productoMuestra = entradaProductoDetalle.ProductoMuestras.First(t => t.EsOrigen == esOrigen);

                    muestras += productoMuestra.Descuento;
                    porcentaje += productoMuestra.Porcentaje;
                }
                else
                {
                    foreach (var productoMuestra in entradaProductoDetalle.ProductoMuestras.Where(t => t.EsOrigen == esOrigen))
                    {
                        muestras += productoMuestra.Descuento;
                        porcentaje += productoMuestra.Porcentaje;
                    }
                }

                penalizacion.Descuento = muestras;
                penalizacion.Porcentaje = porcentaje;

                listaPenalizacion.Add(penalizacion);
            }
            pantallaPenalizaciones.listaPenalizacion = listaPenalizacion;
            pantallaPenalizaciones.ShowDialog();
        }

        /// <summary>
        /// Carga la pantalla de Busqueda de Folios.
        /// </summary>
        private void CargarPantallaBusquedaFolios()
        {
            var busqueda = new BusquedaFolioBoletaRecepcion();
            MostrarCentrado(busqueda);
            if (busqueda.Seleccionado)
            {
                txtFolio.Text = busqueda.EntradaProducto.Folio.ToString(CultureInfo.InvariantCulture);
                ValidarFolio();
            }
        }
        /// <summary>
        /// Método que imprime la direccion de la organizacion si es muy larga la recorta y la pinta en el siguiente renglon
        /// </summary>
        /// <param name="e"></param>
        /// <param name="direccion"></param>
        private void ObtenerDireccionOrganizacion(PrintPageEventArgs e, string direccion)
        {
            const int limiteRenglon = 120;
            string letra = "Lucida Console";
            if (direccion.Length > limiteRenglon)
            {
                e.Graphics.DrawString(direccion.Substring(0, limiteRenglon), new Font(letra, 7), Brushes.Black, 15, 62);
                e.Graphics.DrawString(direccion.Substring(limiteRenglon, direccion.Length - limiteRenglon), new Font(letra, 7), Brushes.Black, 15, 78);
            }
            else
            {
                e.Graphics.DrawString(direccion, new Font(letra, 7), Brushes.Black, 15, 65);
            }
        }

        private void ValidarPnaOrigen()
        {
            ParametroGeneralPL parametroGeneralPl = new ParametroGeneralPL();
            ParametroGeneralInfo parametroGeneralGradoPorcentual = parametroGeneralPl.ObtenerPorClaveParametro(ParametrosEnum.GradoPorcentualPerdidaHumedad.ToString());
            ParametroGeneralInfo parametroGeneralGradoBrix = parametroGeneralPl.ObtenerPorClaveParametro(ParametrosEnum.GradosBrix.ToString());

            decimal humedadPermitidoContrato = 0, humedadBoleta = 0, humedadALaFecha = 0, pnaOrigen = 0, porcentajePerdidaHumedad = 0;
            int pesoOrigen = 0;

            try
            {
                if (txtPesoOrigen.Value != null)
                {
                    pesoOrigen = (int)txtPesoOrigen.Value;
                }

                var esOrigen = EsOrigenEnum.Origen;
                if (entradaProductoPorFolio.Contrato.CalidadOrigen == 0)
                {
                    esOrigen = EsOrigenEnum.Destino;
                }

                if (entradaProductoPorFolio.Producto.SubFamilia.SubFamiliaID == (int)SubFamiliasEnum.Granos &&
                    entradaProductoPorFolio.Contrato.PesoNegociar == PesoNegociarEnum.Origen.ToString())
                {
                    ContratoHumedadPL contratoHumedadPl = new ContratoHumedadPL();
                    ContratoHumedadInfo contratoHumedadInfo = contratoHumedadPl.ObtenerHumedadAlaFecha(entradaProductoPorFolio.Contrato);

                    if (contratoHumedadInfo != null)
                    {
                        humedadALaFecha = contratoHumedadInfo.PorcentajeHumedad;

                        if (entradaProductoPorFolio.Contrato.ListaContratoDetalleInfo != null)
                        {
                            foreach (ContratoDetalleInfo contradoDetalleInfo in entradaProductoPorFolio.Contrato.ListaContratoDetalleInfo)
                            {
                                if (contradoDetalleInfo.Indicador.IndicadorId == (int)IndicadoresEnum.Humedad)
                                {
                                    humedadPermitidoContrato = contradoDetalleInfo.PorcentajePermitido;
                                }
                            }

                            foreach (EntradaProductoDetalleInfo entradaProductoDetalle in entradaProductoPorFolio.ProductoDetalle)
                            {
                                if (entradaProductoDetalle.Indicador.IndicadorId == (int)IndicadoresEnum.Humedad)
                                {
                                    foreach (EntradaProductoMuestraInfo entradaProductoMuestraInfo in entradaProductoDetalle.ProductoMuestras.Where(
                                                t => t.EsOrigen == esOrigen))
                                    {
                                        humedadBoleta = entradaProductoMuestraInfo.Porcentaje;
                                    }
                                }
                            }
                            //if (humedadPermitidoContrato > humedadBoleta)
                            //{

                            porcentajePerdidaHumedad = Decimal.Parse(parametroGeneralGradoPorcentual.Valor);

                            if ((humedadPermitidoContrato - humedadBoleta) >= humedadALaFecha)
                            {
                                pnaOrigen = (((humedadALaFecha * porcentajePerdidaHumedad) * pesoOrigen) / 100) + pesoOrigen;
                                //pnaOrigen = humedadALaFecha*porcentajePerdidaHumedad*pesoOrigen;
                            }

                            if ((humedadPermitidoContrato - humedadBoleta) < humedadALaFecha)
                            {
                                humedadALaFecha = (humedadPermitidoContrato - humedadBoleta);
                                pnaOrigen = (((humedadALaFecha * porcentajePerdidaHumedad) * pesoOrigen) / 100) + pesoOrigen;
                                //pnaOrigen = (humedadPermitidoContrato - humedadBoleta)*porcentajePerdidaHumedad*
                                //            pesoOrigen;
                            }

                            txtPesoOrigenBonificacion.Value = Decimal.Round(pnaOrigen);
                            //}
                        }
                    }
                    else
                    {
                        pnaOrigenAutomatico = true;
                    }
                }
                else if (entradaProductoPorFolio.Producto.SubFamilia.SubFamiliaID == (int)SubFamiliasEnum.Líquidos &&
                                entradaProductoPorFolio.Producto.Descripcion == "MELAZA")
                {
                    decimal porcentajeGradosBrix = 0;
                    foreach (var entradaProductoDetalleInfo in entradaProductoPorFolio.ProductoDetalle)
                    {
                        if (entradaProductoDetalleInfo.Indicador.IndicadorId == (int)IndicadoresEnum.Bx)
                        {
                            if (entradaProductoDetalleInfo.ProductoMuestras != null)
                            {
                                foreach (
                                    var entradaProductoMuestraInfo in
                                        entradaProductoDetalleInfo.ProductoMuestras.Where(
                                            t => t.EsOrigen == esOrigen))
                                {
                                    porcentajeGradosBrix = entradaProductoMuestraInfo.Porcentaje;
                                }
                            }
                            else
                            {
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.BasculaMateriaPrima_MsgNoCuentaConMuestras, MessageBoxButton.OK, MessageImage.Stop);
                                InicializarDatos();
                            }
                        }
                    }

                    if (porcentajeGradosBrix == Decimal.Parse(parametroGeneralGradoBrix.Valor))
                    {
                        txtPesoOrigenBonificacion.Value = txtPesoOrigen.Value;
                    }
                    else
                    {
                        pnaOrigen = (pesoOrigen * porcentajeGradosBrix) / Decimal.Parse(parametroGeneralGradoBrix.Valor);
                        txtPesoOrigenBonificacion.Value = Decimal.Round(pnaOrigen);
                    }
                }
                //else if (entradaProductoPorFolio.Contrato.PesoNegociar == PesoNegociarEnum.Origen.ToString() &&
                //         entradaProductoPorFolio.Contrato.ListaContratoHumedad == null)
                //{
                //    pnaOrigenAutomatico = true;
                //}
                else if (entradaProductoPorFolio.Contrato.PesoNegociar == PesoNegociarEnum.Origen.ToString() &&
                         entradaProductoPorFolio.Producto.Descripcion != "MELAZA")
                {
                    pnaOrigenAutomatico = true;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.BasculaMateriaPrima_MsgErrorAlValidarPnaOrigen,
                                  MessageBoxButton.OK, MessageImage.Stop);
            }
        }
        #endregion

        #region Impresion

        /// <summary>
        /// Método que imprime el ticket
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void print_Page(object sender, PrintPageEventArgs e)
        {
            const int posicionX = 15, posicionXConceptos = 120, tamanoFuenteConceptos = 9;
            string letra = "Lucida Console";
            var organizacion = new OrganizacionPL();
            var organizacionInfo = organizacion.ObtenerPorID(organizacionId);

            e.Graphics.DrawString(Properties.Resources.SalidaIndividualGanado_lblHora, new Font(letra, 8), Brushes.Black, posicionX, 10);
            e.Graphics.DrawString(DateTime.Now.ToShortTimeString(), new Font(letra, 8), Brushes.Black, 50, 10);
            e.Graphics.DrawString(Properties.Resources.SalidaIndividualGanado_lblFecha, new Font(letra, 8), Brushes.Black, 140, 10);
            e.Graphics.DrawString(DateTime.Now.ToShortDateString(), new Font(letra, 8), Brushes.Black, 190, 10);

            // Organizacion
            e.Graphics.DrawString(organizacionInfo.Descripcion, new Font(letra, 10), Brushes.Black, posicionX, 40);
            //Direccion de la organizacion
            ObtenerDireccionOrganizacion(e, organizacionInfo.Direccion);

            //Ticket
            e.Graphics.DrawString(Properties.Resources.BasculaMateriaPrima_Ticket.ToUpper(), new Font(letra, 12, System.Drawing.FontStyle.Bold), Brushes.Black, posicionX, 100);
            e.Graphics.DrawString(txtFolio.Text.Trim(), new Font(letra, 12, System.Drawing.FontStyle.Bold), Brushes.Black, 100, 100);

            //Lote
            var almacenInventarioLotePL = new AlmacenInventarioLotePL();
            AlmacenInventarioLoteInfo almacenInventarioLote = almacenInventarioLotePL.ObtenerAlmacenInventarioLotePorId(
                entradaProductoPorFolio.AlmacenInventarioLote.AlmacenInventarioLoteId);
            e.Graphics.DrawString(Properties.Resources.BasculaMateriaPrima_Lote.ToUpper(), new Font(letra, 12, System.Drawing.FontStyle.Bold), Brushes.Black, posicionX, 130);
            if (almacenInventarioLote != null)
            {
                e.Graphics.DrawString(almacenInventarioLote.Lote.ToString(), new Font(letra, 12, System.Drawing.FontStyle.Bold),
                                      Brushes.Black, 100, 130);
            }

            //Producto
            e.Graphics.DrawString(Properties.Resources.BasculaMateriaPrima_Producto.ToUpper(), new Font(letra, tamanoFuenteConceptos), Brushes.Black, posicionX, 170);
            e.Graphics.DrawString(txtProducto.Text.Trim(), new Font(letra, tamanoFuenteConceptos), Brushes.Black, posicionXConceptos, 170);
            //Contrato
            e.Graphics.DrawString(Properties.Resources.BasculaMateriaPrima_Contrato.ToUpper(), new Font(letra, tamanoFuenteConceptos), Brushes.Black, posicionX, 195);
            e.Graphics.DrawString(txtContrato.Text.Trim(), new Font(letra, tamanoFuenteConceptos), Brushes.Black, posicionXConceptos, 195);
            //Proveedor
            e.Graphics.DrawString(Properties.Resources.BasculaMateriaPrima_Proveedor.ToUpper(), new Font(letra, tamanoFuenteConceptos), Brushes.Black, posicionX, 220);
            e.Graphics.DrawString(txtProveedor.Text.Trim(), new Font(letra, tamanoFuenteConceptos), Brushes.Black, posicionXConceptos, 220);
            //Placas
            e.Graphics.DrawString(Properties.Resources.BasculaMateriaPrima_Placas.ToUpper(), new Font(letra, tamanoFuenteConceptos), Brushes.Black, posicionX, 245);
            e.Graphics.DrawString(txtPlacas.Text.Trim(), new Font(letra, tamanoFuenteConceptos), Brushes.Black, posicionXConceptos, 245);
            //Chofer
            e.Graphics.DrawString(Properties.Resources.BasculaMateriaPrima_Chofer.ToUpper(), new Font(letra, tamanoFuenteConceptos), Brushes.Black, posicionX, 270);
            e.Graphics.DrawString(txtChofer.Text.Trim(), new Font(letra, tamanoFuenteConceptos), Brushes.Black, posicionXConceptos, 270);

            //Peso Bruto
            e.Graphics.DrawString(Properties.Resources.BasculaMateriaPrima_PesoBruto.ToUpper(), new Font(letra, 12, System.Drawing.FontStyle.Bold), Brushes.Black, posicionX, 310);
            e.Graphics.DrawString(txtPesoBruto.Text.Trim().PadLeft(10, ' '), new Font(letra, 12, System.Drawing.FontStyle.Bold), Brushes.Black, 220, 310);

            if (txtPesoTara.Value > 0 && txtPesoNeto.Value > 0)
            {
                //Peso Tara
                e.Graphics.DrawString(Properties.Resources.BasculaMateriaPrima_PesoTara.ToUpper(), new Font(letra, 12, System.Drawing.FontStyle.Bold), Brushes.Black, posicionX, 340);
                e.Graphics.DrawString(txtPesoTara.Text.Trim().PadLeft(10, ' '), new Font(letra, 12, System.Drawing.FontStyle.Bold), Brushes.Black, 220, 340);
                //Peso Neto
                e.Graphics.DrawString(Properties.Resources.BasculaMateriaPrima_PesoNeto.ToUpper(), new Font(letra, 12, System.Drawing.FontStyle.Bold), Brushes.Black, posicionX, 370);
                e.Graphics.DrawString(txtPesoNeto.Text.Trim().PadLeft(10, ' '), new Font(letra, 12, System.Drawing.FontStyle.Bold), Brushes.Black, 220, 370);
                //Descuento
                e.Graphics.DrawString(Properties.Resources.BasculaMateriaPrima_Descuentos.ToUpper(), new Font(letra, 12, System.Drawing.FontStyle.Bold), Brushes.Black, posicionX, 400);
                e.Graphics.DrawString(txtDescuentos.Text.Trim().PadLeft(10, ' '), new Font(letra, 12, System.Drawing.FontStyle.Bold), Brushes.Black, 220, 400);
                //Peso NEto Analizado
                e.Graphics.DrawString(Properties.Resources.BasculaMateriaPrima_NetoAnalizado.ToUpper(), new Font(letra, 12, System.Drawing.FontStyle.Bold), Brushes.Black, posicionX, 430);
                e.Graphics.DrawString(txtNetoAnalizado.Text.Trim().PadLeft(10, ' '), new Font(letra, 12, System.Drawing.FontStyle.Bold), Brushes.Black, 220, 430);

                //Registro
                e.Graphics.DrawString(Properties.Resources.BasculaMateriaPrima_Responsable, new Font(letra, 9),
                    Brushes.Black, posicionX, 470);
                e.Graphics.DrawString(Application.Current.Properties["Nombre"].ToString(), new Font(letra, 9),
                    Brushes.Black, 120, 470);

                //Bascula
                e.Graphics.DrawString(Properties.Resources.BasculaMateriaPrima_Bascula, new Font(letra, 9),
                    Brushes.Black, 120, 500);
            }
            else
            {
                //Registro
                e.Graphics.DrawString(Properties.Resources.BasculaMateriaPrima_Responsable, new Font(letra, 9),
                    Brushes.Black, posicionX, 350);
                e.Graphics.DrawString(Application.Current.Properties["Nombre"].ToString(), new Font(letra, 9),
                    Brushes.Black, 120, 350);

                //Bascula
                e.Graphics.DrawString(Properties.Resources.BasculaMateriaPrima_Bascula, new Font(letra, 9),
                    Brushes.Black, 120, 380);
            }
        }


        /// <summary>
        /// Genera la impresion de la boleta de recepción.
        /// </summary>
        private void ImprimirBoletaRecepcion()
        {
            if (entradaProductoPorFolio != null)
            {
                try
                {
                    var recepcionMateriaPl = new RecepcionMateriaPrimaPL();
                    recepcionMateriaPl.ImprimirBoletaRecepcion(ObtenerEtiquetasImpresion(), entradaProductoPorFolio);
                    InicializarDatos();
                    txtFolio.Text = string.Empty;
                    txtFolio.Focus();
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.BasculaMateriaPrima_MsgErrorAlImprimir, MessageBoxButton.OK,
                        MessageImage.Warning);
                }
            }
        }

        /// <summary>
        /// Obtiene todas las etiquetas del resources para los reportes
        /// </summary>
        /// <returns></returns>
        private ImpresionBoletaRecepcionInfo ObtenerEtiquetasImpresion()
        {
            var impresion = new ImpresionBoletaRecepcionInfo();
            impresion.EtiquetaAnalista = Properties.Resources.BasculaDeMateriaPrima_ReporteAnalista;
            impresion.EtiquetaBascula = Properties.Resources.BasculaDeMateriaPrima_ReporteBascula;
            impresion.EtiquetaBodegaTerceros = Properties.Resources.BasculaDeMateriaPrima_ReporteBodegaTerceros;
            impresion.EtiquetaCabezaEmpresa = Properties.Resources.BasculaDeMateriaPrima_ReporteCabezaEmpresa;
            impresion.EtiquetaChofer = Properties.Resources.BasculaDeMateriaPrima_ReporteChofer;
            impresion.EtiquetaCondiciones = Properties.Resources.BasculaDeMateriaPrima_ReporteCondiciones;
            impresion.EtiquetaContrato = Properties.Resources.BasculaDeMateriaPrima_ReporteContrato;
            impresion.EtiquetasDatosAnalisis = Properties.Resources.BasculaDeMateriaPrima_ReporteDatosAnalisis;
            impresion.EtiquetaDatosBascula = Properties.Resources.BasculaDeMateriaPrima_ReporteDatosBascula;
            impresion.EtiquetaDescarga = Properties.Resources.BasculaDeMateriaPrima_ReporteLoteDescarga;
            impresion.EtiquetaDescuento = Properties.Resources.BasculaDeMateriaPrima_ReporteDescuento;
            impresion.EtiquetaDescuentos = Properties.Resources.BasculaDeMateriaPrima_ReporteDescuentos;
            impresion.EtiquetaDestino = Properties.Resources.BasculaDeMateriaPrima_ReporteDestino;
            impresion.EtiquetaDireccion = Properties.Resources.BasculaDeMateriaPrima_ReporteDireccion;
            impresion.EtiquetaFecha = Properties.Resources.BasculaDeMateriaPrima_ReporteFecha;
            impresion.EtiquetaFirma = Properties.Resources.BasculaDeMateriaPrima_ReporteFirma;
            impresion.EtiquetaFolio = Properties.Resources.BasculaDeMateriaPrima_ReporteFolio;
            impresion.EtiquetaHora = Properties.Resources.BasculaDeMateriaPrima_ReporteHora;
            impresion.EtiquetaHumedadPromedio = Properties.Resources.BasculaDeMateriaPrima_ReporteHumedadPromedio;
            impresion.EtiquetaIndicadores = Properties.Resources.BasculaDeMateriaPrima_ReporteIndicadores;
            impresion.EtiquetaInicio = Properties.Resources.BasculaDeMateriaPrima_ReporteInicio;
            impresion.EtiquetaFin = Properties.Resources.BasculaDeMateriaPrima_ReporteFin;
            impresion.EtiquetaLoteAlmacen = Properties.Resources.BasculaDeMateriaPrima_ReporteLoteAlmacen;
            impresion.EtiquetaLoteProceso = Properties.Resources.BasculaDeMateriaPrima_ReporteLoteProceso;
            impresion.EtiquetaMuestra1 = Properties.Resources.BasculaDeMateriaPrima_ReporteMuestra1;
            impresion.EtiquetaMuestra2 = Properties.Resources.BasculaDeMateriaPrima_ReporteMuestra2;
            impresion.EtiquetaMuestrasHumedades = Properties.Resources.BasculaDeMateriaPrima_ReporteMuestrasHumedades;
            impresion.EtiquetaObservaciones = Properties.Resources.BasculaDeMateriaPrima_ReporteObservaciones;
            impresion.EtiquetaPesoBruto = Properties.Resources.BasculaDeMateriaPrima_ReportePesoBruto;
            impresion.EtiquetaPesoNeto = Properties.Resources.BasculaDeMateriaPrima_ReportePesoNeto;
            impresion.EtiquetaPesoNetoAnalizado = Properties.Resources.BasculaDeMateriaPrima_ReportePesoNetoAnalizado;
            impresion.EtiquetaPesoTara = Properties.Resources.BasculaDeMateriaPrima_ReportePesoTara;
            impresion.EtiquetaPlacasCamion = Properties.Resources.BasculaDeMateriaPrima_ReportePlacasCamion;
            impresion.EtiquetasPiezas = Properties.Resources.BasculaDeMateriaPrima_ReportePiezas;
            impresion.EtiquetaProducto = Properties.Resources.BasculaDeMateriaPrima_ReporteProducto;
            impresion.EtiquetaProductoForraje = Properties.Resources.BasculaDeMateriaPrima_ReporteProductoForraje;
            impresion.EtiquetaProveedor = Properties.Resources.BasculaDeMateriaPrima_ReporteProveedor;
            impresion.EtiquetaResponsable = Properties.Resources.BasculaDeMateriaPrima_ReporteResponsable;
            impresion.EtiquetaTiempoEfectivo = Properties.Resources.BasculaDeMateriaPrima_ReporteTiempoEfectivo;
            impresion.EtiquetaTitulo = Properties.Resources.BasculaDeMateriaPrima_ReporteTitulo;
            impresion.EtiquetaKgs = Properties.Resources.BasculaDeMateriaPrima_ReporteKgs;
            impresion.EtiquetaHumedadOrigen = Properties.Resources.BasculaDeMateriaPrima_ReporteHumedadOrigen;
            impresion.EtiquetaOrigen = Properties.Resources.BasculaDeMateriaPrima_ReporteOrigen;

            return impresion;
        }

        #endregion

        #region Botones

        private void imgBuscar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CargarPantallaBusquedaFolios();
        }

        private void imgPenalizaciones_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CargarPantallaPenalizaciones();
        }

        private void txtFolio_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }

        private void txtPesoTara_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (!ValidarPesoTara())
                {
                    e.Handled = true;
                }
            }
        }

        private void txtPesoBruto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (!ValidarPesoBruto())
                {
                    e.Handled = true;
                }
            }
        }

        private void txtPesoOrigen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (!ValidarPesoOrigen())
                {
                    e.Handled = true;
                }
            }
        }

        private void txtPesoOrigenBonificacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                /*
                if (!ValidarPesoOrigenBonificacion())
                {
                    e.Handled = true;
                }*/
            }
        }

        private void txtFolio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                if (!ValidarFolio())
                {
                    e.Handled = true;
                }
            }
        }

        private void txtFolio_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.BasculaMateriaPrima_MsgCancelar,
                MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {
                InicializarDatos();
                txtFolio.Text = string.Empty;
                txtFolio.Focus();
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            ImprimirBoletaRecepcion();
        }

        private void ControlBase_Loaded(object sender, RoutedEventArgs e)
        {
            organizacionId = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
            usuario = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
            nombreImpresora = AuxConfiguracion.ObtenerConfiguracion().ImpresoraRecepcionGanado;

            InicializarDatos();
            InicializarBascula();
            txtFolio.Focus();

            Dispatcher.BeginInvoke(new Action(ValidaOperador), DispatcherPriority.ContextIdle, null);
        }

        private void txtPesoTara_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            txtPesoNeto.Value = 0;
            txtDescuentos.Value = 0;
            txtNetoAnalizado.Value = 0;
            imgAyudaPenalizaciones.Visibility = Visibility.Hidden;

        }

        private void TxtFolio_OnTextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            InicializarDatos();
        }

        private void ControlBase_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (spManager != null)
                {
                    spManager.StopListening();
                    spManager.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void ValidaOperador()
        {
            var operadorPL = new OperadorPL();
            var operadorInfo = operadorPL.ObtenerPorUsuarioId(usuario, organizacionId);
            if (operadorInfo != null)
            {
                operadorId = operadorInfo.OperadorID;
            }

            if (operadorId == 0)
            {
                txtFolio.IsEnabled = false;
                imgBuscar.IsEnabled = false;
                btnGuardar.IsEnabled = false;
                txtPesoOrigen.IsEnabled = false;
                btnCancelar.IsEnabled = false;

                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.BasculaMateriaPrima_MsgOperadorSinRolBasculista,
                                  MessageBoxButton.OK, MessageImage.Stop);

            }
        }

        private void txtNotaVenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (
                (e.Key >= Key.D0 && e.Key <= Key.D9 && !e.KeyboardDevice.IsKeyDown(Key.LeftShift)) || 
                (e.Key >= Key.A && e.Key <= Key.Z) ||
                e.Key == Key.Tab || 
                e.Key == Key.Enter || 
                e.Key == Key.Divide || 
                e.Key == Key.OemMinus || 
                (e.KeyboardDevice.IsKeyDown(Key.LeftShift) && e.Key == Key.OemMinus)
              )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        #endregion
    }
}
