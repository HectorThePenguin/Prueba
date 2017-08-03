using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class BasculaMateriaPrimaPL
    {
        /// <summary>
        /// Metodo que guarda el primer pesaje (peso tara)
        /// </summary>
        /// <returns></returns>
        public PesajeMateriaPrimaInfo GuardarPrimerPesaje(PesajeMateriaPrimaInfo pesajeMateriaPrimaInfo)
        {
            PesajeMateriaPrimaInfo result;
            try
            {
                var basculaMateriaPrimaBl = new BasculaMateriaPrimaBL();
                result = basculaMateriaPrimaBl.GuardarPrimerPesaje(pesajeMateriaPrimaInfo);
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
            return result;
        }

        /// <summary>
        /// Metodo que guarda el segundo pesaje
        /// </summary>
        /// <param name="pesajeMateriaPrimaInfo"></param>
        /// <param name="pesoNeto"></param>
        /// <param name="pedidoInfo"></param>
        /// <returns></returns>
        public void GuardarSegundoPesaje(PesajeMateriaPrimaInfo pesajeMateriaPrimaInfo, decimal pesoNeto, PedidoInfo pedidoInfo)
        {
            try
            {
                var basculaMateriaPrimaBl = new BasculaMateriaPrimaBL();
                basculaMateriaPrimaBl.GuardarSegundoPesaje(pesajeMateriaPrimaInfo, pesoNeto, pedidoInfo);
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
