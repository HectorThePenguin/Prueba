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
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapConfiguracionAccionesDAL
    {
        internal static IList<ConfiguracionAccionesInfo> ObtenerDatosConfiguracionAcciones(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ConfiguracionAccionesInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ConfiguracionAccionesInfo
                         {
                             Id = info.Field<int>("AccionesSIAPId"),
                             Proceso = info["Proceso"] == DBNull.Value ? String.Empty : info.Field<string>("Proceso"),
                             Descripcion = info["Descripcion"] == DBNull.Value ? String.Empty : info.Field<string>("Descripcion"),
                             Codigo = info["Codigo"] == DBNull.Value ? String.Empty : info.Field<string>("Codigo"),
                             FechaEjecucion = info["FechaEjecucion"] == DBNull.Value ? new DateTime(1900,1,1,0,0,0) : info.Field<DateTime>("FechaEjecucion"),
                             FechaUltimaEjecucion = info["FechaUltimaEjecucion"] == DBNull.Value ? new DateTime(1900, 1, 1, 0, 0, 0) : info.Field<DateTime>("FechaUltimaEjecucion"),
                             Lunes = info["Proceso"] != DBNull.Value && info.Field<bool>("Lunes"),
                             Martes = info["Martes"] != DBNull.Value && info.Field<bool>("Martes"),
                             Miercoles = info["Miercoles"] != DBNull.Value && info.Field<bool>("Miercoles"),
                             Jueves = info["Jueves"] != DBNull.Value && info.Field<bool>("Jueves"),
                             Viernes = info["Viernes"] != DBNull.Value && info.Field<bool>("Viernes"),
                             Sabado = info["Sabado"] != DBNull.Value && info.Field<bool>("Sabado"),
                             Domingo = info["Domingo"] != DBNull.Value && info.Field<bool>("Domingo"),
                             Repetir = info["Repetir"] != DBNull.Value && info.Field<bool>("Repetir"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             FechaCreacion = info["FechaCreacion"] == DBNull.Value ? new DateTime(1900,1,1,0,0,0) : info.Field<DateTime>("FechaCreacion"),
                             UsuarioCreacionId = info["UsuarioCreacionId"] == DBNull.Value ? 0 : info.Field<int>("UsuarioCreacionId"),
                             FechaModificacion = info["FechaModificacion"] == DBNull.Value ? new DateTime(1900, 1, 1, 0, 0, 0) : info.Field<DateTime>("FechaModificacion"),
                             UsuarioModificacionId = info["UsuarioModificacionId"] == DBNull.Value ? 0 : info.Field<int>("UsuarioModificacionId"),
                             
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
        /// Obtiene los datos para un valor de confiuracion de accion
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ConfiguracionAccionesInfo ObtenerDatosConfiguracionAccion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ConfiguracionAccionesInfo valor =
                    (from info in dt.AsEnumerable()
                     select
                         new ConfiguracionAccionesInfo
                         {
                             Id = info.Field<int>("AccionesSIAPId"),
                             Proceso = info["Proceso"] == DBNull.Value ? String.Empty : info.Field<string>("Proceso"),
                             Descripcion = info["Descripcion"] == DBNull.Value ? String.Empty : info.Field<string>("Descripcion"),
                             Codigo = info["Codigo"] == DBNull.Value ? String.Empty : info.Field<string>("Codigo"),
                             FechaEjecucion = info["FechaEjecucion"] == DBNull.Value ? new DateTime(1900, 1, 1, 0, 0, 0) : info.Field<DateTime>("FechaEjecucion"),
                             FechaUltimaEjecucion = info["FechaUltimaEjecucion"] == DBNull.Value ? new DateTime(1900, 1, 1, 0, 0, 0) : info.Field<DateTime>("FechaUltimaEjecucion"),
                             Lunes = info["Proceso"] != DBNull.Value && info.Field<bool>("Lunes"),
                             Martes = info["Martes"] != DBNull.Value && info.Field<bool>("Martes"),
                             Miercoles = info["Miercoles"] != DBNull.Value && info.Field<bool>("Miercoles"),
                             Jueves = info["Jueves"] != DBNull.Value && info.Field<bool>("Jueves"),
                             Viernes = info["Viernes"] != DBNull.Value && info.Field<bool>("Viernes"),
                             Sabado = info["Sabado"] != DBNull.Value && info.Field<bool>("Sabado"),
                             Domingo = info["Domingo"] != DBNull.Value && info.Field<bool>("Domingo"),
                             Repetir = info["Repetir"] != DBNull.Value && info.Field<bool>("Repetir"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             FechaCreacion = info["FechaCreacion"] == DBNull.Value ? new DateTime(1900, 1, 1, 0, 0, 0) : info.Field<DateTime>("FechaCreacion"),
                             UsuarioCreacionId = info["UsuarioCreacionId"] == DBNull.Value ? 0 : info.Field<int>("UsuarioCreacionId"),
                             FechaModificacion = info["Proceso"] == DBNull.Value ? new DateTime(1900, 1, 1, 0, 0, 0) : info.Field<DateTime>("FechaModificacion"),
                             UsuarioModificacionId = info["UsuarioModificacionId"] == DBNull.Value ? 0 : info.Field<int>("UsuarioModificacionId"),

                         }).First();
                return valor;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
