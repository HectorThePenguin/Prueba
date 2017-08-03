using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Recepcion
{
    /// <summary>
    /// Lógica de interacción para Tarifario.xaml
    /// </summary>
    public partial class Tarifario
    {
        #region Propiedades

        public bool Recargar;
        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private TarifarioInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (TarifarioInfo)DataContext;
            }
            set { DataContext = value; }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Tarifario()
        {
            InitializeComponent();
            InicializaContexto();
            Recargar = true;
        }
        #endregion Constructor

        #region Eventos

        /// <summary>
        /// Evento de Carga de la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Recargar)
                {
                    CargarCboEstatus();
                    ucPaginacion.DatosDelegado += ObtenerListaBitacoraTarifa;
                    ucPaginacion.AsignarValoresIniciales();
                    InicializarAyudas();
                    ValidarProveedoresActivos();
                    skAyudaProveedor.txtClave.Focus();
                    Recargar = false;
                }
                
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Tarifario_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Tarifario_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento para buscar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarConfiguracionOrigenDestino())
            {
                Buscar();     
            }
           
        }

        /// <summary>
        /// Evento Para ver detalles de gastos fijos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVerGastosFijos_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)e.Source;
            VerGastosFijos(btn);
        }

        /// <summary>
        /// Evento cuando cuando se selecciona un dato en la ayuda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void skAyudaProveedor_AyudaConDatos(object sender, EventArgs e)
        {
            if (((ProveedorInfo)skAyudaProveedor.Contexto).Activo == EstatusEnum.Activo)
            {
                Contexto.Proveedor = (ProveedorInfo)skAyudaProveedor.Contexto;
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Proveedor_Inactivo,
                                  MessageBoxButton.OK, MessageImage.Stop);
                skAyudaProveedor.LimpiarCampos();
                skAyudaProveedor.AsignarFoco();
            }
           
        }

        /// <summary>
        /// Evento cuando la ayuda no encuentran datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void skAyudaProveedor_AyudaLimpia(object sender, EventArgs e)
        {
            skAyudaProveedor.AsignarFoco();
        }

        /// <summary>
        /// Evento cuando cuando se selecciona un dato en la ayuda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void skAyudaOrigen_AyudaConDatos(object sender, EventArgs e)
        {
            if (((OrganizacionInfo)skAyudaOrganizacionOrigen.Contexto).Activo == EstatusEnum.Activo)
            {
                ValidaOrigenYdestino();
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Origen_Inactivo,
                                  MessageBoxButton.OK, MessageImage.Stop);
                skAyudaOrganizacionOrigen.LimpiarCampos();
                skAyudaOrganizacionOrigen.AsignarFoco();
            }
        }

        /// <summary>
        /// Evento cuando la ayuda no encuentran datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void skAyudaOrigen_AyudaLimpia(object sender, EventArgs e)
        {
            skAyudaOrganizacionOrigen.AsignarFoco();
        }

        /// <summary>
        /// Evento cuando la ayuda no encuentran datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void skAyudaDestino_AyudaLimpia(object sender, EventArgs e)
        {
            skAyudaOrganizacionDestino.AsignarFoco();
        }

        /// <summary>
        /// Evento cuando cuando se selecciona un dato en la ayuda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void skAyudaDestino_AyudaConDatos(object sender, EventArgs e)
        {
            if (((OrganizacionInfo)skAyudaOrganizacionDestino.Contexto).Activo == EstatusEnum.Activo)
            {
                ValidaOrigenYdestino();
                ValidarConfiguracionOrigenDestino();
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Destino_Inactivo,
                                  MessageBoxButton.OK, MessageImage.Stop);
                skAyudaOrganizacionDestino.LimpiarCampos();
                skAyudaOrganizacionDestino.AsignarFoco();
            }
        }

        /// <summary>
        /// Evento para limpiar las ayudas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        /// <summary>
        /// Evento para asignar foco a ruta origen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboEstatus_DropDownClosed(object sender, EventArgs e)
        {
            skAyudaOrganizacionOrigen.AsignarFoco();
        }

        #endregion Eventos

        #region Métodos

        /// <summary>
        /// Carga las ayudas
        /// </summary>
        private void InicializarAyudas()
        {
            skAyudaProveedor.txtClave.LostFocus +=txtClave_LostFocus;
            skAyudaProveedor.btnBusqueda.Click += BtnBusquedaOnClick;
            skAyudaProveedor.txtDescripcion.TextChanged += TxtDescripcionOnTextChanged;
            skAyudaProveedor.ObjetoNegocio = new ProveedorPL();
            skAyudaProveedor.Contexto = Contexto.Proveedor;
            Contexto.Proveedor.CodigoSAP = "0";
            skAyudaProveedor.AyudaConDatos += skAyudaProveedor_AyudaConDatos;
            skAyudaProveedor.AyudaLimpia += skAyudaProveedor_AyudaLimpia;

            skAyudaOrganizacionOrigen.ObjetoNegocio = new OrganizacionPL();
            skAyudaOrganizacionOrigen.Contexto = Contexto.OrganizacionOrigen;
            skAyudaOrganizacionOrigen.AyudaConDatos += skAyudaOrigen_AyudaConDatos;
            skAyudaOrganizacionOrigen.AyudaLimpia += skAyudaOrigen_AyudaLimpia;

            skAyudaOrganizacionDestino.ObjetoNegocio = new OrganizacionPL();
            skAyudaOrganizacionDestino.Contexto = Contexto.OrganizacionDestino;
            Contexto.OrganizacionDestino.OrganizacionID = 0;
            skAyudaOrganizacionDestino.AyudaConDatos += skAyudaDestino_AyudaConDatos;
            skAyudaOrganizacionDestino.AyudaLimpia += skAyudaDestino_AyudaLimpia;

        }

        /// <summary>
        /// Cambiar focus al cboEstatus
        /// </summary>
        private void BtnBusquedaOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            cboEstatus.Focus();
        }

        /// <summary>
        /// Inicializar el proveedor 
        /// </summary>
        private void TxtDescripcionOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            Contexto.Proveedor.ProveedorID = 0;
        }

        /// <summary>
        /// Inicializar el provvedor 
        /// </summary>
        private void txtClave_LostFocus(object sender, EventArgs e)
        {
            if (skAyudaProveedor.txtClave.Text.Trim().Equals(string.Empty))
            {
                skAyudaProveedor.txtClave.Text = "0";
            }
        }

        /// <summary>
        /// Metodo para reiniciar el paginador
        /// </summary>
        private void ReiniciarValoresPaginador()
        {
            ucPaginacion.Inicio = 1;
        }

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new TarifarioInfo
            {   
                Proveedor = new ProveedorInfo()
                {   
                    TipoProveedor = new TipoProveedorInfo(),
                },
                OrganizacionOrigen = new OrganizacionInfo
                    {
                        TipoOrganizacion = new TipoOrganizacionInfo()
                    },
                OrganizacionDestino = new OrganizacionInfo
                    {
                        TipoOrganizacion = new TipoOrganizacionInfo()
                    },
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado()
            };
        }

        /// <summary>
        /// Carga los valores del combo de estatus
        /// </summary>
        private void CargarCboEstatus()
        {
            try
            {
                IList<EstatusEnum> estatusEnums = Enum.GetValues(typeof(EstatusEnum)).Cast<EstatusEnum>().ToList();
                cboEstatus.ItemsSource = estatusEnums;
                cboEstatus.SelectedItem = EstatusEnum.Activo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Buscar
        /// </summary>
        private void Buscar()
        {
            ObtenerListaBitacoraTarifa(ucPaginacion.Inicio, ucPaginacion.Limite);
        }

        /// <summary>
        /// Método para ver gastos fijos
        /// </summary>
        /// <param name="boton"></param>
        private void VerGastosFijos(Button boton)
        {
            try
            {
                var tarifarioInfoSelecionado = (TarifarioInfo)Extensor.ClonarInfo(boton.CommandParameter);
                
                var agregarGastosFijos = new TarifarioGastosFijos(tarifarioInfoSelecionado)
                {
                    ucTitulo = { TextoTitulo = Properties.Resources.TarifarioGastosFijos_LblTitulo },
                };

                MostrarCentrado(agregarGastosFijos);
                ReiniciarValoresPaginador();
                Buscar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Tarifario_ErrorGastosFijos, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private TarifarioInfo ObtenerFiltros()
        {
            TarifarioInfo filtro = new TarifarioInfo();
            filtro.Proveedor = Contexto.Proveedor;
            filtro.OrganizacionOrigen = Contexto.OrganizacionOrigen;
            filtro.OrganizacionDestino = Contexto.OrganizacionDestino;
            filtro.Activo = Contexto.Activo;
            return filtro;
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaBitacoraTarifa(int inicio, int limite)
        {
            try
            {
                if (ucPaginacion.ContextoAnterior != null)
                {
                    bool contextosIguales = ucPaginacion.CompararObjetos(Contexto, ucPaginacion.ContextoAnterior);
                    if (!contextosIguales)
                    {
                        ucPaginacion.Inicio = 1;
                        inicio = 1;
                    }
                }

                var tarifarioPL = new TarifarioPL();
                TarifarioInfo filtros = ObtenerFiltros();
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<TarifarioInfo> resultadoInfo = tarifarioPL.ObtenerPorPagina(pagina, filtros);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatos.ItemsSource = resultadoInfo.Lista;
                    ucPaginacion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {

                    if (Contexto.Proveedor.ProveedorID != 0)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Tarifario_NoExistenConfiguracionEmbarqueProveedor, MessageBoxButton.OK, MessageImage.Error);
                        InicializaContexto();
                        skAyudaProveedor.Contexto = Contexto.Proveedor;
                        skAyudaOrganizacionOrigen.Contexto = Contexto.OrganizacionOrigen;
                        skAyudaOrganizacionDestino.Contexto = Contexto.OrganizacionDestino;
                        skAyudaProveedor.AsignarFoco();
                        Buscar();
                        return;
                    }
                    else if (Contexto.OrganizacionOrigen.OrganizacionID != 0 || Contexto.OrganizacionDestino.OrganizacionID != 0)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Tarifario_NoExistenConfiguracionEmbarqueOrganizacion, MessageBoxButton.OK, MessageImage.Error);
                        InicializaContexto();
                        skAyudaProveedor.Contexto = Contexto.Proveedor;
                        skAyudaOrganizacionOrigen.Contexto = Contexto.OrganizacionOrigen;
                        skAyudaOrganizacionDestino.Contexto = Contexto.OrganizacionDestino;
                        skAyudaProveedor.AsignarFoco();
                        Buscar();
                        return;
                    }

                    ucPaginacion.TotalRegistros = 0;
                    ucPaginacion.AsignarValoresIniciales();
                    gridDatos.ItemsSource = new List<TarifarioInfo>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Tarifario_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Tarifario_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Limpia los campos de la pantalla
        /// </summary>
        /// <returns></returns>
        private void LimpiarCampos()
        {
            ucPaginacion.TotalRegistros = 0;
            ucPaginacion.AsignarValoresIniciales();
            gridDatos.ItemsSource = new List<TarifarioInfo>();
            InicializaContexto();
            skAyudaProveedor.Contexto = Contexto.Proveedor;
            skAyudaOrganizacionOrigen.Contexto = Contexto.OrganizacionOrigen;
            skAyudaOrganizacionDestino.Contexto = Contexto.OrganizacionDestino;
            Buscar();
            skAyudaProveedor.txtClave.Focus();
        }

        /// <summary>
        /// Método que valida las reglas de negocio de las organizaciones de Destino
        /// </summary>
        private void ValidaOrigenYdestino()
        {
            if (string.IsNullOrWhiteSpace(skAyudaOrganizacionOrigen.Clave) || skAyudaOrganizacionOrigen.Clave == "0")
            {
                return;
            }

            if (skAyudaOrganizacionOrigen.Clave.Equals(skAyudaOrganizacionDestino.Clave))
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Tarifario_DestinoDuplicado,
                                  MessageBoxButton.OK, MessageImage.Stop);

                skAyudaOrganizacionDestino.LimpiarCampos();
                var tRequest = new TraversalRequest(FocusNavigationDirection.Previous);
                skAyudaOrganizacionDestino.MoveFocus(tRequest);
            }
        }

        /// <summary>
        /// Método que valida si hay proveedores activas
        /// </summary>
        private void ValidarProveedoresActivos()
        {
            try
            {
                IList<ProveedorInfo> lstProveedorActivo = new ProveedorPL().ObtenerProveedorActivo(EstatusEnum.Activo);
                if (lstProveedorActivo != null)
                {
                    ValidaOrganizacionesActivas();
                    return;
                }

                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Tarifario_NoExistenProveedores,
                MessageBoxButton.OK, MessageImage.Stop);
                BloquearPantalla();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Método que valida si hay organizaciones activas
        /// </summary>
        private void ValidaOrganizacionesActivas()
        {
            try
            {
                IList<OrganizacionInfo> lstOrganizacionActivo = new OrganizacionPL().ObtenerTodos(EstatusEnum.Activo);
                if (lstOrganizacionActivo != null)
                {
                    ValidarConfiguracionEmbarqueActivas();
                    return;
                }

                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Tarifario_NoExistenOrganizaciones,
                MessageBoxButton.OK, MessageImage.Stop);
                BloquearPantalla();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Método que valida si hay configuracion de embarques activas
        /// </summary>
        private void ValidarConfiguracionEmbarqueActivas()
        {
            try
            {
                TarifarioInfo filtros = ObtenerFiltrosOrigenDestino();
                IList<ConfiguracionEmbarqueInfo> lstConfiguracionEmbarqueInfos = new TarifarioPL().ConfiguracionEmbarqueActivas(filtros,EstatusEnum.Activo); 
                if (lstConfiguracionEmbarqueInfos != null)
                {
                    Buscar();
                    return;
                }

                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Tarifario_NoExistenConfiguracionEmbarque,
                MessageBoxButton.OK, MessageImage.Stop);
                BloquearPantalla();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Método para bloquear la pantalla de tarifario
        /// </summary>
        private void BloquearPantalla()
        {
            skAyudaProveedor.IsEnabled = false;
            skAyudaOrganizacionOrigen.IsEnabled = false;
            skAyudaOrganizacionDestino.IsEnabled = false;
            skAyudaProveedor.IsEnabled = false;
            gridDatos.IsEnabled = false;
            ucPaginacion.IsEnabled = false;
            btnBuscar.IsEnabled = false;
            cboEstatus.IsEnabled = false;
            btnLimpiar.IsEnabled = false;
        }

        /// <summary>
        /// Método Validar configuración organización origen – destino
        /// </summary>
        private bool ValidarConfiguracionOrigenDestino()
        {
            bool resp = true;
            try
            {
                if (skAyudaOrganizacionDestino.txtClave.Text != "0")
                {
                    if (skAyudaOrganizacionOrigen.txtClave.Text != "0")
                    {
                        TarifarioInfo filtros = ObtenerFiltrosOrigenDestino();
                        IList<ConfiguracionEmbarqueInfo> lstConfiguracionEmbarqueInfos =
                            new TarifarioPL().ConfiguracionEmbarqueActivas(filtros, EstatusEnum.Activo);
                        if (lstConfiguracionEmbarqueInfos == null)
                        {
                            resp = false;
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Tarifario_NoExistenConfiguracionOrigenDestino,
                            MessageBoxButton.OK, MessageImage.Stop);
                            skAyudaOrganizacionDestino.LimpiarCampos();
                            skAyudaOrganizacionDestino.AsignarFoco();
                        }

                    }
                    else
                    {
                        resp = false;
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.Tarifario_NoExistenConfiguracionOrigenDestino,
                        MessageBoxButton.OK, MessageImage.Stop);
                        skAyudaOrganizacionDestino.LimpiarCampos();
                        skAyudaOrganizacionDestino.AsignarFoco();
                    }
                }
                
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resp;
        }

        /// <summary>
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private TarifarioInfo ObtenerFiltrosOrigenDestino()
        {
            TarifarioInfo filtro = new TarifarioInfo();
            filtro.OrganizacionOrigen = Contexto.OrganizacionOrigen;
            filtro.OrganizacionDestino = Contexto.OrganizacionDestino;
            return filtro;
        }

        #endregion Métodos
    }
    
}
