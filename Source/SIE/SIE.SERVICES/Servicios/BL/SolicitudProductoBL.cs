using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.ORM;

namespace SIE.Services.Servicios.BL
{
    public class SolicitudProductoBL : IDisposable
    {
        private SolicitudProductoDAL solicitudProductoDAL;

        public SolicitudProductoBL()
        {
            solicitudProductoDAL = new SolicitudProductoDAL();
        }

        public void Dispose()
        {
            solicitudProductoDAL.Disposed += (s, e) =>
                                                 {
                                                     solicitudProductoDAL = null;
                                                 };
            solicitudProductoDAL.Dispose();
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
                return solicitudProductoDAL.ObtenerPorPagina(pagina, filtro);
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
        public IList<SolicitudProductoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return solicitudProductoDAL.ObtenerTodos().ToList();
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
        public IList<SolicitudProductoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return solicitudProductoDAL.ObtenerTodos().Where(e => e.Activo == estatus).ToList();
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
        public SolicitudProductoInfo ObtenerPorID(SolicitudProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                return solicitudProductoDAL.ObtenerPorID(filtro.SolicitudProductoID);
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
                return solicitudProductoDAL.ObtenerPorFolioSolicitud(filtro);
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
        /// Obtiene una lista de solicitudes autorizada
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public IList<SolicitudProductoInfo> ObtenerSolicitudesAutorizadas(FolioSolicitudInfo filtro)
        {
            try
            {
                Logger.Info();
                return solicitudProductoDAL.ObtenerSolicitudesAutorizadas(filtro);
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
                return solicitudProductoDAL.Guardar(info);
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
        /// además de generar movimiento de inventario
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public MemoryStream GuardarMovimientoInventario(SolicitudProductoInfo info)
        {
            try
            {
                Logger.Info();
                return solicitudProductoDAL.GuardarMovimientoInventario(info);
            }
            catch (ExcepcionServicio ex)
            {
                throw ex;
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
        /// además de generar movimiento de inventario
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public MemoryStream GuardarMovimientoInventario(SolicitudProductoReplicaInfo info)
        {
            try
            {
                Logger.Info();
                return solicitudProductoDAL.GuardarMovimientoInventarioReplica(info);
            }
            catch (ExcepcionServicio ex)
            {
                throw ex;
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
        /// Obtiene el filtro de las familiar 
        /// que se desea consultar.
        /// </summary>
        public IList<FamiliasEnum> ObtenerFamiliasFiltrar()
        {
            var filtro = new List<FamiliasEnum>
                             {
                                 FamiliasEnum.MateriaPrimas,
                                 FamiliasEnum.Alimento,
                                 FamiliasEnum.Premezclas,
                             };
            var lista = from FamiliasEnum e in Enum.GetValues(typeof (FamiliasEnum)).AsQueryable()
                        where !filtro.Contains(e)
                        select e;

            return lista.ToList();
        }

        /// <summary>
        /// Obtiene el filtro filtrar por tipo de almacén
        /// que se desea consultar.
        /// </summary>
        public IList<TipoAlmacenEnum> ObtenerTipoAlmacenFiltrar()
        {
            var filtrar = new List<TipoAlmacenEnum>
                              {
                                  TipoAlmacenEnum.CentroAcopio,
                                  TipoAlmacenEnum.Pradera,
                                  TipoAlmacenEnum.Enfermeria,
                                  TipoAlmacenEnum.ManejoGanado,
                                  TipoAlmacenEnum.ReimplanteGanado
                              };
            var lista = from TipoAlmacenEnum e in Enum.GetValues(typeof (TipoAlmacenEnum)).AsQueryable()
                        where filtrar.Contains(e)
                        select e;

            return lista.ToList();
        }

        /// <summary>
        /// Metodo para valdiar si el producto existe como codigo parte
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        public ProductoCodigoParteInfo ExisteCodigoParteDeproducto(ProductoInfo producto)
        {
            ProductoCodigoParteInfo result;
            try
            {
                Logger.Info();
                var solicitudDAL = new Integracion.DAL.Implementacion.SolicitudProductoDAL();
                result = solicitudDAL.ObtenerCodigoParteDeProducto(producto);

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
            return result;
        }

        /// <summary>
        /// Valida si existe el folio de la solicitud en INFOR
        /// </summary>
        /// <param name="folioSolicitud"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public bool ExisteFolioEnINFOR(long folioSolicitud,int organizacionId)
        {
            bool result= false;
            try
            {
                Logger.Info();
                //CONEXION INFOR
                string conexion =
                    string.Format(@"Initial Catalog={0};Data Source={1};User ID={2};Password={3}",
                                   "EAMPROD", 
                                   "SRV-INFORDB",
                                   "EAMPROD",
                                   "EAMPROD");
                var solicitudProductoDAL = new Integracion.DAL.Implementacion.SolicitudProductoDAL(conexion);

                List<ProductoINFORInfo> listaProductoINFORInfo =
                    solicitudProductoDAL.ObtenerPrecioPromedioProductorINFOR(folioSolicitud);

                if (listaProductoINFORInfo != null && listaProductoINFORInfo.Any())
                {
                    result = true;
                }
            }
            catch (ExcepcionServicio exs)
            {
                Logger.Error(exs);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), exs);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene una lista de solicitud de productos
        /// </summary>
        /// <param name="almacenMovimientos"></param>
        /// <returns></returns>
        public List<SolicitudProductoInfo> ObtenerConciliacionPorAlmacenMovimientoXML(List<AlmacenMovimientoInfo> almacenMovimientos)
        {
            List<SolicitudProductoInfo> result;
            try
            {
                Logger.Info();                
                var solicitudProductoDAL = new Integracion.DAL.Implementacion.SolicitudProductoDAL();
                result = solicitudProductoDAL.ObtenerConciliacionPorAlmacenMovimientoXML(almacenMovimientos);
            }
            catch (ExcepcionServicio exs)
            {
                Logger.Error(exs);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), exs);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        public bool GuardarInformacionCentros(string numeroDocumento, int usuarioId, int organizacionId, List<AreteInfo> aretes)
        {
            //GuardarInformacionCentros
            var result = false;
            try
            {
                Logger.Info();
                result =
                    new SIE.Services.Integracion.DAL.Implementacion.SolicitudProductoDAL().GuardarInformacionCentros(
                        numeroDocumento, usuarioId, organizacionId, aretes);
               
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
            return result;
        }

        public string ValidarAretesDuplicados(int organizacionId, List<AreteInfo> aretes)
        {
            try
            {
                Logger.Info();
                return new SIE.Services.Integracion.DAL.Implementacion.SolicitudProductoDAL().ValidarAretesDuplicados(organizacionId, aretes);
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
    }
}