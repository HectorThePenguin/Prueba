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
    public class RegistroVigilanciaHumedadBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad RegistroVigilanciaHumedad
        /// </summary>
        /// <param name="info"></param>
        public int Guardar(RegistroVigilanciaHumedadInfo info)
        {
            try
            {
                Logger.Info();
                var registroVigilanciaHumedadDAL = new RegistroVigilanciaHumedadDAL();
                int result = info.RegistroVigilanciaHumedadID;
                if (info.RegistroVigilanciaHumedadID == 0)
                {
                    result = registroVigilanciaHumedadDAL.Crear(info);
                }
                else
                {
                    registroVigilanciaHumedadDAL.Actualizar(info);
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
        public ResultadoInfo<RegistroVigilanciaHumedadInfo> ObtenerPorPagina(PaginacionInfo pagina, RegistroVigilanciaHumedadInfo filtro)
        {
            try
            {
                Logger.Info();
                var registroVigilanciaHumedadDAL = new RegistroVigilanciaHumedadDAL();
                ResultadoInfo<RegistroVigilanciaHumedadInfo> result = registroVigilanciaHumedadDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de RegistroVigilanciaHumedad
        /// </summary>
        /// <returns></returns>
        public IList<RegistroVigilanciaHumedadInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var registroVigilanciaHumedadDAL = new RegistroVigilanciaHumedadDAL();
                IList<RegistroVigilanciaHumedadInfo> result = registroVigilanciaHumedadDAL.ObtenerTodos();
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
        public IList<RegistroVigilanciaHumedadInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var registroVigilanciaHumedadDAL = new RegistroVigilanciaHumedadDAL();
                IList<RegistroVigilanciaHumedadInfo> result = registroVigilanciaHumedadDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad RegistroVigilanciaHumedad por su Id
        /// </summary>
        /// <param name="registroVigilanciaHumedadID">Obtiene una entidad RegistroVigilanciaHumedad por su Id</param>
        /// <returns></returns>
        public RegistroVigilanciaHumedadInfo ObtenerPorID(int registroVigilanciaHumedadID)
        {
            try
            {
                Logger.Info();
                var registroVigilanciaHumedadDAL = new RegistroVigilanciaHumedadDAL();
                RegistroVigilanciaHumedadInfo result = registroVigilanciaHumedadDAL.ObtenerPorID(registroVigilanciaHumedadID);
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
        /// Obtiene una entidad RegistroVigilanciaHumedad por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public RegistroVigilanciaHumedadInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var registroVigilanciaHumedadDAL = new RegistroVigilanciaHumedadDAL();
                RegistroVigilanciaHumedadInfo result = registroVigilanciaHumedadDAL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene un registro de RegistroVigilanciaHumedad
        /// </summary>
        /// <param name="registroVigilanciaID">Identificador del Registro de Vigilancia</param>
        /// <returns></returns>
        public RegistroVigilanciaHumedadInfo ObtenerPorRegistroVigilanciaID(int registroVigilanciaID)
        {
            try
            {
                Logger.Info();
                var registroVigilanciaHumedadDAL = new RegistroVigilanciaHumedadDAL();
                RegistroVigilanciaHumedadInfo result = registroVigilanciaHumedadDAL.ObtenerPorRegistroVigilanciaID(registroVigilanciaID);
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

