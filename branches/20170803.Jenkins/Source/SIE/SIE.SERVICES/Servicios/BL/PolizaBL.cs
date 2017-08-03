using System.Collections.Generic;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using System;
using System.Reflection;
using SIE.Services.Info.Enums;
using System.Linq;
using System.Text;
using System.Threading;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Servicios.BL
{
    internal class PolizaBL
    {
        /// <summary>
        /// Guarda los datos de la poliza
        /// </summary>
        /// <param name="polizaEntradaGanadoInfo"></param>
        /// <param name="tipoPoliza"></param>
        internal void Guardar(IList<PolizaInfo> polizaEntradaGanadoInfo, TipoPoliza tipoPoliza)
        {
            try
            {
                Logger.Info();
                var polizaDAL = new PolizaDAL();

                var ref3 = polizaEntradaGanadoInfo.Select(refeferencia => refeferencia.Referencia3).Distinct().ToList();
                List<PolizaInfo> polizasRef3;
                for (var indexRef3 = 0; indexRef3 < ref3.Count; indexRef3++)
                {
                    polizasRef3 =
                        polizaEntradaGanadoInfo.Where(referencia => referencia.Referencia3.Equals(ref3[indexRef3])).
                            ToList();
                    ValidarImportes(polizasRef3);
                }
                polizaDAL.CrearServicioPI(polizaEntradaGanadoInfo, tipoPoliza);
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
        /// Obtiene los datos de la poliza
        /// </summary>
        /// <returns></returns>
        internal IList<PolizaInfo> ObtenerPoliza(TipoPoliza tipoPoliza, int organizacionID, DateTime fecha
                                               , string clave, string concepto, long estatus)
        {
            try
            {
                Logger.Info();
                var polizaDAL = new PolizaDAL();
                return polizaDAL.ObtenerPoliza(tipoPoliza, organizacionID, fecha, clave, concepto, estatus);
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
        /// Obtiene un listado de las Polizas que estan pendientes
        /// por ser enviadas a SAP
        /// </summary>
        /// <returns></returns>
        internal Queue<IList<PolizaInfo>> ObtenerPolizasPendientes()
        {
            try
            {
                Logger.Info();
                var polizaDAL = new PolizaDAL();
                return polizaDAL.ObtenerPolizasPendientes();
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
        /// Actualiza bandera para indicar si el archivo fue
        /// grabado en el servidor de SAP ó no
        /// </summary>
        /// <returns></returns>
        internal void ActualizaArchivoEnviadoSAP(int polizaID)
        {
            try
            {
                Logger.Info();
                var polizaDAL = new PolizaDAL();
                polizaDAL.ActualizaArchivoEnviadoSAP(polizaID);
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

        internal IList<PolizaInfo> ObtenerPorID(int polizaID)
        {
            try
            {
                Logger.Info();
                var polizaDAL = new PolizaDAL();
                return polizaDAL.ObtenerPoliza(polizaID);
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
        /// Obtiene las polizas para su conciliacion
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <param name="claseDocumento"> </param>
        /// <returns></returns>
        internal IEnumerable<PolizaInfo> ObtenerPolizasConciliacion(int organizacionID, DateTime fechaInicio
                                                                  , DateTime fechaFin, string claseDocumento)
        {
            try
            {
                Logger.Info();
                var polizaDAL = new PolizaDAL();
                return polizaDAL.ObtenerPolizasConciliacion(organizacionID, fechaInicio, fechaFin, claseDocumento);
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
        /// Genera la conciliacion de las polizas faltantes en SAP
        /// </summary>
        /// <param name="polizasReenviar"></param>
        internal void GuardarConciliacion(List<PolizaInfo> polizasReenviar)
        {
            try
            {
                Logger.Info();
                var polizaDAL = new PolizaDAL();
                using (var scope = new TransactionScope())
                {
                    List<PolizaInfo> polizasGuardar;
                    List<string> referencias = polizasReenviar.Select(ref3 => ref3.Referencia3).Distinct().ToList();
                    int milisegundo;
                    for (var indexReferencias = 0; indexReferencias < referencias.Count; indexReferencias++)
                    {
                        polizasGuardar =
                            polizasReenviar.Where(ref3 => ref3.Referencia3.Equals(referencias[indexReferencias])).ToList
                                ();
                        milisegundo = DateTime.Now.Millisecond;
                        polizasGuardar.ForEach(datos =>
                                                    {
                                                        var ref3 = new StringBuilder();
                                                        ref3.Append("03");
                                                        ref3.Append(datos.TextoAsignado.PadLeft(10, ' '));
                                                        ref3.Append(new Random(10).Next(10, 20));
                                                        ref3.Append(new Random(30).Next(30, 40));
                                                        ref3.Append(milisegundo);
                                                        ref3.Append(datos.ClaseDocumento);
                                                        datos.Referencia3 = ref3.ToString();
                                                        if (!string.IsNullOrWhiteSpace(datos.Proveedor) 
                                                            && (datos.Proveedor.StartsWith("1126") 
                                                            || datos.Proveedor.StartsWith("1127")))
                                                        {
                                                            datos.Cuenta = datos.Proveedor;
                                                            datos.Proveedor = string.Empty;
                                                        }
                                                    });
                        polizasGuardar.ForEach(x => x.NumeroReferencia = string.Format("{0}{1}{2}",
                                                                                       DateTime.Now.Hour,
                                                                                       DateTime.Now.Minute,
                                                                                       milisegundo));
                        ValidarImportes(polizasGuardar);
                        Thread.Sleep(20);
                        polizaDAL.CrearServicioPI(polizasGuardar,
                                        (TipoPoliza)polizasGuardar.Select(x => x.TipoPolizaID).FirstOrDefault());
                    }
                    scope.Complete();
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
        /// Genera la conciliacion de las polizas faltantes en SAP
        /// </summary>
        /// <param name="polizasReenviar"></param>
        /// <param name="polizasCancelar"> </param>
        internal void GuardarConciliacion(List<PolizaInfo> polizasReenviar, List<PolizaInfo> polizasCancelar)
        {
            try
            {
                Logger.Info();
                var polizaDAL = new PolizaDAL();
                using (var scope = new TransactionScope())
                {
                    int usuarioModificacionID =
                        polizasReenviar.Select(usuario => usuario.UsuarioCreacionID).FirstOrDefault();
                    polizasCancelar.ForEach(usuario => usuario.UsuarioModificacionID = usuarioModificacionID);
                    if (polizasCancelar.Any())
                    {
                        polizaDAL.DesactivarPolizas(polizasCancelar);
                    }

                    List<PolizaInfo> polizasGuardar;
                    List<string> referencias = polizasReenviar.Select(ref3 => ref3.Referencia3).Distinct().ToList();
                    int milisegundo;
                    for (var indexReferencias = 0; indexReferencias < referencias.Count; indexReferencias++)
                    {
                        polizasGuardar =
                            polizasReenviar.Where(ref3 => ref3.Referencia3.Equals(referencias[indexReferencias])).ToList
                                ();
                        milisegundo = DateTime.Now.Millisecond;
                        polizasGuardar.ForEach(datos =>
                        {
                            var ref3 = new StringBuilder();
                            ref3.Append("03");
                            ref3.Append(
                                (string.IsNullOrWhiteSpace(datos.TextoAsignado) ? string.Empty : datos.TextoAsignado).
                                    PadLeft(10, ' '));
                            ref3.Append(new Random(10).Next(10, 20));
                            ref3.Append(new Random(30).Next(30, 40));
                            ref3.Append(milisegundo);
                            ref3.Append(datos.ClaseDocumento);
                            datos.Referencia3 = ref3.ToString();
                            if (!string.IsNullOrWhiteSpace(datos.Proveedor)
                                && (datos.Proveedor.StartsWith("1126")
                                || datos.Proveedor.StartsWith("1127")))
                            {
                                datos.Cuenta = datos.Proveedor;
                                datos.Proveedor = string.Empty;
                            }
                            datos.Conciliada = true;
                            if (string.IsNullOrWhiteSpace(datos.Segmento))
                            {
                                datos.Segmento = string.Format("S{0}", datos.Sociedad);
                            }
                        });
                        polizasGuardar.ForEach(x => x.NumeroReferencia = string.Format("{0}{1}{2}",
                                                                                       DateTime.Now.Hour,
                                                                                       DateTime.Now.Minute,
                                                                                       milisegundo));
                        ValidarImportes(polizasGuardar);
                        Thread.Sleep(20);
                        polizaDAL.CrearServicioPI(polizasGuardar,
                                        (TipoPoliza)polizasGuardar.Select(x => x.TipoPolizaID).FirstOrDefault());
                    }
                    scope.Complete();
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
        /// Valida los importes de la poliza
        /// </summary>
        /// <param name="polizasGuardar"></param>
        private void ValidarImportes(List<PolizaInfo> polizasGuardar)
        {
            decimal cargo = polizasGuardar.Where(x => !x.Importe.StartsWith("-")).Sum(x => Convert.ToDecimal(x.Importe));
            decimal abono = polizasGuardar.Where(x => x.Importe.StartsWith("-")).Sum(x => Convert.ToDecimal(x.Importe));
            decimal diferencia = Math.Abs(cargo) - Math.Abs(abono);
            PolizaInfo diferenciaPoliza;
            List<PolizaInfo> polizasCargos = polizasGuardar.Where(x => !x.Importe.StartsWith("-")).ToList();
            List<PolizaInfo> polizasAbonos = polizasGuardar.Where(x => x.Importe.StartsWith("-")).ToList();
            if (diferencia < 0)
            {
                diferenciaPoliza = (from pc in polizasCargos
                                    from pa in polizasAbonos
                                    where pc.Importe != pa.Importe.Replace("-", string.Empty)
                                    select pc).OrderByDescending(linea => linea.NumeroLinea).FirstOrDefault();
                if (diferenciaPoliza != null)
                {
                    diferenciaPoliza.Importe =
                        (Convert.ToDecimal(diferenciaPoliza.Importe) + Math.Abs(diferencia)).ToString("F2");
                }
            }
            if (diferencia > 0)
            {
                diferenciaPoliza = (from pc in polizasCargos
                                    from pa in polizasAbonos
                                    where pc.Importe != pa.Importe.Replace("-", string.Empty)
                                    select pa).OrderByDescending(linea => linea.NumeroLinea).FirstOrDefault();
                if (diferenciaPoliza != null)
                {
                    diferenciaPoliza.Importe =
                        (Convert.ToDecimal(diferenciaPoliza.Importe) - diferencia).ToString("F2");
                }
            }
        }

        /// <summary>
        /// Obtiene los datos de la poliza
        /// </summary>
        /// <returns></returns>
        internal IList<PolizaInfo> ObtenerPolizaConDocumentoSAP(TipoPoliza tipoPoliza, int organizacionID, DateTime fecha)
        {
            IList<PolizaInfo> polizas;
            try
            {
                Logger.Info();
                var polizaDAL = new PolizaDAL();
                polizas = polizaDAL.ObtenerPolizaConDocumentoSAP(tipoPoliza, organizacionID, fecha);
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
            return polizas;
        }

        /// <summary>
        /// Obtiene los datos de la poliza
        /// </summary>
        /// <returns></returns>
        internal IList<PolizaInfo> ObtenerPolizaConDocumentoSAPPendientes(TipoPoliza tipoPoliza, int organizacionID, DateTime fecha)
        {
            IList<PolizaInfo> polizas;
            try
            {
                Logger.Info();
                var polizaDAL = new PolizaDAL();
                polizas = polizaDAL.ObtenerPolizaConDocumentoSAPPendientes(tipoPoliza, organizacionID, fecha);
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
            return polizas;
        }

        /// <summary>
        /// Actualiza bandera para indicar si el archivo fue
        /// grabado en el servidor de SAP ó no
        /// </summary>
        /// <returns></returns>
        internal void DesactivarPolizas(List<PolizaInfo> polizasCancelar)
        {
            try
            {
                Logger.Info();
                var polizaDAL = new PolizaDAL();
                polizaDAL.DesactivarPolizas(polizasCancelar);
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
        /// Obtiene los datos de la poliza
        /// </summary>
        /// <returns></returns>
        internal IList<PolizaInfo> ObtenerPolizaConDocumentoSAPPorRango(TipoPoliza tipoPoliza, int organizacionID, DateTime fecha)
        {
            IList<PolizaInfo> polizas;
            try
            {
                Logger.Info();
                var polizaDAL = new PolizaDAL();
                polizas = polizaDAL.ObtenerPolizaConDocumentoSAPPorRango(tipoPoliza, organizacionID, fecha);
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
            return polizas;
        }

        /// <summary>
        /// Actualiza bandera para indicar si el archivo fue
        /// enviado a SAP
        /// </summary>
        /// <returns></returns>
        internal void ActualizaProcesadoSAP(RespuestaServicioPI respuestaServicioPI)
        {
            try
            {
                Logger.Info();
                var polizaDAL = new PolizaDAL();
                polizaDAL.ActualizaProcesadoSAP(respuestaServicioPI);
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
        /// Guarda el mensaje de error por el cual no
        /// se pudo ser procesada la poliza por el
        /// servicio de PI
        /// </summary>
        /// <returns></returns>
        internal void ActualizaMensajeSAP(RespuestaServicioPI respuestaServicioPI)
        {
            try
            {
                Logger.Info();
                var polizaDAL = new PolizaDAL();
                polizaDAL.ActualizaMensajeSAP(respuestaServicioPI);
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
        /// Guarda los datos de la poliza
        /// </summary>
        /// <param name="polizaEntradaGanadoInfo"></param>
        /// <param name="tipoPoliza"></param>
        internal void GuardarServicioPI(IList<PolizaInfo> polizaEntradaGanadoInfo, TipoPoliza tipoPoliza)
        {
            try
            {
                Logger.Info();
                var polizaDAL = new PolizaDAL();

                var ref3 = polizaEntradaGanadoInfo.Select(refeferencia => refeferencia.Referencia3).Distinct().ToList();
                List<PolizaInfo> polizasRef3;
                for (var indexRef3 = 0; indexRef3 < ref3.Count; indexRef3++)
                {
                    polizasRef3 =
                        polizaEntradaGanadoInfo.Where(referencia => referencia.Referencia3.Equals(ref3[indexRef3])).
                            ToList();
                    ValidarImportes(polizasRef3);
                }
                polizaDAL.CrearServicioPI(polizaEntradaGanadoInfo, tipoPoliza);
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
        /// Activa bandera para indicar que la poliza
        /// debe ser cancelada
        /// </summary>
        /// <param name="polizasID"></param>
        internal void ActualizaCanceladoSAP(List<int> polizasID)
        {
            try
            {
                Logger.Info();
                var polizaDAL = new PolizaDAL();
                polizaDAL.ActualizaCanceladoSAP(polizasID);
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
        /// Obtiene un listado de las Polizas que estan canceladas
        /// por ser enviadas a SAP
        /// </summary>
        /// <returns></returns>
        internal Queue<IList<PolizaInfo>> ObtenerPolizasCanceladas()
        {
            try
            {
                Logger.Info();
                var polizaDAL = new PolizaDAL();
                return polizaDAL.ObtenerPolizasCanceladas();
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
        /// Actualiza datos de poliza
        /// </summary>
        /// <param name="respuestaServicioPI"></param>
        internal void Actualizar(RespuestaServicioPI respuestaServicioPI)
        {
            try
            {
                Logger.Info();
                var polizaDAL = new PolizaDAL();
                polizaDAL.Actualizar(respuestaServicioPI);
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

        internal List<int> ObtenerPolizasMal()
        {
            try
            {
                Logger.Info();
                var polizaDAL = new PolizaDAL();
                List<int> polizasMal = polizaDAL.ObtenerPolizasMal();
                return polizasMal;
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
        /// Método que obtiene el listado de pólizas de la organización en el rango de fechas indicado.
        /// </summary>
        /// <param name="conciliaciionParametros"></param>
        internal List<PolizaInfo> ObtenerPolizasConciliacionSapSiap(ConciliaciionParametros conciliaciionParametros)
        {
            try
            {
                Logger.Info();
                var polizaDal = new PolizaDAL();
                return polizaDal.ObtenerPolizasConciliacionSapSiap(conciliaciionParametros);
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

        internal List<TiposCuentaConciliacionInfo> ObtenerTipoCuentaConciliacion()
        {
            try
            {
                Logger.Info();
                var polizaDal = new PolizaDAL();
                return polizaDal.obtenerTiposCuentas();
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
    }
}
