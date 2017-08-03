using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Base.Interfaces;
using System.Collections.Generic;

namespace SIE.Services.Servicios.PL
{
    public class GrupoPL : IPaginador<GrupoInfo>, IAyuda<GrupoInfo>
    {
        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<GrupoInfo> ObtenerPorPagina(PaginacionInfo pagina, GrupoInfo filtro)
        {
            ResultadoInfo<GrupoInfo> result;
            try
            {
                Logger.Info();
                var grupoBL = new GrupoBL();
                result = grupoBL.ObtenerPorPagina(pagina, filtro);
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
 
        /// <summary>
        ///     Metodo que crear un grupo
        /// </summary>
        /// <param name="grupoInfo"></param>
        public void Guardar(GrupoInfo grupoInfo)
        {
            try
            {
                Logger.Info();
                var grupoBL = new GrupoBL();
                grupoBL.Guardar(grupoInfo);
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
        ///     Obtiene un grupo por Id
        /// </summary>
        /// <param name="grupoID"></param>
        /// <returns></returns>
        public GrupoInfo ObtenerPorID(int grupoID)
        {
            GrupoInfo grupoInfo;
            try
            {
                Logger.Info();
                var grupoBL = new GrupoBL();
                grupoInfo = grupoBL.ObtenerPorID(grupoID);
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
            return grupoInfo;
        }

        /// <summary>
        ///     Metodo que inactiva un grupo
        /// </summary>
        /// <param name="grupoID"></param>
        public void BorrarGrupo(int grupoID)
        {
            try
            {
                Logger.Info();
                var grupoBL = new GrupoBL();
                grupoBL.BorrarGrupo(grupoID);
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

        ResultadoInfo<GrupoInfo> IAyuda<GrupoInfo>.ObtenerPorId(int id)
        {
            var resultado = new ResultadoInfo<GrupoInfo>();
            var grupos = new List<GrupoInfo>();
            var grupo = ObtenerPorID(id); 
            grupos.Add(grupo);
            resultado.Lista = grupos;

            return resultado;
        }

        ResultadoInfo<GrupoInfo> IAyuda<GrupoInfo>.ObtenerPorDescripcion(PaginacionInfo pagina, string descripcion)
        {
            var grupo = new GrupoInfo { Descripcion = descripcion, Activo = EstatusEnum.Activo };
            ResultadoInfo<GrupoInfo> resultado = ObtenerPorPagina(pagina, grupo);
            return resultado;
        }

        /// <summary>
        /// Obtiene una entidad por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public GrupoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var grupoBL = new GrupoBL();
                GrupoInfo result = grupoBL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene un lista de los grupos.
        /// </summary>
        /// <returns> </returns>
        public IList<GrupoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var grupoBL = new GrupoBL();
                IList<GrupoInfo> lista = grupoBL.ObtenerTodos();
                return lista;
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
        /// Obtiene un lista de los grupos.
        /// </summary>
        /// <returns> </returns>
        public IList<GrupoInfo> ObtenerTodos(EstatusEnum activo)
        {
            try
            {
                Logger.Info();
                var grupoBL = new GrupoBL();
                IList<GrupoInfo> lista = grupoBL.ObtenerTodos(activo);
                return lista;
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
