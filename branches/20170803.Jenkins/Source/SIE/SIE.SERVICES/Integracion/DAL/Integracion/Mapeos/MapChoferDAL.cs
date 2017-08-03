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
    internal  class MapChoferDAL
    {

        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static ResultadoInfo<ChoferInfo> ObtenerPorPagina(DataSet ds)
        {
            ResultadoInfo<ChoferInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<ChoferInfo> lista = (from info in dt.AsEnumerable()
                                          select new ChoferInfo
                                                     {
                                                         ChoferID = info.Field<int>("ChoferId"),
                                                         Nombre = info.Field<string>("Nombre"),
                                                         ApellidoPaterno = info.Field<string>("ApellidoPaterno"),
                                                         ApellidoMaterno = info.Field<string>("ApellidoMaterno"),
                                                         Activo = info.Field<bool>("Activo").BoolAEnum(),
                                                         Boletinado = info["Boletinado"] == DBNull.Value ? false : info.Field<bool>("Boletinado"),
                                                         Observaciones = info["Observaciones"] == DBNull.Value ? string.Empty : info.Field<string>("Observaciones")
                                                     }).ToList();

                resultado = new ResultadoInfo<ChoferInfo>
                                {
                                    Lista = lista ?? new List<ChoferInfo>(),
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
        internal  static List<ChoferInfo> ObtenerTodos(DataSet ds)
        {
            List<ChoferInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new ChoferInfo
                                    {
                                        ChoferID = info.Field<int>("ChoferId"),
                                        Nombre = info.Field<string>("Nombre"),
                                        ApellidoPaterno = info.Field<string>("ApellidoPaterno"),
                                        ApellidoMaterno = info.Field<string>("ApellidoMaterno"),
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
        internal  static ChoferInfo ObtenerPorID(DataSet ds)
        {
            ChoferInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new ChoferInfo
                                    {
                                        ChoferID = info.Field<int>("ChoferID"),
                                        Nombre = info.Field<string>("Nombre"),
                                        ApellidoPaterno = info.Field<string>("ApellidoPaterno"),
                                        ApellidoMaterno = info.Field<string>("ApellidoMaterno"),
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
        internal  static ChoferInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ChoferInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ChoferInfo
                         {
                             ChoferID = info.Field<int>("ChoferID"),
                             Nombre = info.Field<string>("Nombre"),
                             ApellidoPaterno = info.Field<string>("ApellidoPaterno"),
                             ApellidoMaterno = info.Field<string>("ApellidoMaterno"),
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