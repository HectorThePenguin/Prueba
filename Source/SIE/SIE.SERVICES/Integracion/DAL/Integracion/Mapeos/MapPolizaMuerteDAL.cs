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
    class MapPolizaSalidaGanadoTransitoDAL
    {
        /// <summary>
        /// Mapea los datos faltantes de la salida de ganado en transito para la generacion de la poliza
        /// </summary>
        /// <param name="ds">Dataset de consulta de datos faltantes para la generacion de la poliza</param>
        /// <returns>Los datos faltantes para la generacion de la poliza de salida de ganado por muerte o venta</returns>
        internal static DatosPolizaSalidaGanadoTransitoInfo ObtenerDatosPoliza(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var lista =
                    (from info in dt.AsEnumerable()
                     select
                         new DatosPolizaSalidaGanadoTransitoInfo
                         {
                             PostFijoRef3 = info.Field<string>("PostFijoRef3"),
                             Sociedad = info.Field<string>("Sociedad"),
                             TipoPolizaDescripcion = info.Field<string>("Descripcion"),
                             ParametroOrganizacionValor = info.Field<string>("Valor"),
                             Division = info.Field<string>("Division")
                         }).FirstOrDefault();
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
