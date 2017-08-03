using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using BLToolkit.Data.Sql;
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
    internal class AdministracionDeGastosFijosDAL : DALBase
    {

        /// <summary>
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<AdministracionDeGastosFijosInfo> ObtenerPorPagina(PaginacionInfo pagina, AdministracionDeGastosFijosInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AUXAdministracionDeGastosFijosDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("AdministracionDeGastosFijos_ObtenerGastosFijosPorPagina", parameters);
                ResultadoInfo<AdministracionDeGastosFijosInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAdministracionDeGastosFijosDAL.ObtenerPorPagina(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (BLToolkit.Data.DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }


        /// <summary>
        /// Obtiene una lista de gastos fijos de la embarcacion tarifa
        /// </summary>
        /// <returns></returns>
        internal ResultadoInfo<AdministracionDeGastosFijosInfo> ObtenerTodos(TarifarioInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AUXAdministracionDeGastosFijosDAL.ObtenerParametrosPorID(filtro);
                DataSet ds = Retrieve("Tarifario_ObtenerEmbarqueGastoTarifa", parameters);
                ResultadoInfo<AdministracionDeGastosFijosInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAdministracionDeGastosFijosDAL.ObtenerTodos(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (BLToolkit.Data.DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Crea un nuevo gasto fijo
        /// </summary>
        /// <param name="gastos"></param>
        internal void CrearGastoFijo(AdministracionDeGastosFijosInfo gastos)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AUXAdministracionDeGastosFijosDAL.ObtenerParametrosCrear(gastos);
                Create("AdministracionDeGastosFijos_Crear", parameters);
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
        }

        /// <summary>
        /// Actualiza un gasto fijo existente
        /// </summary>
        /// <param name="gastos"></param>
        internal void ActualizarGastoFijo(AdministracionDeGastosFijosInfo gastos)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AUXAdministracionDeGastosFijosDAL.ObtenerParametrosActualizar(gastos);
                Update("AdministracionDeGastosFijos_Actualizar", parameters);
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
        }

        /// <summary>
        /// Valida que la descripción del gasto fijo a registrar/editar no exista en la bd
        /// </summary>
        /// <param name="gastos"></param>
        /// <returns></returns>
        internal List<AdministracionDeGastosFijosInfo> ValidarDescripcion(AdministracionDeGastosFijosInfo gastos)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AUXAdministracionDeGastosFijosDAL.ObtenerParametrosValidarDescripcion(gastos);
                DataSet ds = Retrieve("AdministracionDeGastosFijos_ObtenerPorDescripcion", parameters);
                List<AdministracionDeGastosFijosInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAdministracionDeGastosFijosDAL.ValidarDescripcion(ds);
                }
                return result;
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
        }
    }
}