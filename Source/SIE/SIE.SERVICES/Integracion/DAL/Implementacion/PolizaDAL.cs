using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class PolizaDAL : DALBase
    {
        /// <summary>
        /// Guarda los datos de la poliza
        /// </summary>
        /// <param name="polizaEntradaGanadoInfo"></param>
        /// <param name="tipoPoliza"></param>
        /// <returns>REGRESA EL POLIZAID DE LA POLIZA REGISTRADA</returns>
        internal int Crear(IList<PolizaInfo> polizaEntradaGanadoInfo, TipoPoliza tipoPoliza)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxPolizaDAL.ObtenerParametrosGuardadoPolizaEntradaGanado(polizaEntradaGanadoInfo, tipoPoliza);
                int res=Create("Poliza_Crear", parameters);
                return res;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
        internal void CrearServicioPI(IList<PolizaInfo> polizaEntradaGanadoInfo, TipoPoliza tipoPoliza)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxPolizaDAL.ObtenerParametrosGuardadoPolizaServicioPI(polizaEntradaGanadoInfo, tipoPoliza);
                Create("Poliza_Crear", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
            IList<PolizaInfo> polizas = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxPolizaDAL.ObtenerParametrosPoliza(tipoPoliza, organizacionID, fecha, clave, concepto, estatus);
                DataSet ds = Retrieve("Poliza_ObtenerXmlPoliza", parameters);
                if (ValidateDataSet(ds))
                {
                    polizas = MapPolizaDAL.ObtenerPoliza(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return polizas;
        }

        /// <summary>
        /// Obtiene un listado de las Polizas que estan pendientes
        /// por ser enviadas a SAP
        /// </summary>
        /// <returns></returns>
        internal Queue<IList<PolizaInfo>> ObtenerPolizasPendientes()
        {
            Queue<IList<PolizaInfo>> polizas = null;
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Poliza_ObtenerPendientesPorEnviarSAP");
                if (ValidateDataSet(ds))
                {
                    polizas = MapPolizaDAL.ObtenerPolizasPendientes(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
        internal void ActualizaArchivoEnviadoSAP(int polizaID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxPolizaDAL.ObtenerParametrosArchivoEnviadoSAP(polizaID);
                Update("Poliza_ActualizaArchivoEnviadoSAP", parametros);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal IList<PolizaInfo> ObtenerPoliza(int polizaID)
        {
            IList<PolizaInfo> polizas = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxPolizaDAL.ObtenerParametrosPolizaPorID(polizaID);
                DataSet ds = Retrieve("Poliza_ObtenerPorID", parameters);
                if (ValidateDataSet(ds))
                {
                    polizas = MapPolizaDAL.ObtenerPoliza(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return polizas;
        }

        /// <summary>
        /// Obtiene los movimientos para su conciliacion
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <param name="claseDocumento"> </param>
        /// <returns></returns>
        internal IEnumerable<PolizaInfo> ObtenerPolizasConciliacion(int organizacionID, DateTime fechaInicio, DateTime fechaFin
                                                                  , string claseDocumento)
        {
            try
            {
                Logger.Info();
                IMapBuilderContext<PolizaInfo> mapeo = MapPolizaDAL.ObtenerPolizasConciliacion();
                IEnumerable<PolizaInfo> foliosFaltantes = GetDatabase().ExecuteSprocAccessor(
                        "Poliza_ObtenerConciliacionPorFechas", mapeo.Build(),
                        new object[] { organizacionID, fechaInicio, fechaFin, claseDocumento });
                return foliosFaltantes;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Desactiva las polizas
        /// </summary>
        /// <param name="polizasCancelar"></param>
        internal void DesactivarPolizas(List<PolizaInfo> polizasCancelar)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxPolizaDAL.ObtenerParametrosDesactivarPolizas(polizasCancelar);
                Update("Poliza_ActualizaPolizaEstatus", parametros);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
        internal IList<PolizaInfo> ObtenerPolizaConDocumentoSAP(TipoPoliza tipoPoliza, int organizacionID, DateTime fecha)
        {
            IList<PolizaInfo> polizas = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxPolizaDAL.ObtenerParametrosPolizaConDocumentoSAP(tipoPoliza, organizacionID, fecha);
                DataSet ds = Retrieve("Poliza_ObtenerPorFechaTipoPoliza", parameters);
                if (ValidateDataSet(ds))
                {
                    polizas = MapPolizaDAL.ObtenerPolizaConDocumentoSAP(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
            IList<PolizaInfo> polizas = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxPolizaDAL.ObtenerParametrosPolizaConDocumentoSAP(tipoPoliza, organizacionID, fecha);
                DataSet ds = Retrieve("Poliza_ObtenerPorFechaTipoPolizaPendientes", parameters);
                if (ValidateDataSet(ds))
                {
                    polizas = MapPolizaDAL.ObtenerPolizaConDocumentoSAP(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
        internal IList<PolizaInfo> ObtenerPolizaConDocumentoSAPPorRango(TipoPoliza tipoPoliza, int organizacionID, DateTime fecha)
        {
            IList<PolizaInfo> polizas = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxPolizaDAL.ObtenerParametrosPolizaConDocumentoSAP(tipoPoliza, organizacionID, fecha);
                DataSet ds = Retrieve("Poliza_ObtenerPorFechaTipoPolizaRango", parameters);
                if (ValidateDataSet(ds))
                {
                    polizas = MapPolizaDAL.ObtenerPolizaConDocumentoSAP(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
                Dictionary<string, object> parametros 
                    = AuxPolizaDAL.ObtenerParametrosProcesadoSAP(respuestaServicioPI);
                Update("Poliza_ActualizaProcesadoSAP", parametros);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
                Dictionary<string, object> parametros = AuxPolizaDAL.ObtenerParametrosMensajeSAP(respuestaServicioPI);
                Update("Poliza_ActualizaMensajeSAP", parametros);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
                Dictionary<string, object> parametros = AuxPolizaDAL.ObtenerParametrosCanceladoSAP(polizasID);
                Update("Poliza_ActualizaCanceladoSAP", parametros);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
            Queue<IList<PolizaInfo>> polizas = null;
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Poliza_ObtenerPolizasPorCancelar");
                if (ValidateDataSet(ds))
                {
                    polizas = MapPolizaDAL.ObtenerPolizasCanceladas(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return polizas;
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
                Dictionary<string, object> parameters =
                    AuxPolizaDAL.ObtenerParametrosActualizarPoliza(respuestaServicioPI);
                Update("Poliza_Actualizar", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
                DataSet ds = Retrieve("PolizasCuentasMal");
                List<int> polizasMal = null;
                if (ValidateDataSet(ds))
                {
                    polizasMal = MapPolizaDAL.ObtenerPolizasCuentasMal(ds);
                }
                return polizasMal;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal List<TiposCuentaConciliacionInfo> obtenerTiposCuentas()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("[ConciliacionPolizasSiap_Sap_ObtenerTiposCuenta]");
                List<TiposCuentaConciliacionInfo> lsTiposCuenta = new List<TiposCuentaConciliacionInfo>();
                if (ValidateDataSet(ds))
                {
                    lsTiposCuenta = MapPolizaDAL.ObtenerTiposCuenta(ds);
                }
                return lsTiposCuenta;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
        /// <param name="conciliacionParametros"></param>
        internal List<PolizaInfo> ObtenerPolizasConciliacionSapSiap(ConciliaciionParametros conciliacionParametros)
        {
            try
            {
                Logger.Info();
                IMapBuilderContext<PolizaInfo> mapeo = MapPolizaDAL.ObtenerPolizasConciliacionSapSiap();
                var xmlDivisiones = AuxPolizaDAL.ObtenerParametrosObtenerPolizasConciliacionSapSiap(conciliacionParametros.diviciones);
                var foliosFaltantes = GetDatabase().ExecuteSprocAccessor(
                        "Poliza_ObtenerConciliacionSapSiapPorFechas", mapeo.Build(),
                        new object[] { conciliacionParametros.Organizacion.OrganizacionID, conciliacionParametros.fechaInicio, conciliacionParametros.fechaFin, xmlDivisiones, conciliacionParametros.Prefijo }).ToList();
                return foliosFaltantes;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
