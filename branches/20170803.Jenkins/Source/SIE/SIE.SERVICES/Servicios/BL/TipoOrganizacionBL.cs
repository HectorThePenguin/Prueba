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
    internal class TipoOrganizacionBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad TipoOrganizacion
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(TipoOrganizacionInfo info)
        {
            try
            {
                Logger.Info();
                var tipoOrganizacionDAL = new TipoOrganizacionDAL();
                int result = info.TipoOrganizacionID;
                if (info.TipoOrganizacionID == 0)
                {
                    result = tipoOrganizacionDAL.Crear(info);
                }
                else
                {
                    tipoOrganizacionDAL.Actualizar(info);
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
        internal ResultadoInfo<TipoOrganizacionInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoOrganizacionInfo filtro)
        {
            try
            {
                Logger.Info();
                var tipoOrganizacionDAL = new TipoOrganizacionDAL();
                ResultadoInfo<TipoOrganizacionInfo> result = tipoOrganizacionDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de TipoOrganizacion
        /// </summary>
        /// <returns></returns>
        internal IList<TipoOrganizacionInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var tipoOrganizacionDAL = new TipoOrganizacionDAL();
                IList<TipoOrganizacionInfo> result = tipoOrganizacionDAL.ObtenerTodos();
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
        internal IList<TipoOrganizacionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoOrganizacionDAL = new TipoOrganizacionDAL();
                IList<TipoOrganizacionInfo> result = tipoOrganizacionDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad TipoOrganizacion por su Id
        /// </summary>
        /// <param name="tipoOrganizacionID">Obtiene una entidad TipoOrganizacion por su Id</param>
        /// <returns></returns>
        internal TipoOrganizacionInfo ObtenerPorID(int tipoOrganizacionID)
        {
            try
            {
                Logger.Info();
                var tipoOrganizacionDAL = new TipoOrganizacionDAL();
                TipoOrganizacionInfo result = tipoOrganizacionDAL.ObtenerPorID(tipoOrganizacionID);
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
        /// Obtiene una entidad TipoOrganizacion por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal TipoOrganizacionInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tipoOrganizacionDAL = new TipoOrganizacionDAL();
                TipoOrganizacionInfo result = tipoOrganizacionDAL.ObtenerPorDescripcion(descripcion);
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

