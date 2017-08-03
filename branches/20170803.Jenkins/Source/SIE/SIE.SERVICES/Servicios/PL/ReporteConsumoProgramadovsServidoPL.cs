//--*********** PL *************
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Info.Reportes;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ReporteConsumoProgramadovsServidoPL
    {
        /// <summary>
        /// Obtiene los datos al informe de ReporteConsumoProgramadovsServido
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public IList<ReporteConsumoProgramadovsServidoInfo> ObtenerReporteConsumoProgramadovsServido(int organizacionID, DateTime fecha)
        {
            IList<ReporteConsumoProgramadovsServidoInfo> lista;
            try
            {
                Logger.Info();
                var reporteBl = new ReporteConsumoProgramadovsServidoBL();
                lista = reporteBl.ObtenerReporteConsumoProgramadovsServido(organizacionID, fecha);

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