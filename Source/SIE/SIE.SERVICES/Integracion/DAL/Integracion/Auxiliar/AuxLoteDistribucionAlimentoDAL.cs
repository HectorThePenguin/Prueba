using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Services.Info.Enums;
using SIE.Base.Log;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    public class AuxLoteDistribucionAlimentoDAL
    {
        /// <summary>
        /// Obtiene parametros para crear
        /// </summary>
        /// <param name="info">Valores de la entidad</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosCrear(LoteDistribucionAlimentoInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@LoteID", info.Lote.LoteID},
							{"@TipoServicioID", info.TipoServicio.TipoServicioId},
							{"@EstatusDistribucionID", info.EstatusDistribucion.EstatusId},
							{"@Fecha", info.Fecha},
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
        ///  Obtiene parametros para actualizar
        /// </summary>
        /// <param name="info">Valores de la entidad</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosActualizar(LoteDistribucionAlimentoInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@LoteDistribucionAlimentoID", info.LoteDistribucionAlimentoID},
							{"@LoteID", info.Lote.LoteID},
							{"@TipoServicioID", info.TipoServicio.TipoServicioId},
							{"@EstatusDistribucionID", info.EstatusDistribucion.EstatusId},
							{"@Fecha", info.Fecha},
							{"@Activo", info.Activo},
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

        /// <summary>
        ///  Obtiene Parametros por Id
        /// </summary>
        /// <param name="loteDistribucionAlimentoID">Identificador de la entidad LoteDistribucionAlimento</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorID(int loteDistribucionAlimentoID)
        {
            try
            {
                Logger.Info();
                var parametros = 
                    new Dictionary<string, object>
                        {
                            {"@LoteDistribucionAlimentoID", loteDistribucionAlimentoID}
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
        /// Obtiene Parametro pora filtrar por estatus 
        /// </summary> 
        /// <param name="estatus">Representa si esta activo el registro </param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var parametros = 
                        new Dictionary<string, object>
                            {
								{"@Activo", estatus}
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
        /// Obtiene Parametro para el Sp LoteDistribucionAlimento_ObtenerImpresion
        /// </summary> 
        /// <param name="filtro">tiene los filtros </param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosImpresionDistribucionAlimento(FiltroImpresionDistribucionAlimento filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                                {"@FechaDistribucion", filtro.Fecha}
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

