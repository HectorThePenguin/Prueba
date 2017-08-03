using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class PreguntaBL 
    {
        Integracion.DAL.ORM.PreguntaDAL preguntaDALORM;

        public PreguntaBL()
        {
            preguntaDALORM = new Integracion.DAL.ORM.PreguntaDAL();
        }
        /// <summary>
        /// Obtiene preguntas por id formulario 
        /// </summary>
        /// <param name="tipoPregunta"></param>
        /// <returns></returns>
        internal ResultadoInfo<PreguntaInfo> ObtenerPorFormularioID(int tipoPregunta)
        {
            ResultadoInfo<PreguntaInfo> preguntaInfo;
            try
            {
                Logger.Info();
                var preguntaDAL = new PreguntaDAL();
                preguntaInfo = preguntaDAL.ObtenerPreguntasPorFormularioID(tipoPregunta);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return preguntaInfo;
        }

        /// <summary>
        /// Obtiene la lista de criterios para la supervision de deteccion
        /// </summary>
        /// <returns></returns>
        internal ResultadoInfo<CriterioSupervisionInfo> ObtenerCriteriosSupervision()
        {
            ResultadoInfo<CriterioSupervisionInfo> criterios;
            try
            {
                Logger.Info();
                var criterioDal = new CriterioSupervisionDAL();
                criterios = criterioDal.ObtenerCriteriosSupervision();
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return criterios;
        }

        internal int GuardarSupervisionDeteccionTecnica(SupervisionDetectoresInfo supervision)
        {
            int retValue = -1;
            try
            {
                Logger.Info();
                var dal = new PreguntaDAL();

                using (var transaccion = new TransactionScope())
                {
                    int supervisionId = 0;
                    supervisionId = dal.GuardarSupervisionDeteccionTecnica(supervision);

                    if (supervisionId > 0)
                    {
                        supervision.SupervisionDetectoresId = supervisionId;
                        foreach (var pregunta in supervision.Preguntas)
                        {
                            var respuesta = new SupervisionDetectoresRespuestaInfo
                            {
                                Activo = EstatusEnum.Activo,
                                PreguntaId = pregunta.PreguntaID,
                                Respuesta = pregunta.Activo ? int.Parse(pregunta.Valor) : 0,
                                SupervisionDetectoresDetalleId = supervisionId,
                                UsuarioCreacionId = supervision.UsuarioCreacionId
                            };

                            dal.GuardarRespuestasSupervisionDeteccionTecnica(respuesta);
                            
                        }
                        
                    }
                    else
                    {
                        throw new ExcepcionDesconocida("No se puede guardar la supervisión de técnica de deteccuón");
                    }

                    transaccion.Complete();
                    retValue = supervisionId;
                }
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return retValue;
        }

        /// <summary>
        /// Obtiene las supervisiones de deteccion a operadores
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="operadorId"></param>
        /// <returns></returns>
        internal ResultadoInfo<SupervisionDetectoresInfo> ObtenerSupervisionesAnteriores(int organizacionId, int operadorId)
        {
            ResultadoInfo<SupervisionDetectoresInfo> resultado;
            try
            {
                Logger.Info();
                var preguntaDAL = new PreguntaDAL();
                resultado = preguntaDAL.ObtenerSupervisionesAnteriores(organizacionId, operadorId);

                if(resultado != null)
                {
                    foreach (var supervision in resultado.Lista)
                    {
                        supervision.Respuestas = preguntaDAL.ObtenerRespuestasSupervisionDeteccion(supervision.SupervisionDetectoresId);

                    }
                }

            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        

        public void Dispose()
        {
            preguntaDALORM.Disposed += (s, e) =>
            {
                preguntaDALORM = null;
            };
            preguntaDALORM.Dispose();
        }

        /// <summary>
        /// Obtiene una lista paginada de Pregunta
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<PreguntaInfo> ObtenerPorPagina(PaginacionInfo pagina, PreguntaInfo filtro)
        {
            try
            {
                Logger.Info();
                return preguntaDALORM.ObtenerPorPagina(pagina, filtro);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de Pregunta
        /// </summary>
        /// <returns></returns>
        public IList<PreguntaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return preguntaDALORM.ObtenerTodos().ToList();
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de Pregunta filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IList<PreguntaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return preguntaDALORM.ObtenerTodos().Where(e => e.Activo).ToList();
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad de Pregunta por su Id
        /// </summary>
        /// <param name="preguntaId">Obtiene una entidad Pregunta por su Id</param>
        /// <returns></returns>
        public PreguntaInfo ObtenerPorID(int preguntaId)
        {
            try
            {
                Logger.Info();
                return preguntaDALORM.ObtenerTodos().FirstOrDefault(e => e.PreguntaID == preguntaId);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad de Pregunta por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad Pregunta por su descripcion</param>
        /// <returns></returns>
        public PreguntaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                return preguntaDALORM.ObtenerTodos().FirstOrDefault(e => e.Descripcion.ToLower() == descripcion.ToLower());
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad de Pregunta por su descripcion
        /// </summary>
        /// <param name="descripcion">Descripcion de la pregunta</param>
        /// <param name="tipoPreguntaID">Tipo de la pregunta</param>
        /// <returns></returns>
        public PreguntaInfo ObtenerPorTipoPreguntaDescripcion(int tipoPreguntaID, string descripcion)
        {
            try
            {
                Logger.Info();
                return preguntaDALORM.ObtenerTodos().FirstOrDefault(e => e.Descripcion.ToLower() == descripcion.ToLower() && e.TipoPreguntaID == tipoPreguntaID);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Pregunta
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(PreguntaInfo info)
        {
            try
            {
                Logger.Info();
                return preguntaDALORM.Guardar(info);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
