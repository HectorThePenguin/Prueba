using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapConfiguracionCreditoDAL
    {
        internal static ResultadoInfo<ConfiguracionCreditoInfo> ConfiguracionCredito_ObtenerConfiguracionCreditoPorFiltro(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ConfiguracionCreditoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ConfiguracionCreditoInfo
                         {
                             ConfiguracionCreditoID = info.Field<int>("ConfiguracionCreditoID"),
                             TipoCredito = new TipoCreditoInfo { Descripcion = info.Field<string>("Descripcion"), TipoCreditoID = info.Field<int>("TipoCreditoID") },
                             PlazoCredito = new PlazoCreditoInfo{ Descripcion = info.Field<string>("DescripcionPlazo"), PlazoCreditoID = info.Field<int>("PlazoCreditoID") },
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             Retenciones = new List<ConfiguracionCreditoRetencionesInfo>()
                         }).ToList();

                var resultado = new ResultadoInfo<ConfiguracionCreditoInfo>
                {
                    Lista = lista,
                    TotalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
                };
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static List<ConfiguracionCreditoRetencionesInfo> ConfiguracionCredito_ObtenerRetencionesPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ConfiguracionCreditoRetencionesInfo> resultado =
                    (from info in dt.AsEnumerable()
                     select
                         new ConfiguracionCreditoRetencionesInfo
                         {
                             NumeroMes = info.Field<int>("NumeroMes"),
                             PorcentajeRetencion = info.Field<int>("PorcentajeRetencion"),
                         }).ToList();

                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static int ConfiguracionCredito_ObtenerPorTipoCreditoYMes(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                var resultado = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
