
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
    class AuxEntradaProductoParcialDAL
    {
        internal static Dictionary<string, object> ObtenerParametrosCrear(ContenedorEntradaMateriaPrimaInfo contenedorEntradaMateriaPrima)
        {
            try
            {

                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in contenedorEntradaMateriaPrima.Contrato.ListaContratoParcial.Where(registro => registro.Seleccionado)
                                 select new XElement("EntradaProductoParcial",
                                        new XElement("EntradaProductoID", contenedorEntradaMateriaPrima.EntradaProducto.EntradaProductoId),
                                        new XElement("ContratoParcialID", detalle.ContratoParcialId),
                                        new XElement("CantidadEntrante", (int)detalle.CantidadEntrante),
                                        new XElement("UsuarioCreacionID", contenedorEntradaMateriaPrima.UsuarioId)
                                     ));
                var parametros = new Dictionary<string, object>
                {
                    {"@XMLEntradaProductoParcial", xml.ToString()}
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
