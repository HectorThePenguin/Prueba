using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Info.Enums;
using SIE.Services.Properties;

namespace SIE.Services.Servicios.BL
{
    internal class BoletaRecepcionForrajeBL
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
                var boletaRecepcionForrajeDal = new BoletaRecepcionForrajeDAL();
                result = boletaRecepcionForrajeDal.ObtenerRangos(registroVigilanciaInfo, indicador);
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
                var boletaRecepcionForrajeDal = new BoletaRecepcionForrajeDAL();
                boletaRecepcionForrajeDal.AgregarNuevoRegistro(entradaProducto);
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
        /// Obtiene mensaje de error de rango.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string ObtenerMensajeRango()
        {
            try
            {
                return ResourceServices.BoletaRecepcion_ErrorRangos;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
