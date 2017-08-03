using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    public class EmpleadoDAL : DALBase
    {
        /// <summary>
        ///     Obtiene un lista paginada de empleados por el filtro especificado
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<EmpleadoInfo> ObtenerPorPagina(PaginacionInfo pagina, EmpleadoInfo filtro)
        {
            ResultadoInfo<EmpleadoInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxEmpleadoDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Empleado_ObtenerPorPagina", parameters, "ConnectionStringRH");
                if (ValidateDataSet(ds))
                {
                    lista = MapEmpleadoDAL.ObtenerPorPagina(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }

        /// <summary>
        ///     Obtiene un Empleado por Id
        /// </summary>
        /// <param name="empleadoID"></param>
        /// <returns></returns>
        internal EmpleadoInfo ObtenerPorID(int empleadoID)
        {
            EmpleadoInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEmpleadoDAL.ObtenerParametroPorID(empleadoID);
                DataSet ds = Retrieve("Empleado_ObtenerPorID", parameters, "ConnectionStringRH");
                if (ValidateDataSet(ds))
                {
                    result = MapEmpleadoDAL.ObtenerPorID(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }
    }
}
