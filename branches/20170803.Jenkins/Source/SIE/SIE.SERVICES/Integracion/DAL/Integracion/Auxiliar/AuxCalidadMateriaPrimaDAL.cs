using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using System.Text;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    public class AuxCalidadMateriaPrimaDAL
    {
        /// <summary> 
        /// Metodo para Guardar Calidad pase proceso
        /// </summary> 
        /// <param name="indicadoresPaseProceso"></param>
        /// <param name="movimiento"> </param>
        /// <param name="observaciones"> </param>
        /// <param name="usuarioCreacionID"> </param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosGuardarCalidadPaseProceso(string indicadoresPaseProceso
                                                                                          , int movimiento
                                                                                          , string observaciones
                                                                                          , int usuarioCreacionID)
        {
            try
            {
                Logger.Info();

                var xDocument = XDocument.Parse(indicadoresPaseProceso);
                var semaforoIndicadores = xDocument.Descendants("semaforo")
                    .Select(element => new XElement("ROOT",
                                                    new XElement("semaforo",
                                                                 new XElement("PedidoDetalleID",
                                                                              movimiento == 2 ? "0" : element.Element("PedidoDetalleID").Value),
                                                                 new XElement("Movimiento",
                                                                              movimiento),
                                                                 new XElement("IndicadorObjetivoID",
                                                                              element.Element("IndicadorObjetivoID").
                                                                                  Value),
                                                                 new XElement("Resultado",
                                                                              element.Element("Resultado").Value),
                                                                 new XElement("ColorObjetivoID",
                                                                              element.Element("ColorObjetivoIDValor").
                                                                                  Value),
                                                                 new XElement("Observaciones", observaciones)
                                                        )).ToString()).ToList();
                var sb = new StringBuilder();
                if (semaforoIndicadores != null)
                {
                    semaforoIndicadores.ForEach(xml =>
                                                    {
                                                        sb.AppendLine(xml);
                                                    });
                }
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@xmlIndicadores", sb.ToString()},
                            {"@UsuarioCreacionID", usuarioCreacionID},
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

