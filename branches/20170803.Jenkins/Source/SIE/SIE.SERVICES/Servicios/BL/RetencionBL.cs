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
    internal class RetencionBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Retencion
        /// </summary>
        /// <param name="info"></param>
        internal void Guardar(RetencionInfo info)
        {
            try
            {
                Logger.Info();
                var tetencionDAL = new RetencionDAL();
                if (info.RetencionID == 0)
                {
                    tetencionDAL.Crear(info);
                }
                else
                {
                    tetencionDAL.Actualizar(info);
                }
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<RetencionInfo> ObtenerPorPagina(PaginacionInfo pagina, RetencionInfo filtro)
        {
            try
            {
                Logger.Info();
                var retencionDAL = new RetencionDAL();
                ResultadoInfo<RetencionInfo> result = retencionDAL.ObtenerPorPagina(pagina, filtro);
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
        ///     Obtiene un lista de Retencions
        /// </summary>
        /// <returns></returns>
        internal IList<RetencionInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var retencionDAL = new RetencionDAL();
                IList<RetencionInfo> result = retencionDAL.ObtenerTodos();
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
        ///   Obtiene una lista de Retencion filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal IList<RetencionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var retencionDAL = new RetencionDAL();
                IList<RetencionInfo> result = retencionDAL.ObtenerTodos(estatus);

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
        ///     Obtiene una entidad Retencion por su Id
        /// </summary>
        /// <param name="retencionID">Obtiene uan entidad Retencion por su Id</param>
        /// <returns></returns>
        internal RetencionInfo ObtenerPorID(int retencionID)
        {
            try
            {
                Logger.Info();
                var retencionDAL = new RetencionDAL();
                RetencionInfo result = retencionDAL.ObtenerPorID(retencionID);
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
        ///     Obtiene un lista de Retenciones con Costo Id
        /// </summary>
        /// <returns></returns>
        internal IList<RetencionInfo> ObtenerRetencionesConCosto(IList<int> costos)
        {
            try
            {
                Logger.Info();
                var retencionDAL = new RetencionDAL();
                IList<RetencionInfo> result = retencionDAL.ObtenerRetencionesConCosto(costos);
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

