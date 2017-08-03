using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Bascula;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using TipoMovimiento = SIE.Services.Info.Enums.TipoMovimiento;
using System.Threading;
using System.Windows.Threading;

namespace SIE.WinForm.MateriaPrima
{
    /// <summary>
    /// Lógica de interacción para SalidaVentaTraspaso.xaml
    /// </summary>
    public partial class SalidaVentaTraspaso
    {
        #region Atributos
        private SKAyuda<SalidaProductoInfo> skAyudaFolioSalida;
        private SKAyuda<ClienteInfo> skAyudaCliente;
        private SKAyuda<CamionInfo> skAyudaPlacas;
        private SKAyuda<ChoferInfo> skAyudaChofer;
        private SKAyuda<OrganizacionInfo> skAyudaDivision;
        private SKAyuda<CuentaSAPInfo> skAyudaCuenta;
        private OrganizacionInfo organizacionActual;
        private int usuarioId;
        private ProductoInfo productoSeleccionado;
        private AlmacenInventarioLoteInfo almacenInventarioLoteSeleccionado;
        private SalidaProductoInfo salidaActual;
        private ClienteInfo clienteActual;
        private OrganizacionInfo divisionActual;
        private SerialPortManager spManager;
        private FechaInfo fechaActualServidor;
        private TipoMovimientoInfo tipoMovimientoSeleccionado;
        private string nombreImpresora;
        private CuentaSAPInfo cuentaActual;
        private SolicitudAutorizacionInfo solicitudInfo;
        private bool basculaConectada;
        private bool existeSolicitud;
        #endregion

        #region Constructores

        public SalidaVentaTraspaso()
        {
            var organizacionId = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
            ObtenerEmpresa(organizacionId);
            usuarioId = Convert.ToInt32(Application.Current.Properties["UsuarioID"]);
            InitializeComponent();
            AgregarAyudas();
            ObtenerTiposMovimientos();

            ConfiguracionInfo configuracion = AuxConfiguracion.ObtenerConfiguracion();
            if (configuracion != null)
            {
                nombreImpresora = configuracion.ImpresoraRecepcionGanado;
            }
            lblChofer.Content = string.Empty;
            LblChoferReq.Content = string.Empty;
            lblPlacas.Content = string.Empty;
            LblPlacasReq.Content = string.Empty;
            basculaConectada = false;
            existeSolicitud = false;
        }

        #endregion

        #region Metodos
        /// <summary>
        /// Obtiene la empresa
        /// </summary>
        /// <param name="organizacionId"></param>
        private void ObtenerEmpresa(int organizacionId)
        {
            try
            {
                var organizacionPl = new OrganizacionPL();
                organizacionActual = organizacionPl.ObtenerPorID(organizacionId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        /// <summary>
        /// agrega las ayudas
        /// </summary>
        private void AgregarAyudas()
        {
            AgregarAyudaFolio();
            AgregarAyudaCliente();

        }
        /// <summary>
        /// Agrega la ayuda para consultar el folio
        /// </summary>
        private void AgregarAyudaFolio()
        {
            try
            {
                skAyudaFolioSalida = new SKAyuda<SalidaProductoInfo>(0,
                    false,
                    new SalidaProductoInfo
                    {
                        Organizacion = organizacionActual,
                        Activo = EstatusEnum.Activo
                    },
                    "PropiedadClaveSalidaProducto",
                    "PropiedadDescripcionSalidaProducto",
                    true,
                    200,
                    true)
                {
                    AyudaPL = new SalidaProductoPL(),
                    MensajeClaveInexistente = Properties.Resources.SalidaVentaTraspaso_AyudaFolioInvalido,
                    MensajeBusquedaCerrar = Properties.Resources.SalidaVentaTraspaso_SalirSinSeleccionar,
                    MensajeBusqueda = Properties.Resources.SalidaVentaTraspaso_Busqueda,
                    MensajeAgregar = Properties.Resources.SalidaVentaTraspaso_Seleccionar,
                    TituloEtiqueta = Properties.Resources.SalidaVentaTraspaso_LeyendaBusqueda,
                    TituloPantalla = Properties.Resources.SalidaVentaTraspaso_Busqueda_Titulo,

                };
                skAyudaFolioSalida.ObtenerDatos += ObtenerDatosFolio;
                skAyudaFolioSalida.LlamadaMetodosNoExistenDatos += FolioSinDatos;

                skAyudaFolioSalida.AsignaTabIndex(0);
                SplAyudaFolioSalida.Children.Clear();
                SplAyudaFolioSalida.Children.Add(skAyudaFolioSalida);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                         ex.Message,
                         MessageBoxButton.OK, MessageImage.Error);
            }

        }
        /// <summary>
        /// Obtiene los datos del folio
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosFolio(string clave)
        {
            try
            {
                if (skAyudaFolioSalida.Info != null)
                {
                    salidaActual = skAyudaFolioSalida.Info;
                    if (salidaActual.AlmacenInventarioLote.AlmacenInventarioLoteId <= 0)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.SalidaVentaTraspaso_SinSurtido,
                        MessageBoxButton.OK, MessageImage.Stop);
                        LimpiarCampos();
                        return;
                    }
                    int folioSalida = 0;
                    if (int.TryParse(clave, out folioSalida))
                    {
                        salidaActual.FolioSalida = folioSalida;
                    }

                    foreach (
                        TipoMovimientoInfo tmpMovimiento in
                            CboSalida.ItemsSource.Cast<TipoMovimientoInfo>()
                                .Where(
                                    tmpMovimiento =>
                                        tmpMovimiento.Descripcion == skAyudaFolioSalida.Info.TipoMovimiento.Descripcion)
                        )
                    {
                        CboSalida.SelectedItem = tmpMovimiento;
                        CboSalida.IsEnabled = false;
                        salidaActual.TipoMovimiento = tmpMovimiento;
                        break;
                    }

                    almacenInventarioLoteSeleccionado = ObtenerAlmacenInventarioLote(salidaActual);
                    if (almacenInventarioLoteSeleccionado != null)
                    {

                        productoSeleccionado = ObtenerProducto(almacenInventarioLoteSeleccionado);
                        if (productoSeleccionado != null)
                        {
                            salidaActual.Producto = productoSeleccionado;
                            txtProducto.Text = productoSeleccionado.Descripcion;
                        }

                        txtLote.Text = almacenInventarioLoteSeleccionado.Lote.ToString(CultureInfo.InvariantCulture);
                        txtTotalKilogramos.Value = Convert.ToInt32(almacenInventarioLoteSeleccionado.Cantidad);
                        txtCostoProducto.Text = almacenInventarioLoteSeleccionado.PrecioPromedio.ToString("N", CultureInfo.InvariantCulture);

                        txtFecha.Text = salidaActual.FechaSalida.ToString("dd'/'MM'/'yyyy");

                        if (salidaActual.TipoMovimiento.TipoMovimientoID == (int)TipoMovimiento.ProductoSalidaVenta)
                        {
                            clienteActual = ObtenerCliente(salidaActual);
                            AgregarCliente();

                            // Validar si ya cuenta con una solicitud
                            solicitudInfo = new SolicitudAutorizacionInfo();
                            solicitudInfo.OrganizacionID = salidaActual.Organizacion.OrganizacionID;
                            solicitudInfo.FolioSalida = salidaActual.FolioSalida;

                            var solicitudPL = new SolicitudAutorizacionPL();
                            solicitudInfo = solicitudPL.ObtenerDatosSolicitudAutorizacion(solicitudInfo);

                            if (solicitudInfo.SolicitudID > 0)
                            {
                                if (solicitudInfo.EstatusSolicitud == (int)Estatus.AMPAutoriz)
                                {
                                    txtPrecioVenta.Text = solicitudInfo.Precio.ToString();
                                    txtPrecioVenta.IsEnabled = false;
                                    existeSolicitud = false;
                                }
                                else
                                {
                                    existeSolicitud = true;
                                }
                            }
                        }
                        else
                        {
                            divisionActual = ObtenerDivision(salidaActual);
                            if (salidaActual.Chofer.ChoferID > 0)
                            {
                                salidaActual.Chofer = ObtenerChofer(salidaActual.Chofer.ChoferID);
                                AgregarAyudaChofer();
                            }
                            if (salidaActual.Camion.CamionID > 0)
                            {
                                salidaActual.Camion = ObtenerCamion(salidaActual.Camion.CamionID);
                                AgregarAyudaPlacasCamion();
                            }
                            AgregarDivision();
                            txtPrecioVenta.Text = almacenInventarioLoteSeleccionado.PrecioPromedio.ToString("N", CultureInfo.InvariantCulture);
                            if (skAyudaCuenta != null)
                            {
                                skAyudaCuenta.IsEnabled = true;
                            }

                        }
                        txtPesoTara.Text = salidaActual.PesoTara.ToString(CultureInfo.InvariantCulture);
                        AgregarAyudaCuenta();
                        BtnCapturarPesoTara.IsEnabled = salidaActual == null;
                        BtnCapturarPesoBruto.IsEnabled = (salidaActual != null && existeSolicitud == false);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                         ex.Message,
                         MessageBoxButton.OK, MessageImage.Error);
            }
        }


        /// <summary>
        /// No se encontraron datos del folio
        /// </summary>
        private void FolioSinDatos()
        {
            LimpiarCampos();
        }
        /// <summary>
        /// Agrega la ayuda de clientes
        /// </summary>
        private void AgregarAyudaCliente()
        {
            try
            {
                if (salidaActual == null)
                {
                    skAyudaCliente = new SKAyuda<ClienteInfo>(170,
                        true,
                        new ClienteInfo
                        {

                            ClienteID = 0
                        },
                        "PropiedadCodigoCliente",
                        "PropiedadDescripcionCliente",
                        true,
                        1,
                        false)
                    {
                        AyudaPL = new ClientePL(),
                        MensajeClaveInexistente = Properties.Resources.SalidaVentaTraspaso_AyudaClienteInvalido,
                        MensajeBusquedaCerrar = Properties.Resources.SalidaVentaTraspaso_AyudaClienteSalirSinSeleccionar,
                        MensajeBusqueda = Properties.Resources.SalidaVentaTraspaso_AyudaClienteBusqueda,
                        MensajeAgregar = Properties.Resources.SalidaVentaTraspaso_AyudaClienteSeleccionar,
                        TituloEtiqueta = Properties.Resources.SalidaVentaTraspaso_AyudaClienteLeyendaBusqueda,
                        TituloPantalla = Properties.Resources.SalidaVentaTraspaso_AyudaClienteBusqueda_Titulo,

                    };
                    skAyudaCliente.ObtenerDatos += ObtenerDatosCliente;
                    StpAyudaCliente.Children.Clear();
                    StpAyudaCliente.Children.Add(skAyudaCliente);
                    AgregarCliente();

                }
                else
                {

                    var textBox = new TextBox();
                    textBox.Width = double.NaN;
                    textBox.Text = salidaActual.Cliente.CodigoSAP;
                    textBox.IsEnabled = false;
                    textBox.Height = 25;

                    var margin = textBox.Margin;
                    margin.Left = 0;
                    margin.Right = 2;
                    margin.Top = 2;
                    margin.Bottom = 2;

                    textBox.Margin = margin;

                    textBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                    StpAyudaCliente.Children.Clear();
                    StpAyudaCliente.Children.Add(textBox);

                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

        }
        /// <summary>
        /// Obtiene los datos del cliente seleccionado en la ayuda
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosCliente(string clave)
        {
            try
            {
                if (skAyudaCliente.Info != null)
                {
                    if (skAyudaCliente.Info.ClienteID > 0)
                    {
                        clienteActual = skAyudaCliente.Info;
                        return;
                    }

                }
                if (!string.IsNullOrEmpty(clave))
                {
                    clienteActual = new ClienteInfo
                    {
                        ClienteID = int.Parse(clave)
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

        }
        /// <summary>
        /// Agrega la ayuda de la division
        /// </summary>
        private void AgregarAyudaDivision()
        {
            try
            {
                if (salidaActual == null)
                {

                    skAyudaDivision = new SKAyuda<OrganizacionInfo>(170,
                        true,
                        new OrganizacionInfo
                        {
                            Activo = EstatusEnum.Activo
                        },
                        "PropiedadClaveSalidaVentaTraspado",
                        "PropiedadDescripcionSalidaVentaTraspado",
                        true,
                        0,
                        false)
                    {
                        AyudaPL = new OrganizacionPL(),
                        MensajeClaveInexistente = Properties.Resources.SalidaVentaTraspaso_AyudaDivisionInvalido,
                        MensajeBusquedaCerrar = Properties.Resources.SalidaVentaTraspaso_AyudaDivisionSalirSinSeleccionar,
                        MensajeBusqueda = Properties.Resources.SalidaVentaTraspaso_AyudaDivisionBusqueda,
                        MensajeAgregar = Properties.Resources.SalidaVentaTraspaso_AyudaDivisionSeleccionar,
                        TituloEtiqueta = Properties.Resources.SalidaVentaTraspaso_AyudaDivisionLeyendaBusqueda,
                        TituloPantalla = Properties.Resources.SalidaVentaTraspaso_AyudaDivisionBusqueda_Titulo,

                    };
                    skAyudaDivision.ObtenerDatos += ObtenerDatosDivision;
                    StpAyudaCliente.Children.Clear();
                    StpAyudaCliente.Children.Add(skAyudaDivision);
                }
                else
                {
                    if (salidaActual.TipoMovimiento.TipoMovimientoID == (int)TipoMovimiento.ProductoSalidaTraspaso)
                    {
                        var textBox = new TextBox();
                        textBox.Width = double.NaN;
                        textBox.Text = salidaActual.OrganizacionDestino.Descripcion;
                        textBox.IsEnabled = false;
                        textBox.Height = 25;

                        var margin = textBox.Margin;
                        margin.Left = 0;
                        margin.Right = 2;
                        margin.Top = 2;
                        margin.Bottom = 2;

                        textBox.Margin = margin;

                        textBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                        StpAyudaCliente.Children.Clear();
                        StpAyudaCliente.Children.Add(textBox);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

        }
        /// <summary>
        /// Obtiene los datos de la division
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosDivision(string clave)
        {
            try
            {
                if (skAyudaDivision.Info != null)
                {
                    if (skAyudaDivision.Info.OrganizacionID > 0)
                    {
                        divisionActual = skAyudaDivision.Info;
                        return;
                    }

                }
                if (!string.IsNullOrEmpty(clave))
                {
                    divisionActual = new OrganizacionInfo
                    {
                        OrganizacionID = int.Parse(clave)
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        /// <summary>
        /// Agrega la ayuda de la cuenta
        /// </summary>
        private void AgregarAyudaCuenta()
        {
            try
            {
                if (salidaActual != null && tipoMovimientoSeleccionado != null && tipoMovimientoSeleccionado.TipoMovimientoID == (int)TipoMovimiento.ProductoSalidaTraspaso)
                {
                    skAyudaCuenta = new SKAyuda<CuentaSAPInfo>(0,
                        false,
                        new CuentaSAPInfo
                        {
                            CuentaSAPID = 0
                        },
                        "PropiedadClaveSalidaVentaTraspaso",
                        "PropiedadDescripcionSalidaVentaTraspaso",
                        true,
                        160,
                        10,
                        true)
                    {
                        AyudaPL = new CuentaSAPPL(),
                        MensajeClaveInexistente = Properties.Resources.SalidaVentaTraspaso_AyudaCuentaInvalido,
                        MensajeBusquedaCerrar = Properties.Resources.SalidaVentaTraspaso_AyudaCuentaSalirSinSeleccionar,
                        MensajeBusqueda = Properties.Resources.SalidaVentaTraspaso_AyudaCuentaBusqueda,
                        MensajeAgregar = Properties.Resources.SalidaVentaTraspaso_AyudaCuentaSeleccionar,
                        TituloEtiqueta = Properties.Resources.SalidaVentaTraspaso_AyudaCuentaLeyendaBusqueda,
                        TituloPantalla = Properties.Resources.SalidaVentaTraspaso_AyudaCuentaBusqueda_Titulo,

                    };

                    skAyudaCuenta.ObtenerDatos += ObtenerDatosCuenta;
                    StpAyudaCuenta.Children.Clear();
                    StpAyudaCuenta.Children.Add(skAyudaCuenta);
                }
                else
                {
                    var valorTexto = string.Empty;
                    if (salidaActual != null)
                    {
                        if (salidaActual.TipoMovimiento.TipoMovimientoID == (int)TipoMovimiento.ProductoSalidaTraspaso)
                        {
                            valorTexto = salidaActual.CuentaSAP.CuentaSAP;
                        }
                    }


                    var textBox = new TextBox();
                    textBox.Width = double.NaN;
                    textBox.Text = valorTexto;
                    textBox.IsEnabled = false;
                    textBox.Height = 25;

                    var margin = textBox.Margin;
                    margin.Left = 0;
                    margin.Right = 2;
                    margin.Top = 2;
                    margin.Bottom = 2;

                    textBox.Margin = margin;

                    textBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                    StpAyudaCuenta.Children.Clear();
                    StpAyudaCuenta.Children.Add(textBox);

                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

        }
        /// <summary>
        /// Obtiene los datos de la cuenta
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosCuenta(string clave)
        {
            try
            {
                if (skAyudaCuenta.Info != null)
                {
                    if (skAyudaCuenta.Info.CuentaSAPID > 0)
                    {
                        cuentaActual = skAyudaCuenta.Info;
                        salidaActual.CuentaSAP = cuentaActual;
                        skAyudaCuenta.Info = new CuentaSAPInfo
                        {
                            CuentaSAPID = 0
                        };
                        return;
                    }

                }
                if (!string.IsNullOrEmpty(clave))
                {
                    cuentaActual = new CuentaSAPInfo
                    {
                        CuentaSAPID = int.Parse(clave)
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        /// <summary>
        /// Agrega la ayuda para buscar el camion
        /// </summary>
        private void AgregarAyudaPlacasCamion()
        {
            try
            {
                if (salidaActual == null)
                {
                    skAyudaPlacas = new SKAyuda<CamionInfo>(170,
                        true,
                        new CamionInfo
                        {
                            Proveedor = new ProveedorInfo
                            {
                                ProveedorID = 0
                            },
                            CamionID = 0,
                            Activo = EstatusEnum.Activo
                        },
                        "PropiedadClaveSalidaVentaTraspaso",
                        "PropiedadDescripcionSalidaVentaTraspaso",
                        true,
                        1,
                        false)
                    {
                        AyudaPL = new CamionPL(),
                        MensajeClaveInexistente = Properties.Resources.SalidaVentaTraspaso_AyudaCamionInvalido,
                        MensajeBusquedaCerrar = Properties.Resources.SalidaVentaTraspaso_AyudaCamionSalirSinSeleccionar,
                        MensajeBusqueda = Properties.Resources.SalidaVentaTraspaso_AyudaCamionBusqueda,
                        MensajeAgregar = Properties.Resources.SalidaVentaTraspaso_AyudaCamionSeleccionar,
                        TituloEtiqueta = Properties.Resources.SalidaVentaTraspaso_AyudaCamionLeyendaBusqueda,
                        TituloPantalla = Properties.Resources.SalidaVentaTraspaso_AyudaCamionBusqueda_Titulo,

                    };
                    skAyudaPlacas.ObtenerDatos += ObtenerDatosPlacas;
                    skAyudaPlacas.LlamadaMetodosNoExistenDatos += PlacasSinDatos;
                    StpAyudaCamion.Children.Clear();
                    StpAyudaCamion.Children.Add(skAyudaPlacas);
                }
                else
                {
                    if (salidaActual.TipoMovimiento.TipoMovimientoID == (int)TipoMovimiento.ProductoSalidaTraspaso)
                    {
                        var textBox = new TextBox();
                        textBox.Width = double.NaN;
                        textBox.Text = salidaActual.Camion.PlacaCamion;
                        textBox.IsEnabled = false;
                        textBox.Height = 25;

                        var margin = textBox.Margin;
                        margin.Left = 0;
                        margin.Right = 2;
                        margin.Top = 2;
                        margin.Bottom = 2;

                        textBox.Margin = margin;

                        textBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                        StpAyudaCamion.Children.Clear();
                        StpAyudaCamion.Children.Add(textBox);
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

        }
        /// <summary>
        /// Obtiene los datos de las placas seleccionado en la ayuda
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosPlacas(string clave)
        {
            try
            {
                if (skAyudaPlacas.Info == null || skAyudaPlacas.Info.CamionID == 0)
                {
                    int clavePlacas = 0;
                    int.TryParse(skAyudaPlacas.Clave, out clavePlacas);

                    CamionInfo tmpCamion = ObtenerCamion(clavePlacas);

                    if (tmpCamion != null)
                    {
                        skAyudaPlacas.Info = tmpCamion;
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        /// <summary>
        ///Metodo cuando no existene datos en la ayuda de placas
        /// </summary>
        private void PlacasSinDatos()
        {
            if (skAyudaPlacas.Info == null || skAyudaPlacas.Info.CamionID == 0)
            {
                skAyudaPlacas.LimpiarCampos();
            }
        }
        /// <summary>
        /// Agrega la ayuda chofer
        /// </summary>
        private void AgregarAyudaChofer()
        {
            try
            {
                if (salidaActual == null)
                {
                    skAyudaChofer = new SKAyuda<ChoferInfo>(135,
                        false,
                        new ChoferInfo
                        {
                            ChoferID = 0,
                            Activo = EstatusEnum.Activo
                        },
                        "PropiedadClaveSalidaVentaTraspaso",
                        "PropiedadDescripcionSalidaVentaTraspaso",
                        true,
                        30,
                        true)
                    {
                        AyudaPL = new ChoferPL(),
                        MensajeClaveInexistente = Properties.Resources.SalidaVentaTraspaso_AyudaChoferInvalido,
                        MensajeBusquedaCerrar = Properties.Resources.SalidaVentaTraspaso_AyudaChoferSalirSinSeleccionar,
                        MensajeBusqueda = Properties.Resources.SalidaVentaTraspaso_AyudaChoferBusqueda,
                        MensajeAgregar = Properties.Resources.SalidaVentaTraspaso_AyudaChoferSeleccionar,
                        TituloEtiqueta = Properties.Resources.SalidaVentaTraspaso_AyudaChoferLeyendaBusqueda,
                        TituloPantalla = Properties.Resources.SalidaVentaTraspaso_AyudaChoferBusqueda_Titulo,

                    };
                    skAyudaChofer.ObtenerDatos += ObtenerDatosChofer;
                    StpAyudaChofer.Children.Clear();
                    StpAyudaChofer.Children.Add(skAyudaChofer);
                }
                else
                {
                    if (salidaActual.TipoMovimiento.TipoMovimientoID == (int)TipoMovimiento.ProductoSalidaTraspaso)
                    {
                        var textBox = new TextBox();
                        textBox.Width = double.NaN;
                        textBox.Text = salidaActual.Chofer.NombreCompleto;
                        textBox.IsEnabled = false;
                        textBox.Height = 25;

                        var margin = textBox.Margin;
                        margin.Left = 0;
                        margin.Right = 2;
                        margin.Top = 2;
                        margin.Bottom = 2;

                        textBox.Margin = margin;

                        textBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                        StpAyudaChofer.Children.Clear();
                        StpAyudaChofer.Children.Add(textBox);
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

        }
        /// <summary>
        /// Obtiene los datos del chofer
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosChofer(string clave)
        {
            try
            {
                if (skAyudaChofer.Info == null)
                {
                    int claveChofer = 0;
                    int.TryParse(skAyudaChofer.Clave, out claveChofer);

                    ChoferInfo tmpChofer = ObtenerChofer(claveChofer);

                    if (tmpChofer != null)
                    {
                        skAyudaChofer.Info = tmpChofer;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        /// <summary>
        /// Obtiene los datos del chofer
        /// </summary>
        /// <param name="choferId"></param>
        /// <returns></returns>
        private ChoferInfo ObtenerChofer(int choferId)
        {
            ChoferInfo chofer = null;
            try
            {
                var choferPl = new ChoferPL();
                chofer = choferPl.ObtenerPorID(choferId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return chofer;
        }
        /// <summary>
        /// Obtener los datos del camion
        /// </summary>
        /// <param name="camionId"></param>
        /// <returns></returns>
        private CamionInfo ObtenerCamion(int camionId)
        {
            CamionInfo camion = null;
            try
            {
                var camionPl = new CamionPL();
                camion = camionPl.ObtenerPorID(camionId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return camion;
        }
        /// <summary>
        /// Obtiene los tipos de movimientos para productos
        /// </summary>
        private void ObtenerTiposMovimientos()
        {
            try
            {
                IList<TipoMovimientoInfo> tiposMovimiento = new List<TipoMovimientoInfo>();
                var tipoMovimientoPl = new TipoMovimientoPL();

                tiposMovimiento = tipoMovimientoPl.ObtenerMovimientosProducto();

                List<TipoMovimientoInfo> tiposMovimientoFiltrado = tiposMovimiento.Where(tmp => tmp.TipoMovimientoID == (int)TipoMovimiento.ProductoSalidaVenta || tmp.TipoMovimientoID == (int)TipoMovimiento.ProductoSalidaTraspaso).ToList();
                var tipoMovimientoSeleccione = new TipoMovimientoInfo
                {
                    TipoMovimientoID = 0,
                    Descripcion = Properties.Resources.SalidaVentaTraspaso_Seleccione
                };

                tiposMovimientoFiltrado.Insert(0, tipoMovimientoSeleccione);

                CboSalida.ItemsSource = tiposMovimientoFiltrado;
                CboSalida.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                Logger.Error(ex);
            }
        }
        /// <summary>
        /// Obtiene el producto de la salida
        /// </summary>
        /// <param name="almacen"></param>
        /// <returns></returns>
        private ProductoInfo ObtenerProducto(AlmacenInventarioLoteInfo almacen)
        {
            ProductoInfo producto = null;
            try
            {
                var productoPl = new ProductoPL();
                producto = productoPl.ObtenerPorID(new ProductoInfo
                {
                    ProductoId = almacen.AlmacenInventario.ProductoID
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                         ex.Message,
                         MessageBoxButton.OK, MessageImage.Error);
            }
            return producto;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="almacen"></param>
        /// <returns></returns>
        private ProductoInfo ObtenerChofer(AlmacenInventarioLoteInfo almacen)
        {
            ProductoInfo producto = null;
            try
            {
                var productoPl = new ProductoPL();
                producto = productoPl.ObtenerPorID(new ProductoInfo
                {
                    ProductoId = almacen.AlmacenInventario.ProductoID
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                         ex.Message,
                         MessageBoxButton.OK, MessageImage.Error);
            }
            return producto;
        }
        /// <summary>
        /// Obtiene el almacen inventario lote
        /// </summary>
        /// <param name="salida"></param>
        /// <returns></returns>
        private AlmacenInventarioLoteInfo ObtenerAlmacenInventarioLote(SalidaProductoInfo salida)
        {
            AlmacenInventarioLoteInfo resultado = null;
            try
            {
                var almacenInventarioLotePl = new AlmacenInventarioLotePL();
                resultado = almacenInventarioLotePl.ObtenerAlmacenInventarioLotePorId(
                    salida.AlmacenInventarioLote.AlmacenInventarioLoteId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                         ex.Message,
                         MessageBoxButton.OK, MessageImage.Error);
            }
            return resultado;
        }
        /// <summary>
        /// Obtiene el cliente
        /// </summary>
        /// <param name="salida"></param>
        /// <returns></returns>
        private ClienteInfo ObtenerCliente(SalidaProductoInfo salida)
        {
            ClienteInfo resultado = null;
            try
            {
                var clientePl = new ClientePL();
                resultado = clientePl.ObtenerPorID(salida.Cliente.ClienteID);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                         ex.Message,
                         MessageBoxButton.OK, MessageImage.Error);
            }
            return resultado;
        }
        /// <summary>
        /// obtiene los datos de la division
        /// </summary>
        /// <param name="salida"></param>
        /// <returns></returns>
        private OrganizacionInfo ObtenerDivision(SalidaProductoInfo salida)
        {
            OrganizacionInfo resultado = null;
            try
            {
                var organizacionPl = new OrganizacionPL();
                resultado = organizacionPl.ObtenerPorID(salida.OrganizacionDestino.OrganizacionID);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                         ex.Message,
                         MessageBoxButton.OK, MessageImage.Error);
            }
            return resultado;
        }
        /// <summary>
        /// Inicializa la bascula
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
                    Properties.Resources.SalidaVentaTraspaso_MsgErrorBascula,
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
        /// Agrega los datos del cliente a la ayuda
        /// </summary>
        private void AgregarCliente()
        {
            try
            {
                if (clienteActual != null)
                {
                    salidaActual.Cliente = clienteActual;
                    AgregarAyudaCliente();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        /// <summary>
        /// Agrega la division
        /// </summary>
        private void AgregarDivision()
        {
            try
            {
                if (divisionActual != null)
                {
                    salidaActual.OrganizacionDestino = divisionActual;
                    AgregarAyudaDivision();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        /// <summary>
        /// Agrega cuenta
        /// </summary>
        private void AgregarCuenta()
        {
            try
            {
                if (cuentaActual != null)
                {
                    skAyudaCuenta.Info = cuentaActual;
                    skAyudaCuenta.Clave = cuentaActual.CuentaSAP;
                    skAyudaCuenta.Descripcion = cuentaActual.Descripcion;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Decide cual impreion se deve de realizar
        /// </summary>
        private void ImprimirTicket()
        {
            Thread.Sleep(200);
            Dispatcher.BeginInvoke(new Action(() =>
            {
                try
                {            
                    bool exito;
                    if (salidaActual == null)
                    {
                        exito = GuardarPrimerPesaje();
                        if (exito)
                        {
                            ImprimirPrimerTicket();
                            LimpiarCampos();
                        }
                    }
                    else
                    {
                        exito = GuardarSegundoPesaje();
                    
                        if (exito)
                        {
                            if (ImprimirSegundoTicket())
                            {
                                BtnGuardar.IsEnabled = true;
                            }
                            if (TipoMovimiento.ProductoSalidaVenta.GetHashCode() == ((TipoMovimientoInfo)CboSalida.SelectedItem).TipoMovimientoID)
                            {
                                chkGeneraFactura.IsEnabled = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }
                finally
                {
                    Mouse.SetCursor(Cursors.None);
                    ucCargando.Visibility = System.Windows.Visibility.Hidden;
                    BtnImprimirTicket.IsEnabled = true;
                }
            }), null);
        }

        /// <summary>
        /// Guarda el primer pesaja (tara)
        /// </summary>
        /// <returns></returns>
        private bool GuardarPrimerPesaje()
        {
            try
            {

                ResultadoValidacion validar = ValidarCamposVacios();
                if (validar.Resultado)
                {
                    int pesoTara = 0;
                    var valorTara = txtPesoTara.Text.Replace(",", "");

                    int.TryParse(valorTara, out pesoTara);

                    if (pesoTara <= 0)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.SalidaVentaTraspaso_PesoTaraMenor,
                        MessageBoxButton.OK, MessageImage.Stop);
                        if (txtPesoTara.IsEnabled)
                        {
                            txtPesoTara.Focus();
                        }
                        else
                        {
                            BtnCapturarPesoTara.Focus();
                        }
                        return false;
                    }
                    var salidaProducto = new SalidaProductoInfo()
                    {
                        Organizacion = organizacionActual,

                        TipoMovimiento = tipoMovimientoSeleccionado,
                        OrganizacionDestino = new OrganizacionInfo
                        {
                            OrganizacionID = tipoMovimientoSeleccionado.TipoMovimientoID == (int)TipoMovimiento.ProductoSalidaVenta ? 0 : divisionActual.OrganizacionID
                        },
                        Cliente = new ClienteInfo
                        {
                            ClienteID = tipoMovimientoSeleccionado.TipoMovimientoID == (int)TipoMovimiento.ProductoSalidaVenta ? clienteActual.ClienteID : 0
                        },
                        Chofer = new ChoferInfo
                        {
                            ChoferID = tipoMovimientoSeleccionado.TipoMovimientoID == (int)TipoMovimiento.ProductoSalidaVenta ? 0 : skAyudaChofer.Info.ChoferID
                        },
                        Camion = new CamionInfo
                        {
                            CamionID = tipoMovimientoSeleccionado.TipoMovimientoID == (int)TipoMovimiento.ProductoSalidaVenta ? 0 : skAyudaPlacas.Info.CamionID
                        },

                        PesoTara = pesoTara,
                        FechaSalida = fechaActualServidor.FechaActual,
                        Activo = EstatusEnum.Activo,
                        UsuarioCreacionId = usuarioId

                    };


                    var salidaProductoPl = new SalidaProductoPL();
                    salidaActual = salidaProductoPl.GuardarPrimerPesajeSalida(salidaProducto);

                    //Aviso que se guardaron los datos exitosamente e indica cual es el número de folio
                    if (salidaActual != null)
                    {
                        string mensaje = String.Format(Properties.Resources.SalidaVentaTraspaso_TicketFolio, salidaActual.FolioSalida);
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            mensaje,
                        MessageBoxButton.OK, MessageImage.Correct);
                        return true;
                    }

                    return false;

                }
                else
                {

                    var mensaje = string.IsNullOrEmpty(validar.Mensaje)
                        ? Properties.Resources.SalidaVentaTraspaso_DatosBlancos
                        : validar.Mensaje;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        mensaje, MessageBoxButton.OK, MessageImage.Stop);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        ex.Message, MessageBoxButton.OK, MessageImage.Error);
                return false;
            }
        }
        /// <summary>
        /// Guarda el segundo pesaje
        /// </summary>
        /// <returns></returns>
        private bool GuardarSegundoPesaje()
        {
            try
            { 
                ResultadoValidacion validar = ValidarCamposVacios();
                ClientePL clientePl = new ClientePL();
                ParametrosValidacionLimiteCredito parametros;

                decimal precioVenta = 0;
                decimal pesoNeto = 0;
                decimal costoProducto = 0;

                if(validar.Resultado)
                {
                    if (!decimal.TryParse(txtPrecioVenta.Text.Replace(",", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out precioVenta))
                    {
                        precioVenta = 0;
                    }
                    
                    if (!decimal.TryParse(txtPesoNeto.Text.Replace(",", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out pesoNeto))
                    {
                        pesoNeto = 0;
                    }
                    if (tipoMovimientoSeleccionado.TipoMovimientoID == (int)TipoMovimiento.ProductoSalidaVenta)
                    {
                        parametros = new ParametrosValidacionLimiteCredito
                        {
                            CodigoSAP = clienteActual.CodigoSAP,
                            Importe = precioVenta * pesoNeto,
                            Moneda = organizacionActual.Moneda,
                            Sociedad = organizacionActual.Sociedad
                        };

                        validar = clientePl.validarLimiteCredito(parametros);
                    }
                }

                if (validar.Resultado)
                {
                    int pesoBruto = 0;
                    var valorBruto = txtPesoBruto.Text.Replace(",", "");
                    int.TryParse(valorBruto, out pesoBruto);
                    if (salidaActual == null)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.SalidaVentaTraspaso_SegundoPesajeIncorrecto,
                        MessageBoxButton.OK, MessageImage.Stop);
                        return false;
                    }
                    if (pesoBruto <= salidaActual.PesoTara)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.SalidaVentaTraspaso_SegundoBrutoMenor,
                        MessageBoxButton.OK, MessageImage.Stop);
                        return false;
                    }

                    if (!decimal.TryParse(txtCostoProducto.Text.Replace(",", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out costoProducto))
                    {
                        costoProducto = 0;
                    }

                    salidaActual.UsuarioModificacionId = usuarioId;

                    salidaActual.PesoBruto = pesoBruto;
                    salidaActual.CuentaSAP = cuentaActual;
                    salidaActual.Precio = precioVenta;

                    var salidaProductoPl = new SalidaProductoPL();
                    salidaActual = salidaProductoPl.GuardarSegundoPesajeSalida(salidaActual);
                    //Aviso que se guardaron los datos exitosamente e indica cual es el número de folio
                    if (salidaActual != null)
                    {
                        return true;

                    }

                    return false;

                }
                else
                {
                    var mensaje = string.IsNullOrEmpty(validar.Mensaje)
                        ? Properties.Resources.SalidaVentaTraspaso_DatosBlancos
                        : validar.Mensaje;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        mensaje, MessageBoxButton.OK, MessageImage.Stop);
                      
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        ex.Message, MessageBoxButton.OK, MessageImage.Error);
                
                return false;
            }
            
        }

        /// <summary>
        /// Valida los campos vacios
        /// </summary>
        /// <returns></returns>
        private ResultadoValidacion ValidarCamposVacios()
        {

            var resultado = new ResultadoValidacion();
            try
            {
                int pesoNeto = txtPesoNeto.Value.HasValue ? txtPesoNeto.Value.Value : 0;
                int pesoLote = txtTotalKilogramos.Value.HasValue ? txtTotalKilogramos.Value.Value : 0;
                if (pesoNeto > pesoLote)
                {
                    resultado.Mensaje = Properties.Resources.SalidaVentaTraspaso_KilosMayorLote;
                    resultado.Resultado = false;
                    return resultado;
                }
                if (tipoMovimientoSeleccionado == null)
                {
                    resultado.Mensaje = Properties.Resources.SalidaVentaTraspaso_SalidaRequerido;
                    resultado.Resultado = false;
                    CboSalida.Focus();
                    return resultado;
                }
                if (tipoMovimientoSeleccionado.TipoMovimientoID == 0)
                {
                    resultado.Mensaje = Properties.Resources.SalidaVentaTraspaso_SalidaRequerido;
                    resultado.Resultado = false;
                    CboSalida.Focus();
                    return resultado;
                }

                if (tipoMovimientoSeleccionado.TipoMovimientoID == (int)TipoMovimiento.ProductoSalidaTraspaso)
                {
                    if (skAyudaPlacas != null)
                    {
                        if (string.IsNullOrEmpty(skAyudaPlacas.Clave))
                        {
                            resultado.Mensaje = Properties.Resources.SalidaVentaTraspaso_PlacasRequerido;
                            resultado.Resultado = false;
                            skAyudaPlacas.Focus();
                            return resultado;
                        }
                    }
                    else
                    {
                        if (salidaActual.Camion.CamionID == 0)
                        {
                            resultado.Mensaje = Properties.Resources.SalidaVentaTraspaso_PlacasRequerido;
                            resultado.Resultado = false;
                            return resultado;
                        }
                    }

                    if (divisionActual == null)
                    {
                        resultado.Mensaje = Properties.Resources.SalidaVentaTraspaso_DivisionRequerido;
                        resultado.Resultado = false;
                        skAyudaDivision.Focus();
                        return resultado;
                    }
                    if (salidaActual != null)
                    {
                        if (string.IsNullOrEmpty(salidaActual.CuentaSAP.CuentaSAP))
                        {
                            resultado.Mensaje = Properties.Resources.SalidaVentaTraspaso_CuentaRequerido;
                            resultado.Resultado = false;
                            return resultado;
                        }
                    }

                    if (skAyudaChofer != null)
                    {
                        if (string.IsNullOrEmpty(skAyudaChofer.Clave))
                        {
                            resultado.Mensaje = Properties.Resources.SalidaVentaTraspaso_ChoferRequerido;
                            resultado.Resultado = false;
                            skAyudaChofer.Focus();
                            return resultado;
                        }
                    }


                }
                else
                {
                    if (clienteActual == null)
                    {
                        resultado.Mensaje = Properties.Resources.SalidaVentaTraspaso_ClienteRequerido;
                        resultado.Resultado = false;
                        skAyudaCliente.Focus();
                        return resultado;
                    }

                    if (salidaActual != null)
                    {

                        if (string.IsNullOrEmpty(txtPrecioVenta.Text))
                        {
                            resultado.Mensaje = Properties.Resources.SalidaVentaTraspaso_CostoRequerido;
                            resultado.Resultado = false;
                            txtPrecioVenta.Focus();
                            return resultado;
                        }
                        decimal precioVenta = 0;
                        if (
                            !decimal.TryParse(txtPrecioVenta.Text.Replace(",", ""), NumberStyles.AllowDecimalPoint,
                                CultureInfo.InvariantCulture, out precioVenta))
                        {
                            resultado.Mensaje = Properties.Resources.SalidaVentaTraspaso_PrecioVentaIncorrecto;
                            resultado.Resultado = false;
                            txtPrecioVenta.Focus();

                            return resultado;
                        }
                    }
                }

                if (string.IsNullOrEmpty(txtPesoTara.Text))
                {
                    resultado.Mensaje = Properties.Resources.SalidaVentaTraspaso_TaraRequerido;
                    resultado.Resultado = false;
                    BtnCapturarPesoTara.Focus();
                    return resultado;
                }

                if (salidaActual != null)
                {
                    if (string.IsNullOrEmpty(txtPesoBruto.Text))
                    {
                        resultado.Mensaje = Properties.Resources.SalidaVentaTraspaso_BrutoRequerido;
                        resultado.Resultado = false;
                        BtnCapturarPesoBruto.Focus();
                        return resultado;
                    }
                }

                resultado.Resultado = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return resultado;
        }
        /// <summary>
        /// Imprime el primer ticket
        /// </summary>
        private bool ImprimirPrimerTicket()
        {
            try
            {

                //Código para imprimir 
                var print = new PrintDocument();
                print.PrintPage += ImprimerTicketTara;
                print.PrinterSettings.PrinterName = nombreImpresora;
                print.PrintController = new StandardPrintController();
                print.Print();
                return true;

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        String.Format("{0} {1}", Properties.Resources.SalidaVentaTraspaso_ErrorImpresion, nombreImpresora),
                        MessageBoxButton.OK, MessageImage.Error);
                return false;
            }
        }
        /// <summary>
        /// Imprime el segundo ticket
        /// </summary>
        private bool ImprimirSegundoTicket()
        {
            try
            {

                //Código para imprimir
                var print = new PrintDocument();
                print.PrintPage += ImprimerTicketBruto;
                print.PrinterSettings.PrinterName = nombreImpresora;
                print.PrintController = new StandardPrintController();
                print.Print();
                return true;

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        String.Format("{0} {1}", Properties.Resources.SalidaVentaTraspaso_ErrorImpresion, nombreImpresora),
                        MessageBoxButton.OK, MessageImage.Error);
                return false;
            }
        }

        /// <summary>
        /// Guarda los datos mostrados en pantalla en la tabla RegistroVigilancia (previa verificación)
        /// </summary>
        private void ImprimerTicketTara(object sender, PrintPageEventArgs e)
        {
            try
            {
                const int tamanoFuenteTitulo = 14, tamanoFuenteTexto = 11;
                const int espacioMargen = 10;
                string fechaEntrada = txtFecha.Text;
                string horaEntrada = DateTime.Now.ToShortTimeString();

                var stringFormatCenter = new StringFormat();
                stringFormatCenter.Alignment = StringAlignment.Near;
                stringFormatCenter.LineAlignment = StringAlignment.Near;
                var rectangulo = new Rectangle(espacioMargen, 47, 300, 100);

                e.Graphics.DrawString(
                    string.Format("{0} {1}", Properties.Resources.SalidaVentaTraspaso_TicketHora, horaEntrada),
                    new Font("Arial", tamanoFuenteTexto),
                    Brushes.Black, espacioMargen, 10);
                e.Graphics.DrawString(
                    string.Format("{0} {1}", Properties.Resources.SalidaVentaTraspaso_TicketFecha, fechaEntrada),
                    new Font("Arial", tamanoFuenteTexto),
                    Brushes.Black, espacioMargen + 150, 10);


                e.Graphics.DrawString(organizacionActual.Descripcion,
                    new Font("Arial", tamanoFuenteTexto), Brushes.Black, espacioMargen, 30);
                e.Graphics.DrawString(organizacionActual.Direccion ?? string.Empty,
                    new Font("Arial", tamanoFuenteTexto), Brushes.Black, rectangulo, stringFormatCenter);

                e.Graphics.DrawString(
                    string.Format("{0} {1}", Properties.Resources.SalidaVentaTraspaso_Ticket, salidaActual.FolioSalida),
                    new Font("Arial", tamanoFuenteTitulo),
                    Brushes.Black, espacioMargen, 120);



                if (salidaActual.TipoMovimiento.TipoMovimientoID == (int)TipoMovimiento.ProductoSalidaVenta)
                {
                    e.Graphics.DrawString(
                        string.Format("{0} {1}", Properties.Resources.SalidaVentaTraspaso_TicketConcepto,
                            salidaActual.TipoMovimiento.Descripcion), new Font("Arial", tamanoFuenteTexto),
                        Brushes.Black, espacioMargen, 170);
                    e.Graphics.DrawString(
                        string.Format("{0} {1}", Properties.Resources.SalidaVentaTraspaso_TicketCliente,
                            clienteActual.Descripcion), new Font("Arial", tamanoFuenteTexto),
                        Brushes.Black, espacioMargen, 185);
                }
                else
                {
                    e.Graphics.DrawString(
                        string.Format("{0} {1}", Properties.Resources.SalidaVentaTraspaso_TicketPlacas,
                            skAyudaPlacas.Descripcion), new Font("Arial", tamanoFuenteTexto),
                        Brushes.Black, espacioMargen, 170);
                    e.Graphics.DrawString(
                        string.Format("{0} {1}", Properties.Resources.SalidaVentaTraspaso_TicketChofer,
                            skAyudaChofer.Descripcion), new Font("Arial", tamanoFuenteTexto),
                        Brushes.Black, espacioMargen, 190);
                    e.Graphics.DrawString(
                        string.Format("{0} {1}", Properties.Resources.SalidaVentaTraspaso_TicketConcepto,
                            salidaActual.TipoMovimiento.Descripcion), new Font("Arial", tamanoFuenteTexto),
                        Brushes.Black, espacioMargen, 210);
                    e.Graphics.DrawString(
                        string.Format("{0} {1}", Properties.Resources.SalidaVentaTraspaso_TicketDivision,
                            divisionActual.Descripcion), new Font("Arial", tamanoFuenteTexto),
                        Brushes.Black, espacioMargen, 230);
                }


                e.Graphics.DrawString(
                    string.Format("{0} {1}", Properties.Resources.SalidaVentaTraspaso_TicketTara, salidaActual.PesoTara.ToString("N0")),
                    new Font("Arial", tamanoFuenteTitulo),
                    Brushes.Black, espacioMargen, 280);


                e.Graphics.DrawString(
                    string.Format("{0} {1}", Properties.Resources.SalidaVentaTraspaso_TicketRegistro,
                        Application.Current.Properties["Nombre"].ToString()), new Font("Arial", tamanoFuenteTexto),
                    Brushes.Black,
                    espacioMargen, 330);

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

        }
        /// <summary>
        /// Imprime el tickete del peso bruto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImprimerTicketBruto(object sender, PrintPageEventArgs e)
        {
            try
            {
                const int tamanoFuenteTitulo = 12, tamanoFuenteTexto = 11;
                const int espacioMargen = 10;
                string fechaEntrada = txtFecha.Text;

                string horaEntrada = DateTime.Now.ToShortTimeString();

                var stringFormatCenter = new StringFormat();
                stringFormatCenter.Alignment = StringAlignment.Near;
                stringFormatCenter.LineAlignment = StringAlignment.Near;
                var rectangulo = new Rectangle(espacioMargen, 47, 300, 100);

                e.Graphics.DrawString(
                    string.Format("{0} {1}", Properties.Resources.SalidaVentaTraspaso_TicketHora, horaEntrada),
                    new Font("Arial", tamanoFuenteTexto),
                    Brushes.Black, espacioMargen, 10);
                e.Graphics.DrawString(
                    string.Format("{0} {1}", Properties.Resources.SalidaVentaTraspaso_TicketFecha, fechaEntrada),
                    new Font("Arial", tamanoFuenteTexto),
                    Brushes.Black, espacioMargen + 150, 10);


                e.Graphics.DrawString(organizacionActual.Descripcion,
                    new Font("Arial", tamanoFuenteTexto), Brushes.Black, espacioMargen, 30);
                e.Graphics.DrawString(organizacionActual.Direccion ?? string.Empty,
                    new Font("Arial", tamanoFuenteTexto), Brushes.Black, rectangulo, stringFormatCenter);

                e.Graphics.DrawString(
                    string.Format("{0} {1}", Properties.Resources.SalidaVentaTraspaso_Ticket, salidaActual.FolioSalida),
                    new Font("Arial", tamanoFuenteTitulo),
                    Brushes.Black, espacioMargen, 120);



                if (salidaActual.TipoMovimiento.TipoMovimientoID == (int)TipoMovimiento.ProductoSalidaVenta)
                {
                    e.Graphics.DrawString(
                        string.Format("{0} {1}", Properties.Resources.SalidaVentaTraspaso_TicketConcepto,
                            salidaActual.TipoMovimiento.Descripcion), new Font("Arial", tamanoFuenteTexto),
                        Brushes.Black, espacioMargen, 170);
                    e.Graphics.DrawString(
                        string.Format("{0} {1}", Properties.Resources.SalidaVentaTraspaso_TicketCliente,
                            clienteActual.Descripcion), new Font("Arial", tamanoFuenteTexto),
                        Brushes.Black, espacioMargen, 185);
                }
                else
                {
                    e.Graphics.DrawString(
                        string.Format("{0} {1}", Properties.Resources.SalidaVentaTraspaso_TicketPlacas,
                            salidaActual.Camion.PlacaCamion), new Font("Arial", tamanoFuenteTexto),
                        Brushes.Black, espacioMargen, 170);
                    e.Graphics.DrawString(
                        string.Format("{0} {1}", Properties.Resources.SalidaVentaTraspaso_TicketChofer,
                            salidaActual.Chofer.NombreCompleto), new Font("Arial", tamanoFuenteTexto),
                        Brushes.Black, espacioMargen, 190);
                    e.Graphics.DrawString(
                        string.Format("{0} {1}", Properties.Resources.SalidaVentaTraspaso_TicketConcepto,
                            salidaActual.TipoMovimiento.Descripcion), new Font("Arial", tamanoFuenteTexto),
                        Brushes.Black, espacioMargen, 210);
                    e.Graphics.DrawString(
                        string.Format("{0} {1}", Properties.Resources.SalidaVentaTraspaso_TicketDivision,
                            divisionActual.Descripcion), new Font("Arial", tamanoFuenteTexto),
                        Brushes.Black, espacioMargen, 230);
                }

                var rectanguloProducto = new Rectangle(espacioMargen, 270, 300, 100);
                e.Graphics.DrawString(
                    string.Format("{0} {1}", Properties.Resources.SalidaVentaTraspaso_TicketProducto,
                        productoSeleccionado.Descripcion), new Font("Arial", tamanoFuenteTitulo),
                    Brushes.Black, rectanguloProducto, stringFormatCenter);
                int sumarPosision = 0;
                if (productoSeleccionado.Descripcion.Length > 25)
                {
                    sumarPosision = 20;
                }

                e.Graphics.DrawString(
                    string.Format("{0} {1}", Properties.Resources.SalidaVentaTraspaso_TicketBruto,
                        salidaActual.PesoBruto.ToString("N0")), new Font("Arial", tamanoFuenteTitulo),
                    Brushes.Black, espacioMargen, 290 + sumarPosision);

                e.Graphics.DrawString(
                    string.Format("{0} {1}", Properties.Resources.SalidaVentaTraspaso_TicketTara, salidaActual.PesoTara.ToString("N0")),
                    new Font("Arial", tamanoFuenteTitulo),
                    Brushes.Black, espacioMargen, 310 + sumarPosision);

                e.Graphics.DrawString(
                    string.Format("{0} {1}", Properties.Resources.SalidaVentaTraspaso_TicketNeto,
                        (salidaActual.PesoBruto - salidaActual.PesoTara).ToString("N0")),
                    new Font("Arial", tamanoFuenteTitulo),
                    Brushes.Black, espacioMargen, 330 + sumarPosision);

                var rectanguloObservaciones = new Rectangle(espacioMargen, 380 + sumarPosision, 300, 100);
                e.Graphics.DrawString(
                    string.Format("{0} {1}", Properties.Resources.SalidaVentaTraspaso_TicketObservaciones,
                        salidaActual.Observaciones), new Font("Arial", tamanoFuenteTexto),
                    Brushes.Black, rectanguloObservaciones, stringFormatCenter);

                if (salidaActual.Observaciones.Length > 25)
                {
                    sumarPosision += 20 * (salidaActual.Observaciones.Length / 20);
                }

                if (salidaActual.Producto.SubFamilia.SubFamiliaID == (int)SubFamiliasEnum.Forrajes)
                {
                    if (productoSeleccionado.Descripcion.Length > 25)
                    {
                        sumarPosision += 20 * (productoSeleccionado.Descripcion.Length / 20);
                    }
                    e.Graphics.DrawString(
                        string.Format("{0} {1}", Properties.Resources.SalidaVentaTraspaso_TicketPiezas,
                            salidaActual.Piezas.ToString(CultureInfo.InvariantCulture)),
                        new Font("Arial", tamanoFuenteTitulo),
                        Brushes.Black, espacioMargen, 400 + sumarPosision);
                }


                e.Graphics.DrawString(
                    string.Format("{0} {1}", Properties.Resources.SalidaVentaTraspaso_TicketRegistro,
                        Application.Current.Properties["Nombre"].ToString()), new Font("Arial", tamanoFuenteTexto),
                    Brushes.Black,
                    espacioMargen, 470 + sumarPosision);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

        }

        /// <summary>
        /// Limpia los datos
        /// </summary>
        private void LimpiarCampos()
        {
            try
            {
                salidaActual = null;
                clienteActual = null;
                divisionActual = null;
                cuentaActual = null;
                skAyudaFolioSalida.LimpiarCampos();
                if (skAyudaCuenta != null)
                {
                    skAyudaCuenta.LimpiarCampos();
                }

                if (skAyudaCliente != null)
                {
                    skAyudaCliente.LimpiarCampos();
                    skAyudaCliente.IsEnabled = true;
                }

                if (skAyudaDivision != null)
                {
                    skAyudaDivision.LimpiarCampos();
                    skAyudaDivision.IsEnabled = true;
                }

                if (skAyudaChofer != null)
                {
                    skAyudaChofer.LimpiarCampos();
                    StpAyudaChofer.Children.Clear();
                }

                if (skAyudaPlacas != null)
                {
                    skAyudaPlacas.LimpiarCampos();
                    skAyudaPlacas = null;
                }

                StpAyudaCamion.Children.Clear();

                StpAyudaChofer.Children.Clear();
                StpAyudaCamion.Children.Clear();

                lblChofer.Content = string.Empty;
                LblChoferReq.Content = string.Empty;
                lblPlacas.Content = string.Empty;
                LblPlacasReq.Content = string.Empty;
                txtCostoProducto.Clear();
                txtPrecioVenta.Text = string.Empty;
                txtPrecioVenta.IsEnabled = true;
                txtFecha.Clear();
                txtKilogramosVenta.Clear();
                txtLote.Clear();
                txtPesoBruto.Text = string.Empty;
                txtPesoNeto.Text = string.Empty;
                txtPesoBruto.IsEnabled = false;
                txtPesoTara.Text = string.Empty;
                txtPesoTara.IsEnabled = false;
                txtProducto.Clear();
                txtTotalKilogramos.Text = string.Empty;
                TxtDisplayPeso.Clear();
                if (CboSalida.SelectedIndex > 0)
                {
                    CboSalida.SelectedIndex = 0;
                }
                BtnCancelar.IsEnabled = false;
                BtnCapturarPesoTara.IsEnabled = false;
                BtnCapturarPesoBruto.IsEnabled = false;
                chkGeneraFactura.IsEnabled = false;
                chkGeneraFactura.IsChecked = true;
                BtnGuardar.IsEnabled = false;
                skAyudaChofer = null;
                AgregarAyudaCliente();
                CboSalida.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        #endregion

        #region Eventos

        /// <summary>
        /// Evento de carga de la pantalla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SalidaVentaTraspaso_OnLoaded(object sender, RoutedEventArgs e)
        {
            InicializarBascula();
            configurarMensajeEspera();
        }

        private void configurarMensajeEspera()
        {
            double left = (cvLoading.ActualWidth - ucCargando.ActualWidth) / 2;
            Canvas.SetLeft(ucCargando, left);

            double top = (cvLoading.ActualHeight - ucCargando.ActualHeight) / 2;
            Canvas.SetTop(ucCargando, top);
        }

        /// <summary>
        /// evento de seleccion del combo de tipo salida
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboSalida_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                skAyudaCliente = null;
                skAyudaDivision = null;
                txtPrecioVenta.IsEnabled = false;
                lblCliente.Content = Properties.Resources.SalidaVentaTraspaso_labelCliente;
                lblKilogramosVenta.Content = Properties.Resources.SalidaVentaTraspaso_labelKilogramosVenta;
                lblCosto.Content = Properties.Resources.SalidaVentaTraspaso_labelCostoVenta;

                if (CboSalida.SelectedItem != null)
                {
                    if (((TipoMovimientoInfo)CboSalida.SelectedItem).TipoMovimientoID == TipoMovimiento.ProductoSalidaVenta.GetHashCode())
                    {
                        chkGeneraFactura.IsChecked = true;
                        chkGeneraFactura.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        chkGeneraFactura.IsChecked = false;
                        chkGeneraFactura.Visibility = Visibility.Hidden;
                    }
                    txtPesoTara.Text = string.Empty;
                    txtFecha.Text = string.Empty;
                    tipoMovimientoSeleccionado = (TipoMovimientoInfo)CboSalida.SelectedItem;
                    BtnCancelar.IsEnabled = true;
                    AgregarAyudaCuenta();
                    if (tipoMovimientoSeleccionado.TipoMovimientoID == 0)
                    {
                        LimpiarCampos();
                        return;
                    }

                    if (tipoMovimientoSeleccionado != null)
                    {
                        BtnCapturarPesoTara.IsEnabled = salidaActual == null;
                        BtnCapturarPesoBruto.IsEnabled = salidaActual != null;
                        if (tipoMovimientoSeleccionado.TipoMovimientoID == (int)TipoMovimiento.ProductoSalidaVenta)
                        {
                            txtPrecioVenta.IsEnabled = salidaActual != null;

                            lblChofer.Content = string.Empty;
                            LblChoferReq.Content = string.Empty;
                            lblPlacas.Content = string.Empty;
                            LblPlacasReq.Content = string.Empty;
                            StpAyudaChofer.Children.Clear();
                            StpAyudaCamion.Children.Clear();
                            AgregarAyudaCliente();
                            return;
                        }
                        lblChofer.Content = Properties.Resources.SalidaVentaTraspaso_labelChofer;
                        LblChoferReq.Content = "*";
                        lblPlacas.Content = Properties.Resources.SalidaVentaTraspaso_labelPlacas;
                        LblPlacasReq.Content = "*";
                        lblCliente.Content = Properties.Resources.SalidaVentaTraspaso_labelDivision;
                        lblKilogramosVenta.Content = Properties.Resources.SalidaVentaTraspaso_labelKilogramosTraspaso;
                        lblCosto.Content = Properties.Resources.SalidaVentaTraspaso_labelCostoTraspaso;
                        AgregarAyudaDivision();
                        AgregarAyudaChofer();
                        AgregarAyudaPlacasCamion();

                        return;
                    }

                }
                AgregarAyudaCliente();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
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
                        TxtDisplayPeso.Text = peso;
                    }), null);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Evento click para capturar el peso 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCapturarPesoBruto_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (basculaConectada)
                {
                    txtPesoBruto.Text = TxtDisplayPeso.Text;
                }
                else
                {
                    txtPesoBruto.IsEnabled = true;
                    txtPesoBruto.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        /// <summary>
        /// Evento clic para calcular el peso tara
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCapturarPesoTara_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (basculaConectada)
                {
                    txtPesoTara.Text = TxtDisplayPeso.Text;
                }
                else
                {
                    txtPesoTara.IsEnabled = true;
                    txtPesoTara.Focus();
                }
                var fechaPl = new FechaPL();
                fechaActualServidor = fechaPl.ObtenerFechaActual();
                if (fechaActualServidor != null)
                {
                    txtFecha.Text = fechaActualServidor.FechaActual.ToString("dd'/'MM'/'yyyy");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

        }

        /// <summary>
        /// Evento al cerrar la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SalidaVentaTraspaso_OnUnloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (spManager != null)
                {
                    spManager.StopListening();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

        }

        private void BtnImprimirTicket_OnClick(object sender, RoutedEventArgs e)
        {
            ucCargando.Visibility = System.Windows.Visibility.Visible;
            BtnImprimirTicket.IsEnabled = false;

            
            Mouse.SetCursor(Cursors.Wait);
            Thread thImprimir = new Thread(ImprimirTicket);
            thImprimir.Start();
        }

        /// <summary>
        /// Evento clic del boton Cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SkMessageBox.Show(
                    Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    Properties.Resources.SalidaVentaTraspaso_Cancelar,
                    MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
                {

                    LimpiarCampos();
                    skAyudaFolioSalida.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        /// <summary>
        /// Evento click del boton guardar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGuardar_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (salidaActual != null)
                {
                    salidaActual.GeneraFactura = chkGeneraFactura.IsChecked == null ? false : chkGeneraFactura.IsChecked.Value;
                    var salidaProductoPl = new SalidaProductoPL();
                    salidaActual.UsuarioModificacionId = usuarioId;
                    if (salidaActual.Organizacion == null || salidaActual.Organizacion.OrganizacionID == 0)
                    {
                        salidaActual.Organizacion = new OrganizacionInfo
                        {
                            OrganizacionID =
                                AuxConfiguracion.ObtenerOrganizacionUsuario()
                        };
                    }
                    salidaActual.UsuarioCreacionId = AuxConfiguracion.ObtenerUsuarioLogueado();
                    MemoryStream resultado = salidaProductoPl.TerminarSalidaProducto(salidaActual);
                    if (resultado != null)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.CrearBoletaVigilancia_DatosGuardadosExito,
                        MessageBoxButton.OK, MessageImage.Correct);

                        var exportarPoliza = new ExportarPoliza();
                        exportarPoliza.ImprimirPoliza(resultado, string.Format("Poliza Salida Folio No. {0}", skAyudaFolioSalida.Clave));

                        LimpiarCampos();
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.SalidaVentaTraspaso_ErrorGuardar,
                        MessageBoxButton.OK, MessageImage.Stop);
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.SalidaVentaTraspaso_MsgErrorBascula, MessageBoxButton.OK, MessageImage.Stop);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  ex.Message, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }
        /// <summary>
        /// Evento para previnir introducir letras
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtCostoVenta_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumerosConPunto(e.Text);
        }
        /// <summary>
        /// Validar solo numero peso bruto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPesoBruto_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }
        /// <summary>
        /// Validar solo numero peso tara
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPesoTara_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }
        /// <summary>
        /// Validar pesos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPesos_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// Cambio de valor del peso bruto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPesoBruto_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPesoBruto.Text) && !string.IsNullOrEmpty(txtPesoTara.Text))
                {
                    var pesoNeto = int.Parse(txtPesoBruto.Text.Replace(",", "")) - int.Parse(txtPesoTara.Text.Replace(",", ""));
                    txtPesoNeto.Text = pesoNeto.ToString(CultureInfo.InvariantCulture);
                    txtKilogramosVenta.Text = pesoNeto.ToString("N0", CultureInfo.InvariantCulture);
                }
                else
                {
                    txtPesoNeto.Text = string.Empty;
                    txtKilogramosVenta.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        /// <summary>
        /// Valida 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtCostoVenta_OnLostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                var precioVenta = (TextBox)sender;
                decimal n = decimal.Parse(precioVenta.Text.Replace(",", ""), NumberStyles.AllowDecimalPoint,
                    CultureInfo.InvariantCulture);

                precioVenta.Text = n.ToString("N3", CultureInfo.InvariantCulture);

                if (salidaActual.TipoMovimiento.TipoMovimientoID == (int)TipoMovimiento.ProductoSalidaVenta)
                {
                    var precioCosto = txtCostoProducto.Text;

                    if (decimal.Parse(precioVenta.Text) < decimal.Parse(precioCosto))
                    {
                        bool generarSolicitud = true;

                        if (existeSolicitud)
                        {
                            // Consultar para obtener el estatus de la solicitud existente
                            solicitudInfo = new SolicitudAutorizacionInfo();
                            solicitudInfo.OrganizacionID = salidaActual.Organizacion.OrganizacionID;
                            solicitudInfo.FolioSalida = salidaActual.FolioSalida;

                            var solicitudPL = new SolicitudAutorizacionPL();
                            solicitudInfo = solicitudPL.ObtenerDatosSolicitudAutorizacion(solicitudInfo);

                            if (solicitudInfo.EstatusSolicitud == (int)Estatus.AMPPendien)
                            {
                                //Existe folio pendiente por autorizar
                                string mensaje = string.Format(Properties.Resources.SalidaVentaTraspaso_SolicitudPendiente, solicitudInfo.SolicitudID);
                                SkMessageBox.Show(mensaje, MessageBoxButton.OK, MessageImage.Stop);
                                generarSolicitud = false;
                            }
                            else
                            {
                                //Existe folio rechazado
                                var solicitudPrecioPL = new SolicitudAutorizacionPL();
                                int folioRechazado = solicitudPrecioPL.ConsultarPrecioRechazadoSolicitud(salidaActual.FolioSalida, decimal.Parse(precioVenta.Text), salidaActual.Organizacion.OrganizacionID);

                                if (folioRechazado > 0)
                                {
                                    string mensaje = string.Format(Properties.Resources.SalidaVentaTraspaso_SolicitudRechazada, precioVenta.Text, folioRechazado);
                                    SkMessageBox.Show(mensaje, MessageBoxButton.OK, MessageImage.Stop);
                                    generarSolicitud = false;
                                }
                            }
                        }

                        if (generarSolicitud)
                        {
                            solicitudInfo = new SolicitudAutorizacionInfo();

                            solicitudInfo.OrganizacionID = salidaActual.Organizacion.OrganizacionID;
                            solicitudInfo.FolioSalida = salidaActual.FolioSalida;
                            solicitudInfo.Precio = decimal.Parse(precioVenta.Text);
                            solicitudInfo.ProductoID = salidaActual.Producto.ProductoId;
                            solicitudInfo.AlmacenID = salidaActual.Almacen.AlmacenID;
                            solicitudInfo.UsuarioCreacionID = usuarioId;

                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                            Properties.Resources.SalidaVentaTraspaso_MsgSolicitud,
                                            MessageBoxButton.OK, MessageImage.Warning);

                            var solicitudAutorizacion = new SolicitarAutorizacionSalidaVenta(solicitudInfo);
                            MostrarCentrado(solicitudAutorizacion);

                            if (solicitudAutorizacion.solicitudGenerada)
                            {
                                existeSolicitud = true;
                            }
                        }
                        txtPrecioVenta.Clear();
                        txtPrecioVenta.Focus();
                    }
                    else
                    {
                        BtnCapturarPesoBruto.IsEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        public delegate void MyDelegate(bool visible);

        public void DelegateMethod(bool visible)
        {
            if (visible)
            {
                ucCargando.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                ucCargando.Visibility = System.Windows.Visibility.Hidden;
            }
            //myControl.Size = new Size(80, 25);
            //myControl.Text = myCaption;
            //this.Controls.Add(myControl);
        }


        //Thread initThread;
        
        
        #endregion
    }
}
