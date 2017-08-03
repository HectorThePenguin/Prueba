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
    internal class MapJaulaDAL
    {

        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<JaulaInfo> ObtenerPorPagina(DataSet ds)
        {
            ResultadoInfo<JaulaInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<JaulaInfo> lista = (from info in dt.AsEnumerable()
                                         select new JaulaInfo
                                                    {
                                                        JaulaID = info.Field<int>("JaulaId"),
                                                        Proveedor =
                                                            new ProveedorInfo
                                                            {
                                                                ProveedorID = info.Field<int>("ProveedorID"),
                                                                Descripcion = info.Field<string>("Proveedor"),
                                                                CodigoSAP = info.Field<string>("CodigoSAP")
                                                            },
                                                        PlacaJaula = info.Field<string>("PlacaJaula"),
                                                        Capacidad = info.Field<int?>("Capacidad") == null ? 0 : info.Field<int>("Capacidad"),
                                                        Secciones = info.Field<int?>("Secciones") == null ? 0 : info.Field<int>("Secciones"),
                                                        NumEconomico = info.Field<string>("NumEconomico"),
                                                        Marca = new MarcasInfo()
                                                        {
                                                            MarcaId = info["MarcaID"] == DBNull.Value ? 0 : info.Field<int>("MarcaID"),
                                                            Descripcion = info["Marca"] == DBNull.Value ? "" : info.Field<string>("Marca")
                                                        },
                                                        Modelo = info.Field<int?>("Modelo") == null ? 0 : info.Field<int>("Modelo"),
                                                        Boletinado = info.Field<bool?>("Boletinado") == null ? false : info.Field<bool>("Boletinado"),
                                                        Observaciones = string.IsNullOrEmpty(info.Field<string>("Observaciones")) ? string.Empty : info.Field<string>("Observaciones"),
                                                        Activo = info.Field<bool>("Activo").BoolAEnum(),
                                                    }).ToList();

                resultado = new ResultadoInfo<JaulaInfo>
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
        internal static List<JaulaInfo> ObtenerTodos(DataSet ds)
        {
            List<JaulaInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new JaulaInfo
                                    {
                                        JaulaID = info.Field<int>("JaulaId"),
                                        Proveedor =
                                            new ProveedorInfo
                                                {
                                                    ProveedorID = info.Field<int>("ProveedorID"),
                                                },
                                        PlacaJaula = info.Field<string>("PlacaJaula"),
                                        Capacidad = info.Field<int?>("Capacidad") == null ? 0 : info.Field<int>("Capacidad"),
                                        Secciones = info.Field<int?>("Secciones") == null ? 0 : info.Field<int>("Secciones"),
                                        NumEconomico = info.Field<string>("NumEconomico"),
                                        Marca = 
                                            new MarcasInfo
                                            {
                                                MarcaId = info["MarcaID"] == DBNull.Value ? 0 : info.Field<int>("MarcaID")
                                            },
                                        Boletinado = info.Field<bool?>("Boletinado") == null ? false : info.Field<bool>("Boletinado"),
                                        Observaciones = string.IsNullOrEmpty(info.Field<string>("Observaciones")) ? string.Empty : info.Field<string>("Observaciones"),                 
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
        internal static JaulaInfo ObtenerPorID(DataSet ds)
        {
            JaulaInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new JaulaInfo
                                    {
                                        JaulaID = info.Field<int>("JaulaId"),
                                        Proveedor =  new ProveedorInfo
                                           {
                                               ProveedorID = info.Field<int>("ProveedorID"),
                                           },
                                        PlacaJaula = info.Field<string>("PlacaJaula"),
                                        Capacidad = info.Field<int?>("Capacidad") == null ? 0 : info.Field<int>("Capacidad"),
                                        Secciones = info.Field<int?>("Secciones") == null ? 0 : info.Field<int>("Secciones"),
                                        NumEconomico = info.Field<string>("NumEconomico"),
                                        Marca = new MarcasInfo
                                            {
                                                MarcaId = info["MarcaID"] == DBNull.Value ? 0 : info.Field<int>("MarcaID")
                                            },
                                        Boletinado = info.Field<bool?>("Boletinado") == null ? false : info.Field<bool>("Boletinado"),
                                        Observaciones = string.IsNullOrEmpty(info.Field<string>("Observaciones")) ? string.Empty : info.Field<string>("Observaciones"),
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
        ///     Metodo que obtiene una lista por Proveedor
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<JaulaInfo> ObtenerPorProveedorID(DataSet ds)
        {
            List<JaulaInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new JaulaInfo
                         {
                             JaulaID = info.Field<int>("JaulaId"),
                             Proveedor =
                                           new ProveedorInfo
                                           {
                                               ProveedorID = info.Field<int>("ProveedorID"),
                                           },
                             PlacaJaula = info.Field<string>("PlacaJaula"),
                             Capacidad = info.Field<int>("Capacidad"),
                             Secciones = info.Field<int>("Secciones"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             NumEconomico = info.Field<string>("NumEconomico")
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
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static JaulaInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                JaulaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new JaulaInfo
                             {
                                 JaulaID = info.Field<int>("JaulaID"),
                                 Proveedor =
                                     new ProveedorInfo
                                         {
                                             ProveedorID = info.Field<int>("ProveedorID"),
                                             Descripcion = info.Field<string>("Proveedor"),
                                             CodigoSAP = info.Field<string>("CodigoSAP")
                                         },
                                 PlacaJaula = info.Field<string>("PlacaJaula"),
                                 Capacidad = info.Field<int>("Capacidad"),
                                 Secciones = info.Field<int>("Secciones"),
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                                 NumEconomico = info.Field<string>("NumEconomico")
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