using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapAjusteDeInventarioDAL
    {
        /// <summary>
        /// Asigna el listado de diferencias de inventario obtenidas
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<AjusteDeInventarioDiferenciasInventarioInfo> ObtenerDiferenciasInventario(DataSet ds)
        {
            List<AjusteDeInventarioDiferenciasInventarioInfo> ajusteDeInventarioDiferenciasInventarioInfo;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                ajusteDeInventarioDiferenciasInventarioInfo = (from info in dt.AsEnumerable()
                                        select new AjusteDeInventarioDiferenciasInventarioInfo
                                        {
                                                Seleccionado = true,
                                                ProductoID = info.Field<int>("ProductoID"),
                                                Descripcion = info.Field<string>("Descripcion"),
                                                LoteAlmacenado = info.Field<int>("LoteAlmacenado"),
                                                UnidadMedida = info.Field<string>("ClaveUnidad"),
                                                Cantidad = info.Field<decimal>("Cantidad"),
                                                Precio = info.Field<decimal>("Precio"),
                                                Importe = info.Field<decimal>("Importe"),
                                                CantidadInventarioTeorico = info.Field<decimal>("CantidadInventarioTeorico"),
                                                PrecioInventarioTeorico = info.Field<decimal>("PrecioInventarioTeorico"),
                                                AlmacenInventarioID = info.Field<int>("AlmacenInventarioID"),
                                                AlmacenMovimientoDetalleID = info.Field<long>("AlmacenMovimientoDetalleID"),
                                        }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return ajusteDeInventarioDiferenciasInventarioInfo;
        }
    }
}
