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
    internal  class MapClienteDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static ResultadoInfo<ClienteInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ClienteInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ClienteInfo
                             {
								ClienteID = info.Field<int>("ClienteID"),
								CodigoSAP = info.Field<string>("CodigoSAP"),
								Descripcion = info.Field<string>("Descripcion"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<ClienteInfo>
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
        internal  static List<ClienteInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ClienteInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ClienteInfo
                             {
								ClienteID = info.Field<int>("ClienteID"),
								CodigoSAP = info.Field<string>("CodigoSAP"),
								Descripcion = info.Field<string>("Descripcion"),
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
        internal  static ClienteInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ClienteInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ClienteInfo
                             {
								ClienteID = info.Field<int>("ClienteID"),
								CodigoSAP = info.Field<string>("CodigoSAP"),
								Descripcion = info.Field<string>("Descripcion"),
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
        internal  static ClienteInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ClienteInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ClienteInfo
                             {
								ClienteID = info.Field<int>("ClienteID"),
								CodigoSAP = info.Field<string>("CodigoSAP"),
								Descripcion = info.Field<string>("Descripcion"),
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

        internal  static ResultadoInfo<ClienteInfo> ObtenerTodosClientes(DataSet ds)
        {
            ResultadoInfo<ClienteInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<ClienteInfo> listaClientes = (from cliente in dt.AsEnumerable()
                                                   select new ClienteInfo
                                                   {
                                                       ClienteID = cliente.Field<int>("ClienteID"),
                                                       CodigoSAP = cliente.Field<string>("CodigoSAP"),
                                                       Descripcion = cliente.Field<string>("Descripcion"),
                                                       Activo = cliente.Field<bool>("Activo").BoolAEnum()
                                                   }).ToList();
                resultado = new ResultadoInfo<ClienteInfo>
                {
                    Lista = listaClientes,
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

        internal  static ClienteInfo ObtenerPorClienteInfo(DataSet ds)
        {
            ClienteInfo resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select new ClienteInfo
                             {
                                 ClienteID = info.Field<int>("ClienteID"),
                                 CodigoSAP = info.Field<string>("CodigoSAP"),
                                 Descripcion = info.Field<string>("Descripcion"),
                                 Activo = info.Field<bool>("Activo").BoolAEnum()
                             }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        public static ResultadoInfo<ClienteInfo> ObtenerClientesAyudaPorPagina(DataSet ds)
        {
            ResultadoInfo<ClienteInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<ClienteInfo> listaClientes = (from cliente in dt.AsEnumerable()
                                                   select new ClienteInfo
                                                   {
                                                       ClienteID = cliente.Field<int>("ClienteID"),
                                                       CodigoSAP = cliente.Field<string>("CodigoSAP"),
                                                       Descripcion = cliente.Field<string>("Descripcion")
                                                   }).ToList();
                resultado = new ResultadoInfo<ClienteInfo>
                {
                    Lista = listaClientes,
                    TotalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
                };
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public static ClienteInfo Obtener_Nombre_CodigoSAP_PorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ClienteInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ClienteInfo
                         {
                             ClienteID = info.Field<int>("ClienteID"),
                             CodigoSAP = info.Field<string>("CodigoSAP"),
                             Descripcion = info.Field<string>("Descripcion")
                         }).First();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static int ObtenerTotalClientesActivos(DataSet ds)
        {
            int resultado = 0;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = int.Parse(dt.Rows[0][0].ToString());
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
