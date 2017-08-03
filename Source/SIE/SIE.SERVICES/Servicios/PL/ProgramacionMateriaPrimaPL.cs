using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ProgramacionMateriaPrimaPL
    {

        /// <summary>
        /// Almacena la programacion de la cantidad de productos en un pedido detalle.
        /// </summary>
        /// <param name="pedido"></param>
        /// <param name="listaProgramacion"></param>
        /// <returns></returns>
        public bool GuardarProgramacionMateriaPrima(PedidoInfo pedido, List<ProgramacionMateriaPrimaInfo> listaProgramacion)
        {
            bool resultado = false;
            try
            {
                var programacionMateriaBl = new ProgramacionMateriaPrimaBL();
                resultado = programacionMateriaBl.GuardarProgramacionMateriaPrima(pedido,listaProgramacion);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(),ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene el listado de lotes que tiene programados para el surtido
        /// </summary>
        /// <param name="pedidoDetalle"></param>
        /// <returns></returns>
        public List<ProgramacionMateriaPrimaInfo> ObtenerProgramacionMateriaPrima(PedidoDetalleInfo pedidoDetalle)
        {
            try
            {
                Logger.Info();
                var programacionMateriaPrimaBl = new ProgramacionMateriaPrimaBL();
                return programacionMateriaPrimaBl.ObtenerProgramacionMateriaPrima(pedidoDetalle);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo que actualiza la cantidad entregada
        /// </summary>
        /// <param name="programacionMateriaPrimaInfo"></param>
        public void ActualizarCantidadEntregada(ProgramacionMateriaPrimaInfo programacionMateriaPrimaInfo)
        {
            try
            {
                Logger.Info();
                var programacionMateriaPrimaBl = new ProgramacionMateriaPrimaBL();
                programacionMateriaPrimaBl.ActualizarCantidadEntregada(programacionMateriaPrimaInfo);
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
