using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxCorteTransferenciaGanado
    {
        /// <summary>
        /// Obtiene parametros para trampa
        /// </summary>
        /// <param name="trampaInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerTrampa(TrampaInfo trampaInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", trampaInfo.Organizacion.OrganizacionID},
                            {"@TipoTrampa", trampaInfo.TipoTrampa},
                            {"@HostName", trampaInfo.HostName}
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
        /// Obtener parametros entrada de ganado
        /// </summary>
        /// <param name="animalInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosEntradaGanado(AnimalInfo animalInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@FolioEntrada", animalInfo.FolioEntrada},
                            {"@OrganizacionID", animalInfo.OrganizacionIDEntrada}
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
        /// Obtener parametros de movimientos animal
        /// </summary>
        /// <param name="animalInfo"></param>
        /// <param name="tipoMovimiento"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosMovimientosAnimal(AnimalInfo animalInfo, int tipoMovimiento)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Arete", animalInfo.Arete},
                            {"@OrganizacionID", animalInfo.OrganizacionIDEntrada},
                            {"@TipoMovimientoID", tipoMovimiento},
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
        /// Obtener parametros para consultar totales de corte por transferencia.
        /// </summary>
        /// <param name="trampaInfo"></param>
        /// <param name="animal"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosTotales(AnimalMovimientoInfo animal)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@LoteID", animal.LoteID},
                            {"@TipoMovimiento",(int)TipoMovimiento.CortePorTransferencia},
                            {"@Activo",(int)EstatusEnum.Activo}

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
        /// Obtener parametros para consultar corrales por tipo.
        /// </summary>
        /// <param name="tipoCorral"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCorralesTipo(TipoCorralInfo tipoCorral, int organizacionID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionID},
                            {"@TipoCorralID", tipoCorral.TipoCorralID}
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
