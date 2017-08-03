using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapEntradaGanadoTransitoDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<EntradaGanadoTransitoInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtDetalle = ds.Tables[ConstantesDAL.DtDetalle];
                List<EntradaGanadoTransitoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new EntradaGanadoTransitoInfo
                             {
                                 EntradaGanadoTransitoID = info.Field<int>("EntradaGanadoTransitoID"),
                                 Lote =
                                     new LoteInfo { LoteID = info.Field<int>("LoteID"), Lote = info.Field<string>("Lote") },
                                 Cabezas = info.Field<int>("Cabezas"),
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                                 Peso = info.Field<int>("Peso"),
                                 EntradasGanadoTransitoDetalles = (from det in dtDetalle.AsEnumerable()
                                                                   where det.Field<int>("EntradaGanadoTransitoID") == info.Field<int>("EntradaGanadoTransitoID")
                                                                   select new EntradaGanadoTransitoDetalleInfo
                                                                              {
                                                                                  Costo = new CostoInfo
                                                                                              {
                                                                                                  CostoID = det.Field<int>("CostoID"),
                                                                                              },
                                                                                  EntradaGanadoTransitoDetalleID = det.Field<int>("EntradaGanadoTransitoDetalleID"),
                                                                                  EntradaGanadoTransitoID = det.Field<int>("EntradaGanadoTransitoID"),
                                                                                  Importe = det.Field<decimal>("Importe"),
                                                                              }).ToList(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<EntradaGanadoTransitoInfo>
                        {
                            Lista = lista,
                            TotalRegistros = totalRegistros
                        };
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un mapeo de entrada ganado transito
        /// </summary>
        /// <returns></returns>
        internal static IEnumerable<EntradaGanadoTransitoInfo> ObtenerMapeoEntradaGanadoTransito(IDataReader reader)
        {
            var entradasGanadoTransito = new List<EntradaGanadoTransitoInfo>();
            try
            {
                Logger.Info();
                EntradaGanadoTransitoInfo entradaGanadoTransito;
                while (reader.Read())
                {
                    entradaGanadoTransito = new EntradaGanadoTransitoInfo
                    {
                        EntradaGanadoTransitoID = Convert.ToInt32(reader["EntradaGanadoTransitoID"]),
                        Cabezas = Convert.ToInt32(reader["Cabezas"]),
                        Lote = new LoteInfo
                        {
                            LoteID = Convert.ToInt32(reader["LoteID"]),
                            Lote = Convert.ToString(reader["Lote"]),
                            Corral = new CorralInfo
                            {
                                CorralID = Convert.ToInt32(reader["CorralID"]),
                                Codigo = Convert.ToString(reader["Codigo"])
                            },
                            CorralID = Convert.ToInt32(reader["CorralID"]),
                        },
                        Peso = Convert.ToInt32(reader["Peso"])
                    };
                    entradasGanadoTransito.Add(entradaGanadoTransito);
                }
                reader.NextResult();
                var detalles = new List<EntradaGanadoTransitoDetalleInfo>();
                EntradaGanadoTransitoDetalleInfo detalle;
                while (reader.Read())
                {
                    detalle = new EntradaGanadoTransitoDetalleInfo
                    {
                        Costo = new CostoInfo
                        {
                            CostoID = Convert.ToInt32(reader["CostoID"]),
                        },
                        EntradaGanadoTransitoDetalleID = Convert.ToInt32(reader["EntradaGanadoTransitoDetalleID"]),
                        EntradaGanadoTransitoID = Convert.ToInt32(reader["EntradaGanadoTransitoID"]),
                        Importe = Convert.ToDecimal(reader["Importe"])
                    };
                    detalles.Add(detalle);
                }
                entradasGanadoTransito.ToList()
                                      .ForEach(dato =>
                                      {
                                          dato.EntradasGanadoTransitoDetalles =
                                                detalles.Where(id => id.EntradaGanadoTransitoID
                                                                  == dato.EntradaGanadoTransitoID).ToList();
                                      });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return entradasGanadoTransito;
        }
    }
}
