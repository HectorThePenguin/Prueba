using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxTratamientoDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, TratamientoInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@TratamientoID", filtro.TratamientoID},
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
        /// Obtiene parametros para crear
        /// </summary>
        /// <param name="info">Valores de la entidad</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrear(TratamientoInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@OrganizacionID", info.Organizacion.OrganizacionID},
							{"@CodigoTratamiento", info.CodigoTratamiento},
							{"@TipoTratamientoID", info.TipoTratamientoInfo.TipoTratamientoID},
                            {"@Sexo", info.Sexo == Sexo.Macho ? 'M' : 'H'},
							//{"@Sexo", info.Sexo},
							{"@RangoInicial", info.RangoInicial},
							{"@RangoFinal", info.RangoFinal},
                            {"@UsuarioCreacionID", info.UsuarioCreacionID},
                            {"@Activo", info.Activo},
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
        internal static Dictionary<string, object> Centros_ObtenerParametrosCrear(TratamientoInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@OrganizacionID", info.Organizacion.OrganizacionID},
							{"@CodigoTratamiento", info.CodigoTratamiento},
							{"@Descripcion", info.Auxiliar},
                            {"@UsuarioCreacionID", info.UsuarioCreacionID},
                            {"@Activo", info.Activo},
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
        internal static Dictionary<string, object> ObtenerParametrosActualizar(TratamientoInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@TratamientoID", info.TratamientoID},
							{"@OrganizacionID", info.Organizacion.OrganizacionID},
							{"@CodigoTratamiento", info.CodigoTratamiento},
							{"@TipoTratamientoID", info.TipoTratamientoInfo.TipoTratamientoID},
                            {"@Sexo", info.Sexo == Sexo.Macho ? 'M' : 'H'},
							//{"@Sexo", info.Sexo},
							{"@RangoInicial", info.RangoInicial},
							{"@RangoFinal", info.RangoFinal},
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
        ///  Obtiene parametros para actualizar
        /// </summary>
        /// <param name="info">Valores de la entidad</param>
        /// <returns></returns>
        internal static Dictionary<string, object> Centros_ObtenerParametrosActualizar(TratamientoInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@TratamientoID", info.TratamientoID},
							{"@OrganizacionID", info.Organizacion.OrganizacionID},
							{"@CodigoTratamiento", info.CodigoTratamiento},
							{"@Descripcion", info.Auxiliar},
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
        /// <param name="tratamientoID">Identificador de la entidad Tratamiento</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorID(int tratamientoID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@TratamientoID", tratamientoID}
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
        internal static Dictionary<string, object> ObtenerTodos(EstatusEnum estatus)
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

        internal static Dictionary<string, object> ObtenerTipoTratamientos(TratamientoInfo tratamiento, Metafilaxia bMetafilaxia)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", tratamiento.OrganizacionId},
                            {"@Sexo",tratamiento.Sexo.ToString()[0]},
                            {"@Peso",tratamiento.Peso},
                            {"@Metafilaxia",bMetafilaxia}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerTratamientosPorTipo(TratamientoInfo tratamiento)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", tratamiento.OrganizacionId},
                            {"@Sexo",tratamiento.Sexo.ToString()[0]},
                            {"@Peso",tratamiento.Peso}
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
        /// <param name="filtroTratamiento">Filtros para Consulta </param>
        /// <param name="pagina">Valores para el paginado </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerTratamientosPorFiltro(PaginacionInfo pagina, FiltroTratamientoInfo filtroTratamiento)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", filtroTratamiento.Organizacion.OrganizacionID},
                            {"@CodigoTratamiento",filtroTratamiento.CodigoTratamiento},
                            {"@TipoTratamientoID",filtroTratamiento.TipoTratamiento.TipoTratamientoID},
                            {"@Inicio",pagina.Inicio},
                            {"@Limite",pagina.Limite},
                            {"@Activo",filtroTratamiento.Estatus}
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
        /// <param name="filtroTratamiento">Filtros para Consulta </param>
        /// <param name="pagina">Valores para el paginado </param>
        /// <returns></returns>
        internal static Dictionary<string, object> Centros_ObtenerTratamientosPorFiltro(PaginacionInfo pagina, FiltroTratamientoInfo filtroTratamiento)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", filtroTratamiento.Organizacion.OrganizacionID},
                            {"@CodigoTratamiento",filtroTratamiento.CodigoTratamiento},
                            {"@Inicio",pagina.Inicio},
                            {"@Limite",pagina.Limite},
                            {"@Activo",filtroTratamiento.Estatus}
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
        /// <param name="tratamiento">Valores de la entidad</param>
        ///  <param name="tratamientoID">Id de la tabla Tratamiento</param>
        /// <returns></returns>
        internal static Dictionary<string, object> GuardarTratamientoProducto(TratamientoInfo tratamiento, int tratamientoID)
        {
            try
            {
                Logger.Info();
                var xml =
                                 new XElement("ROOT",
                                              from info in tratamiento.ListaTratamientoProducto
                                              select
                                                  new XElement("TratamientoProducto",
                                                               new XElement("TratamientoProductoID", info.TratamientoProductoID),
                                                               new XElement("TratamientoID", tratamientoID),
                                                               new XElement("ProductoID", info.Producto.ProductoId),
                                                               new XElement("Dosis", info.Dosis),
                                                               new XElement("Activo", info.Activo.GetHashCode()),
                                                               new XElement("UsuarioCreacionID", info.UsuarioCreacionID),
                                                               new XElement("UsuarioModificacionID", info.UsuarioModificacionID)
                                                  ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlTratamientoProducto", xml.ToString()}
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
        /// <param name="tratamiento">Valores de la entidad</param>
        ///  <param name="tratamientoID">Id de la tabla Tratamiento</param>
        /// <returns></returns>
        internal static Dictionary<string, object> Centros_GuardarTratamientoProducto(TratamientoInfo tratamiento, int tratamientoID, int organizacionID)
        {
            try
            {
                Logger.Info();
                var xml =
                                 new XElement("ROOT",
                                              from info in tratamiento.ListaTratamientoProducto
                                              select
                                                  new XElement("TratamientoProducto",
                                                               new XElement("TratamientoProductoID", info.TratamientoProductoID),
                                                               new XElement("TratamientoID", tratamientoID),
                                                               new XElement("ProductoID", info.Producto.ProductoId),
                                                               new XElement("Dosis", info.Dosis),
                                                               new XElement("Activo", info.Activo.GetHashCode()),
                                                               new XElement("UsuarioCreacionID", info.UsuarioCreacionID),
                                                               new XElement("UsuarioModificacionID", info.UsuarioModificacionID),
                                                               new XElement("OrganizacionID", organizacionID),
                                                               new XElement("FactorMacho", info.FactorMacho),
                                                               new XElement("FactorHembra", info.FactorHembra),
                                                               new XElement("Factor", info.Factor.GetHashCode())
                                                  ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlTratamientoProducto", xml.ToString()}, {"@OrganizacionID", organizacionID}
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
        /// Obtiene los parametros para validar si el código del tratamiento ya existe para esa organización
        /// </summary>
        /// <param name="info">Valores de la entidad</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosValidaExisteTratamiento(TratamientoInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@OrganizacionID", info.Organizacion.OrganizacionID},
							{"@CodigoTratamiento", info.CodigoTratamiento},
                            {"@Sexo", info.Sexo}
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
        /// Obtiene los parametros necesarias para la obtencion de los tratamientos por problema
        /// </summary>
        /// <param name="info"></param>
        /// <param name="listaProblemas"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerTratamientosPorProblemas(TratamientoInfo info, List<int> listaProblemas)
        {
            try
            {
                Logger.Info();
                var xmlProblemas =
                   new XElement("ROOT",
                                from tipoCorral in listaProblemas
                                select
                                    new XElement("Problemas",
                                                 new XElement("ProblemaID", tipoCorral))
                                    );
                var listaTiposTratamientos = new List<int> {(int) TipoTratamiento.Enfermeria};

                /*listaTiposTratamientos.Add((int)TipoTratamiento.Arete);*/

                var xmlTiposTratamientso =
                   new XElement("ROOT",
                                from tipoTratamiento in listaTiposTratamientos
                                select
                                    new XElement("TiposTratamientos",
                                                 new XElement("TiposTratamientosID", tipoTratamiento))
                                    );
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", info.OrganizacionId},
                            {"@Sexo",info.Sexo.ToString()==Sexo.Macho.ToString() ? ((char)Sexo.Macho).ToString(): ((char)Sexo.Hembra).ToString()},
                            {"@Peso", info.Peso},
                            {"@XmlProblemas", xmlProblemas.ToString()},
                            {"@XmlTipos", xmlTiposTratamientso.ToString()}
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
        /// Obtiene el costo de un tratamiento en un movimiento
        /// </summary>
        /// <param name="movimiento">se debe de proporcionar Organizacion y animalmovimiento</param>
        /// <param name="tratamientoID">Identificador del producto</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCostoPorMovimiento(AnimalMovimientoInfo movimiento, int tratamientoID)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@OrganizacionID", movimiento.OrganizacionID},
								{"@Tratamiento", tratamientoID},
                                {"@AnimalMovimiento", movimiento.AnimalMovimientoID}
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
        /// Obtiene el costo de un tratamiento en un movimiento
        /// </summary>
        /// <param name="movimiento">se debe de proporcionar Organizacion y animalmovimiento</param>
        /// <param name="tratamiento">Identificador del producto</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCostoPorMovimientoProducto(AnimalMovimientoInfo movimiento, TratamientoProductoInfo tratamiento)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@OrganizacionID", movimiento.OrganizacionID},
								{"@TratamientoID", tratamiento.Tratamiento.TratamientoID},
                                {"@ProductoID", tratamiento.Producto.ProductoId},
                                {"@AnimalMovimiento", movimiento.AnimalMovimientoID}
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
        /// Obtiene los parametros necesarias para la obtencion de los tratamientos por tipo tratamiento
        /// </summary>
        /// <param name="tratamientoInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerTratamientosPorTipo(TratamientoInfo tratamientoInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", tratamientoInfo.OrganizacionId},
                            {"@Sexo",tratamientoInfo.Sexo.ToString()==Sexo.Macho.ToString() ? ((char)Sexo.Macho).ToString(): ((char)Sexo.Hembra).ToString()},
                            {"@Peso", tratamientoInfo.Peso},
                            {"@TipoTratamiento", tratamientoInfo.TipoTratamiento}
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
