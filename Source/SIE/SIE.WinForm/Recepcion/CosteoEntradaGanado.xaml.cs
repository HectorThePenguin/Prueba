using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using Xceed.Wpf.Toolkit;

namespace SIE.WinForm.Recepcion
{
    /// <summary>
    /// Interaction logic for CosteoEntradaGanado.xaml
    /// </summary>
    public partial class CosteoEntradaGanado
    {
        #region CONSTRUCTORES
        /// <summary>
        /// Constructor por Default de la funcionalidad
        /// </summary>
        public CosteoEntradaGanado()
        {
            InitializeComponent();
            organizacionIdLogin = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
            usuarioLogueadoID = Extensor.ValorEntero(Application.Current.Properties["UsuarioID"].ToString());
            CargarAyudas();
        }
        #endregion CONSTRUCTORES

        #region PROPIEDADES
        /// <summary>
        /// Propiedad donde se almacena el DataContext de la funcionalidad
        /// </summary>
        private ContenedorCosteoEntradaGanadoInfo ContenedorCosteoEntrada
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ContenedorCosteoEntradaGanadoInfo)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }

        private int proveedorIDCostoAgregado = 0;
        private StackPanel panelNuevoCosto;
        #endregion PROPIEDADES

        #region VARIABLES
        /// <summary>
        /// Control para la ayuda de Folio de Entrada
        /// </summary>
        private SKAyuda<EntradaGanadoInfo> skAyudaFolio;
        /// <summary>
        /// Control para la ayuda de Organización
        /// </summary>
        private SKAyuda<OrganizacionInfo> skAyudaOrganizacion;
        /// <summary>
        /// Control para la ayuda de Costo
        /// </summary>
        private SKAyuda<CostoInfo> skAyudaCosto;
        /// <summary>
        /// Control para la ayuda de Proveedor
        /// </summary>
        private SKAyuda<ProveedorInfo> skAyudaProveedor;
        /// <summary>
        /// Control para la ayuda de Organización
        /// </summary>
        private SKAyuda<CuentaSAPInfo> skAyudaCuenta;

        /// <summary>
        /// Variable para tener la Organización Logueada
        /// </summary>
        private readonly int organizacionIdLogin;

        /// <summary>
        /// Vairiable para manejar el Usuario Logueado
        /// </summary>
        private readonly int usuarioLogueadoID;

        /// <summary>
        /// Variable para manejar los costos que solo levanta la ayuda
        /// </summary>
        private static readonly List<int> CostosValidosCaptura = new List<int> { 2, 3, 5, 6, 7, 4, 8 };

        /// <summary>
        /// Propiedad donde se revisan los tipos de Organizaciones a los que se capturan los datos
        /// </summary>
        private readonly int[] tiposOrganizacionesAplicaCaptura = new[] { TipoOrganizacion.CompraDirecta.GetHashCode(), TipoOrganizacion.Intensivo.GetHashCode(), TipoOrganizacion.Maquila.GetHashCode() };

        /// <summary>
        /// Variable para manejar el costo de ganado por default
        /// </summary>
        private const string CuentaInventario = "CTAINVTRAN";

        /// <summary>
        /// Variable para manejar el costo de ganado por default
        /// </summary>
        private IList<CostoInfo> listaCostos;

        /// <summary>
        /// Constante para manejar el Costo default
        /// </summary>
        private const string ClaveContableCostoGanado = "001";

        /// <summary>
        /// Constante para manejar el Costo default
        /// </summary>
        private List<TipoCostoInfo> listaTipoCosto;

        /// <summary>
        /// Valida si se han capturado las calidades del ganado
        /// </summary>
        private bool calidadesRegistradas;

        /// <summary>
        /// Valida que la interfaz tenga datos
        /// para solamente enviar un mensaje
        /// </summary>
        private bool tieneDatosInterface;

        /// <summary>
        /// ID del Costo de Comisión para la generación automatica
        /// </summary>
        private const int CostoComision = 3;

        #endregion VARIABLES

        #region METODOS

        /// <summary>
        /// Metodo que inicializa los objetos principales de la pantalla
        /// </summary>
        private void InicializaContexto()
        {
            ContenedorCosteoEntrada = new ContenedorCosteoEntradaGanadoInfo();
        }

        /// <summary>
        /// Metodo que inicializa los objetos principales de la pantalla
        /// </summary>
        private void CargarListaCostos()
        {
            var costoPL = new CostoPL();
            listaCostos = costoPL.ObtenerTodos(EstatusEnum.Activo);
        }

        /// <summary>
        /// Metodo que inicializa los objetos principales de la pantalla
        /// </summary>
        private void CargarListaTipoCosto()
        {
            var listaTipoCostos = (from costosValidos in CostosValidosCaptura
                                   select new TipoCostoInfo
                                   {
                                       TipoCostoID = costosValidos
                                   }).ToList();
            listaTipoCosto = listaTipoCostos;
        }

        /// <summary>
        /// Metodo para cargar las Horas por Default de los controles
        /// </summary>
        private void CargarHorasDefault()
        {
            dtuHoraRecepcion.Text = DateTime.Now.ToString("00:00");
            dtuHoraSalida.Text = DateTime.Now.ToString("00:00");
        }

        /// <summary>
        /// Metodo para cargar las Ayudas de la Funcionalidad
        /// </summary>
        private void CargarAyudas()
        {
            AgregarAyudaFolio();
            AgregarAyudaOrganizacion();
        }

        /// <summary>
        /// Agrega control de Ayuda para Folio
        /// </summary>
        private void AgregarAyudaFolio()
        {
            skAyudaFolio = new SKAyuda<EntradaGanadoInfo>(100, true, new EntradaGanadoInfo(), "PropiedadClaveCosteoEntrada"
                                                        , "PropiedadDescripcionCosteoEntrada"
                                                        , "PropiedadOcultaCostroEntradaGanado", true)
            {
                AyudaPL = new EntradaGanadoPL(),
                MensajeClaveInexistente = Properties.Resources.FolioEntrada_Inexistente,
                MensajeBusquedaCerrar = Properties.Resources.Folio_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Folio_Busqueda,
                MensajeAgregar = Properties.Resources.Folio_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyehdaAyudaBusquedaFolio,
                TituloPantalla = Properties.Resources.BusquedaEntradaGanado_Titulo,
            };
            var dependencias = ObtenerDependenciasOrganizacion();
            skAyudaFolio.Dependencias = null;
            skAyudaFolio.Dependencias = dependencias;

            skAyudaFolio.MensajeDependencias = null;
            IDictionary<String, String> mensajeDependencias = new Dictionary<String, String>();
            mensajeDependencias.Add("OrganizacionID", Properties.Resources.RecepcionGanado_SeleccionarTipoOrganizacion);
            skAyudaFolio.MensajeDependencias = mensajeDependencias;

            StpAyudaFolio.Children.Clear();
            StpAyudaFolio.Children.Add(skAyudaFolio);

            skAyudaFolio.LlamadaMetodos = ConsultarEntradaGanado;
        }

        /// <summary>
        /// Bloquea los todos los botones al iniciar la forma
        /// </summary>
        private void BloquearBotones()
        {
            btnAgregar.IsEnabled = false;
            btnAgregarCostoGanado.IsEnabled = false;
            btnCancelar.IsEnabled = false;
            btnCancelarCostoGanado.IsEnabled = false;
            btnCancelarTipoGanado.IsEnabled = false;
            btnGuardar.IsEnabled = false;
        }

        /// <summary>
        /// Agrega control de Ayuda para Costo
        /// </summary>
        private void AgregarAyudaCosto(StackPanel stackPanel)
        {
            var costoInfo = new CostoInfo
            {
                ListaTipoCostos = listaTipoCosto
            };
            skAyudaCosto = new SKAyuda<CostoInfo>(160, false, costoInfo, "PropiedadClaveCosteoEntrada"
                                                , "PropiedadDescripcionCosteoEntrada"
                                                , "PropiedadOcultaCosteoEntrada", true, true)
            {
                AyudaPL = new CostoPL(),
                MensajeClaveInexistente = Properties.Resources.Costo_CodigoInvalidoCosteo,
                MensajeBusquedaCerrar = Properties.Resources.Costo_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Costo_Busqueda,
                MensajeAgregar = Properties.Resources.Costo_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyehdaAyudaBusquedaCosto,
                TituloPantalla = Properties.Resources.BusquedaCosto_Titulo,
            };
            skAyudaCosto.ObtenerDatos += ValidarAbonoCosto;

            stackPanel.Children.Clear();
            stackPanel.Children.Add(skAyudaCosto);

        }

        private void ValidarAbonoCosto(string costoID)
        {
            var costo = int.Parse(costoID);

            var costosValidar = ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada[DgCostoGanado.SelectedIndex];

            var costoConsultar = listaCostos.FirstOrDefault(cos => cos.CostoID.Equals(costosValidar.Costo.CostoID));

            switch (costoConsultar.AbonoA)
            {
                case AbonoA.AMBOS:
                    costosValidar.EditarCuentaProveedor = true;
                    costosValidar.EditarTieneCuenta = true;
                    break;
                case AbonoA.CUENTA:
                    costosValidar.TieneCuenta = true;
                    costosValidar.EditarTieneCuenta = false;
                    costosValidar.EditarCuentaProveedor = true;
                    break;
                case AbonoA.PROVEEDOR:
                    costosValidar.EditarCuentaProveedor = true;
                    costosValidar.TieneCuenta = false;
                    costosValidar.EditarTieneCuenta = false;
                    break;
            }
            costosValidar.EditarNumeroDocumento = true;
            costosValidar.CostoEmbarque = true;
            if (costosValidar.Proveedor == null)
            {
                costosValidar.Proveedor = new ProveedorInfo();
            }

            var rowIndex = DgCostoGanado.SelectedIndex;
            var cell = GetCell(rowIndex, 2);
            var cellCombo = GetCell(rowIndex, 4);
            var combo = GetVisualChild<ComboBox>(cellCombo);

            if (costosValidar.Costo.CostoID == Costo.Comision.GetHashCode())
            {
                combo.IsEditable = false;
            }
            else
            {
                combo.IsEditable = true;
            }
            combo.SelectedIndex = -1;
        }

        /// <summary>
        /// Metodo para agregar el Control Ayuda Proveedor
        /// </summary>
        private void AgregarAyudaProveedor(StackPanel stackPanel)
        {
            skAyudaProveedor = new SKAyuda<ProveedorInfo>(160, false, new ProveedorInfo()
                                                        , "PropiedadClaveCosteoGanado"
                                                        , "PropiedadDescripcionCosteoGanado"
                                                        , "PropiedadOcultaCosteo"
                                                        , true, 80, 10, true)
            {
                AyudaPL = new ProveedorPL(),
                MensajeClaveInexistente = Properties.Resources.Proveedor_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.Proveedor_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Proveedor_Busqueda,
                MensajeAgregar = Properties.Resources.Proveedor_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaProveedor,
                TituloPantalla = Properties.Resources.BusquedaProveedor_Titulo,
            };
            skAyudaProveedor.Info = new ProveedorInfo();
            skAyudaProveedor.ObtenerDatos += GenerarComisionAutomatica;
            skAyudaProveedor.LimpiarCampos();
            stackPanel.Children.Clear();
            stackPanel.Children.Add(skAyudaProveedor);
        }

        private void GenerarComisionAutomatica(string proveedorID)
        {

            int proveedor = Convert.ToInt32(proveedorID);

            var rowIndex = DgCostoGanado.SelectedIndex;
            var cell = GetCell(rowIndex, 2);
            var ayuda = GetVisualChild<UserControl>(cell);
            var ayudaProveedor = (SKAyuda<ProveedorInfo>)(ayuda);

            var codigoSap = ayudaProveedor.Clave;

            var proveedorPL = new ProveedorPL();
            var proveedorInfo = proveedorPL.ObtenerPorCodigoSAP(new ProveedorInfo { CodigoSAP = codigoSap });
            proveedor = proveedorInfo.ProveedorID;

            proveedorIDCostoAgregado = Convert.ToInt32(proveedorID);
            List<ComisionInfo> comisiones = new List<ComisionInfo>();
            var costoComisionProveedor =
                ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada.FirstOrDefault(costo =>
                {
                    if ((costo.Costo != null && costo.Costo.CostoID == CostoComision) &&
                        (costo.Proveedor != null && costo.Proveedor.ProveedorID == proveedor))
                    {
                        return true;
                    }
                    return false;
                });

            var cellChk = GetCell(DgCostoGanado.SelectedIndex, 1);
            var chk = GetVisualChild<CheckBox>(cellChk);
            var CostoSeleccionado = (EntradaGanadoCostoInfo)chk.CommandParameter;
            if (proveedorInfo.TipoProveedor.TipoProveedorID == TipoProveedorEnum.Comisionistas.GetHashCode())
            {
                CostoSeleccionado.EditarIvaRetencion = false;
                var proveedorRetencionBL = new ProveedorRetencionBL();
                IList<ProveedorRetencionInfo> listaRetenciones =
                    proveedorRetencionBL.ObtenerPorProveedorID(proveedorInfo.ProveedorID);
                if (listaRetenciones != null && listaRetenciones.Any())
                {
                    CostoSeleccionado.Retencion =
                        listaRetenciones.Any(ret => ret.Activo == EstatusEnum.Activo && ret.Retencion.RetencionID > 0);
                    CostoSeleccionado.Iva = listaRetenciones.Any(ret => ret.Activo == EstatusEnum.Activo && ret.Iva.IvaID > 0);
                }
            }
            
            var cellCombo = GetCell(DgCostoGanado.SelectedIndex, 4);
            var combo = GetVisualChild<ComboBox>(cellCombo);
            if (CostoSeleccionado.Costo.CostoID == Costo.Comision.GetHashCode())
            {

                if (combo != null && combo.Items != null)
                {
                    comisiones = obtenerComisionesProveedor(proveedor);
                    combo.ItemsSource = null;
                    combo.ItemsSource = comisiones;
                    if (comisiones != null && comisiones.Count == 1)
                    {
                        combo.SelectedIndex = -1;
                        combo.SelectedIndex = 0;
                        CostoSeleccionado.Importe = comisiones[0].Tarifa;
                    }
                    else
                        combo.SelectedIndex = -1;
                }
            }
            else
            {
                combo.IsEditable = true;
            }
            if (costoComisionProveedor == null)
            {

                return;
            }

            if (ayudaProveedor == null)
            {
                return;
            }
            if (proveedorInfo.TipoProveedor.TipoProveedorID != TipoProveedorEnum.Comisionistas.GetHashCode())
            {

                ayudaProveedor.LimpiarCampos();

                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          Properties.Resources.CosteoEntradaGanado_NoEsComisionista,
                                          MessageBoxButton.OK, MessageImage.Stop);
                costoComisionProveedor.Proveedor = new ProveedorInfo();
                ayudaProveedor.LimpiarCampos();
                ayudaProveedor.AsignarFoco();
                return;
            }

            if (comisiones.Count < 1)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          Properties.Resources.CosteoEntradaGanado_SinImporteComision,
                                          MessageBoxButton.OK, MessageImage.Stop);
                costoComisionProveedor.Proveedor = new ProveedorInfo();
                ayudaProveedor.LimpiarCampos();
                costoComisionProveedor.Importe = 0;
                ayudaProveedor.AsignarFoco();
                return;
            }
            costoComisionProveedor.CostoEmbarque = false;
        }

        /// <summary>
        /// Metodo para agregar el Control Ayuda Organizacion
        /// </summary>
        private void AgregarAyudaOrganizacion()
        {
            skAyudaOrganizacion = new SKAyuda<OrganizacionInfo>("OrganizacionOrigenID", "OrganizacionOrigen", 160, false, true);
            var camposInfo = new List<String> { "OrganizacionID", "Descripcion" };
            skAyudaOrganizacion.AyudaPL = new OrganizacionPL();
            skAyudaOrganizacion.CamposInfo = camposInfo;
            skAyudaOrganizacion.Info = new OrganizacionInfo();
            skAyudaOrganizacion.AyudaPL = new OrganizacionPL();
            skAyudaOrganizacion.CamposInfo = camposInfo;

            StpAyudaOrigen.Children.Clear();
            StpAyudaOrigen.Children.Add(skAyudaOrganizacion);

            skAyudaOrganizacion.MensajeClaveInexistente = Properties.Resources.Organizacion_CodigoInvalido;
            skAyudaOrganizacion.MensajeAgregar = Properties.Resources.Organizacion_Seleccionar;
            skAyudaOrganizacion.MensajeBusqueda = Properties.Resources.Organizacion_Busqueda;
            skAyudaOrganizacion.MensajeBusquedaCerrar = Properties.Resources.Organizacion_SalirSinSeleccionar;
            skAyudaOrganizacion.TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaOrganizacion;
            skAyudaOrganizacion.TituloPantalla = Properties.Resources.BusquedaOrigen_Titulo;
            skAyudaOrganizacion.IsEnabled = false;
        }

        /// <summary>
        /// Obtiene la Depedencia de Organizacion
        /// </summary>
        /// <returns></returns>
        private IList<IDictionary<IList<string>, Object>> ObtenerDependenciasOrganizacion()
        {
            IList<IDictionary<IList<string>, Object>> dependencias = new List<IDictionary<IList<string>, Object>>();
            IDictionary<IList<string>, Object> dependecia = new Dictionary<IList<string>, Object>();
            IList<string> camposDependientes = new List<string>();

            camposDependientes.Add("OrganizacionID");

            var organizacionInfo = new OrganizacionInfo { OrganizacionID = organizacionIdLogin };

            dependecia.Add(camposDependientes, organizacionInfo);
            dependencias.Add(dependecia);

            return dependencias;
        }

        /// <summary>
        /// Consulta el Folio de Entrada Seleccionado
        /// </summary>
        private void ConsultarEntradaGanado()
        {
            try
            {
                calidadesRegistradas = false;
                tieneDatosInterface = true;
                int folioEntrada;
                int.TryParse(skAyudaFolio.Descripcion, out folioEntrada);
                if (folioEntrada == 0)
                {
                    skAyudaFolio.Descripcion = skAyudaFolio.Clave;
                    int.TryParse(skAyudaFolio.Descripcion, out folioEntrada);
                }
                var entradaGanadoPL = new EntradaGanadoPL();
                var entradaGanado = entradaGanadoPL.ObtenerEntradasGanadoRecibidas(folioEntrada, organizacionIdLogin);
                if (entradaGanado == null)
                {
                    return;
                }
                ContenedorCosteoEntrada.EntradaGanado = entradaGanado;
                CargarCamposConsulta();
                if (tieneDatosInterface)
                {
                    if (entradaGanado.TipoOrigen == TipoOrganizacion.Ganadera.GetHashCode()
                        || calidadesRegistradas)
                    {
                        var dataContext = DataContext;
                        DataContext = null;
                        DataContext = dataContext;
                        skAyudaFolio.Descripcion =
                            ContenedorCosteoEntrada.EntradaGanado.FolioEntrada.ToString(CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          Properties.Resources.CosteoGanado_CalidadesNoCapturadas,
                                          MessageBoxButton.OK, MessageImage.Stop);
                        LimpiarControles();
                    }
                }

                ActualizarImportesGridCostos();

            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void ActualizarImportesGridCostos()
        {
            if (DgCostoGanado.Items.Count > 0)
            {
                for (int i = 0; i < DgCostoGanado.Items.Count; i++)
                {
                    var cellCombo = GetCell(i, 4);
                    var combo = GetVisualChild<ComboBox>(cellCombo);
                    var datos = (EntradaGanadoCostoInfo)combo.DataContext;

                    if (datos.Costo.CostoID == Costo.Comision.GetHashCode())
                    {
                        combo.IsEditable = false;
                        List<ComisionInfo> comisiones = new List<ComisionInfo>();
                        var renglon = (EntradaGanadoCostoInfo)DgCostoGanado.Items[i];
                        decimal importe = decimal.Parse(renglon.Importe.ToString());

                        if (datos.Proveedor != null)
                        {
                            var proveedorComisionID = datos.ProveedorComisionID;
                            if (datos.Proveedor.ProveedorID > 0)
                            {
                                comisiones.AddRange(obtenerComisionesProveedor(datos.Proveedor.ProveedorID));
                            }

                            if (combo.SelectedValue.GetHashCode() > 0)
                                proveedorComisionID = combo.SelectedValue.GetHashCode();

                            combo.ItemsSource = null;
                            combo.ItemsSource = comisiones;
                            if (combo.ItemsSource != null)
                            {
                                combo.SelectedValue = proveedorComisionID;
                                datos.ProveedorComisionID = proveedorComisionID;
                                foreach (ComisionInfo comision in comisiones)
                                {
                                    if (comision.ProveedorComisionID == proveedorComisionID)
                                    {
                                        datos.Importe = comision.Tarifa;
                                    }
                                }
                            }
                        }
                        else
                        {
                            comisiones.Add(new ComisionInfo() { ProveedorComisionID = 0, Tarifa = datos.Importe });
                            combo.ItemsSource = comisiones;
                            combo.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        if (datos.Importe > 0)
                        {
                            List<ComisionInfo> comisiones = new List<ComisionInfo>();
                            comisiones.Add(new ComisionInfo() { ProveedorComisionID = 0, Tarifa = datos.Importe });
                            combo.ItemsSource = comisiones;
                            combo.SelectedIndex = 0;
                        }
                        combo.IsEditable = true;
                        combo.Text = formatearImporte(datos.Importe.ToString());
                        if (datos.Costo.CostoID == Costo.CostoGanado.GetHashCode())
                        {
                            combo.IsEnabled = false;
                        }
                    }
                }
            }
        }

        private string formatearImporte(string importe)
        {
            decimal valor;
            decimal.TryParse(importe.Replace("$", ""), out valor);
            string[] datosimporte = valor.ToString().Split('.');
            string importeFormateado = string.Empty;

            importeFormateado = string.Format("{0:C}", valor);
            return importeFormateado;
        }
        private List<ComisionInfo> obtenerComisionesProveedor(int ProveedorID)
        {
            List<ComisionInfo> ListaComisiones = new List<ComisionInfo>();
            ComisionPL comisionPL = new ComisionPL();
            decimal pesoOrigen = 0;
            ListaComisiones = comisionPL.obtenerComisionesProveedor(ProveedorID);
            if (ListaComisiones.Count > 0)
            {
                ListaComisiones = ListaComisiones.Where(comision => ((comision.TipoComisionID == TipoComisionEnum.CompraDirecta.GetHashCode()
                                                    && ContenedorCosteoEntrada.EntradaGanado.TipoOrigen == TipoOrganizacion.CompraDirecta.GetHashCode())
                                                    || (comision.TipoComisionID == TipoComisionEnum.CompraCentro.GetHashCode()
                                                       && ContenedorCosteoEntrada.EntradaGanado.TipoOrigen == TipoOrganizacion.Centro.GetHashCode())
                                                    || (ContenedorCosteoEntrada.EntradaGanado.TipoOrigen != TipoOrganizacion.Centro.GetHashCode()
                                                        && ContenedorCosteoEntrada.EntradaGanado.TipoOrigen != TipoOrganizacion.CompraDirecta.GetHashCode()))).ToList();

                if (ListaComisiones != null && ListaComisiones.Count > 0)
                {
                    if (ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaEntradaDetalle != null)
                    {
                        pesoOrigen = ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaEntradaDetalle.Sum(cos => cos.PesoOrigen);
                    }
                    else
                    {
                        pesoOrigen = ContenedorCosteoEntrada.EntradaGanado.PesoOrigen;
                    }
                    foreach (var comision in ListaComisiones)
                    {
                        comision.Tarifa *= pesoOrigen;
                    }
                }
            }
            else
            {
                ListaComisiones.Add(new ComisionInfo { Tarifa = 0, TipoComisionID = 0 });
            }
            return ListaComisiones;
        }
        /// <summary>
        /// Carga la información en los Controles del Folio de Entrada consultado
        /// </summary>
        private void CargarCamposConsulta()
        {
            skAyudaFolio.IsEnabled = false;

            skAyudaOrganizacion.Descripcion = ContenedorCosteoEntrada.EntradaGanado.OrganizacionOrigen;
            skAyudaOrganizacion.Clave =
                ContenedorCosteoEntrada.EntradaGanado.OrganizacionOrigenID.ToString(CultureInfo.InvariantCulture);

            List<ComisionInfo> ListaComisiones = new List<ComisionInfo>();
            dpFechaSalida.SelectedDate = ContenedorCosteoEntrada.EntradaGanado.FechaSalida.Date;
            dtuHoraSalida.Text = ContenedorCosteoEntrada.EntradaGanado.FechaSalida.ToString("HH:mm");

            dpFechaRecepcion.SelectedDate = ContenedorCosteoEntrada.EntradaGanado.FechaEntrada.Date;
            dtuHoraRecepcion.Text = ContenedorCosteoEntrada.EntradaGanado.FechaEntrada.ToString("HH:mm");

            var pesoNeto = ContenedorCosteoEntrada.EntradaGanado.PesoBruto -
                               ContenedorCosteoEntrada.EntradaGanado.PesoTara;

            txtPesoLlegada.Text = string.Format(CultureInfo.InvariantCulture, "{0:#,#}", pesoNeto);

            txtDiasEstancia.Text = "0";

            var entradaGanadoCosteoPL = new EntradaGanadoCosteoPL();
            EntradaGanadoCosteoInfo entradaGanadoCosteo =
                entradaGanadoCosteoPL.ObtenerPorEntradaID(ContenedorCosteoEntrada.EntradaGanado.EntradaGanadoID);

            if (entradaGanadoCosteo != null)
            {
                ContenedorCosteoEntrada.EntradaGanadoCosteo = entradaGanadoCosteo;

                CargarGridTipoGanado();
                ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaEntradaDetalle.ToList().ForEach(det => det.ListaTiposGanado = ObtenerListaTiposGanado());
                ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada.ToList().ForEach(cost => cost.EditarNumeroDocumento = false);

                btnCancelar.IsEnabled = true;
                txtObservaciones.IsEnabled = false;

                stpCalidadMacho.IsEnabled = false;
                stpCalidadHembra.IsEnabled = false;

                var cuentaPL = new CuentaPL();

                ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada.ForEach(costo =>
                {
                    if (!string.IsNullOrWhiteSpace(costo.DescripcionCuenta))
                    {
                        return;
                    }
                    var claveContable = cuentaPL.ObtenerPorClaveCuentaOrganizacion(CuentaInventario, ContenedorCosteoEntrada.EntradaGanado.OrganizacionOrigenID);
                    if (claveContable != null)
                    {
                        costo.DescripcionCuenta = claveContable.Descripcion;
                    }
                });
                CargarGridCosto();
                ObtenerCalidadGanado();
                return;
            }
            if (tiposOrganizacionesAplicaCaptura.Contains(ContenedorCosteoEntrada.EntradaGanado.TipoOrigen))
            {
                btnAgregar.IsEnabled = true;
                CargarCostoGanadoDefault();
                btnCancelar.IsEnabled = false;
                CargarListaCalidad();
                ObtenerCalidadGanado();
            }
            else
            {
                if (!CargarDatosInterface())
                {
                    return;
                }
                var entradaDetalleInfo = ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaEntradaDetalle.FirstOrDefault();
                var fechaSalidaInterface = new DateTime();
                if (entradaDetalleInfo != null)
                {
                    fechaSalidaInterface =
                        entradaDetalleInfo.
                            FechaSalidaInterface;
                }
                dpFechaSalida.SelectedDate = fechaSalidaInterface.Date;
                dtuHoraSalida.Text = fechaSalidaInterface.ToString("HH:mm");
                CargarListaCalidad();
                ObtenerCalidadGanado();
            }
            btnGuardar.IsEnabled = true;
            var costoEmbarqueDetallePL = new CostoEmbarqueDetallePL();
            if (ContenedorCosteoEntrada.EntradaGanadoCosteo.EntradaGanadoCosteoID == 0)
            {

                var listaCostosEmbarque =
                    costoEmbarqueDetallePL.ObtenerPorEmbarqueIDOrganizacionOrigen(ContenedorCosteoEntrada.EntradaGanado);
                if (listaCostosEmbarque != null)
                {
                    listaCostosEmbarque.ForEach(costo => costo.EditarIvaRetencion = true);
                    ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada.AddRange(listaCostosEmbarque);
                }
                if (!ValidarCuentaCostos())
                {
                    btnGuardar.IsEnabled = false;
                    return;
                }
            }
            List<EntradaGanadoInfo> listaEntradasDeEmbarque =
                      costoEmbarqueDetallePL.ObtenerEntradasPorEmbarqueID(ContenedorCosteoEntrada.EntradaGanado);
            List<EntradaGanadoInfo> listaCompraDirecta = listaEntradasDeEmbarque.Where(
                entradaGanadoInfo => entradaGanadoInfo.TipoOrigen == (int)TipoOrganizacion.CompraDirecta).ToList();
            if (ContenedorCosteoEntrada.EntradaGanado.EsRuteo)
            {
                listaCostosFletes =
                    costoEmbarqueDetallePL.ObtenerPorEmbarqueID(ContenedorCosteoEntrada.EntradaGanado);

                var interfaceSalidaPL = new InterfaceSalidaPL();
                Dictionary<int, decimal> importesProrrateados;
                if (listaCostosFletes != null)
                {
                    //Se obtieneEntradas que pertenecen al Embarque
                    //Validar si es viene es una compra directa en el ruteo
                    if (ContenedorCosteoEntrada.EntradaGanado.TipoOrigen != (int)TipoOrganizacion.CompraDirecta)
                    {
                        //Validar que no existan compras directas
                        if (listaCompraDirecta.Any())
                        {
                            //Validar que no existan compras directas sin costear
                            if (listaCompraDirecta.Any(entradaGanadoInfo => !entradaGanadoInfo.Costeado))
                            {
                                var folioEntrada =
                                    listaCompraDirecta.First(entradaGanadoInfo => !entradaGanadoInfo.Costeado);

                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    String.Format("{0} {1} {2}",
                                    Properties.Resources.CosteoEntradaGanado_RuteoCompraDirectaNoCosteada,
                                    folioEntrada.FolioEntrada,
                                    Properties.Resources.CosteoEntradaGanado_RuteoCompraDirectaNoCosteada2),
                                    MessageBoxButton.OK,
                                    MessageImage.Stop);

                                btnGuardar.IsEnabled = false;
                                return;
                            }
                        }
                    }
                    /* Obtener los costos prorrateados */
                    importesProrrateados =
                        interfaceSalidaPL.ObtenerCostoFleteProrrateado(ContenedorCosteoEntrada.EntradaGanado,
                                                                       listaCostosFletes,
                                                                       listaCompraDirecta);
                    if (listaCompraDirecta.Count == listaEntradasDeEmbarque.Count)
                    {
                        importesProrrateados = new Dictionary<int, decimal>();
                        int totalPesos = Convert.ToInt32(listaEntradasDeEmbarque.Sum(ent => ent.PesoBruto - ent.PesoTara));
                        int pesoEntrada = Convert.ToInt32(ContenedorCosteoEntrada.EntradaGanado.PesoBruto -
                                          ContenedorCosteoEntrada.EntradaGanado.PesoTara);
                        foreach (var costo in listaCostosFletes)
                        {
                            decimal precioPorKilo = costo.Importe / totalPesos;
                            decimal importeProrrateado = Math.Round(precioPorKilo * pesoEntrada, 2);
                            importesProrrateados.Add(costo.Costo.CostoID, importeProrrateado);
                        }
                    }
                    if (importesProrrateados == null)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          Properties.Resources.CosteoEntradaGanado_InterfacesRuteo,
                                          MessageBoxButton.OK,
                                          MessageImage.Stop);
                        btnGuardar.IsEnabled = false;
                        return;
                    }

                    foreach (var importes in importesProrrateados)
                    {
                        var costoModificar =
                            ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada.FirstOrDefault(
                                costo => costo.Costo.CostoID.Equals(importes.Key));

                        if (costoModificar == null)
                        {
                            continue;
                        }
                        costoModificar.Importe = importes.Value;
                    }
                    foreach (var costoFletes in listaCostosFletes)
                    {
                        var costoExiste =
                            ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada.FirstOrDefault(
                                costo => costo.Costo.CostoID.Equals(costoFletes.Costo.CostoID));

                        if (costoExiste == null)
                        {
                            costoFletes.EditarIvaRetencion = true;
                            ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada.Add(costoFletes);
                        }
                    }
                }
            }

            var entradaGanadoMuerteBL = new EntradaGanadoMuerteBL();
            ContenedorCosteoEntrada.EntradasGanadoMuertes =
                entradaGanadoMuerteBL.ObtenerMuertesEnTransitoPorEntradaGanadoID(
                    ContenedorCosteoEntrada.EntradaGanado.EntradaGanadoID);
            if (ContenedorCosteoEntrada.EntradasGanadoMuertes == null)
            {
                ContenedorCosteoEntrada.EntradasGanadoMuertes = new List<EntradaGanadoMuerteInfo>();
            }

            if (ContenedorCosteoEntrada.EntradaGanado.TipoOrigen == TipoOrganizacion.Ganadera.GetHashCode()
                || calidadesRegistradas)
            {
                CargarCostosAutomaticos();
                CargarGridCosto();
                CargarGridTipoGanado();
                CargarControlesCalidadGanado();
                btnAgregarCostoGanado.IsEnabled = true;

                if (ContenedorCosteoEntrada.EntradaGanadoCosteo.EntradaGanadoCosteoID != 0)
                {
                    btnCancelar.IsEnabled = true;
                    btnAgregarCostoGanado.IsEnabled = false;
                    btnGuardar.IsEnabled = false;
                }
            }
        }

        private List<EntradaGanadoCostoInfo> listaCostosFletes;

        /// <summary>
        /// Metodo para obtener el prorrateo de costos de fletes en ruteos con comprsa directas
        /// </summary>
        private void CalcularProrrateoCostoFlete(decimal totalPesoOrigen)
        {
            var detalles = ContenedorCosteoEntrada.EntradaGanado.ListaInterfaceSalida.SelectMany(
                            isa => isa.ListaInterfaceDetalle).SelectMany(
                                isd => isd.ListaInterfaceSalidaAnimal);

            var pesosDetalles = (from isa in detalles
                                 group isa by isa.SalidaID
                                     into agrupado
                                     select new
                                     {
                                         SalidaID = agrupado.Key,
                                         PesoOrigen = agrupado.Sum(animal => animal.PesoCompra)
                                     }).ToList();
            if (pesosDetalles.Any())
            {
                decimal pesoTotalOrigen = pesosDetalles.Sum(pes => pes.PesoOrigen) + totalPesoOrigen;

                var importesProrrateado = new Dictionary<int, decimal>();
                //Se obtiene el peso capturado en la lista de
                listaCostosFletes.ForEach(costos =>
                {
                    var costoUnitarioKilo = costos.Importe / pesoTotalOrigen;
                    //Ojo con el peso escala cuandpo es compra directa
                    decimal costoFlete = Math.Round(costoUnitarioKilo * totalPesoOrigen, 2);

                    importesProrrateado.Add(costos.Costo.CostoID, costoFlete);
                });

                foreach (var importes in importesProrrateado)
                {
                    var costoModificar =
                        ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada.FirstOrDefault(
                            costo => costo.Costo.CostoID.Equals(importes.Key));

                    if (costoModificar == null)
                    {
                        continue;
                    }
                    costoModificar.Importe = importes.Value;
                }
            }
        }

        /// <summary>
        /// Obtiene las calidades de ganado
        /// </summary>
        private void ObtenerCalidadGanado()
        {
            if (skAyudaFolio.Info.EntradaGanadoID == 0)
            {
                var entradaGanadoPL = new EntradaGanadoPL();
                EntradaGanadoInfo entradaGanado =
                    entradaGanadoPL.ObtenerEntradaGanadoCapturaCalidad(new FiltroCalificacionGanadoInfo
                    {
                        OrganizacionID = organizacionIdLogin,
                        FolioEntrada =
                            Convert.ToInt32(
                                skAyudaFolio.Descripcion)
                    });
                if (entradaGanado != null)
                {
                    skAyudaFolio.Info.EntradaGanadoID = entradaGanado.EntradaGanadoID;
                }
            }
            List<EntradaGanadoCalidadInfo> calidadesGanado =
                ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCalidadGanado.Where(valor => valor.Valor > 0).ToList();
            var entradaGanadoCalidadPL = new EntradaGanadoCalidadPL();
            ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCalidadGanado =
                entradaGanadoCalidadPL.ObtenerEntradaGanadoId(skAyudaFolio.Info.EntradaGanadoID);

            int totalCabezasCalidad;
            if (ContenedorCosteoEntrada.EntradaGanado.TipoOrigen == TipoOrganizacion.Ganadera.GetHashCode())
            {
                EntradaGanadoCalidadInfo entradaGanadoCalidad;
                ContenedorCosteoEntrada.EntradaGanadoCosteo
                    .ListaCalidadGanado
                    .ForEach(datos =>
                    {
                        entradaGanadoCalidad =
                            calidadesGanado.FirstOrDefault(
                                id =>
                                id.CalidadGanado.Descripcion.Equals(datos.CalidadGanado.Descripcion)
                                && id.CalidadGanado.Sexo.Equals(datos.CalidadGanado.Sexo));
                        if (entradaGanadoCalidad != null)
                        {
                            datos.Valor = entradaGanadoCalidad.Valor;
                        }
                    });
                totalCabezasCalidad =
                    ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCalidadGanado.Sum(cab => cab.Valor);
            }
            else
            {
                totalCabezasCalidad = ContenedorCosteoEntrada.EntradaGanado.ListaCondicionGanado.Where(
                    muertas => muertas.CondicionID == 1 || muertas.CondicionID == 5).Sum(cabezas => cabezas.Cabezas);
            }
            lblCabezasCalidad.Content = totalCabezasCalidad;

            if (totalCabezasCalidad > 0)
            {
                calidadesRegistradas = true;

                if (ContenedorCosteoEntrada.EntradaGanado != null
                    && ContenedorCosteoEntrada.EntradaGanado.ListaCondicionGanado != null
                    && ContenedorCosteoEntrada.EntradaGanado.ListaCondicionGanado.Any())
                {
                    decimal cabezasMuertas =
                        ContenedorCosteoEntrada.EntradaGanado.ListaCondicionGanado.Where(
                            muertas => muertas.CondicionID != 1 && muertas.CondicionID != 5).
                            Sum(cabezas => cabezas.Cabezas);

                    lblCabezasMuertas.Content = cabezasMuertas + ContenedorCosteoEntrada.EntradaGanado.CabezasMuertas;
                }
            }
        }

        /// <summary>
        /// Carga los Costos Automaticos de la Organización de Origen
        /// </summary>
        private void CargarCostosAutomaticos()
        {
            var costoOrganizacionPL = new CostoOrganizacionPL();
            var listaCostosAutomaticos =
                costoOrganizacionPL.ObtenerCostosAutomaticos(ContenedorCosteoEntrada.EntradaGanado);
            if (listaCostosAutomaticos == null)
            {
                return;
            }
            AsignarImportesCostosAutomaticos(listaCostosAutomaticos);
            listaCostosAutomaticos.ForEach(costoAutomatico =>
            {
                if (!ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada.Any(cost => cost.Costo.CostoID.Equals(costoAutomatico.Costo.CostoID)))
                {
                    switch (costoAutomatico.Costo.AbonoA)
                    {
                        case AbonoA.AMBOS:
                            costoAutomatico.EditarCuentaProveedor = true;
                            costoAutomatico.EditarTieneCuenta = true;
                            break;
                        case AbonoA.CUENTA:
                            costoAutomatico.TieneCuenta = true;
                            costoAutomatico.EditarTieneCuenta = false;
                            costoAutomatico.EditarCuentaProveedor = true;
                            break;
                        case AbonoA.PROVEEDOR:
                            costoAutomatico.EditarCuentaProveedor = true;
                            costoAutomatico.TieneCuenta = false;
                            costoAutomatico.EditarTieneCuenta = false;
                            break;
                    }
                    costoAutomatico.EditarNumeroDocumento = true;
                    costoAutomatico.CostoEmbarque = true;
                    costoAutomatico.Proveedor = new ProveedorInfo();
                    costoAutomatico.EditarIvaRetencion = true;
                    ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada.Add(costoAutomatico);
                }
            });
        }

        /// <summary>
        /// Metodo que asigna los importes a los Costos Automaticos
        /// </summary>
        private void AsignarImportesCostosAutomaticos(List<EntradaGanadoCostoInfo> listaCostosAutomaticos)
        {
            listaCostosAutomaticos.ForEach(costoAutomatico =>
            {
                var tipoProrrateo = (TipoProrrateo)costoAutomatico.Costo.TipoProrrateo.TipoProrrateoID;
                switch (tipoProrrateo)
                {
                    case TipoProrrateo.Cabezas:
                        costoAutomatico.Importe = (costoAutomatico.Importe *
                                                   ContenedorCosteoEntrada.EntradaGanado.CabezasRecibidas);
                        break;
                    case TipoProrrateo.KilosLlegada:
                        var pesoNeto = ContenedorCosteoEntrada.EntradaGanado.PesoBruto -
                                       ContenedorCosteoEntrada.EntradaGanado.PesoTara;
                        costoAutomatico.Importe = (costoAutomatico.Importe * pesoNeto);
                        break;
                }
            });
        }

        /// <summary>
        /// Metodo para cargar el Costo de Ganado cuando el embarque viene de Compra Directa, Intensivo, Maquila
        /// </summary>
        private void CargarCostoGanadoDefault()
        {
            var costoPL = new CostoPL();
            var costoInfo = new CostoInfo
            {
                ClaveContable = ClaveContableCostoGanado
            };
            var costoGanadoDefault = costoPL.ObtenerPorClaveContable(costoInfo);
            var entradaGanadoCosto = new EntradaGanadoCostoInfo
            {
                Costo = costoGanadoDefault,
                EditarCuentaProveedor = true,
                EditarTieneCuenta = true,
                Proveedor = new ProveedorInfo(),
                EditarIvaRetencion = true,
                EditarNumeroDocumento = true
            };
            ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada.Add(entradaGanadoCosto);
        }

        /// <summary>
        /// Metodo para validar si los costos tienen cuenta de inventario
        /// </summary>
        private bool ValidarCuentaCostos()
        {
            bool valido = true;
            foreach (var costo in ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada)
            {
                if (costo.TieneCuenta)
                {
                    if (costo.CuentaProvision == null || costo.DescripcionCuenta == null)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          Properties.Resources.CosteoEntradaGanado_SinCuentainventario,
                                          MessageBoxButton.OK,
                                          MessageImage.Stop);
                        valido = false;
                        break;
                    }
                }
            }
            return valido;
        }

        /// <summary>
        /// Metodo que llena la información de las interfaces
        /// </summary>
        private bool CargarDatosInterface()
        {
            EntradaGanadoCosteoInfo entradaGanadoCosteoInterface;
            if (ContenedorCosteoEntrada.EntradaGanado.TipoOrigen == TipoOrganizacion.Ganadera.GetHashCode())
            {
                var interfaceSalidaTraspasoPL = new InterfaceSalidaTraspasoPL();
                entradaGanadoCosteoInterface = interfaceSalidaTraspasoPL.ObtenerDatosInterfaceSalidaTraspaso(
                    AuxConfiguracion.ObtenerOrganizacionUsuario(), ContenedorCosteoEntrada.EntradaGanado.FolioOrigen);
                ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCalidadGanado =
                    entradaGanadoCosteoInterface.ListaCalidadGanado;
                entradaGanadoCosteoInterface.ListaEntradaDetalle.ToList().ForEach(
                    det => det.ListaTiposGanado = ObtenerListaTiposGanado());
            }
            else
            {
                var interfaceSalidaPL = new InterfaceSalidaPL();
                entradaGanadoCosteoInterface =
                    interfaceSalidaPL.ObtenerPorSalidaOrganizacion(ContenedorCosteoEntrada.EntradaGanado);
            }

            if (entradaGanadoCosteoInterface == null || !entradaGanadoCosteoInterface.ListaEntradaDetalle.Any())
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                               Properties.Resources.CosteoEntradaGanado_SinDatosInterface, MessageBoxButton.OK,
                               MessageImage.Stop);
                tieneDatosInterface = false;
                return false;
            }
            ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada =
                entradaGanadoCosteoInterface.ListaCostoEntrada;
            ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaEntradaDetalle =
                entradaGanadoCosteoInterface.ListaEntradaDetalle;

            txtDiasEstancia.Text = entradaGanadoCosteoInterface.DiasEstancia.ToString(CultureInfo.InvariantCulture);
            return true;
        }

        /// <summary>
        /// Metodo que llena el combo de Tipos de Organización
        /// </summary>
        private void CargarComboTipoOrganizacion()
        {
            var tipoOrganizacionPL = new TipoOrganizacionPL();
            IEnumerable<TipoOrganizacionInfo> tiposOrganizacion = tipoOrganizacionPL.ObtenerTodos();
            cmbTipoOrigen.ItemsSource = tiposOrganizacion;
        }

        /// <summary>
        /// Metodo que regresa la Lista de Tipos de Ganado
        /// </summary>
        public List<TipoGanadoInfo> ObtenerListaTiposGanado()
        {
            var tipoGanadoPL = new TipoGanadoPL();
            var listaTiposGanado = tipoGanadoPL.ObtenerTodos();
            return listaTiposGanado;
        }

        /// <summary>
        /// Agrega la Ayuda de Cuenta
        /// </summary>
        private void AgregarAyudaCuenta(StackPanel stackPanel)
        {
            skAyudaCuenta = new SKAyuda<CuentaSAPInfo>("CuentaSAP", "Descripcion", 160, false
                                                      , "CuentaProvision", "DescripcionCuenta", 80, 10, true);
            var camposInfo = new List<String> { "CuentaSAP", "Descripcion" };
            skAyudaCuenta.AyudaPL = new CuentaSAPPL();
            skAyudaCuenta.Info = new CuentaSAPInfo
            {
                ListaTiposCuenta = new List<TipoCuentaInfo>
                {
                    new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.Provision.GetHashCode() }
                    , new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.InventarioEnTransito.GetHashCode() }
                },
                Activo = EstatusEnum.Activo
            };
            skAyudaCuenta.CamposInfo = camposInfo;

            skAyudaCuenta.MetodoPorId = "ObtenerPorFiltro";
            skAyudaCuenta.MetodoPorDescripcion = "ObtenerPorPagina";
            skAyudaCuenta.MetodoPaginadoBusqueda = "ObtenerPorPagina";

            skAyudaCuenta.MensajeClaveInexistente = Properties.Resources.Cuenta_CodigoInvalido;
            skAyudaCuenta.MensajeAgregar = Properties.Resources.Cuenta_Seleccionar;
            skAyudaCuenta.MensajeBusqueda = Properties.Resources.Cuenta_Busqueda;
            skAyudaCuenta.MensajeBusquedaCerrar = Properties.Resources.Cuenta_SalirSinSeleccionar;
            skAyudaCuenta.TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaCuenta;
            skAyudaCuenta.TituloPantalla = Properties.Resources.BusquedaCuenta_Titulo;
            skAyudaCuenta.LimpiarCampos();
            stackPanel.Children.Clear();
            stackPanel.Children.Add(skAyudaCuenta);


        }

        /// <summary>
        /// Obtiene un Renglon en base al Indice del mismo
        /// </summary>
        /// <param name="index">Indice del Renglon a Buscar</param>
        /// <returns>DataGridRow</returns>
        public DataGridRow GetRow(int index)
        {
            var row = (DataGridRow)DgCostoGanado.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                DgCostoGanado.UpdateLayout();
                DgCostoGanado.ScrollIntoView(DgCostoGanado.Items[index]);
                row = (DataGridRow)DgCostoGanado.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }

        /// <summary>
        /// Obtiene un Renglon en base al Indice del mismo
        /// </summary>
        /// <param name="row">Indice del Renglon a Buscar</param>
        /// <param name="column">Indice de la Columna a Buscar</param>
        /// <returns>DataGridCell</returns>
        public DataGridCell GetCell(int row, int column)
        {
            var rowContainer = GetRow(row);
            if (rowContainer != null)
            {
                var presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);
                if (presenter == null)
                {
                    DgCostoGanado.ScrollIntoView(rowContainer, DgCostoGanado.Columns[column]);
                    presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);
                }
                var cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                return cell;
            }
            return null;
        }
        /// <summary>
        /// Obtiene el Control Hijo del tipo que se especifica
        /// </summary>
        /// <param name="parent">Control donde se buscará el Tipo de Control especificado</param>
        /// <returns>T</returns>
        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            var child = default(T);
            var numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                var v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T ?? GetVisualChild<T>(v);
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        /// <summary>
        /// Cargar el Grid de Tipo de Ganado
        /// </summary>
        private void CargarGridTipoGanado()
        {
            DgTipoGanado.ItemsSource = null;
            var listaObservable =
                ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaEntradaDetalle.ConvertirAObservable();
            DgTipoGanado.ItemsSource = listaObservable;
            CargarCamposTotalTipoGanado();
        }

        /// <summary>
        /// Cargar el Grid de Costo
        /// </summary>
        private void CargarGridCosto()
        {
            DgCostoGanado.ItemsSource = null;
            var listaObservable = ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada.ConvertirAObservable();
            DgCostoGanado.ItemsSource = listaObservable;
        }

        /// <summary>
        /// Cargar la información del Group Box de Total de Tipo de Ganado
        /// </summary>
        private void CargarCamposTotalTipoGanado()
        {
            decimal totalPesoOrigen =
                ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaEntradaDetalle.Sum(cos => cos.PesoOrigen);
            int totalCabezas = ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaEntradaDetalle.Sum(cos => cos.Cabezas);
            decimal totalImporte = ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaEntradaDetalle.Sum(cos => cos.Importe);
            int cabezasMuertasTransito = ContenedorCosteoEntrada.EntradasGanadoMuertes.Count;

            lblPesoOrigen.Content = string.Format("{0:#,#}", totalPesoOrigen);
            lblCabezas.Content = string.Format("{0:#,#}", totalCabezas);
            lblImporteTotal.Content = string.Format("{0:#,#.00}", totalImporte);

            int diferenciaCabezas = ContenedorCosteoEntrada.EntradaGanado.CabezasRecibidas - totalCabezas;
            if (totalCabezas == 0)
            {
                lblPesoFaltante.Content = string.Format("{0:#,#}", 0);
                lblCabezasFaltantes.Content = string.Format("{0:#,#}", 0);
                lblImporteTotalFaltantes.Content = string.Format("{0:#,#.00}", 0);
            }
            else
            {
                string descripcion;
                if (diferenciaCabezas >= 0)
                {
                    descripcion = Properties.Resources.CosteoEntradaGanado_lblTotaSobrante;
                }
                else
                {
                    descripcion = Properties.Resources.CosteoEntradaGanado_lblTotaFaltante;
                }
                grpFaltante.Header = new Label
                {
                    Content = descripcion
                };
                diferenciaCabezas = Math.Abs(diferenciaCabezas);
                lblPesoFaltante.Content = string.Format("{0:#,#}", Math.Ceiling((totalPesoOrigen / totalCabezas) * diferenciaCabezas));
                lblCabezasFaltantes.Content = string.Format("{0:#,#}", diferenciaCabezas);
                lblImporteTotalFaltantes.Content = string.Format("{0:#,#.00}",
                                                                 (totalImporte / totalCabezas) * diferenciaCabezas);
            }
            if (tiposOrganizacionesAplicaCaptura.Contains(ContenedorCosteoEntrada.EntradaGanado.TipoOrigen))
            {

                var costoGanado = ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada.FirstOrDefault();
                if (costoGanado != null)
                {
                    costoGanado.Importe = totalImporte;
                }
            }
            //Se valida que sea un ruteo para prorratear los costos  
            if (ContenedorCosteoEntrada.EntradaGanado.EsRuteo &&
                ContenedorCosteoEntrada.EntradaGanado.TipoOrigen == (int)TipoOrganizacion.CompraDirecta)
            {
                var costoEmbarqueDetallePL = new CostoEmbarqueDetallePL();
                //Se obtieneEntradas que pertenecen al Embarque
                List<EntradaGanadoInfo> listaEntradasDeEmbarque =
                    costoEmbarqueDetallePL.ObtenerEntradasPorEmbarqueID(ContenedorCosteoEntrada.EntradaGanado);

                var listaCompraDirecta = listaEntradasDeEmbarque.Where(
                    entradaGanadoInfo => entradaGanadoInfo.TipoOrigen == (int)TipoOrganizacion.CompraDirecta).ToList();

                //Si es una compra directa prorratear los costos de fletes
                if (listaEntradasDeEmbarque.Count != listaCompraDirecta.Count)
                {
                    CalcularProrrateoCostoFlete(totalPesoOrigen);
                }
            }

            txtMerma.Clear();
            if (totalPesoOrigen > 0)
            {
                CalcularMerma();
            }
            ActualizarImportesGridCostos();
        }

        private void ActualizarImportesCuentas(object sender)
        {
            var comboBox = (ComboBox)sender;
            var elementoEntradaDetalle = (EntradaGanadoCostoInfo)comboBox.DataContext;
            var rowIndex = DgCostoGanado.Items.IndexOf(elementoEntradaDetalle);
            var cellCheckBox = GetCell(rowIndex, 1);
            var ckTipoCuenta = GetVisualChild<CheckBox>(cellCheckBox);
            ComisionInfo comision = new ComisionInfo();

            decimal valor = 0;
            decimal.TryParse(comboBox.Text.Replace("$", ""), out valor);

            ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada[rowIndex].Importe = valor;

        }
        /// <summary>
        /// Valida la información de las Cabeza de Ganado antes de Guardar
        /// </summary>
        private bool ValidarAntesGuardar()
        {
            if (ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaEntradaDetalle.Any(entradaDetalle =>
                entradaDetalle.PesoOrigen == 0
                || entradaDetalle.PrecioKilo == 0
                || entradaDetalle.TipoGanado == null
                || entradaDetalle.Cabezas == 0))
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.CosteoEntradaGanado_DetallesPendientes, MessageBoxButton.OK,
                                  MessageImage.Stop);
                return false;
            }

            if (ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada.Any(costo =>
                costo.Costo.CostoID == 0
                || costo.Importe == 0
                || (costo.CuentaProvision == null && (costo.Proveedor == null || costo.Proveedor.ProveedorID == 0))))
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.CosteoEntradaGanado_CostosPendietes, MessageBoxButton.OK,
                                  MessageImage.Stop);
                return false;
            }

            var costosProveedor = (from cost in ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada
                                   where cost.Proveedor != null && cost.NumeroDocumento != null
                                   group cost by new { cost.Proveedor.ProveedorID, cost.NumeroDocumento } into agrupado
                                   select new
                                   {
                                       Cantidad = agrupado.Count()
                                   });
            if (costosProveedor.Any(can => can.Cantidad > 1))
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                               Properties.Resources.CosteoEntradaGanado_FacturasRepetidas, MessageBoxButton.OK,
                               MessageImage.Stop);
                return false;
            }
            List<EntradaGanadoCostoInfo> costosRetencion =
                ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada.Where(costo => costo.Retencion).ToList();
            foreach (var retencion in costosRetencion)
            {
                var costoValidar = listaCostos.FirstOrDefault(cos => cos.CostoID.Equals(retencion.Costo.CostoID));
                if (costoValidar == null)
                {
                    continue;
                }
                if (costoValidar.Retencion == null || costoValidar.Retencion.RetencionID == 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                              string.Format(Properties.Resources.CosteoEntradaGanado_CostoSinRetencion, costoValidar.Descripcion), MessageBoxButton.OK,
                              MessageImage.Stop);
                    return false;
                }
                retencion.Costo.Retencion = costoValidar.Retencion;
            }

            var parametroOrganizacionPL = new ParametroOrganizacionPL();
            var codigoParametro = ParametrosEnum.CORRALFALTDIRECTA;
            switch ((TipoOrganizacion)ContenedorCosteoEntrada.EntradaGanado.TipoOrigen)
            {
                case TipoOrganizacion.Cadis:
                case TipoOrganizacion.Centro:
                case TipoOrganizacion.Praderas:
                    codigoParametro = ParametrosEnum.CORRALFALTPROPIO;
                    break;
            }
            ParametroOrganizacionInfo parametroOrganizacion =
                parametroOrganizacionPL.ObtenerPorOrganizacionIDClaveParametro(organizacionIdLogin,
                                                                               codigoParametro.ToString());
            if (parametroOrganizacion == null)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.CosteoEntradaGanado_ConfiguracionCorralesTransito, MessageBoxButton.OK,
                                  MessageImage.Stop);
                return false;
            }
            var entradaGanadoTransitoBL = new EntradaGanadoTransitoBL();
            EntradaGanadoTransitoInfo entradaGanadoTransito =
                entradaGanadoTransitoBL.ObtenerPorCorralOrganizacion(parametroOrganizacion.Valor,
                                                                     organizacionIdLogin);
            int totalCabezasTipoGanado =
                ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaEntradaDetalle.Sum(det => det.Cabezas);
            int diferenciaCabezas = ContenedorCosteoEntrada.EntradaGanado.CabezasRecibidas - totalCabezasTipoGanado;
            int muertesTransito = ContenedorCosteoEntrada.EntradasGanadoMuertes.Count;
            if (diferenciaCabezas != 0 || muertesTransito != 0)
            {
                bool validarTransito = Math.Abs(diferenciaCabezas) - muertesTransito != 0;
                if (entradaGanadoTransito != null && validarTransito)
                {
                    if (diferenciaCabezas > entradaGanadoTransito.Cabezas)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          Properties.Resources.CosteoEntradaGanado_NoExisteSuficienteInventarioTransito,
                                          MessageBoxButton.OK,
                                          MessageImage.Stop);
                        return false;
                    }
                }
                if (diferenciaCabezas != 0 || muertesTransito != 0)
                {
                    if (entradaGanadoTransito == null)
                    {
                        ContenedorCosteoEntrada.EntradaGanadoTransito = new EntradaGanadoTransitoInfo
                        {
                            Lote = new LoteInfo()
                        };
                        var corral = new CorralInfo
                        {
                            Codigo = parametroOrganizacion.Valor,
                            Organizacion = new OrganizacionInfo
                            {
                                OrganizacionID = organizacionIdLogin
                            },
                            Activo = EstatusEnum.Activo
                        };
                        var corraPL = new CorralPL();
                        ContenedorCosteoEntrada.EntradaGanadoTransito.Lote.Corral =
                            corraPL.ObtenerPorCodicoOrganizacionCorral(corral);
                    }
                    else
                    {
                        ContenedorCosteoEntrada.EntradaGanadoTransito = entradaGanadoTransito;
                    }
                    ContenedorCosteoEntrada.EntradaGanadoTransito.Sobrante = diferenciaCabezas >= 0;
                    diferenciaCabezas = Math.Abs(diferenciaCabezas);
                    int cabezasTransito = 0;
                    if (diferenciaCabezas != 0)
                    {
                        if (diferenciaCabezas == muertesTransito)
                        {
                            cabezasTransito = diferenciaCabezas;
                        }
                        else
                        {
                            cabezasTransito = diferenciaCabezas - muertesTransito;
                        }
                    }
                    ContenedorCosteoEntrada.EntradaGanadoTransito.Cabezas = cabezasTransito;
                    if (cabezasTransito != 0)
                    {
                        CalcularDescuentoFaltanteSobrante(totalCabezasTipoGanado,
                                                          ContenedorCosteoEntrada.EntradaGanadoTransito.Cabezas);
                        int entradaGanadoTransitoID = ContenedorCosteoEntrada.EntradaGanadoTransito.EntradaGanadoTransitoID;
                        ContenedorCosteoEntrada.EntradaGanadoTransito.EntradasGanadoTransitoDetalles
                                               .ForEach(id => id.EntradaGanadoTransitoID = entradaGanadoTransitoID);
                        ContenedorCosteoEntrada.EntradaGanadoTransito.UsuarioModificacionID = usuarioLogueadoID;
                        ContenedorCosteoEntrada.EntradaGanadoTransito.Activo = EstatusEnum.Activo;
                    }
                    totalCabezasTipoGanado =
                        ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaEntradaDetalle.Sum(det => det.Cabezas);
                    CalcularDescuentoMuerteTransito(totalCabezasTipoGanado);
                    ContenedorCosteoEntrada.EntradasGanadoMuertes.ForEach(
                        id => id.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado());
                    ContenedorCosteoEntrada.EntradaGanadoTransito.UsuarioCreacionID =
                        AuxConfiguracion.ObtenerUsuarioLogueado();
                }
            }
            return true;
        }

        private void CalcularDescuentoMuerteTransito(int totalCabezasTipoGanado)
        {
            decimal importeDescontar = 0;
            decimal importeTransito = 0;

            EntradaGanadoMuerteDetalleInfo entradaGanadoMuerteDetalle;
            EntradaGanadoMuerteInfo entradaGanadoMuerte = null;

            decimal totalPesoOrigen = ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaEntradaDetalle.Sum(cos => cos.PesoOrigen);

            int cabezasMuertas = ContenedorCosteoEntrada.EntradasGanadoMuertes.Count;
            decimal pesoTransito = 0;
            decimal pesoDescontar = 0;
            for (var muertes = 0; muertes < cabezasMuertas; muertes++)
            {
                entradaGanadoMuerte = ContenedorCosteoEntrada.EntradasGanadoMuertes[muertes];
                if (entradaGanadoMuerte.EntradaGanadMuerteDetalle == null)
                {
                    entradaGanadoMuerte.EntradaGanadMuerteDetalle = new List<EntradaGanadoMuerteDetalleInfo>();
                }

                pesoTransito = entradaGanadoMuerte.Peso;
                pesoDescontar = Math.Ceiling(((totalPesoOrigen - pesoTransito) / totalCabezasTipoGanado));

                List<EntradaGanadoCostoInfo> costos = ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada;
                entradaGanadoMuerte.Peso = pesoDescontar;
                if (costos != null)
                {
                    bool validarTransito = ContenedorCosteoEntrada.EntradaGanadoTransito != null;
                    EntradaGanadoCostoInfo costo;
                    for (int indexCostos = 0; indexCostos < costos.Count; indexCostos++)
                    {
                        costo = costos[indexCostos];
                        if (validarTransito)
                        {
                            importeTransito = ContenedorCosteoEntrada.EntradaGanadoTransito
                                                                     .EntradasGanadoTransitoDetalles
                                                                     .Where(id => id.Costo.CostoID == costo.Costo.CostoID)
                                                                     .Sum(imp => imp.Importe);
                            if (importeTransito > 0)
                            {
                                continue;
                            }
                        }
                        importeDescontar = Math.Round(((costo.Importe - importeTransito) / totalCabezasTipoGanado), 2);
                        entradaGanadoMuerteDetalle = new EntradaGanadoMuerteDetalleInfo
                        {
                            Importe = importeDescontar,
                            Costo = new CostoInfo
                            {
                                CostoID = costos[indexCostos].Costo.CostoID
                            },
                            EntradaGanadoMuerte = new EntradaGanadoMuerteInfo
                            {
                                EntradaGanadoMuerteID = entradaGanadoMuerte.EntradaGanadoMuerteID
                            },
                            UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado()
                        };
                        entradaGanadoMuerte.EntradaGanadMuerteDetalle.Add(entradaGanadoMuerteDetalle);
                    }
                }
            }
        }

        private void CalcularDescuentoFaltanteSobrante(int totalCabezasTipoGanado, int diferenciaCabezas)
        {
            List<EntradaGanadoCostoInfo> costos = ContenedorCosteoEntrada.EntradaGanadoCosteo
                                                                         .ListaCostoEntrada
                                                                         .OrderBy(id => id.Costo.CostoID).ToList();
            if (costos != null)
            {
                decimal importeDescontar = 0;

                EntradaGanadoCostoInfo costo;
                bool sobrante = ContenedorCosteoEntrada.EntradaGanadoTransito.Sobrante;
                EntradaGanadoTransitoDetalleInfo entradaGanadoTransitoDetalle;
                for (int indexCosto = 0; indexCosto < costos.Count; indexCosto++)
                {
                    costo = costos[indexCosto];

                    entradaGanadoTransitoDetalle = ContenedorCosteoEntrada.EntradaGanadoTransito
                                                .EntradasGanadoTransitoDetalles
                                                .FirstOrDefault(id => id.Costo.CostoID == costo.Costo.CostoID);
                    if (entradaGanadoTransitoDetalle == null && sobrante)
                    {
                        continue;
                    }
                    if (entradaGanadoTransitoDetalle == null && !sobrante)
                    {
                        entradaGanadoTransitoDetalle = new EntradaGanadoTransitoDetalleInfo
                        {
                            EntradaGanadoTransitoID = ContenedorCosteoEntrada.EntradaGanadoTransito.EntradaGanadoTransitoID
                        };
                        if (ContenedorCosteoEntrada.EntradaGanadoTransito.EntradasGanadoTransitoDetalles == null)
                        {
                            ContenedorCosteoEntrada.EntradaGanadoTransito
                                                   .EntradasGanadoTransitoDetalles = new List<EntradaGanadoTransitoDetalleInfo>();
                        }
                        ContenedorCosteoEntrada.EntradaGanadoTransito.EntradasGanadoTransitoDetalles.Add(entradaGanadoTransitoDetalle);
                    }
                    importeDescontar = Math.Round((costo.Importe / totalCabezasTipoGanado) * Math.Abs(diferenciaCabezas), 2);
                    entradaGanadoTransitoDetalle.Costo = new CostoInfo
                    {
                        CostoID = costo.Costo.CostoID
                    };
                    entradaGanadoTransitoDetalle.Importe = importeDescontar;

                    if (costo.Costo.CostoID == Costo.CostoGanado.GetHashCode())
                    {
                        decimal totalPesoOrigen = ContenedorCosteoEntrada.EntradaGanadoCosteo
                                                                         .ListaEntradaDetalle.Sum(cos => cos.PesoOrigen);
                        decimal pesoDescontar = Math.Ceiling((totalPesoOrigen / totalCabezasTipoGanado)
                                                * Math.Abs(diferenciaCabezas));

                        ContenedorCosteoEntrada.EntradaGanadoTransito.Peso = Convert.ToInt32(pesoDescontar);

                        ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaEntradaDetalle.ForEach(
                            peso => peso.PesoLlegada = peso.PesoOrigen);
                        ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaEntradaDetalle.ForEach(
                            peso => peso.ImporteOrigen = peso.Importe);

                        EntradaDetalleInfo entradaDetalle = null;

                        if (sobrante)
                        {
                            entradaDetalle =
                                ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaEntradaDetalle.FirstOrDefault();
                        }
                        else
                        {
                            entradaDetalle =
                                ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaEntradaDetalle.Where(peso => peso.PesoOrigen > pesoDescontar && peso.Importe > importeDescontar).OrderByDescending(
                                peso => peso.PesoOrigen).ThenByDescending(peso => peso.Importe).First();    
                        }

                        
                        if (entradaDetalle != null)
                        {
                            if (sobrante)
                            {
                                entradaDetalle.PesoOrigen += pesoDescontar;
                                entradaDetalle.Cabezas += diferenciaCabezas;
                                entradaDetalle.Importe += importeDescontar;
                                entradaDetalle.PrecioKilo = Math.Round(entradaDetalle.Importe / entradaDetalle.PesoOrigen, 2);
                            }
                            else
                            {
                                entradaDetalle.PesoOrigen -= pesoDescontar;
                                entradaDetalle.Cabezas -= diferenciaCabezas;
                                entradaDetalle.Importe -= importeDescontar;
                                entradaDetalle.PrecioKilo = Math.Round(entradaDetalle.Importe / entradaDetalle.PesoOrigen, 2);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Guarda el Costeo Entrada Ganado
        /// </summary>
        private void Guardar()
        {
            if (!ValidarAntesGuardar())
            {
                return;
            }
            CargarEntidadOrganizacion();
            ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada.ToList().ForEach(cost =>
            {
                if (cost.TieneCuenta)
                {
                    cost.Proveedor = null;
                }
                else
                {
                    cost.CuentaProvision = null;
                }
            });
            ContenedorCosteoEntrada.EntradaGanadoCosteo.EntradaGanadoID =
                ContenedorCosteoEntrada.EntradaGanado.EntradaGanadoID;
            ContenedorCosteoEntrada.EntradaGanadoCosteo.Activo = EstatusEnum.Activo;
            ContenedorCosteoEntrada.EntradaGanadoCosteo.UsuarioCreacionID = usuarioLogueadoID;
            ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCalidadGanado.ToList().ForEach(cali =>
            {
                cali.UsuarioCreacionID = usuarioLogueadoID;
                cali.Activo = EstatusEnum.Activo;
            });
            ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada.ToList().ForEach(costo =>
            {
                costo.UsuarioCreacionID = usuarioLogueadoID;
                costo.Activo = EstatusEnum.Activo;
            });
            ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaEntradaDetalle.ToList().ForEach(deta =>
            {
                deta.UsuarioCreacionID = usuarioLogueadoID;
                deta.Activo = EstatusEnum.Activo;
            });

            ContenedorCosteoEntrada.EntradaGanado.UsuarioModificacionID = usuarioLogueadoID;
            ContenedorCosteoEntrada.EntradaGanado.Costeado = true;

            var entradaGanadoCosteoPL = new EntradaGanadoCosteoPL();
            MemoryStream stream = entradaGanadoCosteoPL.Guardar(ContenedorCosteoEntrada);
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                             Properties.Resources.CosteoEntradaGanado_GuardadoExitoso, MessageBoxButton.OK,
                             MessageImage.Correct);
            if (stream != null)
            {
                var exportarPoliza = new ExportarPoliza();
                exportarPoliza.ImprimirPoliza(stream,
                                              string.Format("{0} {1}", "Poliza de Entrada ",
                                                            ContenedorCosteoEntrada.EntradaGanado.FolioEntrada));
            }
            LimpiarControles();
        }

        /// <summary>
        /// Cargar la información de la Organización de Entrada Ganado Costeo
        /// </summary>
        private void CargarEntidadOrganizacion()
        {
            ContenedorCosteoEntrada.EntradaGanadoCosteo.Organizacion = new OrganizacionInfo
            {
                OrganizacionID = organizacionIdLogin
            };
        }

        /// <summary>
        /// Cargar la información de la Calidad de Ganado cuando es una nueva Entrada Ganado Costeo
        /// </summary>
        private void CargarListaCalidad()
        {
            var calidades = new List<EntradaGanadoCalidadInfo>();
            if (ContenedorCosteoEntrada.EntradaGanado != null
                && ContenedorCosteoEntrada.EntradaGanado.TipoOrigen == TipoOrganizacion.Ganadera.GetHashCode())
            {
                ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCalidadGanado.ForEach(calidades.Add);
            }
            var calidadGanadoPL = new CalidadGanadoPL();
            ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCalidadGanado = calidadGanadoPL.ObtenerListaCalidadGanado();
            if (ContenedorCosteoEntrada.EntradaGanado != null
                && ContenedorCosteoEntrada.EntradaGanado.TipoOrigen == TipoOrganizacion.Ganadera.GetHashCode())
            {
                EntradaGanadoCalidadInfo entradaGanadoCalidad;
                ContenedorCosteoEntrada.EntradaGanadoCosteo
                                       .ListaCalidadGanado
                                       .ForEach(dato =>
                                       {
                                           entradaGanadoCalidad =
                                               calidades.FirstOrDefault(id =>
                                                                        id.CalidadGanado.CalidadGanadoID ==
                                                                        dato.CalidadGanado.CalidadGanadoID);
                                           if (entradaGanadoCalidad != null)
                                           {
                                               dato.Valor =
                                                   entradaGanadoCalidad
                                                       .Valor;
                                           }
                                       });
            }
        }

        /// <summary>
        /// Metodo que crea los controles de Calidad de Ganado
        /// </summary>
        private void CargarControlesCalidadGanado()
        {
            var calidadMacho = ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCalidadGanado.Where(n => n.CalidadGanado.Sexo == Sexo.Macho);
            var consecutivoMacho = 0;
            var consecutivoHembra = 0;
            stpCalidadMacho.Children.Clear();
            stpCalidadHembra.Children.Clear();

            foreach (var calidadGanadoEntradaInfo in calidadMacho)
            {
                var control = new IntegerUpDown
                {
                    Name = string.Format("iud{0}{1}", consecutivoMacho++, (Sexo.Macho)),
                    Width = 45,
                    Height = 20,
                    ShowButtonSpinner = false,
                    BorderThickness = new Thickness(1.5),
                    BorderBrush = new SolidColorBrush(Colors.Gray),
                };
                var bind = new Binding("Valor")
                {
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                    Source = calidadGanadoEntradaInfo
                };
                control.SetBinding(IntegerUpDown.ValueProperty, bind);
                var margin = control.Margin;
                margin.Right = 10;
                control.Margin = margin;
                control.KeyDown += IntegerCalidad_KeyDown;
                control.LostFocus += dudIntegerCalidad_LostFocus;
                control.AllowSpin = false;
                control.MaxLength = 3;
                control.IsEnabled = false;
                stpCalidadMacho.Children.Add(control);
            }

            var calidadHembra = ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCalidadGanado.Where(hem => hem.CalidadGanado.Sexo == Sexo.Hembra);
            foreach (var calidadGanadoEntradaInfo in calidadHembra)
            {
                var control = new IntegerUpDown
                {
                    Name = string.Format("iud{0}{1}", consecutivoHembra++, (Sexo.Hembra)),
                    Width = 45,
                    Height = 20,
                    ShowButtonSpinner = false,
                    BorderThickness = new Thickness(1.5),
                    BorderBrush = new SolidColorBrush(Colors.Gray),
                };
                var bind = new Binding("Valor")
                {
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                    Source = calidadGanadoEntradaInfo
                };
                control.SetBinding(IntegerUpDown.ValueProperty, bind);
                var margin = control.Margin;
                margin.Right = 10;
                control.Margin = margin;
                control.KeyDown += IntegerCalidad_KeyDown;
                control.LostFocus += dudIntegerCalidad_LostFocus;
                control.AllowSpin = false;
                control.MaxLength = 3;
                control.IsEnabled = false;
                stpCalidadHembra.Children.Add(control);
            }
        }

        /// <summary>
        /// Metodo para cancelar un Entrada Ganado Costeo
        /// </summary>
        private void CancelarEntradaGanadoCosteo()
        {
            ContenedorCosteoEntrada.EntradaGanadoCosteo.Activo = EstatusEnum.Inactivo;
            ContenedorCosteoEntrada.EntradaGanadoCosteo.UsuarioModificacionID = usuarioLogueadoID;
            ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaEntradaDetalle.ForEach(det =>
            {
                det.Activo = EstatusEnum.Inactivo;
                det.UsuarioModificacionID = usuarioLogueadoID;
            });
            ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada.ForEach(cost =>
            {
                cost.Activo = EstatusEnum.Inactivo;
                cost.UsuarioModificacionID = usuarioLogueadoID;
            });
            ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCalidadGanado.ForEach(cali =>
            {
                if (cali.EntradaGanadoCalidadID != 0)
                {
                    cali.Activo = EstatusEnum.Inactivo;
                    cali.UsuarioModificacionID = usuarioLogueadoID;
                }
            });
            var entradaGanadoCosteoPL = new EntradaGanadoCosteoPL();
            entradaGanadoCosteoPL.Guardar(ContenedorCosteoEntrada);
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                               Properties.Resources.CosteoEntradaGanado_CanceladoExitoso, MessageBoxButton.OK,
                               MessageImage.Correct);
            LimpiarControles();
        }

        /// <summary>
        /// Metodo para dejar utilizable la pantalla de nuevo
        /// </summary>
        private void LimpiarControles()
        {
            DataContext = null;
            txtMerma.Clear();
            txtPesoLlegada.Clear();
            skAyudaOrganizacion.LimpiarCampos();
            skAyudaFolio.LimpiarCampos();
            DgCostoGanado.ItemsSource = null;
            DgTipoGanado.ItemsSource = null;
            skAyudaFolio.IsEnabled = true;
            stpCalidadMacho.Children.Clear();
            stpCalidadHembra.Children.Clear();
            stpCalidadMacho.IsEnabled = true;
            stpCalidadHembra.IsEnabled = true;
            txtObservaciones.IsEnabled = true;
            lblPesoOrigen.Content = "0";
            lblImporteTotal.Content = "0";
            lblCabezas.Content = "0";

            lblPesoFaltante.Content = "0";
            lblImporteTotalFaltantes.Content = "0";
            lblCabezasFaltantes.Content = "0";

            lblCabezasMuertas.Content = "0";
            txtDiasEstancia.Clear();
            CargarHorasDefault();
            dpFechaRecepcion.SelectedDate = null;
            dpFechaSalida.SelectedDate = null;
            BloquearBotones();
            scroll.ScrollToVerticalOffset(0);
            CargarListaCalidad();
            CargarControlesCalidadGanado();
            skAyudaFolio.AsignarFoco();
            lblCabezasCalidad.Content = "0";
        }

        /// <summary>
        /// Metodo para Calcular el Porcentaje de Merma
        /// </summary>
        private void CalcularMerma()
        {
            //( (Peso Origen Total – Peso Llegada ) / Peso Origen Total )* 100
            var pesoOrigenTotal =
                ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaEntradaDetalle.Sum(det => det.PesoOrigen);
            var pesoLlegada = ContenedorCosteoEntrada.EntradaGanado.PesoBruto -
                              ContenedorCosteoEntrada.EntradaGanado.PesoTara;

            var porcentajeMerma = Math.Round(((pesoOrigenTotal - pesoLlegada) / pesoOrigenTotal) * 100, 2);
            txtMerma.Text = porcentajeMerma.ToString(CultureInfo.InvariantCulture);
        }

        #endregion METODOS

        #region EVENTOS


        /// <summary>
        /// Evento que se ejecuta cuando se presiona una tecla en el Control de Importe
        /// </summary>
        private void Decimal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                var keyboardFocus = Keyboard.FocusedElement as UIElement;

                if (keyboardFocus != null)
                {
                    keyboardFocus.MoveFocus(tRequest);
                }
            }
            var control = (DecimalUpDown)sender;
            string valorControl = control.Text ?? string.Empty;

            if (e.Key == Key.Decimal && valorControl.IndexOf('.') > 0)
            {
                e.Handled = true;
            }
            else if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.Decimal || e.Key == Key.OemPeriod)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se presiona una tecla en el Control de Importe
        /// </summary>
        private void Integer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                var keyboardFocus = Keyboard.FocusedElement as UIElement;

                if (keyboardFocus != null)
                {
                    keyboardFocus.MoveFocus(tRequest);
                }
            }
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.Decimal)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se le da Clic al botón Guardar
        /// </summary>
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal]
                                    , Properties.Resources.CosteoEntradaGanado_Guardar
                                    , MessageBoxButton.YesNo, MessageImage.Warning)
                    != MessageBoxResult.Yes)
                {
                    return;
                }
                Guardar();
            }
            catch (ExcepcionServicio ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  ex.Message, MessageBoxButton.OK, MessageImage.Stop);
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando carga la Funcionalidad
        /// </summary>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCalidadGanado.Any())
            {
                return;
            }
            CargarHorasDefault();
            CargarComboTipoOrganizacion();
            BloquearBotones();
            CargarListaCalidad();
            CargarControlesCalidadGanado();
            skAyudaFolio.AsignarFoco();
            CargarListaCostos();
            CargarListaTipoCosto();
        }
        /// <summary>
        /// Evento que se ejecuta cuando se le da Clic al botón Agregar para Tipo de Ganado
        /// </summary>
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var entradaDetalle = new EntradaDetalleInfo
                {
                    ListaTiposGanado = ObtenerListaTiposGanado(),
                    Editar = true
                };
                ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaEntradaDetalle.Add(entradaDetalle);
                CargarGridTipoGanado();
                btnCancelarTipoGanado.IsEnabled = true;
                DgTipoGanado.ScrollIntoView(entradaDetalle);
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se le da Clic al botón Cancelar para Tipo de Ganado
        /// </summary>
        private void btnCancelarTipoGanado_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var elementoBorrar = ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaEntradaDetalle.LastOrDefault();
                if (elementoBorrar == null)
                {
                    return;
                }
                ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaEntradaDetalle.Remove(elementoBorrar);
                CargarGridTipoGanado();
                btnCancelarTipoGanado.IsEnabled = false;
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se le da Clic al botón Eliminar del Grid de Tipo de Ganado
        /// </summary>
        private void btnEliminarTipoGanado_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var btn = (Button)e.Source;
                var elementoBorrar = (EntradaDetalleInfo)btn.CommandParameter;
                ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaEntradaDetalle.Remove(elementoBorrar);
                CargarGridTipoGanado();
                btnCancelarTipoGanado.IsEnabled = false;
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se le da Clic al botón Agregar del Costo de Entrada
        /// </summary>
        private void btnAgregarCostoGanado_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var entradaCosto = new EntradaGanadoCostoInfo
                {
                    Costo = new CostoInfo(),
                    Proveedor = new ProveedorInfo(),
                    EditarNumeroDocumento = true,
                    CostoEmbarque = true,
                    EditarTieneCuenta = true,
                    EditarCuentaProveedor = true,
                    EditarIvaRetencion = true
                };
                ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada.Add(entradaCosto);
                CargarGridCosto();
                btnCancelarCostoGanado.IsEnabled = true;

                ActualizarImportesGridCostos();

                var rowIndex = DgCostoGanado.Items.Count - 1;
                //var cell = GetCell(rowIndex, 2);
                var cellCombo = GetCell(rowIndex, 4);
                var combo = GetVisualChild<ComboBox>(cellCombo);
                DgCostoGanado.ScrollIntoView(entradaCosto);
                combo.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se le da Clic al botón Cancelar del Costo de Entrada
        /// </summary>
        private void btnCancelarCostoGanado_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var elementoBorrar = ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada.LastOrDefault();
                if (elementoBorrar == null)
                {
                    return;
                }
                ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada.Remove(elementoBorrar);
                CargarGridCosto();
                ActualizarImportesGridCostos();
                btnCancelarCostoGanado.IsEnabled = false;
                if (ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada != null && ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada.Count > 0)
                    DgCostoGanado.ScrollIntoView(ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada[ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada.Count - 1]);
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se le da Clic al botón Eliminar del Grid Costo de Entrada
        /// </summary>
        private void btnEliminarCostoGanado_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var btn = (Button)e.Source;
                var elementoBorrar = (EntradaGanadoCostoInfo)btn.CommandParameter;
                ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada.Remove(elementoBorrar);
                CargarGridCosto();
                ActualizarImportesGridCostos();
                btnCancelarCostoGanado.IsEnabled = false;
                if (ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada != null && ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada.Count > 0)
                    DgCostoGanado.ScrollIntoView(ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada[ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada.Count - 1]);
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando cambia el valor del Peso de Origen
        /// </summary>
        private void dudPesoOrigen_ValueChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DgTipoGanado.CurrentCell.Item == DependencyProperty.UnsetValue)
                {
                    return;
                }
                var elemento = (EntradaDetalleInfo)DgTipoGanado.CurrentCell.Item;
                elemento.Importe = elemento.PesoOrigen * elemento.PrecioKilo;
                CargarCamposTotalTipoGanado();
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando cambia el valor del Precio de Kilo
        /// </summary>
        private void iudCabezas_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DgTipoGanado.CurrentCell.Item == DependencyProperty.UnsetValue)
                {
                    return;
                }

                var elemento = (EntradaDetalleInfo)DgTipoGanado.CurrentCell.Item;

                elemento.Importe = elemento.PesoOrigen * elemento.PrecioKilo;
                CargarCamposTotalTipoGanado();
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando cambia el valor del Precio de Kilo
        /// </summary>
        private void dudPrecioKilo_ValueChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DgTipoGanado.CurrentCell.Item == DependencyProperty.UnsetValue)
                {
                    return;
                }
                var elemento = (EntradaDetalleInfo)DgTipoGanado.CurrentCell.Item;
                elemento.Importe = elemento.PesoOrigen * elemento.PrecioKilo;
                CargarCamposTotalTipoGanado();
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando el Control es creado
        /// </summary>
        private void stpAyudaCosto_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var stackPanel = (StackPanel)sender;

                if (stackPanel.Children.Count == 0)
                {
                    AgregarAyudaCosto(stackPanel);
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando el Control es creado
        /// </summary>
        private void stpAyudaProveedorCuenta_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var stackPanel = (StackPanel)sender;
                if (stackPanel.Children.Count == 0)
                {
                    AgregarAyudaProveedor(stackPanel);
                    panelNuevoCosto = stackPanel;
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando el Valor del CheckBox cambia
        /// </summary>
        private void chkCuenta_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                var checkBox = (CheckBox)sender;

                if (!checkBox.IsChecked.HasValue)
                {
                    return;
                }
                var costo = (EntradaGanadoCostoInfo)checkBox.CommandParameter;
                var rowIndex = DgCostoGanado.Items.IndexOf(costo);
                var cell = GetCell(rowIndex, 2);

                var cellCombo = GetCell(rowIndex, 4);
                var combo = GetVisualChild<ComboBox>(cellCombo);
                var stackPanel = GetVisualChild<StackPanel>(cell);
                if (costo.TieneCuenta)
                {
                    costo.EditarIvaRetencion = false;
                    costo.Iva = false;
                    costo.Retencion = false;
                    combo.IsEditable = true;
                    AgregarAyudaCuenta(stackPanel);

                }
                else
                {
                    combo.IsEditable = false;
                    costo.EditarIvaRetencion = true;
                    costo.Iva = false;
                    costo.Retencion = false;
                    costo.Proveedor = null;
                    AgregarAyudaProveedor(stackPanel);
                    var comisiones = new List<ComisionInfo>();
                    comisiones.Add(new ComisionInfo
                    {
                        Tarifa = 0,
                        TipoComisionID = 0
                    });
                    combo.ItemsSource = null;
                    combo.ItemsSource = comisiones;
                    combo.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se le da Clic al botón Cancelar
        /// </summary>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.CosteoEntradaGanado_Cancelar,
                                      MessageBoxButton.YesNo, MessageImage.Warning) != MessageBoxResult.Yes)
                {
                    return;
                }
                CancelarEntradaGanadoCosteo();
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se le da Clic al botón Cancelar
        /// </summary>
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LimpiarControles();
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }



        private void cmbComisiones_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab || e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.OemPeriod)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void cmbComisiones_onLostFocus(object sender, RoutedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var elementoEntradaDetalle = (EntradaGanadoCostoInfo)comboBox.DataContext;
            var rowIndex = DgCostoGanado.Items.IndexOf(elementoEntradaDetalle);
            decimal valor = 0;
            decimal.TryParse(comboBox.Text, out valor);

            if (ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada[rowIndex].Costo.CostoID != Costo.Comision.GetHashCode())
            {
                if (valor > 0)
                {
                    List<ComisionInfo> comisiones = new List<ComisionInfo>();
                    comisiones.Add(new ComisionInfo() { ProveedorComisionID = 0, Tarifa = valor });
                    comboBox.ItemsSource = comisiones;
                    comboBox.SelectedIndex = 0;
                }
                comboBox.Text = formatearImporte(valor.ToString());
                ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada[rowIndex].Costo.ImporteCosto = valor;
                elementoEntradaDetalle.Importe = valor;
                comboBox.UpdateLayout();
            }
        }

        private void cmbComisiones_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                var combo = (ComboBox)sender;
                var costo = (EntradaGanadoCostoInfo)combo.DataContext;
                var importe = formatearImporte(combo.Text);
                decimal valor = decimal.Parse(importe.Replace("$", ""));
                ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada[DgCostoGanado.SelectedIndex].Importe = valor;

            }
            ActualizarImportesCuentas(sender);
        }


        private void cboComisiones_onSelectionChanged(object sender, RoutedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var elementoEntradaDetalle = (EntradaGanadoCostoInfo)comboBox.DataContext;
            var rowIndex = DgCostoGanado.Items.IndexOf(elementoEntradaDetalle);

            var cellCheckBox = GetCell(rowIndex, 1);
            var ckTipoCuenta = GetVisualChild<CheckBox>(cellCheckBox);
            ComisionInfo comision = new ComisionInfo();

            if (comboBox.IsEnabled && (ckTipoCuenta.IsChecked != null && ckTipoCuenta.IsChecked != true))
            {
                if (comboBox.Items.Count > 0)
                {
                    //ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada[rowIndex].Importe = decimal.Parse(comboBox.Text.Replace("$", ""));
                    if (comboBox.SelectedValue != null)
                    {
                        ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada[rowIndex].ProveedorComisionID = int.Parse(comboBox.SelectedValue.ToString());
                        foreach (ComisionInfo costo in comboBox.Items)
                        {
                            if (int.Parse(comboBox.SelectedValue.ToString()) == costo.ProveedorComisionID)
                                ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada[rowIndex].Importe = costo.Tarifa;
                        }
                    }
                    else
                    {
                        ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada[rowIndex].ProveedorComisionID = 0;
                    }
                }
                else if (comboBox.Items.Count == 0)
                {
                    comboBox.SelectedIndex = -1;
                }
            }


        }

        /// <summary>
        /// Evento que se ejecuta cuando se selecciona el Tipo de Ganado
        /// </summary>
        private void cmbTipoGanado_SelectionChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                var comboBox = (ComboBox)sender;
                var elementoEntradaDetalle = (EntradaDetalleInfo)comboBox.DataContext;

                int contieneTipo =
                    ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaEntradaDetalle.Count(
                        desc =>
                        desc.TipoGanado != null &&
                        desc.TipoGanado.TipoGanadoID.Equals(elementoEntradaDetalle.TipoGanado.TipoGanadoID));
                if (contieneTipo > 1)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.CostroEntradaGanado_TipoGanadoExistente,
                                      MessageBoxButton.OK, MessageImage.Stop);
                    var detalle = DgTipoGanado.SelectedValue as EntradaDetalleInfo;
                    ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaEntradaDetalle.Remove(detalle);
                    CargarGridTipoGanado();
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      ex.Message,
                                      MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se selecciona el Tipo de Ganado
        /// </summary>
        private void dudIntegerCalidad_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                var control = (IntegerUpDown)sender;
                if (ContenedorCosteoEntrada.EntradaGanado == null)
                {

                    control.Value = 0;
                    return;
                }
                var totalCabezasCalidad =
                    ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCalidadGanado.Sum(calidad => calidad.Valor);
                var cabezasRecibidas = ContenedorCosteoEntrada.EntradaGanado.CabezasRecibidas;
                if (totalCabezasCalidad > cabezasRecibidas)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.CosteoEntradaGanado_CabezasMayor,
                                      MessageBoxButton.OK, MessageImage.Stop);
                    control.Value = 0;
                }

                totalCabezasCalidad = ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCalidadGanado.Sum(calidad => calidad.Valor);
                lblCabezasCalidad.Content = totalCabezasCalidad;

            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta cuando se presiona una tecla en el Control de Importe
        /// </summary>
        private void IntegerCalidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                var keyboardFocus = Keyboard.FocusedElement as UIElement;

                if (keyboardFocus != null)
                {
                    keyboardFocus.MoveFocus(tRequest);
                }
            }

            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.Decimal)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        private void CambiarFocusSiguienteControl(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                var keyboardFocus = Keyboard.FocusedElement as UIElement;

                if (keyboardFocus != null)
                {
                    keyboardFocus.MoveFocus(tRequest);
                }
            }
        }

        #endregion EVENTOS
    }
}
