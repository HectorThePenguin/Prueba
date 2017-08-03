using System;
using System.Collections.Generic;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    public class RepartoAlimentoDetalleBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad RepartoAlimentoDetalle
        /// </summary>
        /// <param name="info"></param>
        public int Guardar(RepartoAlimentoDetalleInfo info)
        {
            try
            {
                Logger.Info();
                var repartoAlimentoDetalleDAL = new RepartoAlimentoDetalleDAL();
                int result = info.RepartoAlimentoDetalleID;
                if (info.RepartoAlimentoDetalleID == 0)
                {
                    result = repartoAlimentoDetalleDAL.Crear(info);
                }
                else
                {
                    repartoAlimentoDetalleDAL.Actualizar(info);
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
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<RepartoAlimentoDetalleInfo> ObtenerPorPagina(PaginacionInfo pagina, RepartoAlimentoDetalleInfo filtro)
        {
            try
            {
                Logger.Info();
                var repartoAlimentoDetalleDAL = new RepartoAlimentoDetalleDAL();
                ResultadoInfo<RepartoAlimentoDetalleInfo> result = repartoAlimentoDetalleDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de RepartoAlimentoDetalle
        /// </summary>
        /// <returns></returns>
        public IList<RepartoAlimentoDetalleInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var repartoAlimentoDetalleDAL = new RepartoAlimentoDetalleDAL();
                IList<RepartoAlimentoDetalleInfo> result = repartoAlimentoDetalleDAL.ObtenerTodos();
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
        /// Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<RepartoAlimentoDetalleInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var repartoAlimentoDetalleDAL = new RepartoAlimentoDetalleDAL();
                IList<RepartoAlimentoDetalleInfo> result = repartoAlimentoDetalleDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad RepartoAlimentoDetalle por su Id
        /// </summary>
        /// <param name="repartoAlimentoDetalleID">Obtiene una entidad RepartoAlimentoDetalle por su Id</param>
        /// <returns></returns>
        public RepartoAlimentoDetalleInfo ObtenerPorID(int repartoAlimentoDetalleID)
        {
            try
            {
                Logger.Info();
                var repartoAlimentoDetalleDAL = new RepartoAlimentoDetalleDAL();
                RepartoAlimentoDetalleInfo result = repartoAlimentoDetalleDAL.ObtenerPorID(repartoAlimentoDetalleID);
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
        /// Obtiene una entidad RepartoAlimentoDetalle por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public RepartoAlimentoDetalleInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var repartoAlimentoDetalleDAL = new RepartoAlimentoDetalleDAL();
                RepartoAlimentoDetalleInfo result = repartoAlimentoDetalleDAL.ObtenerPorDescripcion(descripcion);
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
    }
}

