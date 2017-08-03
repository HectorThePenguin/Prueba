using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Data.OleDb;
namespace SIE.Services.Integracion.DAL.Implementacion
{
    public class ConfiguracionFormulaDAL:DALBase
    {


		/// <summary>
        /// Obtener la configuracion de las formulas para una organizacion
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public IList<ConfiguracionFormulaInfo> ObtenerConfiguracionFormula(int organizacionID)
        {
            List<ConfiguracionFormulaInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionFormulaDAL.ObtenerParametroOrganizacion(organizacionID);
                DataSet ds = Retrieve("ConfiguracionFormula_ObtenerConfiguracionFormula", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapConfiguracionFormulaDAL.ObtenerConfiguracionFormula(ds);
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

        /// <summary>
        /// Metodo para inactivar la configuracion anterior
        /// </summary>
        /// <param name="configuracionImportar"></param>
        public void InactivarConfiguracionAnterior(ConfiguracionFormulaInfo configuracionImportar)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionFormulaDAL.ObtenerParametroInactivar(configuracionImportar);
                Update("ConfiguracionFormula_InactivarConfiguracionFormula", parameters);

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
        /// Metodo para guardar la configuracion de formulas
        /// </summary>
        /// <param name="configuracion"></param>
        /// <param name="configuracionImportar"></param>
        public void GuardarConfiguracionFormula(ConfiguracionFormulaInfo configuracion, ConfiguracionFormulaInfo configuracionImportar)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionFormulaDAL.ObtenerParametroGuardar(configuracion, configuracionImportar);
                var inserto = Create("ConfiguracionFormula_GuardarConfiguracionFormula", parameters);

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
        /// Obtener los tipos de ganado
        /// </summary>
        /// <param name="tipoGanadoIn"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal List<ConfiguracionFormulaInfo> ObtenerFormulaPorTipoGanado(TipoGanadoInfo tipoGanadoIn, int organizacionID)
        {
            try
            {
                Dictionary<string, object> parameters = AuxConfiguracionFormulaDAL.ObtenerParametrosFormulaPorTipoGanado(tipoGanadoIn, organizacionID);
                DataSet ds = Retrieve("[dbo].[ConfiguracionFormula_ObtenerPorTipoGanado]", parameters);
                List<ConfiguracionFormulaInfo> lista = null;
                if (ValidateDataSet(ds))
                {
                    lista = MapConfiguracionFormulaDAL.ObtenerFormulaPorTipoGanado(ds);
                }
                return lista;
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
