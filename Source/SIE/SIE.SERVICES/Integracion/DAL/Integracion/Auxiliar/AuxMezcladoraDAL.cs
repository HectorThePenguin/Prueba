using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxMezcladoraDAL
    {
        /// <summary>
        /// ObtenerParametrosMezcladora
        /// </summary>
        /// <param name="mezcladoraInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosMezcladora(MezcladoraInfo mezcladoraInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@MezcladoraID", mezcladoraInfo.mezcladoraID},
                            {"@OrganizacionID",mezcladoraInfo.organizaionID},
                            {"@Activo",mezcladoraInfo.Activo}
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
        /// ObtenerParametrosMezcladoraPagina
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosMezcladoraPagina(PaginacionInfo pagina, MezcladoraInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@NumeroEconomico", filtro.NumeroEconomico.Trim()},
                        {"@mezcladoraID", filtro.mezcladoraID},
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
        /// ObtenerParametrosActualizarFactor
        /// </summary>
        /// <param name="listaCalidadMezcladoInfos"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarFactor(List<CalidadMezcladoFactorInfo> listaCalidadMezcladoInfos)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in listaCalidadMezcladoInfos
                                 select new XElement("CalidadMezclado",
                                        new XElement("TipoMuestraID", detalle.TipoMuestraID),
                                        new XElement("Factor", detalle.Factor),
                                        new XElement("PesoBaseHumeda", detalle.PesoBH),
                                        new XElement("PesoBaseSeca", detalle.PesoBS),
                                        new XElement("UsuarioModifica", detalle.UsuarioModifica)
                                       ));
                parametros = new Dictionary<string, object>
                    {
                        {"@XmlCalidadMezclado", xml.ToString()}
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
        /// ObtenerParametrosCalidadMezcladoFormulaAlimento
        /// </summary>
        /// <param name="calidadMezcladoFormulaAlimentoInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCalidadMezcladoFormulaAlimento(CalidadMezcladoFormulasAlimentoInfo calidadMezcladoFormulaAlimentoInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", calidadMezcladoFormulaAlimentoInfo.Organizacion.OrganizacionID},
                        {"@FormulaID", calidadMezcladoFormulaAlimentoInfo.Formula.FormulaId},
                        {"@TipoTecnicaID", calidadMezcladoFormulaAlimentoInfo.TipoTecnicaID}
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
