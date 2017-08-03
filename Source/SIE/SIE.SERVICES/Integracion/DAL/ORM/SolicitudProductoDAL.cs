using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using System.Transactions;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Polizas;
using SIE.Services.Polizas.Fabrica;

namespace SIE.Services.Integracion.DAL.ORM
{
    internal class SolicitudProductoDAL : BaseDAL
    {
        SolicitudProductoAccessor solicitudProductoAccessor;

        protected override void inicializar()
        {
            solicitudProductoAccessor = da.inicializarAccessor<SolicitudProductoAccessor>();
        }

        protected override void destruir()
        {
            solicitudProductoAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de SolicitudProducto
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<FolioSolicitudInfo> ObtenerPorPagina(PaginacionInfo pagina, FolioSolicitudInfo filtro)
        {
            try
            {
                Logger.Info();
                var result = new ResultadoInfo<FolioSolicitudInfo>();
                var condicion = solicitudProductoAccessor.ObtenerPorPagina(filtro.OrganizacionID,
                                                                           filtro.UsuarioIDSolicita,
                                                                           filtro.UsuarioIDAutoriza,
                                                                           filtro.Solicita ?? string.Empty,
                                                                           filtro.Autoriza ?? string.Empty,
                                                                           filtro.EstatusID, filtro.Activo,
                                                                           pagina.Inicio, pagina.Limite);
                int inicio = pagina.Inicio;
                int limite = pagina.Limite;
                if (inicio > 1)
                {
                    int limiteReal = (limite - inicio) + 1;
                    inicio = (limite / limiteReal);
                    limite = limiteReal;
                }
                var paginado = condicion
                    .OrderBy(e => e.FolioID)
                    .Skip((inicio - 1) * limite)
                    .Take(limite).ToList();

                if (paginado.Any())
                {
                    result = new ResultadoInfo<FolioSolicitudInfo>
                                 {
                                     Lista = paginado,
                                     TotalRegistros = condicion.Count()
                                 };
                }

                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de SolicitudProducto
        /// </summary>
        /// <returns></returns>
        public IQueryable<SolicitudProductoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<SolicitudProductoInfo>();
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de SolicitudProducto filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<SolicitudProductoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return ObtenerTodos().Where(e => e.Activo == estatus);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad de SolicitudProducto por su Id
        /// </summary>
        /// <param name="solicitudProductoId">Obtiene una entidad SolicitudProducto por su Id</param>
        /// <returns></returns>
        public SolicitudProductoInfo ObtenerPorID(int solicitudProductoId)
        {
            try
            {
                Logger.Info();
                //SolicitudProductoInfo resultado = null;
                SolicitudProductoInfo solicitud = (from s in da.Tabla<SolicitudProductoInfo>()
                                                   where s.SolicitudProductoID == solicitudProductoId
                                                   select s).FirstOrDefault();

                if (solicitud != null)
                {
                    solicitud.Detalle = da.Tabla<SolicitudProductoDetalleInfo>()
                        .Where(d => d.SolicitudProductoID == solicitud.SolicitudProductoID && d.Activo == EstatusEnum.Activo).
                        ToList();

                    var idsProductos = solicitud.Detalle.Select(e => e.ProductoID).Distinct();
                    var idsCamionReparto = solicitud.Detalle.Select(e => e.CamionRepartoID).Distinct();

                    var idUsuarios = new List<int?>();
                    if (solicitud.UsuarioIDSolicita != null)
                    {
                        idUsuarios.Add(solicitud.UsuarioIDSolicita);
                    }
                    if (solicitud.UsuarioIDAutoriza != null)
                    {
                        idUsuarios.Add(solicitud.UsuarioIDAutoriza);
                    }
                    if (solicitud.UsuarioIDEntrega != null)
                    {
                        idUsuarios.Add(solicitud.UsuarioIDEntrega);
                    }
                    var claseCostoProductoDAL = da.Tabla<ClaseCostoProductoInfo>();
                    var cuentaSAPDAL = da.Tabla<CuentaSAPInfo>();
                    var unidadMedicionDAL = da.Tabla<UnidadMedicionInfo>();

                    AlmacenInfo almacen = null;
                    AlmacenInfo almacenGeneral = null;

                    almacenGeneral = da.Tabla<AlmacenInfo>().FirstOrDefault(a => a.OrganizacionID == solicitud.OrganizacionID && a.TipoAlmacenID == (int)TipoAlmacenEnum.GeneralGanadera);


                    if (solicitud.AlmacenID > 0)
                    {
                        almacen = da.Tabla<AlmacenInfo>().FirstOrDefault(a => a.AlmacenID == solicitud.AlmacenID);
                    }

                    Dictionary<int, UsuarioInfo> usuarios =
                        da.Tabla<UsuarioInfo>().Where(u => idUsuarios.Contains(u.UsuarioID)).ToDictionary(
                            u => u.UsuarioID, f => f);

                    var queryProd = (from p in da.Tabla<ProductoInfo>()
                                     join u in unidadMedicionDAL on p.UnidadId equals u.UnidadID
                                     where idsProductos.Contains(p.ProductoId)
                                     select new ProductoInfo(p, u)).ToDictionary(p => p.ProductoId, f => f);

                    var idsSubFamilia = (from p in queryProd
                                         select p.Value.SubfamiliaId).Distinct();

                    var qyerySubFamilia = (from sf in da.Tabla<SubFamiliaInfo>()
                                           where idsSubFamilia.Contains(sf.SubFamiliaID)
                                           select sf
                                          ).ToDictionary(sf => sf.SubFamiliaID, sf => sf);

                    Dictionary<int, CamionRepartoInfo> queryCamionReparto =
                        da.Tabla<CamionRepartoInfo>().Where(u => idsCamionReparto.Contains(u.CamionRepartoID)).
                            ToDictionary(u => u.CamionRepartoID, f => f);

                    var queryClaseCosto = (from cp in claseCostoProductoDAL
                                           from cs in cuentaSAPDAL
                                           where cp.CuentaSAPID == cs.CuentaSAPID
                                           && cp.AlmacenID == almacenGeneral.AlmacenID

                                           select new ClaseCostoProductoInfo
                                                      {
                                                          ClaseCostoProductoID = cp.ClaseCostoProductoID,
                                                          AlmacenID = cp.AlmacenID,
                                                          ProductoID = cp.ProductoID,
                                                          CuentaSAPID = cp.CuentaSAPID,
                                                          CuentaSAP = new CuentaSAPInfo
                                                                          {
                                                                              CuentaSAPID = cs.CuentaSAPID,
                                                                              CuentaSAP = cs.CuentaSAP
                                                                          }
                                                      }
                                          ).ToList();

                    foreach (var d in solicitud.Detalle)
                    {
                        d.Producto = queryProd.ContainsKey(d.ProductoID)
                                         ? queryProd[d.ProductoID]
                                         : d.Producto;

                        if (d.Producto != null)
                        {
                            if (qyerySubFamilia.ContainsKey(d.Producto.SubfamiliaId))
                            {
                                d.Producto.SubFamilia = qyerySubFamilia[d.Producto.SubfamiliaId];
                                d.Producto.FamiliaId = d.Producto.SubFamilia.FamiliaID;
                            }
                        }
                        d.ClaseCostoProducto =
                            queryClaseCosto.Where(prod => prod.ProductoID == d.ProductoID).Select(
                                cp => new ClaseCostoProductoInfo
                                          {
                                              ClaseCostoProductoID = cp.ClaseCostoProductoID,
                                              AlmacenID = cp.AlmacenID,
                                              ProductoID = cp.ProductoID,
                                              CuentaSAPID = cp.CuentaSAPID,
                                              CuentaSAP = new CuentaSAPInfo
                                                              {
                                                                  CuentaSAPID = cp.CuentaSAP.CuentaSAPID,
                                                                  CuentaSAP = cp.CuentaSAP.CuentaSAP
                                                              }
                                          }).FirstOrDefault();
                        if (!solicitud.AlmacenID.HasValue)
                        {

                            if (d.CamionRepartoID.HasValue)
                            {
                                d.Concepto = queryCamionReparto.ContainsKey(d.CamionRepartoID.Value)
                                                 ? queryCamionReparto[d.CamionRepartoID.Value].NumeroEconomico
                                                 : string.Empty;
                            }
                        }
                        else
                        {
                            if (almacen != null)
                            {
                                d.Concepto = almacen.Descripcion.Trim();
                            }
                        }
                    }
                    if (almacen != null)
                    {
                        solicitud.Almacen = almacen;
                    }
                    solicitud.Solicitud = new FolioSolicitudInfo
                                              {
                                                  FolioID = solicitud.SolicitudProductoID,
                                                  FolioSolicitud = solicitud.FolioSolicitud,
                                                  Descripcion = string.Format("{0}", solicitud.SolicitudProductoID),
                                                  Usuario = new UsuarioInfo()
                                              };
                    solicitud.Estatus = da.Tabla<EstatusInfo>().FirstOrDefault(e => e.EstatusId == solicitud.EstatusID);

                    if (solicitud.UsuarioIDSolicita != null && usuarios.ContainsKey(solicitud.UsuarioIDSolicita.Value))
                    {
                        solicitud.UsuarioSolicita = usuarios.ContainsKey(solicitud.UsuarioIDSolicita.Value)
                                                        ? usuarios[solicitud.UsuarioIDSolicita.Value]
                                                        : null;
                    }
                    if (solicitud.UsuarioIDAutoriza != null && usuarios.ContainsKey(solicitud.UsuarioIDAutoriza.Value))
                    {
                        solicitud.UsuarioAutoriza = usuarios.ContainsKey(solicitud.UsuarioIDAutoriza.Value)
                                                        ? usuarios[solicitud.UsuarioIDAutoriza.Value]
                                                        : null;
                    }
                    if (solicitud.UsuarioIDEntrega != null && usuarios.ContainsKey(solicitud.UsuarioIDEntrega.Value))
                    {
                        solicitud.UsuarioEntrega = usuarios.ContainsKey(solicitud.UsuarioIDEntrega.Value)
                                                       ? usuarios[solicitud.UsuarioIDEntrega.Value]
                                                       : null;
                    }
                }

                return solicitud;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad de SolicitudProducto por su Id
        /// </summary>
        /// <param name="filtro">Obtiene una entidad SolicitudProducto por su Id</param>
        /// <returns></returns>
        public FolioSolicitudInfo ObtenerPorFolioSolicitud(FolioSolicitudInfo filtro)
        {
            try
            {
                Logger.Info();
                FolioSolicitudInfo resultado = null;

                var solicitud = (from s in da.Tabla<SolicitudProductoInfo>()
                                 where s.OrganizacionID == filtro.OrganizacionID
                                       && s.FolioSolicitud == filtro.FolioSolicitud
                                       && s.Activo == filtro.Activo
                                 select s).ToList().AsQueryable();

                if (filtro.UsuarioIDSolicita > 0)
                {
                    solicitud = solicitud.Where(s => s.UsuarioIDSolicita == filtro.UsuarioIDSolicita);
                }
                if (filtro.UsuarioIDEntrega > 0)
                {
                    solicitud = solicitud.Where(s => s.UsuarioIDEntrega == filtro.UsuarioIDEntrega);
                }


                if (solicitud.Any())
                {
                    var registro = solicitud.FirstOrDefault();
                    if (registro != null)
                    {
                        resultado = new FolioSolicitudInfo
                                        {
                                            FolioID = registro.SolicitudProductoID,
                                            FolioSolicitud = registro.FolioSolicitud,
                                            EstatusID = registro.EstatusID,
                                            FechaEntrega = registro.FechaEntrega,
                                            OrganizacionID = registro.OrganizacionID,
                                            Usuario = filtro.Usuario,
                                        };
                    }
                }
                return resultado;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de SolicitudProducto por 
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public IList<SolicitudProductoInfo> ObtenerSolicitudesAutorizadas(FolioSolicitudInfo filtro)
        {
            try
            {
                Logger.Info();
                var solicitud = (from s in da.Tabla<SolicitudProductoInfo>()
                                 where s.OrganizacionID == filtro.OrganizacionID
                                       && s.EstatusID == filtro.EstatusID
                                       && s.Activo == filtro.Activo
                                 select s).ToList();

                solicitud.ForEach(s =>
                                           s.Detalle = da.Tabla<SolicitudProductoDetalleInfo>()
                                                           .Where(d => d.SolicitudProductoID == s.SolicitudProductoID).
                                                           ToList()
                    );


                var results = solicitud;
                return results;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo para Guardar/Modificar una entidad SolicitudProducto
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(SolicitudProductoInfo info)
        {
            try
            {
                Logger.Info();
                int id;
                if (info.SolicitudProductoID > 0)
                {
                    using (var transaction = new TransactionScope())
                    {
                        info.FechaModificacion = da.FechaServidor();
                        solicitudProductoAccessor.BeginTransaction();
                        id = da.Actualizar<SolicitudProductoInfo>(info);
                        info.Detalle.ForEach(
                                d =>
                                {
                                    if (d.SolicitudProductoDetalleID > 0)
                                    {
                                        d.FechaModificacion = info.FechaModificacion;
                                        d.UsuarioModificacionID = info.UsuarioIDAutoriza;
                                        da.Actualizar<SolicitudProductoDetalleInfo>(d);
                                    }
                                    else
                                    {
                                        d.SolicitudProductoID = info.SolicitudProductoID;
                                        d.UsuarioCreacionID = info.UsuarioCreacionID;
                                        da.Insertar<SolicitudProductoDetalleInfo>(d);
                                    }
                                }
                            );
                        solicitudProductoAccessor.CommitTransaction();
                        transaction.Complete();
                    }
                }
                else
                {
                    using (var transaction = new TransactionScope())
                    {
                        var folio = new FolioInfo
                        {
                            OrganizacionID = info.UsuarioSolicita.Organizacion.OrganizacionID,
                            TipoFolioID = (byte)TipoFolio.SolicitudAlmacen,
                            Folio = 0,
                            Valor = 0
                        };
                        solicitudProductoAccessor.BeginTransaction();

                        solicitudProductoAccessor.ObtenerFolioSolicitud(folio.OrganizacionID, folio.TipoFolioID, folio.Folio);

                        var folios = da.Tabla<FolioInfo>().Where(f => f.OrganizacionID == folio.OrganizacionID && f.TipoFolioID == folio.TipoFolioID);
                        info.FolioSolicitud = folios.FirstOrDefault().Valor;

                        info.FechaSolicitud = da.FechaServidor();

                        id = da.Insertar<SolicitudProductoInfo>(info);
                        info.Detalle.ForEach(
                                d =>
                                {
                                    d.SolicitudProductoID = info.SolicitudProductoID;
                                    d.UsuarioCreacionID = info.UsuarioCreacionID;
                                    da.Insertar<SolicitudProductoDetalleInfo>(d);
                                }
                            );
                        solicitudProductoAccessor.CommitTransaction();
                        transaction.Complete();
                    }
                }
                return id;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                solicitudProductoAccessor.RollbackTransaction();
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo para Guardar/Modificar una entidad SolicitudProducto
        /// además de generar movimiento de inventario
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public MemoryStream GuardarMovimientoInventario(SolicitudProductoInfo info)
        {
            MemoryStream pdfPoliza = null;
            try
            {
                Logger.Info();
                if (info.SolicitudProductoID > 0)
                {
                    info.FechaModificacion = da.FechaServidor();
                    AlmacenInfo almacenGeneral = da.Tabla<AlmacenInfo>()
                        .FirstOrDefault(a => a.OrganizacionID == info.OrganizacionID &&
                                             a.TipoAlmacenID == (int)TipoAlmacenEnum.GeneralGanadera);

                    //Actualizar los movimientos del almacén General.
                    var almacenInventario = ObtenerAlmacenInventario(info.Detalle, almacenGeneral.AlmacenID, TipoMovimiento.SalidaPorTraspaso);
                    var almacenInventarioInfos = almacenInventario as List<AlmacenInventarioInfo> ?? almacenInventario.ToList();
                    var almacenMovimientoDetalle = ObtenerAlmacenMovimientoDetalle(info, almacenInventarioInfos);

                    //Obtener PrecioPromedio de los Productos de la Familia HerramientaYEquipo y Combustibles
                    IList<SolicitudProductoDetalleInfo> idsProductosHerramienta = info.Detalle
                        .Where(d => d.Producto.FamiliaId == (int)FamiliasEnum.HerramientaYEquipo || d.Producto.FamiliaId == (int)FamiliasEnum.Combustibles).ToList();
                    if (idsProductosHerramienta.Any())
                    {
                        ObtenerPrecioPromedioINFOR(info, idsProductosHerramienta);
                    }

                    PolizaAbstract poliza = null;
                    IList<PolizaInfo> listaPolizas = null;
                    using (var transaction = new TransactionScope())
                    {
                        var almacenMovimientoDAL = new AlmacenMovimientoDAL();
                        var almacenMovimientoDetalleDAL = new AlmacenMovimientoDetalleDAL();
                        var solicitudProductoDetalleDAL = new Implementacion.SolicitudProductoDetalleDAL();
                        var almacenInventarioDAL = new AlmacenInventarioDAL();
                        var solicitudProductoDAL = new Implementacion.SolicitudProductoDAL();

                        if (almacenInventarioInfos.Count() > 0)
                        {
                            almacenInventarioDAL.Actualizar(almacenInventarioInfos);
                        }
                        long almacenMovimientoId = 0;
                        if (almacenMovimientoDetalle.Count() > 0)
                        {
                            var almacenMovimientoSalidaNuevo = new AlmacenMovimientoInfo
                                                                   {
                                                                       AlmacenID = almacenGeneral.AlmacenID,
                                                                       TipoMovimientoID =
                                                                           TipoMovimiento.SalidaPorTraspaso.GetHashCode(),
                                                                       Status = Estatus.AplicadoInv.GetHashCode(),
                                                                       UsuarioCreacionID =
                                                                           info.UsuarioModificacionID.HasValue
                                                                               ? info.UsuarioModificacionID.Value
                                                                               : 0
                                                                   };
                            almacenMovimientoId = almacenMovimientoDAL.Crear(almacenMovimientoSalidaNuevo);

                            almacenMovimientoDetalle.ToList().ForEach(
                                det => det.AlmacenMovimientoID = almacenMovimientoId);

                            almacenMovimientoDetalleDAL.GuardarDetalleCierreDiaInventarioPA(
                                almacenMovimientoDetalle.ToList());
                        }


                        info.AlmacenMovimientoID = almacenMovimientoId;
                        solicitudProductoDAL.Actualizar(info);

                        info.AlmacenGeneralID = almacenGeneral.AlmacenID;

                        var almacenDAL = new Implementacion.AlmacenDAL();
                        if (info.AlmacenID.HasValue && info.AlmacenID.Value > 0)
                        {
                            AlmacenInfo almacenDestino = almacenDAL.ObtenerPorID(info.AlmacenID.Value);
                            if (almacenDestino != null)
                            {
                                info.Almacen = almacenDestino;
                            }
                        }

                        info.Detalle.ForEach(
                            d =>
                            {
                                AlmacenInventarioInfo almacenInventarioProducto =
                                    almacenInventarioInfos.FirstOrDefault(
                                        ai => ai.Producto.ProductoId == d.ProductoID);
                                if (almacenInventarioProducto != null)
                                {
                                    d.Producto = almacenInventarioProducto.Producto;
                                    d.PrecioPromedio = almacenInventarioProducto.PrecioPromedio;
                                }

                                if (d.SolicitudProductoDetalleID > 0)
                                {
                                    if (info.UsuarioIDSolicita != null)
                                    {
                                        d.UsuarioModificacionID = info.UsuarioIDSolicita.Value;
                                    }
                                }
                                else
                                {
                                    if (info.UsuarioIDSolicita != null)
                                    {
                                        d.UsuarioCreacionID = info.UsuarioIDSolicita.Value;
                                    }
                                    d.SolicitudProductoID = info.SolicitudProductoID;
                                }
                            });
                        solicitudProductoDetalleDAL.GuardarSolicitudDetalle(info.Detalle, info.SolicitudProductoID);

                        if (info.AlmacenID.HasValue)
                        {
                            //Actualizar el movimiento en caso que el destino de la solicitud
                            //sea un almacén.

                            almacenInventario =
                                ObtenerAlmacenInventario(info.Detalle, info.AlmacenID.Value,
                                                         TipoMovimiento.EntradaAlmacen) ?? almacenInventario.ToList();
                            var almacenInventarioTotal = (from det in info.Detalle
                                                          join ag in almacenInventarioInfos on det.ProductoID equals
                                                              ag.ProductoID
                                                          where
                                                              almacenInventario.Count(
                                                                  x => x.ProductoID == det.ProductoID) == 0
                                                          select new AlmacenInventarioInfo
                                                                     {
                                                                         AlmacenInventarioID = -1,
                                                                         AlmacenID = info.AlmacenID.Value,
                                                                         ProductoID = det.ProductoID,
                                                                         Minimo = 0,
                                                                         Maximo = 0,
                                                                         PrecioPromedio = ag.PrecioPromedio,
                                                                         Cantidad = det.Cantidad,
                                                                         Importe = ag.PrecioPromedio * det.Cantidad,
                                                                         UsuarioModificacionID =
                                                                             info.UsuarioModificacionID.HasValue
                                                                                 ? info.UsuarioModificacionID.Value
                                                                                 : 0
                                                                         //Es para creacion
                                                                     }).ToList();
                            almacenInventarioTotal = almacenInventarioTotal.Union(almacenInventario.ToList()).ToList();

                            almacenMovimientoDetalle = ObtenerAlmacenMovimientoDetalle(info, almacenInventario);
                            var almacenMovimientoDetalleTotal = (from s in info.Detalle
                                                                 join ag in almacenInventarioInfos on s.ProductoID
                                                                     equals ag.ProductoID
                                                                 where
                                                                     almacenMovimientoDetalle.Count(
                                                                         x => x.ProductoID == s.ProductoID) == 0
                                                                 select new AlmacenMovimientoDetalle
                                                                            {
                                                                                ProductoID = s.ProductoID,
                                                                                AlmacenInventarioLoteId = 0,
                                                                                Precio = ag.PrecioPromedio,
                                                                                Cantidad = s.Cantidad,
                                                                                Importe = (s.Cantidad * ag.PrecioPromedio),
                                                                                UsuarioCreacionID = s.UsuarioCreacionID
                                                                            }).ToList();
                            almacenMovimientoDetalleTotal =
                                almacenMovimientoDetalleTotal.Union(almacenMovimientoDetalle).ToList();

                            almacenInventarioDAL.Actualizar(almacenInventarioTotal);
                            var almacenMovimientoEntradaNuevo = new AlmacenMovimientoInfo
                                                                    {
                                                                        AlmacenID = info.AlmacenID.Value,
                                                                        TipoMovimientoID =
                                                                            TipoMovimiento.EntradaAlmacen.GetHashCode(),
                                                                        Status = Estatus.AplicadoInv.GetHashCode(),
                                                                        UsuarioCreacionID =
                                                                            info.UsuarioModificacionID.HasValue
                                                                                ? info.UsuarioModificacionID.Value
                                                                                : 0
                                                                    };

                            almacenMovimientoId = almacenMovimientoDAL.Crear(almacenMovimientoEntradaNuevo);

                            almacenMovimientoDetalleTotal.ForEach(det => det.AlmacenMovimientoID = almacenMovimientoId);
                            almacenMovimientoDetalleDAL.GuardarDetalleCierreDiaInventarioPA(
                                almacenMovimientoDetalleTotal);
                        }

                        if (info.AlmacenID.HasValue)
                        {
                            poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.SalidaTraspaso);
                            listaPolizas = poliza.GeneraPoliza(info);
                            pdfPoliza = poliza.ImprimePoliza(info, listaPolizas);

                            var polizaDAL = new PolizaDAL();

                            if (listaPolizas != null && listaPolizas.Any())
                            {
                                listaPolizas.ToList().ForEach(datos =>
                                {
                                    datos.OrganizacionID = info.OrganizacionID;
                                    datos.UsuarioCreacionID = info.UsuarioIDSolicita.HasValue
                                                                        ? info.UsuarioIDSolicita.Value
                                                                        : 0;
                                    datos.Activo = EstatusEnum.Activo;
                                    datos.ArchivoEnviadoServidor = 1;
                                });
                                polizaDAL.CrearServicioPI(listaPolizas, TipoPoliza.SalidaTraspaso);
                            }
                        }
                        else
                        {
                            info.Detalle =
                                info.Detalle.Where(
                                    pro =>
                                    pro.Producto.FamiliaId != (int)FamiliasEnum.HerramientaYEquipo &&
                                    pro.Producto.FamiliaId != (int)FamiliasEnum.Combustibles).ToList();
                            poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.SalidaConsumo);
                            listaPolizas = poliza.GeneraPoliza(info);

                            pdfPoliza = poliza.ImprimePoliza(info, listaPolizas);

                            var polizaDAL = new PolizaDAL();

                            if (listaPolizas != null && listaPolizas.Any())
                            {
                                listaPolizas.ToList().ForEach(datos =>
                                {
                                    datos.OrganizacionID = info.OrganizacionID;
                                    datos.UsuarioCreacionID = info.UsuarioIDSolicita.HasValue
                                                                        ? info.UsuarioIDSolicita.Value
                                                                        : 0;
                                    datos.Activo = EstatusEnum.Activo;
                                    datos.ArchivoEnviadoServidor = 1;
                                });
                                polizaDAL.CrearServicioPI(listaPolizas, TipoPoliza.SalidaConsumo);
                            }
                        }
                        transaction.Complete();
                    }
                }
                return pdfPoliza;
            }
            catch (ExcepcionServicio)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }



        /// <summary>
        /// Metodo para Guardar/Modificar una entidad SolicitudProducto
        /// además de generar movimiento de inventario
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public MemoryStream GuardarMovimientoInventarioReplica(SolicitudProductoReplicaInfo info)
        {
            MemoryStream pdfPoliza = null;
            try
            {
                Logger.Info();
                if (info.FolioSolicitud > 0)
                {
                    //info.FechaModificacion = da.FechaServidor();
                    AlmacenInfo almacenGeneral = da.Tabla<AlmacenInfo>()
                        .FirstOrDefault(a => a.OrganizacionID == info.OrganizacionID &&
                                             a.TipoAlmacenID == (int)TipoAlmacenEnum.GeneralGanadera);

                    //Obtener PrecioPromedio de los Productos de la Familia HerramientaYEquipo y Combustibles
                    IList<SolicitudProductoReplicaDetalleInfo> idsProductosHerramienta = info.Detalle
                        .Where(d => d.Producto.FamiliaId == (int)FamiliasEnum.HerramientaYEquipo || d.Producto.FamiliaId == (int)FamiliasEnum.Combustibles).ToList();
                    if (idsProductosHerramienta.Any())
                    {
                        ObtenerPrecioPromedioINFOR(info, idsProductosHerramienta);
                    }

                    PolizaAbstract poliza = null;
                    IList<PolizaInfo> listaPolizas = null;
                    using (var transaction = new TransactionScope())
                    {
                        var almacenMovimientoDAL = new AlmacenMovimientoDAL();
                        var almacenMovimientoDetalleDAL = new AlmacenMovimientoDetalleDAL();
                        var solicitudProductoDetalleDAL = new Implementacion.SolicitudProductoDetalleDAL();
                        var almacenInventarioDAL = new AlmacenInventarioDAL();

                        info.AlmacenID = almacenGeneral.AlmacenID;
                        IEnumerable<AlmacenInventarioInfo> almacenInventario = null;
                        if (info.AlmacenDestino.AlmacenID > 0)
                        {
                            //Actualizar el movimiento en caso que el destino de la solicitud
                            //sea un almacén.
                            almacenInventario =
                                ObtenerAlmacenInventarioReplica(info.Detalle, info.AlmacenDestino.AlmacenID,
                                                         TipoMovimiento.EntradaAlmacen);
                            var almacenInventarioTotal = (from det in info.Detalle.Where(x => x.Activo).ToList()
                                                          where
                                                              almacenInventario.Count(
                                                                  x => x.ProductoID == det.ProductoID) == 0
                                                          select new AlmacenInventarioInfo
                                                          {
                                                              AlmacenInventarioID = -1,
                                                              AlmacenID = info.AlmacenDestino.AlmacenID,
                                                              ProductoID = det.ProductoID,
                                                              Minimo = 0,
                                                              Maximo = 0,
                                                              PrecioPromedio = det.PrecioUnitario,
                                                              Cantidad = det.Cantidad,
                                                              Importe = (det.Cantidad * det.PrecioUnitario),
                                                              UsuarioModificacionID = det.UsuarioCreacionID.Value,
                                                              UsuarioCreacionID = det.UsuarioCreacionID.Value

                                                              //Es para creacion
                                                          }).ToList();
                            almacenInventarioTotal = almacenInventarioTotal.Union(almacenInventario.ToList()).ToList();

                            var almacenMovimientoDetalle = ObtenerAlmacenMovimientoDetalleReplica(info, almacenInventario);
                            var almacenMovimientoDetalleTotal = (from s in info.Detalle.Where(x => x.Activo).ToList()
                                                                 where
                                                                     almacenMovimientoDetalle.Count(
                                                                         x => x.ProductoID == s.ProductoID) == 0
                                                                 select new AlmacenMovimientoDetalle
                                                                 {
                                                                     ProductoID = s.ProductoID,
                                                                     Producto = s.Producto,
                                                                     AlmacenInventarioLoteId = 0,
                                                                     Precio = s.PrecioUnitario,
                                                                     Cantidad = s.Cantidad,
                                                                     Importe = (s.Cantidad * s.PrecioUnitario),
                                                                     UsuarioCreacionID = s.UsuarioCreacionID.Value,
                                                                     UsuarioModificacionID =
                                                                          s.UsuarioModificacionID.HasValue
                                                                              ? s.UsuarioModificacionID.Value
                                                                              : 0

                                                                 }).ToList();
                            almacenMovimientoDetalleTotal =
                                almacenMovimientoDetalleTotal.Union(almacenMovimientoDetalle).ToList();

                            almacenInventarioDAL.Actualizar(almacenInventarioTotal);
                            var almacenMovimientoEntradaNuevo = new AlmacenMovimientoInfo
                            {
                                AlmacenID = info.AlmacenDestino.AlmacenID, //info.AlmacenID.Value,
                                TipoMovimientoID =
                                    TipoMovimiento.EntradaAlmacen.GetHashCode(),
                                Status = Estatus.AplicadoInv.GetHashCode(),
                                UsuarioCreacionID =
                                    info.UsuarioModificacionID.HasValue
                                        ? info.UsuarioModificacionID.Value
                                        : 0
                            };

                            var almacenMovimientoId = almacenMovimientoDAL.Crear(almacenMovimientoEntradaNuevo);
                            almacenMovimientoDetalleTotal.ForEach(det => det.AlmacenMovimientoID = almacenMovimientoId);
                            info.Detalle.ForEach(det =>
                                {
                                    if (det.Activo)
                                    {
                                        det.AlmacenMovimientoID = almacenMovimientoId;
                                    }
                                });
                            solicitudProductoDetalleDAL.MarcarProductosRecibidos(info.Detalle.Where(x => x.Activo).ToList(), info.FolioSolicitud);

                            almacenMovimientoDetalleDAL.GuardarDetalleCierreDiaInventarioPA(
                                almacenMovimientoDetalleTotal);
                        }

                        if (info.AlmacenDestino != null && info.AlmacenDestino.AlmacenID > 0)
                        {
                            if (almacenInventario != null && almacenInventario.Any())
                            {
                                AlmacenInventarioInfo almacenInventarioPrecioPromedio;
                                info.Detalle.ForEach(det =>
                                {
                                    almacenInventarioPrecioPromedio = almacenInventario.FirstOrDefault(id => id.ProductoID == det.ProductoID);
                                    if (almacenInventarioPrecioPromedio != null)
                                    {
                                        det.PrecioPromedio = almacenInventarioPrecioPromedio.PrecioPromedio;
                                    }
                                });
                            }
                            poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.EntradaTraspasoSAP);
                            listaPolizas = poliza.GeneraPoliza(info);
                            pdfPoliza = poliza.ImprimePoliza(info, listaPolizas);

                            if (listaPolizas != null && listaPolizas.Any())
                            {
                                listaPolizas.ToList().ForEach(datos =>
                                {
                                    datos.OrganizacionID = info.OrganizacionID;
                                    datos.UsuarioCreacionID = info.UsuarioCreacionID;
                                    datos.Activo = EstatusEnum.Activo;
                                    datos.ArchivoEnviadoServidor = 1;
                                });
                                var polizaDAL = new PolizaDAL();
                                polizaDAL.CrearServicioPI(listaPolizas, TipoPoliza.EntradaTraspasoSAP);
                            }
                            else
                            {
                                info.Detalle =
                                    info.Detalle.Where(
                                        pro =>
                                        pro.Producto.FamiliaId != (int)FamiliasEnum.HerramientaYEquipo &&
                                        pro.Producto.FamiliaId != (int)FamiliasEnum.Combustibles).ToList();
                                poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.SalidaConsumo);
                                listaPolizas = poliza.GeneraPoliza(info);

                                pdfPoliza = poliza.ImprimePoliza(info, listaPolizas);
                                if (listaPolizas != null && listaPolizas.Any())
                                {
                                    listaPolizas.ToList().ForEach(datos =>
                                    {
                                        datos.OrganizacionID = info.OrganizacionID;
                                        datos.UsuarioCreacionID = info.UsuarioCreacionID;
                                        datos.Activo = EstatusEnum.Activo;
                                        datos.ArchivoEnviadoServidor = 1;
                                    });
                                    var polizaDAL = new PolizaDAL();
                                    polizaDAL.CrearServicioPI(listaPolizas, TipoPoliza.SalidaConsumo);
                                }
                            }
                            transaction.Complete();
                        }
                    }
                }
                return pdfPoliza;
            }
            catch (ExcepcionServicio)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Crear conexion a INFOR para obtener el precio promedio de los productos de familias de herramientas
        /// </summary>
        /// <param name="info"></param>
        /// <param name="idsProductosHerramienta"></param>
        private void ObtenerPrecioPromedioINFOR(SolicitudProductoInfo info,
                                                IList<SolicitudProductoDetalleInfo> idsProductosHerramienta)
        {
            try
            {
                Logger.Info();
                //CONEXION INFOR
                string conexion =
                    //  @"Initial Catalog={0};Data Source={1};User ID=usrsoporte;Password=usrsoporte
                    string.Format(@"Initial Catalog={0};Data Source={1};User ID={2};Password={3}",
                                   "EAMPROD",
                                   "SRV-INFORDB",
                                   "EAMPROD",
                                   "EAMPROD");
                var solicitudProductoDAL = new Implementacion.SolicitudProductoDAL(conexion);

                List<ProductoINFORInfo> listaProductoINFORInfo =
                    solicitudProductoDAL.ObtenerPrecioPromedioProductorINFOR(info.FolioSolicitud);

                if (listaProductoINFORInfo != null && listaProductoINFORInfo.Any())
                {
                    foreach (var solicitudProductoDetalleInfo in idsProductosHerramienta)
                    {
                        solicitudProductoDetalleInfo.PrecioPromedio = 0;
                        var solicitudDAL = new Implementacion.SolicitudProductoDAL();
                        var result = solicitudDAL.ObtenerCodigoParteDeProducto(solicitudProductoDetalleInfo.Producto);
                        if (result != null)
                        {
                            var productoInforInfo = listaProductoINFORInfo.FirstOrDefault(d => d.CodigoParte == result.CodigoParte);
                            if (productoInforInfo != null)
                            {
                                solicitudProductoDetalleInfo.PrecioPromedio = Math.Round(productoInforInfo.PrecioPromedio, 2);
                            }
                        }
                    }
                }
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Crear conexion a INFOR para obtener el precio promedio de los productos de familias de herramientas
        /// </summary>
        /// <param name="info"></param>
        /// <param name="idsProductosHerramienta"></param>
        private void ObtenerPrecioPromedioINFOR(SolicitudProductoReplicaInfo info,
                                                IList<SolicitudProductoReplicaDetalleInfo> idsProductosHerramienta)
        {
            try
            {
                Logger.Info();
                //CONEXION INFOR
                string conexion =
                    //  @"Initial Catalog={0};Data Source={1};User ID=usrsoporte;Password=usrsoporte
                    string.Format(@"Initial Catalog={0};Data Source={1};User ID={2};Password={3}",
                                   "EAMPROD",
                                   "SRV-INFORDB",
                                   "EAMPROD",
                                   "EAMPROD");
                var solicitudProductoDAL = new Implementacion.SolicitudProductoDAL(conexion);

                List<ProductoINFORInfo> listaProductoINFORInfo =
                    solicitudProductoDAL.ObtenerPrecioPromedioProductorINFOR(info.FolioSolicitud);

                if (listaProductoINFORInfo != null && listaProductoINFORInfo.Any())
                {
                    foreach (var solicitudProductoDetalleInfo in idsProductosHerramienta)
                    {
                        solicitudProductoDetalleInfo.PrecioPromedio = 0;
                        var solicitudDAL = new Implementacion.SolicitudProductoDAL();
                        var result = solicitudDAL.ObtenerCodigoParteDeProducto(solicitudProductoDetalleInfo.Producto);
                        if (result != null)
                        {
                            var productoInforInfo = listaProductoINFORInfo.FirstOrDefault(d => d.CodigoParte == result.CodigoParte);
                            if (productoInforInfo != null)
                            {
                                solicitudProductoDetalleInfo.PrecioPromedio = Math.Round(productoInforInfo.PrecioPromedio, 2);
                            }
                        }
                    }
                }

            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de las existencias
        /// de cada uno de los productos del detalle de la solicitud
        /// </summary>
        /// <param name="info"></param>
        /// <param name="almacenId"> </param>
        /// <param name="tipoMovimientoInventario"> </param>
        /// <returns></returns>
        private IEnumerable<AlmacenInventarioInfo> ObtenerAlmacenInventario(IEnumerable<SolicitudProductoDetalleInfo> info, int almacenId, TipoMovimiento tipoMovimientoInventario)
        {
            //int almacenID = info.AlmacenID.HasValue ? info.AlmacenID.Value : 0;

            var almacenInventarioDAL = new AlmacenInventarioDAL();
            var solicitudProductoDetalleInfos = info as List<SolicitudProductoDetalleInfo> ?? info.ToList();
            IEnumerable<int> idsProductos = solicitudProductoDetalleInfos
                            .Where(d => d.Producto.FamiliaId != (int)FamiliasEnum.HerramientaYEquipo && d.Producto.FamiliaId != (int)FamiliasEnum.Combustibles)
                            .Select(d => d.ProductoID).Distinct().ToList();
            IList<ProductoInfo> producto = idsProductos.Select(p => new ProductoInfo { ProductoId = p }).ToList();
            IList<AlmacenInventarioInfo> almacenInventario =
                almacenInventarioDAL.ExistenciaPorProductos(almacenId, producto) ?? new List<AlmacenInventarioInfo>();
            almacenInventario.ToList().ForEach(a =>
            {
                var registro =
                    solicitudProductoDetalleInfos.FirstOrDefault(
                        d => d.ProductoID == a.ProductoID);

                var cantidad = solicitudProductoDetalleInfos.Where(d => d.ProductoID == a.ProductoID).Sum(d => d.Cantidad);
                if (registro != null)
                {
                    decimal factor = tipoMovimientoInventario == TipoMovimiento.SalidaPorTraspaso
                                         ? -1
                                         : 1;

                    decimal precioPromedio;
                    if (tipoMovimientoInventario == TipoMovimiento.SalidaPorTraspaso)
                    {
                        precioPromedio = a.PrecioPromedio;
                        registro.PrecioPromedio = a.PrecioPromedio;
                    }
                    else
                    {
                        precioPromedio = registro.PrecioPromedio;
                    }
                    decimal importeSalida = (registro.Cantidad * precioPromedio) * factor;
                    decimal cantidadMovto = cantidad * factor;
                    a.Cantidad = (a.Cantidad + cantidadMovto);
                    a.Importe = (a.Importe + importeSalida);
                    if (a.Cantidad > 0)
                    {
                        a.PrecioPromedio = a.Importe / a.Cantidad;
                    }
                    if (registro.UsuarioModificacionID != null)
                    {
                        a.UsuarioModificacionID =
                            registro.UsuarioModificacionID.Value;
                    }
                }
            });
            return almacenInventario;
        }


        /// <summary>
        /// Obtiene una lista de las existencias
        /// de cada uno de los productos del detalle de la solicitud
        /// </summary>
        /// <param name="info"></param>
        /// <param name="almacenId"> </param>
        /// <param name="tipoMovimientoInventario"> </param>
        /// <returns></returns>
        private IEnumerable<AlmacenInventarioInfo> ObtenerAlmacenInventario(IEnumerable<SolicitudProductoReplicaDetalleInfo> info, int almacenId, TipoMovimiento tipoMovimientoInventario)
        {
            //int almacenID = info.AlmacenID.HasValue ? info.AlmacenID.Value : 0;

            var almacenInventarioDAL = new AlmacenInventarioDAL();
            var solicitudProductoDetalleInfos = info.Where(x => x.Activo).ToList();
            IEnumerable<int> idsProductos = solicitudProductoDetalleInfos
                            .Where(d => d.Producto.FamiliaId != (int)FamiliasEnum.HerramientaYEquipo && d.Producto.FamiliaId != (int)FamiliasEnum.Combustibles)
                            .Select(d => d.ProductoID).Distinct().ToList();
            IList<ProductoInfo> producto = idsProductos.Select(p => new ProductoInfo { ProductoId = p }).ToList();
            IList<AlmacenInventarioInfo> almacenInventario =
                almacenInventarioDAL.ExistenciaPorProductos(almacenId, producto) ?? new List<AlmacenInventarioInfo>();
            almacenInventario.ToList().ForEach(a =>
            {
                var registro =
                    solicitudProductoDetalleInfos.FirstOrDefault(
                        d => d.ProductoID == a.ProductoID);

                var cantidad = solicitudProductoDetalleInfos.Where(d => d.ProductoID == a.ProductoID).Sum(d => d.Cantidad);
                if (registro != null)
                {
                    decimal factor = tipoMovimientoInventario == TipoMovimiento.EntradaAlmacen
                                         ? 1
                                         : -1;

                    decimal precioPromedio;
                    if (tipoMovimientoInventario == TipoMovimiento.EntradaAlmacen)
                    {
                        precioPromedio = a.PrecioPromedio;
                        registro.PrecioPromedio = a.PrecioPromedio;
                    }
                    else
                    {
                        precioPromedio = registro.PrecioPromedio;
                    }
                    decimal importeSalida = (registro.Cantidad * precioPromedio) * factor;
                    //decimal importeSalida = (registro.Cantidad * precioPromedio) * factor;
                    decimal cantidadMovto = cantidad * factor;
                    a.Cantidad = (a.Cantidad + cantidadMovto);
                    a.Importe = (a.Importe + importeSalida);
                    if (a.Cantidad > 0)
                    {
                        a.PrecioPromedio = a.Importe / a.Cantidad;
                    }

                    a.UsuarioModificacionID = registro.UsuarioCreacionID.Value;

                }
            });
            return almacenInventario;
        }

        /// <summary>
        /// Obtiene una lista de las existencias
        /// de cada uno de los productos del detalle de la solicitud
        /// </summary>
        /// <param name="info"></param>
        /// <param name="almacenId"> </param>
        /// <param name="tipoMovimientoInventario"> </param>
        /// <returns></returns>
        private IEnumerable<AlmacenInventarioInfo> ObtenerAlmacenInventarioReplica(IEnumerable<SolicitudProductoReplicaDetalleInfo> info, int almacenId, TipoMovimiento tipoMovimientoInventario)
        {
            //int almacenID = info.AlmacenID.HasValue ? info.AlmacenID.Value : 0;

            var almacenInventarioDAL = new AlmacenInventarioDAL();
            var solicitudProductoDetalleInfos = info.Where(x => x.Activo).ToList();
            IEnumerable<int> idsProductos = solicitudProductoDetalleInfos
                            .Where(d => d.Producto.FamiliaId != (int)FamiliasEnum.HerramientaYEquipo && d.Producto.FamiliaId != (int)FamiliasEnum.Combustibles)
                            .Select(d => d.ProductoID).Distinct().ToList();
            IList<ProductoInfo> producto = idsProductos.Select(p => new ProductoInfo { ProductoId = p }).ToList();
            IList<AlmacenInventarioInfo> almacenInventario =
                almacenInventarioDAL.ExistenciaPorProductos(almacenId, producto) ?? new List<AlmacenInventarioInfo>();
            almacenInventario.ToList().ForEach(a =>
            {
                var registro =
                    solicitudProductoDetalleInfos.FirstOrDefault(
                        d => d.ProductoID == a.ProductoID);

                var cantidad = solicitudProductoDetalleInfos.Where(d => d.ProductoID == a.ProductoID).Sum(d => d.Cantidad);
                if (registro != null)
                {
                    decimal factor = tipoMovimientoInventario == TipoMovimiento.EntradaAlmacen
                                         ? 1
                                         : -1;

                    //decimal precioPromedio;
                    //if (tipoMovimientoInventario == TipoMovimiento.EntradaAlmacen)
                    //{
                    //    //precioPromedio = a.PrecioPromedio;
                    //    registro.PrecioPromedio = a.PrecioPromedio;
                    //}
                    //else
                    //{
                    //    precioPromedio = registro.PrecioPromedio;
                    //}
                    decimal importeSalida = (registro.Cantidad * registro.PrecioUnitario) * factor;
                    //decimal importeSalida = (registro.Cantidad * precioPromedio) * factor;
                    decimal cantidadMovto = cantidad * factor;
                    a.Cantidad = (a.Cantidad + cantidadMovto);
                    a.Importe = (a.Importe + importeSalida);
                    if (a.Cantidad > 0)
                    {
                        a.PrecioPromedio = a.Importe / a.Cantidad;
                    }

                    a.UsuarioModificacionID = registro.UsuarioCreacionID.Value;

                }
            });
            return almacenInventario;
        }

        /// <summary>
        /// Crea la lista del detalle de almacén Movimiento.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="almacenInventario"></param>
        /// <returns></returns>
        private IEnumerable<AlmacenMovimientoDetalle> ObtenerAlmacenMovimientoDetalle(SolicitudProductoInfo info,
            IEnumerable<AlmacenInventarioInfo> almacenInventario)
        {

            var detalle = from s in info.Detalle
                          select s;

            var query = from s in detalle
                        join a in almacenInventario on s.ProductoID equals a.ProductoID
                        select new AlmacenMovimientoDetalle
                                   {
                                       ProductoID = s.ProductoID,
                                       Precio = s.PrecioPromedio,
                                       Cantidad = s.Cantidad,
                                       Importe = (s.Cantidad * s.PrecioPromedio),
                                       UsuarioCreacionID = s.UsuarioCreacionID
                                   };

            return query.ToList();
        }

        /// <summary>
        /// Crea la lista del detalle de almacén Movimiento.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="almacenInventario"></param>
        /// <returns></returns>
        private IEnumerable<AlmacenMovimientoDetalle> ObtenerAlmacenMovimientoDetalleReplica(SolicitudProductoReplicaInfo info,
            IEnumerable<AlmacenInventarioInfo> almacenInventario)
        {
            List<SolicitudProductoReplicaDetalleInfo> det = info.Detalle;
            var detalle = from s in det
                          select s;

            var query = from s in detalle
                        join a in almacenInventario on s.ProductoID equals a.ProductoID
                        select new AlmacenMovimientoDetalle
                        {
                            ProductoID = s.ProductoID,
                            Precio = s.PrecioUnitario,
                            Cantidad = s.Cantidad,
                            Importe = (s.Cantidad * s.PrecioUnitario),
                            UsuarioCreacionID = s.UsuarioCreacionID.Value
                        };

            return query.ToList();
        }

    }
}