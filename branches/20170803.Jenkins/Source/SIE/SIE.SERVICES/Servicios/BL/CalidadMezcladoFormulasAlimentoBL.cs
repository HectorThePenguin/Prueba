using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Modelos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    internal class CalidadMezcladoFormulasAlimentoBL
    {
        /// <summary>
        /// Metodos para obtener los registros que se cargaran en el combobox "Analisis de Muestras"
        /// </summary>
        /// <returns></returns>
        internal IList<CalidadMezcladoFormulasAlimentoInfo> CargarComboboxAnalisis()
        {
            try
            {
                Logger.Info();
                var calMezForAliDAL = new CalidadMezcladoFormulasAlimentoDAL();
                IList<CalidadMezcladoFormulasAlimentoInfo> result = calMezForAliDAL.CargarComboboxAnalisis();
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodos para obtener los registros que se cargaran en el combobox "Tecnicas"
        /// </summary>
        /// <returns></returns>
        internal IList<CalidadMezcladoFormulasAlimentoInfo> CargarComboboxTecnica()
        {
            try
            {
                Logger.Info();
                var calMezForAliDAL = new CalidadMezcladoFormulasAlimentoDAL();
                IList<CalidadMezcladoFormulasAlimentoInfo> result = calMezForAliDAL.CargarComboboxTecnica();
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo para consultar los datos de la tabla CalidadMezcladoFactor
        /// </summary>
        /// <returns></returns>
        internal IList<CalidadMezcladoFactorInfo> ObtenerTablaFactor()
        {
            try
            {
                Logger.Info();
                var calidadMezcladoFormulasAlimentoDAL = new CalidadMezcladoFormulasAlimentoDAL();
                IList<CalidadMezcladoFactorInfo> result = calidadMezcladoFormulasAlimentoDAL.ObtenerTablaFactor();
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo para obtener los datos que se ocupan para llenar la tabla resumen del formulario "Calidad Mezclado Alimentos"
        /// </summary>
        /// <returns></returns>
        internal IList<CalidadMezcladoFormula_ResumenInfo> TraerDatosTablaResumen(int organizacionID, int formulasMuestrear)
        {
            try
            {
                Logger.Info();
                var calidadMezcladoFormulasAlimentoDAL = new CalidadMezcladoFormulasAlimentoDAL();
                IList<CalidadMezcladoFormula_ResumenInfo> result =
                    calidadMezcladoFormulasAlimentoDAL.TraerDatosTablaResumen(organizacionID, formulasMuestrear);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public void GuardarCalidadMezcladoFormulaAlimento(CalidadMezcladoFormulasAlimentoInfo resultado,
            IList<CalidadMezcladoFormulasAlimentoInfo> listaTotalRegistrosGuardar)
        {
            try
            {
                Logger.Info();
                var calidadMezcladoFormulasAlimentoDAL = new CalidadMezcladoFormulasAlimentoDAL();
                using (var transaction = new TransactionScope())
                {
                    if (resultado.CalidadMezcladoID == 0)
                    {
                        if (resultado.LugarToma == TipoLugarMuestraEnum.Corral.GetHashCode())
                        {
                            var lote = new LoteInfo
                            {
                                CorralID = resultado.CorralInfo.CorralID,
                                OrganizacionID = resultado.Organizacion.OrganizacionID
                            };
                            var loteBl = new LoteBL();
                            lote = loteBl.ObtenerPorCorralID(lote);
                            resultado.LoteID = lote.LoteID;
                        }
                        resultado =
                            calidadMezcladoFormulasAlimentoDAL.GuardarCalidadMezcladoFormulaAlimentoReparto(resultado);
                    }

                    calidadMezcladoFormulasAlimentoDAL.GuardarCalidadMezcladoFormulaAlimentoRepartoDetalle(
                        listaTotalRegistrosGuardar, resultado);
                    transaction.Complete();
                }
            }
            catch (ExcepcionGenerica)
            {
                throw;
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
        internal IList<CalidadMezcladoFormulasAlimentoInfo> CargarTablaMezcladoDetalle(
            CalidadMezcladoFormulasAlimentoInfo calidadMezcladoFormulaAlimentoInfo)
        {
            try
            {
                Logger.Info();
                var calidadMezcladoFormulasAlimentoDAL = new CalidadMezcladoFormulasAlimentoDAL();
                IList<CalidadMezcladoFormulasAlimentoInfo> result =
                    calidadMezcladoFormulasAlimentoDAL.CargarTablaMezcladoDetalle(calidadMezcladoFormulaAlimentoInfo);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo para consultar los datos de la impresion de Calidad de Mezclado
        /// </summary>
        /// <returns></returns>
        internal IList<ImpresionCalidadMezcladoModel> ObtenerImpresionCalidadMezclado(FiltroImpresionCalidadMezclado filtro)
        {
            try
            {
                Logger.Info();
                var calidadMezcladoFormulasAlimentoDAL = new CalidadMezcladoFormulasAlimentoDAL();
                IList<ImpresionCalidadMezcladoModel> result =
                    calidadMezcladoFormulasAlimentoDAL.ObtenerImpresionCalidadMezclado(filtro);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

        }

        internal void CalculosDetalle(List<ImpresionCalidadMezcladoModel> result)
        {
            try
            {
                Logger.Info();

                const string INICIAL = "Inicial";
                const string MEDIA = "Media";
                const string FINAL = "Final";

                const string NUMERO_MUESTRA1 = "M1";
                const string NUMERO_MUESTRA2 = "M2";
                const string NUMERO_MUESTRA3 = "M3";

                ImpresionCalidadMezcladoModel elemeneto = result[0];
                List<ImpresionCalidadMezcladoDetalleModel> detalle = result[0].Detalle;
                double pesoBHInicial =
                    detalle.Where(det => det.TipoMuestra.Contains(INICIAL)).Sum(peso => peso.PesoBaseHumeda);
                double pesoBHMedia =
                    detalle.Where(det => det.TipoMuestra.Contains(MEDIA)).Sum(peso => peso.PesoBaseHumeda);
                double pesoBHFinal =
                    detalle.Where(det => det.TipoMuestra.Contains(FINAL)).Sum(peso => peso.PesoBaseHumeda);

                elemeneto.PesoBHInicial = pesoBHInicial;
                elemeneto.PesoBHMedia = pesoBHMedia;
                elemeneto.PesoBHFinal = pesoBHFinal;

                elemeneto.PromedioPesoBh = (pesoBHInicial + pesoBHMedia + pesoBHFinal)/3;

                double pesoBSSecaInicial =
                    detalle.Where(det => det.TipoMuestra.Contains(INICIAL)).Sum(peso => peso.PesoBaseSeca);
                double pesoBSSecaMedia =
                    detalle.Where(det => det.TipoMuestra.Contains(MEDIA)).Sum(peso => peso.PesoBaseSeca);
                double pesoBSSecaFinal =
                    detalle.Where(det => det.TipoMuestra.Contains(FINAL)).Sum(peso => peso.PesoBaseSeca);

                elemeneto.PesoBSInicial = pesoBSSecaInicial;
                elemeneto.PesoBSMedia = pesoBSSecaMedia;
                elemeneto.PesoBSFinal = pesoBSSecaFinal;

                elemeneto.PromedioPesoBs = (pesoBSSecaInicial + pesoBSSecaMedia + pesoBSSecaFinal)/3;

                double porcentajeSecaInicial = (pesoBSSecaInicial / pesoBHInicial) * 100;
                double porcentajeSecaMedia = (pesoBSSecaMedia / pesoBHMedia) * 100;
                double porcentajeSecaFinal = (pesoBSSecaFinal / pesoBHFinal) * 100;

                elemeneto.PorcentajeMateriaSecaInicial = porcentajeSecaInicial;
                elemeneto.PorcentajeMateriaSecaMedia = porcentajeSecaMedia;
                elemeneto.PorcentajeMateriaSecaFinal = porcentajeSecaFinal;

                double porcentajeHumedadInicial = 100 - porcentajeSecaInicial;
                elemeneto.PorcentajeHumedadInicial = porcentajeHumedadInicial;
                double porcentajeHumedadMedia = 100 - porcentajeSecaMedia;
                elemeneto.PorcentajeHumedadMedia = porcentajeHumedadMedia;
                double porcentajeHumedadFinal = 100 - porcentajeSecaFinal;
                elemeneto.PorcentajeHumedadFinal = porcentajeHumedadFinal;

                elemeneto.PromedioMateriaSeca = porcentajeSecaInicial + porcentajeSecaMedia + porcentajeSecaFinal/3;
                elemeneto.PromedioHumedad = porcentajeHumedadInicial + porcentajeHumedadMedia + porcentajeHumedadFinal/3;

                double pesoGrsM1Inicial =
                    detalle.Where(det => det.TipoMuestra.Contains(INICIAL) && det.NumeroMuestra.Equals(NUMERO_MUESTRA1))
                        .Sum(
                            peso => peso.Peso);
                double pesoGrsM1Media =
                    detalle.Where(det => det.TipoMuestra.Contains(MEDIA) && det.NumeroMuestra.Equals(NUMERO_MUESTRA1)).
                        Sum(peso => peso.Peso);
                double pesoGrsM1Final =
                    detalle.Where(det => det.TipoMuestra.Contains(FINAL) && det.NumeroMuestra.Equals(NUMERO_MUESTRA1)).
                        Sum(
                            peso => peso.Peso);

                elemeneto.AnalisisInicialM1 = pesoGrsM1Inicial;
                elemeneto.AnalisisInicialM2 = pesoGrsM1Media;
                elemeneto.AnalisisInicialM3 = pesoGrsM1Final;

                double pesoGrsM2Inicial =
                    detalle.Where(det => det.TipoMuestra.Contains(INICIAL) && det.NumeroMuestra.Equals(NUMERO_MUESTRA2))
                        .Sum(peso => peso.Peso);
                double pesoGrsM2Media =
                    detalle.Where(det => det.TipoMuestra.Contains(MEDIA) && det.NumeroMuestra.Equals(NUMERO_MUESTRA2)).
                        Sum(peso => peso.Peso);
                double pesoGrsM2Final =
                    detalle.Where(det => det.TipoMuestra.Contains(FINAL) && det.NumeroMuestra.Equals(NUMERO_MUESTRA2)).
                        Sum(peso => peso.Peso);

                elemeneto.AnalisisMediaM1 = pesoGrsM2Inicial;
                elemeneto.AnalisisMediaM2 = pesoGrsM2Media;
                elemeneto.AnalisisMediaM3 = pesoGrsM2Final;

                double pesoGrsM3Inicial =
                    detalle.Where(det => det.TipoMuestra.Contains(INICIAL) && det.NumeroMuestra.Equals(NUMERO_MUESTRA3))
                        .Sum(peso => peso.Peso);
                double pesoGrsM3Media =
                    detalle.Where(det => det.TipoMuestra.Contains(MEDIA) && det.NumeroMuestra.Equals(NUMERO_MUESTRA3)).
                        Sum(peso => peso.Peso);
                double pesoGrsM3Final =
                    detalle.Where(det => det.TipoMuestra.Contains(FINAL) && det.NumeroMuestra.Equals(NUMERO_MUESTRA3)).
                        Sum(peso => peso.Peso);

                elemeneto.AnalisisFinalM1 = pesoGrsM3Inicial;
                elemeneto.AnalisisFinalM2 = pesoGrsM3Media;
                elemeneto.AnalisisFinalM3 = pesoGrsM3Final;

                double factorInicialM1 = detalle.Where(
                    det => det.TipoMuestra.Contains(INICIAL) && det.NumeroMuestra.Equals(NUMERO_MUESTRA1))
                    .Sum(peso => peso.ParticulasEsperadas);
                elemeneto.ParticulasEsperadasInicialM1 = pesoGrsM1Inicial*((factorInicialM1*100)/porcentajeSecaInicial);

                double factorInicialM2 = detalle.Where(
                    det => det.TipoMuestra.Contains(INICIAL) && det.NumeroMuestra.Equals(NUMERO_MUESTRA2))
                    .Sum(peso => peso.ParticulasEsperadas);
                elemeneto.ParticulasEsperadasInicialM2 = pesoGrsM2Inicial*((factorInicialM2*100)/porcentajeSecaInicial);

                double factorInicialM3 = detalle.Where(
                    det => det.TipoMuestra.Contains(INICIAL) && det.NumeroMuestra.Equals(NUMERO_MUESTRA3))
                    .Sum(peso => peso.ParticulasEsperadas);
                elemeneto.ParticulasEsperadasInicialM3 = pesoGrsM3Inicial*((factorInicialM3*100)/porcentajeSecaInicial);

                double factorMediaM1 = detalle.Where(
                    det => det.TipoMuestra.Contains(MEDIA) && det.NumeroMuestra.Equals(NUMERO_MUESTRA1))
                    .Sum(peso => peso.ParticulasEsperadas);
                elemeneto.ParticulasEsperadasMediaM1 = pesoGrsM1Media*((factorMediaM1*100)/porcentajeSecaMedia);

                double factorMediaM2 = detalle.Where(
                    det => det.TipoMuestra.Contains(MEDIA) && det.NumeroMuestra.Equals(NUMERO_MUESTRA2))
                    .Sum(peso => peso.ParticulasEsperadas);
                elemeneto.ParticulasEsperadasMediaM2 = pesoGrsM2Media*((factorMediaM2*100)/porcentajeSecaMedia);

                double factorMediaM3 = detalle.Where(
                    det => det.TipoMuestra.Contains(MEDIA) && det.NumeroMuestra.Equals(NUMERO_MUESTRA3))
                    .Sum(peso => peso.ParticulasEsperadas);
                elemeneto.ParticulasEsperadasMediaM3 = pesoGrsM3Media*((factorMediaM3*100)/porcentajeSecaMedia);

                double factorFinalM1 = detalle.Where(
                    det => det.TipoMuestra.Contains(FINAL) && det.NumeroMuestra.Equals(NUMERO_MUESTRA1))
                    .Sum(peso => peso.ParticulasEsperadas);
                elemeneto.ParticulasEsperadasFinalM1 = pesoGrsM1Final*((factorFinalM1*100)/porcentajeSecaFinal);

                double factorFinalM2 = detalle.Where(
                    det => det.TipoMuestra.Contains(FINAL) && det.NumeroMuestra.Equals(NUMERO_MUESTRA2))
                    .Sum(peso => peso.ParticulasEsperadas);
                elemeneto.ParticulasEsperadasFinalM2 = pesoGrsM2Final*((factorFinalM2*100)/porcentajeSecaFinal);

                double factorFinalM3 = detalle.Where(
                    det => det.TipoMuestra.Contains(FINAL) && det.NumeroMuestra.Equals(NUMERO_MUESTRA3))
                    .Sum(peso => peso.ParticulasEsperadas);
                elemeneto.ParticulasEsperadasFinalM3 = pesoGrsM3Final*((factorFinalM3*100)/porcentajeSecaFinal);

                double particulasM1 = detalle.Where(
                    det => det.TipoMuestra.Contains(INICIAL) && det.NumeroMuestra.Equals(NUMERO_MUESTRA1))
                    .Sum(peso => peso.Particulas);
                elemeneto.ParticulasEncontradasInicialM1 = particulasM1;
                double particulasM2 = detalle.Where(
                    det => det.TipoMuestra.Contains(INICIAL) && det.NumeroMuestra.Equals(NUMERO_MUESTRA2))
                    .Sum(peso => peso.Particulas);
                elemeneto.ParticulasEncontradasInicialM2 = particulasM2;
                double particulasM3 = detalle.Where(
                    det => det.TipoMuestra.Contains(INICIAL) && det.NumeroMuestra.Equals(NUMERO_MUESTRA3))
                    .Sum(peso => peso.Particulas);
                elemeneto.ParticulasEncontradasInicialM3 = particulasM3;
                elemeneto.PromedioMInicial = (particulasM1 + particulasM2 + particulasM3)/3;

                particulasM1 = detalle.Where(
                    det => det.TipoMuestra.Contains(MEDIA) && det.NumeroMuestra.Equals(NUMERO_MUESTRA1))
                    .Sum(peso => peso.Particulas);
                elemeneto.ParticulasEncontradasMediaM1 = particulasM1;
                particulasM2 = detalle.Where(
                    det => det.TipoMuestra.Contains(MEDIA) && det.NumeroMuestra.Equals(NUMERO_MUESTRA2))
                    .Sum(peso => peso.Particulas);
                elemeneto.ParticulasEncontradasMediaM2 = particulasM2;
                particulasM3 = detalle.Where(
                    det => det.TipoMuestra.Contains(MEDIA) && det.NumeroMuestra.Equals(NUMERO_MUESTRA3))
                    .Sum(peso => peso.Particulas);
                elemeneto.ParticulasEncontradasMediaM3 = particulasM3;
                elemeneto.PromedioMMedia = (particulasM1 + particulasM2 + particulasM3)/3;

                particulasM1 = detalle.Where(
                    det => det.TipoMuestra.Contains(FINAL) && det.NumeroMuestra.Equals(NUMERO_MUESTRA1))
                    .Sum(peso => peso.Particulas);
                elemeneto.ParticulasEncontradasFinalM1 = particulasM1;
                particulasM2 = detalle.Where(
                    det => det.TipoMuestra.Contains(FINAL) && det.NumeroMuestra.Equals(NUMERO_MUESTRA2))
                    .Sum(peso => peso.Particulas);
                elemeneto.ParticulasEncontradasFinalM2 = particulasM2;
                particulasM3 = detalle.Where(
                    det => det.TipoMuestra.Contains(FINAL) && det.NumeroMuestra.Equals(NUMERO_MUESTRA3))
                    .Sum(peso => peso.Particulas);
                elemeneto.ParticulasEncontradasFinalM3 = particulasM3;
                elemeneto.PromedioMFinal = (particulasM1 + particulasM2 + particulasM3)/3;

                elemeneto.ParticulasEsperadasMInicial = (elemeneto.ParticulasEsperadasInicialM1 +
                                                         elemeneto.ParticulasEsperadasInicialM2 +
                                                         elemeneto.ParticulasEsperadasInicialM3) / 3;

                elemeneto.ParticulasEsperadasMMedia = (elemeneto.ParticulasEsperadasMediaM1 +
                                                       elemeneto.ParticulasEsperadasMediaM2 +
                                                       elemeneto.ParticulasEsperadasMediaM3) / 3;

                elemeneto.ParticulasEsperadasMFinal = (elemeneto.ParticulasEsperadasFinalM1 +
                                                       elemeneto.ParticulasEsperadasFinalM2 +
                                                       elemeneto.ParticulasEsperadasFinalM3) / 3;

                elemeneto.PorcentajeEficienciaRecuperacionMInicial = (elemeneto.PromedioMInicial/
                                                                      elemeneto.ParticulasEsperadasMInicial)*100;

                elemeneto.PorcentajeEficienciaRecuperacionMMedia = (elemeneto.PromedioMMedia/
                                                                    elemeneto.ParticulasEsperadasMMedia)*100;

                elemeneto.PorcentajeEficienciaRecuperacionMFinal = (elemeneto.PromedioMFinal/
                                                                    elemeneto.ParticulasEsperadasMFinal)*100;

                elemeneto.Promedio = elemeneto.PorcentajeEficienciaRecuperacionMInicial +
                                     elemeneto.PorcentajeEficienciaRecuperacionMMedia +
                                     elemeneto.PorcentajeEficienciaRecuperacionMFinal/3;

                double diferencia = elemeneto.PorcentajeEficienciaRecuperacionMInicial - elemeneto.Promedio;
                double inicial = diferencia * diferencia;
                diferencia = elemeneto.PorcentajeEficienciaRecuperacionMMedia - elemeneto.Promedio;
                double media = diferencia * diferencia;
                diferencia = elemeneto.PorcentajeEficienciaRecuperacionMFinal - elemeneto.Promedio;
                double final = diferencia * diferencia;

                elemeneto.DesviacionEstandar = Math.Sqrt((inicial + media + final)/2);
                elemeneto.CoeficienteVariacion = (elemeneto.DesviacionEstandar*100)/elemeneto.Promedio;
                elemeneto.EficanciaMezclado = 100 - elemeneto.CoeficienteVariacion;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
