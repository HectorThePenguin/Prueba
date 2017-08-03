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
    internal class AuxCalidadMezcladoFormulasAlimentoDAL
    {
        /// <summary>
        ///     Metodo para obtener los parametros para guardr un Animal
        /// </summary>
        /// <param name="animalInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> TraerDatosTablaResumen(int organizacionID, int FormulasMuestrear)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                    {
                        {"@organizacionID", organizacionID},
                        {"@formulaID", FormulasMuestrear}
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
        /// obtener parametros para guardar calidad mezclado
        /// </summary>
        /// <param name="resultado"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarCalidadMezcladoFormulaAlimentoReparto(
            CalidadMezcladoFormulasAlimentoInfo resultado)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                    {
                        {"@organizacionID", resultado.Organizacion.OrganizacionID},
                        {"@TipoTecnicaID", resultado.TipoTecnicaID},
                        {"@UsuarioIDLaboratorio", resultado.UsuarioLaboratotorista.UsuarioID},
                        {"@FormulaID", resultado.Formula.FormulaId},
                        {"@FechaPremezcla", resultado.FechaPremezcla},
                        {"@FechaBatch", resultado.FechaBatch},
                        {"@TipoLugarMuestraID", resultado.LugarToma},
                        {
                            "@CamionRepartoID",
                            resultado.CamionReparto == null ? (object) null : resultado.CamionReparto.CamionRepartoID
                        },
                        {"@ChoferID", resultado.Chofer == null ? (object) null : resultado.Chofer.ChoferID},
                        {
                            "@MezcladoraID",
                            resultado.Mezcladora == null ? (object) null : resultado.Mezcladora.mezcladoraID
                        },
                        {
                            "@OperadorIDOperador",
                            resultado.Operador == null ? (object) null : resultado.Operador.OperadorID
                        },
                        {"@LoteID", resultado.LoteID},
                        {"@Batch", resultado.Batch},
                        {"@TiempoMezclado", resultado.TiempoMezclado},
                        {"@OperadorIDLaboratorista", resultado.PersonaMuestreo.OperadorID},
                        {"@GramosMicrot", resultado.GramosMicrot},
                        {"@Activo", EstatusEnum.Activo},
                        {"@UsuarioID", resultado.UsuarioLaboratotorista.UsuarioID}
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
        /// obtener parametros para guardar calidad mezclado detalle
        /// </summary>
        /// <param name="listaTotalRegistrosGuardar"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarCalidadMezcladoFormulaAlimentoRepartoDetalle(
            IList<CalidadMezcladoFormulasAlimentoInfo> listaTotalRegistrosGuardar,
            CalidadMezcladoFormulasAlimentoInfo result)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                        from detalle in listaTotalRegistrosGuardar
                        select new XElement("CalidadMezclado",
                            new XElement("TipoMuestra", detalle.AnalisisMuestras),
                            new XElement("NumeroMuestras", detalle.NumeroMuestras),
                            new XElement("Peso", detalle.PesoGramos),
                            new XElement("Particulas", detalle.ParticulasEncontradas),
                            new XElement("CalidadMezcladoID", result.CalidadMezcladoID),
                            new XElement("UsuarioCreacionID", result.UsuarioLaboratotorista.UsuarioID)
                            ));
                parametros = new Dictionary<string, object>
                {
                    {"@XmlCalidadMezclado", xml.ToString()},
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
        /// Metodo que se utiliza para traer de la tabla CalidadMezcladoDetalle, los registros que se ocupan para cargar en el grid "Analisis de muestras
        /// Inicial-Media-Final" cuando hay datos cargados en el mismo dia
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> CargarTablaMezcladoDetalle(
            CalidadMezcladoFormulasAlimentoInfo calidadMezcladoFormulaAlimentoInfo)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object>
                {
                    {"@OrganizacionID", calidadMezcladoFormulaAlimentoInfo.Organizacion.OrganizacionID},
                    {"@FormulaID", calidadMezcladoFormulaAlimentoInfo.Formula.FormulaId},
                    {"@TipoTecnicaID", calidadMezcladoFormulaAlimentoInfo.TipoTecnicaID}
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
        /// Metodo para armar los parametros para el Sp CalidadMezclado_ObtenerImpresion 
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosImpresionCalidadMezclado(FiltroImpresionCalidadMezclado filtro)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object>
                {
                    {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                    {"@Fecha", filtro.Fecha},
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
