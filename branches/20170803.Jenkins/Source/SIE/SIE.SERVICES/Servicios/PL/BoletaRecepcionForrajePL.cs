using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Properties;
using SIE.Services.Servicios.BL;
using SIE.Services.Info.Enums;

namespace SIE.Services.Servicios.PL
{
    public class BoletaRecepcionForrajePL
    {
        /// <summary>
        /// Metodos que obtiene el rango de humedad permito para el producto determinado
        /// </summary>
        /// <returns></returns>
        public RegistroVigilanciaInfo ObtenerRangos(RegistroVigilanciaInfo registroVigilanciaInfo, IndicadoresEnum indicador)
        {
            RegistroVigilanciaInfo result;
            try
            {
                var boletaRecepcionForrajeBl = new BoletaRecepcionForrajeBL();
                result = boletaRecepcionForrajeBl.ObtenerRangos(registroVigilanciaInfo, indicador);
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
        /// Método que agrega un registro a la tabla "EntradaProductoMuestra" 
        /// Se agrega un solo registro por tratarse de un folio que cuya CalidadOrigen= 1
        /// </summary>
        public void AgregarNuevoRegistro(EntradaProductoInfo entradaProducto)
        {
            try
            {
                var boletaRecepcionForrajeBl = new BoletaRecepcionForrajeBL();
                boletaRecepcionForrajeBl.AgregarNuevoRegistro(entradaProducto);
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
        /// Obtiene el mensaje de error por el rango.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string ObtenerMensajeRango()
        {
            try
            {
                var boletaRecepcionForrajeBl = new BoletaRecepcionForrajeBL();
                return boletaRecepcionForrajeBl.ObtenerMensajeRango();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
