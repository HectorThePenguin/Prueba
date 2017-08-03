using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class AbastoMateriaPrimaPL
    {
        /// <summary>
        /// Actualiza el pesaje de materia prima
        /// </summary>
        /// <param name="pesajeMateriaPrima"></param>
        /// <param name="programacionMateriaPrima"></param>
        /// <param name="pedido"></param>
        public void GuardarAbastoDeMateriaPrima(PesajeMateriaPrimaInfo pesajeMateriaPrima, ProgramacionMateriaPrimaInfo programacionMateriaPrima, int pedido)
        {
            try
            {
                Logger.Info();
                var abastoMateriaPrima = new AbastoMateriaPrimaBL();
                abastoMateriaPrima.GuardarAbastoDeMateriaPrima(pesajeMateriaPrima, programacionMateriaPrima, pedido);
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
        /// Actualiza el pesaje de materia prima
        /// </summary>
        /// <param name="pesajeMateriaPrima"></param>
        /// <param name="programacionMateriaPrima"></param>
        public void ActualizarAbastoDeMateriaPrima(PesajeMateriaPrimaInfo pesajeMateriaPrima, ProgramacionMateriaPrimaInfo programacionMateriaPrima)
        {
            try
            {
                Logger.Info();
                var abastoMateriaPrima = new AbastoMateriaPrimaBL();
                abastoMateriaPrima.ActualizarAbastoDeMateriaPrima(pesajeMateriaPrima, programacionMateriaPrima);
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
