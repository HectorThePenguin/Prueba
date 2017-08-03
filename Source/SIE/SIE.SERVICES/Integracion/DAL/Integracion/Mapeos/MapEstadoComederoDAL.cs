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
    internal  class MapEstadoComederoDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static ResultadoInfo<EstadoComederoInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<EstadoComederoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new EstadoComederoInfo
                             {
								EstadoComederoID = info.Field<int>("EstadoComederoID"),
								Descripcion = info.Field<string>("Descripcion"),
								DescripcionCorta = info.Field<string>("DescripcionCorta"),
								NoServir = info.Field<bool>("NoServir"),
								AjusteBase = info.Field<decimal>("AjusteBase"),
								Tendencia = info.Field<string>("Tendencia").StringATendenciaEnum(),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<EstadoComederoInfo>
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
        internal  static List<EstadoComederoInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<EstadoComederoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new EstadoComederoInfo
                             {
								EstadoComederoID = info.Field<int>("EstadoComederoID"),
								Descripcion = info.Field<string>("Descripcion"),
								DescripcionCorta = info.Field<string>("DescripcionCorta"),
								NoServir = info.Field<bool>("NoServir"),
								AjusteBase = info.Field<decimal>("AjusteBase"),
                                Tendencia = info.Field<string>("Tendencia").StringATendenciaEnum(),
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
        internal  static EstadoComederoInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                EstadoComederoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new EstadoComederoInfo
                             {
								EstadoComederoID = info.Field<int>("EstadoComederoID"),
								Descripcion = info.Field<string>("Descripcion"),
								DescripcionCorta = info.Field<string>("DescripcionCorta"),
								NoServir = info.Field<bool>("NoServir"),
								AjusteBase = info.Field<decimal>("AjusteBase"),
                                Tendencia = info.Field<string>("Tendencia").StringATendenciaEnum(),
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
        internal  static EstadoComederoInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                EstadoComederoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new EstadoComederoInfo
                             {
								EstadoComederoID = info.Field<int>("EstadoComederoID"),
								Descripcion = info.Field<string>("Descripcion"),
								DescripcionCorta = info.Field<string>("DescripcionCorta"),
								NoServir = info.Field<bool>("NoServir"),
								AjusteBase = info.Field<decimal>("AjusteBase"),
                                Tendencia = info.Field<string>("Tendencia").StringATendenciaEnum(),
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

