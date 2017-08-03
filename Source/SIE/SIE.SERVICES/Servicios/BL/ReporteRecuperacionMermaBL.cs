using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class ReporteRecuperacionMermaBL
    {
        /// <summary>
        /// Obtiene los datos para el Reporte Recuperacion de Merma
        /// </summary>
        /// <returns> </returns>
        internal List<ReporteRecuperacionMermaInfo> GenerarReporteRecuperacionMerma(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            List<ReporteRecuperacionMermaInfo> lista;
            try
            {
                Logger.Info();
                var reporteRecuperacionMermaDAL = new ReporteRecuperacionMermaDAL();
                lista = reporteRecuperacionMermaDAL.GenerarReporteRecuperacionMerma(organizacionID, fechaInicial, fechaFinal);
                if(lista == null)
                {
                    return null;
                }
                lista.ForEach(merma =>
                                  {
                                      decimal kilosOrigen = merma.PesoOrigen;
                                      decimal kilosLlegada = merma.PesoLlegada;
                                      decimal kilosCorte = merma.PesoCorte;
                                      if (kilosOrigen != 0)
                                      {
                                          merma.MermaTransito = Math.Round(((kilosOrigen-kilosLlegada)/kilosOrigen)*100, 2);
                                          merma.RecuperacionMerma = Math.Round(((kilosOrigen - kilosCorte) / kilosOrigen) * 100, 2);
                                      }
                                  });
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
