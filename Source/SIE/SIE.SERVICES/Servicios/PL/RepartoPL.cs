using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class RepartoPL
    {
        /// <summary>
        /// Obtiene el Reparto por lote
        /// </summary>
        /// <param name="lote">Lote del cual se quiere obtener el reparto</param>
        /// <param name="fechaReparto">Fecha del reparto</param>
        /// <returns>Informacion del reaprto</returns>
        public RepartoInfo ObtnerPorLote(LoteInfo lote, DateTime fechaReparto)
        {
            RepartoInfo result;
            try
            {
                Logger.Info();
                var repartoBl = new RepartoBL();
                result = repartoBl.ObtenerPorLote(lote, fechaReparto);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
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
        /// <returns>Numero de dias de retiro</returns>
        public int ObtenerDiasRetiro(LoteInfo lote)
        {
            int result;
            try
            {
                Logger.Info();
                var repartoBl = new RepartoBL();
                result = repartoBl.ObtenerDiasRetiro(lote);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;

        }
        /// <summary>
        /// Obtiene el detalle de una orden de reparto
        /// </summary>
        /// <param name="reparto">Reparto</param>
        /// <returns>Lista con el detalle</returns>
        public IList<RepartoDetalleInfo> ObtenerDetalle(RepartoInfo reparto)
        {
            IList<RepartoDetalleInfo> result;
            try
            {
                Logger.Info();
                var repartoBl = new RepartoBL();
                result = repartoBl.ObtenerDetalle(reparto);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
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
        /// <returns>Informacion del reparto</returns>
        public RepartoInfo ObtenerRepartoPorOperadorId(OperadorInfo operador, CorralInfo corral)
        {
            RepartoInfo result;
            try
            {
                Logger.Info();
                var repartoBl = new RepartoBL();
                result = repartoBl.ObtenerRepartoPorOperadorId(operador, corral);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene los tipos de servicio
        /// </summary>
        /// <returns>Lista con los servicios configurados</returns>
        public List<TipoServicioInfo> ObtenerTiposDeServicios()
        {
            List<TipoServicioInfo> resultado;
            try
            {
                Logger.Info();
                var repartoBl = new RepartoBL();
                resultado = repartoBl.ObtenerTiposDeServicios();
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene las ordenes de reparto de la fecha actual
        /// </summary>
        /// <param name="lote">informacion del lote</param>
        /// <returns>Lista de repartos</returns>
        public List<RepartoInfo> ObtenerRepartoActual(LoteInfo lote)
        {
            List<RepartoInfo> resultado;
            try
            {
                Logger.Info();
                var repartoBl = new RepartoBL();
                resultado = repartoBl.ObtenerRepartoActual(lote);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
        /// <summary>
        /// Valida la orden de reparto antes de ser guardada
        /// </summary>
        /// <param name="tipoServicioId">Identificador del tipo de servicio</param>
        /// <param name="organizacionId">Identificador de la organizacion</param>
        /// <returns></returns>
        public ResultadoValidacion ValidarOrdenReparto(int tipoServicioId, int organizacionId)
        {
            ResultadoValidacion resultado;
            try
            {
                Logger.Info();
                var repartoBl = new RepartoBL();
                resultado = repartoBl.ValidarOrdenReparto(tipoServicioId, organizacionId);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Genera la orden de reparto
        /// </summary>
        /// <param name="ordenReparto">Informacion de la orden de reparto de alimentacion</param>
        /// <returns>Resultado de la operacion</returns>
        public ResultadoOperacion GenerarOrdenReparto(OrdenRepartoAlimentacionInfo ordenReparto)
        {
            ResultadoOperacion resultado;

            try
            {
                Logger.Info();
                var repartoBl = new RepartoBL();
                resultado = repartoBl.GenerarOrdenReparto(ordenReparto);

            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
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
        /// <returns>Informacion del avance del reparto</returns>
        public RepartoAvanceInfo ObtenerAvanceReparto(int usuarioId)
        {
            RepartoAvanceInfo resultado;
            try
            {
                Logger.Info();
                var repartoBl = new RepartoBL();
                resultado = repartoBl.ObtenerAvanceReparto(usuarioId);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Guarda los cambios en la tabla repartodetalle
        /// </summary>
        /// <param name="cambiosDetalle">Lista de los cambios de reparto</param>
        /// <returns></returns>
        public int GuardarCambiosRepartoDetalle(List<CambiosReporteInfo> cambiosDetalle)
        {
            int result;
            try
            {
                Logger.Info();
                var repartoBl = new RepartoBL();
                result = repartoBl.GuardarCambiosRepartoDetalle(cambiosDetalle);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }
        /// <summary>
        /// Guarda el estatus de la distribucion
        /// </summary>
        /// <param name="corral">Informacion del corral</param>
        /// <param name="estatusDistribucion">Estatus de la distribucion</param>
        /// <returns></returns>
        public int GuardarEstatusDistribucion(CorralInfo corral, int estatusDistribucion)
        {
            int result;
            try
            {
                Logger.Info();
                var repartoBl = new RepartoBL();
                result = repartoBl.GuardarEstatusDistribucion(corral, estatusDistribucion);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }
        /// <summary>
        /// Obtiene la consiguracion para la orden de reparto
        /// </summary>
        /// <param name="organizacionId">Identificador de la organizacion</param>
        /// <param name="parametro">Parametros solicitado</param>
        /// <returns></returns>
        public string LeerConfiguracion(int organizacionId,ParametrosEnum parametro)
        {
            string resultado;
            try
            {
                Logger.Info();
                var repartoBl = new RepartoBL();
                resultado = repartoBl.LeerConfiguracion(organizacionId, parametro);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Genera un reparto manual para cada corral especificado.
        /// </summary>
        /// <param name="corrales"></param>
        /// <param name="parametroConsumo"></param>
        /// <returns></returns>
        public ResultadoOperacion GenerarOrdenRepartoManual(List<CorralInfo> corrales, ParametroCapturaManualConsumoInfo parametroConsumo)
        {
            ResultadoOperacion resultado;

            try
            {
                Logger.Info();
                var repartoBl = new RepartoBL();
                resultado = repartoBl.GenerarOrdenRepartoManual(corrales, parametroConsumo);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene los datos para la generacion de la 
        /// poilza de consumo de alimento
        /// </summary>
        /// <param name="movimientoDetalles"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public PolizaConsumoAlimentoModel ObtenerDatosPolizaConsumo(List<AlmacenMovimientoDetalle> movimientoDetalles, int organizacionID)
        {
            PolizaConsumoAlimentoModel resultado;
            try
            {
                Logger.Info();
                var repartoBl = new RepartoBL();
                resultado = repartoBl.ObtenerDatosPolizaConsumo(movimientoDetalles, organizacionID);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Genera el reparto para los corrales que no tienen
        /// </summary>
        /// <param name="cambiosDetalle"></param>
        /// <returns></returns>
        public int GenerarOrdenRepartoConfiguracionAjustes(List<CambiosReporteInfo> cambiosDetalle)
        {
            int resultado = 0;
            try
            {
                Logger.Info();
                var repartoBl = new RepartoBL();
                resultado = repartoBl.GenerarOrdenRepartoConfiguracionAjustes(cambiosDetalle);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene el Reparto por lote
        /// </summary>
        /// <param name="corral">Lote del cual se quiere obtener el reparto</param>
        /// <param name="fechaReparto">Fecha del reparto</param>
        /// <returns>Informacion del reaprto</returns>
        public RepartoInfo ObtnerPorFechaCorral(CorralInfo corral, DateTime fechaReparto)
        {
            RepartoInfo result;
            try
            {
                Logger.Info();
                var repartoBl = new RepartoBL();
                result = repartoBl.ObtenerRepartoPorFechaCorral(fechaReparto, corral);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;

        }
        /// <summary>
        ///  Metodo que actualiza las formulas de los repartos de la mañana y tarde.
        /// </summary>
        /// <param name="cambiosDetalle"></param>
        /// <returns></returns>
        public int GuardarFormulasRepartoDetalle(List<CambiosReporteInfo> cambiosDetalle)
        {
            int result;
            try
            {
                Logger.Info();
                var repartoBl = new RepartoBL();
                result = repartoBl.GuardarFormulasRepartoDetalle(cambiosDetalle);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
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
        public List<RepartoInfo> ObtenerPorLoteOrganizacion(int loteID, int organizacionId)
        {
            List<RepartoInfo> result;
            try
            {
                Logger.Info();
                var repartoBl = new RepartoBL();
                result = repartoBl.ObtenerPorLoteOrganizacion(loteID, organizacionId);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
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
        public List<RepartoInfo> ObtenerPorCorralOrganizacion(int corralID, int organizacionId)
        {
            List<RepartoInfo> result;
            try
            {
                Logger.Info();
                var repartoBl = new RepartoBL();
                result = repartoBl.ObtenerPorCorralOrganizacion(corralID, organizacionId);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
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
        public List<AplicacionConsumoDetalleModel> ObtenerConsumoPendiente(AplicacionConsumoModel filtros)
        {
            List<AplicacionConsumoDetalleModel> result;
            try
            {
                Logger.Info();
                var repartoBl = new RepartoBL();
                result = repartoBl.ObtenerConsumoPendiente(filtros);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }
    }
}
