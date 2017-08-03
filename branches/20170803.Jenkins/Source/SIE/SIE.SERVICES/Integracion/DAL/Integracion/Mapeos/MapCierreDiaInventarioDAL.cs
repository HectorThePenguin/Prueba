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
    public class MapCierreDiaInventarioDAL
    {
        /// <summary>
        /// Map de cierre almacenCierrMovimiento
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static AlmacenCierreDiaInventarioInfo ObtenerCierreAlmacenMovimientoInfo(DataSet ds)
        {
            AlmacenCierreDiaInventarioInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new AlmacenCierreDiaInventarioInfo
                         {
                             AlmacenMovimientoID = info.Field<long>("AlmacenMovimientoID"),
                             Almacen = new AlmacenInfo(){AlmacenID = info.Field<int>("AlmacenID")},
                             TipoMovimiento = info.Field<int>("TipoMovimientoID"),
                             FolioAlmacen = info.Field<long>("FolioMovimiento"),
                             FechaMovimiento = info.Field<DateTime>("FechaMovimiento"),
                             Observaciones = info.Field<string>("Observaciones"),
                             Estatus = info.Field<int>("Status"),
                             FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                             UsuarioCreacionId = info.Field<int>("UsuarioCreacionID"),
                         }).First();
            }

            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }


        public static AlmacenCierreDiaInventarioInfo ObtenerCierreAlmacenMovimientoDiferencia(DataSet ds)
        {
            AlmacenCierreDiaInventarioInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new AlmacenCierreDiaInventarioInfo
                         {
                             ProductoID = info.Field<int>("ProductoID"),
                             CantidadReal = info.Field<decimal>("Cantidad"),
                             PrecioPromedio = info.Field<decimal>("PrecioPromedio"),
                             ImporteReal = info.Field<decimal>("Importe")
                         }).First();
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
