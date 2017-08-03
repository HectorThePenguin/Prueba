
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxSolicitudPremezclaDetalleDAL
    {
        internal static Dictionary<string,object> ObtenerParametrosGuardar(List<SolicitudPremezclaDetalleInfo> listaSolicitudPremezclaDetalle)
        {
            try
            {
                Logger.Info();
                var xml =
                   new XElement("ROOT",
                       from solicitudPremezclaDetalleInfo in listaSolicitudPremezclaDetalle
                            select
                                    new XElement("PremezclaDetalle",
                                                 new XElement("SolicitudPremezclaID", solicitudPremezclaDetalleInfo.SolicitudPremezclaId),
                                                 new XElement("FechaLlegada", solicitudPremezclaDetalleInfo.FechaLlegada),
                                                 new XElement("PremezclaID", solicitudPremezclaDetalleInfo.Premezcla.PremezclaId),
                                                 new XElement("CantidadSolicitada", solicitudPremezclaDetalleInfo.CantidadSolicitada),
                                                 new XElement("UsuarioCreacionID", solicitudPremezclaDetalleInfo.UsuarioCreacion.UsuarioID),
                                                 new XElement("Activo", solicitudPremezclaDetalleInfo.Activo.GetHashCode())
                                                )
                                    );

                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XMLPremezclaDetalle", xml.ToString()}
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
