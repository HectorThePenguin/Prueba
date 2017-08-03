using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapGenerarArchivoDatalinkDAL
    {
        /// <summary>
        ///     Método asigna el registro del animal movimiento obtenido
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<GenerarArchivoDatalinkInfo> ObtenerRepartoDetalle(DataSet ds)
        {
            List<GenerarArchivoDatalinkInfo> datosRepartoDetalle;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                datosRepartoDetalle = (from info in dt.AsEnumerable()
                                                               select new GenerarArchivoDatalinkInfo
                                                               {
                                                                   Servicio = info.Field<int>("Servicio"),
                                                                   Corral = info.Field<String>("Corral"),
                                                                   Formula = info.Field<String>("Formula"),
                                                                   Kilos = info.Field<int>("Kilos"),
                                                                   Cero = info.Field<int>("Cero"),
                                                                   Seccion = info.Field<int>("Seccion"),
                                                                   Uno = info.Field<int>("Uno")
                                                               }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return datosRepartoDetalle;
        }
    }
}
