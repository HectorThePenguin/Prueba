using System.Collections.Generic;
using SIE.Base.Integracion.DAL;
using SIE.Services.Info.Info;
using System.Data;
using System;
using SIE.Base.Log;
using SIE.Base.Exepciones;
using System.Reflection;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class PrecioPACDAL : DALBase
    {
        /// <summary>
        /// Obtiene un registro por id
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal PrecioPACInfo ObtenerPrecioPACActivo(int organizacionID)
        {
            PrecioPACInfo precioPac = null;
            try
            {
                var parametros = new Dictionary<string, object> {{"@OrganizacionID", organizacionID}};
                DataSet ds = Retrieve("PrecioPAC_ObtenerPorOrganizacion", parametros);
                if (ValidateDataSet(ds))
                {
                    precioPac = MapPrecioPACDAL.ObtenerActivoPorOrganizacion(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return precioPac;
        }
    }
}
