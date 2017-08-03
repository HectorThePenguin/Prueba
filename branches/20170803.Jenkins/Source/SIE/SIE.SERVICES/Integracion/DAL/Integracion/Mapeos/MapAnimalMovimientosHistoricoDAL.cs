using System;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapAnimalMovimientosHistoricoDAL
    {
        /// <summary>
        /// Establece el mapeo para el animal movimiento
        /// </summary>
        /// <returns></returns>
        internal static IMapBuilderContext<AnimalMovimientoInfo> ObtenerMapeoAnimalHistorico()
        {
            try
            {
                Logger.Info();
                IMapBuilderContext<AnimalMovimientoInfo> mapeo = MapBuilder<AnimalMovimientoInfo>.MapNoProperties();
                mapeo = mapeo.Map(x => x.AnimalID).ToColumn("AnimalID");
                mapeo = mapeo.Map(x => x.OrganizacionID).ToColumn("OrganizacionID");
				mapeo = mapeo.Map(x => x.Arete).ToColumn("Arete");
                return mapeo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
