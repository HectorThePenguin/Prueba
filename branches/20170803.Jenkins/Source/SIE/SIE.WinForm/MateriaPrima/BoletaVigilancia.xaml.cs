using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Servicios.PL;
using SIE.Services.Info.Info;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using Application = System.Windows.Application;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using System.Collections.Generic;
using Convert = System.Convert;
using TextBox = System.Windows.UIElement;

namespace SIE.WinForm.MateriaPrima
{
    /// <summary>
    /// Lógica de interacción para BoletaVigilancia.xaml
    /// </summary>
    public partial class BoletaVigilancia
    {
        #region Atributos
        private int organizacionId;
        private ProductoInfo productoActual;
        private ProveedorInfo proveedor;
        private ProveedorInfo trasportista;
        private ProveedorChoferInfo chofer;
        private CamionInfo camion;
        private string fechaEntrada;
        private string horaEntrada;
        private Boolean esFolioExistente;
        #endregion

        # region Contructor
        /// <summary>
        /// Inicializa componentes y pide cargar controles de ayuda
        /// Mayo 20014
        /// </summary>
        /// 
        public BoletaVigilancia()
        {
            InitializeComponent();
            CargarAyudas();
            ckbSalida.IsEnabled = false;
            fechaEntrada = DateTime.Now.ToShortDateString();
            horaEntrada = DateTime.Now.ToShortTimeString();
            DtpFechaEntrada.Text = String.Format("{0} {1}", fechaEntrada, horaEntrada);
            DtpFechaSalida.Text = String.Format("{0} {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString());
            organizacionId = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
            TxtFolio.Text = string.Empty;
            TxtMarcaCamion.Text = string.Empty;
            TxtColorCamion.Text = string.Empty;
            esFolioExistente = false;
            ConfiguracionInfo configuracion = AuxConfiguracion.ObtenerConfiguracion();
            nombreImpresora = configuracion.ImpresoraRecepcionGanado;
            productoActual = null;
            txtTransportista = new System.Windows.Controls.TextBox() { Width = 100, MaxLength = 50 };
            txtChofer = new System.Windows.Controls.TextBox() { Width = 100, MaxLength = 50 };
            txtCamion = new System.Windows.Controls.TextBox() { Width = 100, MaxLength = 50 };
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Sirve para el control de Producto
        /// </summary>
        private SKAyuda<ProductoInfo> skAyudaVigilanciaProducto;
        private SKAyuda<ProveedorInfo> skAyudaVigilanciaProveedor;
        private SKAyuda<VigilanciaInfo> skAyudaVigilanciaTransportista;
        private TextBox txtTransportista;
        private SKAyuda<ChoferInfo> skAyudaChofer;
        private TextBox txtChofer;
        private SKAyuda<VigilanciaInfo> skAyudaCamion;
        private TextBox txtCamion;
        private string nombreImpresora;
        /// <summary>
        /// transportistaId obtiene ID del proveedor seleccionado en el control SplAyudaProveedor que usaran los controles Chofer y Camion
        /// </summary>
        private int transportistaId;

        /// <summary>
        /// Indica si el Producto es una Premezcla
        /// </summary>
        private bool esPremezcla;
        #endregion

        #region Metodos
        /// <summary>
        /// Método que carga todos los controles de ayuda
        /// </summary>
        private void CargarAyudas()
        {
            AgregarAyudaProducto();
            AgregarAyudaProveedor(0);
            AgregarAyudaTransportista();
            AgregarAyudaChofer(0);
            AgregarAyudaCamion(0);
        }
        /// <summary>
        /// Control ayuda Productro
        /// </summary>
        private void AgregarAyudaProducto()
        {
            skAyudaVigilanciaProducto = new SKAyuda<ProductoInfo>(190, false, new ProductoInfo { ProductoId = 0 }
                                                   , "PropiedadClaveBoletaVigilancia"
                                                   , "PropiedadDescripcionBoletaVigilancia",
                                                   "", false, 80, 9, true)
            {
                AyudaPL = new ProductoVigilanciaPL(),
                MensajeClaveInexistente = Properties.Resources.AyudaProductoVigilancia_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.AyudaProductoVigilancia_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.AyudaProductoVigilancia__Busqueda,
                MensajeAgregar = Properties.Resources.AyudaProductoVigilancia_Seleccionar,
                TituloEtiqueta = Properties.Resources.AyudaProductoVigilancia_LeyendaBusqueda,
                TituloPantalla = Properties.Resources.AyudaProductoVigilancia_Busqueda_Titulo
            };

            SplAyudaProducto.Children.Clear();
            SplAyudaProducto.Children.Add(skAyudaVigilanciaProducto);
            skAyudaVigilanciaProducto.ObtenerDatos += ObtenerDatosProducto;
            skAyudaVigilanciaProducto.AyudaLimpia += (sender, args) => LimpiarCamposProveedor();
        }
        /// <summary>
        /// Obtiene los datos obtenidos en la ayuda de productos
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosProducto(String clave)
        {
            var productoPL = new ProductoPL();
            skAyudaVigilanciaProducto.Info = productoPL.ObtenerPorID(skAyudaVigilanciaProducto.Info);
            productoActual = skAyudaVigilanciaProducto.Info;
            if (productoActual.EsPremezcla)
            {
                skAyudaVigilanciaProveedor.Info.ProductoID = 0;
                skAyudaVigilanciaProveedor.Info.OrganizacionID = 0;
            }
            else
            {
                int productoID;
                int.TryParse(clave, out productoID);
                skAyudaVigilanciaProveedor.Info.ProductoID = productoID;
                skAyudaVigilanciaProveedor.Info.OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            }
            LimpiarCamposProveedor();
        }
        /// <summary>
        /// Control ayuda Proveedor
        /// </summary>
        private void AgregarAyudaProveedor(int clave)
        {
            var vigilanciaFiltro = new VigilanciaInfo { ID = 0 };

            if (skAyudaVigilanciaProducto.Info != null)
            {
                if (!string.IsNullOrEmpty(skAyudaVigilanciaProducto.Clave))
                {
                    vigilanciaFiltro.Producto = new ProductoInfo
                                                    {
                                                        ProductoId = clave,
                                                        ProductoDescripcion = skAyudaVigilanciaProducto.Descripcion
                                                    };
                }
                else
                {
                    vigilanciaFiltro.Producto = new ProductoInfo
                                                    {
                                                        ProductoId = 0
                                                    };
                }
            }
            var proveedorInfo = new ProveedorInfo
                                    {
                                        Activo = EstatusEnum.Activo,
                                        OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario()
                                    };
            skAyudaVigilanciaProveedor = new SKAyuda<ProveedorInfo>(190, false, proveedorInfo
                                                                    , "PropiedadCodigoSAPBoletaVigilanciaProducto"
                                                                    , "PropiedadDescripcionBoletaVigilanciaProducto",
                                                                    "PropiedadOcultaBoletaVigilanciaProducto", true, 80, 10,
                                                                    true)
                                             {
                                                 AyudaPL = new ProveedorPL(),
                                                 MensajeClaveInexistente =
                                                     Properties.Resources.AyudaProveedorVigilancia_CodigoInvalido,
                                                 MensajeBusquedaCerrar =
                                                     Properties.Resources.AyudaProveedorVigilancia_SalirSinSeleccionar,
                                                 MensajeBusqueda =
                                                     Properties.Resources.AyudaProveedorVigilancia__Busqueda,
                                                 MensajeAgregar =
                                                     Properties.Resources.AyudaProveedorVigilancia_Seleccionar,
                                                 TituloEtiqueta =
                                                     Properties.Resources.AyudaProveedorVigilancia_LeyendaBusqueda,
                                                 TituloPantalla =
                                                     Properties.Resources.AyudaProveedorVigilancia_Busqueda_Titulo,
                                                 MensajeNoPuedeBuscar =
                                                     Properties.Resources.AyudaProveedorVigilancia_SeleccionarProducto
                                             };
            skAyudaVigilanciaProveedor.PuedeBuscar += () =>
                                                          {
                                                              int productoId;
                                                              int.TryParse(skAyudaVigilanciaProducto.Clave,
                                                                           out productoId);
                                                              return productoId > 0;
                                                          };
            skAyudaVigilanciaProveedor.ObtenerDatos += ObtenerDatosProveedor;
            skAyudaVigilanciaProveedor.LlamadaMetodosNoExistenDatos += LimpiarCamposProveedor;
            skAyudaVigilanciaProveedor.AyudaConDatos += (sender, args) =>
                                                            {
                                                                skAyudaVigilanciaProveedor.Info.OrganizacionID =
                                                                    AuxConfiguracion.ObtenerOrganizacionUsuario();
                                                                int productoId;
                                                                int.TryParse(skAyudaVigilanciaProducto.Clave,
                                                                             out productoId);
                                                                skAyudaVigilanciaProveedor.Info.ProductoID = productoId;
                                                            };
            SplAyudaProveedor.Children.Clear();
            SplAyudaProveedor.Children.Add(skAyudaVigilanciaProveedor);
        }

        private void LimpiarCamposProveedor()
        {
            skAyudaVigilanciaProveedor.LimpiarCampos();
            cmbContrato.ItemsSource = new List<ContratoInfo>();
        }
        /// <summary>
        /// Obtiene los datos del provedor obtenido en la ayuda
        /// </summary>
        /// <param name="valor"></param>
        private void ObtenerDatosProveedor(String valor)
        {
            try
            {
                if (skAyudaVigilanciaProducto.Clave != string.Empty)
                {
                    ProductoInfo producto = skAyudaVigilanciaProducto.Info;
                    var productoPl = new ProductoPL();
                    var productoContrato = productoPl.ObtieneRequiereContrato(producto);

                    if (producto.Familia.FamiliaID == FamiliasEnum.MateriaPrimas.GetHashCode() ||
                        productoContrato != null)
                    {
                        if (valor == string.Empty) return;
                        proveedor = skAyudaVigilanciaProveedor.Info;
                        if (proveedor == null) return;

                        if (proveedor.TipoProveedor.TipoProveedorID ==
                            TipoProveedorEnum.ProveedoresDeMateriaPrima.GetHashCode() ||proveedor.TipoProveedor.TipoProveedorID==TipoProveedorEnum.ProveedoresFletes.GetHashCode()
                        ) 
                        {
                            var contratoPl = new ContratoPL();
                            var listaContratos = contratoPl.ObtenerContratosPorProveedorId(proveedor.ProveedorID,
                                                                                           organizacionId);
                            if (listaContratos != null)
                            {
                                listaContratos = listaContratos.Where(
                                    registro =>
                                        registro.Estatus.EstatusId !=
                                        EstatusContratoEnum.Cancelado.GetHashCode() &&
                                        registro.Estatus.EstatusId != EstatusContratoEnum.Cerrado.GetHashCode())
                                    .ToList();

                                var listaContratosFiltrada = new List<ContratoInfo>
                                {
                                    new ContratoInfo()
                                    {
                                        ContratoId = 0,
                                        FolioCadena = "Seleccione"
                                    }
                                };
                                listaContratosFiltrada.AddRange(listaContratos.Where(
                                    registro =>
                                        registro.Producto.ProductoId ==
                                        Convert.ToInt32(skAyudaVigilanciaProducto.Clave))
                                    .ToList());
                                if (listaContratosFiltrada.Count > 1)
                                {
                                    cmbContrato.ItemsSource = listaContratosFiltrada;
                                    cmbContrato.DisplayMemberPath = "FolioCadena";
                                    cmbContrato.SelectedValuePath = "ContratoID";

                                    if (listaContratosFiltrada.Count == 2)
                                    {
                                        cmbContrato.SelectedIndex = 1;

                                        var contrato = listaContratosFiltrada[1];

                                        var entradaProductoPl = new EntradaProductoPL();
                                        List<EntradaProductoInfo> listaEntradaProductos =
                                            entradaProductoPl.ObtenerEntradaProductoPorContrato(contrato);

                                        if (listaEntradaProductos != null)
                                        {
                                            var pesoTotal = listaEntradaProductos.Sum(registro => (registro.PesoBruto - registro.PesoTara));
                                            decimal toleranciaCalculada = (contrato.Tolerancia *
                                                                           contrato.Cantidad) / 100;

                                            if (pesoTotal >
                                                (toleranciaCalculada + contrato.Cantidad))
                                            {
                                                cmbContrato.SelectedIndex = 0;
                                                cmbContrato.IsEnabled = true;
                                                return;
                                            }
                                        }
                                        cmbContrato.IsEnabled = false;
                                    }
                                    else
                                    {
                                        cmbContrato.SelectedIndex = 0;
                                        cmbContrato.IsEnabled = true;
                                    }
                                }
                                else
                                {
                                    cmbContrato.ItemsSource = new List<ContratoInfo>();
                                    cmbContrato.DisplayMemberPath = "Folio";
                                    cmbContrato.SelectedValuePath = "ContratoID";

                                    InicializarDatosProveedor();
                                    SkMessageBox.Show(
                                        Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.BoletaVigilancia_msgSinContratos,
                                        MessageBoxButton.OK,
                                        MessageImage.Stop);
                                }
                            }
                            else
                            {
                                cmbContrato.ItemsSource = new List<ContratoInfo>();
                                cmbContrato.DisplayMemberPath = "Folio";
                                cmbContrato.SelectedValuePath = "ContratoID";

                                InicializarDatosProveedor();
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.BoletaVigilancia_msgSinContratos, MessageBoxButton.OK,
                                    MessageImage.Stop);
                            }
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.AyudaProveedorTransportista_CodigoInvalido,
                                MessageBoxButton.OK,
                                MessageImage.Stop);
                            InicializarDatosProveedor();
                        }
                    }
                    else
                    {
                        proveedor = skAyudaVigilanciaProveedor.Info;
                        cmbContrato.IsEnabled = false;
                        AgregarAyudaTransportista();
                        AgregarAyudaChofer(0);
                        AgregarAyudaCamion(0);
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.AyudaProductoVigilancia_Seleccionar, MessageBoxButton.OK,
                        MessageImage.Stop);
                    InicializarDatosProveedor();
                    skAyudaVigilanciaProducto.AsignarFoco();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

        }

        private void InicializarDatosProveedor()
        {
            skAyudaVigilanciaProveedor.LimpiarCampos();
            int productoID;
            int.TryParse(skAyudaVigilanciaProducto.Clave, out productoID);
            int organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            if (productoID > 0)
            {
                ProductoInfo producto = skAyudaVigilanciaProducto.Info;
                if (producto.EsPremezcla)
                {
                    organizacionID = 0;
                    productoID = 0;
                }
            }

            skAyudaVigilanciaProveedor.Info = new ProveedorInfo
            {
                Activo = EstatusEnum.Activo,
                ProductoID = productoID,
                OrganizacionID = organizacionID
            };
        }

        /// <summary>
        /// Control ayuda transportista
        /// </summary>
        private void AgregarAyudaTransportista()
        {
            int contrato = 0;
            if (cmbContrato.SelectedIndex > 0)
            {
                contrato = ((ContratoInfo)cmbContrato.SelectedItem).ContratoId;
            }
            skAyudaVigilanciaTransportista = new SKAyuda<VigilanciaInfo>(190, false, new VigilanciaInfo { ID = 0, Contrato = new ContratoInfo() { ContratoId = contrato } }
                                                   , "PropiedadCodigoSAPBoletaVigilanciaTransportista"
                                                   , "PropiedadDescripcionBoletaVigilanciaTransportista", "", false, 80, 10, true)
            {
                AyudaPL = new TransportistaVigilanciaPL(),
                MensajeClaveInexistente = Properties.Resources.AyudaProveedorTransportista_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.AyudaProveedorTransportista_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.AyudaProveedorTransportista_Busqueda,
                MensajeAgregar = Properties.Resources.AyudaProveedorTransportista_Seleccionar,
                TituloEtiqueta = Properties.Resources.AyudaProveedorTransportista_LeyendaBusqueda,
                TituloPantalla = Properties.Resources.AyudaProveedorTransportista_Busqueda_Titulo,
                MaxLengthCampoID = 11
            };
            skAyudaVigilanciaTransportista.ObtenerDatos += ObtenerDatosTransportista;
            SplAyudaTransportista.Children.Clear();
            SplAyudaTransportista.Children.Add(skAyudaVigilanciaTransportista);
        }
        /// <summary>
        /// Obtiene los datos del trasportista que se obtuvo en la ayuda
        /// </summary>
        /// <param name="valor"></param>
        private void ObtenerDatosTransportista(String valor)
        {
            try
            {
                skAyudaChofer.LimpiarCampos();
                skAyudaCamion.LimpiarCampos();
                trasportista = new ProveedorInfo { CodigoSAP = valor };

                var proveedorPl = new ProveedorPL();

                trasportista = proveedorPl.ObtenerPorCodigoSAP(trasportista);

                if (skAyudaChofer.Info == null)
                {
                    var proveedorFlete = new ChoferInfo
                    {
                        ChoferID = 0,
                        ProveedorChoferID = trasportista.ProveedorID
                    };
                    skAyudaChofer.Info = proveedorFlete;

                }
                else
                {
                    skAyudaChofer.Info.ProveedorChoferID = trasportista.ProveedorID;
                }

                if (skAyudaCamion.Info == null)
                {
                    var filtro = new VigilanciaInfo
                    {
                        ID = 0,
                        Camion = new CamionInfo { CamionID = 0 },
                        Transportista = new ProveedorInfo { ProveedorID = trasportista.ProveedorID }
                    };
                    skAyudaCamion.Info = filtro;
                }
                else
                {
                    skAyudaCamion.Info.Camion = new CamionInfo { CamionID = 0 };
                    skAyudaCamion.Info.Transportista = new ProveedorInfo { ProveedorID = trasportista.ProveedorID };
                }
                skAyudaVigilanciaTransportista.Info.Contrato = (ContratoInfo)cmbContrato.SelectedItem;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

        }
        /// <summary>
        /// Control ayuda chofer
        /// </summary>
        private void AgregarAyudaChofer(int proveedorFletes)
        {

            var proveedorFlete = new ChoferInfo
            {
                ChoferID = 0,
                ProveedorChoferID = proveedorFletes
            };
            skAyudaChofer = new SKAyuda<ChoferInfo>(190, false, proveedorFlete
                                                   , "PropiedadClaveEntradaGanado"
                                                    , "PropiedadDescripcionEntradaGanado", true)
            {
                AyudaPL = new ChoferVigilanciaPL(),
                MensajeClaveInexistente = Properties.Resources.AyudaProveedorChofer_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.AyudaProveedorChofer_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.AyudaProveedorChofer_Busqueda,
                MensajeAgregar = Properties.Resources.AyudaProveedorChofer_Seleccionar,
                TituloEtiqueta = Properties.Resources.AyudaProveedorChofer_LeyendaBusqueda,
                TituloPantalla = Properties.Resources.AyudaProveedorChofer_Busqueda_Titulo,
            };
            skAyudaChofer.ObtenerDatos += ObtenerDatosChofer;
            SplAyudaChofer.Children.Clear();
            SplAyudaChofer.Children.Add(skAyudaChofer);
        }
        /// <summary>
        /// Obtiene los datos del chofer obtenido en la ayuda de chofer
        /// </summary>
        /// <param name="valor"></param>
        private void ObtenerDatosChofer(String valor)
        {
            try
            {
                skAyudaChofer.Info.ProveedorChoferID = trasportista.ProveedorID;
                var choferPl = new ProveedorChoferPL();
                chofer = choferPl.ObtenerProveedorChoferPorChoferID(trasportista.ProveedorID, int.Parse(valor));
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

        }

        /// <summary>
        /// Control ayuda camion
        /// </summary>
        private void AgregarAyudaCamion(int transportistaId)
        {
            
                 var filtro = new VigilanciaInfo
                {
                    ID = 0,
                    Camion = new CamionInfo { CamionID = 0 },
                    Transportista = new ProveedorInfo
                    {
                        ProveedorID = transportistaId
                    }
                };
            
            skAyudaCamion = new SKAyuda<VigilanciaInfo>(190, false, filtro
                                                   , "PropiedadClaveBoletaVigilanciaPlacas"
                                                   , "PropiedadDescripcionBoletaVigilanciaCamion", true)
            {
                AyudaPL = new PlacasVigilanciaPL(),
                MensajeClaveInexistente = Properties.Resources.AyudaProveedorPlacasCamion_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.AyudaProveedorPlacasCamion_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.AyudaProveedorPlacasCamion_Busqueda,
                MensajeAgregar = Properties.Resources.AyudaProveedorPlacasCamion_Seleccionar,
                TituloEtiqueta = Properties.Resources.AyudaProveedorPlacasCamion_LeyendaBusqueda,
                TituloPantalla = Properties.Resources.AyudaProveedorPlacasCamion_Busqueda_Titulo,

            };
            skAyudaCamion.ObtenerDatos += ObtenerDatosCamion;
            SplAyudaCamion.Children.Clear();
            SplAyudaCamion.Children.Add(skAyudaCamion);
        }
        /// <summary>
        /// Obtiene los datos del camion obtenido en la ayuda
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosCamion(String clave)
        {
            try
            {
                var camionTemp = new CamionInfo()
                {
                    PlacaCamion = skAyudaCamion.Descripcion
                };
                ResultadoValidacion resultadoValidarCamionSinSalida = ValidarPlacasCamion(camionTemp);
                if (resultadoValidarCamionSinSalida.Resultado)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    resultadoValidarCamionSinSalida.Mensaje,
                    MessageBoxButton.OK, MessageImage.Stop);
                    skAyudaCamion.LimpiarCampos();
                    if (trasportista != null)
                        AgregarAyudaCamion(trasportista.ProveedorID);
                    else
                        AgregarAyudaCamion(0);
                }
                else
                {
                    var camionPl = new CamionPL();
                    skAyudaCamion.Info.Transportista = trasportista;
                    camion = camionPl.ObtenerPorID(int.Parse(clave));
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        /// <summary>
        /// Limpia todos los campos y devuelve foco a campo Folio
        /// </summary>
        private void LimpiarCampos()
        {

            fechaEntrada = DateTime.Now.ToShortDateString();
            horaEntrada = DateTime.Now.ToShortTimeString();
            DtpFechaEntrada.Text = String.Format("{0} {1}", fechaEntrada, horaEntrada);
            DtpFechaSalida.Text = String.Format("{0} {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString());
            TxtFolio.Text = string.Empty;
            TxtColorCamion.Text = string.Empty;
            TxtMarcaCamion.Text = string.Empty;
            AgregarAyudaProducto();
            AgregarAyudaProveedor(0);
            AgregarAyudaChofer(0);
            AgregarAyudaCamion(0);
            AgregarAyudaTransportista();
            TxtFolio.Focus();
            productoActual = null;
            cmbContrato.ItemsSource = new List<ContratoInfo>();
        }
        /// <summary>
        ///Habilita todos los campos menos el checkbox salida
        /// </summary>
        private void HabilitarCampos()
        {
            TxtFolio.IsEnabled = true;
            SplAyudaProducto.IsEnabled = true;
            SplAyudaProveedor.IsEnabled = true;
            SplAyudaTransportista.IsEnabled = true;
            SplAyudaChofer.IsEnabled = true;
            SplAyudaCamion.IsEnabled = true;
            TxtMarcaCamion.IsEnabled = true;
            TxtColorCamion.IsEnabled = true;
            imgImprimir.IsEnabled = true;

        }
        /// <summary>
        /// Una vez que se hace una consulta de folio y se traen datos, se deshabilita campo para evitar modificarlos y solo se habilita chechbox salida
        /// además, se actualiza hora de salida en pantalla
        /// </summary>
        private void DeshabilitarCampos()
        {
            //Deshabiltar campos
            TxtFolio.IsEnabled = false;
            SplAyudaProducto.IsEnabled = false;
            SplAyudaProveedor.IsEnabled = false;
            SplAyudaTransportista.IsEnabled = false;
            SplAyudaChofer.IsEnabled = false;
            SplAyudaCamion.IsEnabled = false;
            TxtMarcaCamion.IsEnabled = false;
            TxtColorCamion.IsEnabled = false;
            //Se actualiza hora de salida en pantalla
            DtpFechaSalida.Text = String.Format("{0} {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString());
            //Habilita checkbox salida
            ckbSalida.IsEnabled = true;
            imgImprimir.IsEnabled = false;
        }
        /// <summary>
        /// Sistema actualiza el campo “FechaSalida ” y el campo “Activo ” = 0 de la tabla “RegistroVigilancia ”
        /// </summary>
        private void RegistrarSalida()
        {
            var registrarSalidaInfo = new RegistroVigilanciaInfo()
            {

                Organizacion = new OrganizacionInfo() { OrganizacionID = Convert.ToInt32(organizacionId) },
                FolioTurno = Convert.ToInt32(TxtFolio.Text),
                Activo = EstatusEnum.Inactivo,
                UsuarioModificacionID = Convert.ToInt32(Application.Current.Properties["UsuarioID"])
            };

            var registroSalidaVigilanciaPl = new RegistroSalidaVigilanciaPL();
            registroSalidaVigilanciaPl.RegistroSalida(registrarSalidaInfo);
        }
        /// <summary>
        /// Guarda los datos ingresados en los campos en la tabla "RegistroVigilancia"
        /// </summary>
        /// <returns></returns>
        private bool Guardar()
        {
            try
            {

                ResultadoValidacion validar = ValidarCamposVacios();
                if (validar.Resultado)
                {
                    var productoPl = new ProductoPL();
                    var producto = productoPl.ObtenerPorID(new ProductoInfo() { ProductoId = Convert.ToInt32(skAyudaVigilanciaProducto.Clave) });
                    var contrato = new ContratoInfo();
                    if (producto.SubFamilia.SubFamiliaID != SubFamiliasEnum.MicroIngredientes.GetHashCode())
                    {
                        contrato = (ContratoInfo)cmbContrato.SelectedItem;

                        if (contrato.TipoFlete.TipoFleteId == (int)TipoFleteEnum.LibreAbordo)
                        {
                            chofer = new ProveedorChoferInfo()
                            {
                                Proveedor = new ProveedorInfo(),
                                Chofer = new ChoferInfo()
                            };

                            var txtTransportistaLocal = (System.Windows.Controls.TextBox)txtTransportista;
                            chofer.Proveedor.Descripcion = txtTransportistaLocal.Text;

                            var txtChoferLocal = (System.Windows.Controls.TextBox)txtChofer;
                            chofer.Chofer.NombreCompleto = txtChoferLocal.Text;

                            var txtCamionLocal = (System.Windows.Controls.TextBox)txtCamion;
                            camion = new CamionInfo() { PlacaCamion = txtCamionLocal.Text };
                        }
                        else
                        {
                            chofer.Proveedor.Descripcion = skAyudaVigilanciaTransportista.Descripcion;

                            chofer.Chofer.NombreCompleto = skAyudaChofer.Descripcion;

                            camion.PlacaCamion = skAyudaCamion.Descripcion;
                        }
                    }
                    else
                    {
                        esPremezcla = true;
                        chofer.Proveedor.Descripcion = skAyudaVigilanciaTransportista.Descripcion;

                        chofer.Chofer.NombreCompleto = skAyudaChofer.Descripcion;

                        camion.PlacaCamion = skAyudaCamion.Descripcion;
                    }
                    ResultadoValidacion resultadoCamionSinSalida = ValidarPlacasCamion(camion);
                    if (resultadoCamionSinSalida.Resultado)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        resultadoCamionSinSalida.Mensaje,
                        MessageBoxButton.OK, MessageImage.Stop);
                        txtCamion.Focus();
                        return false;
                    }
                    else
                    {
                        var vigilanciaGuardarInfo = new RegistroVigilanciaInfo
                        {
                            Producto = productoActual,
                            Contrato = contrato,
                            ProveedorMateriasPrimas = proveedor,
                            Transportista = chofer.Proveedor.Descripcion,
                            Chofer = chofer.Chofer.NombreCompleto,
                            ProveedorChofer = chofer,
                            Camion = camion,
                            CamionCadena = camion.PlacaCamion,
                            Marca = TxtMarcaCamion.Text,
                            Color = TxtColorCamion.Text,
                            Activo = EstatusEnum.Activo,
                            Organizacion = new OrganizacionInfo { OrganizacionID = Convert.ToInt32(organizacionId) },
                            UsuarioCreacionID = Convert.ToInt32(Application.Current.Properties["UsuarioID"]),
                            TipoFolioID = (int)TipoFolio.Vigilancia
                        };

                        var crearvigilanciaPl = new CrearVigilanciaPL();
                        int numeroFolio = crearvigilanciaPl.Guardar(vigilanciaGuardarInfo);

                        //Aviso que se guardaron los datos exitosamente e indica cual es el número de folio

                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            String.Format("{0} {1} {2}", Properties.Resources.AyudaProveedorPlacasCamion_Folio, numeroFolio,
                                Properties.Resources.CrearBoletaVigilancia_DatosGuardadosExito),
                            MessageBoxButton.OK, MessageImage.Correct);

                        //Guarda el úumero de folio en en campo folio, ya que en caso de que ocupe imprimirse de ahí se tomara el valor de número de folio
                        TxtFolio.Text = Convert.ToString(numeroFolio);

                        return true;
                    }
                }
                else
                {

                    var mensaje = string.IsNullOrEmpty(validar.Mensaje)
                        ? Properties.Resources.DatosBlancos_BoletaVigilancia
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
        /// Sirve para registrar la fecha y hora de salidad
        /// </summary>
        private void RegistrarFecha()
        {
            //Sistema actualiza el campo “FechaSalida ” y el campo “Activo ” = 0 de la tabla “BoletaVigilancia ”
            RegistrarSalida();
            //Avisa se guardaron los datos exitosamente
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.CrearBoletaVigilancia_SalidaRegistroExito,
                MessageBoxButton.OK, MessageImage.Correct);
            //Antes de regresar, quita paloma del checkbox y lo deshabilita
            ckbSalida.IsChecked = false;
            ckbSalida.IsEnabled = false;
            esFolioExistente = false;
            //Limpia todos los datos de la pantalla y regresa el foco al campo folio
            LimpiarCampos();
            //Habilita controles para nueva ayuda
            HabilitarCampos();
        }
        /// <summary>
        /// Sirve para Imprimir
        /// </summary>
        private void Imprimir()
        {
            try
            {
                ResultadoValidacion validar = ValidarCamposVacios();
                if (validar.Resultado)
                {
                    //Código para imprimir 
                    var print = new PrintDocument();
                    print.PrintPage += print_Page;
                    print.PrinterSettings.PrinterName = nombreImpresora;
                    print.PrintController = new StandardPrintController();
                    print.Print();
                }
                else
                {

                    var mensaje = string.IsNullOrEmpty(validar.Mensaje)
                        ? Properties.Resources.DatosBlancos_BoletaVigilancia
                        : validar.Mensaje;
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        mensaje, MessageBoxButton.OK, MessageImage.Stop);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        String.Format("{0} {1}", Properties.Resources.BoletaVigilancia_ErrorImpresion, nombreImpresora),
                        MessageBoxButton.OK, MessageImage.Error);
            }
        }
        //---------------------Agregado por Vega
        /// <summary>
        ///  Valida que el las placas del camion ingresado no esté activo y tenga fecha de salida
        /// </summary>
        private ResultadoValidacion ValidarPlacasCamion(CamionInfo camion)
        {
            var resultado = new ResultadoValidacion();
            var registroVigilancia = new RegistroVigilanciaPL();

            bool camionSinSalida = false;
            camionSinSalida = registroVigilancia.ObtenerDisponibilidadCamion(camion);

            resultado.Mensaje = Properties.Resources.BoletaVigilancia_CamionSinSalida;
            resultado.Resultado = camionSinSalida;

            return resultado;
        }
        //---------------------Fin- Agregado por vega
        /// <summary>
        /// Valida que los campos obligatorios no estén vacios
        /// </summary>
        /// <returns></returns>
        private ResultadoValidacion ValidarCamposVacios()
        {
            var resultado = new ResultadoValidacion();

            if (String.IsNullOrEmpty(skAyudaVigilanciaProducto.Clave.Trim()) && String.IsNullOrEmpty(skAyudaVigilanciaProducto.Descripcion.Trim()))
            {
                skAyudaVigilanciaProducto.AsignarFoco();
                resultado.Mensaje = Properties.Resources.BoletaVigilancia_ProductoRequerido;
                resultado.Resultado = false;
                return resultado;
            }
            if (String.IsNullOrEmpty(skAyudaVigilanciaProveedor.Clave.Trim()) && String.IsNullOrEmpty(skAyudaVigilanciaProveedor.Descripcion.Trim()))
            {
                skAyudaVigilanciaProveedor.AsignarFoco();
                resultado.Mensaje = Properties.Resources.BoletaVigilancia_ProveedorRequerido;
                resultado.Resultado = false;
                return resultado;
            }
            var productoPl = new ProductoPL();
            var producto = productoPl.ObtenerPorID(new ProductoInfo() { ProductoId = Convert.ToInt32(skAyudaVigilanciaProducto.Clave) });
            var productoContrato = productoPl.ObtieneRequiereContrato(producto);

            if (producto.Familia.FamiliaID == FamiliasEnum.MateriaPrimas.GetHashCode() ||
                productoContrato != null)
            {
                if (cmbContrato.SelectedIndex <= 0 && TxtFolio.Text == "")
                {
                    cmbContrato.Focus();
                    resultado.Mensaje = Properties.Resources.BoletaVigilancia_ContratoRequerido;
                    resultado.Resultado = false;
                    return resultado;
                }
                var contrato = (ContratoInfo)cmbContrato.SelectedItem;
                if (String.IsNullOrEmpty(skAyudaVigilanciaTransportista.Clave.Trim()) &&
                    String.IsNullOrEmpty(skAyudaVigilanciaTransportista.Descripcion.Trim()))
                {
                    if (contrato.TipoFlete.TipoFleteId == (int)TipoFleteEnum.LibreAbordo)
                    {
                        var texto = (System.Windows.Controls.TextBox)txtTransportista;
                        if (string.IsNullOrEmpty(texto.Text))
                        {
                            txtTransportista.Focus();
                            resultado.Mensaje = Properties.Resources.BoletaVigilancia_TransportistaRequerido;
                            resultado.Resultado = false;
                            return resultado;
                        }
                    }
                    else
                    {
                        skAyudaVigilanciaTransportista.AsignarFoco();
                        resultado.Mensaje = Properties.Resources.BoletaVigilancia_TransportistaRequerido;
                        resultado.Resultado = false;
                        return resultado;
                    }
                }
                if (String.IsNullOrEmpty(skAyudaChofer.Clave.Trim()) &&
                    String.IsNullOrEmpty(skAyudaChofer.Descripcion.Trim()))
                {
                    if (contrato.TipoFlete.TipoFleteId == (int)TipoFleteEnum.LibreAbordo)
                    {
                        var texto = (System.Windows.Controls.TextBox)txtChofer;
                        if (string.IsNullOrEmpty(texto.Text))
                        {
                            txtChofer.Focus();
                            resultado.Mensaje = Properties.Resources.BoletaVigilancia_ChoferRequerido;
                            resultado.Resultado = false;
                            return resultado;
                        }
                    }
                    else
                    {
                        skAyudaChofer.AsignarFoco();
                        resultado.Mensaje = Properties.Resources.BoletaVigilancia_ChoferRequerido;
                        resultado.Resultado = false;
                        return resultado;
                    }
                }
                if (String.IsNullOrEmpty(skAyudaCamion.Clave.Trim()) &&
                    String.IsNullOrEmpty(skAyudaCamion.Descripcion.Trim()))
                {
                    if (contrato.TipoFlete.TipoFleteId == (int)TipoFleteEnum.LibreAbordo)
                    {
                        var texto = (System.Windows.Controls.TextBox)txtCamion;
                        if (string.IsNullOrEmpty(texto.Text))
                        {
                            txtCamion.Focus();
                            resultado.Mensaje = Properties.Resources.BoletaVigilancia_CamionRequerido;
                            resultado.Resultado = false;
                            return resultado;
                        }
                    }
                    else
                    {
                        skAyudaCamion.AsignarFoco();
                        resultado.Mensaje = Properties.Resources.BoletaVigilancia_CamionRequerido;
                        resultado.Resultado = false;
                        return resultado;
                    }
                }
            }
            else
            {
                if (String.IsNullOrEmpty(skAyudaVigilanciaTransportista.Clave.Trim()) &&
                    String.IsNullOrEmpty(skAyudaVigilanciaTransportista.Descripcion.Trim()))
                {
                    txtTransportista.Focus();
                    resultado.Mensaje = Properties.Resources.BoletaVigilancia_TransportistaRequerido;
                    resultado.Resultado = false;
                    return resultado;
                }

                if (String.IsNullOrEmpty(skAyudaChofer.Clave.Trim()) &&
                    String.IsNullOrEmpty(skAyudaChofer.Descripcion.Trim()))
                {
                    txtChofer.Focus();
                    resultado.Mensaje = Properties.Resources.BoletaVigilancia_ChoferRequerido;
                    resultado.Resultado = false;
                    return resultado;
                }

                if (String.IsNullOrEmpty(skAyudaCamion.Clave.Trim()) &&
                    String.IsNullOrEmpty(skAyudaCamion.Descripcion.Trim()))
                {
                    txtCamion.Focus();
                    resultado.Mensaje = Properties.Resources.BoletaVigilancia_CamionRequerido;
                    resultado.Resultado = false;
                    return resultado;
                }
            }
            if (String.IsNullOrEmpty(TxtMarcaCamion.Text.Trim()))
            {
                TxtMarcaCamion.Focus();
                resultado.Mensaje = Properties.Resources.BoletaVigilancia_MarcaCamionRequerido;
                resultado.Resultado = false;
                return resultado;
            }
            if (String.IsNullOrEmpty(TxtColorCamion.Text.Trim()))
            {
                TxtColorCamion.Focus();
                resultado.Mensaje = Properties.Resources.BoletaVigilancia_ColorCamionRequerido;
                resultado.Resultado = false;
                return resultado;
            }
            resultado.Resultado = true;
            return resultado;
        }

        /// <summary>
        /// Obtiene la direccion de la organizacion
        /// </summary>
        /// <param name="e"></param>
        /// <param name="direccion"></param>
        private void ObtenerDireccionOrganizacion(PrintPageEventArgs e, string direccion)
        {
            const int limiteRenglon = 160;
            if (direccion.Length > limiteRenglon)
            {
                e.Graphics.DrawString(direccion.Substring(0, limiteRenglon), new Font("Arial", 10), Brushes.Black, 15, 40);
                e.Graphics.DrawString(direccion.Substring(limiteRenglon, direccion.Length - limiteRenglon), new Font("Arial", 10), Brushes.Black, 15, 48);
            }
            else
            {
                e.Graphics.DrawString(direccion, new Font("Arial", 10), Brushes.Black, 15, 40);
            }
            e.Graphics.DrawString("", new Font("Arial", 10), Brushes.Black, 15, 40);
        }

        #endregion

        #region Eventos
        /// <summary>
        /// Loaded de "BoletaVigilancia"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoletaVigilancia_OnLoaded(object sender, RoutedEventArgs e)
        {
            TxtFolio.Focus();
        }
        /// <summary>
        /// si checkbox esta deshabilitado es nuevo ingreso guarda, genera folio e imprime
        /// si checkbox es habilitado, ya está guardado, así que solo imprime
        /// </summary>
        private void ImgImprimir_OnMouseLeftButtonDown_(object sender, MouseButtonEventArgs e)
        {
            if (!esFolioExistente)
            {
                bool exito = Guardar();
                if (exito)
                {
                    Imprimir();
                    //Limpia todos los datos de la pantalla y regresa el foco al campo folio
                    LimpiarCampos();
                }
            }
            else
            {
                Imprimir();
            }
        }
        /// <summary>
        /// Lógica del proceso para Guardar en la BD en la tabla "RegistroVigilancia" los datos proporcionados por el usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardar_Click(object sender, EventArgs e)
        {

            if (ckbSalida.IsEnabled)
            {
                if (ckbSalida.IsChecked == true)
                {
                    var registroVigilanciaPl = new RegistroVigilanciaPL();
                    var registroVigilancia =
                        registroVigilanciaPl.ObtenerRegistroVigilanciaPorFolioTurno(new RegistroVigilanciaInfo()
                        {
                            FolioTurno = TxtFolio.Text.Trim() != string.Empty? Convert.ToInt32(TxtFolio.Text): 0,
                            Organizacion = new OrganizacionInfo() { OrganizacionID = organizacionId }
                        });
                    
                    var entradaProductoPl = new EntradaProductoPL();
                    var entradaProducto =
                        entradaProductoPl.ObtenerEntradaProductoPorRegistroVigilanciaID(registroVigilancia.RegistroVigilanciaId,
                            organizacionId);

                    if (entradaProducto != null)
                    {
                        if (entradaProducto.PesoBruto <= 0 && entradaProducto.Producto.Familia.FamiliaID==FamiliasEnum.Premezclas.GetHashCode()) 
                        {
                            RegistrarFecha();
                        }
                        else if (entradaProducto.Producto.Familia.FamiliaID != FamiliasEnum.Premezclas.GetHashCode())
                        {
                            if (entradaProducto.Estatus != null && entradaProducto.Estatus.EstatusId == Estatus.Rechazado.GetHashCode())
                            {
                                RegistrarFecha();
                            }
                            else 
                            {
                                if (entradaProducto.PesoTara > 0)
                                {
                                    RegistrarFecha();
                                }
                                else
                                {
                                    //No permite guardar si peso tara es 0
                                    TxtFolio.Focus();
                                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.BoletaVigilancia_msgPesajePendiente, MessageBoxButton.OK,
                                        MessageImage.Stop);
                                   
                                }
                            }
                        }
                        else
                        {
                            if (entradaProducto.PesoTara > 0)
                            {
                                RegistrarFecha();
                            }
                            else
                            {
                                //No permite guardar si peso tara es 0
                                TxtFolio.Focus();
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources.BoletaVigilancia_msgPesajePendiente, MessageBoxButton.OK,
                                    MessageImage.Stop);
                               
                            }
                        }
                    }
                    else
                    {
                        RegistrarFecha();
                    }
                }
                else
                {
                     SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.CrearBoletaVigilancia_SalidanoConfirmada, MessageBoxButton.OK,
                       MessageImage.Stop);
                }
            }
            else
            {
                bool exito = Guardar();
                //limpia todos los datos de la pantalla y regresa el foco al campo folio
                if (exito)
                {
                    LimpiarCampos();
                }
            }
        }

        /// <summary>
        /// Guarda los datos mostrados en pantalla en la tabla RegistroVigilancia (previa verificación)
        /// </summary>
        private void print_Page(object sender, PrintPageEventArgs e)
        {
            const int tamanoFuenteTitulo = 14, tamanoFuenteTexto = 11;
            const int sinMargen = 10;
            int separacionPremezcla = 0;
            var organizacionInfo = new OrganizacionInfo();
            var organizacionPl = new OrganizacionPL();
            organizacionInfo = organizacionPl.ObtenerPorID(organizacionId);


            var vigilanciaConsultarInfo = new RegistroVigilanciaInfo
            {
                Organizacion = new OrganizacionInfo { OrganizacionID = Convert.ToInt32(organizacionId) },
                FolioTurno = Convert.ToInt32(TxtFolio.Text)
            };
            var crearVigilancia = new RegistroVigilanciaPL();
            RegistroVigilanciaInfo tablavigilancia;
            tablavigilancia = crearVigilancia.ObtenerRegistroVigilanciaPorFolioTurno(vigilanciaConsultarInfo);

            if (esPremezcla)
            {
                var entradaProductoPL = new EntradaProductoPL();
                var entradaProducto =
                    entradaProductoPL.ObtenerEntradaProductoPorFolio(vigilanciaConsultarInfo.FolioTurno, organizacionId);

                if (entradaProducto != null)
                {
                    vigilanciaConsultarInfo.RegistroVigilanciaId =
                        entradaProducto.RegistroVigilancia.RegistroVigilanciaId;
                    tablavigilancia = crearVigilancia.ObtenerRegistroVigilanciaPorId(vigilanciaConsultarInfo);
                }
            }
            if (tablavigilancia == null)
            {
                return;
            }

            //Organizacion 
            e.Graphics.DrawString(organizacionInfo.Descripcion.ToUpper(),
                new Font("Arial", tamanoFuenteTitulo), Brushes.Black, sinMargen, 10);
            //Direccion de la organizacion
            ObtenerDireccionOrganizacion(e, organizacionInfo.Direccion.ToUpper());

            e.Graphics.DrawString("Fecha: ".ToUpper() + fechaEntrada.ToUpper(), new Font("Arial", tamanoFuenteTexto),
                Brushes.Black, sinMargen, 83);
            e.Graphics.DrawString("Hora: ".ToUpper() + horaEntrada.ToUpper(), new Font("Arial", tamanoFuenteTexto),
                Brushes.Black, sinMargen + 150, 83);
            if (tablavigilancia.Producto.Familia.FamiliaID == FamiliasEnum.Premezclas.GetHashCode()) 
            {
                separacionPremezcla = 230;
                e.Graphics.DrawString("Folio:          ".ToUpper() + tablavigilancia.FolioTurno, new Font("Arial", tamanoFuenteTitulo),
                Brushes.Black, sinMargen , 110);
            }
            e.Graphics.DrawString("Turno:         ".ToUpper() + TxtFolio.Text.ToUpper(), new Font("Arial", tamanoFuenteTitulo),
                Brushes.Black, sinMargen + separacionPremezcla  , 110);
            //Obtiene la descripción del producto de la tabla "Producto"  a partir de ProductoID
            var productoInfo = new ProductoInfo { ProductoId = tablavigilancia.Producto.ProductoId };
            var productoPl = new ProductoPL();
            var datosProducto = productoPl.ObtenerPorID(productoInfo);
            e.Graphics.DrawString(
                Properties.Resources.FolioImpresoVigilancia_Producto.ToUpper() + Convert.ToChar(9) +
                datosProducto.ProductoDescripcion.ToUpper(), new Font("Arial", tamanoFuenteTexto), Brushes.Black, sinMargen, 143);
            //Obtiene la descripción del proveddor a partir de proveedorID
            var proveedorInfo = new ProveedorInfo { ProveedorID = tablavigilancia.ProveedorMateriasPrimas.ProveedorID };
            var proveedorPl = new ProveedorPL();
            var datosProveedor = proveedorPl.ObtenerPorID(proveedorInfo);
            e.Graphics.DrawString(
                Properties.Resources.FolioImpresoVigilancia_Proveedor.ToUpper() + Convert.ToChar(9) + datosProveedor.Descripcion.ToUpper(),
                new Font("Arial", tamanoFuenteTexto), Brushes.Black, sinMargen, 173);
            //Obtiene nombre de Chofer de la tabla "chofer" a partir del ID del chofer
            string nombreChofer;
            if (skAyudaChofer != null && string.IsNullOrWhiteSpace(skAyudaChofer.Descripcion))
            {
                var txtChoferLocal = (System.Windows.Controls.TextBox)txtChofer;
                nombreChofer = txtChoferLocal.Text;
            }
            else
            {
                nombreChofer = skAyudaChofer.Descripcion;
            }
            e.Graphics.DrawString(
                Properties.Resources.FolioImpresoVigilancia_Chofer.ToUpper() + Convert.ToChar(9) +
                nombreChofer.ToUpper(), new Font("Arial", tamanoFuenteTexto), Brushes.Black, sinMargen, 203);
            //Obtiene placas del camión de la tabla Camion a partir de CamionID
            var camionPl = new CamionPL();
            CamionInfo datosCamion;
            if (tablavigilancia.Camion == null)
            {
                var txtCamionLocal = (System.Windows.Controls.TextBox)txtCamion;
                datosCamion = new CamionInfo
                                  {
                                      PlacaCamion = txtCamionLocal == null ? string.Empty : txtCamionLocal.Text
                                  };
            }
            else
            {
                datosCamion = camionPl.ObtenerPorID(tablavigilancia.Camion.CamionID);
            }
            e.Graphics.DrawString(
                Properties.Resources.FolioImpresoVigilancia_Placas.ToUpper() + Convert.ToChar(9) +
                datosCamion.PlacaCamion.ToUpper(), new Font("Arial", tamanoFuenteTexto), Brushes.Black, sinMargen, 233);
            //Imprime: Color Camion, a donde ingresa y nombre vigilante
            e.Graphics.DrawString(
                Properties.Resources.FolioImpresoVigilancia_Marca.ToUpper() + Convert.ToChar(9) +
                tablavigilancia.Marca.ToUpper(), new Font("Arial", tamanoFuenteTexto), Brushes.Black, sinMargen, 263);
            //Imprime: Color Camion, a donde ingresa y nombre vigilante
            e.Graphics.DrawString(
                Properties.Resources.FolioImpresoVigilancia_Color.ToUpper() + Convert.ToChar(9) + Convert.ToChar(9) +
                tablavigilancia.Color.ToUpper(), new Font("Arial", tamanoFuenteTexto), Brushes.Black, sinMargen, 293);
            e.Graphics.DrawString(
                Properties.Resources.FolioImpresoVigilancia_Vigilante.ToUpper() + Convert.ToChar(9) +
                Application.Current.Properties["Nombre"].ToString().ToUpper(), new Font("Arial", tamanoFuenteTexto), Brushes.Black,
                sinMargen, 323);
            e.Graphics.DrawString(
                Properties.Resources.FolioImpresoVigilancia_AccedeA.ToUpper() + Convert.ToChar(9) +
                Properties.Resources.BoletaVigilancia_Laboratorio.ToUpper(), new Font("Arial", tamanoFuenteTexto), Brushes.Black,
                sinMargen, 353);
        }

        /// <summary>
        /// Eventos del CheckBox Name="ckbSalida"
        /// </summary>
        private void ckbSalida_Checked(object sender, RoutedEventArgs e)
        {
            DtpFechaSalida.Text = String.Format("{0} {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString());
        }
        /// <summary>
        /// Cancelar limpia todos los campo y regresa foco al incio para llenar nuevamente un campo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.MateriaPrima_BoletaVigilancia_MensajeCancelar,
                MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {
                ckbSalida.IsChecked = false;
                ckbSalida.IsEnabled = false;
                esFolioExistente = false;
                HabilitarCampos();
                //También borra el transportista ligado a control de Chofer y Camión
                transportistaId = 0;
                LimpiarCampos();
            }
        }
        /// <summary>
        /// Si cambia el ID transportista, actualiza el filtro de Chofer y placas de chofer con el nuevo ID transportista
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SplAyudaTransportista_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(skAyudaVigilanciaTransportista.Clave))
            {
                if (transportistaId != Convert.ToInt32(skAyudaVigilanciaTransportista.Clave))
                {
                    transportistaId = Convert.ToInt32(skAyudaVigilanciaTransportista.Clave);

                }
            }
        }
        /// <summary>
        /// Éste evento busca el folio guardado en la tabla RegistroVigilancia para registrar salida o para imprimir un folio ya guardado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtFolio_OnKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.Key == Key.Enter || e.Key == Key.Tab) && !String.IsNullOrEmpty(TxtFolio.Text))
                {
                    var vigilanciaConsultarInfo = new RegistroVigilanciaInfo
                    {
                        Organizacion = new OrganizacionInfo { OrganizacionID = Convert.ToInt32(organizacionId) },
                        FolioTurno = Convert.ToInt32(TxtFolio.Text)
                    };
                    var crearVigilancia = new RegistroVigilanciaPL();
                    var tablaVigilancia = crearVigilancia.ObtenerRegistroVigilanciaPorFolioTurno(vigilanciaConsultarInfo);
                    if (tablaVigilancia != null) //si el folio si existe..
                    {
                        var entradaProductoPl = new EntradaProductoPL();
                        var entradaProducto =
                            entradaProductoPl.ObtenerEntradaProductoPorRegistroVigilanciaID(tablaVigilancia.RegistroVigilanciaId,
                                organizacionId);
                        if (entradaProducto != null)
                        {
                            if (entradaProducto.Producto.Familia.FamiliaID != FamiliasEnum.Premezclas.GetHashCode())
                            {
                                if (entradaProducto.Estatus != null && entradaProducto.Estatus.EstatusId != Estatus.Rechazado.GetHashCode())
                                {
                                    if (entradaProducto.PesoBruto > 0 && entradaProducto.PesoTara <= 0)
                                    {
                                        //No permite guardar si peso tara es 0
                                       
                                        if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                            Properties.Resources.BoletaVigilancia_msgPesajePendiente, MessageBoxButton.OK,
                                            MessageImage.Stop) == MessageBoxResult.OK)
                                            {
                                                TxtFolio.Clear();
                                                e.Handled = true;
                                                TxtFolio.Focus();    
                                                return;
                                            }
                                        return;
                                    }
                                }
                            }
                            else {
                                if (entradaProducto.PesoBruto > 0 && entradaProducto.PesoTara <= 0)
                                {
                                    //No permite guardar si peso tara es 0
                                    if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                        Properties.Resources.BoletaVigilancia_msgPesajePendiente, MessageBoxButton.OK,
                                        MessageImage.Stop) == MessageBoxResult.OK)
                                        {
                                            TxtFolio.Clear();
                                            e.Handled = true;
                                            TxtFolio.Focus();
                                            return;
                                        }
                                   
                                    return;
                                }
                            }
                            }
                        
                        //¿Está activo? si está activo, habilta el checkbox para registrar salida
                        if (tablaVigilancia.Activo.ValorBooleanoDesdeEnum())
                        {
                            //Código para imprimrir en pantalla los datos
                            TxtFolio.Text = Convert.ToString(tablaVigilancia.FolioTurno);

                            var vigilanciaConsultarTabla = new RegistroVigilanciaInfo
                            {
                                Organizacion = new OrganizacionInfo { OrganizacionID = Convert.ToInt32(organizacionId) },
                                FolioTurno = Convert.ToInt32(TxtFolio.Text)
                            };
                            var llenarVigilancia = new RegistroVigilanciaPL();
                            var tablavigilancia =
                                llenarVigilancia.ObtenerRegistroVigilanciaPorFolioTurno(vigilanciaConsultarTabla);

                            //Obtiene la descripción del producto de la tabla "Producto"  a partir de ProductoID
                            var productoInfo = new ProductoInfo { ProductoId = tablavigilancia.Producto.ProductoId };
                            var productoPl = new ProductoPL();
                            var datosProducto = productoPl.ObtenerPorID(productoInfo);
                            skAyudaVigilanciaProducto.Clave = Convert.ToString(datosProducto.ProductoId);
                            skAyudaVigilanciaProducto.Descripcion = Convert.ToString(datosProducto.ProductoDescripcion);
                            //Obtiene la descripción del proveedor a partir de proveedorID
                            var proveedorInfo = new ProveedorInfo
                            {
                                ProveedorID = tablavigilancia.ProveedorMateriasPrimas.ProveedorID
                            };
                            var proveedorPl = new ProveedorPL();
                            var datosProveedor = proveedorPl.ObtenerPorID(proveedorInfo);
                            skAyudaVigilanciaProveedor.Clave = Convert.ToString(datosProveedor.CodigoSAP);
                            skAyudaVigilanciaProveedor.Descripcion = Convert.ToString(datosProveedor.Descripcion);

                            var listaContratos = new List<ContratoInfo> { tablaVigilancia.Contrato };
                            cmbContrato.ItemsSource = listaContratos;
                            cmbContrato.SelectedValuePath = "ContratoID";
                            cmbContrato.DisplayMemberPath = "Folio";
                            cmbContrato.SelectedIndex = 0;
                            cmbContrato.IsEnabled = false;

                            if (tablaVigilancia.Contrato != null)
                            {
                                if (tablaVigilancia.Contrato.TipoFlete.TipoFleteId ==
                                    TipoFleteEnum.LibreAbordo.GetHashCode())
                                {
                                    txtTransportista = new System.Windows.Controls.TextBox()
                                    {
                                        Width = TxtFolio.ActualWidth,
                                        MaxLength = 50,
                                        Text = tablaVigilancia.Transportista,
                                        IsEnabled = false
                                    };
                                    SplAyudaTransportista.Children.Clear();
                                    SplAyudaTransportista.Children.Add(txtTransportista);

                                    txtChofer = new System.Windows.Controls.TextBox()
                                    {
                                        Width = TxtMarcaCamion.ActualWidth,
                                        MaxLength = 50,
                                        Text = tablaVigilancia.Chofer,
                                        IsEnabled = false
                                    };
                                    SplAyudaChofer.Children.Clear();
                                    SplAyudaChofer.Children.Add(txtChofer);

                                    txtCamion = new System.Windows.Controls.TextBox()
                                    {
                                        Width = TxtMarcaCamion.ActualWidth,
                                        MaxLength = 50,
                                        Text = tablaVigilancia.CamionCadena,
                                        IsEnabled = false
                                    };
                                    SplAyudaCamion.Children.Clear();
                                    SplAyudaCamion.Children.Add(txtCamion);
                                }
                                else
                                {
                                    AgregarAyudaTransportista();
                                    AgregarAyudaChofer(0);
                                    AgregarAyudaCamion(0);
                                    //Obtiene nombre de Chofer de la tabla "chofer" a partir del ID del chofer
                                    ProveedorChoferPL proveedorChoferPl = new ProveedorChoferPL();
                                    ProveedorChoferInfo proveedorChofer = proveedorChoferPl
                                        .ObtenerProveedorChoferPorProveedorChoferId(
                                            tablaVigilancia.ProveedorChofer.ProveedorChoferID);

                                    if (proveedorChofer != null)
                                    {
                                        if (proveedorChofer.Proveedor != null)
                                        {
                                            skAyudaChofer.Clave = Convert.ToString(proveedorChofer.Chofer.ChoferID);
                                            skAyudaChofer.Descripcion =
                                                Convert.ToString(proveedorChofer.Chofer.NombreCompleto);

                                            skAyudaVigilanciaTransportista.Clave =
                                                Convert.ToString(proveedorChofer.Proveedor.CodigoSAP);
                                            skAyudaVigilanciaTransportista.Descripcion =
                                                proveedorChofer.Proveedor.Descripcion;
                                        }
                                    }


                                    //Obtiene Placas del camión de la tabla Camion a partir del CamionID
                                    var camionPl = new CamionPL();
                                    var datosCamion = camionPl.ObtenerPorID(tablavigilancia.Camion.CamionID);
                                    skAyudaCamion.Clave = Convert.ToString(datosCamion.CamionID);
                                    skAyudaCamion.Descripcion = Convert.ToString(datosCamion.PlacaCamion);
                                }
                            }
                            else
                            {
                                AgregarAyudaTransportista();
                                AgregarAyudaChofer(0);
                                AgregarAyudaCamion(0);
                                //Obtiene nombre de Chofer de la tabla "chofer" a partir del ID del chofer
                                var proveedorChoferPl = new ProveedorChoferPL();
                                ProveedorChoferInfo proveedorChofer = proveedorChoferPl
                                    .ObtenerProveedorChoferPorProveedorChoferId(
                                        tablaVigilancia.ProveedorChofer.ProveedorChoferID);

                                if (proveedorChofer != null)
                                {
                                    if (proveedorChofer.Proveedor != null)
                                    {
                                        skAyudaChofer.Clave = Convert.ToString(proveedorChofer.Chofer.ChoferID);
                                        skAyudaChofer.Descripcion =
                                            Convert.ToString(proveedorChofer.Chofer.NombreCompleto);

                                        skAyudaVigilanciaTransportista.Clave =
                                            Convert.ToString(proveedorChofer.Proveedor.CodigoSAP);
                                        skAyudaVigilanciaTransportista.Descripcion =
                                            proveedorChofer.Proveedor.Descripcion;
                                    }
                                }


                                //Obtiene Placas del camión de la tabla Camion a partir del CamionID
                                var camionPl = new CamionPL();
                                var datosCamion = camionPl.ObtenerPorID(tablavigilancia.Camion.CamionID);
                                skAyudaCamion.Clave = Convert.ToString(datosCamion.CamionID);
                                skAyudaCamion.Descripcion = Convert.ToString(datosCamion.PlacaCamion);
                            }
                            //Obtiene el transportista  de la tabla ProveedorChofer


                            TxtMarcaCamion.Text = tablaVigilancia.Marca;
                            TxtColorCamion.Text = tablaVigilancia.Color;
                            fechaEntrada = tablaVigilancia.FechaLlegada.Date.ToShortDateString();
                            horaEntrada = tablavigilancia.FechaLlegada.ToShortTimeString();
                            DtpFechaEntrada.Text = String.Format("{0} {1}", fechaEntrada, horaEntrada);
                            DtpFechaSalida.Text = String.Format("{0} {1}",
                                tablaVigilancia.FechaLlegada.Date.ToShortDateString(),
                                tablavigilancia.FechaLlegada.ToShortTimeString());
                            //Código para deshabilitar los campos que contienen datos y habilitar checkbox salida
                            DeshabilitarCampos();
                            esFolioExistente = true;
                            ckbSalida.IsEnabled = true;
                            ckbSalida.IsChecked = true;

                            

                            
                        }
                        else
                        //No está activo, se considera que ya no existe porque ya no puede modificar (para checar salida)
                        {
                            //Folio no existe, avisa que no encontró número de folio...
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.CrearBoletaVigilancia_NoseencontroFolio, MessageBoxButton.OK,
                                MessageImage.Warning);
                            TxtFolio.Text = string.Empty;
                            e.Handled = true;
                        }
                    }
                    else
                    {
                        //Folio no existe, avisa que no encontró número de folio...
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.CrearBoletaVigilancia_NoseencontroFolio, MessageBoxButton.OK,
                            MessageImage.Warning);
                        TxtFolio.Text = string.Empty;
                        e.Handled = true;
                    }
                }
                else //Si el usuario no ingresó ningún caracter.....
                {
                    TxtFolio.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        /// <summary>
        /// Evento para validar la entrada de solo numeros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtFolio_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloNumeros(e.Text);
        }
        /// <summary>
        /// Validar la entrada de letras y numeros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtMarcaCamion_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarNumerosLetrasConAcentosGuion(e.Text);
        }
        /// <summary>
        /// Validar la entrada de letras y numeros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtColorCamion_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarNumerosLetrasConAcentosGuion(e.Text);
        }
        #endregion

        private void CmbContrato_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var contrato = (ContratoInfo)cmbContrato.SelectedItem;
            if (cmbContrato.SelectedIndex > 0)
            {
                var entradaProductoPl = new EntradaProductoPL();
                List<EntradaProductoInfo> listaEntradaProductos =
                    entradaProductoPl.ObtenerEntradaProductoPorContrato(contrato);

                if (listaEntradaProductos != null)
                {
                    var pesoTotal = listaEntradaProductos.Sum(registro => (registro.PesoBruto - registro.PesoTara));
                    decimal toleranciaCalculada = (contrato.Tolerancia *
                                                   contrato.Cantidad) / 100;

                    if (pesoTotal >
                        (toleranciaCalculada + contrato.Cantidad))
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.EntradaMateriaPrima_msgPesoBonificacionMayor,
                            MessageBoxButton.OK,
                            MessageImage.Stop);
                        cmbContrato.SelectedIndex = 0;
                        cmbContrato.IsEnabled = true;
                        return;
                    }
                }

                var programacionFletesPl = new ProgramaciondeFletesPL();
                var programacionFletes = programacionFletesPl.ObtenerFletes(contrato);

                if (programacionFletes == null && contrato.TipoFlete.TipoFleteId == (int)TipoFleteEnum.PagoenGanadera)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.BoletaVigilancia_msgRegistrarFlete, MessageBoxButton.OK,
                            MessageImage.Stop);
                    cmbContrato.SelectedIndex = 0;
                    return;
                }

                if (contrato.TipoFlete.TipoFleteId == (int)TipoFleteEnum.LibreAbordo)
                {
                    txtTransportista = new System.Windows.Controls.TextBox() { Width = TxtFolio.ActualWidth, MaxLength = 50 };
                    SplAyudaTransportista.Children.Clear();
                    SplAyudaTransportista.Children.Add(txtTransportista);

                    txtChofer = new System.Windows.Controls.TextBox() { Width = TxtMarcaCamion.ActualWidth, MaxLength = 50 };
                    SplAyudaChofer.Children.Clear();
                    SplAyudaChofer.Children.Add(txtChofer);

                    txtCamion = new System.Windows.Controls.TextBox() { Width = TxtMarcaCamion.ActualWidth, MaxLength = 50 };
                    SplAyudaCamion.Children.Clear();
                    SplAyudaCamion.Children.Add(txtCamion);
                }
                else if (contrato.TipoFlete.TipoFleteId == (int)TipoFleteEnum.PagoenGanadera)
                {
                    AgregarAyudaTransportista();
                    AgregarAyudaChofer(0);
                    AgregarAyudaCamion(0);
                }
            }
            else
            {
                AgregarAyudaTransportista();
                AgregarAyudaChofer(0);
                AgregarAyudaCamion(0);
            }
        }
    }
}