using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    public class CalidadMateriaPrimaDAL : DALBase
    {
        /// <summary>
        /// Guarda la Calidad de pase proceso
        /// </summary>
        /// <param name="indicadoresPaseProceso"></param>
        /// <param name="observaciones"></param>
        /// <param name="movimiento"></param>
        /// <param name="usuarioCreacionID"></param>
        internal void GuardarCalidadPaseProceso(string indicadoresPaseProceso
                                              , string observaciones, int movimiento
                                              , int usuarioCreacionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxCalidadMateriaPrimaDAL.ObtenerParametrosGuardarCalidadPaseProceso(indicadoresPaseProceso,
                                                                                         movimiento, observaciones
                                                                                         , usuarioCreacionID);
                Create("CalidadMateriaPrima_GuardarCalidadPaseProceso", parameters);
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
    }
}

