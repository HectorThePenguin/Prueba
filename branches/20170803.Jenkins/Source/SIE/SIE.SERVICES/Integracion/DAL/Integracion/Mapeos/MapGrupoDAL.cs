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
    internal static class MapGrupoDAL
    {
        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<GrupoInfo> ObtenerPorPagina(DataSet ds)
        {
            ResultadoInfo<GrupoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<GrupoInfo> lista = (from groupinfo in dt.AsEnumerable()
                                         select new GrupoInfo
                                             {
                                                 GrupoID = groupinfo.Field<int>("GrupoId"),
                                                 Descripcion = groupinfo.Field<string>("Descripcion"),
                                                 Activo = groupinfo.Field<bool>("Activo").BoolAEnum(),
                                             }).ToList();
                resultado = new ResultadoInfo<GrupoInfo>
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
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static GrupoInfo ObtenerPorID(DataSet ds)
        {
            GrupoInfo grupoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                grupoInfo = (from groupinfo in dt.AsEnumerable()
                             select new GrupoInfo
                                 {
                                     GrupoID = groupinfo.Field<int>("GrupoId"),
                                     Descripcion = groupinfo.Field<string>("Descripcion"),
                                     Activo = groupinfo.Field<bool>("Activo").BoolAEnum()
                                 }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return grupoInfo;
        }

        /// <summary>
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static GrupoInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                GrupoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new GrupoInfo
                         {
                             GrupoID = info.Field<int>("GrupoID"),
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
        ///  Método que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<GrupoInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                IList<GrupoInfo> entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new GrupoInfo
                             {
                                 GrupoID = info.Field<int>("GrupoID"),
                                 Descripcion = info.Field<string>("Descripcion"),
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();
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