using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using iTextSharp.text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class GenerarArchivoDatalinkBL
    {
        const string comillas = "\"";
        const char espacio = ' ';
        const char coma = ',';
        const int numeroCaracteres = 6;
        const string letraP = "P";
        const string simboloPeso = "$";
        //const string saltoLinea = "\n";

        /// <summary>
        ///      Obtiene registros de RepartoDetalle
        /// </summary>
        /// <returns> </returns>
        public ResultadoOperacion GenerarArchivoDatalink(RepartoInfo repartoInfo, int tipoServicioID)
        {
            ResultadoOperacion resultadoOperacion;
            var parametroOrganizacionBL = new ParametroOrganizacionBL();
            var ruta = parametroOrganizacionBL.ObtenerPorOrganizacionIDClaveParametro(repartoInfo.OrganizacionID, ParametrosEnum.rutaGenerarArchivoDatalink.ToString());        
            var nombre = parametroOrganizacionBL.ObtenerPorOrganizacionIDClaveParametro(repartoInfo.OrganizacionID, ParametrosEnum.nombreGenerarArchivoDatalink.ToString());
            var archivo = String.Format("{0}{1}", ruta.Valor, nombre.Valor);
            try
            {
                Logger.Info();
                var generarArchivoDatalinkDAL = new GenerarArchivoDatalinkDAL();
                var listadoDetalle = generarArchivoDatalinkDAL.ObtenerDatosRepartoDetalle(repartoInfo, tipoServicioID);
                if (listadoDetalle != null)
                {
                    if (File.Exists(archivo))
                    {
                        File.Delete(archivo);
                    }

                    using (var sw = new StreamWriter(archivo))
                    {
                        var cabecero = string.Format("{0}{1}{0}", comillas, letraP);
                        sw.WriteLine(cabecero);

                        foreach (var generarArchivoDatalinkInfo in listadoDetalle)
                        {
                            var contenido = string.Format("{0}{1}{0}{2}{0}{3}{0}{2}{0}{4}{0}{2}{5}{2}{6}{2}{7}{2}{8}", comillas, generarArchivoDatalinkInfo.Servicio, coma, generarArchivoDatalinkInfo.Corral.Trim().PadLeft(numeroCaracteres, espacio), generarArchivoDatalinkInfo.Formula.ToString(CultureInfo.InvariantCulture).Trim().PadLeft(numeroCaracteres, espacio), generarArchivoDatalinkInfo.Kilos.ToString(CultureInfo.InvariantCulture).Trim().PadLeft(numeroCaracteres, espacio), generarArchivoDatalinkInfo.Cero.ToString(CultureInfo.InvariantCulture).Trim().PadLeft(numeroCaracteres, espacio), generarArchivoDatalinkInfo.Seccion, generarArchivoDatalinkInfo.Uno.ToString(CultureInfo.InvariantCulture).Trim().PadLeft(numeroCaracteres, espacio));
                            sw.WriteLine(contenido);
                        }

                        var pie = string.Format("{0}{1}{0}", comillas, simboloPeso);
                        sw.WriteLine(pie);
                    }

                    resultadoOperacion = new ResultadoOperacion()
                    {
                        Resultado = true
                    };
                }
                else
                {
                    if (File.Exists(archivo))
                    {
                        File.Delete(archivo);
                    }

                    using (var sw = new StreamWriter(archivo))
                    {
                        var cabecero = string.Format("{0}{1}{0}", comillas, letraP);
                        sw.WriteLine(cabecero);

                        var pie = string.Format("{0}{1}{0}", comillas, simboloPeso);
                        sw.WriteLine(pie);
                    }

                    resultadoOperacion = new ResultadoOperacion()
                    {
                        Resultado = true
                    };
                }
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                return new ResultadoOperacion()
                {
                    Resultado = false
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return new ResultadoOperacion()
                {
                    Resultado = false
                };
            }
            return resultadoOperacion;
        }

        /// <summary>
        /// Valida si existe la ruta del archivo datalink
        /// </summary>
        /// <param name="ruta"></param>
        /// <returns></returns>
        internal bool ValidarRuta(string ruta)
        {
            var resultado = false;
            try
            {
                Logger.Info();
                if (Directory.Exists(ruta))
                {
                    resultado = true;
                }
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
            return resultado;
        }
    }
}
