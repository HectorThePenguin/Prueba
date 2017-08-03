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
    internal class MapContratoDetalleDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ContratoDetalleInfo> ObtenerPorContratoId(DataSet ds)
        {
            List<ContratoDetalleInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                lista = (from info in dt.AsEnumerable()
                         select new ContratoDetalleInfo
                         {
                             ContratoDetalleId = info.Field<int>("ContratoDetalleID"),
                             ContratoId = info.Field<int>("ContratoID"),
                             Indicador = new IndicadorInfo() {IndicadorId = info.Field<int>("IndicadorID"), Descripcion = info.Field<string>("Descripcion")},
                             PorcentajePermitido = info.Field<decimal>("PorcentajePermitido"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             PorcentajeHumedad = info.Field<decimal>("PorcentajeHumedad"),
                             FechaInicio = info.Field<string>("FechaInicio")
                         }).ToList();

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
