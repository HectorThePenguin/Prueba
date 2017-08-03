using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using System.Linq;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxMermaEsperadaDAL
    {
        /// <summary>
        /// Obtiene los parametros para realizar la busqueda por OrganizacionOrigenID
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerMermaPorOrganizacionOrigenID(MermaEsperadaInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionOrigenID", filtro.OrganizacionOrigen.OrganizacionID},
                            {"@Activo", filtro.Activo}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene los parametros para guardar una lista de mermas
        /// </summary>
        /// <param name="mermas"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardar(List<MermaEsperadaInfo> mermas)
        {
            try
            {
                var xml =
                       new XElement("ROOT",
                                        new XElement("MermaEsperada",
                                                        from merma in mermas
                                                        select
                                                            new XElement("MermaObjeto",
                                                                new XElement("OrganizacionOrigenID", merma.OrganizacionOrigen.OrganizacionID),
                                                                new XElement("OrganizacionDestinoID", merma.OrganizacionDestino.OrganizacionID),
                                                                new XElement("Merma", merma.Merma),
                                                                new XElement("UsuarioID", merma.UsuarioCreacionId),
                                                                new XElement("Nuevo", merma.Nuevo)
                                                                )
                                        )
                    );

                var parametros =
                    new Dictionary<string, object>
                            {
                                {"@XmlMermas", xml.ToString()}
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
