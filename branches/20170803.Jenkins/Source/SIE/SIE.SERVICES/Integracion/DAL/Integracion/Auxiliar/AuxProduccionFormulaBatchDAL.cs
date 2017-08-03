using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxProduccionFormulaBatchDAL
    {
        /// <summary>
        /// Guarda los datos capturados en el formulario "ProduccionFormula" en la tabla "ProduccionFormulaBatch"
        /// </summary>
        internal static Dictionary<string, object> GuardarProduccionFormulaBatch(ProduccionFormulaInfo produccionFormula)
        {
            try
            {
                var xml =
                        new XElement("ROOT",
                        from info in produccionFormula.ProduccionFormulaDetalle
                        select
                            new XElement("ListaProduccionFormula",
                                        new XElement("ProduccionFormulaID", info.ProduccionFormulaId),
                                        new XElement("CantidadProducto", Convert.ToInt32(Math.Round(info.CantidadProducto))),
                                        new XElement("ProductoId", info.Producto.ProductoId)
                            ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlListaProduccionFormula", xml.ToString()},
                            {"@OrganizacionID", produccionFormula.Organizacion.OrganizacionID},
                            {"@FormulaID", produccionFormula.Formula.FormulaId},
                            {"@RotomixID", produccionFormula.RotoMixID},
                            {"@Batch", produccionFormula.Batch},
                            {"@CantidadProgramada", Convert.ToDecimal(Numeros.ValorCero)},
                            {"@Activo", Convert.ToBoolean(EstatusEnum.Activo)},
                            {"@UsuarioCreacionID", produccionFormula.UsuarioCreacionId},

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
        /// Guarda los datos capturados en el formulario "ProduccionFormula" en la tabla "ProduccionFormulaBatch"
        /// </summary>
        internal static Dictionary<string, object> GuardarProduccionFormulaBatchLista(ProduccionFormulaInfo produccionFormula, int ProduccionFormulaId)
        {
            try
            {
                var xml =
                        new XElement("ROOT",
                        from info in produccionFormula.ProduccionFormulaBatch
                        select
                            new XElement("ListaProduccionFormulaBatch",
                                        new XElement("ProduccionFormulaID", ProduccionFormulaId),
                                        new XElement("OrganizacionID",info.OrganizacionID),
                                        new XElement("ProductoID", info.ProductoID),
                                        new XElement("FormulaID",info.FormulaID),
                                        new XElement("RotomixID",info.RotomixID),
                                        new XElement("Batch", info.Batch),
                                        new XElement("CantidadProgramada", info.CantidadProgramada),
                                        new XElement("CantidadReal", info.CantidadReal),
                                        new XElement("Activo",(int)info.Activo),
                                        new XElement("UsuarioCreacionID", produccionFormula.UsuarioCreacionId)
                            ));
                var parametros =
                new Dictionary<string, object>
                        {
                            {"@XmlProduccionFormulaBatch", xml.ToString()},
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
        /// OBtiene los parametros para el Sp ProduccionFormulaBatch_ValidarBatch
        /// </summary>
        internal static Dictionary<string, object> ObtenerParametrosValidarProduccionFormulaBatch(ProcesarArchivoInfo produccionFormula)
        {
            try
            {
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", produccionFormula.OrganizacionID},
                            {"@FormulaID", produccionFormula.FormulaID},
                            {"@RotomixID", produccionFormula.RotoMixID},
                            {"@Batch", produccionFormula.batch},
                            {"@Fecha", produccionFormula.FechaProduccion},
                            {"@ProductoID", produccionFormula.ProductoID},
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
