using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxReimplante
    {
        /// <summary>
        ///     Metodo para obtener los parametros para realizar un reimplante
        /// </summary>
        /// <param name="animalInfo"></param>
        /// <param name="banderaGuardar">Indica si se guardara el cambio</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosReasignarAreteMetalico(AnimalInfo animalInfo, int banderaGuardar)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Arete", animalInfo.Arete},
                            {"@AreteMetalico", animalInfo.AreteMetalico},
                            {"@Guardar", banderaGuardar},
                            {"@OrganizacionIDEntrada", animalInfo.OrganizacionIDEntrada},
                            {"@UsuarioModificacionID", animalInfo.UsuarioModificacionID}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosDatosCompra(long idAnimal, int idOrganizacion)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AnimalID", idAnimal},
                            {"@OrganizacionID", idOrganizacion}
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
        ///     Metodo para obtener los parametros para obtener un animal a reimplantar
        /// </summary>
        /// <param name="animal"></param>
        /// <param name="corte"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerAreteIndividual(AnimalInfo animal,TipoMovimiento corte)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Arete", animal.Arete},
                            {"@OrganizacionID", animal.OrganizacionIDEntrada},
                            {"@MovimientoCorte", (int)corte}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosObtenerAreteMetalico(AnimalInfo animal, TipoMovimiento corte)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AreteMetalico", animal.AreteMetalico},
                            {"@OrganizacionID", animal.OrganizacionIDEntrada},
                            {"@MovimientoCorte", (int)corte}
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
        ///     Metodo para obtener los parametros para validar el corral destino
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosValidarCorralDestino(
            string corralOrigen, string corralDestino, int idOrganizacion)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@CorralOrigen", corralOrigen},
                            {"@CorralDestino", corralDestino},
                            {"@OrganizacionID", idOrganizacion}
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
        ///     Metodo para obtener los parametros para ver si existe programacion de reimplante
        /// </summary>
        /// <param name="idOrganizacion"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosExisteProgramacionReimplante(int idOrganizacion)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", idOrganizacion}
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
        ///     Metodo para obtener los parametros para validar que no se ha realizado un reimplante
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosValidarReimplante(AnimalInfo animal)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", animal.OrganizacionIDEntrada},
                            {"@Arete", animal.Arete},
                            {"@MovimientoReimplante", TipoMovimiento.Reimplante}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosValidarReimplantePorAreteMetalico(AnimalInfo animal)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", animal.OrganizacionIDEntrada},
                            {"@AreteMetalico", animal.AreteMetalico},
                            {"@MovimientoReimplante", TipoMovimiento.Reimplante}
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
        /// Lista los parametro para obtener las cabezas en enfermeria para un lote
        /// </summary>
        /// <param name="ganadoEnfermeria"></param>
        /// <param name="tipoMovimiento"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosCabezasEnEnfermeria(EntradaGanadoInfo ganadoEnfermeria, int tipoMovimiento)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", ganadoEnfermeria.OrganizacionID},
                            {"@Lote", ganadoEnfermeria.LoteID},
                            {"@TipoMovimiento", tipoMovimiento}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public static Dictionary<string, object> ObtenerParametrosCabezasReimplantadas(CabezasCortadas cabezas)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", cabezas.OrganizacionID},
                            {"@Lote", cabezas.NoPartida},
                            {"@TipoMovimiento", cabezas.TipoMovimiento}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public static Dictionary<string, object> ObtenerParametrosCabezasMuertas(CabezasCortadas cabezas)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", cabezas.OrganizacionID},
                            {"@Lote", cabezas.NoPartida},
                            {"@TipoMovimiento", cabezas.TipoMovimiento}
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
