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
    internal class GrupoFormularioBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad GrupoFormulario
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(GrupoFormularioInfo info)
        {
            try
            {
                Logger.Info();
                var grupoFormularioDAL = new GrupoFormularioDAL();
                int result = 0;
                grupoFormularioDAL.Actualizar(info);
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
        internal ResultadoInfo<GrupoFormularioInfo> ObtenerPorPagina(PaginacionInfo pagina, GrupoFormularioInfo filtro)
        {
            try
            {
                Logger.Info();
                var grupoFormularioDAL = new GrupoFormularioDAL();
                ResultadoInfo<GrupoFormularioInfo> result = grupoFormularioDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de GrupoFormulario
        /// </summary>
        /// <returns></returns>
        internal IList<GrupoFormularioInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var grupoFormularioDAL = new GrupoFormularioDAL();
                IList<GrupoFormularioInfo> result = grupoFormularioDAL.ObtenerTodos();
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
        internal IList<GrupoFormularioInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var grupoFormularioDAL = new GrupoFormularioDAL();
                IList<GrupoFormularioInfo> result = grupoFormularioDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad GrupoFormulario por su Id
        /// </summary>
        /// <param name="grupoFormularioID">Obtiene una entidad GrupoFormulario por su Id</param>
        /// <returns></returns>
        internal GrupoFormularioInfo ObtenerPorID(int grupoFormularioID)
        {
            try
            {
                Logger.Info();
                var grupoFormularioDAL = new GrupoFormularioDAL();
                GrupoFormularioInfo result = grupoFormularioDAL.ObtenerPorID(grupoFormularioID);
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
        /// Obtiene una entidad GrupoFormulario por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal GrupoFormularioInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var grupoFormularioDAL = new GrupoFormularioDAL();
                GrupoFormularioInfo result = grupoFormularioDAL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene una entidad por su Grupo Id
        /// </summary>
        /// <param name="grupoID"></param>
        /// <returns></returns>
        internal GrupoFormularioInfo ObtenerPorGrupoID(int grupoID)
        {
            try
            {
                Logger.Info();
                var grupoFormularioDAL = new GrupoFormularioDAL();
                GrupoFormularioInfo result = grupoFormularioDAL.ObtenerPorGrupoID(grupoID);
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

