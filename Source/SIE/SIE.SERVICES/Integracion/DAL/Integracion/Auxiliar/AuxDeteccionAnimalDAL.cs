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
    public class AuxDeteccionAnimalDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, DeteccionAnimalInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@DeteccionAnimalID", filtro.DeteccionAnimalID},
                            //{"@Descripcion", filtro.Descripcion},
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
        public static Dictionary<string, object> ObtenerParametrosCrear(DeteccionAnimalInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@AnimalMovimientoID", info.AnimalMovimientoID},
							{"@Arete", info.Arete},
							{"@AreteMetalico", info.AreteMetalico},
							{"@FotoDeteccion", info.FotoDeteccion},
							{"@LoteID", info.Lote.LoteID},
							{"@OperadorID", info.Operador.OperadorID},
							{"@TipoDeteccionID", info.TipoDeteccion.TipoDeteccionID},
							{"@GradoID", info.Grado.GradoID},
							{"@Observaciones", info.Observaciones},
							{"@NoFierro", info.NoFierro},
							{"@FechaDeteccion", info.FechaDeteccion},
							{"@DeteccionAnalista", info.DeteccionAnalista},
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
        public static Dictionary<string, object> ObtenerParametrosActualizar(DeteccionAnimalInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@DeteccionAnimalID", info.DeteccionAnimalID},
							{"@AnimalMovimientoID", info.AnimalMovimientoID},
							{"@Arete", info.Arete},
							{"@AreteMetalico", info.AreteMetalico},
							{"@FotoDeteccion", info.FotoDeteccion},
							{"@LoteID", info.Lote.LoteID},
							{"@OperadorID", info.Operador.OperadorID},
							{"@TipoDeteccionID", info.TipoDeteccion.TipoDeteccionID},
							{"@GradoID", info.Grado.GradoID},
							{"@Observaciones", info.Observaciones},
							{"@NoFierro", info.NoFierro},
							{"@FechaDeteccion", info.FechaDeteccion},
							{"@DeteccionAnalista", info.DeteccionAnalista},
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
        /// <param name="deteccionAnimalID">Identificador de la entidad DeteccionAnimal</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorID(int deteccionAnimalID)
        {
            try
            {
                Logger.Info();
                var parametros = 
                    new Dictionary<string, object>
                        {
                            {"@DeteccionAnimalID", deteccionAnimalID}
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
								{"@DeteccionAnimalID", descripcion}
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
        ///  Obtiene por el AnimalMovimiento
        /// </summary>
        /// <param name="animalMovimientoID">Identificador del Animal Movimiento</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorAnimalMovimientoID(long animalMovimientoID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AnimalMovimientoID", animalMovimientoID}
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

