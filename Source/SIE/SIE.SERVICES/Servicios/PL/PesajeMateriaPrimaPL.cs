using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using BLToolkit.Data.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;
using System.IO;

namespace SIE.Services.Servicios.PL
{
    public class PesajeMateriaPrimaPL
    {
        /// <summary>
        /// Metodo que obtiene un registro por ticket y pedido
        /// </summary>
        /// <param name="pesajeMateriaPrimaInfo"></param>
        /// <returns></returns>
        public PesajeMateriaPrimaInfo ObtenerPorTicketPedido(PesajeMateriaPrimaInfo pesajeMateriaPrimaInfo)
        {
            try
            {
                Logger.Info();
                var pesajeMateriaPrimaBl = new PesajeMateriaPrimaBL();
                PesajeMateriaPrimaInfo result = pesajeMateriaPrimaBl.ObtenerPorTicketPedido(pesajeMateriaPrimaInfo);
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
        /// Actualiza todos los campos del pesaje ( se consulta primero en base al Id y se sobre Escribe)
        /// </summary>
        /// <param name="pesajeMateriaPrimaInfo"></param>
        public void ActualizarPesajePorId(PesajeMateriaPrimaInfo pesajeMateriaPrimaInfo)
        {
            try
            {
                Logger.Info();
                var pesajeMateriaPrimaBl = new PesajeMateriaPrimaBL();
                pesajeMateriaPrimaBl.ActualizarPesajePorId(pesajeMateriaPrimaInfo);
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

        public List<PesajeMateriaPrimaInfo> ObtenerPesajesPorProgramacionID(int programacionMateriaPrimaId)
        {
            try
            {
                Logger.Info();
                var pesajeMateriaPrimaDal = new PesajeMateriaPrimaBL();
                return pesajeMateriaPrimaDal.ObtenerPesajesPorProgramacionMateriaPrimaId(programacionMateriaPrimaId);
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

        public List<PesajeMateriaPrimaInfo> ObtenerPesajesPorProgramacionIDSinActivo(int programacionMateriaPrimaId)
        {
            try
            {
                Logger.Info();
                var pesajeMateriaPrimaDal = new PesajeMateriaPrimaBL();
                return pesajeMateriaPrimaDal.ObtenerPesajesPorProgramacionMateriaPrimaIdSinActivo(programacionMateriaPrimaId);
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
