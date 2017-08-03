using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;


namespace SIE.Services.Servicios.PL
{
    public class TarifarioPL
    {
        /// <summary>
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<TarifarioInfo> ObtenerPorPagina(PaginacionInfo pagina, TarifarioInfo filtro)
        {
            ResultadoInfo<TarifarioInfo> result;
            try
            {
                Logger.Info();
                var tarifarioBL = new TarifarioBL();
                result = tarifarioBL.ObtenerPorPagina(pagina, filtro);
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
        /// Indica si existen Configuracion de Embarque activas
        /// </summary>
        /// <returns>Regresa un listado de Configuracion de Embarque activas</returns>
        public IList<ConfiguracionEmbarqueInfo> ConfiguracionEmbarqueActivas(TarifarioInfo filtro, EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tarifarioBL = new TarifarioBL();
                return tarifarioBL.ObtenerConfiguracionEmbarqueActivas(filtro, estatus);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }        
        }
    }
}
