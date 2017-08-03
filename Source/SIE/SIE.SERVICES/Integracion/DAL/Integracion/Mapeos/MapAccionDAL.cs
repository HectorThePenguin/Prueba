using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Services.Info.Info;
using System.Data;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapAccionDAL
    {
        /// <summary>
        /// Método que mapea un resultado hacia una lista de acciones
        /// </summary>
        /// <param name="ds">DataSet que se mapeara</param>
        /// <returns>Regresa la lista de acciones recibidas en el DataSet</returns>
        internal static ResultadoInfo<AdministrarAccionInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<AdministrarAccionInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new AdministrarAccionInfo
                         {
                             AccionID = info.Field<int>("AccionID"),                             
                             Descripcion = info.Field<string>("Descripcion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum()
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<AdministrarAccionInfo>
                    {
                        Lista = lista,
                        TotalRegistros = totalRegistros
                    };
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Método que mapea un resultado hacia accion
        /// </summary>
        /// <param name="ds">DataSet que se mapeara</param>
        /// <returns>Regresa la accion recibida en el DataSet</returns>
        internal static AdministrarAccionInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                AdministrarAccionInfo EntidadAccion =
                    (from info in dt.AsEnumerable()
                     select
                         new AdministrarAccionInfo
                         {
                             AccionID = info.Field<int>("AccionID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum()
                         }).First();
                return EntidadAccion;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
