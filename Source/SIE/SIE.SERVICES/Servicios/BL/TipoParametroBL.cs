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
    internal class TipoParametroBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad TipoParametro
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(TipoParametroInfo info)
        {
            try
            {
                Logger.Info();
                var tipoParametroDAL = new TipoParametroDAL();
                int result = info.TipoParametroID;
                if (info.TipoParametroID == 0)
                {
                    result = tipoParametroDAL.Crear(info);
                }
                else
                {
                    tipoParametroDAL.Actualizar(info);
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
        internal ResultadoInfo<TipoParametroInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoParametroInfo filtro)
        {
            try
            {
                Logger.Info();
                var tipoParametroDAL = new TipoParametroDAL();
                ResultadoInfo<TipoParametroInfo> result = tipoParametroDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de TipoParametro
        /// </summary>
        /// <returns></returns>
        internal IList<TipoParametroInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var tipoParametroDAL = new TipoParametroDAL();
                IList<TipoParametroInfo> result = tipoParametroDAL.ObtenerTodos();
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
        internal IList<TipoParametroInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoParametroDAL = new TipoParametroDAL();
                IList<TipoParametroInfo> result = tipoParametroDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad TipoParametro por su Id
        /// </summary>
        /// <param name="tipoParametroID">Obtiene una entidad TipoParametro por su Id</param>
        /// <returns></returns>
        internal TipoParametroInfo ObtenerPorID(int tipoParametroID)
        {
            try
            {
                Logger.Info();
                var tipoParametroDAL = new TipoParametroDAL();
                TipoParametroInfo result = tipoParametroDAL.ObtenerPorID(tipoParametroID);
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
        /// Obtiene una entidad TipoParametro por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal TipoParametroInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tipoParametroDAL = new TipoParametroDAL();
                TipoParametroInfo result = tipoParametroDAL.ObtenerPorDescripcion(descripcion);
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

