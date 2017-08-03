using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class PremezclaDistribucionDAL : DALBase
    {
        /// <summary>
        /// Guarda la premezcla distribucion
        /// </summary>
        /// <param name="premezclaDistribucion"></param>
        /// <returns></returns>
        internal PremezclaDistribucionInfo GuardarPremezclaDistribucion(PremezclaDistribucionInfo premezclaDistribucion)
        {
            PremezclaDistribucionInfo premezcla = null;
            try
            {
                Dictionary<string, object> parametros = AuxPremezclaDistribucionDAL.ObtenerParametrosGuardarPremezclaDistribucion(premezclaDistribucion);
                DataSet ds = Retrieve("PremezclaDistribucion_Crear", parametros);
                if (ValidateDataSet(ds))
                {
                    premezcla = MapPremezclaDistribucionDAL.ObtenerPremezclaDistribucion(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return premezcla;
        }

        /// <summary>
        /// Obtiene una lista de distribucion de ingredientes
        /// </summary>
        /// <param name="organizacionID"> </param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        internal List<DistribucionDeIngredientesInfo> ObtenerPremezclaDistribucionConciliacion(int organizacionID, DateTime fechaInicio, DateTime fechaFinal)
        {
            List<DistribucionDeIngredientesInfo> premezclas = null;
            try
            {
                Dictionary<string, object> parametros =
                    AuxPremezclaDistribucionDAL.ObtenerParametrosPremezclaDistribucionConciliacion(organizacionID,
                                                                                                   fechaInicio,
                                                                                                   fechaFinal);
                using (IDataReader reader = RetrieveReader("PremezclaDistribucion_ObtenerConciliacionMovimientosSIAP", parametros))
                {
                    if (ValidateDataReader(reader))
                    {
                        premezclas = MapPremezclaDistribucionDAL.ObtenerPremezclaDistribucionConciliacion(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return premezclas;
        }

        internal List<PremezclaDistribucionCostoInfo> ObtenerPremezclaDistribucionCosto(int productoID)
        {
            List<PremezclaDistribucionCostoInfo> premezclas = null;
            try
            {
                Dictionary<string, object> parametros =
                    AuxPremezclaDistribucionDAL.ObtenerPremezclaDistribucionCosto(productoID);
                using (IDataReader reader = RetrieveReader("PremezclaDistribucion_ObtenerCostosPorProductoId", parametros))
                {
                    if (ValidateDataReader(reader))
                    {
                        premezclas = MapPremezclaDistribucionDAL.ObtenerPremezclaDistribucionCosto(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return premezclas;
        }
    }
}
