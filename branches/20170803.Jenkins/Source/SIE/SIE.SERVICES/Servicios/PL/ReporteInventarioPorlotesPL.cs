
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
    public class ReporteInventarioPorlotesPL
    {
        /// <summary>
        /// Obtiene la info del reporte
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="familiaId"></param>
        /// <param name="tipoAlmacenId"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public IList<ReporteInventarioPorLotesInfo> ObtenerReporteInventarioPorlotes(int organizacionId, int familiaId, int tipoAlmacenId, DateTime fecha)
        {
            IList<ReporteInventarioPorLotesInfo> lista;
            try
            {
                Logger.Info();
                var vReporteInventarioPorlotesBL = new ReporteInventarioPorlotesBL();
                lista = vReporteInventarioPorlotesBL.ObtenerReporteInventarioPorlotes(organizacionId, familiaId, tipoAlmacenId, fecha);

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