using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxMuestreoTamanoFibraDAL
    {
        internal static Dictionary<string, object> ObtenerParametrosGuardar(List<MuestreoFibraProductoInfo> listaMuestreoFibraIngrediente)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xmlMuelstras =
                  new XElement("ROOT",
                               from muestra in listaMuestreoFibraIngrediente
                               select new XElement("Muestra",
                                          new XElement("OrganizacionID", muestra.Organizacion.OrganizacionID),
                                          new XElement("ProductoID", muestra.Producto.ProductoId),
                                          new XElement("PesoMuestra", muestra.PesoMuestra),
                                          new XElement("Grueso", muestra.Grueso),
                                          new XElement("PesoGranoGrueso", muestra.PesoGranoGrueso),
                                          new XElement("Mediano", muestra.Mediano),
                                          new XElement("PesoGranoMediano", muestra.PesoGranoMediano),
                                          new XElement("Fino", muestra.Fino),
                                          new XElement("PesoGranoFino", muestra.PesoGranoFino),
                                          new XElement("CribaEntrada", muestra.CribaEntrada),
                                          new XElement("CribaSalida", muestra.CribaSalida),
                                          new XElement("Observaciones", muestra.Observaciones),
                                          new XElement("UsuarioID", muestra.UsuarioCreacion.UsuarioID)
                               ));
                parametros = new Dictionary<string, object>
                {
                    {"@MuestrasXML", xmlMuelstras.ToString()}        
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosGuardar(List<MuestreoFibraFormulaInfo> listaMuestreoFibraFormula)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xmlMuestras =
                  new XElement("ROOT",
                               from muestra in listaMuestreoFibraFormula
                               select new XElement("Muestra",
                                      new XElement("OrganizacionID", muestra.Organizacion.OrganizacionID),
                                      new XElement("FormulaID", muestra.Formula.FormulaId),
                                      new XElement("PesoInicial", muestra.PesoInicial),
                                      new XElement("Grande", muestra.Grande),
                                      new XElement("PesoFibraGrande", muestra.PesoFibraGrande),
                                      new XElement("Mediana", muestra.Mediana),
                                      new XElement("PesoFibraMediana", muestra.PesoFibraMediana),
                                      new XElement("FinoTamiz", muestra.FinoTamiz),
                                      new XElement("PesoFinoTamiz", muestra.PesoFinoTamiz),
                                      new XElement("FinoBase", muestra.FinoBase),
                                      new XElement("PesoFinoBase", muestra.PesoFinoBase),
                                      new XElement("Origen", muestra.Origen),
                                      new XElement("UsuarioID", muestra.UsuarioCreacion.UsuarioID)
                               ));
                parametros = new Dictionary<string, object>
                {
                    {"@MuestrasXML", xmlMuestras.ToString()}        
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
