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
    internal class MapProveedorDAL
    {

        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<ProveedorInfo> ObtenerPorPagina(DataSet ds)
        {
            ResultadoInfo<ProveedorInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<ProveedorInfo> lista = (from info in dt.AsEnumerable()
                                          select new ProveedorInfo
                                          {
                                              ProveedorID = info.Field<int>("ProveedorId"),
                                              Descripcion = info.Field<string>("Descripcion"),
                                              CodigoSAP = info.Field<string>("CodigoSAP"),
                                              TipoProveedor =
                                              new TipoProveedorInfo
                                                  {
                                                      TipoProveedorID = info.Field<int>("TipoProveedorID"), 
                                                      Descripcion = info.Field<string>("TipoProveedor")
                                                  },
                                              Activo = info.Field<bool>("Activo").BoolAEnum()
                                          }).ToList();

                resultado = new ResultadoInfo<ProveedorInfo>
                {
                    Lista = lista,
                    TotalRegistros =
                        Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
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
        internal static ResultadoInfo<ProveedorInfo> ObtenerPorPaginaFiltros(DataSet ds)
        {
            ResultadoInfo<ProveedorInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<ProveedorInfo> lista = (from info in dt.AsEnumerable()
                                             select new ProveedorInfo
                                             {
                                                 ProveedorID = info.Field<int>("ProveedorId"),
                                                 Descripcion = info.Field<string>("Descripcion"),
                                                 CodigoSAP = info.Field<string>("CodigoSAP"),
                                                 TipoProveedor =
                                                 new TipoProveedorInfo
                                                 {
                                                     TipoProveedorID = info.Field<int>("TipoProveedorID"),
                                                     Descripcion = info.Field<string>("TipoProveedor")
                                                 },
                                                 ImporteComision = info.Field<decimal?>("ImporteComision") != null ? info.Field<decimal>("ImporteComision") : 0,
                                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                                             }).ToList();

                resultado = new ResultadoInfo<ProveedorInfo>
                {
                    Lista = lista,
                    TotalRegistros =
                        Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
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
        internal static ResultadoInfo<ProveedorInfo> ObtenerPorPaginaCompleto(DataSet ds)
        {
            ResultadoInfo<ProveedorInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<ProveedorInfo> lista = (from info in dt.AsEnumerable()
                                             select new ProveedorInfo
                                             {
                                                 ProveedorID = info.Field<int>("ProveedorId"),
                                                 Descripcion = info.Field<string>("Descripcion"),
                                                 CodigoSAP = info.Field<string>("CodigoSAP"),
                                                 TipoProveedor =
                                                 new TipoProveedorInfo
                                                 {
                                                     TipoProveedorID = info.Field<int>("TipoProveedorID"),
                                                     Descripcion = info.Field<string>("TipoProveedor")
                                                 },
                                                 ImporteComision = info.Field<decimal?>("ImporteComision") != null ? info.Field<decimal>("ImporteComision") : 0,
                                                 Activo = info.Field<bool>("Activo").BoolAEnum()
                                             }).ToList();

                resultado = new ResultadoInfo<ProveedorInfo>
                {
                    Lista = lista,
                    TotalRegistros =
                        Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
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
        ///     Metodo que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ProveedorInfo> ObtenerTodos(DataSet ds)
        {
            List<ProveedorInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new ProveedorInfo
                         {
                             ProveedorID = info.Field<int>("ProveedorId"),
                             Descripcion = info.Field<string>("Descripcion"),
                             TipoProveedor = new TipoProveedorInfo
                             { 
                                 TipoProveedorID = info.Field<int>("TipoProveedorID")
                             },
                             CodigoSAP = info.Field<string>("CodigoSAP"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
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
        ///     Metodo que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ProveedorInfo ObtenerPorID(DataSet ds)
        {
            ProveedorInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new ProveedorInfo
                         {
                             ProveedorID = info.Field<int>("ProveedorId"),
                             Descripcion = info.Field<string>("Descripcion"),
                             TipoProveedor = new TipoProveedorInfo
                             {
                                 TipoProveedorID = info.Field<int>("TipoProveedorID"),
                                 Descripcion = info.Field<string>("TipoProveedor")
                             },
                             CodigoSAP = info.Field<string>("CodigoSAP"),
                             ImporteComision = info.Field<decimal?>("ImporteComision") != null ? info.Field<decimal>("ImporteComision") : 0,
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
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
        ///     Metodo que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ProveedorInfo ObtenerPorCodigoSAP(DataSet ds)
        {
            ProveedorInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new ProveedorInfo
                         {
                             ProveedorID = info.Field<int>("ProveedorId"),
                             Descripcion = info.Field<string>("Descripcion"),
                             TipoProveedor = new TipoProveedorInfo
                             {
                                 TipoProveedorID = info.Field<int>("TipoProveedorID")
                             },
                             CodigoSAP = info.Field<string>("CodigoSAP"),
                             ImporteComision = info.Field<decimal?>("ImporteComision") != null ? info.Field<decimal>("ImporteComision") : 0,
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
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
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ProveedorInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ProveedorInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ProveedorInfo
                         {
                             ProveedorID = info.Field<int>("ProveedorID"),
                             CodigoSAP = info.Field<string>("CodigoSAP"),
                             Descripcion = info.Field<string>("Descripcion"),
                             TipoProveedor = new TipoProveedorInfo { TipoProveedorID = info.Field<int>("TipoProveedorID"), Descripcion = info.Field<string>("TipoProveedor") },
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
        /// Metodo que obtiene un listado de proveedores
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ProveedorInfo> ObtenerPorTipoProveedorID(DataSet ds)
        {
            List<ProveedorInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new ProveedorInfo
                         {
                             ProveedorID = info.Field<int>("ProveedorId"),
                             Descripcion = info.Field<string>("Descripcion"),
                             TipoProveedor = new TipoProveedorInfo
                             {
                                 TipoProveedorID = info.Field<int>("TipoProveedorID")
                             },
                             CodigoSAP = info.Field<string>("CodigoSAP"),
                             Correo = info.Field<string>("CorreoElectronico") ?? string.Empty,
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
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
        /// Obtiene una instancia de Proveedor
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ProveedorInfo ObtenerPorProductoContratoCodigoSAP(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ProveedorInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ProveedorInfo
                             {
                                 ProveedorID = info.Field<int>("ProveedorID"),
                                 CodigoSAP = info.Field<string>("CodigoSAP"),
                                 Descripcion = info.Field<string>("Descripcion"),
                                 TipoProveedor =
                                     new TipoProveedorInfo
                                         {
                                             TipoProveedorID =
                                                 info.Field<int>("TipoProveedorID"),
                                             Descripcion =
                                                 info.Field<string>("TipoProveedor")
                                         },
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
        /// Obtiene un resultado con los proveedores de los contratos
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<ProveedorInfo> ObtenerPorPaginaProductoContrato(DataSet ds)
        {
            ResultadoInfo<ProveedorInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ProveedorInfo> lista = (from info in dt.AsEnumerable()
                                             select new ProveedorInfo
                                                        {
                                                            ProveedorID = info.Field<int>("ProveedorId"),
                                                            Descripcion = info.Field<string>("Descripcion"),
                                                            CodigoSAP = info.Field<string>("CodigoSAP"),
                                                            TipoProveedor =
                                                                new TipoProveedorInfo
                                                                    {
                                                                        TipoProveedorID =
                                                                            info.Field<int>("TipoProveedorID"),
                                                                        Descripcion =
                                                                            info.Field<string>("TipoProveedor")
                                                                    },
                                                        }).ToList();
                resultado = new ResultadoInfo<ProveedorInfo>
                {
                    Lista = lista,
                    TotalRegistros =
                        Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
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
        internal static ProveedorInfo ObtenerPorIDconCorreo(DataSet ds)
        {
            ProveedorInfo resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                                             select new ProveedorInfo
                                             {
                                                 ProveedorID = info.Field<int>("ProveedorId"),
                                                 Descripcion = info.Field<string>("Descripcion"),
                                                 CodigoSAP = info.Field<string>("CodigoSAP"),
                                                 TipoProveedor =
                                                 new TipoProveedorInfo
                                                 {
                                                     TipoProveedorID = info.Field<int>("TipoProveedorID"),
                                                     Descripcion = info.Field<string>("TipoProveedor")
                                                 },
                                                 ImporteComision = info.Field<decimal?>("ImporteComision") != null ? info.Field<decimal>("ImporteComision") : 0,
                                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                                                 Correo = info.Field<string>("CorreoElectronico"),
                                             }).FirstOrDefault<ProveedorInfo>();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        internal static ResultadoInfo<ProveedorInfo> ObtenerPorPaginaCompletoGanadera(DataSet ds)
        {
            ResultadoInfo<ProveedorInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<ProveedorInfo> lista = (from info in dt.AsEnumerable()
                                             select new ProveedorInfo
                                             {
                                                 ProveedorID = info.Field<int>("ProveedorId"),
                                                 Descripcion = info.Field<string>("Descripcion"),
                                                 CodigoSAP = info.Field<string>("CodigoSAP"),
                                                 TipoProveedor =
                                                 new TipoProveedorInfo
                                                 {
                                                     TipoProveedorID = info.Field<int>("TipoProveedorID"),
                                                     Descripcion = info.Field<string>("TipoProveedor")
                                                 },
                                                 ImporteComision = info.Field<decimal?>("ImporteComision") != null ? info.Field<decimal>("ImporteComision") : 0,
                                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                                                 OrganizacionID = info.Field<int>("OrganizacionID"),
                                             }).ToList();

                resultado = new ResultadoInfo<ProveedorInfo>
                {
                    Lista = lista,
                    TotalRegistros =
                        Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        internal static ProveedorInfo ObtenerPorIDGanadera(DataSet ds)
        {
            ProveedorInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new ProveedorInfo
                         {
                             ProveedorID = info.Field<int>("ProveedorId"),
                             Descripcion = info.Field<string>("Descripcion"),
                             TipoProveedor = new TipoProveedorInfo
                             {
                                 TipoProveedorID = info.Field<int>("TipoProveedorID"),
                                 Descripcion = info.Field<string>("TipoProveedor")
                             },
                             CodigoSAP = info.Field<string>("CodigoSAP"),
                             ImporteComision = info.Field<decimal?>("ImporteComision") != null ? info.Field<decimal>("ImporteComision") : 0,
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             OrganizacionID = info.Field<int>("OrganizacionID"),
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
        /// Obtiene una instancia de Proveedor
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ProveedorInfo ObtenerPorCodigoSAPEmbarque(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ProveedorInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ProveedorInfo
                         {
                             ProveedorID = info.Field<int>("ProveedorID"),
                             CodigoSAP = info.Field<string>("CodigoSAP"),
                             Descripcion = info.Field<string>("Descripcion"),
                             TipoProveedor =
                                 new TipoProveedorInfo
                                 {
                                     TipoProveedorID =
                                         info.Field<int>("TipoProveedorID"),
                                     Descripcion =
                                         info.Field<string>("TipoProveedor")
                                 },
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
        /// Obtiene un resultado con los proveedores de los contratos
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<ProveedorInfo> ObtenerPorPaginaEmbarque(DataSet ds)
        {
            ResultadoInfo<ProveedorInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ProveedorInfo> lista = (from info in dt.AsEnumerable()
                                             select new ProveedorInfo
                                             {
                                                 ProveedorID = info.Field<int>("ProveedorId"),
                                                 Descripcion = info.Field<string>("Descripcion"),
                                                 CodigoSAP = info.Field<string>("CodigoSAP"),
                                                 TipoProveedor =
                                                     new TipoProveedorInfo
                                                     {
                                                         TipoProveedorID =
                                                             info.Field<int>("TipoProveedorID"),
                                                         Descripcion =
                                                             info.Field<string>("TipoProveedor")
                                                     },
                                             }).ToList();
                resultado = new ResultadoInfo<ProveedorInfo>
                {
                    Lista = lista,
                    TotalRegistros =
                        Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
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
        ///     Obtiene un Proveedor que cuenta con Origen-Destino Configurado
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ProveedorInfo ObtenerProveedorConfiguradoOrigenDestino(DataSet ds)
        {
            ProveedorInfo resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select new ProveedorInfo
                             {
                                 ProveedorID = info.Field<int>("ProveedorId"),
                                 Descripcion = info.Field<string>("Descripcion"),
                                 CodigoSAP = info.Field<string>("CodigoSAP")
                             }).FirstOrDefault<ProveedorInfo>();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
    }
}