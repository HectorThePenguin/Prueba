using System;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    public class MapPremezclaDistribucionDetalleDAL
    {
        /// <summary>
        /// Obtiene la entidad de la Premezcla Distribucion Detalle
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static PremezclaDistribucionDetalleInfo ObtenerPremezclaDistribucionDetalle(DataSet ds)
        {
            PremezclaDistribucionDetalleInfo premezclaDistribucionDetalle;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                premezclaDistribucionDetalle = (from info in dt.AsEnumerable()
                             select new PremezclaDistribucionDetalleInfo
                             {
                                 PremezclaDistribucionDetalleId = info.Field<int>("PremezclaDistribucionDetalleID"),
                                 PremezclaDistribucionId = info.Field<int>("PremezclaDistribucionID"),
                                 OrganizacionId = info.Field<int>("OrganizacionID"),
                                 AlmacenMovimientoId = info.Field<long>("AlmacenMovimientoID"),
                                 CantidadASurtir = info.Field<long>("CantidadASurtir"),
                                 Activo = info.Field<bool>("Activo").BoolAEnum()
                             }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return premezclaDistribucionDetalle;
        }
    }
}
