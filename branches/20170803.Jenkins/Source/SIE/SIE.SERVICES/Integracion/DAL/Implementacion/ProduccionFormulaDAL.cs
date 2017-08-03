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
    internal class ProduccionFormulaDAL: DALBase
    {
        /// <summary>
        /// Guarda la produccion de una formula y retorna el objeto
        /// </summary>
        /// <param name="produccionFormula"></param>
        /// <returns></returns>
        internal ProduccionFormulaInfo GuardarProduccionFormula(ProduccionFormulaInfo produccionFormula)
        {
            try
            {
                Logger.Info();
                var parameters = AuxProduccionFormulaDAL.GuardarProduccionFormula(produccionFormula);
                var ds = Retrieve("ProduccionFormula_Crear", parameters);
                ProduccionFormulaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProduccionFormulaDAL.GuardarProduccionFormula(ds);
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

        /// <summary>
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ProduccionFormulaInfo> ObtenerPorPagina(PaginacionInfo pagina, ProduccionFormulaInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxProduccionFormulaDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("ProduccionFormula_ObtenerPorPagina", parameters);
                ResultadoInfo<ProduccionFormulaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProduccionFormulaDAL.ObtenerPorPagina(ds);
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

        /// <summary>
        /// Obtiene un registro de ProduccionFormula
        /// </summary>
        /// <param name="produccionFormulaID">Identificador de la ProduccionFormula</param>
        /// <returns></returns>
        public ProduccionFormulaInfo ObtenerPorID(int produccionFormulaID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProduccionFormulaDAL.ObtenerParametrosPorID(produccionFormulaID);
                DataSet ds = Retrieve("ProduccionFormula_ObtenerPorID", parameters);
                ProduccionFormulaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProduccionFormulaDAL.ObtenerPorID(ds);
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

        /// <summary>
        /// Obtiene un registro de ProduccionFormula
        /// </summary>
        /// <param name="produccionFormula">Identificador de la ProduccionFormula</param>
        /// <returns></returns>
        public ProduccionFormulaInfo ObtenerPorFolioMovimiento(ProduccionFormulaInfo produccionFormula)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProduccionFormulaDAL.ObtenerParametrosPorFolioFormula(produccionFormula);
                DataSet ds = Retrieve("ProduccionFormula_ObtenerPorFolioFormula", parameters);
                ProduccionFormulaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProduccionFormulaDAL.ObtenerPorFolioMovimiento(ds);
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

        /// <summary>
        /// Obtiene un registro de ProduccionFormula
        /// </summary>
        /// <param name="produccionFormulaID">Identificador de la ProduccionFormula</param>
        /// <returns></returns>
        public ProduccionFormulaInfo ObtenerPorIDCompleto(int produccionFormulaID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProduccionFormulaDAL.ObtenerParametrosPorIDCompleto(produccionFormulaID);
                DataSet ds = Retrieve("ProduccionFormula_ObtenerPorIDCompleto", parameters);
                ProduccionFormulaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapProduccionFormulaDAL.ObtenerPorIDCompleto(ds);
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

        /// <summary>
        /// Obtiene una lista de produccion de formula
        /// </summary>
        /// <param name="organizacionID"> </param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        internal List<ProduccionFormulaInfo> ObtenerProduccionFormulaConciliacion(int organizacionID, DateTime fechaInicio, DateTime fechaFinal)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProduccionFormulaDAL.ObtenerParametrosConciliacion(organizacionID, fechaInicio, fechaFinal);
                using (IDataReader reader = RetrieveReader("ProduccionFormula_ConciliacionObtenerPorFecha", parameters))
                {
                    List<ProduccionFormulaInfo> result = null;
                    if (ValidateDataReader(reader))
                    {
                        result = MapProduccionFormulaDAL.ObtenerPorConciliacion(reader);
                    }
                    return result;
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
        }


        /// <summary>
        /// Resumen de produccion de formula
        /// </summary>
        /// <param name="produccionFormulasLista"> </param>
        /// <returns></returns>
        internal List<ProduccionFormulaInfo> ObtenerProduccionFormulaResumen(List<ProduccionFormulaInfo> produccionFormulasLista )
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProduccionFormulaDAL.ObtenerParametrosResumen(produccionFormulasLista);
                using (IDataReader reader = RetrieveReader("ProduccionFormulaAutomaticas_Resumen", parameters))
                {
                    List<ProduccionFormulaInfo> result = null;
                    if (ValidateDataReader(reader))
                    {
                        result = MapProduccionFormulaDAL.ObtenerResumenDeProduccion(reader);
                    }
                    return result;
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
        }



    }
}
