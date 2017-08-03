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
    public class RetencionPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Retencion
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public void Guardar(RetencionInfo info)
        {
            try
            {
                Logger.Info();
                var tRetencionBL = new RetencionBL();
                tRetencionBL.Guardar(info);
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
        public ResultadoInfo<RetencionInfo> ObtenerPorPagina(PaginacionInfo pagina, RetencionInfo filtro)
        {
            try
            {
                Logger.Info();
                var retencionBL = new RetencionBL();
                ResultadoInfo<RetencionInfo> result = retencionBL.ObtenerPorPagina(pagina, filtro);

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
        ///     Obtiene una lista de Retencions
        /// </summary>
        /// <returns></returns>
        public IList<RetencionInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var retencionBL = new RetencionBL();
                IList<RetencionInfo> result = retencionBL.ObtenerTodos();

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
        ///     Obtiene una lista de Retencion filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<RetencionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var retencionBL = new RetencionBL();
                IList<RetencionInfo> result = retencionBL.ObtenerTodos(estatus);

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
        /// <param name="retencionID"></param>
        /// <returns></returns>
        public RetencionInfo ObtenerPorID(int retencionID)
        {
            try
            {
                Logger.Info();
                var retencionBL = new RetencionBL();
                RetencionInfo result = retencionBL.ObtenerPorID(retencionID);

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

