using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class TarifarioBL
    {
        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<TarifarioInfo> ObtenerPorPagina(PaginacionInfo pagina, TarifarioInfo filtro)
        {
            ResultadoInfo<TarifarioInfo> result;
            try
            {
                Logger.Info();
                var tarifarioDAL = new TarifarioDAL();
                result = tarifarioDAL.ObtenerPorPagina(pagina, filtro);
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
        ///     Obtiene una Configuracion de Embarque filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="filtro"> </param>
        /// <param name="estatus"> </param>
        /// <returns></returns>
        internal IList<ConfiguracionEmbarqueInfo> ObtenerConfiguracionEmbarqueActivas(TarifarioInfo filtro, EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tarifarioDAL = new TarifarioDAL();
                IList<ConfiguracionEmbarqueInfo> lista = tarifarioDAL.ObtenerConfiguracionEmbarqueActivas(filtro, estatus);

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
