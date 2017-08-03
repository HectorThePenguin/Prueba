using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Recepcion
{


    /// <summary>
    /// Lógica de interacción para CancelacionCosteoEntradaGanado.xaml
    /// </summary>
    public partial class CancelacionCosteoEntradaGanado 
    {
        #region PROPIEDADES
        /// <summary>
        /// Propiedad donde se almacena el Contenedor principal 
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
        #endregion PROPIEDADES

        #region VARIABLES
        /// <summary>
        /// Control para la ayuda de Organización
        /// </summary>
        private SKAyuda<OrganizacionInfo> skAyudaOrganizacion;

        /// <summary>
        /// Control para la ayuda de Folio de Entrada
        /// </summary>
        private SKAyuda<EntradaGanadoInfo> skAyudaFolio;

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
        /// Vairiable para manejar el tipo de Costo de Transportación
        /// </summary>
        private const int TipoCostoTraslado = 1;

        /// <summary>
        /// Propiedad donde se revisan los tipos de Organizaciones a los que se capturan los datos
        /// </summary>
        private readonly int[] tiposOrganizacionesAplicaCaptura = new[] { TipoOrganizacion.CompraDirecta.GetHashCode(), TipoOrganizacion.Intensivo.GetHashCode(), TipoOrganizacion.Maquila.GetHashCode() };

        /// <summary>
        /// Variable para manejar el costo de ganado por default
        /// </summary>
        private const string CuentaInventario = "CTAINVTRAN";


        #endregion VARIABLES
        public CancelacionCosteoEntradaGanado()
        {
            InitializeComponent();
            organizacionIdLogin = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
            usuarioLogueadoID = Extensor.ValorEntero(Application.Current.Properties["UsuarioID"].ToString());
            CargarAyudas();
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


            dpFechaSalida.SelectedDate = ContenedorCosteoEntrada.EntradaGanado.FechaSalida.Date;
            dtuHoraSalida.Text = ContenedorCosteoEntrada.EntradaGanado.FechaSalida.ToString("HH:mm");

            dpFechaRecepcion.SelectedDate = ContenedorCosteoEntrada.EntradaGanado.FechaEntrada.Date;
            dtuHoraRecepcion.Text = ContenedorCosteoEntrada.EntradaGanado.FechaEntrada.ToString("HH:mm");

            var pesoNeto = ContenedorCosteoEntrada.EntradaGanado.PesoBruto -
                               ContenedorCosteoEntrada.EntradaGanado.PesoTara;
            txtPesoLlegada.Text = pesoNeto.ToString(CultureInfo.InvariantCulture);

            var entradaGanadoCosteoPL = new EntradaGanadoCosteoPL();
            var entradaGanadoCosteo =
                entradaGanadoCosteoPL.ObtenerPorEntradaID(ContenedorCosteoEntrada.EntradaGanado.EntradaGanadoID);

            if (entradaGanadoCosteo != null)
            {
                ContenedorCosteoEntrada.EntradaGanadoCosteo = entradaGanadoCosteo;
                ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada.ToList().ForEach(cost => cost.EditarNumeroDocumento = false);

                btnCancelar.IsEnabled = true;
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
                CalcularMerma();
                btnCancelar.IsEnabled = true;
            }
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
            skAyudaFolio.AsignarFoco();
        }

        /// <summary>
        /// Metodo para Calcular el Porcentaje de Merma
        /// </summary>
        private void CalcularMerma()
        {
            var pesoOrigenTotal =
                ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaEntradaDetalle.Sum(det => det.PesoOrigen);
            var pesoLlegada = ContenedorCosteoEntrada.EntradaGanado.PesoBruto -
                              ContenedorCosteoEntrada.EntradaGanado.PesoTara;

            var porcentajeMerma = Math.Round(((pesoOrigenTotal - pesoLlegada) / pesoOrigenTotal) * 100, 2);
            txtMerma.Text = porcentajeMerma.ToString(CultureInfo.InvariantCulture);
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
        /// Agrega control de Ayuda para Folio
        /// </summary>
        private void AgregarAyudaFolio()
        {
            skAyudaFolio = new SKAyuda<EntradaGanadoInfo>(100, true, new EntradaGanadoInfo(), "PropiedadClaveCancelacionCosteoEntrada"
                                                        , "PropiedadDescripcionCancelacionCosteoEntrada"
                                                        , "PropiedadOcultaCancelacionCosteoEntradaGanado", true)
            {
                AyudaPL = new EntradaGanadoPL(),
                MensajeClaveInexistente = Properties.Resources.FolioEntradaCancelacion_Inexistente,
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
        /// Consulta el Folio de Entrada Seleccionado
        /// </summary>
        private void ConsultarEntradaGanado()
        {
            try
            {
                int folioEntrada;
                int.TryParse(skAyudaFolio.Descripcion, out folioEntrada);
                if (folioEntrada == 0)
                {
                    skAyudaFolio.Descripcion = skAyudaFolio.Clave;
                    int.TryParse(skAyudaFolio.Descripcion, out folioEntrada);
                }
                var entradaGanadoPL = new EntradaGanadoPL();
                var entradaGanado = entradaGanadoPL.ObtenerEntradasGanadoCosteado(folioEntrada, organizacionIdLogin);
                if (entradaGanado == null)
                {
                    return;
                }
                ContenedorCosteoEntrada.EntradaGanado = entradaGanado;
                CargarCamposConsulta();
                var dataContext = DataContext;
                DataContext = null;
                DataContext = dataContext;
                skAyudaFolio.Descripcion =
                    ContenedorCosteoEntrada.EntradaGanado.FolioEntrada.ToString(CultureInfo.InvariantCulture);

            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
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

            ContenedorCosteoEntrada.EntradaGanado.UsuarioModificacionID = usuarioLogueadoID;
            ContenedorCosteoEntrada.EntradaGanado.Costeado = false;

            ObtenerDatosEntradaGanadoTransito();
            ObtenerDatosEntradaGanadoMuerte();

            var entradaGanadoCosteoPL = new EntradaGanadoCosteoPL();
            MemoryStream stream = entradaGanadoCosteoPL.Guardar(ContenedorCosteoEntrada);
            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                               Properties.Resources.CosteoEntradaGanado_CanceladoExitoso, MessageBoxButton.OK,
                               MessageImage.Correct);
            if (stream != null)
            {
                var exportarPoliza = new ExportarPoliza();
                exportarPoliza.ImprimirPoliza(stream,
                                              string.Format("{0} {1}", "Cancelacion de Poliza de Entrada Folio ",
                                                            ContenedorCosteoEntrada.EntradaGanado.FolioEntrada));
            }
            LimpiarControles();
        }

        private void ObtenerDatosEntradaGanadoMuerte()
        {
            var entradaGanadoMuerteBL = new EntradaGanadoMuerteBL();
            ContenedorCosteoEntrada.EntradasGanadoMuertes =
                entradaGanadoMuerteBL.ObtenerMuertesEnTransitoPorEntradaGanadoID(
                    ContenedorCosteoEntrada.EntradaGanado.EntradaGanadoID);
            if (ContenedorCosteoEntrada.EntradasGanadoMuertes == null)
            {
                ContenedorCosteoEntrada.EntradasGanadoMuertes = new List<EntradaGanadoMuerteInfo>();
            }
        }

        /// <summary>
        /// Obtiene los datos de la entrada ganado transito
        /// </summary>
        private void ObtenerDatosEntradaGanadoTransito()
        {
            int cabezasOrigen = ContenedorCosteoEntrada.EntradaGanado.CabezasOrigen;
            int cabezasLlegada = ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaEntradaDetalle.Sum(cab => cab.Cabezas);
            int diferenciaCabezas = cabezasLlegada - cabezasOrigen;

            if (diferenciaCabezas != 0)
            {
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
                    return;
                }
                var entradaGanadoTransitoBL = new EntradaGanadoTransitoBL();
                EntradaGanadoTransitoInfo entradaGanadoTransito =
                    entradaGanadoTransitoBL.ObtenerPorCorralOrganizacion(parametroOrganizacion.Valor,
                                                                         organizacionIdLogin);
                if (entradaGanadoTransito != null)
                {
                    entradaGanadoTransito.Sobrante = diferenciaCabezas >= 0;
                    decimal pesoTransito =
                        ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaEntradaDetalle.Sum(
                            peso => peso.PesoLlegada - peso.PesoOrigen);
                    decimal importeTransito = ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaEntradaDetalle.Sum(
                        imp => imp.Importe);
                    decimal importeOrigen = ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaEntradaDetalle.Sum(
                        imp => imp.ImporteOrigen);

                    LoteInfo lote = entradaGanadoTransito.Lote;
                    lote.Cabezas = lote.Cabezas + diferenciaCabezas;
                    lote.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();

                    int multiplicador;
                    if (entradaGanadoTransito.Sobrante)
                    {
                        multiplicador = 1;
                    }
                    else
                    {
                        multiplicador = -1;
                    }
                    entradaGanadoTransito.Cabezas = (diferenciaCabezas * multiplicador) * -1;
                    entradaGanadoTransito.Peso = Convert.ToInt32(pesoTransito * multiplicador);

                    int totalCabezasTipoGanado = ContenedorCosteoEntrada.EntradaGanadoCosteo
                                                                        .ListaEntradaDetalle.Sum(cab => cab.Cabezas);
                    totalCabezasTipoGanado += (diferenciaCabezas * multiplicador);
                    int porcentajeDescontar = Convert.ToInt32(Math.Round(100 - ((importeTransito * 100) / importeOrigen), 0, MidpointRounding.AwayFromZero));

                    List<EntradaGanadoTransitoDetalleInfo> detalles = entradaGanadoTransito.EntradasGanadoTransitoDetalles;
                    EntradaGanadoTransitoDetalleInfo detalle;

                    decimal importeConDescuento;
                    for (int index = 0; index < detalles.Count; index++)
                    {
                        detalle = detalles[index];

                        importeOrigen = ContenedorCosteoEntrada.EntradaGanadoCosteo.ListaCostoEntrada
                                                               .Where(id => id.Costo.CostoID == detalle.Costo.CostoID)
                                                               .Select(imp => imp.Importe).FirstOrDefault();
                        importeConDescuento = (importeOrigen / 100) * porcentajeDescontar;
                        detalle.Importe = importeConDescuento * multiplicador;
                    }
                    entradaGanadoTransito.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
                    ContenedorCosteoEntrada.EntradaGanadoTransito = entradaGanadoTransito;
                }
            }
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
            CargarHorasDefault();
            skAyudaFolio.LimpiarCampos();
            DgCostoGanado.ItemsSource = null;
            skAyudaFolio.IsEnabled = true;
            dpFechaRecepcion.SelectedDate = null;
            dpFechaSalida.SelectedDate = null;
            skAyudaFolio.AsignarFoco();
            btnCancelar.IsEnabled = false;
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
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  ex.Message, MessageBoxButton.OK, MessageImage.Stop);
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
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Metodo que inicializa los objetos principales de la pantalla
        /// </summary>
        private void InicializaContexto()
        {
            ContenedorCosteoEntrada = new ContenedorCosteoEntradaGanadoInfo();
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
                var stackPanel = GetVisualChild<StackPanel>(cell);
                if (costo.TieneCuenta)
                {
                    AgregarAyudaCuenta(stackPanel);
                }
                else
                {
                    AgregarAyudaProveedor(stackPanel);
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
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
                                                        , true, 80,10, true)
            {
                AyudaPL = new ProveedorPL(),
                Info = new ProveedorInfo(),
                MensajeClaveInexistente = Properties.Resources.Proveedor_CodigoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.Proveedor_SalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Proveedor_Busqueda,
                MensajeAgregar = Properties.Resources.Proveedor_Seleccionar,
                TituloEtiqueta = Properties.Resources.LeyendaAyudaBusquedaProveedor,
                TituloPantalla = Properties.Resources.BusquedaProveedor_Titulo,
            };
            stackPanel.Children.Clear();
            stackPanel.Children.Add(skAyudaProveedor);
        }

        /// <summary>
        /// Agrega la Ayuda de Cuenta
        /// </summary>
        private void AgregarAyudaCuenta(StackPanel stackPanel)
        {
            skAyudaCuenta = new SKAyuda<CuentaSAPInfo>("CuentaSAP", "Descripcion", 160, false
                                                      , "CuentaProvision", "DescripcionCuenta", 80,10, true);
            var camposInfo = new List<String> { "CuentaSAP", "Descripcion" };
            skAyudaCuenta.AyudaPL = new CuentaSAPPL();
            skAyudaCuenta.Info = new CuentaSAPInfo
            {
                TipoCuenta = new TipoCuentaInfo
                {
                    TipoCuentaID = TipoCuenta.Provision.GetHashCode()
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

            stackPanel.Children.Clear();
            stackPanel.Children.Add(skAyudaCuenta);
        }

        /// <summary>
        /// Obtiene la Depedencia por Costo
        /// </summary>
        /// <returns></returns>
        private IList<IDictionary<IList<string>, Object>> ObtenerDependenciasCosto()
        {
            IList<IDictionary<IList<string>, Object>> dependencias = new List<IDictionary<IList<string>, Object>>();
            IDictionary<IList<string>, Object> dependecia = new Dictionary<IList<string>, Object>();
            IList<string> camposDependientes = new List<string>();

            camposDependientes.Add("TipoCostoID");

            var tipoCostoInfo = new TipoCostoInfo { TipoCostoID = TipoCostoTraslado };

            dependecia.Add(camposDependientes, tipoCostoInfo);
            dependencias.Add(dependecia);

            return dependencias;
        }

        /// <summary>
        /// Agrega control de Ayuda para Costo
        /// </summary>
        private void AgregarAyudaCosto(StackPanel stackPanel)
        {
            if (!tiposOrganizacionesAplicaCaptura.Contains(ContenedorCosteoEntrada.EntradaGanado.TipoOrigen))
            {
                skAyudaCosto = new SKAyuda<CostoInfo>(160, false, new CostoInfo(), "PropiedadClaveCosteoEntrada"
                                                    , "PropiedadDescripcionCosteoEntrada", "PropiedadOcultaCosteoEntrada", true, true)
                {
                    AyudaPL = new CostoPL(),
                    MensajeClaveInexistente = Properties.Resources.Costo_CodigoInvalido,
                    MensajeBusquedaCerrar = Properties.Resources.Costo_SalirSinSeleccionar,
                    MensajeBusqueda = Properties.Resources.Costo_Busqueda,
                    MensajeAgregar = Properties.Resources.Costo_Seleccionar,
                    TituloEtiqueta = Properties.Resources.LeyehdaAyudaBusquedaCosto,
                    TituloPantalla = Properties.Resources.BusquedaCosto_Titulo,
                };
                var dependencias = ObtenerDependenciasCosto();
                skAyudaCosto.Dependencias = null;
                skAyudaCosto.Dependencias = dependencias;
            }
            else
            {
                skAyudaCosto = new SKAyuda<CostoInfo>(160, false, new CostoInfo { TipoCosto = new TipoCostoInfo { TipoCostoID = TipoCostoTraslado } }, "PropiedadClaveCosteoEntradaSinDependencia"
                                                    , "PropiedadDescripcionCosteoEntradaSinDependencia", "PropiedadOcultaCosteoEntrada", true,  true)
                {
                    AyudaPL = new CostoPL(),
                    MensajeClaveInexistente = Properties.Resources.Costo_CodigoInvalido,
                    MensajeBusquedaCerrar = Properties.Resources.Costo_SalirSinSeleccionar,
                    MensajeBusqueda = Properties.Resources.Costo_Busqueda,
                    MensajeAgregar = Properties.Resources.Costo_Seleccionar,
                    TituloEtiqueta = Properties.Resources.LeyehdaAyudaBusquedaCosto,
                    TituloPantalla = Properties.Resources.BusquedaCosto_Titulo,
                };
            }
            stackPanel.Children.Clear();
            stackPanel.Children.Add(skAyudaCosto);
        }
    }
}
 