using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using System.Xml.Linq;
using System.Linq;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal static class AuxRecibirProductoAlmacenReplicaDAL
    {
        internal static Dictionary<string, object> ObtenerParametros(List<string> aretes, int organizacionId, int usuarioId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                     new XElement("ROOT",
                                  from a in aretes
                                  select new XElement("DATOS",
                                             new XElement("NumeroAreteSukarne", a)
                                      ));
                
                parametros = new Dictionary<string, object>
                    {
                        {"@XML", xml.ToString()},
                        {"@OrganizacionID", organizacionId.ToString()},
                        {"@UsuarioID", usuarioId.ToString()}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametros(List<string> aretes, int organizacionId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                     new XElement("ROOT",
                                  from a in aretes
                                  select new XElement("DATOS",
                                             new XElement("NumeroAreteSukarne", a)
                                      ));

                parametros = new Dictionary<string, object>
                    {
                        {"@XML", xml.ToString()},
                        {"@OrganizacionID", organizacionId.ToString()}
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
