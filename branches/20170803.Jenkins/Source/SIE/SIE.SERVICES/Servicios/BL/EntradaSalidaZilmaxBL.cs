using System;
using System.Collections.Generic;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Base.Exepciones;
using System.Reflection;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    class EntradaSalidaZilmaxBL
    {
        /// <summary>
        ///  Obtiene una lista de los corrales entrantes a Zilmax
        /// </summary>
        /// <returns></returns>
        public IList<ReporteEntradaSalidaZilmaxInfo> ObtenerEntrantesZilmaxTodos(int organizacionID)
        {
            try
            {
                Logger.Info();
                var reporteEntradaSalidaZilmaxDAL = new ReporteEntradaSalidaZilmaxDAL();
                IList<ReporteEntradaSalidaZilmaxInfo> lista =
                    reporteEntradaSalidaZilmaxDAL.ObtenerDatosReporte(organizacionID);
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
        internal IList<ReporteEntradaSalidaZilmaxInfo> ObtenerEntrantesSalidaZilmaxSeleccionados(int organizacionId, DateTime fechaZilmax)
        {
            try
            {
                Logger.Info();
                var reporteEntradaSalidaZilmaxDAL = new ReporteEntradaSalidaZilmaxDAL();
                IList<ReporteEntradaSalidaZilmaxInfo> lista =
                    reporteEntradaSalidaZilmaxDAL.ObtenerDatosReporteSeleccionado(organizacionId, fechaZilmax);
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
