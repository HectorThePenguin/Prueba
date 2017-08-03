using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxEnfermeriaDAL
    {
        /// <summary>
        ///     Metodo para obtener los parametros para obtener los animales enfermos por corral
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="corralId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerAnimalesEnfermeriaPorCorral(int organizacionId, int corralId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            
                            {"@OrganizacionID", organizacionId},
                            {"@CorralID", corralId},
                            {"@TipoRecepcion", (int)TipoCorral.Recepcion}
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
        ///     Metodo para obtener los parametros para obtener los animales enfermos por corral
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="pagina"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCorralesConEnfermosPorPaginas(int organizacionId, PaginacionInfo pagina)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            
                            {"@OrganizacionID", organizacionId},
                            {"@Inicio", pagina.Inicio},
                            {"@Limite", pagina.Limite},
                            {"@TipoRecepcion", (int)TipoCorral.Recepcion}
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
        /// Obtener animal en deteccion
        /// </summary>
        /// <param name="animal">Animal que se buscara en deteccion</param>
        /// <returns>Diccionario con los paramatros</returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerAnimalDetectadoPorArete(AnimalInfo animal)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            
                            {"@Arete", animal.Arete},
                            {"@AreteTestigo", animal.AreteMetalico},
                            {"@OrganizacionID", animal.OrganizacionIDEntrada},
                            {"@TipoRecepcion", (int)TipoCorral.Recepcion}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosUltimoMovimientoEnfermeria(AnimalInfo animal)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            
                            {"@Arete", animal.Arete},
                            {"@TipoMovimiento", (int)TipoMovimiento.EntradaEnfermeria},
                            {"@OrganizacionIDEntrada", animal.OrganizacionIDEntrada}
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
        ///     Metodo para obtener los parametros para obtener las enfermerias por operadorID
        /// </summary>
        /// <param name="operadorId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosEnfermeriasPorOperadorID(int operadorId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            
                            {"@OperadorID", operadorId}
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
        /// obtiene el ultimo movimiento de recuperacion
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosUltimoMovimientoRecuperacion(AnimalInfo animal)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            
                            {"@AnimalID", animal.AnimalID},
                            {"@OrganizacionID", animal.OrganizacionIDEntrada}
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
        /// Obtener animal en deteccion
        /// </summary>
        /// <param name="animal">Animal que se buscara en deteccion</param>
        /// <returns>Diccionario con los paramatros</returns>
        internal static Dictionary<string, object> ObtenerHistorialClinico(AnimalInfo animal)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                       
                            {"@Arete", animal.Arete},
                            {"@TipoMovEntradaEnfermeria", (int)TipoMovimiento.EntradaEnfermeria},
                            {"@TipoMovEntradaSalidaEnfermeria", (int)TipoMovimiento.EntradaSalidaEnfermeria},
                            {"@TipoMovSalidaRecuperacion", (int)TipoMovimiento.SalidaEnfermeria},
                            {"@OrganizacionID", animal.OrganizacionIDEntrada}
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
        /// Obtiene los parametros necesarios para eliminar una deteccion
        /// </summary>
        /// <param name="deteccion"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosEliminarDeteccion(DeteccionInfo deteccion)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@DeteccionID", deteccion.DeteccionID},
                            {"@Arete", deteccion.Arete},
                            {"@UsuarioModificacion", deteccion.UsuarioCreacionID},
                            {"@Estatus", (int)EstatusEnum.Inactivo}
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
        /// obtiene los parametros necesarios para guardar una deteccion
        /// </summary>
        /// <param name="deteccion"></param>
        /// <param name="problemasDetectados"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarDeteccion(AnimalDeteccionInfo deteccion, IList<ProblemaInfo> problemasDetectados)
        {

            try
            {
                Logger.Info();
                var xml =
                   new XElement("ROOT",
                                from probelma in problemasDetectados where probelma.isCheked
                               
                                select
                                    new XElement("Problemas",
                                                 new XElement("ProblemaID", probelma.ProblemaID))
                                              
                                    );


                var parametros =
                    new Dictionary<string, object>
                        {
                            
                            {"@Deteccion", deteccion.DeteccionID},
                            {"@Arete",deteccion.Animal.Arete},
                            {"@Estatus", (int)EstatusEnum.Activo},
                            {"@AnimalMovimientoID", deteccion.AnimalMovimiento.AnimalMovimientoID},
                            {"@Usuario", deteccion.UsuarioID},
                            {"@GradoId", deteccion.GradoEnfermedad.GradoID},
                            {"@Justificacion", deteccion.Justificacion},
                            {"@Diagnostico", deteccion.Diagnostico},
                            {"@ProblemasAnalista", xml.ToString()},
                            
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
        /// obtiene los parametros necesarios para consultar una animal en interfaz salida
        /// </summary>
        /// <param name="animalInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosAnimalSalidaEnfermeria(AnimalInfo animalInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                    {
                        {"@AnimalID", animalInfo.AnimalID},
                        {"@OrganizacionID", animalInfo.OrganizacionIDEntrada},
                        {"@TipoMovimientoID", (int) TipoMovimiento.SalidaEnfermeria},
                        {"@Activo", EstatusEnum.Activo}
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
        /// Obtiene los parametros necesarios para obtener los datos de la compra por folio entrada
        /// </summary>
        /// <param name="folioEntrada"></param>
        /// <param name="organicacionId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosDatosCompra(int folioEntrada, int organicacionId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            
                            {"@FolioEntrada", folioEntrada},
                            {"@OrganizacionID", organicacionId}
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
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, EnfermeriaInfo filtro)
        {
            try
            {
                Logger.Info();

                filtro.Organizacion = string.IsNullOrWhiteSpace(filtro.Organizacion) ? "0" : filtro.Organizacion;
                filtro.OrganizacionInfo = filtro.OrganizacionInfo ?? new OrganizacionInfo
                                                                         {
                                                                             OrganizacionID =
                                                                                 int.Parse(filtro.Organizacion)
                                                                         };
               
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@EnfermeriaID", filtro.EnfermeriaID},
                            {"@OrganizacionID", filtro.OrganizacionInfo.OrganizacionID},
                            {"@Descripcion", filtro.Descripcion},
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
        ///  Obtiene Parametros por Id
        /// </summary>
        /// <param name="filtro">Identificador de la entidad Enfermeria</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorID(EnfermeriaInfo filtro)
        {
            try
            {
                Logger.Info();
                filtro.Organizacion = string.IsNullOrWhiteSpace(filtro.Organizacion) ? "0" : filtro.Organizacion;
                filtro.OrganizacionInfo = filtro.OrganizacionInfo ?? new OrganizacionInfo
                {
                    OrganizacionID =
                        int.Parse(filtro.Organizacion)
                };
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@EnfermeriaID", filtro.EnfermeriaID},
                            {"@OrganizacionID", filtro.OrganizacionInfo.OrganizacionID}
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
        public static Dictionary<string, object> ObtenerParametrosCrear(EnfermeriaInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@OrganizacionID", info.Organizacion},
							{"@Descripcion", info.Descripcion},
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
        public static Dictionary<string, object> ObtenerParametrosActualizar(EnfermeriaInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@EnfermeriaID", info.EnfermeriaID},
							{"@OrganizacionID", info.Organizacion},
							{"@Descripcion", info.Descripcion},
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
        /// Obtiene Parametro pora filtrar por descripción 
        /// </summary> 
        /// <param name="descripcion">Descripción de la entidad </param>
        /// <param name="organizacionID"> </param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorDescripcion(string descripcion, int organizacionID)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@Descripcion", descripcion},
                                {"@OrganizacionID", organizacionID}
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
