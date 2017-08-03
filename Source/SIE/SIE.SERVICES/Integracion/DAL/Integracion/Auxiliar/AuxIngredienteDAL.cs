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
    internal class AuxIngredienteDAL
    {
        /// <summary>
        /// Obtiene los parametros para obtener por id
        /// </summary>
        /// <param name="ingrediente"></param>
        /// <param name="estatus"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorId(IngredienteInfo ingrediente,EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@IngredienteID", ingrediente.IngredienteId},
                            {"@Activo",estatus}
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
        /// Obtiene los parametros para obtener por formula
        /// </summary>
        /// <param name="ingrediente"></param>
        /// <param name="estatus"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorFormula(IngredienteInfo ingrediente,EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@FormulaID", ingrediente.Formula.FormulaId},
                            {"@OrganizacionID",ingrediente.Organizacion.OrganizacionID},
                            {"@Activo",estatus}
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
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, IngredienteInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@IngredienteID", filtro.IngredienteId},
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
        internal static Dictionary<string, object> ObtenerParametrosCrear(IngredienteInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@OrganizacionID", info.Organizacion.OrganizacionID},
							{"@FormulaID", info.Formula.FormulaId},
							{"@ProductoID", info.Producto.ProductoId},
							{"@PorcentajeProgramado", info.PorcentajeProgramado},
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
        internal static Dictionary<string, object> ObtenerParametrosActualizar(IngredienteInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@IngredienteID", info.IngredienteId},
							{"@OrganizacionID", info.Organizacion.OrganizacionID},
							{"@FormulaID", info.Formula.FormulaId},
							{"@ProductoID", info.Producto.ProductoId},
							{"@PorcentajeProgramado", info.PorcentajeProgramado},
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
        /// <param name="ingredienteID">Identificador de la entidad Ingrediente</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorID(int ingredienteID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@IngredienteID", ingredienteID}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        //ObtenerPorIdOrganizacionFormulaProducto
        internal static Dictionary<string, object> ObtenerPorIdOrganizacionFormulaProducto(int formula, int ProductoId, int OrganizacionID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@FormulaID", formula},
                            {"@ProductoID", ProductoId},
                            {"@OrganizacionID", OrganizacionID}
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
								{"@IngredienteID", descripcion}
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
        internal static Dictionary<string, object> ObtenerParametrosPorPaginaFormulaOrganizacion(PaginacionInfo pagina, IngredienteInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", filtro.Organizacion != null ? filtro.Organizacion.OrganizacionID : 0},
                            {"@TipoFormulaID", filtro.Formula != null && filtro.Formula.TipoFormula != null ? filtro.Formula.TipoFormula.TipoFormulaID : 0},
                            {"@ProductoID", filtro.Producto != null ? filtro.Producto.ProductoId : 0},
                            {"@FormulaID", filtro.Formula != null ? filtro.Formula.FormulaId : 0},
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
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPaginaFormula(PaginacionInfo pagina, IngredienteInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@IngredienteID", filtro.IngredienteId},
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
        /// <param name="listaIngredientes">Valores de la entidad</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarIngredientesXML(List<IngredienteInfo> listaIngredientes)
        {
            try
            {
                Logger.Info();
                var xml =
                                 new XElement("ROOT",
                                              from info in listaIngredientes
                                              select
                                                  new XElement("Ingrediente",
                                                               new XElement("IngredienteID", info.IngredienteId),
                                                               new XElement("OrganizacionID", info.Organizacion.OrganizacionID),
                                                               new XElement("FormulaID", info.Formula.FormulaId),
                                                               new XElement("ProductoID", info.Producto.ProductoId),
                                                               new XElement("PorcentajeProgramado", info.PorcentajeProgramado),
                                                               new XElement("Activo", info.Activo.GetHashCode()),
                                                               new XElement("UsuarioCreacionID", info.UsuarioCreacionID),
                                                               new XElement("UsuarioModificacionID", info.UsuarioModificacionID)
                                                  ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@IngredientesXML", xml.ToString()}
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
