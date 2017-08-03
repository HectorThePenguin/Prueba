using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles.Ayuda;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.MateriaPrima
{
    /// <summary>
    /// Lógica de interacción para ProgramacionFletesInterna.xaml
    /// </summary>
    public partial class ProgramacionFletesInterna
    {

        #region Propiedades
        private FleteInternoInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (FleteInternoInfo)DataContext;
            }
            set { DataContext = value; }
        }

        private SKAyuda<ProductoInfo> skAyudaProductos;
        private int organizacionID;
        #endregion

        #region Constructor
        //Constructor
        public ProgramacionFletesInterna()
        {
            InitializeComponent();
            InicializaContexto();
            CargarComboTipoMovimiento();
            CargarAyudaProductos();
            organizacionID = Extensor.ValorEntero(Application.Current.Properties["OrganizacionID"].ToString());
        }
        #endregion

        #region Eventos
        /// <summary>
        /// Validar caracteres especiales
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtDestino_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Extensor.ValidarSoloLetrasYNumerosConPunto(e.Text);
        }

        /// <summary>
        /// Abre la pantalla para registrar un flete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNuevo_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var registrarProgramacionFletesInterna = new RegistrarProgramacionFletesInterna(true);
                MostrarCentrado(registrarProgramacionFletesInterna);
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Asignar foco a combo tipo movimiento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgramacionFletesInterna_OnLoaded(object sender, RoutedEventArgs e)
        {
            UcPaginacion.DatosDelegado += ObtenerFletes;
            UcPaginacion.AsignarValoresIniciales();
            Buscar();
            CboTipoMovimiento.Focus();
        }

        /// <summary>
        /// Cambia al siguiente elemento al dar enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgramacionFletesInterna_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AvanzarSiguienteControl(e);
            }
        }

        /// <summary>
        /// Limpiar controles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLimpiar_OnClick(object sender, RoutedEventArgs e)
        {
            CboTipoMovimiento.SelectedIndex = 0;
            skAyudaProductos.LimpiarCampos();
            TxtDestino.Clear();
            CboTipoMovimiento.Focus();
        }

        /// <summary>
        /// Busca fletes internos y los muestra en el grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBuscar_OnClick(object sender, RoutedEventArgs e)
        {
            Contexto.Organizacion = new OrganizacionInfo() { OrganizacionID = organizacionID, Descripcion = TxtDestino.Text.Trim() };
            if (String.IsNullOrEmpty(skAyudaProductos.Clave.Trim()) &&
                String.IsNullOrEmpty(skAyudaProductos.Descripcion.Trim()))
            {
                Contexto.Producto = new ProductoInfo();
            }
            else
            {
                var productoInfo = new ProductoInfo()
                {
                    ProductoId = Convert.ToInt32(skAyudaProductos.Clave)
                };
                Contexto.Producto = productoInfo;
            }
            switch (CboTipoMovimiento.SelectedIndex)
            {
                case 1:
                    Contexto.TipoMovimiento = new TipoMovimientoInfo() { TipoMovimientoID = TipoMovimiento.PaseProceso.GetHashCode() };
                    break;
                case 2:
                    Contexto.TipoMovimiento = new TipoMovimientoInfo()
                    {
                        TipoMovimientoID = TipoMovimiento.SalidaPorTraspaso.GetHashCode()
                    };
                    break;
                default:
                    Contexto.TipoMovimiento = new TipoMovimientoInfo()
                    {
                        TipoMovimientoID = 0
                    };
                    break;
            }
            Contexto.Activo = EstatusEnum.Activo;
            Buscar();
        }

        /// <summary>
        /// Editar 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditar_OnClick(object sender, RoutedEventArgs e)
        {
            var botonEditar = (Button)e.Source;
            try
            {
                var fleteInternoInfo = (FleteInternoInfo)Extensor.ClonarInfo(botonEditar.CommandParameter);
                if (fleteInternoInfo == null) return;
                var registrarProgramacionFletesInterna = new RegistrarProgramacionFletesInterna(fleteInternoInfo);
                MostrarCentrado(registrarProgramacionFletesInterna);
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Metodo que carga los datos en combo ajuste
        /// </summary>
        private void CargarComboTipoMovimiento()
        {
            try
            {
                CboTipoMovimiento.Items.Clear();
                CboTipoMovimiento.Items.Insert(0, Properties.Resources.RegistrarProgramacionFletesInterna_Seleccione);
                CboTipoMovimiento.Items.Insert(1, Properties.Resources.RegistrarProgramacionFletesInterna_CboTipoMovimientoPaseProceso);
                CboTipoMovimiento.Items.Insert(2, Properties.Resources.RegistrarProgramacionFletesInterna_CboTipoMovimientoSalidaPorTraspaso);
                CboTipoMovimiento.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Iniciliaza el contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new FleteInternoInfo
            {
                Organizacion = new OrganizacionInfo(),
                TipoMovimiento = new TipoMovimientoInfo(),
                AlmacenOrigen = new AlmacenInfo(),
                AlmacenDestino = new AlmacenInfo(),
                Producto = new ProductoInfo(),
                Activo = EstatusEnum.Activo
            };
        }

        /// <summary>
        /// Carga ayudas de productos
        /// </summary>
        private void CargarAyudaProductos()
        {
            var productoInfo = new ProductoInfo
            {
                ProductoId = 0,
                Activo = EstatusEnum.Activo
            };
            skAyudaProductos = new SKAyuda<ProductoInfo>(200, false, productoInfo
                                                   , "PropiedadClaveProgramacionMateriaPrima"
                                                   , "PropiedadDescripcionProgramacionMateriaPrima",
                                                   "", false, 80, 9, true)
            {
                AyudaPL = new ProductoPL(),
                MensajeClaveInexistente = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaProductoInvalido,
                MensajeBusquedaCerrar = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaProductoSalirSinSeleccionar,
                MensajeBusqueda = Properties.Resources.Proveedor_Busqueda,
                MensajeAgregar = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaProductoSeleccionar,
                TituloEtiqueta = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaLeyendaProducto,
                TituloPantalla = Properties.Resources.RegistrarProgramacionFletesInterna_AyudaProductoTitulo,
            };
            skAyudaProductos.ObtenerDatos += ObtenerDatosProducto;
            SplAyudaProducto.Children.Clear();
            SplAyudaProducto.Children.Add(skAyudaProductos);
        }

        /// <summary>
        /// Obtiene los datos del proveedor con la clave obtenida en la ayuda
        /// </summary>
        /// <param name="clave"></param>
        private void ObtenerDatosProducto(string clave)
        {
            try
            {
                skAyudaProductos.Info = new ProductoInfo
                {
                    ProductoId = 0,
                    Activo = EstatusEnum.Activo
                };
                TxtDestino.Focus();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                Properties.Resources.ConfiguracionPremezcla_MensajeErrorObtenerDatosProducto, MessageBoxButton.OK,
                MessageImage.Error);
            }
        }

        /// <summary>
        /// Avanza al siguiente control en la pantalla
        /// </summary>
        /// <param name="e"></param>
        private void AvanzarSiguienteControl(KeyEventArgs e)
        {
            var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
            var keyboardFocus = Keyboard.FocusedElement as UIElement;

            if (keyboardFocus != null)
            {
                keyboardFocus.MoveFocus(tRequest);
            }
            e.Handled = true;
        }

        /// <summary>
        /// Buscar fletes activos
        /// </summary>
        public void Buscar()
        {
            ObtenerFletes(UcPaginacion.Inicio, UcPaginacion.Limite);
        }

        /// <summary>
        /// Obtiene los contratos
        /// </summary>
        /// <param name="inicio"></param>
        /// <param name="limite"></param>
        private void ObtenerFletes(int inicio, int limite)
        {
            try
            {
                var fleteInternoPl = new FleteInternoPL();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<FleteInternoInfo> resultadoInfo = fleteInternoPl.ObtenerPorPaginaFiltroDescripcionOrganizacion(pagina, Contexto);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    GridFletes.ItemsSource = resultadoInfo.Lista;
                    UcPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    UcPaginacion.TotalRegistros = 0;
                    UcPaginacion.AsignarValoresIniciales();
                    GridFletes.ItemsSource = new List<FleteInternoInfo>();
                }

            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.AdministrarContrato_MensajeError, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.AdministrarContrato_MensajeError, MessageBoxButton.OK, MessageImage.Error);
            }
        }
        #endregion
    }
}
