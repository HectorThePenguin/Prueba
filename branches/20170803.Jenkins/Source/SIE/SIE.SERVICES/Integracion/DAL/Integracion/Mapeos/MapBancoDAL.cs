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
    internal static class MapBancoDAL
    {

        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<BancoInfo> ObtenerPorPagina(DataSet ds)
        {
            ResultadoInfo<BancoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<BancoInfo> lista = (from bancoInfo in dt.AsEnumerable()
                                           select new BancoInfo
                                           {
                                               BancoID = bancoInfo.Field<int>("BancoID"),
                                               Descripcion = bancoInfo.Field<string>("Descripcion"),
                                               Telefono = bancoInfo.Field<string>("Telefono"),
                                               Pais = new PaisInfo
                                                        {
                                                            PaisID = bancoInfo.Field<int>("PaisID"),
                                                            Descripcion = bancoInfo.Field<string>("Pais"),
                                                        },
                                               Activo = bancoInfo.Field<bool>("Activo").BoolAEnum()
                                           }).ToList();
                resultado = new ResultadoInfo<BancoInfo>
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
        internal static BancoInfo ObtenerPorID(DataSet ds)
        {
            BancoInfo bancoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                bancoInfo = (from banco in dt.AsEnumerable()
                               select new BancoInfo
                               {
                                   BancoID = banco.Field<int>("BancoID"),
                                   Descripcion = banco.Field<string>("Descripcion"),
                                   Telefono = banco.Field<string>("Telefono"),
                                   Pais = new PaisInfo
                                            {
                                                PaisID = banco.Field<int>("PaisID"),
                                                Descripcion = banco.Field<string>("Pais"),
                                            }
                               }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return bancoInfo;
        }

        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static BancoInfo ObtenerPorTelefono(DataSet ds)
        {
            BancoInfo bancoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                bancoInfo = (from banco in dt.AsEnumerable()
                             select new BancoInfo
                             {
                                 BancoID = banco.Field<int>("BancoID"),
                                 Descripcion = banco.Field<string>("Descripcion"),
                                 Telefono = banco.Field<string>("Telefono"),
                                 Pais = new PaisInfo
                                            {
                                                PaisID = banco.Field<int>("PaisID"),
                                                Descripcion = banco.Field<string>("Pais"),
                                            }
                             }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return bancoInfo;
        }

        /// <summary>
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static BancoInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                BancoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new BancoInfo
                         {
                             BancoID = info.Field<int>("BancoID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Telefono = info.Field<string>("Telefono"),
                             Pais = new PaisInfo
                                        {
                                            PaisID = info.Field<int>("PaisID"),
                                            Descripcion = info.Field<string>("Pais"),
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
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static BancoInfo ObtenerPorPaisID(DataSet ds)
        {
            BancoInfo bancoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                bancoInfo = (from banco in dt.AsEnumerable()
                             select new BancoInfo
                             {
                                 BancoID = banco.Field<int>("BancoID"),
                                 Descripcion = banco.Field<string>("Descripcion"),
                                 Telefono = banco.Field<string>("Telefono"),
                                 Pais = new PaisInfo
                                            {
                                                PaisID = banco.Field<int>("PaisID"),
                                                Descripcion = banco.Field<string>("Pais")
                                            }
                             }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return bancoInfo;
        }

        internal static List<BancoInfo> ObtenerTodos(DataSet ds)
        {
            List<BancoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from bancoInfo in dt.AsEnumerable()
                                         select new BancoInfo
                                         {
                                             BancoID = bancoInfo.Field<int>("BancoID"),
                                             Descripcion = bancoInfo.Field<string>("Descripcion"),
                                             Telefono = bancoInfo.Field<string>("Telefono"),
                                             Pais = new PaisInfo
                                             {
                                                 PaisID = bancoInfo.Field<int>("PaisID"),
                                                 Descripcion = bancoInfo.Field<string>("Pais"),
                                             },
                                             Activo = bancoInfo.Field<bool>("Activo").BoolAEnum()
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
