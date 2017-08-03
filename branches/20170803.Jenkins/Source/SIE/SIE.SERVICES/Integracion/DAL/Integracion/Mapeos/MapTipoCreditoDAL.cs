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
    internal class MapTipoCreditoDAL
    {
        internal static ResultadoInfo<TipoCreditoInfo> TipoCredito_ObtenerTiposCreditoPorFiltro(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<TipoCreditoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoCreditoInfo
                         {
                             TipoCreditoID = info.Field<int>("TipoCreditoID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum()
                         }).ToList();

                var resultado = new ResultadoInfo<TipoCreditoInfo>
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

        internal static TipoCreditoInfo TipoCredito_ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                TipoCreditoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoCreditoInfo
                         {
                             TipoCreditoID = info.Field<int>("TipoCreditoID"),
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

        internal static List<TipoCreditoInfo> TipoCredito_ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<TipoCreditoInfo> resultado =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoCreditoInfo
                         {
                             TipoCreditoID = info.Field<int>("TipoCreditoID"),
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

        internal static ConfiguracionCreditoInfo TipoCredito_ObtenerValidarConfiguracion(DataSet ds)
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

