using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.ORM;

namespace SIE.Services.Servicios.BL
{
    public class SolicitudProductoDetalleBL : IDisposable
    {
        SolicitudProductoDetalleDAL solicitudProductoDetalleDAL;

        public SolicitudProductoDetalleBL()
        {
            solicitudProductoDetalleDAL = new SolicitudProductoDetalleDAL();
        }

        public void Dispose()
        {
            solicitudProductoDetalleDAL.Disposed += (s, e) =>
            {
                solicitudProductoDetalleDAL = null;
            };
            solicitudProductoDetalleDAL.Dispose();
        }
        
        /// <summary>
        /// Obtiene una lista de SolicitudProductoDetalle
        /// </summary>
        /// <returns></returns>
        public IList<SolicitudProductoDetalleInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return solicitudProductoDetalleDAL.ObtenerTodos().ToList();
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
        public IList<SolicitudProductoDetalleInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return solicitudProductoDetalleDAL.ObtenerTodos().Where(e=> e.Activo == estatus).ToList();
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
                return solicitudProductoDetalleDAL.ObtenerTodos().FirstOrDefault(e => e.SolicitudProductoDetalleID == solicitudProductoDetalleId);
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
        /// Metodo para Guardar/Modificar una entidad SolicitudProductoDetalle
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(SolicitudProductoDetalleInfo info)
        {
            try
            {
                Logger.Info();
                return solicitudProductoDetalleDAL.Guardar(info);
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
