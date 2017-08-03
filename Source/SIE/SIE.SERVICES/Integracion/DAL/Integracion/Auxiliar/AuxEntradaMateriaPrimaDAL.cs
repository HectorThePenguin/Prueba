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
    internal class AuxEntradaMateriaPrimaDAL
    {
        /// <summary>
        /// Obtiene los parametros para obtener los costos de los fletes
        /// </summary>
        /// <param name="contrato"></param>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCostosFletes(ContratoInfo contrato, EntradaProductoInfo entradaProducto)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@ContratoID", contrato.ContratoId},
                            {"@RegistroVigilanciaID", entradaProducto.RegistroVigilancia.RegistroVigilanciaId},
                            {"@OrganizacionID", contrato.Organizacion.OrganizacionID},
                            {"@Activo", (int)EstatusEnum.Activo}
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
        /// Metodo para obtener los parametros para guardar los costos de entrada de materia prima
        /// </summary>
        /// <param name="entradaMateriaPrima">Contiene los datos necesarios para guardar</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPaginaTiposProveedores(ContenedorEntradaMateriaPrimaInfo entradaMateriaPrima)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                var element = new XElement("ROOT",
                                                from costo in entradaMateriaPrima.ListaCostoEntradaMateriaPrima
                                                select new XElement("Datos",
                                                                    new XElement("EntradaProductoID",
                                                                                 entradaMateriaPrima.EntradaProducto.EntradaProductoId),
                                                                    new XElement("CostoID",costo.Costos.CostoID),
                                                                    new XElement("TieneCuenta",
                                                                                 costo.TieneCuenta),
                                                                    new XElement("ProveedorID",
                                                                                 costo.Provedor==null ? 0 : costo.Provedor.ProveedorID),
                                                                    new XElement("CuentaProvision",
                                                                                 costo.CuentaSap),
                                                                    new XElement("Importe",
                                                                                 costo.Importe),
                                                                    new XElement("Iva",
                                                                                 costo.Iva),
                                                                    new XElement("Retencion",
                                                                                 costo.Retencion),
                                                                    new XElement("TipoEntrada",
                                                                                 entradaMateriaPrima.TipoEntrada),
                                                                    new XElement("Observaciones",
                                                                                 entradaMateriaPrima.Observaciones)
                                                                                 ));

                parametros = new Dictionary<string, object>
                    {
                        {"@UsuarioID", entradaMateriaPrima.UsuarioId},
                        {"@Activo", EstatusEnum.Activo.GetHashCode()},
                        {"@xmlTipoCuenta", element.ToString()},
                        
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene los parametros de la merma permitida
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        internal static Dictionary <string, object> ObtenerParametrosMermaPermitida(EntradaProductoInfo entradaProducto)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@FolioEntradaProducto", entradaProducto.Folio},
                            {"@OrganizacionID", entradaProducto.Organizacion.OrganizacionID},
                            {"@Activo", (int)EstatusEnum.Activo}
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
