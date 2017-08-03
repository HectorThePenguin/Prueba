using System;
using System.Reflection;
using System.Collections.Generic;
using SIE.Base.Log;
using SIE.Base.Exepciones;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class CalidadMezcladoFormulasAlimentoPL
    {
        /// <summary>
        /// Metodos para obtener los registros que se cargaran en el combobox "Analisis de Muestras"
        /// </summary>
        public IList<CalidadMezcladoFormulasAlimentoInfo> CargarComboboxAnalisis()
        {
            try
            {
                Logger.Info();
                var calMezForAliBL = new CalidadMezcladoFormulasAlimentoBL();
                IList<CalidadMezcladoFormulasAlimentoInfo> result = calMezForAliBL.CargarComboboxAnalisis();
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodos para obtener los registros que se cargaran en el combobox "tecnica"
        /// </summary>
        public IList<CalidadMezcladoFormulasAlimentoInfo> CargarComboboxTecnica()
        {
            try
            {
                Logger.Info();
                var calMezForAliBL = new CalidadMezcladoFormulasAlimentoBL();
                IList<CalidadMezcladoFormulasAlimentoInfo> result = calMezForAliBL.CargarComboboxTecnica();
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
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
        /// <returns></returns>
        public IList<CalidadMezcladoFactorInfo> ObtenerTablaFactor()
        {
            try
            {
                Logger.Info();
                var calidadMezcladoFormulasAlimentoBL = new CalidadMezcladoFormulasAlimentoBL();
                IList<CalidadMezcladoFactorInfo> result = calidadMezcladoFormulasAlimentoBL.ObtenerTablaFactor();
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
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
        /// <returns></returns>
        public IList<CalidadMezcladoFormula_ResumenInfo> TraerDatosTablaResumen(int organizacionID, int formulasMuestrear)
        {
            try
            {
                Logger.Info();
                var calidadMezcladoFormulasAlimentoBL = new CalidadMezcladoFormulasAlimentoBL();
                IList<CalidadMezcladoFormula_ResumenInfo> result =
                    calidadMezcladoFormulasAlimentoBL.TraerDatosTablaResumen(organizacionID, formulasMuestrear);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public void GuardarCalidadMezcladoFormulaAlimento(CalidadMezcladoFormulasAlimentoInfo resultado,
            IList<CalidadMezcladoFormulasAlimentoInfo> listaTotalRegistrosGuardar)
        {
            try
            {
                Logger.Info();
                var calidadMezcladoFormulasAlimentoBL = new CalidadMezcladoFormulasAlimentoBL();
                calidadMezcladoFormulasAlimentoBL.GuardarCalidadMezcladoFormulaAlimento(resultado,
                    listaTotalRegistrosGuardar);
            }
            catch (ExcepcionGenerica)
            {
                throw;
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
        public IList<CalidadMezcladoFormulasAlimentoInfo> CargarTablaMezcladoDetalle(
            CalidadMezcladoFormulasAlimentoInfo calidadMezcladoFormulaAlimentoInfo)
        {
            try
            {
                Logger.Info();
                var calidadMezcladoFormulasAlimentoBL = new CalidadMezcladoFormulasAlimentoBL();
                IList<CalidadMezcladoFormulasAlimentoInfo> result =
                    calidadMezcladoFormulasAlimentoBL.CargarTablaMezcladoDetalle(calidadMezcladoFormulaAlimentoInfo);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
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
        public IList<ImpresionCalidadMezcladoModel> ObtenerImpresionCalidadMezclado(FiltroImpresionCalidadMezclado filtro)
        {
            try
            {
                Logger.Info();
                var calidadMezcladoFormulasAlimentoBL = new CalidadMezcladoFormulasAlimentoBL();
                IList<ImpresionCalidadMezcladoModel> result =
                    calidadMezcladoFormulasAlimentoBL.ObtenerImpresionCalidadMezclado(filtro);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }


        }

        public void CalculosDetalle(List<ImpresionCalidadMezcladoModel> detalle)
        {
            try
            {
                Logger.Info();
                var calidadMezcladoFormulasAlimentoBL = new CalidadMezcladoFormulasAlimentoBL();
                calidadMezcladoFormulasAlimentoBL.CalculosDetalle(detalle);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
