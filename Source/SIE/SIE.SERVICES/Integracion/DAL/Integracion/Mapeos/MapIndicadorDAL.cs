﻿using System;
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
    internal class MapIndicadorDAL
    {
        /// <summary>
        /// Obtiene un indicador por id
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IndicadorInfo ObtenerPorId(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                IndicadorInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new IndicadorInfo
                         {
                             IndicadorId = info.Field<int>("IndicadorID"),
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
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<IndicadorInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<IndicadorInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new IndicadorInfo
                         {
                             IndicadorId = info.Field<int>("IndicadorID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<IndicadorInfo>
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
        public static List<IndicadorInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<IndicadorInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new IndicadorInfo
                         {
                             IndicadorId = info.Field<int>("IndicadorID"),
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
        public static IndicadorInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                IndicadorInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new IndicadorInfo
                         {
                             IndicadorId = info.Field<int>("IndicadorID"),
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
    }
}
