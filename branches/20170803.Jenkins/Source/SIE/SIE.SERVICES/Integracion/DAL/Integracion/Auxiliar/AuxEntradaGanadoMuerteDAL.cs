using SIE.Services.Info.Info;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using SIE.Base.Log;
using System;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxEntradaGanadoMuerteDAL
    {
        /// <summary>
        /// Obtiene los parametros necesarios para 
        /// la ejecucion del procedimiento almacenado
        /// [EntradaGanadoMuerte_Actualizar]
        /// </summary>
        /// <param name="animales"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarMuertesXML(List<EntradaGanadoMuerteInfo> muertes)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from muerte in muertes
                                 select new XElement("Animal",
                                                     new XElement("Arete", muerte.Animal.Arete),
                                                     new XElement("EntradaGanadoID", muerte.EntradaGanado.EntradaGanadoID),
                                                     new XElement("EntradaGanadoMuerteID", muerte.EntradaGanadoMuerteID),
                                                     new XElement("Fecha", muerte.Fecha),
                                                     new XElement("FolioMuerte", muerte.FolioMuerte),
                                                     new XElement("Peso", Convert.ToInt32(muerte.Peso)),
                                                     new XElement("UsuarioModificacionID", muerte.UsuarioModificacionID)
                                     ));
                var detalles = new List<EntradaGanadoMuerteDetalleInfo>();
                muertes.ForEach(det => detalles.AddRange(det.EntradaGanadMuerteDetalle));
                var xmlDetalle =
                    new XElement("ROOT",
                                 from det in detalles
                                 select new XElement("Detalle",
                                                     new XElement("CostoID", det.Costo.CostoID),
                                                     new XElement("EntradaGanadoMuerteID", det.EntradaGanadoMuerte.EntradaGanadoMuerteID),
                                                     new XElement("Importe", det.Importe),
                                                     new XElement("UsuarioModificacionID", det.UsuarioModificacionID)
                                ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlMuertes", xml.ToString()},
                            {"@XmlDetalle", xmlDetalle.ToString()},
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
