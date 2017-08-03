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
   internal static class MapPreguntaDAL
    {
        /// <summary>
        /// Obtiene preguntas por id formulario 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>

       internal static ResultadoInfo<PreguntaInfo> ObtenerPreguntasPorFormularioID(DataSet ds)
        {
            ResultadoInfo<PreguntaInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<PreguntaInfo> listaPreguntaInfo = (from pregunta in dt.AsEnumerable()
                                select new PreguntaInfo
                                {

                                    PreguntaID = pregunta.Field<int>("PreguntaID"),
                                    TipoPreguntaID = pregunta.Field<int>("TipoPreguntaID"),
                                    Descripcion = pregunta.Field<string>("Descripcion"),
                                    Valor = pregunta.Field<string>("Valor")
                                }).ToList();
                resultado = new ResultadoInfo<PreguntaInfo>
                {
                    Lista = listaPreguntaInfo,
                    //TotalRegistros = Convert.ToInt32(listaPreguntaInfo.Count())
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

       internal static int ObtenerSupervisionId(DataSet ds)
       {
           int resultado = 0;
           try
           {
               Logger.Info();
               DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
               foreach (DataRow dr in dt.Rows)
               {
                   resultado = Convert.ToInt32(dr["SupervisionDetectoresId"]);
               }

           }
           catch (Exception ex)
           {
               Logger.Error(ex);
               throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
           }

           return resultado;
       }

       internal static ResultadoInfo<SupervisionDetectoresInfo> ObtenerSupervisionDeteccion(DataSet ds)
       {
           
           ResultadoInfo<SupervisionDetectoresInfo> resultado;
           try
           {
               Logger.Info();
               DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

               List<SupervisionDetectoresInfo> listaSupervisiones = (from dato in dt.AsEnumerable()
                                                       select new SupervisionDetectoresInfo
                                                       {
                                                           SupervisionDetectoresId = dato.Field<int>("SupervisionDetectoresID"),
                                                           OrganizacionId = dato.Field<int>("OrganizacionID"),
                                                           OperadorId = dato.Field<int>("OperadorID"),
                                                           FechaSupervision = dato.Field<DateTime>("FechaSupervision"),
                                                           CriterioSupervisionId = dato.Field<int>("CriterioSupervisionID"),
                                                           Observaciones = dato.Field<string>("Observaciones").Trim(),
                                                           
                                                           FechaCreacion = dato.Field<DateTime>("FechaCreacion"),
                                                           UsuarioCreacionId = dato.Field<int>("UsuarioCreacionID"),
                                                           Activo = dato.Field<bool>("Activo").BoolAEnum()

                                                       }).ToList();
               resultado = new ResultadoInfo<SupervisionDetectoresInfo>
               {
                   Lista = listaSupervisiones,
                   TotalRegistros = Convert.ToInt32(listaSupervisiones.Count())
               };
           }
           catch (Exception ex)
           {
               Logger.Error(ex);
               throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
           }
           return resultado;

       }

       /// <summary>
       /// Mapeo de datos de Respuesta de supervision de detectores
       /// </summary>
       /// <param name="ds"></param>
       /// <returns></returns>
       internal static ResultadoInfo<SupervisionDetectoresRespuestaInfo> ObtenerRespuestasSupervisionDeteccion(DataSet ds)
       {
           ResultadoInfo<SupervisionDetectoresRespuestaInfo> resultado;
           try
           {
               Logger.Info();
               DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

               List<SupervisionDetectoresRespuestaInfo> listaRespuestas = 
                   
                   (from dato in dt.AsEnumerable()
                        select new SupervisionDetectoresRespuestaInfo
                        {
                            SupervisionDetectoresDetalleId = dato.Field<int>("SupervisionDetectoresDetalleId"),
                            SupervisionDetectoresId = dato.Field<int>("SupervisionDetectoresID"),
                            PreguntaId = dato.Field<int>("PreguntaId"),
                            Respuesta = dato.Field<int>("Respuesta"),
                            Activo = dato.Field<bool>("Activo").BoolAEnum(),
                            FechaCreacion = dato.Field<DateTime>("FechaCreacion"),
                            UsuarioCreacionId = dato.Field<int>("UsuarioCreacion"),
                            Pregunta = new PreguntaInfo()
                            {
                                PreguntaID = dato.Field<int>("PreguntaId"),
                                Descripcion = dato.Field<string>("Descripcion"),
                                Valor = dato.Field<string>("Valor")
                            }

                        }).ToList();
               resultado = new ResultadoInfo<SupervisionDetectoresRespuestaInfo>
               {
                   Lista = listaRespuestas,
                   TotalRegistros = Convert.ToInt32(listaRespuestas.Count())
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
