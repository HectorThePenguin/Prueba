using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using CrystalDecisions.Shared.Json;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal  class MapCamionDAL
    {

        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static ResultadoInfo<CamionInfo> ObtenerPorPagina(DataSet ds)
        {
            ResultadoInfo<CamionInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<CamionInfo> lista = (from info in dt.AsEnumerable()
                                          select new CamionInfo
                                                     {
                                                         CamionID = info.Field<int>("CamionId"),
                                                         Proveedor =
                                                             new ProveedorInfo
                                                                 {
                                                                     ProveedorID = info.Field<int>("ProveedorID"),
                                                                     Descripcion = info.Field<string>("Proveedor"),
                                                                     CodigoSAP = info.Field<string>("CodigoSAP")
                                                                 },
                                                         PlacaCamion = info.Field<string>("PlacaCamion"),
                                                         Economico = (info.Field<string>("NumEconomico") == null) ? "" : info.Field<string>("NumEconomico"),
                                                         Color = (info.Field<string>("Color") == null) ? "" : info.Field<string>("Color"),
                                                         MarcaID = info["MarcaID"] == DBNull.Value ? 0 : info.Field<int>("MarcaID"),
                                                         MarcaDescripcion = (info.Field<string>("MarcaDescripcion") == null) ? "" : info.Field<string>("MarcaDescripcion"),
                                                         Modelo = info.Field<int?>("Modelo"),
                                                         Boletinado = (info.Field<bool?>("Boletinado") == null) ? false : info.Field<bool>("Boletinado"),
                                                         ObservacionesObtener = (info.Field<string>("Observaciones") == null) ? "" : info.Field<string>("Observaciones"),
                                                         Activo = info.Field<bool>("Activo").BoolAEnum()
                                                     }).ToList();

                resultado = new ResultadoInfo<CamionInfo>
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
        internal  static List<CamionInfo> ObtenerTodos(DataSet ds)
        {
            List<CamionInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new CamionInfo
                         {
                             CamionID = info.Field<int>("CamionId"),
                             Proveedor = new ProveedorInfo { ProveedorID = info.Field<int>("ProveedorID") },
                             PlacaCamion = info.Field<string>("PlacaCamion"),
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
        internal  static CamionInfo ObtenerPorID(DataSet ds)
        {
            CamionInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new CamionInfo
                         {
                             CamionID = info.Field<int>("CamionId"),
                             Proveedor = new ProveedorInfo { ProveedorID = info.Field<int>("ProveedorID") },
                             PlacaCamion = info.Field<string>("PlacaCamion"),
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
        ///     Metodo que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static List<CamionInfo> ObtenerPorProveedorID(DataSet ds)
        {
            List<CamionInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new CamionInfo
                         {
                             CamionID = info.Field<int>("CamionId"),
                             Proveedor = new ProveedorInfo { ProveedorID = info.Field<int>("ProveedorID") },
                             PlacaCamion = info.Field<string>("PlacaCamion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             Economico = info.Field<string>("NumEconomico")
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
        /// Obtiene una entidad de CamionInfo
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static CamionInfo ObtenerPorInfoDependencias(DataSet ds)
        {
            CamionInfo camion;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                camion = (from info in dt.AsEnumerable()
                          select new CamionInfo
                                     {
                                         CamionID = info.Field<int>("CamionID"),
                                         Proveedor = new ProveedorInfo
                                                         {
                                                             ProveedorID = info.Field<int>("ProveedorID")
                                                         },
                                         PlacaCamion = info.Field<string>("PlacaCamion"),
                                         Activo = info.Field<bool>("Activo").BoolAEnum(),
                                     }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return camion;
        }

        internal  static ResultadoInfo<CamionInfo> ObtenerPorDependencias(DataSet ds)
        {
            ResultadoInfo<CamionInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<CamionInfo> lista = (from info in dt.AsEnumerable()
                                          select new CamionInfo
                                          {
                                              CamionID = info.Field<int>("CamionID"),
                                              Proveedor = new ProveedorInfo
                                                         {
                                                             ProveedorID = info.Field<int>("ProveedorID")
                                                         },
                                              PlacaCamion = info.Field<string>("PlacaCamion"),
                                              Activo = info.Field<bool>("Activo").BoolAEnum(),                                              
                                          }).ToList();

                resultado = new ResultadoInfo<CamionInfo>
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
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static CamionInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CamionInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CamionInfo
                             {
                                 CamionID = info.Field<int>("CamionID"),
                                 Proveedor =
                                     new ProveedorInfo
                                         {
                                             ProveedorID = info.Field<int>("ProveedorID"),
                                             Descripcion = info.Field<string>("Proveedor"),
                                             CodigoSAP = info.Field<string>("CodigoSAP")
                                         },
                                 PlacaCamion = info.Field<string>("PlacaCamion"),
                                 Economico = info.Field<string>("NumEconomico"),
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
        /// Obtiene un camion por placa
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static CamionInfo ObtenerPorPlaca(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CamionInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CamionInfo
                         {
                             CamionID = info.Field<int>("CamionID"),
                             PlacaCamion = info.Field<string>("PlacaCamion"),
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