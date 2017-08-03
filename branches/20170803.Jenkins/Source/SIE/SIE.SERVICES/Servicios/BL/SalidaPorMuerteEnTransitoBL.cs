using System;
using System.IO;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Polizas.Fabrica;

namespace SIE.Services.Servicios.BL
{
    public class SalidaPorMuerteEnTransitoBl
    {
        /// <summary>
        /// Registra una salida de ganado en transito, genera la poliza y factura correspondiente e imprime el archivo de poliza en formato PDF
        /// </summary>
        /// <param name="input">salida de ganado en transito que se registrara</param>
        /// <param name="registroExitoso">indica si se completo el registro de la salida de ganado en transito</param>
        /// <returns>Regresa los datos que conformaran el archivo impreso en PDF</returns>
        public MemoryStream GuardarSalida(SalidaGanadoEnTransitoInfo input,out bool registroExitoso)
        {
            try
            {
                var salida = new SalidaGanadoEnTransitoDAL();
                var ms=new MemoryStream();
                registroExitoso = false;
                using (var transaction = new TransactionScope()) //agrupa el proceso siguiente como una sola operacion
                {
                    salida.GuardarSalida(ref input);//registrar la salida de ganado en transito y actualiza el folio generado para la salida
                    salida.ActualizarEntradas(input);//actualiza las entradas de ganado en transito, y del lote correspondiente
                    if (input.Muerte)// si es salida de ganado en transito por muerte
                    {
                        var polizaSalida = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.SalidaMuerteEnTransito);
                        var polizaDal = new PolizaDAL();
                        var polizaSalidaMuerteTransito = polizaSalida.GeneraPoliza(input);//genera el XML de la poliza //genera el XML de la poliza de salida de  ganado por muerte
                        input.PolizaID = polizaDal.Crear(polizaSalidaMuerteTransito, TipoPoliza.SalidaMuerteEnTransito);//almacena la poliza generada en la base de datos
                        ms = polizaSalida.ImprimePoliza(input, polizaSalidaMuerteTransito);//genera el archivo en formato PDF de la poliza generada
                        registroExitoso = true;//indica que se completo la operacion exitosamente
                    }
                    else if (input.Venta)//en caso de ser una salida de ganado en transito por venta
                    {
                        var polizaSalida = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.SalidaVentaEnTransito);
                        var polizaDal = new PolizaDAL();
                        var poliza = polizaSalida.GeneraPoliza(input);//genera el XML de la poliza de salida de  ganado por venta
                        input.PolizaID = polizaDal.Crear(poliza, TipoPoliza.SalidaVentaEnTransito);//almacena la poliza generada en la base de datos
                        ms = polizaSalida.ImprimePoliza(input, poliza);//genera el archivo en formato PDF de la poliza generada
                       
                        var generarFactura = new FacturaBL();
                        generarFactura.SalidaGanadoTransito_ObtenerDatosFactura(input.Folio, input.OrganizacionID, true);//genera la factura de la salida de ganado en transito
                        registroExitoso = true;//indica que se completo la operacion exitosamente
                    }
                    salida.AsignarPolizaRegistrada(input);//asigna la poliza generada a la salida de ganado en transito registrada
                    transaction.Complete();//da por concluido el proceso de Salida de ganado en transito por venta o muerte
                    return ms;//regresa los datos del archivo PDF que se imprimiran
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                registroExitoso = false;
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

    }
}
