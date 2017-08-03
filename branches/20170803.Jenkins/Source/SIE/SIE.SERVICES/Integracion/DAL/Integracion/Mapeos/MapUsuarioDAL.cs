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
    internal static class MapUsuarioDAL
    {
        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<UsuarioInfo> ObtenerPorPagina(DataSet ds)
        {
            ResultadoInfo<UsuarioInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<UsuarioInfo> lista = (from usuarioInfo in dt.AsEnumerable()
                                           select new UsuarioInfo
                                                      {
                                                          UsuarioID = usuarioInfo.Field<int>("UsuarioID"),
                                                          Nombre = usuarioInfo.Field<string>("Nombre"),
                                                          OrganizacionID = usuarioInfo.Field<int>("OrganizacionID"),
                                                          Organizacion =
                                                              new OrganizacionInfo
                                                                  {
                                                                      OrganizacionID =
                                                                          usuarioInfo.Field<int>("OrganizacionID"),
                                                                      Descripcion =
                                                                          usuarioInfo.Field<string>("Organizacion")
                                                                  },
                                                          UsuarioActiveDirectory =
                                                              usuarioInfo.Field<string>("UsuarioActiveDirectory"),
                                                              Corporativo = usuarioInfo.Field<bool>("Corporativo"),
                                                          Activo = usuarioInfo.Field<bool>("Activo").BoolAEnum(),
                                                          NivelAcceso = usuarioInfo.Field<int>("NivelAcceso").NivelAccesoEnum()
                                                      }).ToList();
                resultado = new ResultadoInfo<UsuarioInfo>
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
        internal static UsuarioInfo ObtenerPorID(DataSet ds)
        {
            UsuarioInfo usuarioInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                usuarioInfo = (from usuario in dt.AsEnumerable()
                               select new UsuarioInfo
                                          {
                                              UsuarioID = usuario.Field<int>("UsuarioID"),
                                              Nombre = usuario.Field<string>("Nombre"),
                                              OrganizacionID = usuario.Field<int>("OrganizacionID"),
                                              Organizacion =
                                                  new OrganizacionInfo
                                                      {
                                                          OrganizacionID =
                                                              usuario.Field<int>("OrganizacionID"),
                                                          Descripcion = usuario.Field<string>("Organizacion")
                                                      },
                                              UsuarioActiveDirectory = usuario.Field<string>("UsuarioActiveDirectory"),
                                              Corporativo = usuario.Field<bool>("Corporativo")
                                          }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return usuarioInfo;
        }

        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static UsuarioInfo ObtenerPorActiveDirectory(DataSet ds)
        {
            UsuarioInfo usuarioInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                usuarioInfo = (from usuario in dt.AsEnumerable()
                               select new UsuarioInfo
                               {
                                   UsuarioID = usuario.Field<int>("UsuarioID"),
                                   Nombre = usuario.Field<string>("Nombre"),
                                   OrganizacionID = usuario.Field<int>("OrganizacionID"),
                                   Organizacion =
                                       new OrganizacionInfo
                                       {
                                           OrganizacionID =
                                               usuario.Field<int>("OrganizacionID"),
                                       },
                                   UsuarioActiveDirectory = usuario.Field<string>("UsuarioActiveDirectory"),
                                   Corporativo = usuario.Field<bool>("Corporativo"),
                                   NivelAcceso = usuario.Field<int>("NivelAcceso").NivelAccesoEnum()
                               }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return usuarioInfo;
        }

        /// <summary>
        /// Metodo que obtiene un Usuario
        /// con su Rol
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static UsuarioInfo ObtenerPorSupervisorID(DataSet ds)
        {
            UsuarioInfo usuarioInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                usuarioInfo = (from usuario in dt.AsEnumerable()
                               select new UsuarioInfo
                               {
                                   UsuarioID = usuario.Field<int>("UsuarioID"),
                                   Nombre = usuario.Field<string>("Nombre"),
                                   OrganizacionID = usuario.Field<int>("OrganizacionID"),
                                   Organizacion =
                                       new OrganizacionInfo
                                       {
                                           OrganizacionID =
                                               usuario.Field<int>("OrganizacionID"),
                                           Descripcion = usuario.Field<string>("Organizacion")
                                       },
                                   UsuarioActiveDirectory = usuario.Field<string>("UsuarioActiveDirectory"),
                                   Corporativo = usuario.Field<bool>("Corporativo"),
                                   Operador = new OperadorInfo
                                                  {
                                                      Rol = new RolInfo
                                                                {
                                                                    RolID = usuario.Field<int?>("RolID") ?? 0,
                                                                    Descripcion = usuario.Field<string>("Rol")
                                                                }
                                                  }
                               }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return usuarioInfo;
        }

        /// <summary>
        /// Metodo que obtiene un Usuario
        /// con su Rol
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static UsuarioInfo ObtenerNivelAlertaPorUsuarioID(DataSet ds)
        {
            UsuarioInfo usuarioInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                usuarioInfo = (from usuario in dt.AsEnumerable()
                               select new UsuarioInfo
                               {
                                   UsuarioID = usuario.Field<int>("UsuarioID"),
                                   UsuarioActiveDirectory = usuario.Field<string>("UsuarioActiveDirectory"),
                                   Corporativo = usuario.Field<bool>("Corporativo"),
                                   OrganizacionID = usuario.Field<int>("OrganizacionID"),
                                   Operador = new OperadorInfo
                                                  {
                                                      Rol = new RolInfo
                                                                {
                                                                    RolID = usuario.Field<int?>("RolID") ?? 0,
                                                                    Descripcion = usuario.Field<string>("Descripcion"),
                                                                    NivelAlerta = new NivelAlertaInfo()
                                                                    {
                                                                        NivelAlertaId = usuario.Field<int>("NivelAlertaID")
                                                                    }
                                                                }
                                                  }
                               }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return usuarioInfo;
        }

        /// <summary>
        /// Método que mapea un listado de usuarios
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<UsuarioInfo> ObtenerListado(DataSet ds)
        {
            List<UsuarioInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from usuarioInfo in dt.AsEnumerable()
                            select new UsuarioInfo
                            {
                                UsuarioID = usuarioInfo.Field<int>("UsuarioID"),
                                Nombre = usuarioInfo.Field<string>("Nombre"),
                                OrganizacionID = usuarioInfo.Field<int>("OrganizacionID"),
                                UsuarioActiveDirectory = usuarioInfo.Field<string>("UsuarioActiveDirectory"),
                                Corporativo = usuarioInfo.Field<bool>("Corporativo"),
                                Activo = usuarioInfo.Field<bool>("Activo").BoolAEnum()
                            }).ToList();
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