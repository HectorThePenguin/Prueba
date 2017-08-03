using System;
using System.IO;
using System.Linq;
using System.Transactions;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;
using SIE.Services.Polizas.Fabrica;
using System.Collections.Generic;
using SIE.Services.Polizas;
using SIE.Services.ServicioPolizasLogin;
using SIE.Services.ServicioPolizasCancelacion;

namespace SIE.Services.Servicios.BL
{
    internal class EntradaGanadoCosteoBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una Entrada Ganado Costeo
        /// </summary>
        /// <param name="contenedorCosteoEntradaGanado"></param>
        internal MemoryStream Guardar(ContenedorCosteoEntradaGanadoInfo contenedorCosteoEntradaGanado)
        {
            try
            {
                MemoryStream stream;
                Logger.Info();
                var entradaGanadoCosteoDAL = new EntradaGanadoCosteoDAL();

                PolizaAbstract poliza;
                IList<PolizaInfo> polizasEntrada;
                using (var transaction = new TransactionScope())
                {
                    var costeoEntradaId = contenedorCosteoEntradaGanado.EntradaGanadoCosteo.EntradaGanadoCosteoID;
                    if (costeoEntradaId == 0)
                    {
                        costeoEntradaId = entradaGanadoCosteoDAL.Crear(contenedorCosteoEntradaGanado.EntradaGanadoCosteo);
                    }
                    else
                    {
                        entradaGanadoCosteoDAL.Actualizar(contenedorCosteoEntradaGanado.EntradaGanadoCosteo);
                    }

                    if (contenedorCosteoEntradaGanado.EntradaGanadoCosteo.ListaEntradaDetalle.Any())
                    {
                        entradaGanadoCosteoDAL.GuardarEntradaDetalle(
                            contenedorCosteoEntradaGanado.EntradaGanadoCosteo.ListaEntradaDetalle, costeoEntradaId);
                    }

                    if (contenedorCosteoEntradaGanado.EntradaGanadoCosteo.ListaCostoEntrada.Any())
                    {
                        entradaGanadoCosteoDAL.GuardarCostoEntrada(
                            contenedorCosteoEntradaGanado.EntradaGanadoCosteo.ListaCostoEntrada, costeoEntradaId);
                    }

                    int cabezasTransito = 0;
                    if (contenedorCosteoEntradaGanado.EntradaGanadoTransito != null
                        && contenedorCosteoEntradaGanado.EntradaGanadoTransito.Cabezas != 0)
                    {
                        LoteInfo loteInfo = contenedorCosteoEntradaGanado.EntradaGanadoTransito.Lote;
                        cabezasTransito = contenedorCosteoEntradaGanado.EntradaGanadoTransito.Cabezas;
                        loteInfo.Cabezas = cabezasTransito;
                        loteInfo.CabezasInicio = cabezasTransito;

                        var loteBL = new LoteBL();
                        var entradaGanadoTransitoBL = new EntradaGanadoTransitoBL();
                        if (contenedorCosteoEntradaGanado.EntradaGanadoTransito.EntradaGanadoTransitoID == 0)
                        {
                            var tipoOrganizacionBL = new TipoOrganizacionBL();
                            TipoOrganizacionInfo tipoOrganizacion =
                                tipoOrganizacionBL.ObtenerPorID(contenedorCosteoEntradaGanado.EntradaGanado.TipoOrigen);
                            loteInfo.TipoProcesoID = tipoOrganizacion.TipoProceso.TipoProcesoID;
                            loteInfo.OrganizacionID = contenedorCosteoEntradaGanado.EntradaGanado.OrganizacionID;
                            loteInfo.UsuarioCreacionID = contenedorCosteoEntradaGanado.EntradaGanadoTransito.UsuarioCreacionID;
                            loteInfo.CorralID = loteInfo.Corral.CorralID;
                            loteInfo.TipoCorralID = loteInfo.Corral.TipoCorral.TipoCorralID;
                            loteInfo.Activo = EstatusEnum.Activo;
                            loteInfo.DisponibilidadManual = false;

                            int loteID = loteBL.GuardaLote(contenedorCosteoEntradaGanado.EntradaGanadoTransito.Lote);
                            contenedorCosteoEntradaGanado.EntradaGanadoTransito.Lote.LoteID = loteID;
                        }
                        else
                        {
                            loteBL.AcutalizaCabezasLote(contenedorCosteoEntradaGanado.EntradaGanadoTransito.Lote);
                        }
                        entradaGanadoTransitoBL.Guardar(contenedorCosteoEntradaGanado.EntradaGanadoTransito);
                    }

                    var tipoPoliza = TipoPoliza.EntradaGanado;
                    var entradaGanadoDAL = new EntradaGanadoDAL();
                    entradaGanadoDAL.ActualizaEntradaGanadoCosteo(contenedorCosteoEntradaGanado.EntradaGanado);
                    if (contenedorCosteoEntradaGanado.EntradaGanado.TipoOrigen == TipoOrganizacion.Ganadera.GetHashCode())
                    {
                        poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.EntradaGanadoDurango);
                        tipoPoliza = TipoPoliza.EntradaGanadoDurango;
                    }
                    else
                    {
                        poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.EntradaGanado);
                    }
                    poliza.Cancelacion = contenedorCosteoEntradaGanado.EntradaGanadoCosteo.Activo == EstatusEnum.Inactivo;

                    if (contenedorCosteoEntradaGanado.EntradaGanadoCosteo.Activo == EstatusEnum.Activo &&
                        contenedorCosteoEntradaGanado.EntradaGanado.TipoOrigen == TipoOrganizacion.CompraDirecta.GetHashCode())
                    {
                        int cabezasOrigen =
                            contenedorCosteoEntradaGanado.EntradaGanadoCosteo.ListaEntradaDetalle.Sum(cab => cab.Cabezas);
                        if (contenedorCosteoEntradaGanado.EntradaGanadoTransito != null &&
                            contenedorCosteoEntradaGanado.EntradaGanadoTransito.Sobrante)
                        {
                            cabezasOrigen -= cabezasTransito;
                        }
                        else
                        {
                            cabezasOrigen += cabezasTransito;
                        }
                        entradaGanadoDAL.ActualizaCabezasOrigen(
                            contenedorCosteoEntradaGanado.EntradaGanado.EntradaGanadoID, cabezasOrigen);
                    }
                    else
                    {
                        if (contenedorCosteoEntradaGanado.EntradaGanado.CabezasOrigen == 0)
                        {
                            int cabezasOrigen =
                            contenedorCosteoEntradaGanado.EntradaGanadoCosteo.ListaEntradaDetalle.Sum(cab => cab.Cabezas);

                            entradaGanadoDAL.ActualizaCabezasOrigen(
                            contenedorCosteoEntradaGanado.EntradaGanado.EntradaGanadoID, cabezasOrigen);
                        }
                    }
                    entradaGanadoDAL.ActualizaEntradaGanadoCosteo(contenedorCosteoEntradaGanado.EntradaGanado);

                    polizasEntrada = poliza.GeneraPoliza(contenedorCosteoEntradaGanado);
                    polizasEntrada.ToList().ForEach(
                        org =>
                        org.OrganizacionID =
                        contenedorCosteoEntradaGanado.EntradaGanadoCosteo.Organizacion.OrganizacionID);
                    if (poliza.Cancelacion)
                    {
                        polizasEntrada.ToList().ForEach(
                            usuario =>
                            usuario.UsuarioCreacionID =
                            Convert.ToInt32(contenedorCosteoEntradaGanado.EntradaGanadoCosteo.UsuarioModificacionID));
                    }
                    else
                    {
                        polizasEntrada.ToList().ForEach(
                            usuario =>
                            usuario.UsuarioCreacionID =
                            contenedorCosteoEntradaGanado.EntradaGanadoCosteo.UsuarioCreacionID);
                    }
                    polizasEntrada.ToList().ForEach(
                        act => act.Activo = EstatusEnum.Activo);
                    polizasEntrada.ToList().ForEach(env => env.ArchivoEnviadoServidor = 1);

                    var polizaBL = new PolizaBL();
                    if (poliza.Cancelacion)
                    {
                        int organizacionID = contenedorCosteoEntradaGanado.EntradaGanado.OrganizacionID;
                        DateTime fechaEntrada = contenedorCosteoEntradaGanado.EntradaGanado.FechaEntrada;
                        int folioEntrada = contenedorCosteoEntradaGanado.EntradaGanado.FolioEntrada;
                        string folio = contenedorCosteoEntradaGanado.EntradaGanado.FolioEntrada.ToString();
                        IList<PolizaInfo> polizasPorCancelar = polizaBL.ObtenerPoliza(tipoPoliza, organizacionID, fechaEntrada, folio, "EG", 1);
                            //polizaBL.ObtenerPolizaConDocumentoSAP(tipoPoliza, organizacionID, fechaEntrada); //polizaBL.ObtenerPoliza(tipoPoliza, organizacionID, fechaEntrada, folio, "EG", 1);
                        if (polizasPorCancelar != null
                            && polizasPorCancelar.Any())
                        {
                            List<int> polizasID = polizasPorCancelar.Where(con => con.Concepto.StartsWith(string.Format("EG-{0} ", folioEntrada)))
                                                                    .Select(id => id.PolizaID).Distinct().ToList();
                            polizaBL.ActualizaCanceladoSAP(polizasID);
                        }
                    }
                    else
                    {
                        List<string> referencias = polizasEntrada.Select(ref3 => ref3.Referencia3).Distinct().ToList();
                        List<PolizaInfo> polizasRef3;
                        for (int indexRef = 0; indexRef < referencias.Count; indexRef++)
                        {
                            polizasRef3 = polizasEntrada.Where(ref3 => ref3.Referencia3.Equals(referencias[indexRef])).ToList();
                            polizaBL.GuardarServicioPI(polizasRef3, tipoPoliza);
                        }
                    }
                    stream = poliza.ImprimePoliza(contenedorCosteoEntradaGanado, polizasEntrada);
                    if (contenedorCosteoEntradaGanado.EntradasGanadoMuertes != null
                            && contenedorCosteoEntradaGanado.EntradasGanadoMuertes.Any())
                    {
                        var entradaGanadoMuerteBL = new EntradaGanadoMuerteBL();
                        entradaGanadoMuerteBL.Actualizar(contenedorCosteoEntradaGanado.EntradasGanadoMuertes);

                        poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.PolizaMuerteTransito);
                        poliza.Cancelacion = contenedorCosteoEntradaGanado.EntradaGanadoCosteo.Activo != EstatusEnum.Activo;
                        polizasEntrada = poliza.GeneraPoliza(contenedorCosteoEntradaGanado.EntradasGanadoMuertes);
                        if (polizasEntrada != null && polizasEntrada.Any())
                        {
                            polizasEntrada.ToList().ForEach(
                                        org =>
                                        org.OrganizacionID =
                                        contenedorCosteoEntradaGanado.EntradaGanadoCosteo.Organizacion.OrganizacionID);
                            if (poliza.Cancelacion)
                            {
                                polizasEntrada.ToList().ForEach(
                                    usuario =>
                                    usuario.UsuarioCreacionID =
                                    Convert.ToInt32(contenedorCosteoEntradaGanado.EntradaGanadoCosteo.UsuarioModificacionID));
                            }
                            else
                            {
                                polizasEntrada.ToList().ForEach(
                                    usuario =>
                                    usuario.UsuarioCreacionID =
                                    contenedorCosteoEntradaGanado.EntradaGanadoCosteo.UsuarioCreacionID);
                            }
                            polizaBL.GuardarServicioPI(polizasEntrada, TipoPoliza.PolizaMuerteTransito);
                        }
                    }

                    transaction.Complete();

                    contenedorCosteoEntradaGanado.EntradaGanadoCosteo.EntradaGanadoCosteoID = costeoEntradaId;
                }
                return stream;
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
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
        ///     Obtiene una Entrada Ganado Costeo por su Id
        /// </summary>
        /// <param name="entradaGanadoCosteoID"></param>
        /// <returns></returns>
        internal EntradaGanadoCosteoInfo ObtenerPorID(int entradaGanadoCosteoID)
        {
            try
            {
                Logger.Info();
                var entradaGanadoCosteoDAL = new EntradaGanadoCosteoDAL();
                EntradaGanadoCosteoInfo result = entradaGanadoCosteoDAL.ObtenerPorID(entradaGanadoCosteoID);
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
        ///     Obtiene una Entrada Ganado Costeo por su Entrada Id
        /// </summary>
        /// <param name="entradaID"></param>
        /// <returns></returns>
        internal EntradaGanadoCosteoInfo ObtenerPorEntradaGanadoID(int entradaID)
        {
            try
            {
                Logger.Info();
                var entradaGanadoCosteoDAL = new EntradaGanadoCosteoDAL();
                EntradaGanadoCosteoInfo result = entradaGanadoCosteoDAL.ObtenerPorEntradaGanadoID(entradaID);
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
        /// Guardar la Calidad de la entrada de ganado
        /// </summary>
        /// <param name="filtroCalificacionGanadoInfo"></param>
        /// <returns></returns>
        internal void GuardarCalidadGanado(FiltroCalificacionGanadoInfo filtroCalificacionGanadoInfo)
        {
            try
            {
                Logger.Info();
                var entradaGanadoCosteoDAL = new EntradaGanadoCosteoDAL();
                var listaCalidadGuardar = new List<EntradaGanadoCalidadInfo>();
                //var calidadGanadoBL = new CalidadGanadoBL();

                //var calidades = calidadGanadoBL.ObtenerTodos(EstatusEnum.Activo);
                filtroCalificacionGanadoInfo.ListaCalidades.ForEach(cali =>
                {
                    var entradaCalidadGanado = new EntradaGanadoCalidadInfo
                    {
                        EntradaGanadoID = filtroCalificacionGanadoInfo.EntradaGanadoID,
                        CalidadGanado = new CalidadGanadoInfo
                        {
                            CalidadGanadoID = cali.CalidadID
                        },
                        Valor = cali.ValorCabezas,
                        Activo = EstatusEnum.Activo,
                        UsuarioCreacionID = filtroCalificacionGanadoInfo.UsuarioID
                    };
                    listaCalidadGuardar.Add(entradaCalidadGanado);

                    #region metodo anterior
                    //if(cali.CabezasMacho > 0)
                    //{
                    //    var calidadMacho = calidades.FirstOrDefault(calidad => calidad.Calidad.Equals(cali.Calidad.Trim()) && calidad.Sexo == Sexo.Macho);
                    //    if(calidadMacho != null)
                    //    {
                    //        var entradaCalidadGanadoMacho = new EntradaGanadoCalidadInfo
                    //            {
                    //                EntradaGanadoID = filtroCalificacionGanadoInfo.EntradaGanadoID,
                    //                CalidadGanado = new CalidadGanadoInfo
                    //                    {
                    //                        CalidadGanadoID = calidadMacho.CalidadGanadoID
                    //                    },
                    //                Valor = cali.CabezasMacho,
                    //                Activo = EstatusEnum.Activo,
                    //                UsuarioCreacionID = filtroCalificacionGanadoInfo.UsuarioID
                    //            };
                    //        listaCalidadGuardar.Add(entradaCalidadGanadoMacho);
                    //    }
                    //}
                    //if (cali.CabezasHembra > 0)
                    //{
                    //    var calidadHembra =
                    //        calidades.FirstOrDefault(calidad => calidad.Calidad.Equals(cali.Calidad.Trim()) && calidad.Sexo == Sexo.Hembra);
                    //    if (calidadHembra != null)
                    //    {
                    //        var entradaCalidadGanadoHembra = new EntradaGanadoCalidadInfo
                    //        {
                    //            EntradaGanadoID = filtroCalificacionGanadoInfo.EntradaGanadoID,
                    //            CalidadGanado = new CalidadGanadoInfo
                    //            {
                    //                CalidadGanadoID = calidadHembra.CalidadGanadoID
                    //            },
                    //            Valor = cali.CabezasHembra,
                    //            Activo = EstatusEnum.Activo,
                    //            UsuarioCreacionID = filtroCalificacionGanadoInfo.UsuarioID
                    //        };
                    //        listaCalidadGuardar.Add(entradaCalidadGanadoHembra);
                    //    }
                    //}
                    #endregion
                });
                using (var transaction = new TransactionScope())
                {
                    entradaGanadoCosteoDAL.GuardarCalidadGanado(listaCalidadGuardar);
                    if (filtroCalificacionGanadoInfo.CabezasMuertas > 0)
                    {
                        var entradaGanadoDAL = new EntradaGanadoDAL();
                        entradaGanadoDAL.ActualizarCabezasMuertas(filtroCalificacionGanadoInfo);
                    }
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
        /// Marcar el costeo de la entrada de ganado como prorrateado
        /// </summary>
        /// <param name="entradaGanadoCosteoId"></param>
        public void InactivarProrrateoaCosteo(int entradaGanadoCosteoId)
        {
            try
            {
                Logger.Info();
                var entradaGanadoCosteoDAL = new EntradaGanadoCosteoDAL();
                entradaGanadoCosteoDAL.InactivarProrrateoaCosteo(entradaGanadoCosteoId);

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
        /// Obtiene una lista de entradas ganado costeo
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        internal List<EntradaGanadoCosteoInfo> ObtenerEntradasPorFechasConciliacion(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                Logger.Info();
                var entradaGanadoCosteoDAL = new EntradaGanadoCosteoDAL();
                List<EntradaGanadoCosteoInfo> result =
                    entradaGanadoCosteoDAL.ObtenerEntradasPorFechasConciliacion(organizacionID, fechaInicial, fechaFinal);
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
        ///     Obtiene una Entrada Ganado Costeo por su Entrada Id
        /// </summary>
        /// <param name="entradaID"></param>
        /// <param name="estatus"> </param>
        /// <returns></returns>
        internal EntradaGanadoCosteoInfo ObtenerPorEntradaGanadoID(int entradaID, EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var entradaGanadoCosteoDAL = new EntradaGanadoCosteoDAL();
                EntradaGanadoCosteoInfo result = entradaGanadoCosteoDAL.ObtenerPorEntradaGanadoID(entradaID, estatus);
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
        /// Obtiene los folios que se cancelaran
        /// </summary>
        /// <param name="polizasEntrada"></param>
        /// <returns></returns>
        private List<string> ObtenerFoliosCancelar(IList<PolizaInfo> polizasEntrada)
        {
            var foliosCancelar = new List<string>();
            var conceptos = new HashSet<string>(polizasEntrada.Select(con => con.Concepto));
            if (conceptos != null)
            {
                conceptos = new HashSet<string>(conceptos.Select(con => con.Split(',')[0]));
                foliosCancelar.AddRange(conceptos);
            }
            return foliosCancelar.ToList();
        }
    }
}
