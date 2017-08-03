using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    /// <summary>
    /// Clase de presentacion para Gastos al inventario
    /// </summary>
    public class GastoInventarioPL
    {
        /// <summary>
        /// Guarda la informacion del gasto al inventario
        /// </summary>
        /// <param name="gasto"></param>
        /// <returns></returns>
        public long Guardar(GastoInventarioInfo gasto)
        {
            long retValue;
            try
            {
                Logger.Info();
                var gastoInventarioBL = new GastoInventarioBL();
                retValue = gastoInventarioBL.Guardar(gasto);
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
            return retValue;
        }

        /// <summary>
        /// Obtiene una coleccion de gastos por inventario
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        public IEnumerable<GastoInventarioInfo> ObtenerGastosInventarioPorFechaConciliacion(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            IEnumerable<GastoInventarioInfo> gastosInventario;
            try
            {
                Logger.Info();
                var gastoInventarioBL = new GastoInventarioBL();
                gastosInventario = gastoInventarioBL.ObtenerGastosInventarioPorFechaConciliacion(organizacionID,
                                                                                                 fechaInicial,
                                                                                                 fechaFinal);
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
            return gastosInventario;
        }
    }
}
