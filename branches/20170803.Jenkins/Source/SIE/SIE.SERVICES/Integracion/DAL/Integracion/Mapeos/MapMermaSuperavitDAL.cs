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

    internal class MapMermaSuperavitDAL
    {
        /// <summary>
        /// Obtiene un listado de registros por almacenid
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static MermaSuperavitInfo ObtenerPorAlmacenIdProductoId(DataSet ds)
        {
            MermaSuperavitInfo result;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                result = (from info in dt.AsEnumerable()
                          select new MermaSuperavitInfo
                                     {
                                         MermaSuperavitId = info.Field<int>("MermaSuperavitID"),
                                         Almacen = new AlmacenInfo { AlmacenID = info.Field<int>("AlmacenID") },
                                         Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID") },
                                         Merma = info.Field<decimal>("Merma"),
                                         Superavit = info.Field<decimal>("Superavit"),
                                         Activo = info.Field<bool>("Activo").BoolAEnum()
                                     }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene un listado de registros por almacenid
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<MermaSuperavitInfo> ObtenerPorAlmacenID(DataSet ds)
        {
            List<MermaSuperavitInfo> result;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                result = (from info in dt.AsEnumerable()
                          select new MermaSuperavitInfo
                          {
                              MermaSuperavitId = info.Field<int>("MermaSuperavitID"),
                              Almacen = new AlmacenInfo { AlmacenID = info.Field<int>("AlmacenID") },
                              Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID") },
                              Merma = info.Field<decimal>("Merma"),
                              Superavit = info.Field<decimal>("Superavit"),
                              Activo = info.Field<bool>("Activo").BoolAEnum()
                          }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<MermaSuperavitInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<MermaSuperavitInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new MermaSuperavitInfo
                             {
                                 MermaSuperavitId = info.Field<int>("MermaSuperavitID"),
                                 Almacen = new AlmacenInfo
                                               {
                                                   AlmacenID = info.Field<int>("AlmacenID"),
                                                   Descripcion = info.Field<string>("Almacen"),
                                                   Organizacion = new OrganizacionInfo
                                                                      {
                                                                          OrganizacionID = info.Field<int>("OrganizacionID"),
                                                                          Descripcion= info.Field<string>("Organizacion")
                                                                      }
                                               },
                                 Producto =
                                     new ProductoInfo
                                         {
                                             ProductoId = info.Field<int>("ProductoID"),
                                             Descripcion = info.Field<string>("Producto"),
                                             ProductoDescripcion = info.Field<string>("Producto"),
                                         },
                                 Merma = info.Field<decimal>("Merma"),
                                 Superavit = info.Field<decimal>("Superavit"),
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<MermaSuperavitInfo>
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
        /// Método que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static List<MermaSuperavitInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<MermaSuperavitInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new MermaSuperavitInfo
                         {
                             MermaSuperavitId = info.Field<int>("MermaSuperavitID"),
                             Almacen = new AlmacenInfo
                             {
                                 AlmacenID = info.Field<int>("AlmacenID"),
                                 Descripcion = info.Field<string>("Almacen")
                             },
                             Producto =
                                 new ProductoInfo
                                 {
                                     ProductoId = info.Field<int>("ProductoID"),
                                     Descripcion = info.Field<string>("Producto")
                                 },
                             Merma = info.Field<decimal>("Merma"),
                             Superavit = info.Field<decimal>("Superavit"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
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
        public static MermaSuperavitInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                MermaSuperavitInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new MermaSuperavitInfo
                         {
                             MermaSuperavitId = info.Field<int>("MermaSuperavitID"),
                             Almacen = new AlmacenInfo
                             {
                                 AlmacenID = info.Field<int>("AlmacenID"),
                                 Descripcion = info.Field<string>("Almacen")
                             },
                             Producto =
                                 new ProductoInfo
                                 {
                                     ProductoId = info.Field<int>("ProductoID"),
                                     Descripcion = info.Field<string>("Producto")
                                 },
                             Merma = info.Field<decimal>("Merma"),
                             Superavit = info.Field<decimal>("Superavit"),
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
        public static MermaSuperavitInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                MermaSuperavitInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new MermaSuperavitInfo
                         {
                             MermaSuperavitId = info.Field<int>("MermaSuperavitID"),
                             Almacen = new AlmacenInfo
                                           {
                                               AlmacenID = info.Field<int>("AlmacenID"),
                                           },
                             Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID"), Descripcion = info.Field<string>("Producto") },
                             Merma = info.Field<decimal>("Merma"),
                             Superavit = info.Field<decimal>("Superavit"),
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
