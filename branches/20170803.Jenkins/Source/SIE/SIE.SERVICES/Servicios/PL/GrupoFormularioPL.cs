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
    public class GrupoFormularioPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad GrupoFormulario
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(GrupoFormularioInfo info)
        {
            try
            {
                Logger.Info();
                var grupoFormularioBL = new GrupoFormularioBL();
                int result = grupoFormularioBL.Guardar(info);
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
        public ResultadoInfo<GrupoFormularioInfo> ObtenerPorPagina(PaginacionInfo pagina, GrupoFormularioInfo filtro)
        {
            try
            {
                Logger.Info();
                var grupoFormularioBL = new GrupoFormularioBL();
                ResultadoInfo<GrupoFormularioInfo> result = grupoFormularioBL.ObtenerPorPagina(pagina, filtro);
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
        public IList<GrupoFormularioInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var grupoFormularioBL = new GrupoFormularioBL();
                IList<GrupoFormularioInfo> result = grupoFormularioBL.ObtenerTodos();
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
        public IList<GrupoFormularioInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var grupoFormularioBL = new GrupoFormularioBL();
                IList<GrupoFormularioInfo> result = grupoFormularioBL.ObtenerTodos(estatus);
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
        /// <param name="grupoFormularioID"></param>
        /// <returns></returns>
        public GrupoFormularioInfo ObtenerPorID(int grupoFormularioID)
        {
            try
            {
                Logger.Info();
                var grupoFormularioBL = new GrupoFormularioBL();
                GrupoFormularioInfo result = grupoFormularioBL.ObtenerPorID(grupoFormularioID);
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
        public GrupoFormularioInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var grupoFormularioBL = new GrupoFormularioBL();
                GrupoFormularioInfo result = grupoFormularioBL.ObtenerPorDescripcion(descripcion);
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
        public GrupoFormularioInfo ObtenerPorGrupoID(int grupoID)
        {
            try
            {
                Logger.Info();
                var grupoFormularioBL = new GrupoFormularioBL();
                GrupoFormularioInfo result = grupoFormularioBL.ObtenerPorGrupoID(grupoID);
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

