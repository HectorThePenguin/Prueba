using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class TrampaPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Trampa
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(TrampaInfo info)
        {
            try
            {
                Logger.Info();
                var trampaBL = new TrampaBL();
                int result = trampaBL.Guardar(info);
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
        public ResultadoInfo<TrampaInfo> ObtenerPorPagina(PaginacionInfo pagina, TrampaInfo filtro)
        {
            try
            {
                Logger.Info();
                var trampaBL = new TrampaBL();
                ResultadoInfo<TrampaInfo> result = trampaBL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una lista
        /// </summary>
        /// <returns></returns>
        public IList<TrampaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var trampaBL = new TrampaBL();
                IList<TrampaInfo> result = trampaBL.ObtenerTodos();
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
        ///  Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<TrampaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var trampaBL = new TrampaBL();
                IList<TrampaInfo> result = trampaBL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad por su Id
        /// </summary>
        /// <param name="trampaID"></param>
        /// <returns></returns>
        public TrampaInfo ObtenerPorID(int trampaID)
        {
            try
            {
                Logger.Info();
                var trampaBL = new TrampaBL();
                TrampaInfo result = trampaBL.ObtenerPorID(trampaID);
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
        /// Obtiene una entidad por su Id
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public TrampaInfo ObtenerPorID(TrampaInfo filtro)
        {
            try
            {
                Logger.Info();
                var trampaBL = new TrampaBL();
                TrampaInfo result = trampaBL.ObtenerPorID(filtro.TrampaID);
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
        /// Obtiene una entidad por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public TrampaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var trampaBL = new TrampaBL();
                TrampaInfo result = trampaBL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene trampas por su Organizacion
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public List<TrampaInfo> ObtenerPorOrganizacion(int organizacionID)
        {
            try
            {
                Logger.Info();
                var trampaBL = new TrampaBL();
                List<TrampaInfo> result = trampaBL.ObtenerPorOrganizacion(organizacionID);
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
        /// Metrodo Para obtener la trampa
        /// </summary>
        public TrampaInfo ObtenerTrampa(TrampaInfo trampaInfo)
        {
            TrampaInfo result;
            try
            {
                Logger.Info();
                var trampaBL = new TrampaBL();
                result = trampaBL.ObtenerTrampa(trampaInfo);

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
            return result;

        }

    }
}

