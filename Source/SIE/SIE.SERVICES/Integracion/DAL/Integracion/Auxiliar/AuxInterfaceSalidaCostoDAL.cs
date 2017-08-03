using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxInterfaceSalidaCostoDAL
    {
        /// <summary>
        /// Obtener olos animales y sus costos en la interface salida
        /// </summary>
        /// <param name="folioOrigen"></param>
        /// <param name="organizacionOrigenID"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametroCostoAnimales(int folioOrigen, int organizacionOrigenID)
        {
            try
            {
                Logger.Info();
                
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@FolioOrigen", folioOrigen},
                            {"@OrganizacionOrigenID", organizacionOrigenID}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
