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
    internal class RotomixBL
    {
        ///<summary>
        /// Obtiene una lista de la tabla RotoMix para cargar el commobox del mismo nombre "RotoMix"
        /// </summary>
        /// <returns></returns>
        internal RotoMixInfo ObtenerRotoMixXOrganizacionYDescripcion(int organizacionId, string Descripcion)
        {
            try
            {
                Logger.Info();
                var rotomixDAL = new RotomixDAL();
                RotoMixInfo result = rotomixDAL.ObtenerRotoMixXOrganizacionYDescripcion(organizacionId,Descripcion);
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
        /// Metodo para Guardar/Modificar una entidad Rotomix
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(RotoMixInfo info)
        {
            try
            {
                Logger.Info();
                var rotomixDAL = new RotomixDAL();
                int result = info.RotoMixId;
                if (info.RotoMixId == 0)
                {
                    result = rotomixDAL.Crear(info);
                }
                else
                {
                    rotomixDAL.Actualizar(info);
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
        internal ResultadoInfo<RotoMixInfo> ObtenerPorPagina(PaginacionInfo pagina, RotoMixInfo filtro)
        {
            try
            {
                Logger.Info();
                var rotomixDAL = new RotomixDAL();
                ResultadoInfo<RotoMixInfo> result = rotomixDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de Rotomix
        /// </summary>
        /// <returns></returns>
        internal IList<RotoMixInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var rotomixDAL = new RotomixDAL();
                IList<RotoMixInfo> result = rotomixDAL.ObtenerTodos();
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
        internal IList<RotoMixInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var rotomixDAL = new RotomixDAL();
                IList<RotoMixInfo> result = rotomixDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad Rotomix por su Id
        /// </summary>
        /// <param name="rotomixID">Obtiene una entidad Rotomix por su Id</param>
        /// <returns></returns>
        internal RotoMixInfo ObtenerPorID(int rotomixID)
        {
            try
            {
                Logger.Info();
                var rotomixDAL = new RotomixDAL();
                RotoMixInfo result = rotomixDAL.ObtenerPorID(rotomixID);
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
        /// Obtiene una entidad Rotomix por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal RotoMixInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var rotomixDAL = new RotomixDAL();
                RotoMixInfo result = rotomixDAL.ObtenerPorDescripcion(descripcion);
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
