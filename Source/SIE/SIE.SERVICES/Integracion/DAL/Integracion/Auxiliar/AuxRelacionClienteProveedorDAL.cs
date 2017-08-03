using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Auxiliar;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxRelacionClienteProveedorDAL
    {


        /// <summary>
        ///  Obtener parametros por  clienteID
        /// </summary>
        /// <param name="clienteID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPorProveedorID(int proveedorID, int organizacionID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                    {"@ProveedorID", proveedorID},
                    {"@OrganizacionID", organizacionID},
                };
            
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;

        }

    }
}
