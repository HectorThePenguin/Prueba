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
    public class MapTiempoMuertoDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<TiempoMuertoInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<TiempoMuertoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new TiempoMuertoInfo
                             {
								TiempoMuertoID = info.Field<int>("TiempoMuertoID"),
								ProduccionDiaria = new ProduccionDiariaInfo { ProduccionDiariaID = info.Field<int>("ProduccionDiariaID")},
								RepartoAlimento = new RepartoAlimentoInfo { RepartoAlimentoID = info.Field<int>("RepartoAlimentoID")},
								HoraInicio = info.Field<string>("HoraInicio"),
								HoraFin = info.Field<string>("HoraFin"),
								CausaTiempoMuerto = new CausaTiempoMuertoInfo { CausaTiempoMuertoID = info.Field<int>("CausaTiempoMuertoID"), Descripcion = info.Field<string>("CausaTiempoMuerto") },
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<TiempoMuertoInfo>
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
        public static List<TiempoMuertoInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<TiempoMuertoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new TiempoMuertoInfo
                             {
								TiempoMuertoID = info.Field<int>("TiempoMuertoID"),
								ProduccionDiaria = new ProduccionDiariaInfo { ProduccionDiariaID = info.Field<int>("ProduccionDiariaID")},
								RepartoAlimento = new RepartoAlimentoInfo { RepartoAlimentoID = info.Field<int>("RepartoAlimentoID")},
								HoraInicio = info.Field<string>("HoraInicio"),
								HoraFin = info.Field<string>("HoraFin"),
								CausaTiempoMuerto = new CausaTiempoMuertoInfo { CausaTiempoMuertoID = info.Field<int>("CausaTiempoMuertoID"), Descripcion = info.Field<string>("CausaTiempoMuerto") },
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
        public static TiempoMuertoInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                TiempoMuertoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new TiempoMuertoInfo
                             {
								TiempoMuertoID = info.Field<int>("TiempoMuertoID"),
								ProduccionDiaria = new ProduccionDiariaInfo { ProduccionDiariaID = info.Field<int>("ProduccionDiariaID")},
								RepartoAlimento = new RepartoAlimentoInfo { RepartoAlimentoID = info.Field<int>("RepartoAlimentoID")},
								HoraInicio = info.Field<string>("HoraInicio"),
								HoraFin = info.Field<string>("HoraFin"),
								CausaTiempoMuerto = new CausaTiempoMuertoInfo { CausaTiempoMuertoID = info.Field<int>("CausaTiempoMuertoID"), Descripcion = info.Field<string>("CausaTiempoMuerto") },
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
        public static TiempoMuertoInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                TiempoMuertoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new TiempoMuertoInfo
                             {
								TiempoMuertoID = info.Field<int>("TiempoMuertoID"),
								ProduccionDiaria = new ProduccionDiariaInfo { ProduccionDiariaID = info.Field<int>("ProduccionDiariaID")},
								RepartoAlimento = new RepartoAlimentoInfo { RepartoAlimentoID = info.Field<int>("RepartoAlimentoID")},
								HoraInicio = info.Field<string>("HoraInicio"),
								HoraFin = info.Field<string>("HoraFin"),
								CausaTiempoMuerto = new CausaTiempoMuertoInfo { CausaTiempoMuertoID = info.Field<int>("CausaTiempoMuertoID"), Descripcion = info.Field<string>("CausaTiempoMuerto") },
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
    }
}

