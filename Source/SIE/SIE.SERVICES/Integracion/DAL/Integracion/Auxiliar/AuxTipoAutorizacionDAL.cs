using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxTipoAutorizacionDAL
    {
        /// <summary>
        /// Obtiene los parametros necesarios para obtener los movimientos por contrato
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                    {
                        {"@Activo", EstatusEnum.Activo.GetHashCode()},
                    };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Obtiene los parametros necesarios para obtener los movimientos por contrato
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ValidarPreCondiciones()
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                    {
                        {"@TipoGanaderaID", TipoOrganizacion.Ganadera.GetHashCode()},
                        {"@PendienteID", Estatus.AMPPendien.GetHashCode()},
                        {"@Activo", EstatusEnum.Activo.GetHashCode()}
                    };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
