using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using System.Data;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapComisionDAL
    {
        internal static List<TipoComisionInfo> ObtenerTiposComisiones(DataSet ds)
        {
            List<TipoComisionInfo> resultado = new List<TipoComisionInfo>();
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from tipoComisionInfo in dt.AsEnumerable()
                                         select new TipoComisionInfo
                                         {
                                             TipoComisionID = tipoComisionInfo.Field<int>("TipoComisionID"),
                                             Descripcion = tipoComisionInfo.Field<string>("Descripcion")
                                         }).ToList<TipoComisionInfo>();
                
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }


        internal static List<ComisionInfo> obtenerResultadoComisionesProveedor(DataSet ds)
        {
            List<ComisionInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                
                resultado = (from info in dt.AsEnumerable()
                             select new ComisionInfo
                                        {
                                            DescripcionComision = info.Field<string>("descripcion"),
                                            ProveedorComisionID = info.Field<int>("ProveedorComisionID"),
                                            ProveedorID = info.Field<int>("ProveedorID"),
                                            Tarifa = info.Field<decimal>("Tarifa"),
                                            TipoComisionID = info.Field<int>("TipoComisionID")                                                         
                                        }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        ///     Método asigna el resultado del guardo de la comision
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        //internal static bool ObtenerResultadoComision(IDataReader reader)
        //{
        //    bool resultado = false;
        //    try
        //    {
        //        Logger.Info();
        //        var lista = new List<AlmacenInfo>();

        //        if (reader.Read())
        //        {
        //            resultado = Convert.ToBoolean(reader["resultado"]);
        //        }
                
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //        throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
        //    }
        //    return resultado;
        //}

        

    }
}
