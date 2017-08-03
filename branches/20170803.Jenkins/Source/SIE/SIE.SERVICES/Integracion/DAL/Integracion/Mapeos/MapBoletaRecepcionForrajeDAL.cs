using System;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapBoletaRecepcionForrajeDAL 
    {
        internal static RegistroVigilanciaInfo ObtenerRangos(DataSet ds)
        {
            try
            {
            Logger.Info();
            DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
            RegistroVigilanciaInfo entidad =
                (from info in dt.AsEnumerable()
                    select
                        new RegistroVigilanciaInfo
                        {
                            porcentajePromedioMin = info.Field<decimal>("RangoMinimo"),
                            porcentajePromedioMax = info.Field<decimal>("RangoMaximo")
                        }).First();
            return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
