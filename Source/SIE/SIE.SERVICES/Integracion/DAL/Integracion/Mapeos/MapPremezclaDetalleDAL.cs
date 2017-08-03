using System;
using System.Collections.Generic;
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
    internal class MapPremezclaDetalleDAL
    {
        /// <summary>
        /// Obtener detalle de premezcla
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<PremezclaDetalleInfo> ObtenerPremezclaDetallePorPremezclaId(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<PremezclaDetalleInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new PremezclaDetalleInfo
                         {
                             PremezclaDetalleID = info.Field<int>("PremezclaDetalleID"),
                             Premezcla = new PremezclaInfo() { PremezclaId = info.Field<int>("PremezclaID") },
                             Producto = new ProductoInfo() { ProductoId = info.Field<int>("ProductoID") },
                             Porcentaje = info.Field<decimal>("Porcentaje"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                             UsuarioCreacionId = info.Field<int>("UsuarioCreacionID"),
                             Guardado = true
                         }).ToList();
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
