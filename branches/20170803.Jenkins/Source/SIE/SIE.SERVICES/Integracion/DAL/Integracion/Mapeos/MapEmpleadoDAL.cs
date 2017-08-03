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
    internal static class MapEmpleadoDAL
    {
        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<EmpleadoInfo> ObtenerPorPagina(DataSet ds)
        {
            ResultadoInfo<EmpleadoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<EmpleadoInfo> lista = (from info in dt.AsEnumerable()
                                           select new EmpleadoInfo
                                           {
                                               EmpleadoID = info.Field<int>("EmpleadoID"),
                                               Empleado = info.Field<string>("Empleado"),
                                               Nombre = info.Field<string>("Nombre"),
                                               Paterno = info.Field<string>("Paterno"),
                                               Materno = info.Field<string>("Materno")
                                           }).ToList();
                resultado = new ResultadoInfo<EmpleadoInfo>
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
        ///     Metodo que obtiene un empleado
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static EmpleadoInfo ObtenerPorID(DataSet ds)
        {
            EmpleadoInfo info;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                info = (from empleado in dt.AsEnumerable()
                               select new EmpleadoInfo
                               {
                                   EmpleadoID = empleado.Field<int>("EmpleadoID"),
                                   Empleado = empleado.Field<string>("Empleado"),
                                   Nombre = empleado.Field<string>("Nombre"),
                                   Paterno = empleado.Field<string>("Paterno"),
                                   Materno = empleado.Field<string>("Materno"),
                               }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return info;
        }
    }
}
