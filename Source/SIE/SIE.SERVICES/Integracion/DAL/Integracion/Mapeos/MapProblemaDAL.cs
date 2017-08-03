using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;


namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    /// <summary>
    /// Clase de map de datos para Problema DAL
    /// </summary>
    internal class MapProblemaDAL
    {
        /// <summary>
        /// Mapea los datos de la tabla Problema
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ProblemaInfo> ObtenerListaProblemasNecropsia(DataSet ds)
        {
            List<ProblemaInfo> resultado = null;

            Logger.Info();
            DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
            resultado =
                (dt.AsEnumerable().Select(info => new ProblemaInfo()
                {
                    //ProblemaId, Descripcion, TipoProblemaID, Activo, FechaCreacion, UsuarioCreacionID, FechaModificacion, UsuarioModificacionID 
                    ProblemaID = info.Field<int>("ProblemaId"),
                    Descripcion = info.Field<String>("Descripcion").Trim(),
                    TipoProblema = new TipoProblemaInfo{TipoProblemaId = info.Field<int>("TipoProblemaId")},
                    FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                    UsuarioCreacionID = info.Field<int>("UsuarioCreacionID"),
                    FechaModificacion = info.Field<DateTime>("FechaModificacion"),
                    UsuarioModificacionID = info.Field<int>("UsuarioModificacionID"),
                    Activo = info.Field<bool>("Activo").BoolAEnum()
                })).ToList();

            return resultado;
        }
        /// <summary>
        /// Obtiene los datos de los problemas de la deteccion
        /// </summary>
        /// <param name="ds">Dataset con los datos</param>
        /// <param name="tratamiento"></param>
        /// <returns></returns>
        internal static List<ProblemaInfo> ObtenerProblemasPorDeteccion(DataSet ds, TratamientoInfo tratamiento)
        {
            List<ProblemaInfo> problemas;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                var tratamientosDAL = new TratamientoDAL();
                problemas = (from info in dt.AsEnumerable()
                             select new ProblemaInfo
                             {
                                 ProblemaID = info.Field<int>("ProblemaID"),
                                 Descripcion = info.Field<string>("Descripcion"),
                                 isCheked = true,
                                 Tratamientos = tratamiento == null ? null :tratamientosDAL.ObtenerTratamientosPorProblemas(tratamiento, new List<int> { info.Field<int>("ProblemaID") }),
                                 TipoProblema = new TipoProblemaInfo{ TipoProblemaId = info.Field<int>("TipoProblemaId") },
                             }).ToList();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return problemas;
        }
internal static IList<ProblemaInfo> ObtenerListaProblemasSecundarios(DataSet ds)
        {
            IList<ProblemaInfo> resultado = null;

            Logger.Info();
            DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
            resultado =
                (dt.AsEnumerable().Select(info => new ProblemaInfo()
                {
                    //ProblemaId, Descripcion, TipoProblemaID, Activo, FechaCreacion, UsuarioCreacionID, FechaModificacion, UsuarioModificacionID 
                    ProblemaID = info.Field<int>("ProblemaId"),
                    Descripcion = info.Field<String>("Descripcion").Trim()
                })).ToList();

            return resultado;
        }
    }
}
