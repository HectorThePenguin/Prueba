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
    internal class AuxFleteMermaPermitidaDAL
    {
        /// <summary>
        /// Obtiene un registro
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerConfiguracion(FleteMermaPermitidaInfo fleteMermaPermitidaInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@OrganizacionIDDestino", fleteMermaPermitidaInfo.Organizacion.OrganizacionID},
                                {"@SubFamilia", fleteMermaPermitidaInfo.SubFamilia.SubFamiliaID},
                                {"@Activo", fleteMermaPermitidaInfo.Activo.GetHashCode()},
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosCrear(FleteMermaPermitidaInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@OrganizacionID", info.Organizacion.OrganizacionID},
                                {"@SubFamiliaID", info.SubFamilia.SubFamiliaID},
                                {"@Activo", info.Activo},
                                {"@MermaPermitida", info.MermaPermitida},
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

        internal static Dictionary<string, object> ObtenerParametrosActualizar(FleteMermaPermitidaInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@FleteMermaPermitidaID", info.FleteMermaPermitidaID},
                                {"@OrganizacionID", info.Organizacion.OrganizacionID},
                                {"@SubFamiliaID", info.SubFamilia.SubFamiliaID},
                                {"@Activo", info.Activo},
                                {"@MermaPermitida", info.MermaPermitida},
                                {"@UsuarioModificacionID", info.UsuarioModificacionID},
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, FleteMermaPermitidaInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                                {"@Activo", filtro.Activo},
                                {"@Inicio", pagina.Inicio},
                                {"@Limite", pagina.Limite},
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerTodos(EstatusEnum estatus)
        {
            throw new NotImplementedException();
        }

        internal static Dictionary<string, object> ObtenerParametrosPorID(int fleteMermaPermitidaID)
        {
            throw new NotImplementedException();
        }

        internal static Dictionary<string, object> ObtenerParametrosPorDescripcion(FleteMermaPermitidaInfo fleteMermaPermitida)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@OrganizacionID", fleteMermaPermitida.Organizacion.OrganizacionID},
                                {"@SubFamiliaID", fleteMermaPermitida.SubFamilia.SubFamiliaID},
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
