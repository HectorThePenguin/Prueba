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
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    public class MapLoteDistribucionAlimentoDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<LoteDistribucionAlimentoInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<LoteDistribucionAlimentoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new LoteDistribucionAlimentoInfo
                             {
								LoteDistribucionAlimentoID = info.Field<int>("LoteDistribucionAlimentoID"),
								Lote = new LoteInfo { LoteID = info.Field<int>("LoteID"), Lote = info.Field<string>("Lote") },
								TipoServicio = new TipoServicioInfo { TipoServicioId = info.Field<int>("TipoServicioID"), Descripcion = info.Field<string>("TipoServicio") },
								EstatusDistribucion = new EstatusInfo { EstatusId = info.Field<int>("EstatusID"), Descripcion = info.Field<string>("Estatus") },
								Fecha = info.Field<DateTime>("Fecha"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<LoteDistribucionAlimentoInfo>
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
        public static List<LoteDistribucionAlimentoInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<LoteDistribucionAlimentoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new LoteDistribucionAlimentoInfo
                             {
                                 LoteDistribucionAlimentoID = info.Field<int>("LoteDistribucionAlimentoID"),
                                 Lote = new LoteInfo { LoteID = info.Field<int>("LoteID"), Lote = info.Field<string>("Lote") },
                                 TipoServicio = new TipoServicioInfo { TipoServicioId = info.Field<int>("TipoServicioID"), Descripcion = info.Field<string>("TipoServicio") },
                                 EstatusDistribucion = new EstatusInfo { EstatusId = info.Field<int>("EstatusID"), Descripcion = info.Field<string>("Estatus") },
                                 Fecha = info.Field<DateTime>("Fecha"),
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
        public static LoteDistribucionAlimentoInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                LoteDistribucionAlimentoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new LoteDistribucionAlimentoInfo
                             {
                                 LoteDistribucionAlimentoID = info.Field<int>("LoteDistribucionAlimentoID"),
                                 Lote = new LoteInfo { LoteID = info.Field<int>("LoteID"), Lote = info.Field<string>("Lote") },
                                 TipoServicio = new TipoServicioInfo { TipoServicioId = info.Field<int>("TipoServicioID"), Descripcion = info.Field<string>("TipoServicio") },
                                 EstatusDistribucion = new EstatusInfo { EstatusId = info.Field<int>("EstatusID"), Descripcion = info.Field<string>("Estatus") },
                                 Fecha = info.Field<DateTime>("Fecha"),
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
        /// Método que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static IList<ImpresionDistribucionAlimentoModel> ObtenerImpresionDistribucionAlimento(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ImpresionDistribucionAlimentoModel> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ImpresionDistribucionAlimentoModel
                         {
                             Fecha = info.Field<DateTime>("Fecha"),
                             Lector = info.Field<string>("Lector"),
                             Corral = info.Field<string>("Corral"),
                             Camion = info.Field<string>("Camion"),
                             Estatus = info.Field<string>("Estatus"),
                             DescripcionCorta = info.Field<string>("DescripcionCorta"),
                             TipoServicio = new TipoServicioInfo
                                 {
                                     TipoServicioId = info.Field<int>("TipoServicioID"),
                                     Descripcion = info.Field<string>("TipoServicio")
                                 }
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

