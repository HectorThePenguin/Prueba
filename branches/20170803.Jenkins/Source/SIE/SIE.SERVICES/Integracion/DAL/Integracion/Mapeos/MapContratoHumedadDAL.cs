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
    internal class MapContratoHumedadDAL 
    {
        /// <summary>
        /// Obtiene un listado de humedades por contratoid
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ContratoHumedadInfo> ObtenerPorContratoId(DataSet ds)
        {
            List<ContratoHumedadInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new ContratoHumedadInfo()
                         {
                             ContratoHumedadID = info.Field<int>("ContratoHumedadID"),
                             ContratoID = info.Field<int>("ContratoID"),
                             FechaInicio = info.Field<DateTime>("FechaInicio"),
                             PorcentajeHumedad = info.Field<decimal>("PorcentajeHumedad"),
                             UsuarioCreacionId = info.Field<int>("UsuarioCreacionID"),
                             FechaCreacion = info.Field<DateTime>("FechaCreacion"),
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

        /// <summary>
        /// Obtiene la humedad a la fecha
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ContratoHumedadInfo ObtenerHumedadAlaFecha(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                ContratoHumedadInfo humedad = (from info in dt.AsEnumerable()
                    select new ContratoHumedadInfo()
                    {
                        ContratoHumedadID = info.Field<int>("ContratoHumedadID"),
                        ContratoID = info.Field<int>("ContratoID"),
                        FechaInicio = info.Field<DateTime>("FechaInicio"),
                        PorcentajeHumedad = info.Field<decimal>("PorcentajeHumedad"),
                        UsuarioCreacionId = info.Field<int>("UsuarioCreacionID"),
                        FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                        Guardado = true
                    }).First();

                return humedad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
