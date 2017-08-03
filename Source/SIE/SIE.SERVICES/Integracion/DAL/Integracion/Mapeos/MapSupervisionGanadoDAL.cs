using System;
using SIE.Services.Info.Info;
using System.Data;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using System.Reflection;
using SIE.Base.Exepciones;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapSupervisionGanadoDAL
    {
        internal static SupervisionGanadoInfo ObtenerSupervision(DataSet ds)
        {
            SupervisionGanadoInfo supervision = null;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                supervision = new SupervisionGanadoInfo();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    supervision.SupervisionGanadoID = Convert.ToInt32(dr["SupervisionGanadoID"]);
                    supervision.LoteID = Convert.ToInt32(dr["LoteID"]);
                    supervision.Arete = Convert.ToString(dr["Arete"]);
                    supervision.AreteMetalico = Convert.ToString(dr["AreteMetalico"]);
                    supervision.FechaDeteccion = Convert.ToDateTime(dr["FechaDeteccion"]);
                    supervision.ConceptoDeteccionID = Convert.ToInt32(dr["ConceptoDeteccionID"]);
                    supervision.Acuerdo = Convert.ToString(dr["Acuerdo"]);
                    supervision.Notificacion = Convert.ToInt32(dr["Notificacion"]);
                    supervision.Activo = Convert.ToInt32(dr["Activo"]);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return supervision;
        }
    }
}
