using System.Collections.Generic;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using System;
using System.Reflection;
using SIE.Services.Info.Enums;
namespace SIE.Services.Servicios.PL
{
    public class PolizaPL
    {
        /// <summary>
        /// Guarda la Poliza de Entrada
        /// </summary>
        /// <param name="polizaEntradaGanadoInfo"></param>
        /// <param name="tipoPoliza"></param>
        public  void GuardarPolizaEntradaGanado(IList<PolizaInfo> polizaEntradaGanadoInfo, TipoPoliza tipoPoliza)
        {
            try
            {
                Logger.Info();
                var polizaBL = new PolizaBL();
                polizaBL.Guardar(polizaEntradaGanadoInfo, tipoPoliza);
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
        public Queue<IList<PolizaInfo>> ObtenerPolizasPendientes()
        {
            try
            {
                Logger.Info();
                var polizaBL = new PolizaBL();
                return polizaBL.ObtenerPolizasPendientes();
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
        public void ActualizaArchivoEnviadoSAP(int polizaID)
        {
            try
            {
                Logger.Info();
                var polizaBL = new PolizaBL();
                polizaBL.ActualizaArchivoEnviadoSAP(polizaID);
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
        public IList<PolizaInfo> ObtenerPoliza(TipoPoliza tipoPoliza, int organizacionID, DateTime fecha
                                               , string clave, string concepto, long estatus)
        {
            try
            {
                Logger.Info();
                var polizaBL = new PolizaBL();
                return polizaBL.ObtenerPoliza(tipoPoliza, organizacionID, fecha, clave, concepto, estatus);
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
        public IList<PolizaInfo> ObtenerPorID(int polizaID)
        {
            try
            {
                Logger.Info();
                var polizaBL = new PolizaBL();
                return polizaBL.ObtenerPorID(polizaID);
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
        public IEnumerable<PolizaInfo> ObtenerPolizasConciliacion(int organizacionID, DateTime fechaInicio, DateTime fechaFin
                                                                , string claseDocumento)
        {
            try
            {
                Logger.Info();
                var polizaBL = new PolizaBL();
                return polizaBL.ObtenerPolizasConciliacion(organizacionID, fechaInicio, fechaFin, claseDocumento);
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
        public void GuardarConciliacion(List<PolizaInfo> polizasReenviar)
        {
            try
            {
                Logger.Info();
                var polizaBL = new PolizaBL();
                polizaBL.GuardarConciliacion(polizasReenviar);
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
        public void GuardarConciliacion(List<PolizaInfo> polizasReenviar, List<PolizaInfo> polizasCancelar)
        {
            try
            {
                Logger.Info();
                var polizaBL = new PolizaBL();
                polizaBL.GuardarConciliacion(polizasReenviar, polizasCancelar);
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
        public IList<PolizaInfo> ObtenerPolizaConDocumentoSAP(TipoPoliza tipoPoliza, int organizacionID, DateTime fecha)
        {
            IList<PolizaInfo> polizas;
            try
            {
                Logger.Info();
                var polizaBL = new PolizaBL();
                polizas = polizaBL.ObtenerPolizaConDocumentoSAP(tipoPoliza, organizacionID, fecha);
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
        public void DesactivarPolizas(List<PolizaInfo> polizasCancelar)
        {
            try
            {
                Logger.Info();
                var polizaBL = new PolizaBL();
                polizaBL.DesactivarPolizas(polizasCancelar);
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
        public IList<PolizaInfo> ObtenerPolizaConDocumentoSAPPorRango(TipoPoliza tipoPoliza, int organizacionID, DateTime fecha)
        {
            IList<PolizaInfo> polizas;
            try
            {
                Logger.Info();
                var polizaBL = new PolizaBL();
                polizas = polizaBL.ObtenerPolizaConDocumentoSAPPorRango(tipoPoliza, organizacionID, fecha);
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
        public void ActualizaProcesadoSAP(RespuestaServicioPI respuestaServicioPI)
        {
            try
            {
                Logger.Info();
                var polizaBL = new PolizaBL();
                polizaBL.ActualizaProcesadoSAP(respuestaServicioPI);
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
        public void ActualizaMensajeSAP(RespuestaServicioPI respuestaServicioPI)
        {
            try
            {
                Logger.Info();
                var polizaBL = new PolizaBL();
                polizaBL.ActualizaMensajeSAP(respuestaServicioPI);
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
        /// Guarda la Poliza de Entrada
        /// </summary>
        /// <param name="polizaEntradaGanadoInfo"></param>
        /// <param name="tipoPoliza"></param>
        public void GuardarPolizaServicioPI(IList<PolizaInfo> polizaEntradaGanadoInfo, TipoPoliza tipoPoliza)
        {
            try
            {
                Logger.Info();
                var polizaBL = new PolizaBL();
                polizaBL.GuardarServicioPI(polizaEntradaGanadoInfo, tipoPoliza);
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
        public Queue<IList<PolizaInfo>> ObtenerPolizasCanceladas()
        {
            try
            {
                Logger.Info();
                var polizaBL = new PolizaBL();
                return polizaBL.ObtenerPolizasCanceladas();
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
        public void Actualizar(RespuestaServicioPI respuestaServicioPI)
        {
            try
            {
                Logger.Info();
                var polizaBL = new PolizaBL();
                polizaBL.Actualizar(respuestaServicioPI);
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
        public void ActualizaCanceladoSAP(List<int> polizasID)
        {
            try
            {
                Logger.Info();
                var polizaBL = new PolizaBL();
                polizaBL.ActualizaCanceladoSAP(polizasID);
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

        public List<int> ObtenerPolizasMal()
        {
            try
            {
                Logger.Info();
                var polizaBL = new PolizaBL();
                List<int> polizasMal = polizaBL.ObtenerPolizasMal();
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
        public List<PolizaInfo> ObtenerPolizasConciliacionSapSiap(ConciliaciionParametros conciliaciionParametros)
        {
            try
            {
                Logger.Info();
                var polizaBl = new PolizaBL();
                return polizaBl.ObtenerPolizasConciliacionSapSiap(conciliaciionParametros);
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

        public List<TiposCuentaConciliacionInfo> ObtenerTiposCuentaConciliacion()
        {
            try
            {
                Logger.Info();
                var polizaBl = new PolizaBL();
                return polizaBl.ObtenerTipoCuentaConciliacion();
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
