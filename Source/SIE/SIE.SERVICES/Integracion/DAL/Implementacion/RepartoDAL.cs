using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class RepartoDAL : DALBase
    {
        /// <summary>
        /// obtiene un reparto por id
        /// </summary>
        /// <param name="reparto"></param>
        /// <returns></returns>
        public RepartoInfo ObtenerPorId(RepartoInfo reparto)
        {
            RepartoInfo result = null;
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametroPorId(reparto);
                var ds = Retrieve("Reparto_ObtenerPorID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapRepartoDAL.ObtenerPorLote(ds);
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
            return result;
        }
        /// <summary>
        /// Obteiene el reparto por lote
        /// </summary>
        /// <param name="lote">Lote</param>
        /// <param name="fechaReparto">Fecha del reparto</param>
        /// <returns></returns>
        internal RepartoInfo ObtenerPorLote(LoteInfo lote, DateTime fechaReparto)
        {
            RepartoInfo result = null;
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametroPorLote(lote, fechaReparto);
                var ds = Retrieve("Reparto_ObtenerPorLote", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapRepartoDAL.ObtenerPorLote(ds);
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
            return result;
        }

        /// <summary>
        /// Ontiene el detalle del reparto
        /// </summary>
        /// <param name="reparto">Datos del reparto</param>
        /// <returns></returns>
        internal IList<RepartoDetalleInfo> ObtenerDetalle(RepartoInfo reparto)
        {
            IList<RepartoDetalleInfo> result = null;
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametroDetallePorLote(reparto);
                var ds = Retrieve("Reparto_ObtenerDetallePorReparto", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapRepartoDAL.ObtenerDetalle(ds);
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
            return result;
        }
        /// <summary>
        /// Obtiene el detalle de todas las ordenes del dia
        /// </summary>
        /// <param name="organizacionId">Identificador de la organizacion</param>
        /// <param name="fecha">Fecha del reparto</param>
        /// <returns></returns>
        internal IList<RepartoDetalleInfo> ObtenerDetalleDelDia(int organizacionId, DateTime fecha)
        {
            IList<RepartoDetalleInfo> result = null;
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametroDetallePorDia(organizacionId, fecha);
                var ds = Retrieve("Reparto_ObtenerDetalleDia", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapRepartoDAL.ObtenerDetallePorDia(ds);
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
            return result;
        }
        /// <summary>
        /// Obtiene los dias de retiro
        /// </summary>
        /// <param name="lote">Informacion del lote</param>
        /// <returns></returns>
        internal int ObtenerDiasRetiro(LoteInfo lote)
        {
            var result = 0;
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametroDiasRetiro(lote);
                var ds = Retrieve("OrdenSacrificio_CalcularDiasRetiro", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapRepartoDAL.ObtenerDiasRetiro(ds);
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
            return result;
        }

        /// <summary>
        /// Obtiene el detalle de los reparto de un lote con los tipo de formula de produccion y finalizacion
        /// </summary>
        /// <param name="lote">Informacion del lote</param>
        /// <returns>Lista del detalle del reparto</returns>
        internal IList<RepartoDetalleInfo> ObtenerDetalleRepartoPorLote(LoteInfo lote)
        {
            IList<RepartoDetalleInfo> result = null;
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametroDetalleRepartoLoteYformulasProduccionFinalizacion(lote);
                var ds = Retrieve("Reparto_ObtenerDetalleRepartosPorLoteTipoFormulas", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapRepartoDAL.ObtenerDetalle(ds);
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
            return result;
        }

        /// <summary>
        /// Obtiene el listado de repartos del operador logueado.
        /// </summary>
        /// <param name="operador">Informacion del operador</param>
        /// <param name="corral">Informacion del corral</param>
        /// <returns></returns>
        internal RepartoInfo ObtenerRepartoPorOperadorId(OperadorInfo operador, CorralInfo corral)
        {
            RepartoInfo result = null;
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerRepartoPorOperadorId(operador, corral);
                var ds = Retrieve("Reparto_ConsultarPorOperadorId", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapRepartoDAL.ObtenerRepartoPorOperadorId(ds);
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
            return result;
        }
        /// <summary>
        /// Obtiene el total de repartos por operador id
        /// </summary>
        /// <param name="operadorId">Identificador del operador</param>
        /// <param name="organizacionId">Identificador de la organizacion</param>
        /// <returns></returns>
        internal int ObtenerTotalRepartosPorOperadorId(int operadorId, int organizacionId)
        {
            var resultado = 0;
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerTotalesRepartoPorOperadorId(operadorId, organizacionId);
                var ds = Retrieve("Reparto_ConsultarTotalPorOperadorId", parameters);
                if (ValidateDataSet(ds))
                {
                    resultado = MapRepartoDAL.ObtenerTotalRepartosPorOperadorId(ds);
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

            return resultado;
        }

        /// <summary>
        /// Obtiene el total de repartos leidos por operador id
        /// </summary>
        /// <param name="operadorId">Identificador del operador</param>
        /// <param name="organizacionId">Identificador de la organizacion</param>
        /// <returns></returns>
        internal int ObtenerTotalRepartosLeidosPorOperadorId(int operadorId, int organizacionId)
        {
            var resultado = 0;
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerTotalesRepartoPorOperadorId(operadorId, organizacionId);
                var ds = Retrieve("Reparto_ConsultarLeidosPorOperadorId", parameters);
                if (ValidateDataSet(ds))
                {
                    resultado = MapRepartoDAL.ObtenerTotalRepartosPorOperadorId(ds);
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

            return resultado;
        }

        /// <summary>
        /// Obtiene los tipos de servicios
        /// </summary>
        /// <returns></returns>
        internal List<TipoServicioInfo> ObtenerTiposDeServicios()
        {
            List<TipoServicioInfo> result = null;
            try
            {
                Logger.Info();
                var ds = Retrieve("Reparto_ObtenerTiposDeServicio");
                if (ValidateDataSet(ds))
                {
                    result = MapRepartoDAL.ObtenerTiposDeServicios(ds);
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
            return result;
        }
        /// <summary>
        /// Obtiene las orden de reparto del dia actual
        /// </summary>
        /// <param name="lote">Lote del cual se consultara el reparto</param>
        /// <returns></returns>
        internal List<RepartoInfo> ObtenerRepartoActual(LoteInfo lote)
        {
            List<RepartoInfo> result = null;
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametroObtenerRepartoActual(lote);
                var ds = Retrieve("Reparto_ObtenerRepartoActual", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapRepartoDAL.ObtenerRepartoActual(ds);
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
            return result;
        }

        /// <summary>
        /// Obtiene el consumo total del dia
        /// </summary>
        /// <param name="organizacionId">Identificador de la organizacion</param>
        /// <param name="corral">Informacion del lote</param>
        /// <returns></returns>
        internal int ObtenerConsumoTotalDia(int organizacionId, CorralInfo corral)
        {
            var result = 0;
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametroConsumoTotalDia(organizacionId, corral);
                var ds = Retrieve("Reparto_ObtenerConsumoTotalDelDia", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapRepartoDAL.ObtenerConsumoTotalDia(ds);
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
            return result;
        }
        /// <summary>
        /// Obtener peso llegada
        /// </summary>
        /// <param name="lote">Informacion del lote</param>
        /// <returns></returns>
        internal OrdenRepartoAlimentacionInfo ObtenerPesoLlegada(LoteInfo lote)
        {
            var resultado = new OrdenRepartoAlimentacionInfo();
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametroPesoLlegada(lote);
                var ds = Retrieve("Reparto_ObtenerPesoLlegadaPorLote", parameters);
                if (ValidateDataSet(ds))
                {
                    resultado = MapRepartoDAL.ObtenerPesoLlegada(ds);
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
            return resultado;
        }

        /// <summary>
        /// Genera la orden de reparto de alimentacion
        /// </summary>
        /// <param name="ordenReparto">Orden de reparto que se guardara</param>
        /// <returns>OrdenRepartoAlimentacion con los identificadores nuevos de la orden y del detalle</returns>
        internal OrdenRepartoAlimentacionInfo GenerarOrdenReparto(OrdenRepartoAlimentacionInfo ordenReparto)
        {
            var resultado = new OrdenRepartoAlimentacionInfo();
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametroGenerarOrdenReparto(ordenReparto);
                var ds = Retrieve("Reparto_GuardarReparto", parameters);
                if (ValidateDataSet(ds))
                {
                    resultado = MapRepartoDAL.ObtenerGenerarOrdenReparto(ds);
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
            return resultado;
        }

        /// <summary>
        /// Obtiene un reparto por lote y fecha
        /// </summary>
        /// <param name="fecha">Fecha del reparto</param>
        /// <param name="lote">Lote del reparto</param>
        /// <returns>Informacion del reparo</returns>
        internal RepartoInfo ObtenerRepartoPorLoteFecha(DateTime fecha, LoteInfo lote)
        {
            RepartoInfo resultado = null;
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametroRepartoPorFecha(fecha, lote);
                var ds = Retrieve("Reparto_ObtenerRepartoActual", parameters);
                if (ValidateDataSet(ds))
                {
                    resultado = MapRepartoDAL.ObtenerRepartoPorFecha(ds);
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
            return resultado;
        }
        /// <summary>
        /// Obtiene una orden de reparto por lote y fecha
        /// </summary>
        /// <param name="fecha">Fecha de la orden de reparto</param>
        /// <param name="lote">Lote de la orden de reparto</param>
        /// <returns>Informacion del reparto</returns>
        internal RepartoInfo ObtenerRepartoPorFecha(DateTime fecha, LoteInfo lote)
        {
            RepartoInfo resultado = null;
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametroRepartoPorFecha(fecha, lote);
                var ds = Retrieve("Reparto_ObtenerRepartoFecha", parameters);
                if (ValidateDataSet(ds))
                {
                    resultado = MapRepartoDAL.ObtenerRepartoPorFecha(ds);
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
            return resultado;
        }
        /// <summary>
        /// Obtiene el avance del reparto
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario</param>
        /// <returns></returns>
        internal RepartoAvanceInfo ObtenerAvanceReparto(int usuarioId)
        {
            RepartoAvanceInfo resultado = null;
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametroAvanceReparto(usuarioId);
                var ds = Retrieve("Reparto_ObtenerAvanceReparto", parameters);
                if (ValidateDataSet(ds))
                {
                    resultado = MapRepartoDAL.ObtenerAvanceReparto(ds);
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
            return resultado;
        }

        /// <summary>
        /// Reportar avance del reparto
        /// </summary>
        /// <param name="avance">Informacion del avance</param>
        /// <returns></returns>
        internal void ReportarAvanceReparto(RepartoAvanceInfo avance)
        {
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametroReporteAvanceReparto(avance);
                Update("Reparto_ReportarAvanceReparto", parameters);

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
        /// Gurada los cambios al detalle del reparto
        /// </summary>
        /// <param name="cambiosDetalle">Lista con los cambios</param>
        /// <returns></returns>
        internal int GuardarCambiosRepartoDetalle(List<CambiosReporteInfo> cambiosDetalle)
        {
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametrosGuardarCambiosRepartoDetalle(cambiosDetalle);
                var result = Create("[dbo].[Configuracion_GuardarCambios]", parameters);
                return result;
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
        /// Guarda el estatus de la distribucion
        /// </summary>
        /// <param name="corral">Informacion del corral</param>
        /// <param name="estatusDistribucion">Estatus de la distribucion</param>
        /// <returns></returns>
        internal int GuardarEstatusDistribucion(CorralInfo corral, int estatusDistribucion)
        {
            var result = 0;
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.GuardarEstatusDistribucion(corral, estatusDistribucion);
                var ds = Retrieve("Reparto_GuardarDistribucionAlimento", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapRepartoDAL.ObtenerIdLoteDistribucion(ds);
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
            return result;
        }
        /// <summary>
        /// Carga los datos que se obtubieron del datalink
        /// </summary>
        /// <param name="listaDatalink">Lista que se obtuvo del archivo datalink</param>
        /// <returns></returns>
        internal int CargarArchivoDatalink(List<DataLinkInfo> listaDatalink)
        {
            int result;
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametrosActualizarDataLink(listaDatalink);
                result = Create("Reparto_ActualizarDetalleReparto", parameters);

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
            return result;
        }

        internal List<RepartoDetalleInfo> ObtenerRepartoDetallePorOrganizacionID(OrganizacionInfo organizacion, DateTime fecha)
        {
            List<RepartoDetalleInfo> result = null;
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametroObtenerRepartoDetallePorOrganizacionID(organizacion, fecha);
                var ds = Retrieve("RepartoDetalle_ObtenerPorOrganizacionID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapRepartoDAL.ObtenerRepartoDetallePorOrganizacionID(ds);
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
            return result;
        }


        /// <summary>
        /// Genera la orden de reparto de alimentacion Manual
        /// </summary>
        /// <param name="ordenReparto">Orden de reparto que se guardara</param>
        /// <returns>OrdenRepartoAlimentacion con los identificadores nuevos de la orden y del detalle</returns>
        internal OrdenRepartoAlimentacionInfo GenerarOrdenRepartoManual(OrdenRepartoAlimentacionInfo ordenReparto)
        {
            var resultado = new OrdenRepartoAlimentacionInfo();
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametroGenerarOrdenRepartoManual(ordenReparto);
                var ds = Retrieve("Reparto_GuardarRepartoManual", parameters);
                if (ValidateDataSet(ds))
                {
                    resultado = MapRepartoDAL.ObtenerGenerarOrdenReparto(ds);
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
            return resultado;
        }

        /// <summary>
        /// Metodo para obtener los repartos de Forraje
        /// </summary>
        /// <param name="corteGanadoGuardarInfo"></param>
        /// <returns></returns>
        public List<RepartoDetalleInfo> ObtenerRepartoPorTipoServicioFecha(CorteGanadoGuardarInfo corteGanadoGuardarInfo)
        {
            List<RepartoDetalleInfo> result = null;
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametroObtenerRepartoPorTipoServicioFecha(corteGanadoGuardarInfo);
                var ds = Retrieve("Reparto_ObtenerRepartoPorTipoServicioLoteApartirFechaEntrada", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapRepartoDAL.ObtenerRepartoPorTipoServicioFecha(ds);
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
            return result;
        }

        internal int GuardarImporte(RepartoDetalleInfo repartoDetalleInfo, AlmacenInventarioInfo inventarioInfo, LoteInfo lote)
        {
            int retorno = 0;
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametroGuardarImporte(repartoDetalleInfo, inventarioInfo, lote);
                Update("Reparto_GuardarImporte", parameters);
                retorno = 1;
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
            return retorno;
        }

        /// <summary>
        /// Obtiene los datos para generar la poliza de consumo
        /// </summary>
        /// <returns></returns>
        internal PolizaConsumoAlimentoModel ObtenerDatosPolizaConsumo(List<AlmacenMovimientoDetalle> movimientoDetalles, int organizacionID)
        {
            PolizaConsumoAlimentoModel retorno = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxRepartoDAL.ObtenerParametrosPolizaConsumo(
                    movimientoDetalles, organizacionID);
                DataSet ds = Retrieve("PolizaConsumoAlimento_ObtenerDatos", parametros);
                if (ValidateDataSet(ds))
                {
                    retorno = MapRepartoDAL.ObtenerDatosPolizaConsumo(ds);
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
            return retorno;
        }

        /// <summary>
        /// Obtiene un reparto completo
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="lotes"></param>
        /// <returns></returns>
        internal IList<RepartoInfo> ObtenerRepartoPorFechaCompleto(DateTime fecha, IList<LoteInfo> lotes)
        {
            IList<RepartoInfo> resultado = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRepartoDAL.ObtenerParametroRepartoPorFechaCompleto(fecha,
                                                                                                              lotes);
                var ds = Retrieve("Reparto_ObtenerRepartoFechaCompleto", parameters);
                if (ValidateDataSet(ds))
                {
                    resultado = MapRepartoDAL.ObtenerRepartoPorFechaCompleto(ds);
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
            return resultado;
        }

        /// <summary>
        /// Carga los datos que se obtubieron del datalink
        /// </summary>
        /// <param name="info">Lista que se obtuvo del archivo datalink</param>
        /// <returns></returns>
        internal int Crear(RepartoInfo info)
        {
            int result;
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametrosCrear(info);
                result = Create("Reparto_Crear", parameters);
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
            return result;
        }

        /// <summary>
        /// Carga los datos que se obtubieron del datalink
        /// </summary>
        /// <param name="repartoDetalle">Lista que se obtuvo del archivo datalink</param>
        /// <returns></returns>
        internal int GuardarRepartoDetalle(List<RepartoDetalleInfo> repartoDetalle)
        {
            int result;
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametrosGuardarRepartoDetalle(repartoDetalle);
                result = Create("RepartoDetalle_GuardarDetalleXml", parameters);
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
            return result;
        }

        /// <summary>
        /// Genera orden de reparto para los corrales que no tienen
        /// </summary>
        /// <param name="cambiosDetalle"></param>
        /// <returns></returns>
        internal int GenerarOrdenRepartoConfiguracionAjustes(List<CambiosReporteInfo> cambiosDetalle)
        {
            int result;
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametrosGenerarOrdenRepartoConfiguracionAjustes(cambiosDetalle);
                result = Create("ConfiguracionAjustes_GenerarReparto", parameters);
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
            return result;
        }

        /// <summary>
        /// Obtiene una orden de reparto por lote y fecha
        /// </summary>
        /// <param name="fecha">Fecha de la orden de reparto</param>
        /// <param name="corral">Lote de la orden de reparto</param>
        /// <returns>Informacion del reparto</returns>
        internal RepartoInfo ObtenerRepartoPorFechaCorral(DateTime fecha, CorralInfo corral)
        {
            RepartoInfo resultado = null;
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametroRepartoPorFechaCorral(fecha, corral);
                var ds = Retrieve("Reparto_ObtenerRepartoFechaCorral", parameters);
                if (ValidateDataSet(ds))
                {
                    resultado = MapRepartoDAL.ObtenerRepartoPorFechaCorral(ds);
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
            return resultado;
        }
        /// <summary>
        /// Metodo que actualiza las formulas de los repartos de la mañana y tarde.
        /// </summary>
        /// <param name="cambiosDetalle"></param>
        /// <returns></returns>
        internal int GuardarFormulasRepartoDetalle(List<CambiosReporteInfo> cambiosDetalle)
        {
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametrosActualizarFormulaRepartoConfiguracionAjustes(cambiosDetalle);
                var result = Create("[dbo].[ConfiguracionAjustes_ActualizarFormulas]", parameters);
                return result;
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
        /// Carga los datos que se obtubieron del datalink
        /// </summary>
        /// <param name="repartoDetalle">Lista que se obtuvo del archivo datalink</param>
        /// <returns></returns>
        internal int ActualizarAlmacenMovimientoReparto(List<RepartoDetalleInfo> repartoDetalle)
        {
            int result;
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametrosActualizarAlmacenMovimientoReparto(repartoDetalle);
                result = Create("RepartoDetalle_ActualizarAlmacenMovimiento", parameters);
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
            return result;
        }

        /// <summary>
        /// Obtiene parametros para obtener los repartos por la lista de ids
        /// </summary>
        /// <param name="repartosID"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal List<RepartoInfo> ObtenerPorRepartosID(List<long> repartosID, int organizacionID)
        {
            List<RepartoInfo> result = null;
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametroPorRepartosId(repartosID, organizacionID);
                var ds = Retrieve("Reparto_ObtenerPorRepartosID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapRepartoDAL.ObtenerPorRepartosID(ds);
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
            return result;
        }

        /// <summary>
        /// Obtiene los repartos ajustados en el dia
        /// </summary>
        /// <param name="fechaReparto">Fecha del Reparto</param>
        /// <param name="organizacionID">Organizacion ID</param>
        /// <returns></returns>
        internal List<RepartoInfo> ObtenerRepartosAjustados(DateTime fechaReparto, int organizacionID)
        {
            var result = new List<RepartoInfo>();
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametroRepartosAjustados(fechaReparto, organizacionID);
                var ds = Retrieve("Reparto_ObtenerRepartosAjustados", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapRepartoDAL.ObtenerRepartosAjustados(ds);
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
            return result;
        }

        /// <summary>
        /// Carga los datos que se obtubieron del datalink
        /// </summary>
        /// <param name="repartoDetalle">Lista que se obtuvo del archivo datalink</param>
        /// <returns></returns>
        internal int ActualizarCantidadProgramadaReparto(List<RepartoDetalleInfo> repartoDetalle)
        {
            int result;
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametrosActualizarCantidadProgramadaReparto(repartoDetalle);
                result = Create("RepartoDetalle_ActualizarCantidadProgramada", parameters);
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
            return result;
        }

        /// <summary>
        /// Obtiene repartos por lote y organizacion
        /// </summary>
        /// <param name="loteID"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal List<RepartoInfo> ObtenerPorLoteOrganizacion(int loteID, int organizacionId)
        {
            var result = new List<RepartoInfo>();
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRepartoDAL.ObtenerParametrosPorLoteOrganizacion(loteID,
                                                                                                           organizacionId);
                var ds = Retrieve("Reparto_ObtenerPorLoteOrganizacion", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapRepartoDAL.ObtenerRepartosPorLoteOrganizacion(ds);
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
            return result;
        }

        internal int GuardarImporteXML(List<RepartoDetalleInfo> listaRepartos)
        {
            int retorno = 0;
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametroGuardarImporteXML(listaRepartos);
                Update("Reparto_GuardarImporteXML", parameters);
                retorno = 1;
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
            return retorno;
        }

        /// <summary>
        /// Obtener peso llegada
        /// </summary>
        /// <param name="lotesXml">Informacion del lote</param>
        /// <param name="organizacionID">Informacion del lote</param>
        /// <returns></returns>
        internal IList<OrdenRepartoAlimentacionInfo> ObtenerPesoLlegadaXML(IList<LoteInfo> lotesXml, int organizacionID)
        {
            IList<OrdenRepartoAlimentacionInfo> resultado = null;
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametroPesoLlegadaXML(lotesXml, organizacionID);
                var ds = Retrieve("Reparto_ObtenerPesoLlegadaPorLoteXML", parameters);
                if (ValidateDataSet(ds))
                {
                    resultado = MapRepartoDAL.ObtenerPesoLlegadaXML(ds);
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
            return resultado;
        }

        /// <summary>
        /// Obtiene una orden de reparto por lote y fecha
        /// </summary>
        /// <param name="fecha">Fecha de la orden de reparto</param>
        /// <param name="lotesXml">Lote de la orden de reparto</param>
        /// <param name="organizacionID">Lote de la orden de reparto</param>
        /// <returns>Informacion del reparto</returns>
        internal IList<RepartoInfo> ObtenerRepartoPorFechaXML(DateTime fecha, IList<LoteInfo> lotesXml, int organizacionID)
        {
            IList<RepartoInfo> resultado = null;
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametroRepartoPorFechaXML(fecha, lotesXml, organizacionID);
                var ds = Retrieve("Reparto_ObtenerRepartoFechaLoteXML", parameters);
                if (ValidateDataSet(ds))
                {
                    resultado = MapRepartoDAL.ObtenerRepartoPorFechaXML(ds);
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
            return resultado;
        }

        /// <summary>
        /// Obtiene el consumo total del dia
        /// </summary>
        /// <param name="organizacionId">Identificador de la organizacion</param>
        /// <param name="corralesXml">Informacion del lote</param>
        /// <param name="fecha">fecha de los consumos</param>
        /// <returns></returns>
        internal IList<ConsumoTotalCorralModel> ObtenerConsumoTotalDiaXML(int organizacionId, IList<CorralInfo> corralesXml, DateTime fecha)
        {
            IList<ConsumoTotalCorralModel> result = null;
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametroConsumoTotalDiaXML(organizacionId, corralesXml, fecha);
                var ds = Retrieve("Reparto_ObtenerConsumoTotalDelDiaXML", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapRepartoDAL.ObtenerConsumoTotalDiaXML(ds);
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
            return result;
        }

        /// <summary>
        /// Obtiene repartos por lote y organizacion
        /// </summary>
        /// <param name="corralID"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal List<RepartoInfo> ObtenerPorCorralOrganizacion(int corralID, int organizacionId)
        {
            var result = new List<RepartoInfo>();
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRepartoDAL.ObtenerParametrosPorCorralOrganizacion(corralID,
                                                                                                           organizacionId);
                var ds = Retrieve("Reparto_ObtenerPorCorralOrganizacion", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapRepartoDAL.ObtenerRepartosPorCorralOrganizacion(ds);
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
            return result;
        }

        /// <summary>
        /// Obtiene los consumos pendientes de aplicar
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        internal List<AplicacionConsumoDetalleModel> ObtenerConsumoPendiente(AplicacionConsumoModel filtros)
        {
            var result = new List<AplicacionConsumoDetalleModel>();
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRepartoDAL.ObtenerParametrosConsumoPendiente(filtros);
                var ds = Retrieve("Reparto_ObtenerConsumoPendiente", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapRepartoDAL.ObtenerConsumoPendiente(ds);
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
            return result;
        }

        /// <summary>
        /// Obtiene una orden de reparto por lote y fecha
        /// </summary>
        /// <param name="fecha">Fecha de la orden de reparto</param>
        /// <param name="organizacionID">Organizacion del reparto</param>
        /// <param name="corrales">corrales del reparto</param>
        /// <returns>Informacion del reparto</returns>
        internal List<RepartoInfo> ObtenerRepartosPorFechaCorrales(DateTime fecha,int organizacionID, List<CorralInfo> corrales)
        {
            List<RepartoInfo> resultado = null;
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametroRepartosPorFechaCorrales(fecha,organizacionID, corrales);
                var ds = Retrieve("Reparto_ObtenerRepartoFechaCorralesXML", parameters);
                if (ValidateDataSet(ds))
                {
                    resultado = MapRepartoDAL.ObtenerRepartosPorFechaCorrales(ds);
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
            return resultado;
        }

        /// <summary>
        /// Gurada los cambios al detalle del reparto
        /// </summary>
        /// <param name="cambiosDetalle">Lista con los cambios</param>
        /// <returns></returns>
        internal int GuardarRepartosServicioCorrales(List<CambiosReporteInfo> cambiosDetalle)
        {
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametrosGuardarRepartosServicioCorrales(cambiosDetalle);
                var result = Create("[dbo].[Reparto_GenerarRepartoServicioCorral]", parameters);
                return result;
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
        /// Obtiene una orden de reparto por lote y fecha
        /// </summary>
        /// <param name="fecha">Fecha de la orden de reparto</param>
        /// <param name="corral">Lote de la orden de reparto</param>
        /// <param name="tipoServicioID">servicio del reparto</param>
        /// <returns>Informacion del reparto</returns>
        internal RepartoInfo ObtenerRepartoPorFechaCorralServicio(DateTime fecha, CorralInfo corral, int tipoServicioID)
        {
            RepartoInfo resultado = null;
            try
            {
                Logger.Info();
                var parameters = AuxRepartoDAL.ObtenerParametroRepartoPorFechaCorralServicio(fecha, corral, tipoServicioID);
                var ds = Retrieve("Reparto_ObtenerRepartoFechaCorralServicio", parameters);
                if (ValidateDataSet(ds))
                {
                    resultado = MapRepartoDAL.ObtenerRepartoPorFechaCorralServicio(ds);
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
            return resultado;
        }
    }
}
