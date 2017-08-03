using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Infos;
using System.Data;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal  static class MapCalidadGanadoDAL
    {
        /// <summary>
        ///     Método  que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static ResultadoInfo<CalidadGanadoInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<CalidadGanadoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select new CalidadGanadoInfo
                                {
                                    CalidadGanadoID = info.Field<int>("CalidadGanadoId"),
                                    Descripcion = info.Field<string>("Descripcion"),
                                    Sexo = Convert.ToChar(info.Field<string>("Sexo")) == 'M'
                                               ? Sexo.Macho
                                               : Sexo.Hembra, //Convert.ToChar(info.Field<string>("Sexo")),
                                    Activo = info.Field<bool>("Activo").BoolAEnum(),
                                    Calidad = info.Field<string>("Calidad").Trim()
                                }).ToList();

                var resultado =
                    new ResultadoInfo<CalidadGanadoInfo>
                    {
                        Lista = lista,
                        TotalRegistros =
                            Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
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
        ///     Método  que obtiene una lista de todos los registros
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static List<CalidadGanadoInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CalidadGanadoInfo> result =
                    (from info in dt.AsEnumerable()
                     select
                         new CalidadGanadoInfo
                         {
                             CalidadGanadoID = info.Field<int>("CalidadGanadoId"),
                             Calidad = info.Field<string>("Calidad"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Sexo = Convert.ToChar(info.Field<string>("Sexo")) == 'M'
                                               ? Sexo.Macho
                                               : Sexo.Hembra,
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///     Método  que obtiene un registro de CalidadGanado
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static CalidadGanadoInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                CalidadGanadoInfo result =
                    (from info in dt.AsEnumerable()
                     select
                         new CalidadGanadoInfo
                         {
                             CalidadGanadoID = info.Field<int>("CalidadGanadoId"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Sexo = Convert.ToChar(info.Field<string>("Sexo")) == 'M'
                                               ? Sexo.Macho
                                               : Sexo.Hembra,
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).First();

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///     Método  que obtiene una lista de todos los registros
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static List<EntradaGanadoCalidadInfo> ObtenerListaCalidadGanado(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<EntradaGanadoCalidadInfo> result =
                    (from info in dt.AsEnumerable()
                     select
                         new EntradaGanadoCalidadInfo
                             {
                                 CalidadGanado =
                                     new CalidadGanadoInfo
                                         {
                                             CalidadGanadoID = info.Field<int>("CalidadGanadoId"),
                                             Descripcion = info.Field<string>("Descripcion"),
                                             Sexo = info.Field<string>("Sexo").StringASexoEnum(),
                                             Calidad = info.Field<string>("Calidad")
                                         }
                             }).ToList();
                return result;
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
        internal  static CalidadGanadoInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CalidadGanadoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CalidadGanadoInfo
                             {
                                 CalidadGanadoID = info.Field<int>("CalidadGanadoID"),
                                 Descripcion = info.Field<string>("Descripcion"),
                                 Sexo = Convert.ToChar(info.Field<string>("Sexo")) == 'M'
                                            ? Sexo.Macho
                                            : Sexo.Hembra,
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
