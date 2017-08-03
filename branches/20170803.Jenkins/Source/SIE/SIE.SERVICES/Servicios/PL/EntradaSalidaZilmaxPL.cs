using System;
using System.Collections.Generic;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class EntradaSalidaZilmaxPL
    {
        /// <summary>
        /// Obtiene una lista de los corrales entrantes a Zilmax
        /// </summary>
        /// <returns></returns>
        public IList<ReporteEntradaSalidaZilmaxInfo> ObtenerEntrantesSalidaZilmaxTodos(int organizacionID)
        {
            try
            {
                Logger.Info();
                var entradaSalidaZilmaxBL = new EntradaSalidaZilmaxBL();
                IList<ReporteEntradaSalidaZilmaxInfo> lista =
                    entradaSalidaZilmaxBL.ObtenerEntrantesZilmaxTodos(organizacionID);
                return lista;
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
        /// Obtiene las entradas/salidas zilmax
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="fechaZilmax"> </param>
        /// <returns></returns>
        public IList<ReporteEntradaSalidaZilmaxInfo> ObtenerEntrantesSalidaZilmaxSeleccionados(int organizacionId, DateTime fechaZilmax)
        {
            try
            {
                Logger.Info();
                var entradaSalidaZilmaxBL = new EntradaSalidaZilmaxBL();
                IList<ReporteEntradaSalidaZilmaxInfo> lista =
                    entradaSalidaZilmaxBL.ObtenerEntrantesSalidaZilmaxSeleccionados(organizacionId, fechaZilmax);
                return lista;
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
