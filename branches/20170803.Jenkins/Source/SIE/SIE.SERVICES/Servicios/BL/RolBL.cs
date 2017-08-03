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
    internal class RolBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Rol
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(RolInfo info)
        {
            try
            {
                Logger.Info();
                var rolDAL = new RolDAL();
                int result = info.RolID;
                if (info.RolID == 0)
                {
                    result = rolDAL.Crear(info);
                }
                else
                {
                    rolDAL.Actualizar(info);
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
        internal ResultadoInfo<RolInfo> ObtenerPorPagina(PaginacionInfo pagina, RolInfo filtro)
        {
            try
            {
                Logger.Info();
                var rolDAL = new RolDAL();
                ResultadoInfo<RolInfo> result = rolDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de Rol
        /// </summary>
        /// <returns></returns>
        internal IList<RolInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var rolDAL = new RolDAL();
                IList<RolInfo> result = rolDAL.ObtenerTodos();
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
        /// Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal IList<RolInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var rolDAL = new RolDAL();
                IList<RolInfo> result = rolDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad Rol por su Id
        /// </summary>
        /// <param name="rolID">Obtiene una entidad Rol por su Id</param>
        /// <returns></returns>
        internal RolInfo ObtenerPorID(int rolID)
        {
            try
            {
                Logger.Info();
                var rolDAL = new RolDAL();
                RolInfo result = rolDAL.ObtenerPorID(rolID);
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
        /// Obtiene una entidad Rol por su descripcion
        /// </summary>
        /// <param name="rolID">Obtiene una entidad Rol por su Id</param>
        /// <returns></returns>
        internal RolInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var rolDAL = new RolDAL();
                RolInfo result = rolDAL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene una lista de los niveles de alerta
        /// </summary>
        /// <returns></returns>
        public IList<NivelAlertaInfo> ObtenerNivelAlerta()
        {
            try
            {
                Logger.Info();
                var nivelAlertaDal = new RolDAL();
                IList<NivelAlertaInfo> result = nivelAlertaDal.ObtenerNivelAlerta();
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

