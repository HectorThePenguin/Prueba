using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapPremezclaDAL
    {
        /// <summary>
        /// Obtiene una lista de premezclas
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<PremezclaInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<PremezclaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new PremezclaInfo
                             {
                                 PremezclaId = info.Field<int>("PremezclaID"),
                                 Organizacion =
                                     new OrganizacionInfo {OrganizacionID = info.Field<int>("OrganizacionID")},
                                 Descripcion = info.Field<string>("Descripcion"),
                                 Producto = new ProductoInfo {ProductoId = info.Field<int>("ProductoID")},
                                 FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                                 UsuarioCreacion = new UsuarioInfo {UsuarioID = info.Field<int>("UsuarioCreacionID")},
                                 Activo = info.Field<bool>("Activo").BoolAEnum()
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
        /// Obtiene una premezcla por productoid y organizacionid
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static PremezclaInfo ObtenerPorProductoIdOrganizacionId(DataSet ds)
        {
            PremezclaInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new PremezclaInfo
                         {
                             PremezclaId = info.Field<int>("PremezclaID"),
                             Organizacion = new OrganizacionInfo
                             {
                                 OrganizacionID = info.Field<int>("OrganizacionID"),
                             },
                             Descripcion = info.Field<string>("Descripcion"),
                             Producto = new ProductoInfo
                             {
                                 ProductoId = info.Field<int>("ProductoID"),
                             },
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                             UsuarioCreacion = new UsuarioInfo
                             {
                                 UsuarioID = info.Field<int>("UsuarioCreacionID")
                             }
                         }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }

        /// <summary>
        /// Obtiene una lista de premezcla con su detalle
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<PremezclaInfo> ObtenerPorOrganizacionDetalle(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtDetalle = ds.Tables[ConstantesDAL.DtDetalle];

                List<PremezclaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new PremezclaInfo
                             {
                                 PremezclaId = info.Field<int>("PremezclaID"),
                                 Organizacion =
                                     new OrganizacionInfo {OrganizacionID = info.Field<int>("OrganizacionID")},
                                 Descripcion = info.Field<string>("Descripcion"),
                                 Producto = new ProductoInfo {ProductoId = info.Field<int>("ProductoID")},
                                 ListaPremezclaDetalleInfos = (from det in dtDetalle.AsEnumerable()
                                                               where
                                                                   info.Field<int>("PremezclaID") ==
                                                                   det.Field<int>("PremezclaID")
                                                               select new PremezclaDetalleInfo
                                                                          {
                                                                              PremezclaDetalleID =
                                                                                  det.Field<int>("PremezclaDetalleID"),
                                                                              Premezcla = new PremezclaInfo
                                                                                              {
                                                                                                  PremezclaId =
                                                                                                      det.Field<int>(
                                                                                                          "PremezclaID")
                                                                                              },
                                                                              Producto = new ProductoInfo
                                                                                             {
                                                                                                 ProductoId =
                                                                                                     det.Field<int>(
                                                                                                         "ProductoID")
                                                                                             },
                                                                              Porcentaje =
                                                                                  det.Field<decimal>("Porcentaje")
                                                                          }).ToList()
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
