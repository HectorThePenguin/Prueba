
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using System;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxProduccionFormulaDetalleDAL
    {
        /// <summary>
        /// Obtiene los parametros para guardar el detalle
        /// </summary>
        /// <param name="produccionFormulaDetalle"></param>
        /// <returns></returns>
        internal static Dictionary<string,object> GuardarProduccionFormulaDetalle(List<ProduccionFormulaDetalleInfo> produccionFormulaDetalle)
        {
            Logger.Info();
            var xml =
               new XElement("ROOT",
                        new XElement("ProduccionFormula",
                                    from produccionFormulaDetalleInfo in produccionFormulaDetalle
                                    select new XElement("ProduccionFormulaDetalle",
                                    new XElement("ProduccionFormulaID", produccionFormulaDetalleInfo.ProduccionFormulaId),
                                    new XElement("ProductoID", produccionFormulaDetalleInfo.Producto.ProductoId),
                                    new XElement("CantidadProducto", produccionFormulaDetalleInfo.CantidadProducto),
                                    new XElement("IngredienteID", produccionFormulaDetalleInfo.Ingrediente.IngredienteId),
                                    new XElement("UsuarioCreacionID", produccionFormulaDetalleInfo.UsuarioCreacionId)
                                )
                        )
            );
            var parametros =
                new Dictionary<string, object>
                        {
                            {"@XMLProduccionFormulaDetalle", xml.ToString()}
                        };
            return parametros;
        }
    }
}
