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
    public class TiempoMuertoBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad TiempoMuerto
        /// </summary>
        /// <param name="info"></param>
        public int Guardar(TiempoMuertoInfo info)
        {
            try
            {
                Logger.Info();
                var tiempoMuertoDAL = new TiempoMuertoDAL();
                int result = info.TiempoMuertoID;
                if (info.TiempoMuertoID == 0)
                {
                    result = tiempoMuertoDAL.Crear(info);
                }
                else
                {
                    tiempoMuertoDAL.Actualizar(info);
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
        public ResultadoInfo<TiempoMuertoInfo> ObtenerPorPagina(PaginacionInfo pagina, TiempoMuertoInfo filtro)
        {
            try
            {
                Logger.Info();
                var tiempoMuertoDAL = new TiempoMuertoDAL();
                ResultadoInfo<TiempoMuertoInfo> result = tiempoMuertoDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de TiempoMuerto
        /// </summary>
        /// <returns></returns>
        public IList<TiempoMuertoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var tiempoMuertoDAL = new TiempoMuertoDAL();
                IList<TiempoMuertoInfo> result = tiempoMuertoDAL.ObtenerTodos();
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
        public IList<TiempoMuertoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tiempoMuertoDAL = new TiempoMuertoDAL();
                IList<TiempoMuertoInfo> result = tiempoMuertoDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad TiempoMuerto por su Id
        /// </summary>
        /// <param name="tiempoMuertoID">Obtiene una entidad TiempoMuerto por su Id</param>
        /// <returns></returns>
        public TiempoMuertoInfo ObtenerPorID(int tiempoMuertoID)
        {
            try
            {
                Logger.Info();
                var tiempoMuertoDAL = new TiempoMuertoDAL();
                TiempoMuertoInfo result = tiempoMuertoDAL.ObtenerPorID(tiempoMuertoID);
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

