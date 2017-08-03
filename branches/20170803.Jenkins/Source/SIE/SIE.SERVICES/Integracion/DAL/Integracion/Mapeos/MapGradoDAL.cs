using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Base.Infos;
using SIE.Services.Info.Info;
using System.Data;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapGradoDAL
    {
        internal static IList<GradoInfo> ObtenerGrado(DataSet ds)
        {
            IList<GradoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<GradoInfo> lista = (from info in dt.AsEnumerable()
                                         select new GradoInfo
                                          {
                                              GradoID = info.Field<int>("GradoID"),
                                              Descripcion = info.Field<string>("Descripcion"),
                                              NivelGravedad = info.Field<string>("NivelGravedad"),
                                          }).ToList();

                resultado = lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<GradoInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<GradoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new GradoInfo
                         {
                             GradoID = info.Field<int>("GradoID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             NivelGravedad = info.Field<string>("NivelGravedad"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<GradoInfo>
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
        public static List<GradoInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<GradoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new GradoInfo
                         {
                             GradoID = info.Field<int>("GradoID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             NivelGravedad = info.Field<string>("NivelGravedad"),
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
        public static GradoInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                GradoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new GradoInfo
                         {
                             GradoID = info.Field<int>("GradoID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             NivelGravedad = info.Field<string>("NivelGravedad"),
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
        public static GradoInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                GradoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new GradoInfo
                         {
                             GradoID = info.Field<int>("GradoID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             NivelGravedad = info.Field<string>("NivelGravedad"),
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
