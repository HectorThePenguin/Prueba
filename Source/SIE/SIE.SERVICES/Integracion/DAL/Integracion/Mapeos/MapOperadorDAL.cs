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
    internal static class MapOperadorDAL
    {
        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<OperadorInfo> ObtenerPorPagina(DataSet ds)
        {
            ResultadoInfo<OperadorInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<OperadorInfo> lista = (from operadorInfo in dt.AsEnumerable()
                                            select new OperadorInfo
                                                       {
                                                           OperadorID = operadorInfo.Field<int>("OperadorID"),
                                                           Nombre = operadorInfo.Field<string>("Nombre"),
                                                           ApellidoPaterno =
                                                               operadorInfo.Field<string>("ApellidoPaterno"),
                                                           ApellidoMaterno =
                                                               operadorInfo.Field<string>("ApellidoMaterno"),
                                                           CodigoSAP = operadorInfo.Field<string>("CodigoSAP"),
                                                           Rol = new RolInfo
                                                                     {
                                                                         RolID = operadorInfo.Field<int>("RolID"),
                                                                         Descripcion = operadorInfo.Field<string>("Rol"),
                                                                     },
                                                           Usuario = (operadorInfo["UsuarioID"] != DBNull.Value)
                                                                         ? 
                                                                         new UsuarioInfo
                                                                               {
                                                                                   UsuarioID = operadorInfo.Field<int>("UsuarioID"),
                                                                                   Nombre = operadorInfo.Field<string>("Usuario"),
                                                                               }
                                                                         : null,
                                                           Organizacion = new OrganizacionInfo
                                                                              {
                                                                                  OrganizacionID =
                                                                                      operadorInfo.Field<int>(
                                                                                          "OrganizacionID"),
                                                                                  Descripcion =
                                                                                      operadorInfo.Field<string>(
                                                                                          "Descripcion")
                                                                              },
                                                           Activo = operadorInfo.Field<bool>("Activo").BoolAEnum(),
                                                       }).ToList();
                resultado = new ResultadoInfo<OperadorInfo>
                    {
                        Lista = lista,
                        TotalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
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
        internal static OperadorInfo ObtenerPorID(DataSet ds)
        {
            OperadorInfo operadorInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                operadorInfo = (from operador in dt.AsEnumerable()
                                select new OperadorInfo
                                           {
                                               OperadorID = operador.Field<int>("OperadorID"),
                                               Nombre = operador.Field<string>("Nombre"),
                                               ApellidoPaterno = operador.Field<string>("ApellidoPaterno"),
                                               ApellidoMaterno = operador.Field<string>("ApellidoMaterno"),
                                               CodigoSAP = operador.Field<string>("CodigoSAP"),
                                               Rol = new RolInfo
                                                         {
                                                             RolID = operador.Field<int>("RolID"),
                                                             Descripcion = operador.Field<string>("Rol")
                                                         },
                                               Usuario = (operador["UsuarioID"] != DBNull.Value)
                                                             ? new UsuarioInfo
                                                                   {
                                                                       UsuarioID = operador.Field<int>("UsuarioID"),
                                                                       Nombre = operador.Field<string>("Usuario"),
                                                                       Organizacion = new OrganizacionInfo
                                                                                          {
                                                                                              OrganizacionID =
                                                                                                  operador.Field<int>(
                                                                                                      "OrganizacionID"),
                                                                                              Descripcion =
                                                                                                  operador.Field<string>
                                                                                                  ("Organizacion")
                                                                                          }
                                                                   }
                                                             : null,
                                               Organizacion = new OrganizacionInfo
                                                                  {
                                                                      OrganizacionID =
                                                                          operador.Field<int>("OrganizacionID"),
                                                                      Descripcion =
                                                                          operador.Field<string>("Organizacion")
                                                                  },
                                               Activo = operador.Field<bool>("Activo").BoolAEnum(),
                                           }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return operadorInfo;
        }

        /// <summary>
        ///     Metodo que obtiene operadores por Id rol
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<OperadorInfo> ObtenerPorIdRol(DataSet ds)
        {
            IList<OperadorInfo> operadorInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                operadorInfo = (from operador in dt.AsEnumerable()
                                select new OperadorInfo
                                           {
                                               OperadorID = operador.Field<int>("OperadorID"),
                                               Nombre = operador.Field<string>("Nombre"),
                                               ApellidoPaterno = operador.Field<string>("ApellidoPaterno"),
                                               ApellidoMaterno = operador.Field<string>("ApellidoMaterno"),
                                               Rol = new RolInfo
                                                         {
                                                             RolID = operador.Field<int>("RolID"),
                                                             Descripcion = operador.Field<string>("Rol")
                                                         },
                                               Usuario = (operador["UsuarioID"] != DBNull.Value)
                                                             ? new UsuarioInfo
                                                                   {
                                                                       UsuarioID = operador.Field<int>("UsuarioID"),
                                                                       Nombre = operador.Field<string>("Nombre"),
                                                                       Organizacion = new OrganizacionInfo
                                                                                          {
                                                                                              OrganizacionID = operador.Field<int>("OrganizacionID"),
                                                                                              Descripcion = operador.Field<string>("Organizacion")
                                                                                          }
                                                                   }
                                                             : null,
                                               Activo = operador.Field<bool>("Activo").BoolAEnum(),
                                               Organizacion = new OrganizacionInfo
                                               {
                                                   OrganizacionID = operador.Field<int>("OrganizacionID"),
                                                   Descripcion = operador.Field<string>("Organizacion")
                                               },
                                           }).ToList<OperadorInfo>();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return operadorInfo;
        }

        /// <summary>
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static OperadorInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                OperadorInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new OperadorInfo
                         {
                             OperadorID = info.Field<int>("OperadorID"),
                             Nombre = info.Field<string>("Nombre"),
                             ApellidoPaterno = info.Field<string>("ApellidoPaterno"),
                             ApellidoMaterno = info.Field<string>("ApellidoMaterno"),
                             CodigoSAP = info.Field<string>("CodigoSAP"),
                             Rol = new RolInfo
                                       {
                                           RolID = info.Field<int>("RolID")
                                       },
                             Usuario = (info["UsuarioID"] != DBNull.Value)
                                                                        ?
                                                                        new UsuarioInfo
                                                                        {
                                                                            UsuarioID = info.Field<int>("UsuarioID"),
                                                                        }
                                                                        : null,
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
        /// Obtiene una instancia de operador
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static OperadorInfo ObtenerPorOperador(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                OperadorInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new OperadorInfo
                         {
                             OperadorID = info.Field<int>("OperadorID"),
                             Nombre = info.Field<string>("Nombre"),
                             ApellidoPaterno = info.Field<string>("ApellidoPaterno"),
                             ApellidoMaterno = info.Field<string>("ApellidoMaterno"),
                             CodigoSAP = info.Field<string>("CodigoSAP"),
                             Rol = new RolInfo
                                       {
                                           RolID = info.Field<int>("RolID")
                                       },
                             Usuario = (info["UsuarioID"] != DBNull.Value)
                                                                       ?
                                                                       new UsuarioInfo
                                                                       {
                                                                           UsuarioID = info.Field<int>("UsuarioID"),
                                                                       }
                                                                       : null,
                             Organizacion = new OrganizacionInfo
                                                {
                                                    OrganizacionID = info.Field<int>("OrganizacionID")
                                                },
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
        /// Obtiene un Operador configurado como usuario
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static OperadorInfo ObtenerPorUsuarioIdRol(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                OperadorInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new OperadorInfo
                         {
                             OperadorID = info.Field<int>("OperadorID"),
                             Nombre = info.Field<string>("Nombre"),
                             ApellidoPaterno = info.Field<string>("ApellidoPaterno"),
                             ApellidoMaterno = info.Field<string>("ApellidoMaterno"),
                             CodigoSAP = info.Field<string>("CodigoSAP"),
                             Rol = new RolInfo { RolID = info.Field<int>("RolID") },
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID") },
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
        /// Obtiene un Operador configurado como usuario
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static OperadorInfo ObtenerPorUsuarioID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                OperadorInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new OperadorInfo
                         {
                             OperadorID = info.Field<int>("OperadorID"),
                             Nombre = info.Field<string>("Nombre"),
                             ApellidoPaterno = info.Field<string>("ApellidoPaterno"),
                             ApellidoMaterno = info.Field<string>("ApellidoMaterno"),
                             CodigoSAP = info.Field<string>("CodigoSAP"),
                             Rol = new RolInfo {    RolID = info.Field<int>("RolID"),
                                                    Activo = info.Field<bool>("EstatusRol").BoolAEnum() 
                                    },
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID") },
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             EstatusUsuario = info.Field<bool>("EstatusUsuario").BoolAEnum(),
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
        /// Obtiene un el operador detector del corral seleccionado
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static OperadorInfo ObtenerDetectorCorral(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                OperadorInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new OperadorInfo
                         {
                             OperadorID = info.Field<int>("OperadorID"),
                             Nombre = info.Field<string>("Nombre"),
                             ApellidoPaterno = info.Field<string>("ApellidoPaterno"),
                             ApellidoMaterno = info.Field<string>("ApellidoMaterno"),
                             CodigoSAP = info.Field<string>("CodigoSAP"),
                             Rol = new RolInfo { RolID = info.Field<int>("RolID") },
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID") },
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
        /// Obtiene las notificaciones para un operador
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<NotificacionInfo> ObtenerNotificacionesDeteccionLista(DataSet ds)
        {
            IList<NotificacionInfo> notificacionInfo;
            notificacionInfo = null;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                notificacionInfo = (from operador in dt.AsEnumerable()
                                    select new NotificacionInfo
                                {
                                    supervisionGanadoID = operador.Field<int>("SupervisionGanadoID"),
                                    codigo = operador.Field<string>("Codigo"),
                                    descripcion = operador.Field<string>("Descripcion"),
                                    acuerdo = operador.Field<string>("Acuerdo"),
                                    arete = operador.Field<string>("Arete"),
                                    areteTestigo = operador.Field<string>("AreteTestigo"),
                                    fotoSupervision = operador.Field<string>("FotoSupervision")
                                }).ToList<NotificacionInfo>();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return notificacionInfo;
        }

        /// <summary>
        /// Obtiene un Operador por Organizacion
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static OperadorInfo ObtenerOperadorPorOrganizacion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                OperadorInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new OperadorInfo
                         {
                             OperadorID = info.Field<int>("OperadorID"),
                             Nombre = info.Field<string>("Nombre"),
                             ApellidoPaterno = info.Field<string>("ApellidoPaterno"),
                             ApellidoMaterno = info.Field<string>("ApellidoMaterno"),
                             CodigoSAP = info.Field<string>("CodigoSAP"),
                             Rol = new RolInfo
                             {
                                 RolID = info.Field<int>("RolID")
                             },
                             Usuario = (info["UsuarioID"] != DBNull.Value)
                                                                       ?
                                                                       new UsuarioInfo
                                                                       {
                                                                           UsuarioID = info.Field<int>("UsuarioID"),
                                                                       }
                                                                       : null,
                             Organizacion = new OrganizacionInfo
                             {
                                 OrganizacionID = info.Field<int>("OrganizacionID")
                             },
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
        public static List<OperadorInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                 DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<OperadorInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new OperadorInfo
                         {
                             OperadorID = info.Field<int>("OperadorID"),
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID")},
                             Nombre = info.Field<string>("Nombre"),
                             ApellidoPaterno = info.Field<string>("ApellidoPaterno"),
                             ApellidoMaterno = info.Field<string>("ApellidoMaterno"),
                             CodigoSAP = info.Field<string>("CodigoSAP"),
                             Rol = new RolInfo { RolID = info.Field<int>("RolID")},
                             Usuario = info["UsuarioID"] == DBNull.Value ? null :  new UsuarioInfo { UsuarioID = info.Field<int>("UsuarioID") },
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
        ///     Metodo que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static OperadorInfo ObtenerBasculistaPorId(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                var lista = (from info in dt.AsEnumerable()
                             select new OperadorInfo()
                             {
                                 OperadorID = info.Field<int>("OperadorID"),
                                 NombreCompleto = string.Format("{0} {1} {2}", info.Field<string>("Nombre"), info.Field<string>("ApellidoPaterno"), info.Field<string>("ApellidoMaterno")),
                                 Usuario = new UsuarioInfo() {UsuarioID = info.Field<int>("UsuarioID")},
                             }).First();
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

        }

        /// <summary>
        /// Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<OperadorInfo> ObtenerPorPaginaCompletoBasculista(DataSet ds)
        {
            ResultadoInfo<OperadorInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<OperadorInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new OperadorInfo()
                         {
                             OperadorID = info.Field<int>("OperadorID"),
                             NombreCompleto = string.Format("{0} {1} {2}", info.Field<string>("Nombre"), info.Field<string>("ApellidoPaterno"), info.Field<string>("ApellidoMaterno")),
                             Usuario = new UsuarioInfo() { UsuarioID = info.Field<int?>("UsuarioID") == null ? 0 : info.Field<int>("UsuarioID") },
                         }).ToList();

                resultado = new ResultadoInfo<OperadorInfo>
                {
                    Lista = lista,
                    TotalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
                };
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