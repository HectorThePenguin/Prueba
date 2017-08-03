using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxCamionDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, CamionInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@PlacaCamion", filtro.PlacaCamion ?? string.Empty},
                            {"@ProveedorID", filtro.Proveedor.ProveedorID},
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
        ///     Obtiene parametros para crear
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrear(CamionInfo info)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@ProveedorID", info.Proveedor.ProveedorID},
                                     {"@PlacaCamion", info.PlacaCamion},
                                     {"@Activo", info.Activo},
                                     {"@UsuarioCreacionID", info.UsuarioCreacionID},
                                     {"@NumEconomico", info.Economico},
                                     {"@MarcaID", info.MarcaID},
                                     {"@Modelo", info.Modelo},
                                     {"@Color", info.Color},
                                     {"@Boletinado", info.Boletinado},
                                     {"@Observaciones", info.ObservacionesEnviar},
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
        /// <param name="info"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizar(CamionInfo info)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@CamionID", info.CamionID},
                                     {"@ProveedorID", info.Proveedor.ProveedorID},
                                     {"@PlacaCamion", info.PlacaCamion},
                                     {"@Activo", info.Activo},
                                     {"@UsuarioModificacionID", info.UsuarioModificacionID},
                                     {"@NumEconomico", info.Economico},
                                     {"@MarcaID", info.MarcaID},
                                     {"@Modelo", info.Modelo},
                                     {"@Color", info.Color},
                                     {"@Boletinado", info.Boletinado},
                                     {"@Observaciones", info.ObservacionesEnviar},
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
        /// <param name="camionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorID(int camionID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@CamionID", camionID}
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
        ///      Obtiene la configuración de embarque
        /// </summary>
        /// <param name="proveedorId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorProveedorId(int proveedorId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros =
                    new Dictionary<string, object>
                        {
                            {"@ProveedorID", proveedorId}
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
        ///   Obtiene una lista de costo filtrando por el estatus Activo = 1, Inactivo = 0
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
        /// Obtiene los parametros para ejecutar
        /// el procedimiento
        /// </summary>
        /// <param name="camion"></param>
        /// <param name="dependencias"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorInfoDependencias(CamionInfo camion, IList<IDictionary<IList<string>, object>> dependencias)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@PlacaCamion", camion.PlacaCamion},
                                 };
                AuxDAL.ObtenerDependencias(parametros, dependencias);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;            
        }

        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, CamionInfo filtro, IList<IDictionary<IList<string>, object>> dependencias)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@PlacaCamion", filtro.PlacaCamion},
                                     {"@Inicio", pagina.Inicio},
                                     {"@Limite", pagina.Limite}
                                 };
                AuxDAL.ObtenerDependencias(parametros, dependencias);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary> 
        /// Obtiene Parametro pora filtrar por descripción 
        /// </summary> 
        /// <param name="descripcion">Descripción de la entidad </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@Descripcion", descripcion}
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
        /// Obtiene Parametros por Id
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorId(CamionInfo camionInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@CamionID", camionInfo.CamionID}
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
        /// Obtiene Parametros obtener por proveedorid y camionid
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroObtenerPorProveedorIdCamionId(CamionInfo camionInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@ProveedorID", camionInfo.Proveedor.ProveedorID},
                                     {"@CamionID", camionInfo.CamionID}
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
        /// Obtiene los parametros para obtener un camion
        /// por placa
        /// </summary>
        /// <param name="placaCamion"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorPlaca(string placaCamion)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@PlacaCamion", placaCamion},
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
