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
    internal class MapEntradaMateriaPrima
    {
        /// <summary>
        /// Obtiene los datos de los contos de los fletes
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<CostoEntradaMateriaPrimaInfo> ObtenerCostosFletes(DataSet ds)
        {
            List<CostoEntradaMateriaPrimaInfo> lista = null;

            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                
                lista = (from info in dt.AsEnumerable()
                         select new CostoEntradaMateriaPrimaInfo
                         {
                             Flete = new FleteInfo
                                         {
                                             ContratoID = info.Field<int>("ContratoID"),
                                             Observaciones = info.Field<string>("Observaciones"),
                                             MermaPermitida = info.Field<decimal>("MermaPermitida"),
                                             Proveedor =    new ProveedorInfo{ProveedorID = info.Field<int>("ProveedorID")},
                                             FleteID = info.Field<int>("FleteId")
                                         },
                              FleteDetalle = new FleteDetalleInfo
                              {
                                  FleteDetalleID = info.Field<int>("FleteDetalleID"),
                                  CostoID = info.Field<int>("CostoID"),
                                  Tarifa = info.Field<decimal>("Tarifa")
                              },
                              EsFlete = true
                         }).ToList();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        /// <summary>
        /// Obtiene la merma permitida
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static CostoEntradaMateriaPrimaInfo ObtenerMermaPermitida(DataSet ds)
        {

            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CostoEntradaMateriaPrimaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CostoEntradaMateriaPrimaInfo
                         {
                             Flete = new FleteInfo
                             {
                                 ContratoID = info.Field<int>("ContratoID"),
                                 Observaciones = info.Field<string>("Observaciones"),
                                 MermaPermitida = info.Field<decimal>("MermaPermitida"),
                                 Proveedor = new ProveedorInfo { ProveedorID = info.Field<int>("ProveedorID") },
                                 FleteID = info.Field<int>("FleteId")
                             },
                             EsFlete = true
                         }).First();

                return entidad;

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

        }
    }
}
