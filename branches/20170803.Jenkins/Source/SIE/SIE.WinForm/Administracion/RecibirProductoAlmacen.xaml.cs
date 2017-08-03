using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using SIE.Services.Info.Modelos;

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para RecibirProductoAlmacen.xaml
    /// </summary>
    public partial class RecibirProductoAlmacen
    {
        #region Propiedades

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private SolicitudProductoInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (SolicitudProductoInfo) DataContext;
            }
            set { DataContext = value; }
        }

        private UsuarioInfo usuario;
        private AlmacenInfo almacenGeneral;
        private int autorizadorId;
        private readonly SolicitudProductoBL solicitudProductoBL;

        #endregion

        #region Constructor

        public RecibirProductoAlmacen()
        {
            solicitudProductoBL = new SolicitudProductoBL();
            InitializeComponent();
            InicializaContexto();
        }

        #endregion

        #region Eventos

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            skAyudaFolio.ObjetoNegocio = solicitudProductoBL;
            skAyudaFolio.AyudaConDatos += (o, args) => AyudaConDatosFolio();
            Limpiar();
            Dispatcher.BeginInvoke(new Action(ValidaTipoAlmacen), DispatcherPriority.ContextIdle, null);
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
        /// Evento para  cancelar la la solicitd de productos.   
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Cancelar();
        }

        /// <summary>
        /// Evento que se ejecuta cuando el Valor del CheckBox cambia
        /// </summary>
        private void chkConfirma_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                var checkBox = (CheckBox) sender;
                if (!checkBox.IsChecked.HasValue)
                {
                    return;
                }
                var registro = (SolicitudProductoDetalleInfo) checkBox.CommandParameter;
                if (registro != null)
                {
                    registro.EstatusID = checkBox.IsChecked.Value
                                             ? Estatus.SolicitudProductoRecibido.GetHashCode()
                                             : Estatus.SolicitudProductoEntregado.GetHashCode();
                    registro.Entregado = checkBox.IsChecked.Value;
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message,
                                  MessageBoxButton.OK, MessageImage.Error);
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            usuario = new UsuarioPL().ObtenerPorID(AuxConfiguracion.ObtenerUsuarioLogueado());
            usuario.OrganizacionID = usuario.Organizacion.OrganizacionID;
            almacenGeneral = ObtenerAlmacenGenerarl(usuario.Organizacion.OrganizacionID);
            autorizadorId = 0;
            Contexto =
                new SolicitudProductoInfo
                    {
                        OrganizacionID = usuario.OrganizacionID,
                        AlmacenID = null,
                        //UsuarioIDSolicita = usuario.UsuarioID,
                        //UsuarioSolicita = usuario,
                        Solicitud = InicializaContextoSolicitud(),
                        Detalle = new List<SolicitudProductoDetalleInfo>(),
                        DetalleGrid = new ObservableCollection<SolicitudProductoDetalleInfo>(),
                        FechaCreacion = DateTime.Today,
                        UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                        EstatusID = Estatus.SolicitudProductoEntregado.GetHashCode(),
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
                                    CentroCosto = new CentroCostoInfo {AutorizadorID = autorizadorId},
                                    ClaseCostoProducto = new ClaseCostoProductoInfo(),
                                    AlmacenID = 0,
                                    EstatusID = Estatus.SolicitudProductoEntregado.GetHashCode(),
                                };
            return solicitud;
        }

        /// <summary>
        /// Asigna el valor al campo folio
        /// </summary>
        private void AyudaConDatosFolio()
        {
            if (Contexto.Solicitud.FolioID > 0)
            {
                Contexto = ObtenerSolicitudPorId(Contexto.Solicitud.FolioID);
                //Contexto.AlmacenID = almacen.AlmacenID;

                int activos = Contexto.Detalle.Count(e => e.Activo == EstatusEnum.Activo);
                int recibidos = Contexto.Detalle.Count(e => e.EstatusID == Estatus.SolicitudProductoRecibido.GetHashCode());

                
                Contexto.Detalle = Contexto.Detalle.Where(e => e.Activo == EstatusEnum.Activo).ToList();
                Contexto.Detalle.ForEach(e =>
                                                {
                                                    e.Recibido = e.EstatusID == Estatus.SolicitudProductoRecibido.GetHashCode();
                                                    e.Editar = e.EstatusID == Estatus.SolicitudProductoEntregado.GetHashCode();
                                                });

                DtpFecha.SelectedDate = Contexto.FechaSolicitud;
                Contexto.DetalleGrid = Contexto.Detalle.ConvertirAObservable();
                Contexto.UsuarioEntrega = usuario;

                    //var habilitarBotonGuardar = Contexto.Detalle.FirstOrDefault(e => e.Editar);
                    var habilitarBotonGuardar = Contexto.Detalle.Count(e => !e.Recibido);
                    //if (habilitarBotonGuardar == null)
                    if (habilitarBotonGuardar == 0)
                    {
                        Contexto.Guardar = false;
                    }
                    else
                    {
                        Contexto.Guardar = true;
                    }

                    gridDatos.ItemsSource = Contexto.Detalle.Where(e => e.Activo == EstatusEnum.Activo);
                    skAyudaFolio.IsEnabled = false;
            }
        }

        /// <summary>
        /// Obtiene una solicitud de productos por su folio de solicitud.
        /// </summary>
        /// <param name="solicitudId"></param>
        /// <returns></returns>
        private SolicitudProductoInfo ObtenerSolicitudPorId(int solicitudId)
        {
            SolicitudProductoInfo solicitud =
                solicitudProductoBL.ObtenerPorID(new SolicitudProductoInfo {SolicitudProductoID = solicitudId});
            solicitud.Solicitud = InicializaContextoSolicitud();
            solicitud.Solicitud.FolioID = solicitud.SolicitudProductoID;
            solicitud.Solicitud.FolioSolicitud = solicitud.FolioSolicitud;
            solicitud.Solicitud.Autoriza = string.Format("{0}", solicitud.FolioSolicitud);
            return solicitud;
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
                int selecionados = Contexto.Detalle.Count(e => e.Recibido && e.Editar);
                if (selecionados == 0)
                {
                    mensaje = Properties.Resources.RecibirProductoAlmacen_MsgSeleccioneUnRegistro;
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
                    SolicitudProductoInfo solicitudGuardar = Contexto;
                    solicitudGuardar.UsuarioIDEntrega = usuario.UsuarioID;
                    solicitudGuardar.FechaEntrega = DateTime.Now;
                    solicitudGuardar.UsuarioModificacionID = usuario.UsuarioID;
                    //solicitudGuardar.AlmacenID = almacen.AlmacenID;
                    solicitudGuardar.TipoMovimientoInventario = TipoMovimiento.EntradaAlmacen;

                    int activos = solicitudGuardar.Detalle.Count(e => e.Activo == EstatusEnum.Activo);
                    int recibidos =
                        solicitudGuardar.Detalle.Count(
                            e => e.EstatusID == Estatus.SolicitudProductoRecibido.GetHashCode());

                    if (activos == recibidos)
                    {
                        solicitudGuardar.EstatusID = Estatus.SolicitudProductoRecibido.GetHashCode();
                    }

                    solicitudGuardar.Detalle = solicitudGuardar.Detalle.Where(e => e.Recibido && e.Editar).ToList();
                    MemoryStream pdf =  solicitudProductoBL.GuardarMovimientoInventario(solicitudGuardar);
                    if (pdf != null)
                    {
                        var exportarPoliza = new ExportarPoliza();
                        if (solicitudGuardar.AlmacenID.HasValue)
                        {
                            exportarPoliza.ImprimirPoliza(pdf, string.Format("{0} {1}", "Poliza", TipoPoliza.SalidaTraspaso));
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
                catch (ExcepcionServicio ex)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      ex.Message, MessageBoxButton.OK,
                                      MessageImage.Stop);
                }
                catch (ExcepcionGenerica)
                {
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
                InicializaContexto();
                Limpiar();
            }
        }

        /// <summary>
        /// Limpia todos los controles de la pantalla.
        /// </summary>
        private void Limpiar()
        {
            InicializaContexto();
            Contexto.Guardar = false;
            btnGuardar.Content = Properties.Resources.btnGuardar;
            skAyudaFolio.IsEnabled = true;
            gridDatos.ItemsSource = Contexto.DetalleGrid;
            skAyudaFolio.AsignarFoco();
            DtpFecha.SelectedDate = null;
        }

        /// <summary>
        /// Obtiene el almacén general que tenga configurado
        /// el usuario.
        /// </summary>
        private AlmacenInfo ObtenerAlmacenGenerarl(int almacenId)
        {
            var almacenDaL = new AlmacenPL();
            AlmacenInfo almacenInfo = null;
            IList<AlmacenInfo> almacenes = almacenDaL.ObtenerAlmacenPorOrganizacion(almacenId);
            if (almacenes != null && almacenes.Count > 0)
            {
                almacenInfo = almacenes.FirstOrDefault(a => a.TipoAlmacenID == (int) TipoAlmacenEnum.GeneralGanadera
                                                            && a.Activo == EstatusEnum.Activo);
            }
            return almacenInfo;
        }

        /// <summary>
        /// Valida que el almacén sea de tipo Almacén General
        /// </summary>
        private void ValidaTipoAlmacen()
        {
            if (almacenGeneral == null)
            {
                skAyudaFolio.IsEnabled = false;
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.
                                      SolicitudProductosAlmacen_MsgNoExisteAlmacen,
                                  MessageBoxButton.OK, MessageImage.Warning);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="solicitud"></param>
        /// <returns></returns>
        private IEnumerable<SolicitudProductoDetalleModel> ValidaDisponibilidad(SolicitudProductoInfo solicitud)
        {
            IEnumerable<int> idsProductos = solicitud.Detalle.Select(d => d.ProductoID).Distinct().ToList();
            var filtro = new FolioSolicitudInfo
                             {
                                 OrganizacionID = solicitud.OrganizacionID,
                                 IdsProductos = idsProductos.ToList(),
                                 EstatusID = Estatus.SolicitudProductoAutorizado.GetHashCode(),
                                 Activo = EstatusEnum.Activo
                             };

            IEnumerable<AlmacenInventarioInfo> existencia = ObtenerExistencia(idsProductos);
            IList<SolicitudProductoInfo> solicitudesAutorizadas =
                solicitudProductoBL.ObtenerSolicitudesAutorizadas(filtro);

            var productosValidar = solicitud.Detalle.Select(d => new
                                                                     {
                                                                         d.ProductoID,
                                                                         d.Cantidad
                                                                     }).ToList();

            var autorizadas = (from p in solicitudesAutorizadas.SelectMany(sd => sd.Detalle)
                               where p.EstatusID == Estatus.SolicitudProductoAutorizado.GetHashCode()
                               group p by p.ProductoID
                               into pg
                               select new
                                          {
                                              ProductoID = pg.Key,
                                              Autorizada = pg.Sum(c => c.Cantidad)
                                          }).ToList();

            var query = (from p in productosValidar
                         join e in existencia on p.ProductoID equals e.ProductoID
                         join a in autorizadas on p.ProductoID equals a.ProductoID into gj
                         from pa in gj.DefaultIfEmpty()
                         select new SolicitudProductoDetalleModel
                                    {
                                        ProductoID = p.ProductoID,
                                        Cantidad = p.Cantidad,
                                        Existencia = e.Cantidad,
                                        Autorizada = (pa == null ? 0 : pa.Autorizada),
                                        IsDisponible =
                                            (e.Cantidad - (pa == null ? 0 : pa.Autorizada - p.Cantidad)) >= p.Cantidad
                                    }).ToList();

            return query;
        }

        /// <summary>
        /// Obtiene la existencia del almacen
        /// </summary>
        /// <param name="idProductos"></param>
        /// <returns></returns>
        private IEnumerable<AlmacenInventarioInfo> ObtenerExistencia(IEnumerable<int> idProductos)
        {
            IList<ProductoInfo> producto = idProductos.Select(id => new ProductoInfo {ProductoId = id}).ToList();
            //int organizacionId = usuario.OrganizacionID;
            //AlmacenInfo almacen = ObtenerAlmacenGenerarl(organizacionId);
            int almacenId = almacenGeneral.AlmacenID;

            var almacenInventarioBL = new AlmacenInventarioPL();
            IList<AlmacenInventarioInfo> result = almacenInventarioBL.ExistenciaPorProductos(almacenId, producto);
            return result;
        }


        #endregion
    }
}
