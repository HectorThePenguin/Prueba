using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class TipoCancelacionDAL : DALBase
    {
        /// <summary>
        /// Metodo para obtener la lista de los tipos de cancelacion activos
        /// </summary>
        /// <returns></returns>
        internal List<TipoCancelacionInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var ds = Retrieve("TipoCancelacion_ObtenerTodos");
                List<TipoCancelacionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoCancelacionDAL.ObtenerTodos(ds);
                }
                return result;
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
