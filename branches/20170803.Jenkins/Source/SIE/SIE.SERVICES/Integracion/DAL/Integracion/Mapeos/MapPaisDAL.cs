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
    internal static class MapPaisDAL
    {
        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<PaisInfo> ObtenerPorPagina(DataSet ds)
        {
            ResultadoInfo<PaisInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<PaisInfo> lista = (from paisInfo in dt.AsEnumerable()
                                         select new PaisInfo
                                         {
                                             PaisID = paisInfo.Field<int>("PaisID"),
                                             Descripcion = paisInfo.Field<string>("Descripcion"),
                                             DescripcionCorta = paisInfo.Field<string>("DescripcionCorta"),
                                             Activo = paisInfo.Field<bool>("Activo").BoolAEnum()
                                         }).ToList();
                resultado = new ResultadoInfo<PaisInfo>
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
        internal static PaisInfo ObtenerPorID(DataSet ds)
        {
            PaisInfo paisInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                paisInfo = (from pais in dt.AsEnumerable()
                             select new PaisInfo
                             {
                                 PaisID = pais.Field<int>("PaisID"),
                                 Descripcion = pais.Field<string>("Descripcion"),
                                 DescripcionCorta = pais.Field<string>("DescripcionCorta")
                             }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return paisInfo;
        }

        /// <summary>
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static PaisInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                PaisInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new PaisInfo
                         {
                             PaisID = info.Field<int>("PaisID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             DescripcionCorta = info.Field<string>("DescripcionCorta"),
                             Activo = info.Field<bool>("Activo").BoolAEnum()
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
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static PaisInfo ObtenerPorPaisID(DataSet ds)
        {
            PaisInfo paisInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                paisInfo = (from pais in dt.AsEnumerable()
                             select new PaisInfo
                             {
                                 PaisID = pais.Field<int>("PaisID"),
                                 Descripcion = pais.Field<string>("Descripcion"),
                                 DescripcionCorta = pais.Field<string>("DescripcionCorta")
                             }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return paisInfo;
        }
    }
}
