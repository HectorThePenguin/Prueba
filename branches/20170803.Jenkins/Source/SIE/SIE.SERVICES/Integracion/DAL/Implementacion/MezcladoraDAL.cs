using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using BLToolkit.Data.Sql;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using DataException = BLToolkit.Data.DataException;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class MezcladoraDAL : DALBase
    {
        /// <summary>
        /// ObtenerPorID
        /// </summary>
        /// <param name="mezcladoraInfo"></param>
        /// <returns></returns>
        internal MezcladoraInfo ObtenerPorID(MezcladoraInfo mezcladoraInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxMezcladoraDAL.ObtenerParametrosMezcladora(mezcladoraInfo);
                var ds = Retrieve("Mezcladora_ObtenerPorID", parameters);
                MezcladoraInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapMezcladoraDAL.ObtenerMezcladora(ds);
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
        /// ObtenerPorPagina
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal  ResultadoInfo<MezcladoraInfo> ObtenerPorPagina(PaginacionInfo pagina, MezcladoraInfo filtro)
        {
            ResultadoInfo<MezcladoraInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxMezcladoraDAL.ObtenerParametrosMezcladoraPagina(pagina, filtro);
                DataSet ds = Retrieve("Mezcladora_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapMezcladoraDAL.ObtenerPorPagina(ds);
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
        /// ObtenerCalidadMezcladoFactor
        /// </summary>
        /// <returns></returns>
        internal List<CalidadMezcladoFactorInfo> ObtenerCalidadMezcladoFactor()
        {
            List<CalidadMezcladoFactorInfo> result = null;
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Obtener_CalidadMezcladoFactor");
                if (ValidateDataSet(ds))
                {
                    result = MapMezcladoraDAL.ObtenerCalidadMezcladoFactor(ds);
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
        /// 
        /// </summary>
        /// <param name="listaCalidadMezcladoInfos"></param>
        internal void GuardarConsultaFactor(List<CalidadMezcladoFactorInfo> listaCalidadMezcladoInfos)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxMezcladoraDAL.ObtenerParametrosActualizarFactor(listaCalidadMezcladoInfos);
                Update("ConsultarFactores_Actualizar", parameters);
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
        /// ObtenerCalidadMezcladoFormulaAlimento
        /// </summary>
        /// <param name="calidadMezcladoFormulaAlimentoInfo"></param>
        /// <returns></returns>
        internal CalidadMezcladoFormulasAlimentoInfo ObtenerCalidadMezcladoFormulaAlimento(CalidadMezcladoFormulasAlimentoInfo calidadMezcladoFormulaAlimentoInfo)
        {
            CalidadMezcladoFormulasAlimentoInfo resultado = null;
            try
            {
                Dictionary<string, object> parameters = AuxMezcladoraDAL.ObtenerParametrosCalidadMezcladoFormulaAlimento(calidadMezcladoFormulaAlimentoInfo);
                DataSet ds = Retrieve("CalidadMezclado_ObtenerPorFormulaID", parameters);
                if (ValidateDataSet(ds))
                {
                    resultado = MapMezcladoraDAL.ObtenerCalidadMezcladoFormulaAlimento(ds);
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
            return resultado;
        }
    }
}
