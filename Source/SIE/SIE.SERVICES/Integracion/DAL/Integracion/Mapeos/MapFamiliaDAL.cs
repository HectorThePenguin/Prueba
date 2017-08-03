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
    internal  class MapFamiliaDAL
    {
        /// <summary>
        /// Obtiene Info de Familia
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static FamiliaInfo ObtenerPorID(DataSet ds)
        {
            FamiliaInfo resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select new FamiliaInfo
                             {
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                                 Descripcion = info.Field<string>("Descripcion"),
                                 FamiliaID = info.Field<int>("FamiliaID"),
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
        /// a la Familia
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static ResultadoInfo<FamiliaInfo> ObtenerPorPagina(DataSet ds)
        {
            ResultadoInfo<FamiliaInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<FamiliaInfo> lista = (from info in dt.AsEnumerable()
                                           select new FamiliaInfo
                                                  {
                                                      Activo = info.Field<bool>("Activo").BoolAEnum(),
                                                      Descripcion = info.Field<string>("Descripcion"),
                                                      FamiliaID = info.Field<int>("FamiliaID"),
                                                  }).ToList();
                resultado = new ResultadoInfo<FamiliaInfo>
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
        internal  static List<FamiliaInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<FamiliaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new FamiliaInfo
                         {
                             FamiliaID = info.Field<int>("FamiliaID"),
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
        /// Método que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<FamiliaInfo> Centros_ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<FamiliaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new FamiliaInfo
                         {
                             FamiliaID = info.Field<int>("FamiliaID"),
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
        internal  static FamiliaInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                FamiliaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new FamiliaInfo
                         {
                             FamiliaID = info.Field<int>("FamiliaID"),
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
