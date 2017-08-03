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
using SIE.Services.Info.Enums;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
   public class MapConfiguracionAlertasDAL
    {
        /// <summary>
        /// Mapeo de la informacion devuelta de la consulta
        /// generada en el procedimiento almacenado
        /// ConfiguracionAlertasConsulta
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>regresa la la informacion que genero la consulta</returns>
       internal static ResultadoInfo<ConfiguracionAlertasGeneraInfo> ConsultarConfiguracionAlertas(DataSet ds)
        {
            List<ConfiguracionAlertasGeneraInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                lista = (from info in dt.AsEnumerable()
                         select new ConfiguracionAlertasGeneraInfo
                             {
                                 AlertaInfo = new AlertaInfo
                                 {
                                     Descripcion = info.Field<string>("Descripcion"),
                                     AlertaID = info.Field<int>("AlertaID"),
                                     ConfiguracionAlerta = new ConfiguracionAlertasInfo()
                                     { 
                                         AlertaConfiguracionID = info.Field<int>("AlertaConfiguracionID"), 
                                         Activo = info.Field<bool>("Estatus").BoolAEnum(),
                                         Datos = info.Field<string>("Datos"),
                                         Fuentes = info.Field<string>("Fuentes"),
                                         Condiciones = info.Field<string>("Condiciones"),
                                         Agrupador = info.Field<string>("Agrupador"),
                                         NivelAlerta = new NivelAlertaInfo()
                                         {
                                             NivelAlertaId = info.Field<int>("NivelAlertaID"),
                                             Descripcion = info.Field<string>("NivelDescripcion")
                                         }
                                     }
                                 }
                             }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
           
            var resultado =
                    new ResultadoInfo<ConfiguracionAlertasGeneraInfo>
                    {
                        Lista = lista,
                        TotalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
                    };
            return resultado;
        }

       /// <summary>
       /// Mapeo de la informacion devuelta de la consulta
       /// generada en el procedimiento almacenado
       /// ConfiguracionAlertasConsultaAccion
       /// </summary>
       /// <param name="ds"></param>
       /// <returns>la informacion regresada por el procedimiento</returns>
       internal static List<AlertaAccionInfo> ObtenerListaAcciones(DataSet ds)
       {
           try
           {
               Logger.Info();
               DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

               List<AlertaAccionInfo> lista = (from info in dt.AsEnumerable()
                                         select new AlertaAccionInfo()
                                         {
                                             AlertaAccionId = info.Field<int>("AlertaAccionID"),
                                             Descripcion = (info.Field<int>("AccionID") == 0) ? "" : info.Field<string>("Descripcion"),
                                             AccionId = (info.Field<int>("AccionID") == 0) ? 0 : info.Field<int>("AccionID"),
                                             AlertaId = info.Field<int>("AlertaID"),
                                             Nuevo = false
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
       /// Mapeo de la informacion devuelta de la consulta
       /// generada en el procedimiento almacenado
       /// Alertas_ObtenerTodas
       /// </summary>
       /// <param name="ds"></param>
       /// <returns>regresa la la informacion que genero la consulta</returns>
       internal static List<AlertaInfo> ObtenerTodasLasAlertasActivas(DataSet ds)
       {
           try
           {
               Logger.Info();
               DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

               List<AlertaInfo> lista = (from info in dt.AsEnumerable()
                            select new  AlertaInfo()
                            {
                                Descripcion = info.Field<string>("Descripcion"),
                                AlertaID = info.Field<int>("AlertaID")
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
       /// Mapeo de la informacion devuelta de la consulta
       /// generada en el procedimiento almacenado
       /// Acciones_ObtenerTodasLasActivas
       /// </summary>
       /// <param name="ds"></param>
       /// <returns>regresa la la informacion que genero la consulta</returns>
       internal static List<AccionInfo> ObtenerTodasLasAccionesActivas(DataSet ds)
       {
           try
           {
               Logger.Info();
               DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

               List<AccionInfo> lista = (from info in dt.AsEnumerable()
                                         select new AccionInfo()
                                         {
                                             Descripcion = info.Field<string>("Descripcion"),
                                             AccionID = info.Field<int>("AccionID")
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
        /// Mapeo de la informacion devuelta de la consulta
        /// generada en el procedimiento almacenado
        /// ConfiguracionAlerta_ObtenerAlertaPorID
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>regresa la la informacion que genero la consulta</returns>
        internal static AlertaInfo ObtenerAlertaPorId(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                var lista = (from info in dt.AsEnumerable()
                             select new AlertaInfo()
                             {
                                 Descripcion = info.Field<string>("Descripcion"),
                                 AlertaID = info.Field<int>("AlertaID"),
                                 Activo = EstatusEnum.Activo,
                                 ConfiguracionAlerta = new ConfiguracionAlertasInfo
                                 {
                                     Activo = EstatusEnum.Activo
                                 }
                             }).First();
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

        }

        /// <summary>
        /// Mapeo de la informacion devuelta de la consulta
        /// generada en el procedimiento almacenado
        /// ConfiguracionAlertas_ObtenerAlertasPorPaginas
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>regresa la la informacion que genero la consulta</returns>
        internal static ResultadoInfo<AlertaInfo> ObtenerAlertaPorPaginaCompleto(DataSet ds)
        {
            ResultadoInfo<AlertaInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<AlertaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new AlertaInfo()
                         {
                             Descripcion = info.Field<string>("Descripcion"),
                             AlertaID = info.Field<int>("AlertaID"),
                             Activo = EstatusEnum.Activo,
                             ConfiguracionAlerta = new ConfiguracionAlertasInfo
                             {
                                 Activo = EstatusEnum.Activo
                             }
                         }).ToList();

                resultado = new ResultadoInfo<AlertaInfo>
                {
                    Lista = lista,
                    TotalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
                };
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
