using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal  class MapEntradaGanadoCalidadDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static ResultadoInfo<EntradaGanadoCalidadInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<EntradaGanadoCalidadInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new EntradaGanadoCalidadInfo
                             {
								EntradaGanadoCalidadID = info.Field<int>("EntradaGanadoCalidadID"),
								EntradaGanadoID = info.Field<int>("EntradaGanadoID"),
								CalidadGanado = new CalidadGanadoInfo { CalidadGanadoID = info.Field<int>("CalidadGanadoID"), Descripcion = info.Field<string>("CalidadGanado") },
								Valor = info.Field<int>("Valor"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<EntradaGanadoCalidadInfo>
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
        internal  static List<EntradaGanadoCalidadInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<EntradaGanadoCalidadInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new EntradaGanadoCalidadInfo
                             {
								EntradaGanadoCalidadID = info.Field<int>("EntradaGanadoCalidadID"),
								EntradaGanadoID = info.Field<int>("EntradaGanadoID"),
								CalidadGanado = new CalidadGanadoInfo { CalidadGanadoID = info.Field<int>("CalidadGanadoID"), Descripcion = info.Field<string>("CalidadGanado") },
								Valor = info.Field<int>("Valor"),
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
        internal  static EntradaGanadoCalidadInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                EntradaGanadoCalidadInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new EntradaGanadoCalidadInfo
                             {
								EntradaGanadoCalidadID = info.Field<int>("EntradaGanadoCalidadID"),
								EntradaGanadoID = info.Field<int>("EntradaGanadoID"),
								CalidadGanado = new CalidadGanadoInfo { CalidadGanadoID = info.Field<int>("CalidadGanadoID"), Descripcion = info.Field<string>("CalidadGanado") },
								Valor = info.Field<int>("Valor"),
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
        internal  static EntradaGanadoCalidadInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                EntradaGanadoCalidadInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new EntradaGanadoCalidadInfo
                             {
								EntradaGanadoCalidadID = info.Field<int>("EntradaGanadoCalidadID"),
								EntradaGanadoID = info.Field<int>("EntradaGanadoID"),
								CalidadGanado = new CalidadGanadoInfo { CalidadGanadoID = info.Field<int>("CalidadGanadoID"), Descripcion = info.Field<string>("CalidadGanado") },
								Valor = info.Field<int>("Valor"),
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
        internal  static List<EntradaGanadoCalidadInfo> ObtenerPorEntradaGanadoID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<EntradaGanadoCalidadInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new EntradaGanadoCalidadInfo
                             {
                                 EntradaGanadoCalidadID = info.Field<int?>("EntradaGanadoCalidadID") ?? 0,
                                 EntradaGanadoID = info.Field<int?>("EntradaGanadoID") ?? 0,
                                 CalidadGanado = new CalidadGanadoInfo
                                                     {
                                                         CalidadGanadoID = info.Field<int?>("CalidadGanadoID") ?? 0,
                                                         Descripcion = info.Field<string>("CalidadGanado"),
                                                         Sexo = Convert.ToChar(info.Field<string>("Sexo")) == 'M'
                                                                    ? Sexo.Macho
                                                                    : Sexo.Hembra,
                                                         Calidad = info.Field<string>("Calidad")
                                                     },
                                 Valor = info.Field<int?>("Valor") ?? 0,
                                 Activo =
                                     info.Field<bool?>("Activo") == null
                                         ? EstatusEnum.Activo
                                         : info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}

