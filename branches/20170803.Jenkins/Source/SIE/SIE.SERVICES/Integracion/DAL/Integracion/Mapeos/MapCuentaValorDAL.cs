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
    public class MapCuentaValorDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<CuentaValorInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CuentaValorInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CuentaValorInfo
                             {
								CuentaValorID = info.Field<int>("CuentaValorID"),
								Cuenta = new CuentaInfo { CuentaID = info.Field<int>("CuentaID"), Descripcion = info.Field<string>("Cuenta") },
								Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
								Valor = info.Field<string>("Valor"),
                                //Usuario = new UsuarioInfo { UsuarioID = info.Field<int>("UsuarioID"), Descripcion = info.Field<string>("Usuario") },
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<CuentaValorInfo>
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
        public static List<CuentaValorInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CuentaValorInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CuentaValorInfo
                             {
								CuentaValorID = info.Field<int>("CuentaValorID"),
								Cuenta = new CuentaInfo { CuentaID = info.Field<int>("CuentaID"), Descripcion = info.Field<string>("Cuenta") },
								Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
								Valor = info.Field<string>("Valor"),
								//Usuario = new UsuarioInfo { UsuarioID = info.Field<int>("UsuarioID"), Descripcion = info.Field<string>("Usuario") },
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
        public static CuentaValorInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CuentaValorInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CuentaValorInfo
                             {
								CuentaValorID = info.Field<int>("CuentaValorID"),
								Cuenta = new CuentaInfo { CuentaID = info.Field<int>("CuentaID"), Descripcion = info.Field<string>("Cuenta") },
								Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
								Valor = info.Field<string>("Valor"),
                                //Usuario = new UsuarioInfo { UsuarioID = info.Field<int>("UsuarioID"), Descripcion = info.Field<string>("Usuario") },
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
        public static CuentaValorInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CuentaValorInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CuentaValorInfo
                             {
								CuentaValorID = info.Field<int>("CuentaValorID"),
								Cuenta = new CuentaInfo { CuentaID = info.Field<int>("CuentaID"), Descripcion = info.Field<string>("Cuenta") },
								Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
								Valor = info.Field<string>("Valor"),
                                //Usuario = new UsuarioInfo { UsuarioID = info.Field<int>("UsuarioID"), Descripcion = info.Field<string>("Usuario") },
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
        public static CuentaValorInfo ObtenerPorFiltros(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CuentaValorInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CuentaValorInfo
                         {
                             CuentaValorID = info.Field<int>("CuentaValorID"),
                             Cuenta = new CuentaInfo { CuentaID = info.Field<int>("CuentaID"), Descripcion = info.Field<string>("Cuenta") },
                             Organizacion = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), Descripcion = info.Field<string>("Organizacion") },
                             Valor = info.Field<string>("Valor"),
                             //Usuario = new UsuarioInfo { UsuarioID = info.Field<int>("UsuarioID"), Descripcion = info.Field<string>("Usuario") },
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

