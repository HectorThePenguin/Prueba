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
    public class MapTipoServicioDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<TipoServicioInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<TipoServicioInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoServicioInfo
                             {
								TipoServicioId = info.Field<int>("TipoServicioID"),
								HoraInicio = info.Field<string>("HoraInicio"),
								HoraFin = info.Field<string>("HoraFin"),
								Descripcion = info.Field<string>("Descripcion"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<TipoServicioInfo>
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
        public static List<TipoServicioInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<TipoServicioInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoServicioInfo
                             {
								TipoServicioId = info.Field<int>("TipoServicioID"),
								HoraInicio = info.Field<string>("HoraInicio"),
								HoraFin = info.Field<string>("HoraFin"),
								Descripcion = info.Field<string>("Descripcion"),
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
        public static TipoServicioInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                TipoServicioInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoServicioInfo
                             {
								TipoServicioId = info.Field<int>("TipoServicioID"),
								HoraInicio = info.Field<string>("HoraInicio"),
								HoraFin = info.Field<string>("HoraFin"),
								Descripcion = info.Field<string>("Descripcion"),
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
        public static TipoServicioInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                TipoServicioInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoServicioInfo
                             {
								TipoServicioId = info.Field<int>("TipoServicioID"),
								HoraInicio = info.Field<string>("HoraInicio"),
								HoraFin = info.Field<string>("HoraFin"),
								Descripcion = info.Field<string>("Descripcion"),
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

