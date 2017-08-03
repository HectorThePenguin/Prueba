using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using SIE.Services.Info.Modelos;
namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para EntregarProductosAlmacen.xaml
    /// </summary>
    public partial class EntregarProductoAlmacen
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
                    //InicializaContexto();
                }
                return (SolicitudProductoInfo)DataContext;
            }
            set { DataContext = value; }
        }
        private UsuarioInfo usuario;
        //private AlmacenInfo almacen;
        private AlmacenInfo almacenGeneral;
        private int autorizadorId;
        private readonly  SolicitudProductoBL solicitudProductoBL;
        #endregion

        #region Constructor
        public EntregarProductoAlmacen()
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
                var checkBox = (CheckBox)sender;
                if (!checkBox.IsChecked.HasValue)
                {
                    return;
                }
                var registro = (SolicitudProductoDetalleInfo)checkBox.CommandParameter;
                if (registro != null)
                {
                    registro.EstatusID = checkBox.IsChecked.Value
                        ? Estatus.SolicitudProductoEntregado.GetHashCode()
                        : Estatus.SolicitudProductoAutorizado.GetHashCode();
                    registro.Entregado = checkBox.IsChecked.Value;
                }
                btnGuardar.IsEnabled = Contexto.Detalle.Count(ent => ent.Entregado) > 0;
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
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
                        //UsuarioIDSolicita = 0,
                        //UsuarioSolicita = new UsuarioInfo(),
                        UsuarioEntrega = new UsuarioInfo(),
                        UsuarioIDEntrega = 0,
                        Solicitud = InicializaContextoSolicitud(),
                        Detalle = new List<SolicitudProductoDetalleInfo>(),
                        DetalleGrid = new ObservableCollection<SolicitudProductoDetalleInfo>(),
                        FechaCreacion = DateTime.Today,
                        UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                        EstatusID = Estatus.SolicitudProductoAutorizado.GetHashCode(),
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
                                    OrganizacionID = usuario.OrganizacionID,
                                    Producto = new ProductoInfo(),
                                    Cantidad = decimal.Zero,
                                    CentroCosto = new CentroCostoInfo { AutorizadorID = autorizadorId },
                                    ClaseCostoProducto = new ClaseCostoProductoInfo(),
                                    AlmacenID = 0,
                                    //AlmacenID = almacen != null
                                    //                ? almacen.AlmacenID
                                    //                : 0,
                                    EstatusID = Estatus.SolicitudProductoAutorizado.GetHashCode(),
                                };
            return solicitud;
        }

        /// <summary>
        /// Asigna el valor al campo folio
        /// </summary>
        private void AyudaConDatosFolio()
        {
            try
            {
                if (Contexto.Solicitud.FolioID > 0)
                {
                    if (Contexto.UsuarioIDEntrega == 0)
                    {
                        Contexto.UsuarioIDEntrega = usuario.UsuarioID;
                        Contexto.UsuarioEntrega = usuario;
                    }
                    Contexto = ObtenerSolicitudPorId(Contexto.Solicitud.FolioID);

                    //Obtener los productos ligados a familia de HerramientaYEquipo y valdiar si ya se entrego en INFOR
                    IList<SolicitudProductoDetalleInfo> idsProductosHerramienta = Contexto.Detalle
                        .Where(d => d.Producto.FamiliaId == (int)FamiliasEnum.HerramientaYEquipo).ToList();
                    if (idsProductosHerramienta.Any())
                    {
                        //Validar si el folio ya se encuentra en INFOR Entregado
                        if (!solicitudProductoBL.ExisteFolioEnINFOR(Contexto.Solicitud.FolioSolicitud, Contexto.Solicitud.OrganizacionID))
                        {
                            Limpiar();
                            SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                              Properties.Resources.EntregarProductoAlmacen_MsgNoExisteReferenciaInfor,
                                              MessageBoxButton.OK,
                                              MessageImage.Warning);
                            return;
                        }

                    }

                    int porEntregar = Contexto.Detalle.Count(e => e.EstatusID == Estatus.SolicitudProductoAutorizado.GetHashCode());

                    if (porEntregar > 0)
                    {
                        Contexto.Detalle = Contexto.Detalle.Where(e => e.Activo == EstatusEnum.Activo).ToList();
                        var disponibilidad = ValidaDisponibilidad(Contexto);

                        Contexto.Detalle.ForEach(e =>
                        {
                            e.Entregado = e.EstatusID == Estatus.SolicitudProductoEntregado.GetHashCode()
                                   || e.EstatusID == Estatus.SolicitudProductoRecibido.GetHashCode();
                            var registro =
                                disponibilidad.FirstOrDefault(d => d.ProductoID == e.ProductoID);
                            if (registro != null && !registro.IsDisponible)
                            {
                                e.Editar = registro.IsDisponible;
                            }
                            else
                            {
                                e.Editar = e.EstatusID == Estatus.SolicitudProductoPendiente.GetHashCode() ||
                                           e.EstatusID == Estatus.SolicitudProductoAutorizado.GetHashCode();
                            }
                        });

                        DtpFecha.SelectedDate = Contexto.FechaSolicitud;
                        Contexto.DetalleGrid = Contexto.Detalle.ConvertirAObservable();
                        Contexto.UsuarioEntrega = usuario;

                        var habilitarBotonGuardar = Contexto.Detalle.FirstOrDefault(e => e.Editar);
                        if (habilitarBotonGuardar == null)
                        {
                            Contexto.Guardar = false;
                        }

                        gridDatos.ItemsSource = Contexto.Detalle.Where(e => e.Activo == EstatusEnum.Activo);
                        skAyudaFolio.IsEnabled = false;
                    }
                    else
                    {
                        Limpiar();
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                          Properties.Resources.EntregarProductoAlmacen_MsgNoTieneEstatusAutorizada,
                                          MessageBoxButton.OK, MessageImage.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.EntregarProductoAlmacen_MsgErrorConsultarFolio,
                                  MessageBoxButton.OK, MessageImage.Error);
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
                solicitudProductoBL.ObtenerPorID(new SolicitudProductoInfo { SolicitudProductoID = solicitudId });
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
                SolicitudProductoInfo solicitud = Contexto;
                int selecionados = Contexto.Detalle.Count(e => e.Entregado && e.Editar);

                if (string.IsNullOrWhiteSpace(Contexto.ObservacionUsuarioEntrega))
                {
                    mensaje = Properties.Resources.EntregarProductoAlmacen_MsgObservacionesRequerida;
                }
                else if (selecionados > 0)
                {
                    var disponibilidad = ValidaDisponibilidad(solicitud);
                    solicitud.Detalle.ForEach(se =>
                                                  {
                                                      bool entregado = (se.Entregado);
                                                      if ((se.Activo == EstatusEnum.Activo && entregado) && se.Editar)
                                                      {
                                                          var registro =
                                                              disponibilidad.FirstOrDefault(
                                                                  d => d.ProductoID == se.ProductoID);
                                                          if (registro != null && !registro.IsDisponible)
                                                          {
                                                              mensaje +=
                                                                  string.Format(
                                                                      Properties.Resources.
                                                                          EntregarProductoAlmacen_MsgSinDisponibilidad,
                                                                      se.Producto.Descripcion);
                                                          }
                                                      }
                                                  });
                }
                else
                {
                    mensaje = Properties.Resources.EntregarProductoAlmacen_MsgSeleccioneUnRegistro;
                }
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            if (!string.IsNullOrWhiteSpace(mensaje))
            {
                resultado = false;
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje, MessageBoxButton.OK, MessageImage.Warning);
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
                    solicitudGuardar.TipoMovimientoInventario = TipoMovimiento.SalidaPorTraspaso;

                    int activos = solicitudGuardar.DetalleGrid.Count(e => e.Activo == EstatusEnum.Activo);
                    int entregados = solicitudGuardar.DetalleGrid.Count(e => e.EstatusID == Estatus.SolicitudProductoEntregado.GetHashCode());

                    if (activos == entregados)
                    {
                        solicitudGuardar.EstatusID = Estatus.SolicitudProductoEntregado.GetHashCode();
                    }

                    solicitudGuardar.Detalle = solicitudGuardar.Detalle.Where(e => e.Entregado && e.Editar).ToList();
                    
                    solicitudProductoBL.Guardar(solicitudGuardar);

                    string mensaje = Properties.Resources.EntregarProductoAlmacen_GuardadoConExito;

                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], mensaje,
                                      MessageBoxButton.OK,
                                      MessageImage.Correct);
                    Limpiar();
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.EntregarProductoAlmacen_ErrorGuardar, MessageBoxButton.OK,
                                      MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.EntregarProductoAlmacen_ErrorGuardar, MessageBoxButton.OK,
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
            txtObservaciones.Text = string.Empty;
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
                almacenInfo = almacenes.FirstOrDefault(a => a.TipoAlmacenID == (int)TipoAlmacenEnum.GeneralGanadera
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
        private  IEnumerable<SolicitudProductoDetalleModel> ValidaDisponibilidad(SolicitudProductoInfo solicitud)
        {
            //IEnumerable<int> idsProductos = solicitud.Detalle.Select(d => d.ProductoID).Distinct().ToList();
            IEnumerable<int> idsProductos = solicitud.Detalle
                .Where(d => d.Producto.FamiliaId != (int)FamiliasEnum.HerramientaYEquipo)
                .Select(d => d.ProductoID).Distinct().ToList();

            var filtro = new FolioSolicitudInfo
            {
                OrganizacionID = solicitud.OrganizacionID,
                IdsProductos = idsProductos.ToList(),
                EstatusID = Estatus.SolicitudProductoAutorizado.GetHashCode(),
                Activo = EstatusEnum.Activo
            };

            IEnumerable<AlmacenInventarioInfo> existencia = ObtenerExistencia(idsProductos);
            IList<SolicitudProductoInfo> solicitudesAutorizadas = solicitudProductoBL.ObtenerSolicitudesAutorizadas(filtro);

            var productosValidar = solicitud.Detalle.Select(d => new
            {
                d.ProductoID,
                d.Cantidad,
                d.Producto.FamiliaId
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

            List<SolicitudProductoDetalleModel> query = (from p in productosValidar
                        join a in autorizadas on p.ProductoID equals a.ProductoID into gj
                        from pa in gj.DefaultIfEmpty()
                        select new SolicitudProductoDetalleModel
                        {
                            ProductoID = p.ProductoID,
                            Cantidad = p.Cantidad,
                            Existencia = ObtenerCantidadExistencia(p.ProductoID, p.FamiliaId, p.Cantidad, existencia, (pa == null ? 0 : pa.Autorizada)),/* Para los  */
                            Autorizada = (pa == null ? 0 : pa.Autorizada),
                            IsDisponible = (ObtenerCantidadExistencia(p.ProductoID, p.FamiliaId, p.Cantidad, existencia, (pa == null ? 0 : pa.Autorizada)) - (pa == null ? 0 : pa.Autorizada)) >= 0
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
            IList<ProductoInfo> producto = idProductos.Select(id => new ProductoInfo { ProductoId = id }).ToList();
            //int organizacionId = usuario.OrganizacionID;
            //AlmacenInfo almacen = ObtenerAlmacenGenerarl(organizacionId);
            int almacenId = almacenGeneral.AlmacenID;

            var almacenInventarioBL = new AlmacenInventarioPL();
            IList<AlmacenInventarioInfo> result = almacenInventarioBL.ExistenciaPorProductos(almacenId, producto);
            return result;
        }

        /// <summary>
        /// Validar la existencia del producto
        /// </summary>
        /// <param name="productoID"></param>
        /// <param name="familiaId"></param>
        /// <param name="cantidad"></param>
        /// <param name="existencia"></param>
        /// <param name="paAutorizada"></param>
        /// <returns></returns>
        private static decimal ObtenerCantidadExistencia(int productoID, int familiaId, decimal cantidad, 
            IEnumerable<AlmacenInventarioInfo> existencia, decimal paAutorizada)
        {
            decimal cant = 0;
            if (familiaId == (int)FamiliasEnum.HerramientaYEquipo || familiaId == (int)FamiliasEnum.Combustibles)
            {
                return (paAutorizada == 0 ? cantidad : paAutorizada + cantidad);
            }

            var listaExistencias = (from p in existencia
                                    where p.ProductoID == productoID
                                    select new { p.Cantidad }
                ).ToList();
            var firstOrDefault = listaExistencias.FirstOrDefault();
            if (firstOrDefault != null) cant = firstOrDefault.Cantidad;

            return cant;
        }

        #endregion
    }
}
