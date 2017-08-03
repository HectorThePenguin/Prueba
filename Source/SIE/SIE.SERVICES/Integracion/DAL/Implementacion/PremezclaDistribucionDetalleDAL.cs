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
    internal class PremezclaDistribucionDetalleDAL : DALBase
    {
        /// <summary>
        /// Guarda la premezcla distribucion detalle
        /// </summary>
        /// <param name="premezclaDistribucionDetalle"></param>
        /// <returns></returns>
        internal PremezclaDistribucionDetalleInfo GuardarPremezclaDistribucionDetalle(PremezclaDistribucionDetalleInfo premezclaDistribucionDetalle)
        {
            PremezclaDistribucionDetalleInfo premezcla = null;
            try
            {
                Dictionary<string, object> parametros = AuxPremezclaDistribucionDetalleDAL.ObtenerParametrosGuardarPremezclaDistribucionDetalle(premezclaDistribucionDetalle);
                DataSet ds = Retrieve("PremezclaDistribucionDetalle_Crear", parametros);
                if (ValidateDataSet(ds))
                {
                    premezcla = MapPremezclaDistribucionDetalleDAL.ObtenerPremezclaDistribucionDetalle(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return premezcla;
        }
    }
}
