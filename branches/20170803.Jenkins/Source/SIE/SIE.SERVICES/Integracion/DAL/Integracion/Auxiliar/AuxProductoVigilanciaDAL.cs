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
using SIE.Base.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxProductoVigilanciaDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, VigilanciaInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@ID", filtro.ID},
                        {"@Descripcion", filtro.Descripcion},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite},
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
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosProductosPorPaginaPagina(PaginacionInfo pagina, ProductoInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var listaFamilias = new List<FamiliasEnum>
                {
                    FamiliasEnum.MateriaPrimas
                };
                var listaSubFamilia = new List<SubFamiliasEnum>
                {
                    SubFamiliasEnum.Pacas,
                    SubFamiliasEnum.MicroIngredientes
                };

                var xmlFamilia =
                   new XElement("ROOT",
                                from familia in listaFamilias
                                select new XElement("Familias",
                                       new XElement("Familia", (int)familia)));

                var xmlSubFamilia =
                   new XElement("ROOT",
                                from subFamilia in listaSubFamilia
                                select new XElement("SubFamilias",
                                       new XElement("SubFamilia", (int)subFamilia)));

                parametros = new Dictionary<string, object>
                    {
                        {"@Descripcion", filtro.ProductoDescripcion},
                        {"@Activo", filtro.Activo},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite},
                        {"@ParametroDescripcion", ParametrosEnum.SubProductosCrearContrato.ToString()},
                        {"@FamiliaXML", xmlFamilia.ToString()},
                        {"@SubFamiliaXML", xmlSubFamilia.ToString()}
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
        /// Metodo que obtiene los parametros para lista pagina de organizaciones 
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <param name="dependencias"> </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorFolio( PaginacionInfo pagina,
                                                                            VigilanciaInfo filtro,
                                                                            IList<IDictionary<IList<String>, Object>> dependencias)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros =
                    new Dictionary<string, object>
                        {
                            {"@ID", filtro.ID},
                            {"@Descripcion", filtro.Descripcion},
                            {"@Inicio", pagina.Inicio},
                            {"@Limite", pagina.Limite},
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
        /// Obtiene parametros para crear
        /// </summary>
        /// <param name="info">Valores de la entidad</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrear(VigilanciaInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@TipoOrganizacionID", info.TipoOrganizacion.TipoOrganizacionID},
							{"@Descripcion", info.Descripcion},
                            {"@Direccion", info.Direccion},
							{"@RFC", info.RFC},
							{"@IvaID", info.Iva.IvaID},
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
        internal static Dictionary<string, object> ObtenerParametrosActualizar(VigilanciaInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@ID", info.ID},
							{"@TipoOrganizacionID", info.TipoOrganizacion.TipoOrganizacionID},
							{"@Descripcion", info.Descripcion},
                            {"@Direccion", info.Direccion},
							{"@RFC", info.RFC},
							{"@IvaID", info.Iva.IvaID},
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


        internal static Dictionary<string, object> ObtenerParametrosPorFolio(VigilanciaInfo filtro, IList<IDictionary<IList<string>, object>> dependencias)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object> {{"@ID", filtro.ID}};
                AuxDAL.ObtenerDependencias(parametros, dependencias);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosPorTipoOrigen(VigilanciaInfo filtro, IList<IDictionary<IList<string>, object>> dependencias)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = 
                    new Dictionary<string, object>
                        {
                            {"@ID", filtro.ID}
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

        internal static Dictionary<string, object> ObtenerParametrosPorTipoOrigen(PaginacionInfo pagina, VigilanciaInfo filtro, IList<IDictionary<IList<string>, object>> dependencias)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros =
                    new Dictionary<string, object>
                        {
                            {"@ID", filtro.ID},
                            {"@Descripcion", filtro.Descripcion},
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

        internal static Dictionary<string, object> ObtenerParametrosEmbarqueTipoOrganizacion(PaginacionInfo pagina, VigilanciaInfo organizacionInfo, IList<IDictionary<IList<string>, object>> dependencias)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros =
                    new Dictionary<string, object>
                        {
                            {"@ID", organizacionInfo.ID},
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

        internal static Dictionary<string, object> ObtenerParametrosEmbarqueTipoOrganizacion(VigilanciaInfo organizacionInfo, IList<IDictionary<IList<string>, object>> dependencias)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = 
                    new Dictionary<string, object>
                        {
                            {"@ID", organizacionInfo.ID}
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
        ///     Obtiene Parametros para traer las organizaciones que tengan embarques pendientes de recibir
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="estatus"> </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPendientesRecibir(int organizacionId, int estatus)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ID", organizacionId},
                            {"@Estatus", estatus}
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
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroPorIdConIva(int organizacionId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@ID", organizacionId}
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
        /// Obtiene parametros para obtener lista paginada
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosProductosPorID(ProductoInfo producto)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var listaFamilias = new List<FamiliasEnum>
                {
                    FamiliasEnum.MateriaPrimas
                };
                var listaSubFamilia = new List<SubFamiliasEnum>
                {
                    SubFamiliasEnum.Pacas,
                    SubFamiliasEnum.MicroIngredientes
                };

                var xmlFamilia =
                   new XElement("ROOT",
                                from familia in listaFamilias
                                select new XElement("Familias",
                                       new XElement("Familia", (int)familia)));

                var xmlSubFamilia =
                   new XElement("ROOT",
                                from subFamilia in listaSubFamilia
                                select new XElement("SubFamilias",
                                       new XElement("SubFamilia", (int)subFamilia)));

                parametros = new Dictionary<string, object>
                    {
                        {"@ProductoID", producto.ProductoId},
                        {"@ParametroDescripcion", ParametrosEnum.SubProductosCrearContrato.ToString()},
                        {"@FamiliaXML", xmlFamilia.ToString()},
                        {"@SubFamiliaXML", xmlSubFamilia.ToString()}
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
