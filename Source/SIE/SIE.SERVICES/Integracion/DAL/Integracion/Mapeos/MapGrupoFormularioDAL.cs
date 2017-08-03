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
    internal class MapGrupoFormularioDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<GrupoFormularioInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<GrupoFormularioInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new GrupoFormularioInfo
                             {
                                 Grupo =
                                     new GrupoInfo
                                         {
                                             GrupoID = info.Field<int>("GrupoID"),
                                             Descripcion = info.Field<string>("Grupo"),
                                             Activo = info.Field<bool>("Activo").BoolAEnum()
                                         },
                             }).ToList();
                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<GrupoFormularioInfo>
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
        internal static List<GrupoFormularioInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<GrupoFormularioInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new GrupoFormularioInfo
                             {
                                 Grupo =
                                     new GrupoInfo
                                         {
                                             GrupoID = info.Field<int>("GrupoID"),
                                             Descripcion = info.Field<string>("Grupo")
                                         },
                                 Formulario =
                                     new FormularioInfo
                                         {
                                             FormularioID = info.Field<int>("FormularioID"),
                                             Descripcion = info.Field<string>("Formulario")
                                         },
                                 Acceso =
                                     new AccesoInfo
                                         {
                                             AccesoId = info.Field<int>("AccesoID"),
                                             Descripcion = info.Field<string>("Acceso")
                                         },
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
        internal static GrupoFormularioInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                GrupoFormularioInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new GrupoFormularioInfo
                             {
                                 Grupo =
                                     new GrupoInfo
                                         {
                                             GrupoID = info.Field<int>("GrupoID"),
                                             Descripcion = info.Field<string>("Grupo")
                                         },
                                 Formulario =
                                     new FormularioInfo
                                         {
                                             FormularioID = info.Field<int>("FormularioID"),
                                             Descripcion = info.Field<string>("Formulario")
                                         },
                                 Acceso =
                                     new AccesoInfo
                                         {
                                             AccesoId = info.Field<int>("AccesoID"),
                                             Descripcion = info.Field<string>("Acceso")
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
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static GrupoFormularioInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                GrupoFormularioInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new GrupoFormularioInfo
                             {
                                 Grupo =
                                     new GrupoInfo
                                         {
                                             GrupoID = info.Field<int>("GrupoID"),
                                             Descripcion = info.Field<string>("Grupo")
                                         },
                                 Formulario =
                                     new FormularioInfo
                                         {
                                             FormularioID = info.Field<int>("FormularioID"),
                                             Descripcion = info.Field<string>("Formulario")
                                         },
                                 Acceso =
                                     new AccesoInfo
                                         {
                                             AccesoId = info.Field<int>("AccesoID"),
                                             Descripcion = info.Field<string>("Acceso")
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
        /// Obtiene una entidad de GrupoFormulario por su GrupoID
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static GrupoFormularioInfo ObtenerPorGrupoID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dtGrupo = ds.Tables[ConstantesDAL.DtGrupo];
                DataTable dtFormulario = ds.Tables[ConstantesDAL.DtFormulario];
                GrupoFormularioInfo resultado =
                    (from grupo in dtGrupo.AsEnumerable()
                     select
                         new GrupoFormularioInfo
                             {
                                 Grupo =
                                     new GrupoInfo
                                         {
                                             GrupoID = grupo.Field<int>("GrupoID"),
                                             Descripcion = grupo.Field<string>("Grupo"),
                                             Activo = grupo.Field<bool>("Activo").BoolAEnum(),
                                         },
                             }).FirstOrDefault();
                if (resultado == null)
                {
                    resultado = new GrupoFormularioInfo();
                }
                resultado.ListaFormulario = (from pantallas in dtFormulario.AsEnumerable()
                                             select new FormularioInfo
                                                        {
                                                            FormularioID =
                                                                pantallas.Field<int>("FormularioID"),
                                                            Descripcion =
                                                                pantallas.Field<string>("Formulario"),
                                                            Modulo = new ModuloInfo
                                                                         {
                                                                             ModuloID =
                                                                                 pantallas.Field<int>(
                                                                                     "ModuloID"),
                                                                             Descripcion =
                                                                                 pantallas.Field
                                                                                 <string>("Modulo"),
                                                                         },
                                                        }).ToList();
                List<FormularioInfo> accesos = (from acceso in ds.Tables[ConstantesDAL.DtAcceso].AsEnumerable()
                                                select new FormularioInfo
                                                           {
                                                               FormularioID = acceso.Field<int>("FormularioID"),
                                                               Acceso = new AccesoInfo
                                                                            {
                                                                                AccesoId =
                                                                                    acceso.Field<int>("AccesoID")
                                                                            },
                                                           }).ToList();
                resultado.ListaFormulario.ForEach(formulario =>
                                                      {
                                                          AccesoInfo accesoInfo =
                                                              accesos.Where(
                                                                  ac => ac.FormularioID ==
                                                                        formulario.FormularioID).Select(
                                                                            ac => ac.Acceso).FirstOrDefault();
                                                          if (accesoInfo == null)
                                                          {
                                                              accesoInfo = new AccesoInfo();
                                                          }
                                                          formulario.Acceso = accesoInfo;
                                                      });
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
