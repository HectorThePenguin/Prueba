using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;


namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapTarifarioDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<TarifarioInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<TarifarioInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new TarifarioInfo
                         {
                             EmbarqueID = info.Field<int>("embarqueTarifaId"),
                             NombreProveedor = info.Field<string>("proveedor"),
                             Organizaciones = info.Field<string>("origen") + " - " + info.Field<string>("destino"),
                             Ruta = info.Field<string>("ruta"),
                             Kilometros = info.Field<decimal>("Kilometros"),
                             Tarifa = info.Field<decimal>("tarifa"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<TarifarioInfo>
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
        ///     Metodo que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ConfiguracionEmbarqueInfo> ObtenerConfiguracionEmbarqueActivas(DataSet ds)
        {
            List<ConfiguracionEmbarqueInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new ConfiguracionEmbarqueInfo
                         {
                             ConfiguracionEmbarqueID = info.Field<int>("ConfiguracionEmbarqueID"),
                             OrganizacionOrigen = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionOrigenID")},
                             OrganizacionDestino = new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionDestinoID")},
                             Kilometros = info.Field<decimal>("Kilometros"),
                             Horas = info.Field<decimal>("Horas"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
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
