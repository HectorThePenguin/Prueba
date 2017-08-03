using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SIE.WinForm.Controles;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using System.Text.RegularExpressions;
using SIE.Services.Servicios.BL;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for ProveedorEdicion.xaml
    /// </summary>
    public partial class ProveedorEdicion
    {
        #region Propiedades

        public List<ChoferInfo> Choferes { get; set; }

        private ProveedorInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ProveedorInfo)DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        private HashSet<int> choferesPorEliminar = new HashSet<int>();

        private ComisionInfo ComisionEditada = new ComisionInfo();
        private List<ComisionInfo> ComisionesOriginales = new List<ComisionInfo>();
        private List<ComisionInfo> ComisionesFinales = new List<ComisionInfo>();

        #endregion Propiedades

        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public ProveedorEdicion()
        {
            InitializeComponent();
            InicializaContexto();
            CargarTipoProveedor();
            CargarTiposComision();
        }

        /// <summary>
        /// Constructor para editar una entidad Proveedor Existente
        /// </summary>
        /// <param name="proveedorInfo"></param>
        public ProveedorEdicion(ProveedorInfo proveedorInfo)
        {
            ProveedorPL proveedorPl = new ProveedorPL();
            ProveedorInfo proveedorInfoCorreo = new ProveedorInfo();
            InitializeComponent();
            CargarTipoProveedor();
            CargarTiposComision();
            proveedorInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            proveedorInfoCorreo = proveedorPl.ObtenerPorIDConCorreo(proveedorInfo.ProveedorID);
            proveedorInfo.Correo = proveedorInfoCorreo != null ? proveedorInfoCorreo.Correo : "";
            Contexto = proveedorInfo;
        }

        #endregion Constructores

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
                ucPaginacionJaula.DatosDelegado += ObtenerListaJaula;
                ucPaginacionJaula.AsignarValoresIniciales();
                BuscarJaula();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Jaula_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Jaula_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }

            try
            {
                ucPaginacionCamion.DatosDelegado += ObtenerListaCamion;
                ucPaginacionCamion.AsignarValoresIniciales();
                BuscarCamion();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Camion_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Camion_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }

            try
            {
                ucPaginacionChofer.DatosDelegado += ObtenerListaChofer;
                ucPaginacionChofer.AsignarValoresIniciales();
                BuscarChofer();
                cmbEstatus.Focus();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Chofer_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Chofer_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }

            try
            {
                BuscarComisiones();
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Chofer_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Chofer_ErrorCargar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento que se ejecuta mientras se esta cerrando la ventana
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (confirmaSalir)
            {
                MessageBoxResult result = SkMessageBox.Show(this, Properties.Resources.Msg_CerrarSinGuardar, MessageBoxButton.YesNo,
                                                            MessageImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Contexto = null;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Evento que guarda los datos de la entidad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        /// <summary>
        /// Evento para regresar a la pantalla anterior
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Evento para crear un registro de jaula
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotonNuevoJaula_Click(object sender, RoutedEventArgs e)
        {
            EditarJaula(null);
        }

        /// <summary>
        /// Evento para crear un registro de camión
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotonNuevoCamion_Click(object sender, RoutedEventArgs e)
        {
            EditarCamion(null);
        }

        /// <summary>
        /// Evento para agregar un chofer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotonNuevoChofer_Click(object sender, RoutedEventArgs e)
        {
            BusquedaChoferes();
        }

        /// <summary>
        /// Evento para Editar un registro de jaula
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotonEditarJaula_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)e.Source;
            EditarJaula(btn);
        }

        /// <summary>
        /// Evento para Editar un registro de camión
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotonEditarCamion_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)e.Source;
            EditarCamion(btn);
        }

        /// <summary>
        /// Evento para quitar un chofer del proveedor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuitar_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)e.Source;
            var dtc = btn.DataContext as ChoferInfo;
            QuitarChofer(dtc);
        }

        /// <summary>
        /// Agrega la tarifa capturada en el la lista de tarifas para el proveedor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAgregarTarifa_Click(object sender, RoutedEventArgs e)
        {
            AgregarALista();
        }

        /// <summary>
        /// Evento para Editar un registro de comsion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditarComision_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)e.Source;
            EditarComision(btn);
        }

        /// <summary>
        /// Borra la comision seleccionada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBorrarComision_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)e.Source;
            BorrarComision(btn);
        }

        /// <summary>
        /// Cancela la edicion de la comision
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelarEdicionComision_Click(object sender, RoutedEventArgs e)
        {
            CancelarEdicionComision();
        }

        /// <summary>
        /// Actualiza los datos de la comision
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnActualizarComision_Click(object sender, RoutedEventArgs e)
        {
            ActualizarComision();
        }


        #endregion Eventos


        #region Métodos
        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new ProveedorInfo
                           {
                               UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                               RetencionISR = new RetencionInfo(),
                               RetencionIVA = new RetencionInfo(),
                               IVA = new IvaInfo()
                           };
        }

        /// <summary>
        /// Metodo que valida los datos para guardar
        /// </summary>
        /// <returns></returns>
        private bool ValidaGuardar()
        {
            bool resultado = true;
            string mensaje = string.Empty;
            try
            {
                if (txtDescripcion.Text == string.Empty)
                {
                    resultado = false;
                    txtDescripcion.Focus();
                }
                int proveedorId = Extensor.ValorEntero(txtProveedorId.Text);
                string descripcion = txtDescripcion.Text;

                var proveedorPL = new ProveedorPL();

                ProveedorInfo proveedor = proveedorPL.ObtenerPorDescripcion(descripcion);

                if (proveedor != null && (proveedorId == 0 || proveedorId != proveedor.ProveedorID))
                {
                    resultado = false;
                    mensaje = string.Format(Properties.Resources.ProveedorEdicion_MsgDescripcionExistente,
                                                  proveedor.ProveedorID);

                    txtDescripcion.Focus();
                }

                if (resultado && !ValidarCorreoElectronico(txtCorreo.Text.Trim()))
                {
                    resultado = false;
                    mensaje = string.Format(Properties.Resources.Proveedor_CorreoRequerido,
                                                  proveedor.ProveedorID);
                }
                if (ckbRetencionISR.IsChecked.HasValue && ckbRetencionISR.IsChecked.Value)
                {
                    if (Contexto.RetencionISR == null || Contexto.RetencionISR.RetencionID == 0)
                    {
                        resultado = false;
                        mensaje = Properties.Resources.Proveedor_RetencionISRRequerido;
                    }
                }
                if (ckbRetencionIVA.IsChecked.HasValue && ckbRetencionIVA.IsChecked.Value)
                {
                    if (Contexto.RetencionIVA == null || Contexto.RetencionIVA.RetencionID == 0)
                    {
                        resultado = false;
                        mensaje = Properties.Resources.Proveedor_RetencionIVARequerido;
                    }
                }
                if (ckbIVA.IsChecked.HasValue && ckbIVA.IsChecked.Value)
                {
                    if (Contexto.IVA == null || Contexto.IVA.IvaID == 0)
                    {
                        resultado = false;
                        mensaje = Properties.Resources.Proveedor_IVARequerido;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            if (!string.IsNullOrWhiteSpace(mensaje))
            {
                SkMessageBox.Show(this, mensaje, MessageBoxButton.OK, MessageImage.Warning);
            }

            return resultado;
        }

        private Boolean ValidarCorreoElectronico(String email)
        {
            String expresion;
            Boolean resultado = false;
            if (email.Trim().Length > 0)
            {
                expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
                if (Regex.IsMatch(email, expresion))
                {
                    if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                    {
                        resultado = true;
                    }
                    else
                    {
                        resultado = false;
                    }
                }
                else
                {
                    resultado = false;
                }
            }
            else
            {
                resultado = true;
            }

            return resultado;
        }

        /// <summary>
        /// Guardar los datos del proveedor
        /// </summary>
        private void Guardar()
        {
            bool guardar = ValidaGuardar();

            if (guardar)
            {
                try
                {
                    ObtenerChoferesProveedor();
                    Contexto.Choferes = Choferes;
                    var proveedorPL = new ProveedorPL();
                    Contexto.Comisiones = ComisionesFinales;
                    if (Contexto.TipoProveedor.TipoProveedorID == TipoProveedorEnum.Comisionistas.GetHashCode())
                    {
                        Contexto.RetencionISR.Activo = (ckbRetencionISR.IsChecked.HasValue &&
                                                        ckbRetencionISR.IsChecked.Value) ? EstatusEnum.Activo : EstatusEnum.Inactivo;
                        Contexto.RetencionIVA.Activo = (ckbRetencionIVA.IsChecked.HasValue &&
                                                        ckbRetencionIVA.IsChecked.Value) ? EstatusEnum.Activo : EstatusEnum.Inactivo;
                        Contexto.IVA.Activo = (ckbIVA.IsChecked.HasValue && ckbIVA.IsChecked.Value)
                            ? EstatusEnum.Activo : EstatusEnum.Inactivo;
                    }
                    proveedorPL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK,
                                      MessageImage.Correct);
                    confirmaSalir = false;
                    Close();
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.Proveedor_ErrorGuardar, MessageBoxButton.OK,
                                      MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.Proveedor_ErrorGuardar, MessageBoxButton.OK,
                                      MessageImage.Error);
                }
            }
        }

        private void ObtenerChoferesProveedor()
        {
            var pagina = new PaginacionInfo
                             {
                                 Inicio = 1,
                                 Limite = 1000
                             };
            var choferPL = new ChoferPL();
            ResultadoInfo<ChoferInfo> choferes = choferPL.ObtenerChoferesDeProveedorPorPagina(pagina, Contexto);
            if (choferes != null && choferes.Lista != null)
            {
                if (Choferes != null)
                {
                    if (choferesPorEliminar.Any())
                    {
                        List<int> choferesActuales = choferes.Lista.Select(id => id.ChoferID).ToList();
                        choferesActuales = choferesActuales.Except(choferesPorEliminar).ToList();
                        choferes.Lista = (from lst in choferes.Lista
                                          from act in choferesActuales
                                          where lst.ChoferID == act
                                          select lst).ToList();
                        Choferes = Choferes.Where(eli => !eli.Eliminado).ToList();
                    }
                    Choferes = Choferes.Union(choferes.Lista).ToList();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CargarTipoProveedor()
        {
            try
            {
                var proveedorPL = new TipoProveedorPL();
                IList<TipoProveedorInfo> listaTipoProveedor = proveedorPL.ObtenerTodos();
                cmbTipoProveedor.ItemsSource = listaTipoProveedor;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.Proveedor_ErrorCargarTipoProveedor, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        /// <summary>
        /// Carta el catalogo de Tipos de comision
        /// </summary>
        private void CargarTiposComision()
        {
            ComisionPL comisioPL = new ComisionPL();
            List<TipoComisionInfo> lsComisiones = new List<TipoComisionInfo>();
            lsComisiones = comisioPL.obtenerTiposComision();

            cboTipoComision.ItemsSource = lsComisiones;
            cboTipoComision.DisplayMemberPath = "Descripcion";
            cboTipoComision.SelectedIndex = 0;
        }
        /// <summary>
        /// Buscar
        /// </summary>
        public void BuscarJaula()
        {
            ObtenerListaJaula(ucPaginacionJaula.Inicio, ucPaginacionJaula.Limite);
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void BuscarCamion()
        {
            ObtenerListaCamion(ucPaginacionCamion.Inicio, ucPaginacionCamion.Limite);
        }

        /// <summary>
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private JaulaInfo ObtenerFiltrosJaula()
        {
            try
            {
                var filtro =
                    new JaulaInfo
                        {
                            JaulaID = 0,
                            PlacaJaula = string.Empty,
                            Proveedor = Contexto,
                            Activo = EstatusEnum.Activo,
                        };
                return filtro;
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private CamionInfo ObtenerFiltrosCamion()
        {
            try
            {
                var filtro =
                    new CamionInfo
                    {
                        CamionID = 0,
                        PlacaCamion = string.Empty,
                        Proveedor = Contexto,
                        Activo = EstatusEnum.Activo,
                    };
                return filtro;
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaJaula(int inicio, int limite)
        {
            try
            {
                var jaulaPL = new JaulaPL();
                JaulaInfo filtros = ObtenerFiltrosJaula();
                if (ucPaginacionJaula.ContextoAnterior != null)
                {
                    bool contextosIguales = ucPaginacionJaula.CompararObjetos(Contexto, ucPaginacionJaula.ContextoAnterior);
                    if (!contextosIguales)
                    {
                        ucPaginacionJaula.Inicio = 1;
                        inicio = 1;
                    }
                }
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<JaulaInfo> resultadoInfo = jaulaPL.ObtenerPorPagina(pagina, filtros);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatosJaula.ItemsSource = resultadoInfo.Lista;
                    ucPaginacionJaula.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacionJaula.TotalRegistros = 0;
                    gridDatosJaula.ItemsSource = new List<JaulaInfo>();
                }
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaCamion(int inicio, int limite)
        {
            try
            {
                var camionPL = new CamionPL();
                CamionInfo filtros = ObtenerFiltrosCamion();
                if (ucPaginacionCamion.ContextoAnterior != null)
                {
                    bool contextosIguales = ucPaginacionCamion.CompararObjetos(Contexto, ucPaginacionCamion.ContextoAnterior);
                    if (!contextosIguales)
                    {
                        ucPaginacionCamion.Inicio = 1;
                        inicio = 1;
                    }
                }
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<CamionInfo> resultadoInfo = camionPL.ObtenerPorPagina(pagina, filtros);
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    gridDatosCamion.ItemsSource = resultadoInfo.Lista;
                    ucPaginacionCamion.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacionCamion.TotalRegistros = 0;
                    gridDatosCamion.ItemsSource = new List<CamionInfo>();
                }
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Método para editar/crear una Jaula
        /// </summary>
        /// <param name="boton"></param>
        private void EditarJaula(Button boton)
        {
            try
            {
                JaulaEdicion jaulaEdicion = null;
                if (boton == null)
                {
                    jaulaEdicion =
                        new JaulaEdicion(Contexto)
                            {
                                ucTitulo = { TextoTitulo = Properties.Resources.Jaula_Nuevo_Titulo },
                            };

                }
                else
                {
                    var jaulaInfoSelecionado = (JaulaInfo)boton.CommandParameter;
                    if (jaulaInfoSelecionado != null)
                    {
                        var jaulaInfo =
                            new JaulaInfo
                                {
                                    JaulaID = jaulaInfoSelecionado.JaulaID,
                                    PlacaJaula = jaulaInfoSelecionado.PlacaJaula,
                                    Proveedor = jaulaInfoSelecionado.Proveedor,
                                    Capacidad = jaulaInfoSelecionado.Capacidad,
                                    Secciones = jaulaInfoSelecionado.Secciones,
                                    Activo = jaulaInfoSelecionado.Activo,
                                };

                        jaulaEdicion =
                            new JaulaEdicion(jaulaInfo)
                                {
                                    ucTitulo = { TextoTitulo = Properties.Resources.Jaula_Editar_Titulo }
                                };
                    }
                }
                if (jaulaEdicion != null)
                {
                    bool habilitar = Contexto == null;
                    jaulaEdicion.BloqueaProveedor(habilitar);
                    MostrarCentrado(jaulaEdicion);
                    ucPaginacionJaula.Inicio = 1;
                    BuscarJaula();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Jaula_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }


        /// <summary>
        /// Marca un elemento seleccionado como borrado
        /// </summary>
        /// <param name="boton"></param>
        private void BorrarComision(Button boton)
        {
            var comisionInfoSelecionado = (ComisionInfo)boton.CommandParameter;

            for (int i = 0; i < ComisionesFinales.Count; i++)
            {
                if (ComisionesFinales[i].ProveedorComisionID == comisionInfoSelecionado.ProveedorComisionID
                    && ComisionesFinales[i].TipoComisionID == comisionInfoSelecionado.TipoComisionID
                    && ComisionesFinales[i].Tarifa == comisionInfoSelecionado.Tarifa)
                {
                    ComisionesFinales[i].Accion = 3;
                    ComisionesFinales[i].Usuario = AuxConfiguracion.ObtenerUsuarioLogueado();
                }
            }
            CargarComisiones(ComisionesFinales);
        }


        /// <summary>
        /// Carga una lista de comisiones en el grid
        /// </summary>
        /// <param name="comisiones"></param>
        private void CargarComisiones(List<ComisionInfo> comisiones)
        {
            txtTarifa.Text = "";
            cboTipoComision.SelectedIndex = 0;
            grdTarifas.ItemsSource = null;
            grdTarifas.ItemsSource = new List<ComisionInfo>();
            grdTarifas.ItemsSource = (from comision in comisiones where comision.Accion != 3 select comision).ToList<ComisionInfo>();
            grdTarifas.UpdateLayout();

        }

        /// <summary>
        /// Prepara el formulario para la edicion de la comision
        /// </summary>
        /// <param name="boton"></param>
        private void EditarComision(Button boton)
        {
            var comisionInfoSelecionado = (ComisionInfo)boton.CommandParameter;
            int index = 0;
            ComisionEditada = comisionInfoSelecionado;
            var comisionBuscada = new TipoComisionInfo
            {
                TipoComisionID = ComisionEditada.TipoComisionID,
                Descripcion = ComisionEditada.DescripcionComision
            };


            btnAgregarComision.Visibility = System.Windows.Visibility.Hidden;
            btnActualizarComision.Visibility = System.Windows.Visibility.Visible;
            btnCancelarEdicionComision.Visibility = System.Windows.Visibility.Visible;

            txtTarifa.Text = ComisionEditada.Tarifa.ToString();
            cboTipoComision.SelectedValue = ComisionEditada.TipoComisionID;

            foreach (TipoComisionInfo comision in cboTipoComision.Items)
            {

                if (comision.TipoComisionID == ComisionEditada.TipoComisionID)
                {
                    cboTipoComision.SelectedIndex = index;
                    cboTipoComision.Text = comision.Descripcion;
                }
                index++;
            }


        }

        /// <summary>
        /// Cancela la edicion de Comisiones
        /// </summary>
        private void CancelarEdicionComision()
        {
            btnAgregarComision.Visibility = System.Windows.Visibility.Visible;
            btnActualizarComision.Visibility = System.Windows.Visibility.Hidden;
            btnCancelarEdicionComision.Visibility = System.Windows.Visibility.Hidden;
            ComisionEditada = new ComisionInfo();
        }

        /// <summary>
        /// Actualiza la lista de comisiones con respecto al elemento modificado
        /// </summary>
        private void ActualizarComision()
        {
            decimal valor;
            if (decimal.TryParse(txtTarifa.Text.ToString(), out valor))
            {
                if (cboTipoComision.SelectedIndex >= 0)
                {
                    for (int i = 0; i < ComisionesFinales.Count; i++)
                    {
                        if (ComisionesFinales[i].ProveedorComisionID == ComisionEditada.ProveedorComisionID
                            && ComisionesFinales[i].TipoComisionID == ComisionEditada.TipoComisionID
                            && ComisionesFinales[i].Tarifa == ComisionEditada.Tarifa)
                        {
                            if (ComisionEditada.ProveedorComisionID > 0)
                            {
                                ComisionesFinales[i].Accion = 2;
                            }
                            else
                            {
                                ComisionesFinales[i].Accion = 1;
                            }

                            ComisionesFinales[i].DescripcionComision = cboTipoComision.Text;
                            ComisionesFinales[i].Tarifa = decimal.Parse(txtTarifa.Text);
                            ComisionesFinales[i].TipoComisionID = int.Parse(cboTipoComision.SelectedValue.ToString());
                            ComisionesFinales[i].Usuario = AuxConfiguracion.ObtenerUsuarioLogueado();

                        }
                    }
                    btnCancelarEdicionComision.Visibility = System.Windows.Visibility.Hidden;
                    btnActualizarComision.Visibility = System.Windows.Visibility.Hidden;
                    btnAgregarComision.Visibility = System.Windows.Visibility.Visible;
                    txtTarifa.Text = decimal.Parse("0").ToString();
                    cboTipoComision.SelectedIndex = 0;
                    CargarComisiones(ComisionesFinales);
                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.ProveedorEdicion_TipoComisionObligatoria, MessageBoxButton.OK, MessageImage.Error);
                }
            }
            else
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.ProveedorEdicion_ComisionInvalida, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Método para editar un camión
        /// </summary>
        /// <param name="boton"></param>
        private void EditarCamion(Button boton)
        {
            try
            {
                CamionEdicion camionEdicion = null;
                if (boton == null)
                {
                    camionEdicion =
                        new CamionEdicion(Contexto)
                            {
                                ucTitulo = { TextoTitulo = Properties.Resources.Camion_Nuevo_Titulo }
                            };
                }
                else
                {
                    var camionInfoSelecionado = (CamionInfo)boton.CommandParameter;
                    if (camionInfoSelecionado != null)
                    {
                        var camionInfo =
                            new CamionInfo
                                {
                                    CamionID = camionInfoSelecionado.CamionID,
                                    PlacaCamion = camionInfoSelecionado.PlacaCamion,
                                    Proveedor = camionInfoSelecionado.Proveedor,
                                    Activo = camionInfoSelecionado.Activo,
                                };

                        camionEdicion =
                            new CamionEdicion(camionInfo)
                                {
                                    ucTitulo = { TextoTitulo = Properties.Resources.Camion_Editar_Titulo }
                                };
                    }
                }
                if (camionEdicion != null)
                {
                    bool habilitar = Contexto == null;
                    camionEdicion.BloqueaProveedor(habilitar);
                    MostrarCentrado(camionEdicion);
                    ucPaginacionCamion.Inicio = 1;
                    BuscarCamion();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Jaula_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #region Choferes
        /// <summary>
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private ProveedorInfo ObtenerFiltrosChofer()
        {
            try
            {
                var filtro =
                    new ProveedorInfo
                    {
                        ProveedorID = (DataContext as ProveedorInfo).ProveedorID,
                        Activo = EstatusEnum.Activo,
                    };
                return filtro;
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Buscar
        /// </summary>
        public void BuscarChofer()
        {
            ObtenerListaChofer(ucPaginacionChofer.Inicio, ucPaginacionChofer.Limite);
        }

        /// <summary>
        /// Obtiene la lista de comisiones asignadas al proveedor
        /// </summary>
        public void BuscarComisiones()
        {
            ComisionPL comisionPL = new ComisionPL();
            int iProveedorID = 0;

            iProveedorID = int.Parse(txtProveedorId.Text);
            ComisionesOriginales = comisionPL.obtenerComisionesProveedor(iProveedorID);
            ComisionesFinales = ComisionesOriginales;
            CargarComisiones(ComisionesOriginales);
        }

        /// <summary>
        /// Obtiene la lista para mostrar en el grid
        /// </summary>
        private void ObtenerListaChofer(int inicio, int limite)
        {
            try
            {
                var choferPL = new ChoferPL();
                var filtros = ObtenerFiltrosChofer();

                if (ucPaginacionChofer.ContextoAnterior != null)
                {
                    bool contextosIguales = ucPaginacionChofer.CompararObjetos(Contexto, ucPaginacionChofer.ContextoAnterior);
                    if (!contextosIguales)
                    {
                        ucPaginacionChofer.Inicio = 1;
                        inicio = 1;
                    }
                }
                var pagina = new PaginacionInfo { Inicio = inicio, Limite = limite };
                ResultadoInfo<ChoferInfo> resultadoInfo = choferPL.ObtenerChoferesDeProveedorPorPagina(pagina, filtros);
                Choferes = resultadoInfo != null ? resultadoInfo.Lista as List<ChoferInfo> : new List<ChoferInfo>();
                if (resultadoInfo != null && resultadoInfo.Lista != null &&
                    resultadoInfo.Lista.Count > 0)
                {
                    //gridDatosChofer.ItemsSource = resultadoInfo.Lista;
                    ucPaginacionChofer.TotalRegistros = resultadoInfo.TotalRegistros;
                }
                else
                {
                    ucPaginacionChofer.TotalRegistros = 0;
                }
                ActualizarGridChoferes();
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private void BusquedaChoferes()
        {
            var skAyuda = new ControlAyudaBusqueda
                              {
                                  MetodoInvocacion = "ObtenerPorPagina",
                                  MensajeAgregar = Properties.Resources.Chofer_Seleccionar,
                                  MensajeCerrar = Properties.Resources.Chofer_SalirSinSeleccionar,
                                  EncabezadoClaveGrid = "ID",
                                  EncabezadoDescripcionGrid = "Nombre Completo",
                                  ConceptoBusqueda = Properties.Resources.LeyendaAyudaBusquedaChofer,
                                  TituloBusqueda = Properties.Resources.BusquedaChofer_Titulo,
                                  CampoClave = "ChoferID",
                                  CampoDescripcion = "NombreCompleto",
                                  LongitudMaximaCampoDescripcion = 200,
                                  Contexto = new ChoferInfo
                                                 {
                                                     Nombre = string.Empty,
                                                     ApellidoMaterno = string.Empty,
                                                     ApellidoPaterno = string.Empty,
                                                     Activo = EstatusEnum.Activo
                                                 },
                                  ObjetoNegocio = new ChoferPL()
                              };
            var b = skAyuda.ShowDialog();
            if (b.HasValue && skAyuda.Contexto != null && (skAyuda.Contexto as ChoferInfo).ChoferID > 0)
            {
                var seleccionado = skAyuda.Contexto as ChoferInfo;
                if (Choferes.Count(e => e.ChoferID == seleccionado.ChoferID) == 0)
                {
                    if (choferesPorEliminar.Contains(seleccionado.ChoferID))
                    {
                        choferesPorEliminar.Remove(seleccionado.ChoferID);
                    }
                    Choferes.Add(seleccionado);
                    ActualizarGridChoferes();
                }
            }
        }

        private void QuitarChofer(ChoferInfo chofer)
        {
            ChoferInfo choferEliminado = Choferes.FirstOrDefault(id => id.ChoferID == chofer.ChoferID);
            if (choferEliminado != null)
            {
                choferesPorEliminar.Add(chofer.ChoferID);
                Choferes.Remove(chofer);
            }
            ActualizarGridChoferes();
        }

        void ActualizarGridChoferes()
        {
            gridDatosChofer.ItemsSource = null;
            gridDatosChofer.ItemsSource = Choferes;
        }

        #endregion

        private void cmbTipoProveedor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {


                if (cmbTipoProveedor.SelectedValue != null
                    && (cmbTipoProveedor.SelectedValue.ToString().Trim() == TipoProveedorEnum.ProveedoresFletes.GetHashCode().ToString().Trim()
                    || cmbTipoProveedor.SelectedValue.ToString().Trim() == TipoProveedorEnum.Comisionistas.GetHashCode().ToString().Trim()
                    ))
                {
                    txtCorreo.IsEnabled = true;
                }
                else
                {
                    txtCorreo.Text = "";
                    txtCorreo.IsEnabled = false;
                }
                if (Contexto.TipoProveedor != null &&
                    Contexto.TipoProveedor.TipoProveedorID == TipoProveedorEnum.Comisionistas.GetHashCode())
                {
                    stpRetenciones.Visibility = Visibility.Visible;
                    CargarConfiguracionRetencion();
                    CargarComboIVA();
                    CargarComboRetencion(TipoRetencionEnum.ISR.ToString());
                    CargarComboRetencion(TipoRetencionEnum.IVA.ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.Proveedor_ErrorTipoProveedor, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void AgregarALista()
        {
            ComisionInfo tipoInfo = new ComisionInfo();
            ComisionInfo tipoInfoAux = new ComisionInfo();
            bool yaRegistrado = false;
            decimal valor = 0;
            //decimal.TryParse(txtTarifa.Text, out tipoInfo.Tarifa);
            bool esDecimalValido = Decimal.TryParse(txtTarifa.Text, out valor);

            if (esDecimalValido && decimal.Parse(txtTarifa.Text.ToString()) > 0)
            {
                tipoInfo.Usuario = AuxConfiguracion.ObtenerUsuarioLogueado();
                tipoInfo.ProveedorID = int.Parse(txtProveedorId.Text);
                tipoInfo.Tarifa = decimal.Parse(txtTarifa.Text.ToString());
                tipoInfo.DescripcionComision = cboTipoComision.Text;
                tipoInfo.TipoComisionID = ((TipoComisionInfo)cboTipoComision.SelectionBoxItem).TipoComisionID;
                tipoInfo.Accion = 1;

                List<ComisionInfo> lsTarifa = new List<ComisionInfo>();
                lsTarifa = (from ComisionInfo info in grdTarifas.Items select info).ToList<ComisionInfo>();

                yaRegistrado = lsTarifa.Any(tarifa => tarifa.TipoComisionID == tipoInfo.TipoComisionID && tarifa.Tarifa == tipoInfo.Tarifa);
                if (!yaRegistrado)
                {
                    ComisionesFinales.Add(tipoInfo);
                    grdTarifas.ItemsSource = (from comision in ComisionesFinales where comision.Accion != 3 select comision).ToList<ComisionInfo>();
                }
                else
                {
                    SkMessageBox.Show(this, Properties.Resources.ProveedorEdicion_ComisionYaCapturada, MessageBoxButton.OK,
                        MessageImage.Warning);
                }
                txtTarifa.Text = "";
            }
            else
            {
                SkMessageBox.Show(this, Properties.Resources.ProveedorEdicion_ComisionInvalida, MessageBoxButton.OK,
                                      MessageImage.Error);
            }

        }

        private void CargarComboRetencion(string tipoRetencion)
        {
            var retencionPL = new RetencionPL();
            IList<RetencionInfo> retenciones = retencionPL.ObtenerTodos(EstatusEnum.Activo);
            if (retenciones != null && retenciones.Any())
            {
                List<RetencionInfo> retencionesFiltradas =
                    retenciones.Where(
                        ret => ret.TipoRetencion.ToUpper().Equals(tipoRetencion.ToUpper())).ToList();
                if (retencionesFiltradas.Any())
                {
                    var retencionSeleccione = new RetencionInfo
                    {
                        RetencionID = 0,
                        TipoRetencion = Properties.Resources.cbo_Seleccione
                    };
                    retencionesFiltradas.Insert(0, retencionSeleccione);
                    if (tipoRetencion.Equals(TipoRetencionEnum.ISR.ToString(), StringComparison.CurrentCultureIgnoreCase))
                    {
                        cboRetencionISR.ItemsSource = retencionesFiltradas;
                        if (Contexto.RetencionISR == null)
                        {
                            Contexto.RetencionISR = new RetencionInfo
                                                    {
                                                        RetencionID = 0
                                                    };
                        }
                        cboRetencionISR.SelectedIndex = 0;
                    }
                    else
                    {
                        cboRetencionIVA.ItemsSource = retencionesFiltradas;
                        if (Contexto.RetencionIVA == null)
                        {
                            Contexto.RetencionIVA = new RetencionInfo
                            {
                                RetencionID = 0
                            };
                        }
                        cboRetencionIVA.SelectedIndex = 0;
                    }

                }

            }
        }

        private void CargarComboIVA()
        {
            var ivaPL = new IvaPL();
            IList<IvaInfo> listaIvas = ivaPL.ObtenerTodos(EstatusEnum.Activo);
            if (listaIvas != null && listaIvas.Any())
            {
                var ivaSeleccione = new IvaInfo
                {
                    IvaID = 0,
                    Descripcion = Properties.Resources.cbo_Seleccione
                };
                listaIvas.Insert(0, ivaSeleccione);
                cboIVA.ItemsSource = listaIvas;
                if (Contexto.IVA == null)
                {
                    Contexto.IVA = new IvaInfo();
                }
                cboIVA.SelectedIndex = 0;
            }

        }

        private void CargarConfiguracionRetencion()
        {
            var proveedorRetencionBL = new ProveedorRetencionBL();
            IList<ProveedorRetencionInfo> listaRetenciones =
                proveedorRetencionBL.ObtenerPorProveedorID(Contexto.ProveedorID);

            if (listaRetenciones != null && listaRetenciones.Any())
            {
                ProveedorRetencionInfo retencionISR =
                    listaRetenciones.FirstOrDefault(
                        ret => !string.IsNullOrWhiteSpace(ret.Retencion.TipoRetencion) && ret.Retencion.TipoRetencion.ToUpper().Equals(TipoRetencionEnum.ISR.ToString().ToUpper()));
                if (retencionISR != null)
                {
                    Contexto.RetencionISR = retencionISR.Retencion;
                    ckbRetencionISR.IsChecked = retencionISR.Activo == EstatusEnum.Activo;
                }

                ProveedorRetencionInfo retencionIVA =
                    listaRetenciones.FirstOrDefault(
                        ret => !string.IsNullOrWhiteSpace(ret.Retencion.TipoRetencion) && ret.Retencion.TipoRetencion.ToUpper().Equals(TipoRetencionEnum.IVA.ToString().ToUpper()));
                if (retencionIVA != null)
                {
                    Contexto.RetencionIVA = retencionIVA.Retencion;
                    ckbRetencionIVA.IsChecked = retencionIVA.Activo == EstatusEnum.Activo;
                }
                ProveedorRetencionInfo iva = listaRetenciones.FirstOrDefault(ret => ret.Iva != null && ret.Iva.IvaID > 0);
                if (iva != null)
                {
                    Contexto.IVA = iva.Iva;
                    ckbIVA.IsChecked = iva.Activo == EstatusEnum.Activo;
                }
            }
        }

        #endregion Métodos

        private void CheckBoxRetenciones_OnChecked(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;
            switch (check.Name)
            {
                case "ckbRetencionISR":
                    cboRetencionISR.IsEnabled = check.IsChecked ?? false;
                    if (!cboRetencionISR.IsEnabled)
                    {
                        Contexto.RetencionISR.Activo = EstatusEnum.Inactivo;
                    }
                    break;
                case "ckbRetencionIVA":
                    cboRetencionIVA.IsEnabled = check.IsChecked ?? false;
                    if (!cboRetencionIVA.IsEnabled)
                    {
                        Contexto.RetencionIVA.Activo = EstatusEnum.Inactivo;
                    }
                    break;
                case "ckbIVA":
                    cboIVA.IsEnabled = check.IsChecked ?? false;
                    if (!cboIVA.IsEnabled)
                    {
                        Contexto.IVA.Activo = EstatusEnum.Inactivo;
                    }
                    break;
            }
        }
    }
}
