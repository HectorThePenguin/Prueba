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
    internal class MapCuentaGastosDAL
    {
        internal static ResultadoInfo<CuentaGastosInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<CuentaGastosInfo> lista =
                    (from info in dt.AsEnumerable()
                        select new CuentaGastosInfo
                        {
                            CuentaGastoID = info.Field<int>("CuentaGastoID"),
                            Organizacion = new OrganizacionInfo
                            {
                                OrganizacionID = info.Field<int>("OrganizacionID"),
                                Descripcion = info.Field<string>("DescripcionOrganizacion"),
                            },
                            CuentaSAP = new CuentaSAPInfo
                            {
                                CuentaSAPID = info.Field<int>("CuentaSAPID"),
                                CuentaSAP = info.Field<string>("CuentaSAP"),
                                Descripcion = info.Field<string>("DescripcionCuenta")
                            },
                            Costos = new CostoInfo
                            {
                                CostoID = info.Field<int>("CostoID"),
                                Descripcion = info.Field<string>("DescripcionCosto"),
                                ClaveContable = info.Field<string>("ClaveContable")
                            },
                            Activo = info.Field<bool>("Activo").BoolAEnum(),
                            UsuarioCreacionID = info.Field<int>("UsuarioCreacionID"),
                            FechaCreacion = info.Field<DateTime>("FechaCreacion")
                        }).ToList();

                var resultado =
                    new ResultadoInfo<CuentaGastosInfo>
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

        internal static List<CuentaGastosInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<CuentaGastosInfo> lista =
                    (from info in dt.AsEnumerable()
                     select new CuentaGastosInfo
                     {
                         CuentaGastoID = info.Field<int>("CuentaGastoID"),
                         Organizacion = new OrganizacionInfo
                         {
                             OrganizacionID = info.Field<int>("OrganizacionID"),
                             Descripcion = info.Field<string>("DescripcionOrganizacion"),
                         },
                         CuentaSAP = new CuentaSAPInfo
                         {
                             CuentaSAPID = info.Field<int>("CuentaSAPID"),
                             CuentaSAP = info.Field<string>("CuentaSAP"),
                             Descripcion = info.Field<string>("DescripcionCuenta")
                         },
                         Costos = new CostoInfo
                         {
                             CostoID = info.Field<int>("CostoID"),
                             Descripcion = info.Field<string>("DescripcionCosto"),
                             ClaveContable = info.Field<string>("ClaveContable")
                         },
                         Activo = info.Field<bool>("Activo").BoolAEnum(),
                         UsuarioCreacionID = info.Field<int>("UsuarioCreacionID"),
                         FechaCreacion = info.Field<DateTime>("FechaCreacion")
                     }).ToList();

                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
