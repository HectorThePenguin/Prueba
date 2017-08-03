using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;


namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para RecibirProductoAlmacenReplica.xaml
    /// </summary>
    public partial class RecibirProductoAlmacenReplica
    {

        #region VARIABLES

        private UsuarioInfo usuario;
        private AlmacenInfo almacenGeneral;
        private SolicitudProductoReplicaDetalleInfo productoRow;
        private readonly SolicitudProductoBL solicitudProductoBL;
        List<ProductoInfo> listaProductos = new List<ProductoInfo>();
        private int _esSukarne;
        
        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private SolicitudProductoReplicaInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (SolicitudProductoReplicaInfo)DataContext;
            }
            set { DataContext = value; }
        }
        
        #endregion VARIABLES

        #region CONSTRUCTOR

        public RecibirProductoAlmacenReplica()
        {
            solicitudProductoBL = new SolicitudProductoBL();
            InitializeComponent();
        }

        #endregion CONSTRUCTOR

        #region METODOS

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            usuario = new UsuarioPL().ObtenerPorID(AuxConfiguracion.ObtenerUsuarioLogueado());
            usuario.OrganizacionID = usuario.Organizacion.OrganizacionID;
            almacenGeneral = ObtenerAlmacenGeneral(usuario.Organizacion.OrganizacionID);
            Contexto =
                new SolicitudProductoReplicaInfo
                {
                    OrganizacionID = usuario.OrganizacionID,
                    AlmacenID = almacenGeneral.AlmacenID,
                    Solicitud = InicializaContextoSolicitud(),
                    Detalle = new List<SolicitudProductoReplicaDetalleInfo>(),
                    Activo = EstatusEnum.Activo,
                    Guardar = false
                };
        }

        /// <summary>
        /// Obtiene el Contexto de la solicitud
        /// </summary>
        /// <returns></returns>
        private FolioSolicitudInfo InicializaContextoSolicitud()
        {
            var solicitud = new FolioSolicitudInfo
            {
                Descripcion = string.Empty,
                Usuario = usuario,
                UsuarioIDEntrega = usuario.UsuarioID,
                OrganizacionID = usuario.OrganizacionID,
                Producto = new ProductoInfo(),
                Cantidad = decimal.Zero,
                ClaseCostoProducto = new ClaseCostoProductoInfo(),
                AlmacenID = 0,
                FolioSolicitud = 0,
                EstatusID = Estatus.SolicitudProductoEntregado.GetHashCode(),
            };
            return solicitud;
        }

        /// <summary>
        /// Obtiene el almacén general que tenga configurado
        /// el usuario.
        /// </summary>
        private AlmacenInfo ObtenerAlmacenGeneral(int organizacionId)
        {
            var almacenDaL = new AlmacenPL();
            AlmacenInfo almacenInfo = null;
            IList<AlmacenInfo> almacenes = almacenDaL.ObtenerAlmacenPorOrganizacion(organizacionId);
            if (almacenes != null && almacenes.Count > 0)
            {
                almacenInfo = almacenes.FirstOrDefault(a => a.TipoAlmacenID == (int)TipoAlmacenEnum.GeneralGanadera
                                                            && a.Activo == EstatusEnum.Activo);
            }
            return almacenInfo;
        }

        /// <summary>
        /// Obtiene los filtros de la consulta
        /// </summary>
        /// <returns></returns>
        private SolicitudProductoReplicaInfo ObtenerFiltros()
        {
            try
            {
                return Contexto;
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private void ConsultarProductos()
        {
            try
            {
                var productoPL = new ProductoPL();
                listaProductos = productoPL.ObtenerPorEstados(EstatusEnum.Activo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.RecibirProductoAlmacen_MsgProductos,
                                          MessageBoxButton.OK,
                                          MessageImage.Warning);
            }
        }

        /// <summary>
        /// Obtiene la lista de productos de un folio de consulta
        /// </summary>
        private void ObtenerSolicitudProducto()
        {
            try
            {
                var SolicitudProductoBL = new SolicitudProductoReplicaBL();
                Contexto.OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
                SolicitudProductoReplicaInfo filtros = ObtenerFiltros();
                SolicitudProductoReplicaInfo resultadoInfo = SolicitudProductoBL.ObtenerPorID(filtros);
                if (resultadoInfo != null && resultadoInfo.Detalle != null && resultadoInfo.Detalle.Count > 0)
                {
                    var um = new UnidadMedicionPL();
                    resultadoInfo.Detalle.ToList().ForEach(e =>
                    {
                        e.Producto = listaProductos.FirstOrDefault(x => x.ProductoId == e.ProductoID);
                        e.Producto.UnidadMedicion = um.ObtenerPorID(e.Producto.UnidadId);
                        e.UsuarioCreacionID = usuario.UsuarioID;
                    });
                    gridDatos.ItemsSource = resultadoInfo.Detalle;
                    skAyudaFolio.IsEnabled = false;
                    var recibido = resultadoInfo.Detalle.Any(a => a.AlmacenMovimientoID > 0);
                    Contexto = resultadoInfo;
                    if (!recibido)
                    {
                        Contexto.Guardar = resultadoInfo.Guardar;
                        //if (ValidaHabilitarCapturaAretes(resultadoInfo.Detalle))
                        //{
                        //    btnCapturarAretes.IsEnabled = true;
                        //}
                    }
                    else
                    {
                        Contexto.Detalle.ForEach(a => a.Editar = false);
                        string mensaje = Properties.Resources.RecibirProductoAlmacen_MsgRecibido;
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                                          MessageBoxButton.OK,
                                          MessageImage.Warning);

                    }
                }
                else
                {
                    gridDatos.ItemsSource = new List<RecibirProductoAlmacenReplica>();
                }
            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.RecibirProductoAlmacen_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.RecibirProductoAlmacen_ErrorBuscar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Método para cancelar
        /// </summary>
        private void Cancelar()
        {
            MessageBoxResult result = SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                        Properties.Resources.
                                                            SolicitudProductosAlmacen_MsgConfirmaCancelar,
                                                        MessageBoxButton.YesNo, MessageImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Limpiar();
            }
        }

        /// <summary>
        /// Limpia todos los controles de la pantalla.
        /// </summary>
        private void Limpiar()
        {
            InicializaContexto();
            btnGuardar.Content = Properties.Resources.btnGuardar;
            skAyudaFolio.IsEnabled = true;
            btnCapturarAretes.IsEnabled = false;
            gridDatos.ItemsSource = Contexto.Detalle;
            skAyudaFolio.AsignarFoco();
        }

        /// <summary>
        /// Método para cancelar
        /// </summary>
        private void Guardar()
        {
            bool guardar = ValidaGuardar();
            if (guardar)
            {
                try
                {
                    SolicitudProductoReplicaInfo solicitudGuardar = Contexto;
                    var organizacion = new OrganizacionPL().ObtenerPorID(solicitudGuardar.AlmacenDestino.Organizacion.OrganizacionID);
                    var tipoOrg = organizacion.TipoOrganizacion.TipoOrganizacionID;
                    
                    solicitudGuardar.UsuarioModificacionID = usuario.UsuarioID;
                    int activos = solicitudGuardar.Detalle.Count(e => e.Activo == true);
                    int recibidos =
                        solicitudGuardar.Detalle.Count(
                            e => e.Activo == true);
                    if (tipoOrg == TipoOrganizacion.Centro.GetHashCode() || tipoOrg == TipoOrganizacion.Cadis.GetHashCode() || tipoOrg == TipoOrganizacion.Descanso.GetHashCode())
                    {
                        var misAretes = ObtenerTotalAretesGrid();
                        var result = new SolicitudProductoBL().GuardarInformacionCentros(Contexto.FolioSolicitud.ToString(), AuxConfiguracion.ObtenerUsuarioLogueado(), solicitudGuardar.AlmacenDestino.Organizacion.OrganizacionID, misAretes);
                        if(result)
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.RecibirProductoAlmacen_GuardadoConExito,
                                              MessageBoxButton.OK,
                                              MessageImage.Correct);
                            Limpiar();
                        }
                        else
                        {
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], Properties.Resources.RecibirProductoAlmacen_ErrorGuardar,
                                              MessageBoxButton.OK,
                                              MessageImage.Error);
                        }

                    }
                    else
                    {
                        MemoryStream pdf = solicitudProductoBL.GuardarMovimientoInventario(solicitudGuardar);
                        if (pdf != null)
                        {
                            var exportarPoliza = new ExportarPoliza();
                            if (solicitudGuardar.AlmacenID.HasValue)
                            {
                                exportarPoliza.ImprimirPoliza(pdf, string.Format("{0} {1}", "Poliza", TipoPoliza.EntradaTraspasoSAP));
                            }
                            else
                            {
                                exportarPoliza.ImprimirPoliza(pdf,
                                                              string.Format("{0} {1}", "Poliza", TipoPoliza.SalidaConsumo));
                            }
                        }

                        string mensaje = Properties.Resources.RecibirProductoAlmacen_GuardadoConExito;
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                                          MessageBoxButton.OK,
                                          MessageImage.Correct);
                        Limpiar();
                    }
                }
                catch (ExcepcionServicio ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.RecibirProductoAlmacen_ErrorGuardar, MessageBoxButton.OK,
                                      MessageImage.Stop);
                }
                catch (ExcepcionGenerica ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.RecibirProductoAlmacen_ErrorGuardar, MessageBoxButton.OK,
                                      MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.RecibirProductoAlmacen_ErrorGuardar, MessageBoxButton.OK,
                                      MessageImage.Error);
                }
            }
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
                bool selecionados = Contexto.Detalle.Any(a => a.Activo);
                if (!selecionados)
                {
                    mensaje = Properties.Resources.RecibirProductoAlmacen_MsgProductosRequeridos;
                }
                else
                {
                    SolicitudProductoReplicaInfo solicitudGuardar = Contexto;
                    var organizacion = new OrganizacionPL().ObtenerPorID(solicitudGuardar.AlmacenDestino.Organizacion.OrganizacionID);
                    var tipoOrg = organizacion.TipoOrganizacion.TipoOrganizacionID;
                    
                    if (tipoOrg == TipoOrganizacion.Centro.GetHashCode() || tipoOrg == TipoOrganizacion.Cadis.GetHashCode() || tipoOrg == TipoOrganizacion.Descanso.GetHashCode())
                    {
                        if (!ValidarAretesCompletos())
                        {
                            mensaje = Properties.Resources.RecibirProductoAlmacen_ValidarAretes;
                        }
                        /*else
                        {
                            var existeArete = ValidarAretesDuplicados();
                            if (existeArete != string.Empty)
                            {
                                if (existeArete.Length > 15)
                                {
                                    mensaje = string.Format(existeArete);
                                }
                                else
                                {
                                    mensaje = string.Format(Properties.Resources.RecibirProductoAlmacen_ValidarAretesAsignado, existeArete);
                                }
                            }
                        }*/
                    }

                }
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            if (!string.IsNullOrWhiteSpace(mensaje))
            {
                resultado = false;
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                                  MessageBoxButton.OK, MessageImage.Warning);
            }
            return resultado;
        }

        private bool ValidaHabilitarCapturaAretes(List<SolicitudProductoReplicaDetalleInfo> list)
        {
            var result = false;
            var parametroGeneralPL = new ParametroGeneralPL();
            var parametroGeneral = parametroGeneralPL.ObtenerPorClaveParametro(ParametrosEnum.RecProdAlmRepSAP.ToString());
            var parametroGeneralNacional = parametroGeneralPL.ObtenerPorClaveParametro(ParametrosEnum.RecProdAlmRepSAPNac.ToString());
            _esSukarne = 0;

            if (parametroGeneral != null)
            {
                var productos = parametroGeneral.Valor.Split('|').ToList();
                var productosNac = parametroGeneralNacional.Valor.Split('|').ToList();
                var productosCapturaAretes = productos.Select(p => Convert.ToInt32(p)).ToList();
                var productosCapturaAretesNacionales = productosNac.Select(p => Convert.ToInt32(p)).ToList();

                if (list.Any(item => productosCapturaAretesNacionales.Contains(Convert.ToInt32(item.ProductoID))))
                {
                    result = true;
                }

                if(list.Any(item => productosCapturaAretes.Contains(Convert.ToInt32(item.ProductoID))))
                {
                    _esSukarne = 1;
                    result = true;
                }
            }
            
            return result;
        }

        private List<AreteInfo> ObtenerTotalAretesGrid()
        {
            var totalAretes = new List<AreteInfo>();

            foreach (var row in gridDatos.Items)
            {
                var aretes = row as SolicitudProductoReplicaDetalleInfo;
                if (aretes != null)
                {
                    if (aretes.ListadoAretes != null)
                    {
                        totalAretes.AddRange(aretes.ListadoAretes);
                    }
                }
            }

            return totalAretes;

        }

        private bool ValidarAretesCompletos()
        {
            var completo = true;

            foreach (var row in gridDatos.Items)
            {
                var aretes = row as SolicitudProductoReplicaDetalleInfo;
                if (aretes != null)
                {
                    if (ValidaHabilitarCapturaAretes(new List<SolicitudProductoReplicaDetalleInfo>() { aretes }))
                    {
                        if (aretes.ListadoAretes != null)
                        {
                            if (aretes.ListadoAretes.Count != aretes.Cantidad)
                            {
                                completo = false;
                            }
                        }
                        else
                        {
                            completo = false;
                        }
                    }
                    if (!aretes.Editar)
                    {
                        completo = false;
                    }
                }
            }

            return completo;

        }

        private string ValidarAretesDuplicados()
        {
            var mensaje = string.Empty;
            try
            {
                SolicitudProductoReplicaInfo solicitudGuardar = Contexto;
                var misAretes = ObtenerTotalAretesGrid();
                mensaje = new SolicitudProductoBL().ValidarAretesDuplicados(solicitudGuardar.AlmacenDestino.Organizacion.OrganizacionID, misAretes);
            }
            catch (Exception)
            {
                mensaje = Properties.Resources.RecibirProductoAlmacen_ErrorValidarAretesAsignado;
            }

            return mensaje;
        }

        #endregion METODOS

        #region EVENTOS

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            Limpiar();
            skAyudaFolio.ObjetoNegocio = new SolicitudProductoReplicaBL();
            Dispatcher.BeginInvoke((Action)(ConsultarProductos), DispatcherPriority.Background);
            skAyudaFolio.AyudaConDatos += (env, o) =>
            {
                Contexto.OrganizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
                Contexto.FolioSolicitud = Contexto.Solicitud.FolioSolicitud;
                ObtenerSolicitudProducto();
            };
            skAyudaFolio.AyudaLimpia += (env, o) =>
            {
                Contexto.FolioSolicitud = 0;
            };
        }

        /// <summary>
        /// Evento para buscar folio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            ObtenerSolicitudProducto();
        }

        /// <summary>
        /// Evento para  cancelar la la solicitd de productos.   
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Cancelar();
        }

        /// <summary>
        /// Evento guardar la solicitud de productos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }
        
        /// <summary>
        /// Evento que se ejecuta cuando el Valor del CheckBox cambia
        /// </summary>
        private void chkConfirma_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                var checkBox = (CheckBox)sender;
                if (!checkBox.IsChecked.HasValue)
                {
                    return;
                }
                var registro = (SolicitudProductoReplicaDetalleInfo)checkBox.CommandParameter;
                if (registro != null)
                {
                    registro.Activo = checkBox.IsChecked.Value;
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message,
                                  MessageBoxButton.OK, MessageImage.Error);
            }
        }

        /// <summary>
        /// Evento para una nueva recepcion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Limpiar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.RecibirProductoAlmacen_ErrorNuevo, MessageBoxButton.OK, MessageImage.Error);
            }

        }

        /// <summary>
        /// Evento para la captura de aretes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCapturarAretes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var pagoEdicion = new RecibirProductoAlmacenReplicaAretes(Contexto.OrganizacionID, productoRow.Cantidad, _esSukarne);
                pagoEdicion.Left = (ActualWidth - pagoEdicion.Width) / 2;
                pagoEdicion.Top = ((ActualHeight - pagoEdicion.Height) / 2);
                pagoEdicion.Owner = Application.Current.Windows[ConstantesVista.WindowPrincipal];
                pagoEdicion.ShowDialog();
                productoRow.ListadoAretes = new List<AreteInfo>();
                if (pagoEdicion.ListAretes != null)
                {
                    productoRow.TotalAretes = pagoEdicion.ListAretes.Count;
                    var aretes = pagoEdicion.ListAretes.Select(item => item.NumeroAreteSukarne).ToList();
                    foreach (var a in aretes)
                    {
                        var miArete = new AreteInfo()
                        {
                            Arete = a,
                            Tipo = _esSukarne == 1 ? TipoAreteEnum.Sukarne.GetHashCode() : TipoAreteEnum.Siniga.GetHashCode()
                        };
                      
                        productoRow.ListadoAretes.Add(miArete);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.RecibirProductoAlmacen_ErrorCapturarAretes, MessageBoxButton.OK, MessageImage.Error);
                productoRow.ListadoAretes = new List<AreteInfo>();
            }

        }

        private void gridDatos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                btnCapturarAretes.IsEnabled = false;
                if (e.AddedItems.Count > 0)
                {
                    productoRow = e.AddedItems[0] as SolicitudProductoReplicaDetalleInfo;
                    btnCapturarAretes.IsEnabled =
                        ValidaHabilitarCapturaAretes(new List<SolicitudProductoReplicaDetalleInfo>() { productoRow });
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        #endregion EVENTOS
        
    }
}