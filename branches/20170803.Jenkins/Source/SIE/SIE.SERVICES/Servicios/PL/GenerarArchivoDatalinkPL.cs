using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class GenerarArchivoDatalinkPL
    {
        /// <summary>
        ///      Obtiene un Almacen por su Id
        /// </summary>
        /// <returns> </returns>
        public ResultadoOperacion GenerarArchivoDatalink(RepartoInfo repartoInfo, int tipoServicioID)
        {
            try
            {
                Logger.Info();
                var generarArchivoDatalinkBL = new GenerarArchivoDatalinkBL();
                return generarArchivoDatalinkBL.GenerarArchivoDatalink(repartoInfo, tipoServicioID);
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
        /// Metodo para validar si la ruta del parametro existe
        /// </summary>
        /// <param name="ruta">Ruta que se validara</param>
        public bool ValidarRutaArchivo(string ruta)
        {
            try
            {
                Logger.Info();
                var dataLink = new GenerarArchivoDatalinkBL();
                bool result = dataLink.ValidarRuta(ruta);
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
    }
}
