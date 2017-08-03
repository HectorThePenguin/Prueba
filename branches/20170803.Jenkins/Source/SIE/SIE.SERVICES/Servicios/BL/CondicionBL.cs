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
    internal class CondicionBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Condicion
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(CondicionInfo info)
        {
            try
            {
                Logger.Info();
                var condicionDAL = new CondicionDAL();
                int result = info.CondicionID;
                if (info.CondicionID == 0)
                {
                    result = condicionDAL.Crear(info);
                }
                else
                {
                    condicionDAL.Actualizar(info);
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
        internal ResultadoInfo<CondicionInfo> ObtenerPorPagina(PaginacionInfo pagina, CondicionInfo filtro)
        {
            try
            {
                Logger.Info();
                var condicionDAL = new CondicionDAL();
                ResultadoInfo<CondicionInfo> result = condicionDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de Condicion
        /// </summary>
        /// <returns></returns>
        internal IList<CondicionInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var condicionDAL = new CondicionDAL();
                IList<CondicionInfo> result = condicionDAL.ObtenerTodos();
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
        internal IList<CondicionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var condicionDAL = new CondicionDAL();
                IList<CondicionInfo> result = condicionDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad Condicion por su Id
        /// </summary>
        /// <param name="condicionID">Obtiene una entidad Condicion por su Id</param>
        /// <returns></returns>
        internal CondicionInfo ObtenerPorID(int condicionID)
        {
            try
            {
                Logger.Info();
                var condicionDAL = new CondicionDAL();
                CondicionInfo result = condicionDAL.ObtenerPorID(condicionID);
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
        /// Obtiene una entidad Condicion por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal CondicionInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var condicionDAL = new CondicionDAL();
                CondicionInfo result = condicionDAL.ObtenerPorDescripcion(descripcion);
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

