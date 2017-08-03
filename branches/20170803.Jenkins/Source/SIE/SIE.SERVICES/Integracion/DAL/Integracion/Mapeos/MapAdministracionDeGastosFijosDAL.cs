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
    internal class MapAdministracionDeGastosFijosDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<AdministracionDeGastosFijosInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<AdministracionDeGastosFijosInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new AdministracionDeGastosFijosInfo
                         {
                             GastoFijoID = info.Field<int>("GastoFijoID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             Importe = info.Field<decimal>("Importe")
                             
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<AdministracionDeGastosFijosInfo>
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
        /// Obtiene una lista de gastos fijos de la embarcacion tarifa
        /// </summary>
        /// <returns></returns>
        internal static ResultadoInfo<AdministracionDeGastosFijosInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<AdministracionDeGastosFijosInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new AdministracionDeGastosFijosInfo
                         {
                             GastoFijoID = info.Field<int>("GastoFijoID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Importe = info.Field<decimal>("Importe")
                         }).ToList();

                var resultado =
                    new ResultadoInfo<AdministracionDeGastosFijosInfo>
                    {
                        Lista = lista,
                        TotalRegistros = 0
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
        /// Valida que la descripción del gasto fijo a registrar/editar no exista en la bd
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<AdministracionDeGastosFijosInfo> ValidarDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<AdministracionDeGastosFijosInfo> resultado =
                    (from info in dt.AsEnumerable()
                     select
                         new AdministracionDeGastosFijosInfo
                         {
                             GastoFijoID = info.Field<int>("GastoFijoID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Importe = info.Field<decimal>("Importe")
                         }).ToList();
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
