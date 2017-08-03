using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class SalidaGanadoMuertoPL
    {
        /// <summary>
        /// Obtiene el folio para el reporte
        /// </summary>
        /// <param name="salidaGanadoMuerteInfo"></param>
        /// <returns></returns>
        public int ObtenerFolio(SalidaGanadoMuertoInfo salidaGanadoMuerteInfo)
        {
            int folio = 0;
            try
            {
                Logger.Info();
                var salidaGanadoMuerteBl = new SalidaGanadoMuertoBL();
                folio = salidaGanadoMuerteBl.ObtenerFolio(salidaGanadoMuerteInfo);
            }
            catch (ExcepcionDesconocida exg)
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
        /// Crea el reporte de salida de ganado por muerte
        /// </summary>
        /// <param name="datosReporte"></param>
        /// <param name="listaSalidasGanadoMuertoInfo"></param>
        public void CrearOrdenSalidaGandoMuerto(ImpresionSalidaGanadoMuertoInfo datosReporte, IList<SalidaGanadoMuertoInfo> listaSalidasGanadoMuertoInfo)
        {
            try
            {
                Logger.Info();
                var salidaGandoMuertoBl = new SalidaGanadoMuertoBL();
                salidaGandoMuertoBl.CrearOrdenSalidaGandoMuerto(datosReporte, listaSalidasGanadoMuertoInfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Asigna el folio a las muertes relacionadas con el reporte
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="listaSalidasGanadoMuertoInfo"></param>
        /// <returns></returns>
        public bool AsignarFolioMuertes(IList<SalidaGanadoMuertoInfo> listaSalidasGanadoMuertoInfo)
        {
            bool result = false;
            try
            {
                Logger.Info();
                var salidaGandoMuertoBl = new SalidaGanadoMuertoBL();
                result = salidaGandoMuertoBl.AsignarFolioMuertes(listaSalidasGanadoMuertoInfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        public bool MostrarReportePantalla()
        {
            var impresionSalidaGanadoMuerto = new ImpresionSalidaGanadoMuerteBL();
            return impresionSalidaGanadoMuerto.MostrarPantallaImpresion();
        }
    }
}
