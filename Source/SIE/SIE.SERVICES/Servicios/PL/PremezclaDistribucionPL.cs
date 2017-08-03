using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Services.Integracion.DAL.Excepciones;

namespace SIE.Services.Servicios.PL
{
    public class PremezclaDistribucionPL
    {
        /// <summary>
        /// Guarda la premezcla distribucion
        /// </summary>
        /// <param name="distribucionIngredientes"></param>
        /// <returns></returns>
        public PremezclaDistribucionInfo GuardarPremezclaDistribucion(DistribucionDeIngredientesInfo distribucionIngredientes)
        {
            try
            {
                var premezclaDistribucion = new PremezclaDistribucionBL();
                return premezclaDistribucion.GuardarPremezclaDistribucion(distribucionIngredientes);
            }
            catch(ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de distribucion de ingredientes
        /// </summary>
        /// <param name="organizacionID"> </param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        public List<DistribucionDeIngredientesInfo> ObtenerPremezclaDistribucionConciliacion(int organizacionID, DateTime fechaInicio, DateTime fechaFinal)
        {
            try
            {
                var premezclaDistribucion = new PremezclaDistribucionBL();
                return premezclaDistribucion.ObtenerPremezclaDistribucionConciliacion(organizacionID, fechaInicio, fechaFinal);
            }
            catch (ExcepcionServicio ex)
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
        /// Obtiene una lista de Costos
        /// </summary>
        /// <returns></returns>
        public List<PremezclaDistribucionCostoInfo> ObtenerPremezclaDistribucionCosto(int productoID)
        {
            try
            {
                var premezclaDistribucion = new PremezclaDistribucionBL();
                return premezclaDistribucion.ObtenerPremezclaDistribucionCosto(productoID);
            }
            catch (ExcepcionServicio ex)
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
