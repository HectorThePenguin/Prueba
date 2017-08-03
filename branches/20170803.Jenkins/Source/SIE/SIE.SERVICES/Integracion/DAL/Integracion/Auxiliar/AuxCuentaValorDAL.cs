using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Services.Info.Enums;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    public class AuxCuentaValorDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, CuentaValorInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@CuentaValorID", filtro.CuentaValorID},
                            {"@CuentaID", filtro.Cuenta.CuentaID},
                            {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
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
        /// Obtiene parametros para crear
        /// </summary>
        /// <param name="info">Valores de la entidad</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosCrear(CuentaValorInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@CuentaID", info.Cuenta.CuentaID},
							{"@OrganizacionID", info.Organizacion.OrganizacionID},
							{"@Valor", info.Valor},
							{"@Activo", info.Activo},
                            //{"@UuarioModificacionID", info.Usuario.UuarioModificacionID},
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
        public static Dictionary<string, object> ObtenerParametrosActualizar(CuentaValorInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@CuentaValorID", info.CuentaValorID},
							{"@CuentaID", info.Cuenta.CuentaID},
							{"@OrganizacionID", info.Organizacion.OrganizacionID},
							{"@Valor", info.Valor},
							{"@Activo", info.Activo},
                            //{"@UuarioModificacionID", info.Usuario.UuarioModificacionID},
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
        /// <param name="cuentaValorID">Identificador de la entidad CuentaValor</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorID(int cuentaValorID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@CuentaValorID", cuentaValorID}
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
        /// Obtiene Parametro pora filtrar por descripción 
        /// </summary> 
        /// <param name="descripcion">Descripción de la entidad </param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@CuentaValorID", descripcion}
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
        /// Obtiene Parametro pora filtrar por descripción 
        /// </summary> 
        /// <param name="cuentaValor">Descripción de la entidad </param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorFiltros(CuentaValorInfo cuentaValor)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@OrganizacionID", cuentaValor.Organizacion.OrganizacionID},
                                {"@CuentaID", cuentaValor.Cuenta.CuentaID},
                                {"@Activo", cuentaValor.Activo.GetHashCode()}
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

