using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using System.Linq;
using System.Xml.Linq;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxProduccionFormulaDAL
    {
        /// <summary>
        /// Obtiene los parametros para guardar una formula
        /// </summary>
        /// <param name="produccionFormula"></param>
        /// <returns></returns>
        internal static Dictionary<string,object> GuardarProduccionFormula(ProduccionFormulaInfo produccionFormula)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", produccionFormula.Organizacion.OrganizacionID},
                        {"@FormulaID", produccionFormula.Formula.FormulaId},
                        {"@CantidadProducida", produccionFormula.CantidadProducida},
                        {"@TipoFolio",TipoFolio.ProduccionFormula},
                        {"@AlmacenMovimientoEntradaID",produccionFormula.AlmacenMovimientoEntradaID},
                        {"@AlmacenMovimientoSalidaID",produccionFormula.AlmacenMovimientoSalidaID},
                        {"@UsuarioCreacionID", produccionFormula.UsuarioCreacionId},
                        {"@FechaProduccion",produccionFormula.FechaProduccion}
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
        public static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, ProduccionFormulaInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                            {"@Descripcion", filtro.DescripcionFormula ?? string.Empty},
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
        /// <param name="produccionFormulaID">Identificador de la entidad ProduccionFormula</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorID(int produccionFormulaID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ProduccionFormulaID", produccionFormulaID}
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
        /// <param name="produccionFormula">Identificador de la entidad ProduccionFormula</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorFolioFormula(ProduccionFormulaInfo produccionFormula)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@FolioMovimiento", produccionFormula.FolioMovimiento},
                            {"@OrganizacionID", produccionFormula.Organizacion.OrganizacionID}
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
        /// <param name="produccionFormulaID">Identificador de la entidad ProduccionFormula</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosPorIDCompleto(int produccionFormulaID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ProduccionFormulaID", produccionFormulaID}
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
        /// Obtiene los parametros necesarios
        /// para la ejecucion del procedimiento
        /// almacenado ProduccionFormula_ConciliacionObtenerPorFecha
        /// </summary>
        /// <param name="organizacionID"> </param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosConciliacion(int organizacionID, DateTime fechaInicio, DateTime fechaFinal)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@FechaInicial", fechaInicio},
                            {"@FechaFinal", fechaFinal},
                            {"@OrganizacionID", organizacionID},
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
        /// <param name="produccionFormulaLista"> </param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosResumen(List<ProduccionFormulaInfo> produccionFormulaLista)
        {
            try
            {
                Logger.Info();

                var xmlFormulas =
                  new XElement("ROOT",
                               from formula in produccionFormulaLista
                               select new XElement("Formula",
                                          new XElement("FormulaID", formula.Formula.FormulaId)
                               ));

                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", produccionFormulaLista[0].Organizacion.OrganizacionID},
                            {"@AlmacenID", produccionFormulaLista[0].Almacen.AlmacenID},
                            {"@Formulas", xmlFormulas.ToString()}
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
