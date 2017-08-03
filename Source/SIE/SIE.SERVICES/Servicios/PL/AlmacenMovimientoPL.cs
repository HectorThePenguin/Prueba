using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class AlmacenMovimientoPL
    {
        /// <summary>
        /// Actualiza el estatus de un movimiento
        /// </summary>
        /// <param name="almacenMovimientoInfo"></param>
        public void ActualizarEstatus(AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            try
            {
                Logger.Info();
                var almacenMovimientoBl = new AlmacenMovimientoBL();
                almacenMovimientoBl.ActualizarEstatus(almacenMovimientoInfo);
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
        /// Guarda el Cierre de Dia
        /// </summary>
        /// <param name="cierreDiaInventarioPA"></param>
        public IList<ResultadoPolizaModel> GuardarCierreDiaInventarioPA(CierreDiaInventarioPAInfo cierreDiaInventarioPA)
        {
            try
            {
                Logger.Info();
                var almacenMovimientoBl = new AlmacenMovimientoBL();
                return almacenMovimientoBl.GuardarCierreDiaInventarioPA(cierreDiaInventarioPA);
            }
            catch (ExcepcionServicio)
            {
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
        /// Guarda el Cierre de Dia
        /// </summary>
        /// <param name="almacenID"></param>
        public bool ValidarEjecucionCierreDia(int almacenID)
        {
            try
            {
                Logger.Info();
                var almacenMovimientoBl = new AlmacenMovimientoBL();
                return almacenMovimientoBl.ValidarEjecucionCierreDia(almacenID);
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
        /// Obtiene los movimientos pendientes pro autorizar del cierre de dia de inventario de planta de alimentos
        /// </summary>
        /// <returns></returns>
        public List<MovimientosAutorizarCierreDiaPAModel> ObtenerMovimientosPendientesAutorizar(
            FiltrosAutorizarCierreDiaInventarioPA filtrosAutorizarCierreDia)
        {
            try
            {
                Logger.Info();
                var almacenMovimientoBl = new AlmacenMovimientoBL();
                return almacenMovimientoBl.ObtenerMovimientosPendientesAutorizar(filtrosAutorizarCierreDia);
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
        /// Guarda el Cierre de Dia
        /// </summary>
        /// <param name="cierreDiaInventarioPA"></param>
        public void GuardarAutorizarCierreDiaInventarioPA(CierreDiaInventarioPAInfo cierreDiaInventarioPA)
        {
            try
            {
                Logger.Info();
                var almacenMovimientoBl = new AlmacenMovimientoBL();
                almacenMovimientoBl.GuardarAutorizarCierreDiaInventarioPA(cierreDiaInventarioPA);
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
        /// Obtiene los movimientos de inventario para generar
        /// poliza de consumo de producto
        /// </summary>
        /// <param name="almacenID"></param>
        /// <param name="organizacionID"></param>
        /// <param name="fecha"> </param>
        /// <returns></returns>
        public List<ContenedorAlmacenMovimientoCierreDia> ObtenerMovimientosInventario(int almacenID, int organizacionID, DateTime fecha)
        {
            List<ContenedorAlmacenMovimientoCierreDia> result;
            try
            {
                Logger.Info();
                var almacenMovimientoDal = new AlmacenMovimientoBL();
                result = almacenMovimientoDal.ObtenerMovimientosInventario(almacenID, organizacionID, fecha);
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
            return result;
        }

        /// <summary>
        /// Actualiza el estatus para indicar si ya se genero ó no poliza
        /// </summary>
        /// <param name="movimientos"></param>
        /// <returns></returns>
        public void ActualizarGeneracionPoliza(List<ContenedorAlmacenMovimientoCierreDia> movimientos)
        {
            try
            {
                Logger.Info();
                var almacenMovimientoDal = new AlmacenMovimientoBL();
                almacenMovimientoDal.ActualizarGeneracionPoliza(movimientos);
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
