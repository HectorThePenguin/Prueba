


//--*********** BL *************
using System;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class ReporteConsumoProgramadovsServidoBL
    {
        /// <summary>
        /// Obtiene los datos al reporte ReporteConsumoProgramadovsServido
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        internal IList<ReporteConsumoProgramadovsServidoInfo> ObtenerReporteConsumoProgramadovsServido(int organizacionID, DateTime fecha)
        {
            IList<ReporteConsumoProgramadovsServidoInfo> lista = null;
            try
            {
                Logger.Info();
                var reporteDal = new ReporteConsumoProgramadovsServidoDAL();
                
                lista =
                    reporteDal.ObtenerParametrosReporteConsumoProgramadovsServido(organizacionID, fecha);
               
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
