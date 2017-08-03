using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapEnfermeriaCorralDAL
    {
        /// <summary>
        /// Obtiene los corrales en los que fueron detectados animales enfermo
        /// </summary>
        /// <param name="ds">Data set que contiene los datos</param>
        /// <returns>Lista de los corrales con ganado enfermo</returns>
        internal static List<EnfermeriaCorralInfo> ObtenerCorralesEnfermeria(DataSet ds)
        {
            List<EnfermeriaCorralInfo> result;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];


                result = (from info in dt.AsEnumerable()
                          select new EnfermeriaCorralInfo
                                               {
                                                   EnfermeriaCorralID = info.Field<int>("EnfermeriaCorralID"),
                                                   Corral = new CorralInfo
                                                       {
                                                           CorralID = info.Field<int>("CorralID"), 
                                                           Codigo = info.Field<string>("Codigo")
                                                       },
                                                   Activo = info.Field<bool>("Activo").BoolAEnum(),
                                                   
                                               }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return result;
        }
    }
}
