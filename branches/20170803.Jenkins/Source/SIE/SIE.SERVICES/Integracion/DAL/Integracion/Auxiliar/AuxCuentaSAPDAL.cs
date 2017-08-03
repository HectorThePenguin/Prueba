using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using System.Xml.Linq;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxCuentaSAPDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, CuentaSAPInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@CuentaSAP", filtro.CuentaSAP},
                            {"@CuentaSAPID", filtro.CuentaSAPID},
                            {"@Descripcion", filtro.Descripcion},
                            {"@Activo", filtro.Activo.GetHashCode()},
                            {"@Inicio", pagina.Inicio},
                            {"@Limite", pagina.Limite}
                        };
                var element = new XElement("ROOT",
                                               from tipoCuenta in filtro.ListaTiposCuenta
                                               select new XElement("Datos",
                                                                   new XElement("tipoCuenta",
                                                                                tipoCuenta.TipoCuentaID)));
                parametros.Add("@xmlTipoCuenta", element.ToString());
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPaginaSinId(PaginacionInfo pagina, CuentaSAPInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@CuentaSAP", filtro.CuentaSAP},
                            {"@CuentaSAPID", 0},
                            {"@Descripcion", filtro.Descripcion},
                            {"@Activo", filtro.Activo.GetHashCode()},
                            {"@Inicio", pagina.Inicio},
                            {"@Limite", pagina.Limite}
                        };
                var element = new XElement("ROOT",
                                               from tipoCuenta in filtro.ListaTiposCuenta
                                               select new XElement("Datos",
                                                                   new XElement("tipoCuenta",
                                                                                tipoCuenta.TipoCuentaID)));
                parametros.Add("@xmlTipoCuenta", element.ToString());
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
        /// <param name="info">Valores de la entidad</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrear(CuentaSAPInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@CuentaSAP", info.CuentaSAP},
                            {"@Descripcion", info.Descripcion},
                            {"@TipoCuentaID", info.TipoCuenta.TipoCuentaID},
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
        ///     Obtiene parametros para actualizar
        /// </summary>
        /// <param name="info">Valores de la entidad</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizar(CuentaSAPInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                             {"@CuentaSAPID", info.CuentaSAPID},
                             {"@CuentaSAP", info.CuentaSAP},
                            {"@Descripcion", info.Descripcion},
                            {"@TipoCuentaID", info.TipoCuenta.TipoCuentaID},
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
        ///     Obtiene Parametros por Id
        /// </summary>
        /// <param name="cuentaSAPID">Identificador de la CuentaSAP</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorID(int cuentaSAPID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@CuentaSAPID", cuentaSAPID}
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
        ///     Obtiene Parametros por Id
        /// </summary>
        /// <param name="cuentaSAP">Identificador de la CuentaSAP</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorCuentaSAP(string cuentaSAP)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@CuentaSAPID", cuentaSAP},
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
        ///     Obtiene Parametros por filtro
        /// </summary>
        /// <param name="filtro"> </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorFiltro(CuentaSAPInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@CuentaSAPID", filtro.CuentaSAPID},
                            {"@CuentaSAP", filtro.CuentaSAP},
                            {"@Descripcion", filtro.Descripcion},                            
                            {"@Activo", filtro.Activo},
                        };
                var element = new XElement("ROOT",
                                                from tipoCuenta in filtro.ListaTiposCuenta
                                                select new XElement("Datos",
                                                                    new XElement("tipoCuenta",
                                                                                 tipoCuenta.TipoCuentaID)));
                parametros.Add("@xmlTipoCuenta", element.ToString());
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Obtiene los parametros para consultar las cuentas sap
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPaginaCuentasSap(PaginacionInfo pagina, CuentaSAPInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@CuentaSAPID", filtro.CuentaSAPID},
                            {"@CuentaSAP", filtro.CuentaSAP ?? string.Empty},
                            {"@Descripcion", filtro.Descripcion ?? string.Empty},
                            {"@TipoCuentaID", filtro.TipoCuenta != null ? filtro.TipoCuenta.TipoCuentaID : 0},
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
        /// Obtiene los parametros para obtner por filtro sin tipo de cuenta
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorFiltroSinTipo(CuentaSAPInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@CuentaSAPID", filtro.CuentaSAPID},
                            {"@CuentaSAP", filtro.CuentaSAP},
                            {"@Descripcion", filtro.Descripcion},                            
                            {"@Activo",filtro.Activo},
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


