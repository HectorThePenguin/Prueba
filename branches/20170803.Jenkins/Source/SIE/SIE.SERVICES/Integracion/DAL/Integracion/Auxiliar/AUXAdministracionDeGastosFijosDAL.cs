using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AUXAdministracionDeGastosFijosDAL
    {  
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, AdministracionDeGastosFijosInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Descripcion", filtro.Descripcion},
                            {"@Activo", filtro.Activo},
                            {"@Inicio", pagina.Inicio},
                            {"@Limite", pagina.Limite}
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
        /// Obtiene parametros para obtener por ID
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorID(TarifarioInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@embarqueTarifaID", filtro.EmbarqueID}
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
        /// Obtiene los parametros para la creacion de un gasto fijo
        /// </summary>
        /// <param name="gastos"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrear(AdministracionDeGastosFijosInfo gastos)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@Descripcion", gastos.Descripcion},
                                     {"@Activo", gastos.Activo},
                                     {"@Importe", gastos.Importe},
                                     {"@UsuarioCreacionID", gastos.UsuarioCreacionID}
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return parametros;
        }

        /// <summary>
        /// Obtiene los parametros para la actualización de un gasto fijo
        /// </summary>
        /// <param name="gastos"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizar(AdministracionDeGastosFijosInfo gastos)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@GastoFijoID", gastos.GastoFijoID},
                                     {"@Descripcion", gastos.Descripcion},
                                     {"@Activo", gastos.Activo},
                                     {"@Importe", gastos.Importe},
                                     {"@UsuarioModificacionID", gastos.UsuarioCreacionID}
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return parametros;
        }

        /// <summary>
        /// Obtiene los parametros para la validacion de un gasto fijo existente
        /// </summary>
        /// <param name="gastos"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosValidarDescripcion(AdministracionDeGastosFijosInfo gastos)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@Descripcion", gastos.Descripcion}
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return parametros;
        }
    }
}
