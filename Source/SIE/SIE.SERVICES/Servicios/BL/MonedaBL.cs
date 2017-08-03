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
    public class MonedaBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Moneda
        /// </summary>
        /// <param name="info"></param>
        public int Guardar(MonedaInfo info)
        {
            try
            {
                Logger.Info();
                var monedaDAL = new MonedaDAL();
                int result = info.MonedaID;
                if (info.MonedaID == 0)
                {
                    result = monedaDAL.Crear(info);
                }
                else
                {
                    monedaDAL.Actualizar(info);
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
        public ResultadoInfo<MonedaInfo> ObtenerPorPagina(PaginacionInfo pagina, MonedaInfo filtro)
        {
            try
            {
                Logger.Info();
                var monedaDAL = new MonedaDAL();
                ResultadoInfo<MonedaInfo> result = monedaDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de Moneda
        /// </summary>
        /// <returns></returns>
        public IList<MonedaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var monedaDAL = new MonedaDAL();
                IList<MonedaInfo> result = monedaDAL.ObtenerTodos();
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
        public IList<MonedaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var monedaDAL = new MonedaDAL();
                IList<MonedaInfo> result = monedaDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad Moneda por su Id
        /// </summary>
        /// <param name="monedaID">Obtiene una entidad Moneda por su Id</param>
        /// <returns></returns>
        public MonedaInfo ObtenerPorID(int monedaID)
        {
            try
            {
                Logger.Info();
                var monedaDAL = new MonedaDAL();
                MonedaInfo result = monedaDAL.ObtenerPorID(monedaID);
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
        /// Obtiene una entidad Moneda por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public MonedaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var monedaDAL = new MonedaDAL();
                MonedaInfo result = monedaDAL.ObtenerPorDescripcion(descripcion);
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

