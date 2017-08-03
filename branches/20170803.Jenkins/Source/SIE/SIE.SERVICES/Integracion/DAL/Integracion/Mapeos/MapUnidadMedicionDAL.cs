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
    internal class MapUnidadMedicionDAL
    {
        /// <summary>
        /// Obtiene Info de Unidad Medicion
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static UnidadMedicionInfo ObtenerPorID(DataSet ds)
        {
            UnidadMedicionInfo resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select new UnidadMedicionInfo
                                        {
                                            UnidadID = info.Field<int>("UnidadID"),
                                            Activo = info.Field<bool>("Activo").BoolAEnum(),
                                            ClaveUnidad = info.Field<string>("ClaveUnidad"),
                                            Descripcion = info.Field<string>("Descripcion"),
                                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene Info de Resultado correspondiente
        /// a la Unidad de Medicion
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<UnidadMedicionInfo> ObtenerPorPagina(DataSet ds)
        {
            ResultadoInfo<UnidadMedicionInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<UnidadMedicionInfo> lista = (from info in dt.AsEnumerable()
                                                  select new UnidadMedicionInfo
                                                             {
                                                                 UnidadID = info.Field<int>("UnidadID"),
                                                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                                                                 ClaveUnidad = info.Field<string>("ClaveUnidad"),
                                                                 Descripcion = info.Field<string>("Descripcion"),
                                                             }).ToList();
                resultado = new ResultadoInfo<UnidadMedicionInfo>
                {
                    Lista = lista,
                    TotalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Método que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<UnidadMedicionInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<UnidadMedicionInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new UnidadMedicionInfo
                         {
                             UnidadID = info.Field<int>("UnidadID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             ClaveUnidad = info.Field<string>("ClaveUnidad"),
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

        internal static IList<UnidadMedicionInfo> ObtenerTodos(IDataReader reader)
        {
            try
            {
                Logger.Info();
                var lista = new List<UnidadMedicionInfo>();
                UnidadMedicionInfo elemento;
                while (reader.Read())
                {
                    elemento = new UnidadMedicionInfo
                    {
                        UnidadID = Convert.ToInt32(reader["UnidadID"]),
                        Descripcion = Convert.ToString(reader["Descripcion"]),
                        ClaveUnidad = Convert.ToString(reader["ClaveUnidad"]),
                        Activo = Convert.ToBoolean(reader["Activo"]).BoolAEnum(),
                    };
                    lista.Add(elemento);
                }
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
