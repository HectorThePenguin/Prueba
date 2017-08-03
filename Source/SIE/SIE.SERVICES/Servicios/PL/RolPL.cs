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
    public class RolPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Rol
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(RolInfo info)
        {
            try
            {
                Logger.Info();
                var rolBL = new RolBL();
                int result = rolBL.Guardar(info);
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
        public ResultadoInfo<RolInfo> ObtenerPorPagina(PaginacionInfo pagina, RolInfo filtro)
        {
            try
            {
                Logger.Info();
                var rolBL = new RolBL();
                ResultadoInfo<RolInfo> result = rolBL.ObtenerPorPagina(pagina, filtro);
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

        /*
        /// <summary>
        /// Obtiene una lista
        /// </summary>
        /// <returns></returns>
        public IList<RolInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var rolBL = new RolBL();
                IList<RolInfo> result = rolBL.ObtenerTodos();
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
        */

        /// <summary>
        ///  Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<RolInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var rolBL = new RolBL();
                IList<RolInfo> result = rolBL.ObtenerTodos(estatus);
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
        /// <param name="rolID"></param>
        /// <returns></returns>
        public RolInfo ObtenerPorID(int rolID)
        {
            try
            {
                Logger.Info();
                var rolBL = new RolBL();
                RolInfo result = rolBL.ObtenerPorID(rolID);
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
        public RolInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var rolBL = new RolBL();
                RolInfo result = rolBL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene una lista de niveles de alerta
        /// </summary>
        /// <returns></returns>
        public IList<NivelAlertaInfo> NivelAlertaInfo()
        {
            try
            {
                Logger.Info();
                var nivelAlertaBl = new RolBL();
                IList<NivelAlertaInfo> result = nivelAlertaBl.ObtenerNivelAlerta();
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

