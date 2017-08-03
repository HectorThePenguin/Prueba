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
    public class MapAlmacenUsuarioDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<AlmacenUsuarioInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<AlmacenUsuarioInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new AlmacenUsuarioInfo
                             {
								AlmacenUsuarioID = info.Field<int>("AlmacenUsuarioID"),
								Almacen = new AlmacenInfo
								    {
								        AlmacenID = info.Field<int>("AlmacenID"), 
                                        Descripcion = info.Field<string>("Almacen"),
                                        CodigoAlmacen = info.Field<string>("CodigoAlmacen"),
                                        Organizacion = new OrganizacionInfo
                                            {
                                                OrganizacionID = info.Field<int>("OrganizacionID"),
                                                Descripcion = info.Field<string>("Organizacion"),
                                            }
								    },
                                Usuario = new UsuarioInfo
                                    {
                                        UsuarioID = info.Field<int>("UsuarioID"), 
                                        Nombre = info.Field<string>("Usuario")
                                    },
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<AlmacenUsuarioInfo>
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
        public static List<AlmacenUsuarioInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<AlmacenUsuarioInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new AlmacenUsuarioInfo
                             {
								AlmacenUsuarioID = info.Field<int>("AlmacenUsuarioID"),
								Almacen = new AlmacenInfo { AlmacenID = info.Field<int>("AlmacenID"), Descripcion = info.Field<string>("Almacen") },
                                Usuario = new UsuarioInfo { UsuarioID = info.Field<int>("UsuarioID"), Nombre = info.Field<string>("Usuario") },
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
        public static AlmacenUsuarioInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                AlmacenUsuarioInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new AlmacenUsuarioInfo
                             {
								AlmacenUsuarioID = info.Field<int>("AlmacenUsuarioID"),
								Almacen = new AlmacenInfo { AlmacenID = info.Field<int>("AlmacenID"), Descripcion = info.Field<string>("Almacen") },
                                Usuario = new UsuarioInfo { UsuarioID = info.Field<int>("UsuarioID"), Nombre = info.Field<string>("Usuario") },
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
        public static AlmacenUsuarioInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                AlmacenUsuarioInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new AlmacenUsuarioInfo
                             {
								AlmacenUsuarioID = info.Field<int>("AlmacenUsuarioID"),
								Almacen = new AlmacenInfo { AlmacenID = info.Field<int>("AlmacenID"), Descripcion = info.Field<string>("Almacen") },
                                Usuario = new UsuarioInfo { UsuarioID = info.Field<int>("UsuarioID"), Nombre = info.Field<string>("Usuario") },
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
        /// Método que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static List<AlmacenUsuarioInfo> ObtenerPorAlmacenID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<AlmacenUsuarioInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new AlmacenUsuarioInfo
                         {
                             AlmacenUsuarioID = info.Field<int>("AlmacenUsuarioID"),
                             Almacen = new AlmacenInfo
                                 {
                                     AlmacenID = info.Field<int>("AlmacenID"), 
                                     Descripcion = info.Field<string>("Almacen"),
                                     CodigoAlmacen = info.Field<string>("CodigoAlmacen")
                                 },
                             Usuario = new UsuarioInfo
                                 {
                                     UsuarioID = info.Field<int>("UsuarioID"), 
                                     Nombre = info.Field<string>("Usuario")
                                 },
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

        internal static AlmacenUsuarioInfo ObtenerPorUsuarioId(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                AlmacenUsuarioInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new AlmacenUsuarioInfo
                         {
                             AlmacenUsuarioID = info.Field<int>("AlmacenUsuarioID"),
                             Almacen = new AlmacenInfo { AlmacenID = info.Field<int>("AlmacenID"), Descripcion = info.Field<string>("Almacen"), TipoAlmacenID = info.Field<int>("TipoAlmacenID") },
                             Usuario = new UsuarioInfo { UsuarioID = info.Field<int>("UsuarioID"), Nombre = info.Field<string>("Usuario") },
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

