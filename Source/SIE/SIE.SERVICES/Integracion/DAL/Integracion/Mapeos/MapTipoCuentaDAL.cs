using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Services.Info.Info;
using SIE.Base.Infos;
using System.Data;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapTipoCuentaDAL
    {

        /// <summary>
        ///     Método  que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<TipoCuentaInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<TipoCuentaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select new TipoCuentaInfo
                     {
                         TipoCuentaID = info.Field<int>("TipoCuentaId"),
                         Descripcion = info.Field<string>("Descripcion"),
                         Activo = info.Field<bool>("Activo").BoolAEnum(),
                     }).ToList();

                var resultado =
                    new ResultadoInfo<TipoCuentaInfo>
                    {
                        Lista = lista,
                        TotalRegistros =
                            Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
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
        ///     Método  que obtiene una lista de todos los registros
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<TipoCuentaInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<TipoCuentaInfo> result =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoCuentaInfo
                         {
                             TipoCuentaID = info.Field<int>("TipoCuentaId"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///     Método  que obtiene un registro de TipoCuenta
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static TipoCuentaInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                TipoCuentaInfo result =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoCuentaInfo
                         {
                             TipoCuentaID = info.Field<int>("TipoCuentaId"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).First();

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///     Método  que obtiene una lista de TipoCuentas por tipo de TipoCuenta
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<TipoCuentaInfo> ObtenerPorTipoCuentaID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<TipoCuentaInfo> result =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoCuentaInfo
                         {
                             TipoCuentaID = info.Field<int>("TipoCuentaId"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             UsuarioCreacionID = info.Field<int>("UsuarioCreacionID"),
                             UsuarioModificacionID = info.Field<int>("UsuarioModificacionID")
                         }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
