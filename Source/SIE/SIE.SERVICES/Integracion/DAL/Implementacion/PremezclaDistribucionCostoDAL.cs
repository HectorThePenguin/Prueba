using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using BLToolkit.Data;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    class PremezclaDistribucionCostoDAL : DALBase
    {
        internal List<PremezclaDistribucionCostoInfo> GuardarPremezclaDistribucionCosto(DistribucionDeIngredientesInfo distribucionIngredientes)
        {
            Logger.Info();
            var premezcla = new List<PremezclaDistribucionCostoInfo>();
            try
            {
                Dictionary<string, object> parametros = AuxPremezclaDistribucionCostoDAL.ObtenerParametrosGuardarPremezclaDistribucionCosto(distribucionIngredientes);
                DataSet ds = Retrieve("PremezclaDistribucionCosto_Crear", parametros);
                if (ValidateDataSet(ds))
                {
                    premezcla = MapPremezclaDistribucionCostoDAL.ObtenerPremezclaDistribucionCosto(ds);
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
