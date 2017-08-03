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
    internal static class MapTipoCostoDAL
    {
        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<TipoCostoInfo> ObtenerPorPagina(DataSet ds)
        {
            ResultadoInfo<TipoCostoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<TipoCostoInfo> lista = (from groupinfo in dt.AsEnumerable()
                                             select new TipoCostoInfo
                                         {
                                             TipoCostoID = groupinfo.Field<int>("TipoCostoID"),
                                             Descripcion = groupinfo.Field<string>("Descripcion"),
                                             Activo = groupinfo.Field<bool>("Activo").BoolAEnum(),
                                         }).ToList();
                resultado = new ResultadoInfo<TipoCostoInfo>
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
        internal static TipoCostoInfo ObtenerPorID(DataSet ds)
        {
            TipoCostoInfo costoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                costoInfo = (from costinfo in dt.AsEnumerable()
                             select new TipoCostoInfo
                             {
                                 TipoCostoID = costinfo.Field<int>("TipoCostoID"),
                                 Descripcion = costinfo.Field<string>("Descripcion"),
                                 Activo = costinfo.Field<bool>("Activo").BoolAEnum()
                             }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return costoInfo;
        }

         /// <summary>
        ///     Metodo que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<TipoCostoInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<TipoCostoInfo> lista =
                    (from groupinfo in dt.AsEnumerable()
                     select
                         new TipoCostoInfo
                             {
                                 TipoCostoID = groupinfo.Field<int>("TipoCostoID"),
                                 Descripcion = groupinfo.Field<string>("Descripcion"),
                                 Activo = groupinfo.Field<bool>("Activo").BoolAEnum(),
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
        internal static TipoCostoInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                TipoCostoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoCostoInfo
                         {
                             TipoCostoID = info.Field<int>("TipoCostoID"),
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


