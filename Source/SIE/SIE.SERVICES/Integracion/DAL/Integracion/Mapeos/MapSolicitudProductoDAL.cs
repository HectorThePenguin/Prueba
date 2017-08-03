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
    public class MapSolicitudProductoDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<SolicitudProductoInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<SolicitudProductoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new SolicitudProductoInfo
                             {
								SolicitudProductoID = info.Field<int>("SolicitudProductoID"),
								Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
								FolioSolicitud = info.Field<int>("FolioSolicitud"),
								FechaSolicitud = info.Field<DateTime>("FechaSolicitud"),
                                UsuarioSolicita = new UsuarioInfo { UsuarioID = info.Field<int>("UsuarioID"), },
								Estatus = new EstatusInfo { EstatusId = info.Field<int>("EstatusID"), Descripcion = info.Field<string>("Estatus") },
                                UsuarioAutoriza = new UsuarioInfo { UsuarioID = info.Field<int>("UsuarioID")},
								FechaAutorizado = info.Field<DateTime>("FechaAutorizado"),
                                UsuarioEntrega = new UsuarioInfo { UsuarioID = info.Field<int>("UsuarioID")},
								FechaEntrega = info.Field<DateTime>("FechaEntrega"),
								CentroCosto = new CentroCostoInfo { CentroCostoID = info.Field<int>("CentroCostoID"), Descripcion = info.Field<string>("CentroCosto") },
								Almacen = new AlmacenInfo { AlmacenID = info.Field<int>("AlmacenID"), Descripcion = info.Field<string>("Almacen") },
								ObservacionUsuarioEntrega = info.Field<string>("ObservacionUsuarioEntrega"),
								ObservacionUsuarioAutoriza = info.Field<string>("ObservacionUsuarioAutoriza"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<SolicitudProductoInfo>
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
        public static List<SolicitudProductoInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<SolicitudProductoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new SolicitudProductoInfo
                             {
                                 SolicitudProductoID = info.Field<int>("SolicitudProductoID"),
                                 Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
                                 FolioSolicitud = info.Field<int>("FolioSolicitud"),
                                 FechaSolicitud = info.Field<DateTime>("FechaSolicitud"),
                                 UsuarioSolicita = new UsuarioInfo { UsuarioID = info.Field<int>("UsuarioID"), },
                                 Estatus = new EstatusInfo { EstatusId = info.Field<int>("EstatusID"), Descripcion = info.Field<string>("Estatus") },
                                 UsuarioAutoriza = new UsuarioInfo { UsuarioID = info.Field<int>("UsuarioID") },
                                 FechaAutorizado = info.Field<DateTime>("FechaAutorizado"),
                                 UsuarioEntrega = new UsuarioInfo { UsuarioID = info.Field<int>("UsuarioID") },
                                 FechaEntrega = info.Field<DateTime>("FechaEntrega"),
                                 CentroCosto = new CentroCostoInfo { CentroCostoID = info.Field<int>("CentroCostoID"), Descripcion = info.Field<string>("CentroCosto") },
                                 Almacen = new AlmacenInfo { AlmacenID = info.Field<int>("AlmacenID"), Descripcion = info.Field<string>("Almacen") },
                                 AlmacenMovimientoID = info.Field<long?>("AlmacenMovimientoID"),
                                 ObservacionUsuarioEntrega = info.Field<string>("ObservacionUsuarioEntrega"),
                                 ObservacionUsuarioAutoriza = info.Field<string>("ObservacionUsuarioAutoriza"),
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
        public static SolicitudProductoInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                SolicitudProductoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new SolicitudProductoInfo
                             {
								SolicitudProductoID = info.Field<int>("SolicitudProductoID"),
								Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
								FolioSolicitud = info.Field<int>("FolioSolicitud"),
								FechaSolicitud = info.Field<DateTime>("FechaSolicitud"),
                                UsuarioSolicita = new UsuarioInfo { UsuarioID = info.Field<int>("UsuarioID"), },
                                Estatus = new EstatusInfo { EstatusId = info.Field<int>("EstatusID"), Descripcion = info.Field<string>("Estatus") },
                                UsuarioAutoriza = new UsuarioInfo { UsuarioID = info.Field<int>("UsuarioID") },
                                FechaAutorizado = info.Field<DateTime>("FechaAutorizado"),
                                UsuarioEntrega = new UsuarioInfo { UsuarioID = info.Field<int>("UsuarioID") },
								FechaEntrega = info.Field<DateTime>("FechaEntrega"),
								CentroCosto = new CentroCostoInfo { CentroCostoID = info.Field<int>("CentroCostoID"), Descripcion = info.Field<string>("CentroCosto") },
								Almacen = new AlmacenInfo { AlmacenID = info.Field<int>("AlmacenID"), Descripcion = info.Field<string>("Almacen") },
                                AlmacenMovimientoID = info.Field<long?>("AlmacenMovimientoID"),
								ObservacionUsuarioEntrega = info.Field<string>("ObservacionUsuarioEntrega"),
								ObservacionUsuarioAutoriza = info.Field<string>("ObservacionUsuarioAutoriza"),
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
        public static SolicitudProductoInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                SolicitudProductoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new SolicitudProductoInfo
                             {
								SolicitudProductoID = info.Field<int>("SolicitudProductoID"),
								Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
								FolioSolicitud = info.Field<int>("FolioSolicitud"),
								FechaSolicitud = info.Field<DateTime>("FechaSolicitud"),
                                UsuarioSolicita = new UsuarioInfo { UsuarioID = info.Field<int>("UsuarioID"), },
                                Estatus = new EstatusInfo { EstatusId = info.Field<int>("EstatusID"), Descripcion = info.Field<string>("Estatus") },
                                UsuarioAutoriza = new UsuarioInfo { UsuarioID = info.Field<int>("UsuarioID") },
                                FechaAutorizado = info.Field<DateTime>("FechaAutorizado"),
                                UsuarioEntrega = new UsuarioInfo { UsuarioID = info.Field<int>("UsuarioID") },
								FechaEntrega = info.Field<DateTime>("FechaEntrega"),
								CentroCosto = new CentroCostoInfo { CentroCostoID = info.Field<int>("CentroCostoID"), Descripcion = info.Field<string>("CentroCosto") },
								Almacen = new AlmacenInfo { AlmacenID = info.Field<int>("AlmacenID"), Descripcion = info.Field<string>("Almacen") },
								ObservacionUsuarioEntrega = info.Field<string>("ObservacionUsuarioEntrega"),
								ObservacionUsuarioAutoriza = info.Field<string>("ObservacionUsuarioAutoriza"),
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
        /// Obtener pricio promedio
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static  List<ProductoINFORInfo> ObtenerPrecioPromedioProductoINFOR(DataSet ds)
        {
            List<ProductoINFORInfo> solicitudProducto;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                solicitudProducto = (from info in dt.AsEnumerable()
                                     select new ProductoINFORInfo()
                                     {
                                        CodigoTransaccion = info.Field<string>("TRA_CODE"),
                                        Referencia = info.Field<string>("TRA_ADVICE"),
                                        Cantidad = info.Field<decimal>("TRL_QTY"), 
                                        PrecioPromedio = info.Field<decimal>("TRL_PRICE"),
                                        CodigoParte = info.Field<string>("PAR_CODE"),
                                        DescripcionParte = info.Field<string>("PAR_DESC"),
                                        CodigoCosto = info.Field<string>("TRL_COSTCODE"),
                                        ParUdfchar02 = info.Field<string>("PAR_UDFCHAR02")
                                     }).ToList();                
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return solicitudProducto;
        }

        /// <summary>
        /// Metodo para valdiar si el producto existe como codigo parte
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ProductoCodigoParteInfo ObtenerCodigoParteDeProducto(DataSet ds)
        {
            ProductoCodigoParteInfo codigoParteDeProducto;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                codigoParteDeProducto = (from info in dt.AsEnumerable()
                                         select new ProductoCodigoParteInfo()
                                         {
                                             ProductoCodigoParteID = info.Field<int>("ProductoCodigoParteID"),
                                             Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID") },
                                             CodigoParte = info.Field<string>("CodigoParte")
                                         }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return codigoParteDeProducto;
        }

        /// <summary>
        /// Metodo para valdiar si existe el número de documento
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static bool ObtenerRespuestaNumeroDocumento(DataSet ds)
        {
            var result = false;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var resultado = dt.Rows[0][0].ToString();
                if (int.Parse(resultado) == 1)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        internal static string ObtenerRespuestaArete(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var result = dt.Rows[0][0].ToString();
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static List<SolicitudProductoInfo> ObtenerConciliacionPorAlmacenMovimientoXML(IDataReader reader)
        {
            try
            {
                Logger.Info();
                var distribuciones = new List<SolicitudProductoInfo>();
                SolicitudProductoInfo distribucion;
                while (reader.Read())
                {
                    distribucion = new SolicitudProductoInfo
                    {
                        SolicitudProductoID = Convert.ToInt32(reader["SolicitudProductoID"]),
                        Organizacion = new OrganizacionInfo
                                           {
                                               OrganizacionID = Convert.ToInt32(reader["OrganizacionID"])
                                           },
                        OrganizacionID = Convert.ToInt32(reader["OrganizacionID"]),
                        FolioSolicitud = Convert.ToInt64(reader["FolioSolicitud"]),
                        FechaSolicitud = Convert.ToDateTime(reader["FechaSolicitud"]),
                        Estatus = new EstatusInfo
                                      {
                                          EstatusId = Convert.ToInt32(reader["EstatusID"])
                                      },
                        EstatusID = Convert.ToInt32(reader["EstatusID"]),
                        FechaAutorizado = Convert.ToDateTime(reader["FechaAutorizado"]),
                        FechaEntrega = Convert.ToDateTime(reader["FechaEntrega"]),
                        CentroCosto = new CentroCostoInfo
                                          {
                                              CentroCostoID = Convert.ToInt32(reader["CentroCostoID"])
                                          },
                        CentroCostoID = Convert.ToInt32(reader["CentroCostoID"]),
                        Almacen = new AlmacenInfo
                                      {
                                          AlmacenID = Convert.ToInt32(reader["AlmacenID"]),
                                          Descripcion = Convert.ToString(reader["Almacen"]),
                                          TipoAlmacen = new TipoAlmacenInfo
                                                            {
                                                                TipoAlmacenID = Convert.ToInt32(reader["TipoAlmacenID"])
                                                            },
                                          TipoAlmacenID = Convert.ToInt32(reader["TipoAlmacenID"])
                                      },
                        AlmacenID = Convert.ToInt32(reader["AlmacenID"]),
                        AlmacenMovimientoID = Convert.ToInt64(reader["AlmacenMovimientoID"])
                    };
                    distribuciones.Add(distribucion);
                }
                reader.NextResult();
                var distribucionIngredientesOrganizaciones = new List<SolicitudProductoDetalleInfo>();
                SolicitudProductoDetalleInfo distribucionIngredienteOrganizacion;
                while (reader.Read())
                {
                    distribucionIngredienteOrganizacion = new SolicitudProductoDetalleInfo
                    {
                        SolicitudProductoDetalleID = Convert.ToInt32(reader["SolicitudProductoDetalleID"]),
                        SolicitudProductoID = Convert.ToInt32(reader["SolicitudProductoID"]),
                        Producto = new ProductoInfo
                                       {
                                           ProductoId = Convert.ToInt32(reader["ProductoID"]),
                                           Descripcion = Convert.ToString(reader["Producto"]),
                                           UnidadId = Convert.ToInt32(reader["UnidadID"]),
                                           UnidadMedicion = new UnidadMedicionInfo
                                                                {
                                                                    UnidadID = Convert.ToInt32(reader["UnidadID"]),
                                                                },
                                           SubFamilia = new SubFamiliaInfo
                                                            {
                                                                SubFamiliaID = Convert.ToInt32(reader["SubFamiliaID"])
                                                            },
                                           SubfamiliaId = Convert.ToInt32(reader["SubFamiliaID"])
                                       },
                        ProductoID = Convert.ToInt32(reader["ProductoID"]),
                        Cantidad = Convert.ToDecimal(reader["Cantidad"]),
                        CamionReparto = new CamionRepartoInfo
                                            {
                                                CamionRepartoID = Convert.ToInt32(reader["CamionRepartoID"])
                                            },
                        CamionRepartoID = Convert.ToInt32(reader["CamionRepartoID"]),
                        Estatus = new EstatusInfo
                                      {
                                          EstatusId = Convert.ToInt32(reader["EstatusID"])
                                      },
                        EstatusID = Convert.ToInt32(reader["EstatusID"]),
                        PrecioPromedio = Convert.ToDecimal(reader["Precio"])
                    };
                    distribucionIngredientesOrganizaciones.Add(distribucionIngredienteOrganizacion);
                }
                distribuciones.ForEach(datos =>
                                           {
                                               datos.Detalle =
                                                   distribucionIngredientesOrganizaciones.Where(
                                                       id => id.SolicitudProductoID == datos.SolicitudProductoID).ToList();
                                           });
                return distribuciones;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}

