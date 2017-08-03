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
    class MapZonaDAL
    {
        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<ZonaInfo> ObtenerPorPagina(DataSet ds)
        {
            ResultadoInfo<ZonaInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<ZonaInfo> lista = (from zonaInfo in dt.AsEnumerable()
                                         select new ZonaInfo
                                         {
                                             ZonaID = zonaInfo.Field<int>("ZonaID"),
                                             Descripcion = zonaInfo.Field<string>("Descripcion"),
                                             Pais = new PaisInfo
                                             {
                                                 PaisID=zonaInfo.Field<int>("PaisID"),
                                                 Descripcion=zonaInfo.Field<string>("Pais")
                                             },
                                             Activo = zonaInfo.Field<bool>("Activo").BoolAEnum()
                                         }).ToList();
                resultado = new ResultadoInfo<ZonaInfo>
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
        internal static ZonaInfo ObtenerPorID(DataSet ds)
        {
            ZonaInfo zonaInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                zonaInfo = (from zona in dt.AsEnumerable()
                             select new ZonaInfo
                             {
                                 ZonaID = zona.Field<int>("ZonaID"),
                                 Descripcion = zona.Field<string>("Zona"),
                                 Pais = new PaisInfo
                                 {
                                     PaisID = zona.Field<int>("PaisID"),
                                     Descripcion = zona.Field<string>("Pais")
                                 }
                             }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return zonaInfo;
        }

        /// <summary>
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ZonaInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ZonaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ZonaInfo
                         {
                             ZonaID = info.Field<int>("ZonaID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Pais = new PaisInfo
                             {
                                 PaisID = info.Field<int>("PaisID"),
                                 Descripcion = info.Field<string>("Pais")
                             },
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
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ZonaInfo ObtenerPorPaisID(DataSet ds)
        {
            ZonaInfo zonaInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                zonaInfo = (from zona in dt.AsEnumerable()
                             select new ZonaInfo
                             {
                                 ZonaID = zona.Field<int>("ZonaID"),
                                 Descripcion = zona.Field<string>("Descripcion"),
                                 Pais = new PaisInfo 
                                    {
                                        PaisID = zona.Field<int>("PaisID"),
                                        Descripcion = zona.Field<string>("Pais")
                                    }
                             }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return zonaInfo;
        }
    }
}
