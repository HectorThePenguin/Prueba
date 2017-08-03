using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal static class AuxCostoOrganizacionDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, CostoOrganizacionInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@CostoOrganizacionID", filtro.CostoOrganizacionID},
                        {"@TipoOrganizacionID", filtro.TipoOrganizacion.TipoOrganizacionID},
                        {"@CostoID", filtro.Costo.CostoID},
                        {"@Activo", filtro.Activo},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite}
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
        ///     Obtiene Parametros por Id
        /// </summary>
        /// <param name="costoOrganizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorID(int costoOrganizacionID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@CostoOrganizacionID", costoOrganizacionID}
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
        ///     Obtiene parametros para actualizar
        /// </summary>
        /// <param name="costoOrganizacionInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizar(CostoOrganizacionInfo costoOrganizacionInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@CostoOrganizacionID", costoOrganizacionInfo.CostoOrganizacionID},
                        {"@Activo", costoOrganizacionInfo.Activo},
                        {"@Automatico", costoOrganizacionInfo.Automatico},
                        {"@UsuarioModificacionID", costoOrganizacionInfo.UsuarioModificacionID},
                        {"@TipoOrganizacionID", costoOrganizacionInfo.TipoOrganizacion.TipoOrganizacionID},
                        {"@CostoID", costoOrganizacionInfo.Costo.CostoID},
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
        ///     Obtiene Parametros pora filtrar por estatus
        /// </summary>
        /// <param name="estatus">Representa si esta activo el registro </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Activo", estatus.GetHashCode()}
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
        /// Obtiene los parametros para Crear un nuevo Costo Organizacion
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardado(CostoOrganizacionInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@TipoOrganizacionID", info.TipoOrganizacion.TipoOrganizacionID},
							{"@CostoID", info.Costo.CostoID},
							{"@Automatico", info.Automatico},
							{"@Activo", info.Activo},
                            {"@UsuarioCreacionID", info.UsuarioCreacionID},
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
        ///     Obtiene Parametros por Organizacion
        /// </summary>
        /// <param name="entradaGanadoInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorOrganizacion(EntradaGanadoInfo entradaGanadoInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@TipoOrganizacionOrigenID", entradaGanadoInfo.TipoOrigen},
                        {"@OrganizacionDestinoID", entradaGanadoInfo.OrganizacionID},
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
        /// Obtiene Parametros para ejecutar
        /// procedimiento para obtener un costo
        /// organizacion por, clave de costo y tipo
        /// de organizacion
        /// </summary>
        /// <param name="costoOrganizacion"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorTipoOrganizacionCosto(CostoOrganizacionInfo costoOrganizacion)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@TipoOrganizacionID", costoOrganizacion.TipoOrganizacion.TipoOrganizacionID},
                        {"@CostoID", costoOrganizacion.Costo.CostoID},
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