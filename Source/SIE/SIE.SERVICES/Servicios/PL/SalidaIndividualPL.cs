using System.IO;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using System;
using System.Reflection;
using SIE.Services.Facturas;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;
using SIE.Services.Info.Enums;

namespace SIE.Services.Servicios.PL
{
    public class SalidaIndividualPL
    {
        public int Guardar(string arete, int organizacion, string codigoCorral, int corraletaID, int usuarioCreacion, int tipoMovimiento, int operador)
        {
            try
            {
                Logger.Info();
                var salidaIndividual = new SalidaIndividualBL();
                int result = salidaIndividual.Guardar(arete, organizacion, codigoCorral, corraletaID, usuarioCreacion, tipoMovimiento, operador);
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
        /// Método para obtener el ticket
        /// </summary>
        /// <returns></returns>
        public int ObtenerTicket(TicketInfo ticket)
        {
            try
            {
                Logger.Info();
                var salidaBL = new SalidaIndividualBL();
                int result = salidaBL.ObtenerTicket(ticket);
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
        /// Metodo que le da salida por venta al ganado
        /// </summary>
        /// <param name="salidaIndividual"></param>
        /// <returns></returns>
        public MemoryStream GuardarSalidaIndividualGanadoVenta(SalidaIndividualInfo salidaIndividual)
        {
            try
            {
                Logger.Info();
                var salidaBl = new SalidaIndividualBL();
                MemoryStream result = new MemoryStream();

                if (salidaIndividual.TipoVenta == TipoVentaEnum.Propio)
                {
                    result = salidaBl.GuardarSalidaIndividualGanadoVenta(salidaIndividual);
                }
                else
                {
                    result = salidaBl.GuardarSalidaGanadoVentaIntensiva(salidaIndividual);
                }
                return result;
            }
            catch (ExcepcionServicio)
            {
                throw;
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
