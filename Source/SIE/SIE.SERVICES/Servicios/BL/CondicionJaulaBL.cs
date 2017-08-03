using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    public class CondicionJaulaBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad CondicionJaula
        /// </summary>
        /// <param name="info"></param>
        public int Guardar(CondicionJaulaInfo info)
        {
            try
            {
                Logger.Info();
                var condicionJaulaDAL = new CondicionJaulaDAL();
                int result = info.CondicionJaulaID;
                if (info.CondicionJaulaID == 0)
                {
                    result = condicionJaulaDAL.Crear(info);
                }
                else
                {
                    condicionJaulaDAL.Actualizar(info);
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
        public ResultadoInfo<CondicionJaulaInfo> ObtenerPorPagina(PaginacionInfo pagina, CondicionJaulaInfo filtro)
        {
            try
            {
                Logger.Info();
                var condicionJaulaDAL = new CondicionJaulaDAL();
                ResultadoInfo<CondicionJaulaInfo> result = condicionJaulaDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de CondicionJaula
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CondicionJaulaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var condicionJaulaDAL = new CondicionJaulaDAL();
                IEnumerable<CondicionJaulaInfo> result = condicionJaulaDAL.ObtenerTodos();
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
        public IEnumerable<CondicionJaulaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var condicionJaulaDAL = new CondicionJaulaDAL();
                IEnumerable<CondicionJaulaInfo> result = condicionJaulaDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad CondicionJaula por su Id
        /// </summary>
        /// <param name="condicionJaulaID">Obtiene una entidad CondicionJaula por su Id</param>
        /// <returns></returns>
        public CondicionJaulaInfo ObtenerPorID(int condicionJaulaID)
        {
            try
            {
                Logger.Info();
                var condicionJaulaDAL = new CondicionJaulaDAL();
                IEnumerable<CondicionJaulaInfo> result = condicionJaulaDAL.ObtenerPorID(condicionJaulaID);
                CondicionJaulaInfo condicionJaula = ObtieneCondicionJaula(result);
                return condicionJaula;
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
        /// Obtiene una entidad CondicionJaula por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public CondicionJaulaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var condicionJaulaDAL = new CondicionJaulaDAL();
                IEnumerable<CondicionJaulaInfo> result = condicionJaulaDAL.ObtenerPorDescripcion(descripcion);
                CondicionJaulaInfo condicionJaula = ObtieneCondicionJaula(result);
                return condicionJaula;
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
        /// Obtiene el primer elemento de la coleccion
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private CondicionJaulaInfo ObtieneCondicionJaula(IEnumerable<CondicionJaulaInfo> result)
        {
            CondicionJaulaInfo condicionJaula = null;
            if (result != null)
            {
                condicionJaula = result.FirstOrDefault();
            }
            return condicionJaula;
        }
    }
}
