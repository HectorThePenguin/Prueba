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
    internal class ParametroBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Parametro
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(ParametroInfo info)
        {
            try
            {
                Logger.Info();
                var parametroDAL = new ParametroDAL();
                int result = info.ParametroID;
                if (info.ParametroID == 0)
                {
                    result = parametroDAL.Crear(info);
                }
                else
                {
                    parametroDAL.Actualizar(info);
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
        internal ResultadoInfo<ParametroInfo> ObtenerPorPagina(PaginacionInfo pagina, ParametroInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametroDAL = new ParametroDAL();
                ResultadoInfo<ParametroInfo> result = parametroDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de Parametro
        /// </summary>
        /// <returns></returns>
        internal IList<ParametroInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var parametroDAL = new ParametroDAL();
                IList<ParametroInfo> result = parametroDAL.ObtenerTodos();
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
        internal IList<ParametroInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var parametroDAL = new ParametroDAL();
                IList<ParametroInfo> result = parametroDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad Parametro por su Id
        /// </summary>
        /// <param name="parametroID">Obtiene una entidad Parametro por su Id</param>
        /// <returns></returns>
        internal ParametroInfo ObtenerPorID(int parametroID)
        {
            try
            {
                Logger.Info();
                var parametroDAL = new ParametroDAL();
                ParametroInfo result = parametroDAL.ObtenerPorID(parametroID);
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
        /// Obtiene una entidad Parametro por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal ParametroInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var parametroDAL = new ParametroDAL();
                ParametroInfo result = parametroDAL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene una entidad por su Clave
        /// </summary>
        /// <param name="parametroInfo"></param>
        /// <returns></returns>
        internal ParametroInfo ObtenerPorParametroTipoParametro(ParametroInfo parametroInfo)
        {
            try
            {
                Logger.Info();
                var parametroDAL = new ParametroDAL();
                ParametroInfo result = parametroDAL.ObtenerPorParametroTipoParametro(parametroInfo);
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
