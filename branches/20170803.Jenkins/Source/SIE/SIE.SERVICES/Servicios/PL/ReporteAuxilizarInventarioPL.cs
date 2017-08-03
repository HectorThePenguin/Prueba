using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Servicios.BL;
using System.Collections.Generic;
using SIE.Services.Info.Enums;

namespace SIE.Services.Servicios.PL
{
    public class ReporteAuxilizarInventarioPL
    {
        /// <summary>
        /// Obtiene los datos para el Reporte Auxiliar de Inventario
        /// </summary>
        /// <returns> </returns>
        public CorralReporteAuxiliarInventarioInfo ObtenerDatosCorral(string codigoCorral, int organizacionID)
        {
            CorralReporteAuxiliarInventarioInfo datosCorral;
            try
            {
                Logger.Info();
                var reporteAuxiliarInventarioBL = new ReporteAuxiliarInventarioBL();
                datosCorral = reporteAuxiliarInventarioBL.ObtenerDatosCorral(codigoCorral, organizacionID);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return datosCorral;
        }

        /// <summary>
        /// Obtiene los datos del reporte
        /// </summary>
        /// <param name="loteID"></param>
        /// <param name="grupoCorral"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public List<AuxiliarDeInventarioInfo> ObtenerDatosReporteAuxiliarInventario(int loteID, GrupoCorralEnum grupoCorral, int organizacionId)
        {
            List<AuxiliarDeInventarioInfo> lista;
            try
            {
                Logger.Info();
                var reporteAuxiliarInventarioBL = new ReporteAuxiliarInventarioBL();
                lista = reporteAuxiliarInventarioBL.ObtenerDatosReporteAuxiliarInventario(loteID, grupoCorral, organizacionId);
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
            return lista;
        }

    }
}
