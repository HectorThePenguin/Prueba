using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
   internal class AuxNivelAlertaDAL
    {
        /// <summary>
        /// Obtiene parametros para crear
        /// </summary>
        /// <param name="info">Valores de la entidad</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrear(NivelAlertaInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                        {
                            {"@Descripcion", info.Descripcion.Trim()},
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
        ///  Obtiene parametros para actualizar el nivel de alertas
        /// </summary>
        /// <param name="info">Valores de la entidad</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizar(NivelAlertaInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
					        {"@NivelAlertaID", info.NivelAlertaId},
					        {"@Descripcion", info.Descripcion.Trim()},
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
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, NivelAlertaInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            
                            {"@Descripcion", string.IsNullOrWhiteSpace(filtro.Descripcion) ? string.Empty : filtro.Descripcion.Trim()},
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
        /// Se obtiene la descripcion en caso de existir
        /// </summary>
        /// <param name="Descripcion"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPorDescripcion(String Descripcion)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                        {							
                            {"@Descripcion", Descripcion.Trim()},
							
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
        /// verifica si el nivel seleccionado no a sido asignado
        /// </summary>
        /// <param name="nivelAlertaId"></param>
        /// <returns></returns>
        public static Dictionary<string, object> VerificarAsignacionNivelAlerta(int nivelAlertaId)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                        {							
                            {"@ID", nivelAlertaId},
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
        /// Obtiene un contador de los niveles que se encuentran deshabilitados
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, object> NivelesAlertaDesactivados()
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                        {							
                            {"@Activo", EstatusEnum.Inactivo.GetHashCode()},
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
        /// SP que devuelve el primer campo inactivo de la tabla NivelAlerta 
        /// y verifica si es el mismo que se le envio.
        /// </summary>
        /// <param name="nivelAlertaId"></param>
        /// <returns>Si regresa 0 no es el primero deshabilitado si regresa > 0 es el primero deshabilitado</returns>
        public static Dictionary<string, object> NivelAlerta_ActivarPrimerNivelDesactivado(int nivelAlertaId)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                        {							
                            {"@Activo", EstatusEnum.Inactivo.GetHashCode()},
                            {"@ID", nivelAlertaId}
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
