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
    internal class MapFechaDAL
    {
        /// <summary>
        /// Obtiene fecha actual
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static FechaInfo ObtenerFechaActual(DataSet ds)
        {
            FechaInfo fecha;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                fecha = (from info in dt.AsEnumerable()
                         select new FechaInfo
                         {
                             FechaActual = info.Field<DateTime>("FechaActual")
                         }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return fecha;
        }
    }
}
