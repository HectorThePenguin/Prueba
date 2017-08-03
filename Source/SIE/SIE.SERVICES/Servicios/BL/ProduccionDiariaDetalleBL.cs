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
    public class ProduccionDiariaDetalleBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad ProduccionDiariaDetalle
        /// </summary>
        /// <param name="info"></param>
        public int Guardar(ProduccionDiariaDetalleInfo info)
        {
            try
            {
                Logger.Info();
                var produccionDiariaDetalleDAL = new ProduccionDiariaDetalleDAL();
                int result = info.ProduccionDiariaDetalleID;
                if (info.ProduccionDiariaDetalleID == 0)
                {
                    result = produccionDiariaDetalleDAL.Crear(info);
                }
                else
                {
                    produccionDiariaDetalleDAL.Actualizar(info);
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
        public ResultadoInfo<ProduccionDiariaDetalleInfo> ObtenerPorPagina(PaginacionInfo pagina, ProduccionDiariaDetalleInfo filtro)
        {
            try
            {
                Logger.Info();
                var produccionDiariaDetalleDAL = new ProduccionDiariaDetalleDAL();
                ResultadoInfo<ProduccionDiariaDetalleInfo> result = produccionDiariaDetalleDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de ProduccionDiariaDetalle
        /// </summary>
        /// <returns></returns>
        public IList<ProduccionDiariaDetalleInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var produccionDiariaDetalleDAL = new ProduccionDiariaDetalleDAL();
                IList<ProduccionDiariaDetalleInfo> result = produccionDiariaDetalleDAL.ObtenerTodos();
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
        public IList<ProduccionDiariaDetalleInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var produccionDiariaDetalleDAL = new ProduccionDiariaDetalleDAL();
                IList<ProduccionDiariaDetalleInfo> result = produccionDiariaDetalleDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad ProduccionDiariaDetalle por su Id
        /// </summary>
        /// <param name="produccionDiariaDetalleID">Obtiene una entidad ProduccionDiariaDetalle por su Id</param>
        /// <returns></returns>
        public ProduccionDiariaDetalleInfo ObtenerPorID(int produccionDiariaDetalleID)
        {
            try
            {
                Logger.Info();
                var produccionDiariaDetalleDAL = new ProduccionDiariaDetalleDAL();
                ProduccionDiariaDetalleInfo result = produccionDiariaDetalleDAL.ObtenerPorID(produccionDiariaDetalleID);
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
        /// Obtiene una entidad ProduccionDiariaDetalle por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public ProduccionDiariaDetalleInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var produccionDiariaDetalleDAL = new ProduccionDiariaDetalleDAL();
                ProduccionDiariaDetalleInfo result = produccionDiariaDetalleDAL.ObtenerPorDescripcion(descripcion);
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

