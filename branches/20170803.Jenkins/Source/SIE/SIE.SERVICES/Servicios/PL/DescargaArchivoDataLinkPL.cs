using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class DescargaArchivoDataLinkPL
    {
        /// <summary>
        /// Metodo para validar si la ruta del parametro existe
        /// </summary>
        /// <param name="ruta">Ruta que se validara</param>
        public bool ValidarRutaArchivo(string ruta)
        {
            try
            {
                Logger.Info();
                var dataLink = new DescargarArchivoDataLinkBL();
                var result = dataLink.ValidarRuta(ruta);
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
        /// Obtiene los datos del archivo del dataLink
        /// </summary>
        /// <param name="validacion">Informacion para realizar la validacion data link</param>
        /// <returns></returns>
        public ResultadoValidacion ObtenerDatosArchivo(ValidacionDataLink validacion)
        {
            try
            {
                Logger.Info();
                var dataLink = new DescargarArchivoDataLinkBL();
                var result = dataLink.LeerArchivo(validacion);
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
        /// Cargar el archivo en la orden de reparto
        /// </summary>
        /// <param name="validacionDatalink">Informacion necesaria para la validacion del los datos del datalink</param>
        /// <param name="usuario">Informacion del usuario</param>
        /// <returns>Resultado de la operacion</returns>
        public ResultadoOperacion CargarArchivoDataLink(ValidacionDataLink validacionDatalink, UsuarioInfo usuario)
        {
            try
            {
                Logger.Info();
                var dataLink = new DescargarArchivoDataLinkBL();
                var result = dataLink.CargarArchivoDatalink(validacionDatalink, usuario);
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
        /// Valida que el archivo contenga la fecha correspondiente
        /// </summary>
        /// <param name="validacion">Informacion para realizar la validacion del los datos del datalink</param>
        /// <returns>Resultado de la validacion</returns>
        public ResultadoValidacion ValidarDatosArchivo(ValidacionDataLink validacion)
        {
            try
            {
                Logger.Info();
                var dataLink = new DescargarArchivoDataLinkBL();
                var result = dataLink.ValidarDatosArchivo(validacion);
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
