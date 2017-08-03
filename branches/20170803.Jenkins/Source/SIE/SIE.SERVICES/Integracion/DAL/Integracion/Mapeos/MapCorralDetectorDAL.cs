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
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal  class MapCorralDetectorDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static ResultadoInfo<CorralDetectorInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CorralDetectorInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CorralDetectorInfo
                             {
                                 CorralDetectorID = info.Field<int>("CorralDetectorID"),
                                 Operador =
                                     new OperadorInfo
                                         {
                                             OperadorID = info.Field<int>("OperadorID"),
                                             Nombre = info.Field<string>("NombreOperador"),
                                             ApellidoPaterno = info.Field<string>("ApellidoPaternoOperador"),
                                             ApellidoMaterno = info.Field<string>("ApellidoMaternoOperador"),
                                             CodigoSAP = info.Field<string>("CodigoSAP"),
                                             Rol = new RolInfo
                                                       {
                                                           RolID = info.Field<int>("RolID"),
                                                           Descripcion = info.Field<string>("Rol")
                                                       },
                                             Organizacion = new OrganizacionInfo
                                                                {
                                                                    OrganizacionID = info.Field<int>("OrganizacionID"),
                                                                    Descripcion = info.Field<string>("Organizacion")
                                                                }
                                         },
                                 /*Corral =
                                     new CorralInfo
                                         {
                                             CorralID = info.Field<int>("CorralID"),
                                             Codigo = info.Field<string>("CodigoCorral"),
                                             Organizacion = new OrganizacionInfo
                                                                {
                                                                    OrganizacionID = info.Field<int>("OrganizacionID"),
                                                                    Descripcion = info.Field<string>("Organizacion")
                                                                },
                                             TipoCorral = new TipoCorralInfo()
                                         },*/
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<CorralDetectorInfo>
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
        internal static CorralDetectorInfo ObtenerTodosPorDetector(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                //SeleccionInfoModelo<CorralInfo>
                var corralDetectorInfo = new CorralDetectorInfo();

                corralDetectorInfo.Corrales = new List<SeleccionInfoModelo<CorralInfo>>();

                List<SeleccionInfoModelo<CorralInfo>> listaCorrales = 
                    (from info in dt.AsEnumerable()
                     select
                         new SeleccionInfoModelo<CorralInfo>
                         {
                             Elemento = new CorralInfo()
                             {
                                 CorralID = info.Field<int>("CorralID"),
                                 Codigo = info.Field<string>("Codigo"),
                                 CorralDetectorID = info.Field<int>("CorralDetectorID"),
                                 TipoCorral =
                                     new TipoCorralInfo
                                         {
                                             TipoCorralID = info.Field<int>("TipoCorralID"),
                                             Descripcion = info.Field<string>("Descripcion")
                                         }
                             }
                                
                         }).ToList();
                if (listaCorrales != null && listaCorrales.Any())
                {
                    corralDetectorInfo.Operador = (from info in dt.AsEnumerable()
                                                 select new OperadorInfo
                                                     {
                                                         OperadorID = info.Field<int>("OperadorID")
                                                     }).FirstOrDefault();
                    corralDetectorInfo.Corrales = listaCorrales;
                }

                return corralDetectorInfo;
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
        internal  static List<CorralDetectorInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CorralDetectorInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CorralDetectorInfo
                             {
                                 CorralDetectorID = info.Field<int>("CorralDetectorID"),
                                 Operador =
                                     new OperadorInfo
                                         {
                                             OperadorID = info.Field<int>("OperadorID"),
                                             //Descripcion = info.Field<string>("Operador")
                                         },
                                 /*Corral =
                                     new CorralInfo
                                         {
                                             CorralID = info.Field<int>("CorralID"),
                                             //Descripcion = info.Field<string>("Corral")
                                         },
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),*/
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
        internal  static CorralDetectorInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CorralDetectorInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CorralDetectorInfo
                             {
                                 CorralDetectorID = info.Field<int>("CorralDetectorID"),
                                 Operador =
                                     new OperadorInfo
                                         {
                                             OperadorID = info.Field<int>("OperadorID"),
                                             //Descripcion = info.Field<string>("Operador")
                                         },
                                 /*Corral =
                                     new CorralInfo
                                         {
                                             CorralID = info.Field<int>("CorralID"),
                                             //Descripcion = info.Field<string>("Corral")
                                         },*/
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
        internal  static CorralDetectorInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CorralDetectorInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CorralDetectorInfo
                             {
                                 CorralDetectorID = info.Field<int>("CorralDetectorID"),
                                 Operador =
                                     new OperadorInfo
                                         {
                                             OperadorID = info.Field<int>("OperadorID"),
                                             //Descripcion = info.Field<string>("Operador")
                                         },
                                 /*Corral =
                                     new CorralInfo
                                         {
                                             CorralID = info.Field<int>("CorralID"),
                                             //Descripcion = info.Field<string>("Corral")
                                         },*/
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
        /// Obtiene un corral Detector por Opeador y Corral
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static CorralDetectorInfo ObtenerPorOperadorCorral(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CorralDetectorInfo resultado =
                    (from info in dt.AsEnumerable()
                     select
                         new CorralDetectorInfo
                             {
                                 CorralDetectorID = info.Field<int>("CorralDetectorID"),
                                 Operador =
                                     new OperadorInfo
                                         {
                                             OperadorID = info.Field<int>("OperadorID"),
                                             Nombre = info.Field<string>("NombreOperador"),
                                             ApellidoPaterno = info.Field<string>("ApellidoPaternoOperador"),
                                             ApellidoMaterno = info.Field<string>("ApellidoMaternoOperador"),
                                             CodigoSAP = info.Field<string>("CodigoSAP"),
                                         },
                                 /*Corral =
                                     new CorralInfo
                                         {
                                             CorralID = info.Field<int>("CorralID"),
                                             Codigo = info.Field<string>("CodigoCorral"),
                                             Organizacion = new OrganizacionInfo
                                                                {
                                                                    OrganizacionID = info.Field<int>("OrganizacionID"),
                                                                    Descripcion = info.Field<string>("Organizacion")
                                                                }
                                         },*/
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).FirstOrDefault();
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
