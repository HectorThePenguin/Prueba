using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class SalidaGanadoMuertoBL
    {
        /// <summary>
        /// Obtiene el folio para una orden de salida de ganado por muerte
        /// </summary>
        /// <param name="salidaGanadoMuertoInfo"></param>
        /// <returns></returns>
        internal int ObtenerFolio(SalidaGanadoMuertoInfo salidaGanadoMuertoInfo)
        {
            int folio = 0;
            try
            {
                Logger.Info();
                var salidaGanadoMuerteDal = new SalidaGanadoMuerteDAL();
                folio = salidaGanadoMuerteDal.ObtenerFolio(salidaGanadoMuertoInfo);
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return folio;
        }

        /// <summary>
        /// Crea el reporte de Salida de ganado por muerte
        /// </summary>
        /// <param name="impresionGandoMuerto"></param>
        /// <param name="listaGanadoMuerto"></param>
        internal void CrearOrdenSalidaGandoMuerto(ImpresionSalidaGanadoMuertoInfo impresionGandoMuerto, IList<SalidaGanadoMuertoInfo> listaGanadoMuerto)
        {
            //Asignamos el folio a las muertes
            var impresionBl = new ImpresionSalidaGanadoMuerteBL();
            impresionBl.CrearReporte(impresionGandoMuerto, listaGanadoMuerto);
        }

        /// <summary>
        /// Asigna el folio a las muertes del reporte
        /// </summary>
        /// <param name="listaGanadoMuerto"></param>
        /// <returns></returns>
        internal bool AsignarFolioMuertes(IList<SalidaGanadoMuertoInfo> listaGanadoMuerto)
        {
            bool result = false;
            try
            {
                using (var transaction = new TransactionScope())
                {
                    Logger.Info();
                    var salidaGanadoMuerteDal = new SalidaGanadoMuerteDAL();
                    salidaGanadoMuerteDal.AsignarFolioMuerte(listaGanadoMuerto);
                    result = true;
                    transaction.Complete();
                }
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return result;
        }
    }
}
