using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using System.Xml.Linq;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class AuxMigracionDAL : DALBase
    {
        /// <summary>
        /// Metodo para almacenar el resumen
        /// </summary>
        /// <param name="lista"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarResumenXML(List<ResumenInfo> lista, int organizacionId)
        {
            try
            {
                Logger.Info();
                var xml =
                 new XElement("ROOT",
                              from resumenInfo in lista
                              select
                                new XElement("ResumenInfo",
                                    new XElement("Organizacion", organizacionId),
                                    new XElement("FechaInicio", resumenInfo.FechaInicio),
                                    new XElement("FechaDisponibilidad", resumenInfo.FechaDisponibilidad),
                                    new XElement("Corral", resumenInfo.Corral),
                                    new XElement("Lote", resumenInfo.Lote),
                                    new XElement("TipoGanado", resumenInfo.TipoGanado),
                                    new XElement("Cabezas", resumenInfo.Cabezas),
                                    new XElement("DiasEng", resumenInfo.DiasEng),
                                    new XElement("GananciaDiaria", resumenInfo.GananciaDiaria),
                                    new XElement("PesoOrigen", resumenInfo.PesoOrigen),
                                    new XElement("PesoProyectado", resumenInfo.PesoProyectado),
                                    new XElement("FormulaActual", resumenInfo.FormulaActual),
                                    new XElement("Consumo", resumenInfo.Consumo),
                                    new XElement("Ganado", resumenInfo.Ganado),
                                    new XElement("Alimento", resumenInfo.Alimento),
                                    new XElement("Comision", resumenInfo.Comision),
                                    new XElement("Fletes", resumenInfo.Fletes),
                                    new XElement("ImpPred", resumenInfo.ImpPred),
                                    new XElement("GuiaTrans", resumenInfo.GuiaTrans),
                                    new XElement("SeguroDeCorrales", resumenInfo.SeguroDeCorrales),
                                    new XElement("SeguroDeTransporte", resumenInfo.SeguroDeTransporte),
                                    new XElement("GastoDeEngorda", resumenInfo.GastoDeEngorda),
                                    new XElement("GastoDeCentro", resumenInfo.GastoDeCentro),
                                    new XElement("GastoDePlanta", resumenInfo.GastoDePlanta),
                                    new XElement("CertificadoyBanos", resumenInfo.CertificadoyBanos),
                                    new XElement("AlimentoCentros", resumenInfo.AlimentoCentros),
                                    new XElement("Pruebas", resumenInfo.Pruebas),
                                    new XElement("MedicamentoEnCentros", resumenInfo.MedicamentoEnCentros),
                                    new XElement("Renta", resumenInfo.Renta),
                                    new XElement("SeguroDeEnfermedadesExoticas", resumenInfo.SeguroDeEnfermedadesExoticas),
                                    new XElement("MedicamentoDeEnfermerias", resumenInfo.MedicamentoDeEnfermerias),
                                    new XElement("MedicamentoDeImpolante", resumenInfo.MedicamentoDeImpolante),
                                    new XElement("MedicamentoDeReimplante", resumenInfo.MedicamentoDeReimplante),
                                    new XElement("GastosIndirectos", resumenInfo.GastosIndirectos),
                                    new XElement("SeguroDeGanadoEnCentro", resumenInfo.SeguroDeGanadoEnCentro),
                                    new XElement("MedicamentoEnPradera", resumenInfo.MedicamentoEnPradera),
                                    new XElement("AlimentacionEnCadis", resumenInfo.AlimentacionEnCadis),
                                    new XElement("MedicamentoEnCadis", resumenInfo.MedicamentoEnCadis),
                                    new XElement("GastosMateriasPrimas", resumenInfo.GastosMateriasPrimas),

                                    new XElement("GastosFinancieros", resumenInfo.GastosFinancieros),
                                    new XElement("GastosPraderasFijos", resumenInfo.GastosPraderasFijos),
                                    new XElement("SeguroAltaMortandadPradera", resumenInfo.SeguroAltaMortandadPradera),
                                    new XElement("AlimentoEnDescanso", resumenInfo.AlimentoEnDescanso),
                                    new XElement("MedicamentoEnDescanso", resumenInfo.MedicamentoEnDescanso),
                                    new XElement("ManejoDeGanado", resumenInfo.ManejoDeGanado),
                                    new XElement("CostoDePradera", resumenInfo.CostoDePradera),
                                    new XElement("Demoras", resumenInfo.Demoras),
                                    new XElement("Maniobras", resumenInfo.Maniobras)

                                )
                            );
                var parametros =
                        new Dictionary<string, object>
                                {{"@ResumenXML", xml.ToString()},
                                 {"@OrganizacionID", organizacionId}};

                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo pra obtener los parametros y almacenar los animales de control individual en SIAP
        /// ademas de crear las cargas iniciales
        /// </summary>
        /// <param name="controlIndividualInfo"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCargaInicialSIAP(
            ControlIndividualInfo controlIndividualInfo, int organizacionId)
        {
            try
            {
                Logger.Info();
                var ctrManiInfoXml =
                    new XElement("ROOT",
                        from ctrManiInfo in controlIndividualInfo.ListaCtrManiInfo
                        select
                            new XElement("CtrManiInfo",
                                new XElement("Arete", ctrManiInfo.Arete),
                                new XElement("FechaComp", ctrManiInfo.FechaComp),
                                new XElement("FechaCorte", ctrManiInfo.FechaCorte),
                                new XElement("NumCorr", ctrManiInfo.NumCorr),
                                new XElement("CalEng", ctrManiInfo.CalEng),
                                new XElement("PesoCorte", ctrManiInfo.PesoCorte),
                                new XElement("Paletas", ctrManiInfo.Paletas),
                                new XElement("TipoGan", ctrManiInfo.TipoGan),
                                new XElement("Temperatura", ctrManiInfo.Temperatura)
                                )
                        );
                var ctrReimInfoXml =
                    new XElement("ROOT",
                        from ctrReimInfo in controlIndividualInfo.ListaCtrReimInfo
                        select
                            new XElement("CtrReimInfo",
                                new XElement("Arete", ctrReimInfo.Arete),
                                new XElement("FechaComp", ctrReimInfo.FechaComp),
                                new XElement("FechaReim", ctrReimInfo.FechaReim),
                                new XElement("PesoReimp", ctrReimInfo.PesoReimp)
                                )
                        );
                var parametros =
                    new Dictionary<string, object>
                    {
                        {"@CtrManiXML", ctrManiInfoXml.ToString()},
                        {"@CtrReimXML", ctrReimInfoXml.ToString()},
                        {"@OrganizacionID", organizacionId}
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
