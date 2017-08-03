using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    internal class SolicitudProductoDetalleDAL : BaseDAL
    {
        SolicitudProductoDetalleAccessor solicitudProductoDetalleAccessor;

        protected override void inicializar()
        {
            solicitudProductoDetalleAccessor = da.inicializarAccessor<SolicitudProductoDetalleAccessor>();
        }

        protected override void destruir()
        {
            solicitudProductoDetalleAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista de SolicitudProductoDetalle
        /// </summary>
        /// <returns></returns>
        public IQueryable<SolicitudProductoDetalleInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<SolicitudProductoDetalleInfo>();
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de SolicitudProductoDetalle filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<SolicitudProductoDetalleInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.Activo == estatus);
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad de SolicitudProductoDetalle por su Id
        /// </summary>
        /// <param name="solicitudProductoDetalleId">Obtiene una entidad SolicitudProductoDetalle por su Id</param>
        /// <returns></returns>
        public SolicitudProductoDetalleInfo ObtenerPorID(int solicitudProductoDetalleId)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().FirstOrDefault(e => e.SolicitudProductoDetalleID == solicitudProductoDetalleId);
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad de SolicitudProductoDetalle por su descripcion
        /// </summary>
        /// <param name="filtro">Obtiene una entidad SolicitudProductoDetalle por su descripcion</param>
        /// <returns></returns>
        public IQueryable<SolicitudProductoDetalleInfo> ObtenerPorSolicitudProductoId(int filtro)
        {
            try
            {
                Logger.Info();
                return ObtenerTodos().Where(e => e.SolicitudProductoID == filtro);
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        ///// <summary>
        ///// Obtiene una entidad de SolicitudProductoDetalle por su descripcion
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable<SolicitudProductoDetalleInfo> ObtenerPorSolicitudProductoCompletoPorId(SolicitudProductoInfo filtro)
        //{
        //    try
        //    {
        //        Logger.Info();
        //        var condicion = ObtenerTodos();

        //        if (filtro.SolicitudProductoID > 0)
        //        {
        //            condicion = condicion.Where(c => c.SolicitudProductoID == filtro.SolicitudProductoID);
        //        }

        //        var productoDAL = new ProductoDAL();
        //        var centroCostoDAL = new CentroCostoDAL();
        //        var estatusDAL = new EstatusDAL();

        //        var prods = productoDAL.ObtenerTodosConUnidad();
        //        var cecos = centroCostoDAL.ObtenerTodos(EstatusEnum.Activo);
        //        var estatus = estatusDAL.ObtenerTodos(EstatusEnum.Activo);

        //        var query = from q in condicion
        //                    join p in prods on q.ProductoID equals p.ProductoId
        //                    join c in cecos on q.CentroCostoID equals c.CentroCostoID
        //                    join e in estatus on q.EstatusID equals e.EstatusId
        //                    select new SolicitudProductoDetalleInfo(q, p, c, e);

        //        return query;
        //    }
        //    catch (ExcepcionGenerica)
        //    {
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //        throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
        //    }
        //}



        /// <summary>
        /// Metodo para Guardar/Modificar una entidad SolicitudProductoDetalle
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(SolicitudProductoDetalleInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.SolicitudProductoDetalleID > 0)
                {
                    info.FechaModificacion = da.FechaServidor();
                    id = da.Actualizar<SolicitudProductoDetalleInfo>(info);
                }
                else
                {
                    id = da.Insertar<SolicitudProductoDetalleInfo>(info);
                }
                return id;
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
