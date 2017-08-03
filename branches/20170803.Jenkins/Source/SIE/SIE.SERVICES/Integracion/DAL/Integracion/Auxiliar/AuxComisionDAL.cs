using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxComisionDAL
    {
        /// <summary>
        /// Obtiene los parametros para guardar una lista de comisiones
        /// </summary>
        /// <param name="comisiones">Lista de comisiones a cargar</param>
        /// <returns>Lista de parametros para invocar el procedimiento almacenado</returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardar(List<ComisionInfo> comisiones)
        {
            try
            {
                XElement xml =
                       new XElement("ROOT",
                                        new XElement("ProveedorComisiones",
                                                        from comision in comisiones
                                                        select
                                                            new XElement("ComisionesObjeto",
                                                                new XElement("ProveedorComisionID", comision.ProveedorComisionID),
                                                                new XElement("ProveedorID", comision.ProveedorID),
                                                                new XElement("TipoComisionID", comision.TipoComisionID),
                                                                new XElement("Tarifa", comision.Tarifa),
                                                                new XElement("UsuarioID", comision.Usuario),
                                                                new XElement("Accion", comision.Accion)
                                                                )
                                        )
                    );

                Dictionary<string, object> parametros =
                    new Dictionary<string, object>
                            {
                                {"@XmlComisiones", xml.ToString()}
                            };
                return parametros;

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosComisionesProveedor(int ProveedorID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ProveedorID", ProveedorID}
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
