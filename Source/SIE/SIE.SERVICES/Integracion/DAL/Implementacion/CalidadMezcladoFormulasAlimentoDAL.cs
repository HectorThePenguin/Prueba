using System;
using System.Data;
using System.Reflection;
using System.Data.SqlClient;
using System.Collections.Generic;
using SIE.Base.Log;
using SIE.Base.Infos;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class CalidadMezcladoFormulasAlimentoDAL : DALBase
    {
        /// <summary>
        /// Metodos para obtener los registros que se cargaran en el combobox "Analisis de Muestras"
        /// </summary>
        internal IList<CalidadMezcladoFormulasAlimentoInfo> CargarComboboxAnalisis()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("CalidadMezcladoFormulasAlimento_ObtenerCamposAnalisMuestra");
                IList<CalidadMezcladoFormulasAlimentoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCalidadMezcladoFormulasAlimentoDAL.CargarComboboxAnalisis(ds);
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
        /// Metodos para obtener los registros que se cargaran en el combobox "Tecnica"
        /// </summary>
        internal IList<CalidadMezcladoFormulasAlimentoInfo> CargarComboboxTecnica()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("CalidadMezcladoFormulasAlimento_ObtenerCamposTecnica");
                IList<CalidadMezcladoFormulasAlimentoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCalidadMezcladoFormulasAlimentoDAL.CargarComboboxTecnica(ds);
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
        /// Metodo para consultar los datos de la tabla CalidadMezcladoFactor
        /// </summary>
        internal IList<CalidadMezcladoFactorInfo> ObtenerTablaFactor()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("CalidadMezcladoFormulasAlimento_ObtenerDatosFormulas");
                IList<CalidadMezcladoFactorInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCalidadMezcladoFormulasAlimentoDAL.ObtenerTablaFactor(ds);
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
        /// Metodo para obtener los datos que se ocupan para llenar la tabla resumen del formulario "Calidad Mezclado Alimentos"
        /// </summary>
        internal IList<CalidadMezcladoFormula_ResumenInfo> TraerDatosTablaResumen(int organizacionID,
            int FormulasMuestrear)
        {
            try
            {
                Logger.Info();
                var parameters = AuxCalidadMezcladoFormulasAlimentoDAL.TraerDatosTablaResumen(organizacionID,
                    FormulasMuestrear);
                DataSet ds = Retrieve("CalidadMezcladoFormulasAlimento_resumen", parameters);
                IList<CalidadMezcladoFormula_ResumenInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCalidadMezcladoFormulasAlimentoDAL.TraerDatosTablaResumen(ds);
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

        public CalidadMezcladoFormulasAlimentoInfo GuardarCalidadMezcladoFormulaAlimentoReparto(
            CalidadMezcladoFormulasAlimentoInfo resultado)
        {
            try
            {
                Logger.Info();
                var parameters =
                    AuxCalidadMezcladoFormulasAlimentoDAL.ObtenerParametrosGuardarCalidadMezcladoFormulaAlimentoReparto(
                        resultado);
                DataSet ds = Retrieve("CalidadMezclado_Guardar", parameters);
                CalidadMezcladoFormulasAlimentoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapMezcladoraDAL.ObtenerCalidadMezcladoFormula(ds);
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

        public void GuardarCalidadMezcladoFormulaAlimentoRepartoDetalle(
            IList<CalidadMezcladoFormulasAlimentoInfo> listaTotalRegistrosGuardar,
            CalidadMezcladoFormulasAlimentoInfo result)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxCalidadMezcladoFormulasAlimentoDAL
                        .ObtenerParametrosGuardarCalidadMezcladoFormulaAlimentoRepartoDetalle(
                            listaTotalRegistrosGuardar, result);
                Update("CalidadMezcladoDetalle_Guardar", parameters);
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
        /// Metodo que se utiliza para traer de la tabla CalidadMezcladoDetalle, los registros que se ocupan para cargar en el grid "Analisis de muestras
        /// Inicial-Media-Final" cuando hay datos cargados en el mismo dia
        /// </summary>
        /// <returns></returns>
        internal IList<CalidadMezcladoFormulasAlimentoInfo> CargarTablaMezcladoDetalle(
            CalidadMezcladoFormulasAlimentoInfo calidadMezcladoFormulaAlimentoInfo)
        {
            try
            {
                Logger.Info();
                var parameters =
                    AuxCalidadMezcladoFormulasAlimentoDAL.CargarTablaMezcladoDetalle(calidadMezcladoFormulaAlimentoInfo);
                DataSet ds = Retrieve("CalidadMezcladoFormulasAlimento_CargarTablaMezcladoDetalle", parameters);
                IList<CalidadMezcladoFormulasAlimentoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCalidadMezcladoFormulasAlimentoDAL.CargarTablaMezcladoDetalle(ds);
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
        /// Metodo para consultar los datos de la impresion de Calidad de Mezclado
        /// </summary>
        /// <returns></returns>
        internal IList<ImpresionCalidadMezcladoModel> ObtenerImpresionCalidadMezclado(FiltroImpresionCalidadMezclado filtro)
        {
            try
            {
                Logger.Info();
                var parameters =
                    AuxCalidadMezcladoFormulasAlimentoDAL.ObtenerParametrosImpresionCalidadMezclado(filtro);
                DataSet ds = Retrieve("CalidadMezclado_ObtenerImpresion", parameters);
                IList<ImpresionCalidadMezcladoModel> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCalidadMezcladoFormulasAlimentoDAL.ObtenerImpresionCalidadMezclado(ds);
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
