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
    internal class MapTipoCambioDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<TipoCambioInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<TipoCambioInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoCambioInfo
                         {
                             TipoCambioId = info.Field<int>("TipoCambioID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Moneda = new MonedaInfo
                                 {
                                     MonedaID = info.Field<int>("MonedaID"),
                                     Descripcion = info.Field<string>("Moneda")
                                 },
                             Cambio = info.Field<decimal>("Cambio"),
                             Fecha = info.Field<DateTime>("Fecha"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<TipoCambioInfo>
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
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static TipoCambioInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                TipoCambioInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoCambioInfo
                         {
                             TipoCambioId = info.Field<int>("TipoCambioID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Cambio = info.Field<decimal>("Cambio"),
                             Fecha = info.Field<DateTime>("Fecha"),
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
        internal static TipoCambioInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                TipoCambioInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoCambioInfo
                         {
                             TipoCambioId = info.Field<int>("TipoCambioID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Moneda = new MonedaInfo
                             {
                                 MonedaID = info.Field<int>("MonedaID"),
                                 Descripcion = info.Field<string>("Moneda")
                             },
                             Cambio = info.Field<decimal>("Cambio"),
                             Fecha = info.Field<DateTime>("Fecha"),
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
        /// Obtener 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<TipoCambioInfo> ObtenerTodos(DataSet ds)
        {
            List<TipoCambioInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                lista = (from info in dt.AsEnumerable()
                         select new TipoCambioInfo
                         {
                             TipoCambioId = info.Field<int>("TipoCambioID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Cambio = info.Field<decimal>("Cambio"),
                             Fecha = info.Field<DateTime>("Fecha"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             DescripcionTabla = info.Field<string>("Descripcion")
                         }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }

        /// <summary>
        /// Obtener tipo de cambio por id
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static TipoCambioInfo ObtenerTipoCambioPorId(DataSet ds)
        {
            TipoCambioInfo costoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                costoInfo = (from cambioInfo in dt.AsEnumerable()
                             select new TipoCambioInfo
                             {
                                 TipoCambioId = cambioInfo.Field<int>("TipoCambioID"),
                                 Descripcion = cambioInfo.Field<string>("Descripcion"),
                                 Cambio = cambioInfo.Field<decimal>("Cambio"),
                                 Fecha = cambioInfo.Field<DateTime>("Fecha"),
                                 Activo = cambioInfo.Field<bool>("Activo").BoolAEnum()
                             }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return costoInfo;
        }

        internal static TipoCambioInfo ObtenerTipoCambioPorId(IDataReader reader)
        {
            TipoCambioInfo costoInfo = null;
            try
            {
                Logger.Info();
                while (reader.Read())
                {
                    costoInfo = new TipoCambioInfo
                                    {
                                        TipoCambioId = Convert.ToInt32(reader["TipoCambioID"]),
                                        Descripcion = Convert.ToString(reader["Descripcion"]),
                                        Cambio = Convert.ToDecimal(reader["Cambio"]),
                                        Fecha = Convert.ToDateTime(reader["Fecha"]),
                                        Activo = Convert.ToBoolean(reader["Activo"]).BoolAEnum()
                                    };
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return costoInfo;
        }

        /// <summary>
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static TipoCambioInfo ObtenerPorDescripcionFecha(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                TipoCambioInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoCambioInfo
                         {
                             TipoCambioId = info.Field<int>("TipoCambioID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Cambio = info.Field<decimal>("Cambio"),
                             Fecha = info.Field<DateTime>("Fecha"),
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
