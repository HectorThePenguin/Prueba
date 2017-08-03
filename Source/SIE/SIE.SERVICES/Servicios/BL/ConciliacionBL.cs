using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class ConciliacionBL
    {
        /// <summary>
        /// Obtiene los datos necesarios para la conciliacion
        /// de pases a proceso
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        public List<PolizaPaseProcesoModel> ObtenerConciliacionPaseProceso(int organizacionId, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                Logger.Info();
                var conciliacionDAL = new ConciliacionDAL();
                List<PolizaPaseProcesoModel> conciliacionPaseProceso =
                    conciliacionDAL.ObtenerConciliacionPaseProceso(organizacionId, fechaInicial, fechaFinal);
                return conciliacionPaseProceso;
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
        /// Metodo que obtiene una lista de  conciliacion  por tipo poliza
        /// </summary>
        /// <param name="tipoPoliza"></param>
        /// <returns></returns>
        internal IList<PolizasIncorrectasInfo> ConciliacionTipoPoliza(int  tipoPoliza)
        {
            try
            {
                Logger.Info();
                var conciliacioDAL = new ConciliacionDAL();
                IList<PolizasIncorrectasInfo> result = conciliacioDAL.ConciliacionTipoPoliza(tipoPoliza);
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
        ///  Metodo  que obtiene una lista de Conciliacion detalle  por tipo poliza
        /// </summary>
        /// <param name="TipoPoliza"></param>
        /// <returns></returns>
        internal IList<ConciliacionInfo> ConciliacionDetalle(int TipoPoliza)
        {         
            try
            {
                Logger.Info();
                var conciliacionDAL = new ConciliacionDAL();
                IList<ConciliacionInfo> result = conciliacionDAL.ConciliacionDetalle(TipoPoliza);
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
        /// Obtiene los movimientos de almacen
        /// para su conciliacion
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        public ConciliacionMovimientosAlmacenModel ObtenerMovimientosAlmacenConciliacion(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                Logger.Info();
                var conciliacionDAL = new ConciliacionDAL();
                ConciliacionMovimientosAlmacenModel almacenesMovimiento =
                    conciliacionDAL.ObtenerMovimientosAlmacenConciliacion(organizacionID, fechaInicial, fechaFinal);
                return almacenesMovimiento;
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
        /// Obtiene los movimientos a conciliar de materia prima
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        public List<ContenedorEntradaMateriaPrimaInfo> ObtenerEntradasMateriaPrimaConciliacion(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                Logger.Info();
                var conciliacionDAL = new ConciliacionDAL();
                List<ContenedorEntradaMateriaPrimaInfo> contenedorEntradaMateriaPrima =
                    conciliacionDAL.ObtenerContenedorEntradaMateriaPrimaConciliacion(organizacionID, fechaInicial,
                                                                                     fechaFinal);
                return contenedorEntradaMateriaPrima;
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
