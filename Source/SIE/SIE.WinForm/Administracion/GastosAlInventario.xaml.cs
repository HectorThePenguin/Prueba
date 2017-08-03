using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using Costo = SIE.Services.Info.Enums.Costo;
using TipoCorral = SIE.Services.Info.Enums.TipoCorral;
using TipoCuenta = SIE.Services.Info.Enums.TipoCuenta;
using TipoOrganizacion = SIE.Services.Info.Enums.TipoOrganizacion;

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para GastosAlInventario.xaml
    /// </summary>
    public partial class GastosAlInventario
    {
        #region Atributos
        private SKAyuda<OrganizacionInfo> skAyudaOrganizacion;
        private SKAyuda<CorralInfo> skAyudaCorral;
        private SKAyuda<CostoInfo> skAyudaCostos;
        private SKAyuda<CuentaSAPInfo> skAyudaCuenta;
        private SKAyuda<ProveedorInfo> skAyudaProveedor;
        private OrganizacionInfo organizacionSeleccionada;
        private CorralInfo corralSeleccionado;
        private CuentaSAPInfo cuentaSeleccionada;
        private ProveedorInfo proveedorSeleccionado;
        private CostoInfo costoSeleccionado;
        private CostoInfo gastoSeleccionado;
        private TipoCostoInfo movimientoSeleccionado;
        private string opcionSeleccionada;

        #endregion

        #region Metodos
        /// <summary>
        /// Constructor
        /// </summary>
        public GastosAlInventario()
        {
            corralSeleccionado = null;
            InitializeComponent();

            InicializarDatosUsuario();
        }

        /// <summary>
        /// Carga la informacion del combo de gastos
        /// </summary>
        private void CargarGastos()
        {
            try
            {
                var costoPL = new CostoPL();
                var listaCostoTipoGasto = costoPL.ObtenerPorTipoGasto();
                var listaFinal = new List<CostoInfo>();

                var configuracionParametrosPL = new ConfiguracionParametrosPL();

                var configuracionParametro = new ConfiguracionParametrosInfo
                {
                    TipoParametro = TiposParametrosEnum.GastosAlInventario.GetHashCode(),
                    OrganizacionID = organizacionSeleccionada.OrganizacionID
                };
                var seleccione = new CostoInfo
                {
                    Activo = EstatusEnum.Activo,
                    Descripcion = Properties.Resources.GastosAlInventario_ElementoSeleccione,
                };
                listaFinal.Add(seleccione);

                var listaParametros = configuracionParametrosPL.ObtenerPorOrganizacionTipoParametro(configuracionParametro);

                if (listaParametros != null && listaParametros.Count > 0)
                {
                    foreach (var parametro in listaParametros)
                    {
                        ParametrosEnum enumTemporal;
                        var res = ParametrosEnum.TryParse(parametro.Clave, out enumTemporal);
                        if (res)
                        {
                            switch (enumTemporal)
                            {
                                case ParametrosEnum.CuentaGastoEngordaFijo:
                                    AsignaCostoALista(listaFinal, Costo.GastosEngordaFijos, listaCostoTipoGasto);
                                    break;
                                case ParametrosEnum.CuentaGastoAlimentosFijo:
                                    AsignaCostoALista(listaFinal, Costo.GastosPlantaAlimentoFijos, listaCostoTipoGasto);
                                    break;
                                case ParametrosEnum.CuentaGastoFinanciero:
                                    AsignaCostoALista(listaFinal, Costo.GastosFinancieros, listaCostoTipoGasto);
                                    break;
                                case ParametrosEnum.CuentaGastosCentrosFijos:
                                    AsignaCostoALista(listaFinal, Costo.GastosCentrosFijos, listaCostoTipoGasto);
                                    break;
                                case ParametrosEnum.CuentaGastoMateriaPrima:
                                    AsignaCostoALista(listaFinal, Costo.GastosMateriaPrima, listaCostoTipoGasto);
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.GastosAlInventario_MensajeSinConfiguracionCuenta,
                      MessageBoxButton.OK, MessageImage.Warning);
                }
                //lista.Insert(0, seleccione);
                cmbGasto.ItemsSource = listaFinal;
                cmbGasto.DisplayMemberPath = "Descripcion";
                cmbGasto.SelectedValuePath = "TipoCostoID";
                cmbGasto.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.GastosAlInventario_MensajeFalloCargarGastos,
                       MessageBoxButton.OK, MessageImage.Error);
            }
        }
        /// <summary>
        /// Metodo que valida si el costo biene configurado en la lista de costos por tipo de gasto y 
        /// lo asigna en una lista en caos de encontrarlo
        /// </summary>
        /// <param name="listaFinal"></param>
        /// <param name="gastosEngordaFijos"></param>
        /// <param name="listaCostoTipoGasto"></param>
        private void AsignaCostoALista(List<CostoInfo> listaFinal, Costo gastosEngordaFijos, IList<CostoInfo> listaCostoTipoGasto)
        {
            CostoInfo costoConfigurado =
                listaCostoTipoGasto.FirstOrDefault(co => co.CostoID == gastosEngordaFijos.GetHashCode());
            if (costoConfigurado != null)
            {
                listaFinal.Add(costoConfigurado);
            }
        }

        /// <summary>
        /// Carga la informacion del centro de costo
        /// </summary>
        /// <param name="organizacion"></param>
        /// <param name="gasto"></param>
        private void CargarConfiguracionCentroCosto(OrganizacionInfo organizacion, CostoInfo gasto)
        {
            try
            {
                var organizacionPl = new OrganizacionPL();
                var costoPl = new CostoPL();
                if (organizacion != null)
                {
                    var datoOrganizacion = organizacionPl.ObtenerPorIdConIva(organizacion.OrganizacionID);
                    var centro = costoPl.ObtenerCentroCostoSAPPorCosto(gasto);
                    if (datoOrganizacion.Sociedad != null && centro != null)
                    {
                        txtCentroCostoGasto.Text = string.Format("{0}{1}{2}", "SA0",
                                                                 datoOrganizacion.OrganizacionID == Organizacion.GanaderaIntegralLucero.GetHashCode()
                                                                 ? 9 : datoOrganizacion.OrganizacionID, centro.CentroCostoSAP);
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.GastosAlInventario_MensajeGastoCentroCosto,
                      MessageBoxButton.OK, MessageImage.Warning);
                        cmbGasto.SelectedIndex = 0;
                        txtCentroCostoGasto.Text = string.Empty;
                        txtCuentaGasto.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.GastosAlInventario_MensajeFalloCargarConfiguracionCentroCosto,
                       MessageBoxButton.OK, MessageImage.Error);
            }
        }


        /// <summary>
        /// Cargar la informacion de las cuentas configuradas a la organizacion
        /// </summary>
        private void CargarConfiguracionCuentas(OrganizacionInfo organizacion, int costoId)
        {
            try
            {
                var pl = new ConfiguracionParametrosPL();

                var parametro = new ConfiguracionParametrosInfo
                {
                    TipoParametro = TiposParametrosEnum.GastosAlInventario.GetHashCode(),
                    OrganizacionID = organizacion.OrganizacionID
                };

                var lista = pl.ObtenerPorOrganizacionTipoParametro(parametro);
                var cuenta = string.Empty;
                if (lista != null && lista.Count > 0)
                {
                    if (costoId == Costo.GastosEngordaFijos.GetHashCode())
                    {
                        cuenta = lista.Where(p => p.Clave.Trim().ToUpper() == ParametrosEnum.CuentaGastoEngordaFijo.ToString().Trim().ToUpper()).ToList()[0].Valor;
                    }
                    else if (costoId == Costo.GastosPlantaAlimentoFijos.GetHashCode())
                    {
                        cuenta = lista.Where(p => p.Clave.Trim().ToUpper() == ParametrosEnum.CuentaGastoAlimentosFijo.ToString().Trim().ToUpper()).ToList()[0].Valor;
                    }
                    else if (costoId == Costo.GastosFinancieros.GetHashCode())
                    {
                        cuenta = lista.Where(p => p.Clave.Trim().ToUpper() == ParametrosEnum.CuentaGastoFinanciero.ToString().Trim().ToUpper()).ToList()[0].Valor;
                    }
                    else if (costoId == Costo.GastosCentrosFijos.GetHashCode())
                    {
                        cuenta = lista.Where(p => p.Clave.Trim().ToUpper() == ParametrosEnum.CuentaGastosCentrosFijos.ToString().Trim().ToUpper()).ToList()[0].Valor;
                    }
                    else if (costoId == Costo.GastosMateriaPrima.GetHashCode())
                    {
                        cuenta = lista.Where(p => p.Clave.Trim().ToUpper() == ParametrosEnum.CuentaGastoMateriaPrima.ToString().Trim().ToUpper()).ToList()[0].Valor;
                    }
                }

                txtCuentaGasto.Text = cuenta;

                if (txtCuentaGasto.Text == String.Empty)
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.GastosAlInventario_MensajeFalloCargarConfiguracionCuentaGastos,
                       MessageBoxButton.OK, MessageImage.Error);

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.GastosAlInventario_MensajeFalloCargarConfiguracionCuentaGastos,
                       MessageBoxButton.OK, MessageImage.Error);

            }
        }

        /// <summary>
        /// Inicializa la informacion de usuario
        /// </summary>
        private void InicializarDatosUsuario()
        {

            grpDetalleMovimientoGasto.Visibility = Visibility.Hidden;
            grpDetalleMovimientoFlete.Visibility = Visibility.Visible;
            CargarComboOpcion();
            MostrarFecha();
            CargarAyudas();
            CargarTipoMovimiento();


            //Validar corrales activos de enfermeria y produccion
        }

        /// <summary>
        /// Carga la lista de tipos de movimiento
        /// </summary>
        private void CargarTipoMovimiento()
        {
            try
            {
                var pl = new TipoCostoPL();
                var lista = pl.ObtenerTodos(EstatusEnum.Activo);

                var filteredItems = lista.Where(
                    p => p.TipoCostoID == (int)TipoCostoEnum.Flete ||
                         p.TipoCostoID == (int)TipoCostoEnum.Gasto).ToList();


                var seleccione = new TipoCostoInfo
                {
                    Activo = EstatusEnum.Activo,
                    Descripcion = Properties.Resources.GastosAlInventario_ElementoSeleccione,
                    TipoCostoID = 0

                };
                var costo = new TipoCostoInfo
                {
                    Activo = EstatusEnum.Activo,
                    Descripcion = Properties.Resources.GastosAlInventario_ElementoCosto,
                    TipoCostoID = 9

                };

                filteredItems.Insert(0, seleccione);
                filteredItems.Insert(1, costo);
                cmbTipoMovimiento.ItemsSource = filteredItems;
                cmbTipoMovimiento.DisplayMemberPath = "Descripcion";
                cmbTipoMovimiento.SelectedValuePath = "TipoCostoID";
                cmbTipoMovimiento.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.GastosAlInventario_MensajeFalloCargarTipoMovimiento,
                       MessageBoxButton.OK, MessageImage.Error);

            }
        }

        /// <summary>
        /// Muestra la fecha del sistema
        /// </summary>
        private void MostrarFecha()
        {
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        /// <summary>
        /// Carga la lista de opciones para carga del gasto
        /// </summary>
        private void CargarComboOpcion()
        {
            try
            {
                var lista = Enum.GetNames(typeof(OpcionGastoInventarioEnum)).ToList();

                lista.Insert(0, Properties.Resources.GastosAlInventario_ElementoSeleccione);
                cmbOpcion.ItemsSource = lista;
                cmbOpcion.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.GastosAlInventario_MensajeFalloCargarComboOpcion,
                       MessageBoxButton.OK, MessageImage.Error);

            }
        }

        /// <summary>
        /// Obtiene y muestra el total de corrales a afectar en opcion total
        /// </summary>
        private void ObtenerTotalCorralesAfectar()
        {
            var totalCorrales = 0;
            try
            {
                if (organizacionSeleccionada != null)
                {
                    var pl = new CorralPL();
                    var listaGrupo = new List<GrupoCorralInfo>
                    {
                        new GrupoCorralInfo{ Activo = EstatusEnum.Activo, GrupoCorralID = (int)GrupoCorralEnum.Produccion },
                        new GrupoCorralInfo{ Activo = EstatusEnum.Activo, GrupoCorralID = (int)GrupoCorralEnum.Enfermeria}
                    };

                    var corralinfo =
                    new CorralInfo
                    {
                        Codigo = String.Empty,
                        Activo = EstatusEnum.Activo,
                        OrganizacionId = organizacionSeleccionada.OrganizacionID,
                        ListaGrupoCorral = listaGrupo
                    };

                    var pagina = new PaginacionInfo { Inicio = 1, Limite = 15 };

                    var resultado = pl.ObtenerPorPaginaGruposCorrales(pagina, corralinfo);
                    if (resultado != null)
                        totalCorrales = resultado.TotalRegistros;

                    txtTotalCorrales.Text = totalCorrales.ToString(CultureInfo.InvariantCulture);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.GastosAlInventario_FalloObtenerTotalCorrales,
                       MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Inicializa los valores de corral para la ayuda
        /// </summary>
        /// <param name="corral"></param>
        /// <returns></returns>
        private CorralInfo InicializarInfoAyudaCorral(CorralInfo corral)
        {

            if (organizacionSeleccionada != null && corral != null)
            {
                var listaGrupo = new List<GrupoCorralInfo>
                {
                    new GrupoCorralInfo {Activo = EstatusEnum.Activo, GrupoCorralID = (int) GrupoCorralEnum.Produccion},
                    new GrupoCorralInfo {Activo = EstatusEnum.Activo, GrupoCorralID = (int) GrupoCorralEnum.Enfermeria}
                };

                corral.Codigo = string.Empty;
                corral.Activo = EstatusEnum.Activo;
                corral.Organizacion = organizacionSeleccionada;
                corral.OrganizacionId = organizacionSeleccionada.OrganizacionID;
                corral.ListaGrupoCorral = listaGrupo;
            }

            return corral;

        }

        /// <summary>
        /// Carga las ayudas iniciales
        /// </summary>
        private void CargarAyudas()
        {
            AgregarAyudaOrganizacion(splAyudaOrganizacion);
            AgregarAyudaProveedor(splAyudaCuentaProveedor);
        }

        /// <summary>
        /// Limpia los valores de costo o fletes
        /// </summary>
        private void LimpiarCamposCostoFlete()
        {
            cuentaSeleccionada = null;
            chkCuenta.IsChecked = false;
            proveedorSeleccionado = null;
            costoSeleccionado = null;
            chkIva.IsChecked = false;
            chkRetencion.IsChecked = false;
            txtImporte.Text = String.Empty;
            txtFactura.Text = String.Empty;
            costoSeleccionado = null;
            cuentaSeleccionada = null;
            proveedorSeleccionado = null;
        }
        /// <summary>
        /// Limpia los valores del gasto
        /// </summary>
        private void LimpiarCamposGasto()
        {
            txtCentroCostoGasto.Text = "";
            cmbGasto.SelectedIndex = 0;
            txtImporteGasto.Text = "";
            gastoSeleccionado = null;
        }
        /// <summary>
        /// Restablece el formulario a su estado por defecto
        /// </summary>
        private void LimpiarCampos(bool cambioOrganizacion = false)
        {
            if (skAyudaCorral != null) skAyudaCorral.LimpiarCampos();
            if (skAyudaCostos != null) skAyudaCostos.LimpiarCampos();
            if (skAyudaCuenta != null) skAyudaCuenta.LimpiarCampos();
            if (skAyudaProveedor != null) skAyudaProveedor.LimpiarCampos();

            if (!cambioOrganizacion)
            {
                if (skAyudaOrganizacion != null) skAyudaOrganizacion.LimpiarCampos();
                organizacionSeleccionada = null;
            }

            LimpiarCamposCostoFlete();
            LimpiarCamposGasto();

            cmbTipoMovimiento.SelectedIndex = 0;
            cmbOpcion.SelectedIndex = 0;
            cmbOpcion.IsEnabled = true;
            txtTotalCorrales.Text = "";
            txtObservaciones.Text = "";
            lblCorral.Visibility = Visibility.Hidden;
            lblCorralRequerido.Visibility = Visibility.Hidden;
            splAyudaCorral.Visibility = Visibility.Hidden;

            if (skAyudaOrganizacion != null) skAyudaOrganizacion.Focus();


            corralSeleccionado = null;
            movimientoSeleccionado = null;
        }

        #endregion

        #region Ayudas

        /// <summary>
        /// Inicializa la ayuda para obtener la cuenta del proveedor
        /// </summary>
        private void AgregarAyudaCuenta(StackPanel stackPanel)
        {
            var info = new CuentaSAPInfo
            {
                ListaTiposCuenta = new List<TipoCuentaInfo>
                {
                    new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.MateriaPrima.GetHashCode() },
                    new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.Inventario.GetHashCode()},
                    new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.InventarioEnTransito.GetHashCode()},
                    new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.Iva.GetHashCode()},
                    new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.Producto.GetHashCode()},
                    new TipoCuentaInfo{ TipoCuentaID = TipoCuenta.Provision.GetHashCode()},
                },

                Activo = EstatusEnum.Activo
            };

            skAyudaCuenta = new SKAyuda<CuentaSAPInfo>(250, false, info,
                "PropuedadClaveGastoInventario",
                "PropuedadDescripcionGastoInventario",true, 80,10, true)
                {
                    AyudaPL = new CuentaSAPPL(),
                    MetodoPorId = "ObtenerPorFiltro",
                    MetodoPorDescripcion = "ObtenerPorPagina",
                    MetodoPaginadoBusqueda = "ObtenerPorPagina",
                    MensajeClaveInexistente = Properties.Resources.Cuenta_CodigoInvalido,
                    MensajeAgregar = Properties.Resources.Cuenta_Seleccionar,
                    MensajeBusqueda = Properties.Resources.Cuenta_Busqueda,
                    MensajeBusquedaCerrar = Properties.Resources.EntradaMateriaPrima_CuentaSalirSinSeleccionar,
                    TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaCuenta,
                    TituloPantalla = Properties.Resources.BusquedaCuenta_Titulo
                };

            skAyudaCuenta.ObtenerDatos += ObtenerDatosCuenta;
            skAyudaCuenta.AsignaTabIndex(10);
            stackPanel.Children.Clear();
            stackPanel.Children.Add(skAyudaCuenta);


        }

        /// <summary>
        /// Obtiene la informacion de la cuenta seleccionada
        /// </summary>
        /// <param name="filtro"></param>
        private void ObtenerDatosCuenta(string filtro)
        {
            try
            {
                if (string.IsNullOrEmpty(filtro))
                {
                    return;
                }
                if (skAyudaCuenta.Info == null)
                {
                    return;
                }

                cuentaSeleccionada = skAyudaCuenta.Info;

                var cuentaInfo = new CuentaSAPInfo
                {

                    ListaTiposCuenta =
                        new List<TipoCuentaInfo>
                        {
                            new TipoCuentaInfo {TipoCuentaID = TipoCuenta.MateriaPrima.GetHashCode()},
                            new TipoCuentaInfo {TipoCuentaID = TipoCuenta.Inventario.GetHashCode()},
                            new TipoCuentaInfo {TipoCuentaID = TipoCuenta.InventarioEnTransito.GetHashCode()},
                            new TipoCuentaInfo {TipoCuentaID = TipoCuenta.Iva.GetHashCode()},
                            new TipoCuentaInfo {TipoCuentaID = TipoCuenta.Provision.GetHashCode()},
                        },
                    Activo = EstatusEnum.Activo
                };

                skAyudaCuenta.Info = cuentaInfo;

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Muestra la informacion de la ayuda de proveedor
        /// </summary>
        /// <param name="stackPanel"></param>
        private void AgregarAyudaProveedor(StackPanel stackPanel)
        {
            var proveedorInfo = new ProveedorInfo
            {
                Activo = EstatusEnum.Activo
            };

            skAyudaProveedor = new SKAyuda<ProveedorInfo>(250, false, proveedorInfo
                , "PropiedadCodigoSapEntradaMateriaPrima"
                , "PropiedadDescripcion", true, 80, 10, false)
                {
                    AyudaPL = new ProveedorPL(),
                    MensajeClaveInexistente = Properties.Resources.Proveedor_CodigoInvalido,
                    MensajeBusquedaCerrar = Properties.Resources.EntradaMateriaPrima_ProveedorSalirSinSeleccionar,
                    MensajeBusqueda = Properties.Resources.Proveedor_Busqueda,
                    MensajeAgregar = Properties.Resources.Proveedor_Seleccionar,
                    TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaProveedor,
                    TituloPantalla = Properties.Resources.BusquedaProveedor_Titulo
                };
            skAyudaProveedor.MaxLengthCampoID = 10;
            skAyudaProveedor.ObtenerDatos += ObtenerDatosProveedor;
            skAyudaProveedor.LlamadaMetodosNoExistenDatos += LimpiarProveedor;
            skAyudaProveedor.AsignaTabIndex(10);
            stackPanel.Children.Clear();
            stackPanel.Children.Add(skAyudaProveedor);

        }

        private void LimpiarProveedor()
        {
            skAyudaProveedor.LimpiarCampos();
        }

        /// <summary>
        /// Obtiene la informacion de la cuenta seleccionada
        /// </summary>
        /// <param name="filtro"></param>
        private void ObtenerDatosProveedor(string filtro)
        {
            try
            {
                if (string.IsNullOrEmpty(filtro))
                {
                    return;
                }
                if (skAyudaProveedor.Info == null)
                {
                    return;
                }

                proveedorSeleccionado = skAyudaProveedor.Info;

                var proveedorInfo = new ProveedorInfo
                {
                    Activo = EstatusEnum.Activo
                };

                skAyudaProveedor.Info = proveedorInfo;

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Agrega la ayuda para obtener el costo
        /// </summary>
        private void AgregarAyudaCostos(StackPanel stackPanel, CostoInfo filtro)
        {
            skAyudaCostos = new SKAyuda<CostoInfo>(200, false, filtro
                                                          , "PropiedadClaveGastoInventario"
                                                          , "PropiedadDescripcionGastoInventario", false, 50, true)
            {
                AyudaPL = new CostoPL(),
                MensajeClaveInexistente = Properties.Resources.CostoOrganizacion_CodigoInvalidoCosteo,
                MensajeBusquedaCerrar = Properties.Resources.Costo_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Costo_Busqueda,
                MensajeAgregar = Properties.Resources.Costo_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyehdaAyudaBusquedaCosto,
                TituloPantalla = Properties.Resources.BusquedaCosto_Titulo,
                MensajeNoPuedeBuscar = Properties.Resources.BusquedaCosto_CostoInvalido,
            };
            skAyudaCostos.ObtenerDatos += ObtenerDatosCosto;
            skAyudaCostos.LlamadaMetodosNoExistenDatos += LimpiaCostos;
            skAyudaCostos.AsignaTabIndex(4);
            skAyudaCostos.PuedeBuscar += () =>
            {
                if (string.IsNullOrWhiteSpace(skAyudaCostos.Clave))
                {
                    return true;
                }
                return Convert.ToInt32(skAyudaCostos.Clave) != Costo.CostoGanado.GetHashCode();
            };
            skAyudaCostos.IsTabStop = false;

            stackPanel.Children.Clear();
            stackPanel.Children.Add(skAyudaCostos);
        }

        private void LimpiaCostos()
        {
            skAyudaCostos.LimpiarCampos();
        }

        /// <summary>
        /// Obtiene el info seleccionado del costo
        /// </summary>
        /// <param name="filtro"></param>
        private void ObtenerDatosCosto(string filtro)
        {
            try
            {
                if (string.IsNullOrEmpty(filtro))
                {
                    return;
                }
                if (skAyudaCostos.Info == null)
                {
                    return;
                }

                if (costoSeleccionado == null)
                    costoSeleccionado = skAyudaCostos.Info;

                if (costoSeleccionado.CostoID != skAyudaCostos.Info.CostoID &&
                    skAyudaCostos.Info.CostoID != 0)
                    costoSeleccionado = skAyudaCostos.Info;

                var costo = new CostoInfo();

                var valor = (TipoCostoInfo)cmbTipoMovimiento.SelectedItem;
                if (valor.TipoCostoID == 9) //Tipo de movimiento costo, no ingresado a la bd
                {
                    costo = new CostoInfo
                    {
                        Activo = EstatusEnum.Activo,
                        ListaTipoCostos = new List<TipoCostoInfo>
                        {
                            new TipoCostoInfo{Activo = EstatusEnum.Activo,TipoCostoID = TipoCostoEnum.Alimento.GetHashCode()},
                            new TipoCostoInfo{Activo = EstatusEnum.Activo,TipoCostoID = TipoCostoEnum.Ganado.GetHashCode()},
                            new TipoCostoInfo{Activo = EstatusEnum.Activo,TipoCostoID = TipoCostoEnum.MateriaPrima.GetHashCode()},
                            new TipoCostoInfo{Activo = EstatusEnum.Activo,TipoCostoID = TipoCostoEnum.Medicamento.GetHashCode()},
                            new TipoCostoInfo{Activo = EstatusEnum.Activo,TipoCostoID = TipoCostoEnum.Operativo.GetHashCode()},
                            new TipoCostoInfo{Activo = EstatusEnum.Activo,TipoCostoID = TipoCostoEnum.Seguro.GetHashCode()}
                        }
                    };

                    if (costoSeleccionado.TipoCosto.TipoCostoID == TipoCostoEnum.Flete.GetHashCode() ||
                        costoSeleccionado.TipoCosto.TipoCostoID == TipoCostoEnum.Gasto.GetHashCode())
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.GastoInventario_AyudaCostoInvalido,
                            MessageBoxButton.OK, MessageImage.Stop);

                        skAyudaCostos.LimpiarCampos();
                        skAyudaCostos.Focus();
                    }

                }
                else if (valor.TipoCostoID == (int)TipoCostoEnum.Flete)
                {

                    costo = new CostoInfo
                    {
                        Activo = EstatusEnum.Activo,
                        ListaTipoCostos = new List<TipoCostoInfo>
                    {
                        new TipoCostoInfo{Activo = EstatusEnum.Activo,TipoCostoID = TipoCostoEnum.Flete.GetHashCode()},
                      
                    }
                    };

                    if (costoSeleccionado.TipoCosto.TipoCostoID != TipoCostoEnum.Flete.GetHashCode())
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.GastoInventario_AyudaCostoInvalido,
                            MessageBoxButton.OK, MessageImage.Stop);

                        skAyudaCostos.LimpiarCampos();
                        skAyudaCostos.Focus();
                    }

                }
                ValidacionesCosto(costoSeleccionado);
                skAyudaCostos.Info = costo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// realiza las validaciones del costo para bloquear los controles por costo
        /// </summary>
        private void ValidacionesCosto(CostoInfo costo)
        {
            if (costo.AbonoA == AbonoA.PROVEEDOR)
            {
                chkCuenta.IsEnabled = false;
                chkCuenta.IsChecked = false;

                chkIva.IsEnabled = true;
                chkRetencion.IsEnabled = true;
            }
            else
            {
                if (costo.AbonoA == AbonoA.CUENTA)
                {
                    chkCuenta.IsEnabled = false;
                    chkCuenta.IsChecked = true;

                    chkIva.IsEnabled = false;
                    chkRetencion.IsEnabled = true;
                }
                else
                {
                    chkCuenta.IsEnabled = true;
                    chkCuenta.IsChecked = false;

                    chkIva.IsEnabled = true;
                    chkRetencion.IsEnabled = true;
                }

            }

        }

        /// <summary>
        /// realiza las validaciones del costo para bloquear los controles por costo
        /// </summary>
        private void HabilitaDeshabilitaControles(bool tieneCuenta)
        {
            if (tieneCuenta)
            {
                chkIva.IsChecked = false;
                chkRetencion.IsChecked = false;

                chkIva.IsEnabled = false;
                chkRetencion.IsEnabled = false;
            }
            else
            {
                chkIva.IsChecked = false;
                chkRetencion.IsChecked = false;

                chkIva.IsEnabled = true;
                chkRetencion.IsEnabled = true;
            }

        }

        /// <summary>
        /// Obtener la ayuda para seleccionar el corral
        /// </summary>
        private void AgregarAyudaCorral(StackPanel stackPanel)
        {
            try
            {
                var corral = new CorralInfo();
                corral = InicializarInfoAyudaCorral(corral);
                if (corral != null)
                {
                    skAyudaCorral = new SKAyuda<CorralInfo>(50,
                        true,
                        corral,
                        "PropiedadClaveGastoAlInventario",
                        "PropiedadCodigoGastoAlInventario",
                        false)
                    {
                        AyudaPL = new CorralPL(),
                        MensajeClaveInexistente = Properties.Resources.GastoInventario_AyudaCorralInvalido,
                        MensajeBusquedaCerrar = Properties.Resources.GastoInventario_AyudaCorralSalirSinSeleccionar,
                        MensajeBusqueda = Properties.Resources.GastoInventario_AyudaCorralMensajeBusqueda,
                        MensajeAgregar = Properties.Resources.GastoInventario_AyudaCorralMensajeAgregar,
                        TituloEtiqueta = Properties.Resources.GastoInventario_AyudaCorralTituloEtiqueta,
                        TituloPantalla = Properties.Resources.GastoInventario_AyudaCorralTituloPantalla
                    };

                    skAyudaCorral.ObtenerDatos += ObtenerDatosCorral;
                    skAyudaCorral.LlamadaMetodosNoExistenDatos += LimpiarCorrales;

                    skAyudaCorral.AsignaTabIndex(3);
                    stackPanel.Children.Clear();
                    stackPanel.Children.Add(skAyudaCorral);
                    skAyudaCorral.TabIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);

                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.GastosAlInventario_MensajeFalloCargarAyudaCorral,
                       MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void LimpiarCorrales()
        {
            skAyudaCorral.LimpiarCampos();
        }

        /// <summary>
        /// Obtiene la informacion del corral seleccionado
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosCorral(string clave)
        {
            try
            {
                if (string.IsNullOrEmpty(clave))
                {
                    return;
                }
                if (skAyudaCorral.Info == null)
                {
                    return;
                }

                var lotePl = new LotePL();

                corralSeleccionado = skAyudaCorral.Info;

                var listaGrupo = new List<GrupoCorralInfo>
                    {
                        new GrupoCorralInfo{ Activo = EstatusEnum.Activo, GrupoCorralID = (int)GrupoCorralEnum.Produccion },
                        new GrupoCorralInfo{ Activo = EstatusEnum.Activo, GrupoCorralID = (int)GrupoCorralEnum.Enfermeria}
                    };

                skAyudaCorral.Info.Activo = EstatusEnum.Activo;
                skAyudaCorral.Info.OrganizacionId = organizacionSeleccionada.OrganizacionID;
                skAyudaCorral.Info.ListaGrupoCorral = listaGrupo;

                var lote = lotePl.ObtenerLotesActivos(organizacionSeleccionada.OrganizacionID, corralSeleccionado.CorralID);

                if (lote == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.GastoInventario_AyudaCorralInvalido,
                      MessageBoxButton.OK, MessageImage.Error);
                    skAyudaCorral.LimpiarCampos();

                    return;
                }

                var corralPl = new CorralPL();
                corralSeleccionado = corralPl.ObtenerPorId(corralSeleccionado.CorralID);

                if (corralSeleccionado.TipoCorral.TipoCorralID != TipoCorral.Enfermeria.GetHashCode() &&
                    corralSeleccionado.TipoCorral.TipoCorralID != TipoCorral.Produccion.GetHashCode())
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       Properties.Resources.GastoInventario_AyudaCorralInvalido,
                      MessageBoxButton.OK, MessageImage.Error);
                    skAyudaCorral.LimpiarCampos();
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Obtener la ayuda de seleccion de organizacion
        /// </summary>
        private void AgregarAyudaOrganizacion(StackPanel stackPanel)
        {
            try
            {
                var organizacionInfo = new OrganizacionInfo
                {
                    TipoOrganizacion = new TipoOrganizacionInfo { TipoOrganizacionID = TipoOrganizacion.Ganadera.GetHashCode() },
                    Activo = EstatusEnum.Activo
                };

                skAyudaOrganizacion = new SKAyuda<OrganizacionInfo>(200, false, organizacionInfo
                                                   , "PropiedadClaveConfiguracionPremezclas"
                                                   , "PropiedadDescripcionConfiguracionPremezclas",
                                                   "", false, 50, 9, true)
                {
                    AyudaPL = new OrganizacionPL(),
                    MensajeClaveInexistente = Properties.Resources.GastoInventario_AyudaOrganizacionClaveInexistente,
                    MensajeBusquedaCerrar = Properties.Resources.GastoInventario_AyudaOrganizacionSalirSinSeleccionar,
                    MensajeBusqueda = Properties.Resources.GastoInventario_AyudaOrganizacionMensajeBusqueda,
                    MensajeAgregar = Properties.Resources.GastoInventario_AyudaOrganizacionMensajeAgregar,
                    TituloEtiqueta = Properties.Resources.GastoInventario_AyudaOrganizacionTituloEtiqueta,
                    TituloPantalla = Properties.Resources.GastoInventario_AyudaOrganizacionTituloPantalla
                };


                skAyudaOrganizacion.ObtenerDatos += ObtenerDatosOrganizacion;
                skAyudaOrganizacion.LlamadaMetodosNoExistenDatos += LimpiarOrganizacion;

                skAyudaOrganizacion.AsignaTabIndex(0);
                stackPanel.Children.Clear();
                stackPanel.Children.Add(skAyudaOrganizacion);
                skAyudaOrganizacion.TabIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void LimpiarOrganizacion()
        {
            skAyudaOrganizacion.LimpiarCampos();
        }
        /// <summary>
        /// Obtiene la informacion de la clave seleccionada
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosOrganizacion(string clave)
        {
            try
            {
                if (string.IsNullOrEmpty(clave))
                {
                    return;
                }
                if (skAyudaOrganizacion.Info == null)
                {
                    return;
                }
                if (skAyudaOrganizacion.Info.TipoOrganizacion.TipoOrganizacionID !=
                    TipoOrganizacion.Ganadera.GetHashCode())
                {
                    skAyudaOrganizacion.Info = new OrganizacionInfo
                    {
                        TipoOrganizacion = new TipoOrganizacionInfo { TipoOrganizacionID = TipoOrganizacion.Ganadera.GetHashCode() },
                        Activo = EstatusEnum.Activo
                    };

                    skAyudaOrganizacion.LimpiarCampos();
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.GastoInventario_AyudaOrganizacionClaveInexistente,
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                }
                else
                {
                    organizacionSeleccionada = skAyudaOrganizacion.Info;

                    LimpiarCampos(true);

                    skAyudaOrganizacion.Info = new OrganizacionInfo
                    {
                        TipoOrganizacion = new TipoOrganizacionInfo { TipoOrganizacionID = TipoOrganizacion.Ganadera.GetHashCode() },
                        Activo = EstatusEnum.Activo
                    };

                    if (skAyudaCorral != null)
                    {
                        skAyudaCorral.LimpiarCampos();
                        InicializarInfoAyudaCorral(skAyudaCorral.Info);
                    }



                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Handler del boton guardar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            long folioGasto = GuardarGastoAlInventario();

            if (folioGasto > 0)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                    string.Format(Properties.Resources.GastosAlInventario_MensajeGuardadoExito,folioGasto),
                    MessageBoxButton.OK, MessageImage.Correct);

                LimpiarCampos();
            }
        }

        private long GuardarGastoAlInventario()
        {
            long retValue = 0;
            try
            {

                if (organizacionSeleccionada == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.GastosAlInventario_DatoObligatorioOrganizacion,
                        MessageBoxButton.OK, MessageImage.Stop);

                    if (skAyudaOrganizacion != null) skAyudaOrganizacion.Focus();

                    return 0;

                }

                if (cmbOpcion.SelectedIndex == 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.GastosAlInventario_DatoObligatorioOpcion,
                        MessageBoxButton.OK, MessageImage.Stop);

                    cmbOpcion.Focus();

                    return 0;
                }

                if (opcionSeleccionada.Equals(OpcionGastoInventarioEnum.Parcial.ToString()))
                {
                    //validamos el corral
                    if (corralSeleccionado == null || corralSeleccionado.CorralID == 0)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.GastosAlInventario_DatoObligatorioCorral,
                            MessageBoxButton.OK, MessageImage.Stop);

                        skAyudaCorral.Focus();

                        return 0;
                    }
                }

                if (cmbTipoMovimiento.SelectedIndex == 0)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.GastosAlInventario_DatoObligatorioTipoMovimiento,
                        MessageBoxButton.OK, MessageImage.Stop);

                    cmbTipoMovimiento.Focus();

                    return 0;
                }

                if (movimientoSeleccionado.TipoCostoID != TipoCostoEnum.Gasto.GetHashCode())
                {
                    //es flete o costo
                    if (costoSeleccionado == null)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.GastosAlInventario_DatoObligatorioCosto,
                            MessageBoxButton.OK, MessageImage.Stop);

                        skAyudaCostos.Focus();

                        return 0;
                    }

                    if (txtImporte.Text.Length == 0)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.GastosAlInventario_DatoObligatorioImporteCosto,
                            MessageBoxButton.OK, MessageImage.Stop);

                        txtImporte.Focus();

                        return 0;
                    }

                    if (txtFactura.Text == String.Empty)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.GastosAlInventario_DatoObligatorioFactura,
                            MessageBoxButton.OK, MessageImage.Stop);

                        txtFactura.Focus();

                        return 0;
                    }

                    if (chkCuenta.IsChecked == true)
                    {
                        //validar la cuenta ingresada

                        if (cuentaSeleccionada == null)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.GastosAlInventario_MensajeSinCuentaProveedor,
                                MessageBoxButton.OK, MessageImage.Stop);

                            skAyudaCuenta.Focus();

                            return 0;
                        }

                    }
                    else
                    {
                        //es proveedor
                        if (proveedorSeleccionado == null)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.GastosAlInventario_MensajeSinCuentaProveedor,
                                MessageBoxButton.OK, MessageImage.Stop);

                            skAyudaProveedor.Focus();

                            return 0;
                        }
                    }
                }
                else
                {
                    //es gasto
                    if (gastoSeleccionado == null)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.GastosAlInventario_DatoObligatorioGasto,
                            MessageBoxButton.OK, MessageImage.Stop);

                        cmbGasto.Focus();

                        return 0;
                    }

                    if (txtImporteGasto.Text.Length == 0)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.GastosAlInventario_DatoObligatorioImporteGasto,
                            MessageBoxButton.OK, MessageImage.Stop);

                        txtImporteGasto.Focus();

                        return 0;
                    }

                    if (txtCentroCostoGasto.Text == string.Empty)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.GastosAlInventario_DatoObligatorioCentroCosto,
                            MessageBoxButton.OK, MessageImage.Stop);

                        txtCentroCostoGasto.Focus();

                        return 0;
                    }

                    if (txtCuentaGasto.Text == string.Empty)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.GastosAlInventario_DatoObligatorioCuentaGasto,
                            MessageBoxButton.OK, MessageImage.Stop);

                        txtCuentaGasto.Focus();

                        return 0;
                    }
                }

                var gastoInventario = new GastoInventarioInfo
                    {
                        TieneCuenta = chkCuenta.IsChecked.HasValue && chkCuenta.IsChecked.Value,
                        IVA = chkIva.IsChecked.HasValue && chkIva.IsChecked.Value,
                        Retencion = chkRetencion.IsChecked.HasValue && chkRetencion.IsChecked.Value,
                        CentroCosto = txtCentroCostoGasto.Text,
                        Corral = corralSeleccionado,
                        CuentaGasto = txtCuentaGasto.Text,
                        CuentaSAP = cuentaSeleccionada,
                        Factura = txtFactura.Text.Trim()
                    };

                if (movimientoSeleccionado.TipoCostoID != TipoCostoEnum.Gasto.GetHashCode())
                {
                    gastoInventario.Importe = decimal.Parse(txtImporte.Text);
                    gastoInventario.Costo = costoSeleccionado;
                }
                else
                {
                    gastoInventario.Importe = decimal.Parse(txtImporteGasto.Text);
                    gastoInventario.Costo = gastoSeleccionado;

                }

                gastoInventario.Observaciones = txtObservaciones.Text.Trim();
                gastoInventario.TipoGasto =
                    opcionSeleccionada.Equals(OpcionGastoInventarioEnum.Parcial.ToString())
                        ? OpcionGastoInventarioEnum.Parcial
                        : OpcionGastoInventarioEnum.Total;

                gastoInventario.Organizacion = organizacionSeleccionada;
                gastoInventario.Proveedor = proveedorSeleccionado;
                gastoInventario.TipoMovimiento = movimientoSeleccionado;
                gastoInventario.TotalCorrales = !string.IsNullOrWhiteSpace(txtTotalCorrales.Text) ? int.Parse(txtTotalCorrales.Text) : 1;

                gastoInventario.Activo = EstatusEnum.Activo;
                gastoInventario.FechaGasto = DateTime.Now;
                gastoInventario.UsuarioId = int.Parse(Application.Current.Properties["UsuarioID"].ToString());

                var gastoPl = new GastoInventarioPL();
                retValue = gastoPl.Guardar(gastoInventario);
            }
            catch (ExcepcionServicio ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                       ex.Message,
                       MessageBoxButton.OK,
                       MessageImage.Stop);
            }
            catch (InvalidCastException ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.GastoInventario_ConversionInvalida,
                        MessageBoxButton.OK,
                        MessageImage.Error);
            }
            catch (Exception exep)
            {
                Logger.Error(exep);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.GastoInventario_GuardarError,
                        MessageBoxButton.OK,
                        MessageImage.Error);
            }
            return retValue;

        }

        /// <summary>
        /// handler del boton cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e)
        {
            if (SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.GastoInventario_MensajeCancelar,
                MessageBoxButton.YesNo, MessageImage.Warning) == MessageBoxResult.Yes)
            {
                LimpiarCampos();
            }
        }

        /// <summary>
        /// Handler del evento del combo de opciones
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbOpcion_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (organizacionSeleccionada != null)
            {
                var valor = (String)((ComboBox)e.Source).SelectedItem;
                if (valor.Equals(OpcionGastoInventarioEnum.Parcial.ToString()))
                {
                    //Mostramos el corral y su ayuda
                    lblCorral.Content = Properties.Resources.GastosAlInventario_labelCorral;
                    lblCorral.Visibility = Visibility.Visible;
                    lblCorralRequerido.Visibility = Visibility.Visible;
                    splAyudaCorral.Visibility = Visibility.Visible;
                    txtTotalCorrales.Visibility = Visibility.Hidden;
                    AgregarAyudaCorral(splAyudaCorral);
                }
                else if (valor.Equals(OpcionGastoInventarioEnum.Total.ToString()))
                {
                    //Mostramos el numero de corrales a afectar
                    lblCorral.Content = Properties.Resources.GastosAlInventario_labelCorrales;
                    lblCorral.Visibility = Visibility.Visible;
                    lblCorralRequerido.Visibility = Visibility.Hidden;
                    splAyudaCorral.Visibility = Visibility.Hidden;
                    txtTotalCorrales.Visibility = Visibility.Visible;
                    ObtenerTotalCorralesAfectar();
                }
                else
                {
                    txtTotalCorrales.Text = String.Empty;
                    splAyudaCorral.Visibility = Visibility.Hidden;
                    txtTotalCorrales.Visibility = Visibility.Hidden;
                    lblCorralRequerido.Visibility = Visibility.Hidden;
                    lblCorral.Visibility = Visibility.Hidden;
                }

                opcionSeleccionada = valor;
            }

        }

        /// <summary>
        /// Handler del evento change del tipo de movimiento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbTipoMovimiento_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var valor = (TipoCostoInfo)((ComboBox)e.Source).SelectedItem;
            if (valor.TipoCostoID == 9) //Tipo de movimiento costo, no ingresado a la bd
            {
                grpDetalleMovimientoFlete.Visibility = Visibility.Visible;
                grpDetalleMovimientoGasto.Visibility = Visibility.Hidden;
                cmbOpcion.IsEnabled = true;
                LimpiarCamposCostoFlete();

                var costo = new CostoInfo
                {
                    Activo = EstatusEnum.Activo,
                    ListaTipoCostos = new List<TipoCostoInfo>
                    {
                        new TipoCostoInfo{Activo = EstatusEnum.Activo,TipoCostoID = TipoCostoEnum.Alimento.GetHashCode()},
                        new TipoCostoInfo{Activo = EstatusEnum.Activo,TipoCostoID = TipoCostoEnum.Ganado.GetHashCode()},
                        new TipoCostoInfo{Activo = EstatusEnum.Activo,TipoCostoID = TipoCostoEnum.MateriaPrima.GetHashCode()},
                        new TipoCostoInfo{Activo = EstatusEnum.Activo,TipoCostoID = TipoCostoEnum.Medicamento.GetHashCode()},
                        new TipoCostoInfo{Activo = EstatusEnum.Activo,TipoCostoID = TipoCostoEnum.Operativo.GetHashCode()},
                        new TipoCostoInfo{Activo = EstatusEnum.Activo,TipoCostoID = TipoCostoEnum.Seguro.GetHashCode()}
                    }
                };

                AgregarAyudaCostos(splAyudaCosto, costo);

            }
            else if (valor.TipoCostoID == (int)TipoCostoEnum.Flete)
            {
                grpDetalleMovimientoFlete.Visibility = Visibility.Visible;
                grpDetalleMovimientoGasto.Visibility = Visibility.Hidden;

                LimpiarCamposGasto();

                cmbOpcion.SelectedIndex = 1; //Opcion parcial
                cmbOpcion.IsEnabled = false;

                var costo = new CostoInfo
                {
                    Activo = EstatusEnum.Activo,
                    ListaTipoCostos = new List<TipoCostoInfo>
                    {
                        new TipoCostoInfo{Activo = EstatusEnum.Activo,TipoCostoID = TipoCostoEnum.Flete.GetHashCode()},
                      
                    }
                };

                AgregarAyudaCostos(splAyudaCosto, costo);

            }
            else if (valor.TipoCostoID == (int)TipoCostoEnum.Gasto)
            {
                grpDetalleMovimientoFlete.Visibility = Visibility.Hidden;
                grpDetalleMovimientoGasto.Visibility = Visibility.Visible;

                CargarGastos();
                LimpiarCamposCostoFlete();
                cmbOpcion.IsEnabled = true;
            }
            else if (valor.TipoCostoID == 0)
            {
                grpDetalleMovimientoFlete.Visibility = Visibility.Hidden;
                grpDetalleMovimientoGasto.Visibility = Visibility.Hidden;
                cmbOpcion.IsEnabled = true;
            }

            movimientoSeleccionado = valor;
        }

        /// <summary>
        /// Handler del evento clic del check de cuenta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkCuenta_OnClick(object sender, RoutedEventArgs e)
        {
            var check = (CheckBox)sender;

            if (check != null)
            {
                if (check.IsChecked == true)
                {
                    AgregarAyudaCuenta(splAyudaCuentaProveedor);
                }
                else
                {
                    AgregarAyudaProveedor(splAyudaCuentaProveedor);
                }
                HabilitaDeshabilitaControles(check.IsChecked != null && check.IsChecked.Value);
            }

        }

        /// <summary>
        /// Handler de seleccion del combo gasto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbGasto_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cmb = (ComboBox)sender;

            if (cmb.SelectedIndex > 0)
            {
                gastoSeleccionado = (CostoInfo)cmb.SelectedItem;
                CargarConfiguracionCuentas(organizacionSeleccionada, gastoSeleccionado.CostoID);
                CargarConfiguracionCentroCosto(organizacionSeleccionada, gastoSeleccionado);
            }
        }

        /// <summary>
        /// Valida la factura entrada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtFactura_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumerosConGuion(e.Text);
        }

        private void TxtImporteGastoPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var valor = sender as TextBox;
            e.Handled = Extensor.ValidarSoloNumerosNegativosDecimales(valor.Text, e.Text);
        }

        #endregion

    }
}
