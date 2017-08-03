using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    public class MapInterfaceSalidaTraspasoCostoDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<InterfaceSalidaTraspasoCostoInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<InterfaceSalidaTraspasoCostoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new InterfaceSalidaTraspasoCostoInfo
                             {
                                 InterfaceSalidaTraspasoCostoID = info.Field<int>("InterfaceSalidaTraspasoCostoID"),
                                 InterfaceSalidaTraspasoDetalle =
                                     new InterfaceSalidaTraspasoDetalleInfo
                                         {
                                             InterfaceSalidaTraspasoDetalleID =
                                                 info.Field<int>("InterfaceSalidaTraspasoDetalleID"),
                                         },
                                 AnimalID = info.Field<int>("AnimalID"),
                                 Costo =
                                     new CostoInfo
                                         {
                                             CostoID = info.Field<int>("CostoID"),
                                             Descripcion = info.Field<string>("Costo")
                                         },
                                 Importe = info.Field<decimal>("Importe"),
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<InterfaceSalidaTraspasoCostoInfo>
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
        /// Método que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static List<InterfaceSalidaTraspasoCostoInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<InterfaceSalidaTraspasoCostoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new InterfaceSalidaTraspasoCostoInfo
                             {
                                 InterfaceSalidaTraspasoCostoID = info.Field<int>("InterfaceSalidaTraspasoCostoID"),
                                 InterfaceSalidaTraspasoDetalle =
                                     new InterfaceSalidaTraspasoDetalleInfo
                                         {
                                             InterfaceSalidaTraspasoDetalleID =
                                                 info.Field<int>("InterfaceSalidaTraspasoDetalleID"),
                                         },
                                 AnimalID = info.Field<int>("AnimalID"),
                                 Costo =
                                     new CostoInfo
                                         {
                                             CostoID = info.Field<int>("CostoID"),
                                             Descripcion = info.Field<string>("Costo")
                                         },
                                 Importe = info.Field<decimal>("Importe"),
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static InterfaceSalidaTraspasoCostoInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                InterfaceSalidaTraspasoCostoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new InterfaceSalidaTraspasoCostoInfo
                             {
                                 InterfaceSalidaTraspasoCostoID = info.Field<int>("InterfaceSalidaTraspasoCostoID"),
                                 InterfaceSalidaTraspasoDetalle =
                                     new InterfaceSalidaTraspasoDetalleInfo
                                         {
                                             InterfaceSalidaTraspasoDetalleID =
                                                 info.Field<int>("InterfaceSalidaTraspasoDetalleID"),
                                         },
                                 AnimalID = info.Field<int>("AnimalID"),
                                 Costo =
                                     new CostoInfo
                                         {
                                             CostoID = info.Field<int>("CostoID"),
                                             Descripcion = info.Field<string>("Costo")
                                         },
                                 Importe = info.Field<decimal>("Importe"),
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).First();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static InterfaceSalidaTraspasoCostoInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                InterfaceSalidaTraspasoCostoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new InterfaceSalidaTraspasoCostoInfo
                             {
                                 InterfaceSalidaTraspasoCostoID = info.Field<int>("InterfaceSalidaTraspasoCostoID"),
                                 InterfaceSalidaTraspasoDetalle =
                                     new InterfaceSalidaTraspasoDetalleInfo
                                         {
                                             InterfaceSalidaTraspasoDetalleID =
                                                 info.Field<int>("InterfaceSalidaTraspasoDetalleID"),
                                         },
                                 AnimalID = info.Field<int>("AnimalID"),
                                 Costo =
                                     new CostoInfo
                                         {
                                             CostoID = info.Field<int>("CostoID"),
                                             Descripcion = info.Field<string>("Costo")
                                         },
                                 Importe = info.Field<decimal>("Importe"),
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).First();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de costos
        /// de traspaso
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<InterfaceSalidaTraspasoCostoInfo> ObtenerCostosInterfacePorDetalle(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<InterfaceSalidaTraspasoCostoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new InterfaceSalidaTraspasoCostoInfo
                         {
                             InterfaceSalidaTraspasoCostoID = info.Field<int>("InterfaceSalidaTraspasoCostoID"),
                             InterfaceSalidaTraspasoDetalle =
                                 new InterfaceSalidaTraspasoDetalleInfo
                                 {
                                     InterfaceSalidaTraspasoDetalleID =
                                         info.Field<int>("InterfaceSalidaTraspasoDetalleID"),
                                 },
                             AnimalID = info.Field<int>("AnimalID"),
                             Costo =
                                 new CostoInfo
                                 {
                                     CostoID = info.Field<int>("CostoID"),
                                 },
                             Importe = info.Field<decimal>("Importe"),
                         }).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
