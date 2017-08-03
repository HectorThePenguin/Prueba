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
    internal class AuxJaulaDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, JaulaInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                int proveedorID = filtro.Proveedor == null ? 0 : filtro.Proveedor.ProveedorID;
                parametros = new Dictionary<string, object>
                    {
                        {"@PlacaJaula", filtro.PlacaJaula},
                        {"@ProveedorID", proveedorID},
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
        ///     Obtiene parametros para crear
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrear(JaulaInfo info)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@ProveedorID", info.Proveedor.ProveedorID},
                                     {"@PlacaJaula", info.PlacaJaula},
                                     {"@Capacidad", info.Capacidad},
                                     {"@Secciones", info.Secciones},
                                     {"@NumEconomico", info.NumEconomico},
                                     {"@MarcaID", info.Marca.MarcaId},
                                     {"@Modelo", info.Modelo},
                                     {"@Boletinado", info.Boletinado},
                                     {"@Observaciones", info.Observaciones},
                                     {"@Activo", info.Activo},
                                     {"@UsuarioCreacionID", info.UsuarioCreacionID},
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
        internal static Dictionary<string, object> ObtenerParametrosActualizar(JaulaInfo info)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@JaulaID", info.JaulaID},
                                     {"@ProveedorID", info.Proveedor.ProveedorID},
                                     {"@PlacaJaula", info.PlacaJaula},
                                     {"@Capacidad", info.Capacidad},
                                     {"@Secciones", info.Secciones},
                                     {"@NumEconomico", info.NumEconomico},
                                     {"@MarcaID", info.Marca.MarcaId},
                                     {"@Modelo", info.Modelo},
                                     {"@Boletinado", info.Boletinado},
                                     {"@Observaciones", info.Observaciones},
                                     {"@Activo", info.Activo},
                                     {"@UsuarioModificacionID", info.UsuarioModificacionID}
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
        /// <param name="jaulaID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorID(int jaulaID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@JaulaID", jaulaID}
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
        ///     Obtiene Parametros por ProveedorId
        /// </summary>
        /// <param name="proveedorId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorProveedorId(int proveedorId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
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
        /// Obtiene un diccionario con los
        /// parametros para ejecutar
        /// el procedimiento almacenado
        /// </summary>
        /// <param name="jaula"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorJaula(JaulaInfo jaula)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();                
                int proveedorID = jaula.Proveedor == null ? 0 : jaula.Proveedor.ProveedorID;
                
                parametros = new Dictionary<string, object>
                                 {
                                     {"@PlacaJaula", jaula.PlacaJaula}
                                     ,
                                     {"@ProveedorID", proveedorID}
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
        /// Obtiene un diccionario con los
        /// parametros para ejecutar
        /// el procedimiento almacenado
        /// </summary>
        /// <param name="jaula"></param>
        /// <param name="Dependencias"> </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorJaula(JaulaInfo jaula, IList<IDictionary<IList<string>, object>> Dependencias)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@PlacaJaula", jaula.PlacaJaula}
                                 };
                AuxDAL.ObtenerDependencias(parametros, Dependencias);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <param name="Dependencias"> </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, JaulaInfo filtro, IList<IDictionary<IList<string>, object>> Dependencias)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@PlacaJaula", filtro.PlacaJaula},
                        {"@Activo", filtro.Activo},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite}
                    };
                AuxDAL.ObtenerDependencias(parametros, Dependencias);
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

