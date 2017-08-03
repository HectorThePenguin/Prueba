using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;
using System.Collections.Generic;

namespace SIE.Services.Servicios.PL
{
    public class GastoMateriaPrimaPL
    {
        /// <summary>
        /// Guarda los datos para una solicitud de premezclas
        /// </summary>
        /// <param name="gasto"></param>
        /// <returns></returns>
        public long Guardar(GastoMateriaPrimaInfo gasto)
        {
            try
            {
                Logger.Info();
                var gastoMateriaPrimaBl = new GastoMateriaPrimaBL();
                return gastoMateriaPrimaBl.Guardar(gasto);
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
        /// Obtiene una lista de gastos de materia prima
        /// por sus movimientos de almacen
        /// </summary>
        /// <param name="almacenesMovimiento"></param>
        /// <returns></returns>
        public IEnumerable<GastoMateriaPrimaInfo> ObtenerGastosMateriaPrimaPorAlmacenMovimientoXML(List<AlmacenMovimientoInfo> almacenesMovimiento)
        {
            try
            {
                Logger.Info();
                var gastoMateriaPrimaBl = new GastoMateriaPrimaBL();
                return gastoMateriaPrimaBl.ObtenerGastosMateriaPrimaPorAlmacenMovimientoXML(almacenesMovimiento);
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

        public IEnumerable<AreteInfo> ConsultarAretes(List<AreteInfo> aretes, int organizacionID, bool esAreteSukarne, bool esEntradaAlmacen)
        {
            try
            {
                Logger.Info();
                var bl = new GastoMateriaPrimaBL();
                return bl.ConsultarAretes(aretes, organizacionID, esAreteSukarne, esEntradaAlmacen);
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
    }
}
