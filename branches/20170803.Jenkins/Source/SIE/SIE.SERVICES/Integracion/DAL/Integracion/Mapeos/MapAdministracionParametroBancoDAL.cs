using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapAdministracionParametroBancoDAL
    {
        /// <summary>
        /// Obtiene los datos de la consulta para obtener los parametros de cheque por pagina
        /// </summary>
        /// <param name="ds">Data set con el resultado de la consilta</param>
        /// <returns>Diccionario con parametros</returns>
        internal static ResultadoInfo<CatParametroBancoInfo> ObtenerParametroBancoPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CatParametroBancoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CatParametroBancoInfo
                         {
                             ParametroID = info.Field<int>("CatParametroBancoID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Clave = info.Field<string>("Clave"),
                             TipoParametroID = (TipoParametroBancoEnum)info.Field<int>("TipoParametro"),
                             Valor = info.Field<string>("Valor"),
                             Activo = info.Field<bool>("Activo").BoolAEnum()
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<CatParametroBancoInfo>
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
        /// Obtiene los parametros del cheque en una lista
        /// </summary>
        /// <param name="ds">Data set con los resultados de la consulta</param>
        /// <returns>Lista de resultados</returns>
        internal static List<CatParametroBancoInfo> ObtenerParametroBancoLista(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CatParametroBancoInfo> lista =
                (from info in dt.AsEnumerable()
                    select
                        new CatParametroBancoInfo
                        {
                            ParametroID = info.Field<int>("ParametroID"),
                            Descripcion = info.Field<string>("Descripcion")
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
        /// Obtiene un parametro de cheque por descripción
        /// </summary>
        /// <param name="ds">Data ser con los resultados de la consulta</param>
        /// <returns>Objeto con el parametro chque</returns>
        internal static CatParametroBancoInfo ObtenerParametroBancoPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                CatParametroBancoInfo resultado =
                    (from info in dt.AsEnumerable()
                        select
                            new CatParametroBancoInfo
                            {
                                ParametroID = info.Field<int>("CatParametroBancoID"),
                                Descripcion = info.Field<string>("Descripcion"),
                                Clave = info.Field<string>("Clave"),
                                TipoParametroID = (TipoParametroBancoEnum) info.Field<int>("TipoParametro"),
                                Valor = info.Field<string>("Valor"),
                                Activo = info.Field<bool>("Activo").BoolAEnum()
                            }).First();
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
