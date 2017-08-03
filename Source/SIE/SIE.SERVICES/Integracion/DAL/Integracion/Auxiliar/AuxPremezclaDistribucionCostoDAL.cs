using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    class AuxPremezclaDistribucionCostoDAL
    {

        internal static Dictionary<string, object> ObtenerParametrosGuardarPremezclaDistribucionCosto(DistribucionDeIngredientesInfo distribucionIngredientes)
        {
            try
            {
                Logger.Info();

                var xml =
                    new XElement("ROOT",
                        from info in distribucionIngredientes.ListaPremezclaDistribucionCosto
                        select
                            new XElement("PremezclaDistribucionCosto",
                                        new XElement("PremezclaDistribucionID", distribucionIngredientes.PremezclaDistribucionID),
                                        new XElement("Costo", info.Costo.CostoID),
                                        new XElement("TieneCuenta", info.TieneCuenta),
                                        new XElement("Proveedor", info.Proveedor.ProveedorID),
                                        new XElement("CuentaProvision", info.CuentaSAP.CuentaSAP),
                                        new XElement("Importe", info.Importe),
                                        new XElement("Iva", info.Iva),
                                        new XElement("Retencion", info.Retencion),
                                        new XElement("Activo", info.Activo.GetHashCode()),
                                        new XElement("UsuarioCreacionID", info.UsuarioCreacionID),
                                        new XElement("UsuarioModificacionID", info.UsuarioModificacionID)
                            ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@PremezclaDistribucionCostoXML", xml.ToString()}
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
