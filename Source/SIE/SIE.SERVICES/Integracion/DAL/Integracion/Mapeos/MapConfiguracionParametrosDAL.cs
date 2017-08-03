using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    /// <summary>
    /// Clase para mapear los datos de la configuracion de parametros
    /// </summary>
    internal  class MapConfiguracionParametrosDAL
    {
        /// <summary>
        /// Obtiene los datos de una configuracion proporcionada
        /// </summary>
        /// <param name="ds">Data set que contiene la configuracion</param>
        /// <returns>ConfiguracionParametrosInfo</returns>
        internal  static ConfiguracionParametrosInfo ObtenerConfiguracionParametros(DataSet ds)
        {
            ConfiguracionParametrosInfo resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select
                                 new ConfiguracionParametrosInfo
                                 {
                                     ParametroID = info.Field<int>("ParametroID"),
                                     Valor = info.Field<String>("Valor"),
                                     Clave = info.Field<String>("Clave").Trim(),
                                     Descripcion = info.Field<String>("Descripcion"),
                                     
                                 }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
        /// <summary>
        /// obtiene la lista de configuracion de parametros
        /// </summary>
        /// <param name="ds">Data set que contiene los datos</param>
        /// <returns>Lista de configuracion</returns>
        internal  static IList<ConfiguracionParametrosInfo> ObtenerListaConfiguracionParametros(DataSet ds)
        {
            IList<ConfiguracionParametrosInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select
                                 new ConfiguracionParametrosInfo
                                 {
                                     ParametroID = info.Field<int>("ParametroID"),
                                     Valor = info.Field<String>("Valor"),
                                     Clave = info.Field<String>("Clave").Trim(),
                                     Descripcion = info.Field<String>("Descripcion")

                                 }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

    }
}
