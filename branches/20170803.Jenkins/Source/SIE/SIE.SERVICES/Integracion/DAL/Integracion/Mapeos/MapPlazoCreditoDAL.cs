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
    internal class MapPlazoCreditoDAL
    {
        internal static ResultadoInfo<PlazoCreditoInfo> PlazoCredito_ObtenerPlazosCreditoPorFiltro(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<PlazoCreditoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new PlazoCreditoInfo
                         {
                             PlazoCreditoID = info.Field<int>("PlazoCreditoID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum()
                         }).ToList();

                var resultado = new ResultadoInfo<PlazoCreditoInfo>
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

        internal static List<PlazoCreditoInfo> PlazoCredito_ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<PlazoCreditoInfo> resultado =
                    (from info in dt.AsEnumerable()
                     select
                         new PlazoCreditoInfo
                         {
                             PlazoCreditoID = info.Field<int>("PlazoCreditoID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum()
                         }).ToList();
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static PlazoCreditoInfo PlazoCredito_ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                PlazoCreditoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new PlazoCreditoInfo
                         {
                             PlazoCreditoID = info.Field<int>("PlazoCreditoID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).First();

                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static ConfiguracionCreditoInfo PlazoCredito_ObtenerValidarConfiguracion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ConfiguracionCreditoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ConfiguracionCreditoInfo
                         {
                             ConfiguracionCreditoID = info.Field<int>("ConfiguracionCreditoID"),
                         }).First();

                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
