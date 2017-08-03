using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapProductoTiempoEstandarDAL
    {
        internal static ResultadoInfo<ProductoTiempoEstandarInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ProductoTiempoEstandarInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ProductoTiempoEstandarInfo
                         {
                             ProductoTiempoEstandarID = info.Field<int>("ProductoTiempoEstandarID"),
                             Producto = new ProductoInfo()
                             {
                                 ProductoId = info.Field<int>("ProductoID"),
                                 Descripcion = info.Field<string>("Descripcion").Trim(),
                             },
                             Tiempo = info.Field<string>("Tiempo"),
                             Estatus = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<ProductoTiempoEstandarInfo>
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

        internal static ProductoTiempoEstandarInfo ObtenerPorProductoID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ProductoTiempoEstandarInfo productoTiempoEstandarInfo =
                    (from info in dt.AsEnumerable()
                     select
                         new ProductoTiempoEstandarInfo
                         {
                             ProductoTiempoEstandarID = info.Field<int>("ProductoTiempoEstandarID"),
                             Producto = new ProductoInfo()
                             {
                                 ProductoId = info.Field<int>("ProductoID"),
                                 Descripcion = info.Field<string>("Descripcion").Trim(),
                             },
                             Tiempo = info.Field<string>("Tiempo"),
                             Estatus = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList().FirstOrDefault();


                return productoTiempoEstandarInfo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static bool GuardarProductoTiempoEstandar(DataSet ds)
        {
            bool resultado = false;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtDatos].Rows[0]["ProductoTiempoEstandarID"]);
                if(totalRegistros > 0 )
                {
                    resultado = true;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static bool ActualizarProductoTiempoEstandar(DataSet ds)
        {
            bool resultado = false;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtDatos].Rows[0]["Resultado"]);
                if (totalRegistros > 0)
                {
                    resultado = true;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
