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
using SIE.Services.Integracion.DAL.ORM;

namespace SIE.Services.Servicios.BL
{
    public class ProblemaSintomaBL : IDisposable
    {
        private ProblemaSintomaDAL problemaSintomaDAL;

        public ProblemaSintomaBL()
        {
            problemaSintomaDAL = new ProblemaSintomaDAL();
        }

        public void Dispose()
        {
            problemaSintomaDAL.Disposed += (s, e) =>
                {
                    problemaSintomaDAL = null;
                };
            problemaSintomaDAL.Dispose();
        }

        /// <summary>
        /// Obtiene una lista paginada de ProblemaSintoma
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ProblemaSintomaInfo> ObtenerPorPagina(PaginacionInfo pagina, ProblemaSintomaInfo filtro)
        {
            try
            {
                Logger.Info();
                return problemaSintomaDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una lista de ProblemaSintoma
        /// </summary>
        /// <returns></returns>
        public IList<ProblemaSintomaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return problemaSintomaDAL.ObtenerTodos().ToList();
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
        /// Obtiene una lista de ProblemaSintoma
        /// </summary>
        /// <returns></returns>
        public IList<ProblemaSintomaInfo> ObtenerProblemasSintomaTodos()
        {
            try
            {
                Logger.Info();
                return problemaSintomaDAL.ObtenerListaProblemasSintomaTodos();
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
        /// Obtiene una lista de ProblemaSintoma filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IList<ProblemaSintomaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return problemaSintomaDAL.ObtenerTodos().Where(e => e.Activo == estatus).ToList();
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
        /// Obtiene una entidad de ProblemaSintoma por su Id
        /// </summary>
        /// <param name="problemaSintomaId">Obtiene una entidad ProblemaSintoma por su Id</param>
        /// <returns></returns>
        public ProblemaSintomaInfo ObtenerPorID(int problemaSintomaId)
        {
            try
            {
                Logger.Info();
                return
                    problemaSintomaDAL.ObtenerTodos().Where(e => e.ProblemaSintomaID == problemaSintomaId).
                        FirstOrDefault();
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
        /// Obtiene una entidad de ProblemaSintoma por su ProblemaID y su SintomaID
        /// </summary>
        /// <param name="filtro">parametro que contiene los filtros de busqueda del Problema Sintoma</param>
        /// <returns></returns>
        public ProblemaSintomaInfo ObtenerPorProblemaSintoma(ProblemaSintomaInfo filtro)
        {
            try
            {
                Logger.Info();
                return
                    problemaSintomaDAL.ObtenerTodos().FirstOrDefault(
                        pro => pro.ProblemaID == filtro.ProblemaID && pro.SintomaID == filtro.SintomaID);
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
        /// Metodo para Guardar/Modificar una entidad ProblemaSintoma
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public void Guardar(ProblemaSintomaInfo info)
        {
            try
            {
                Logger.Info();
                List<ProblemaSintomaInfo> listaGuardar = ArmarListaGuardar(info);

                using (TransactionScope transaction = new TransactionScope())
                {
                    foreach (var problemaSintomaInfo in listaGuardar)
                    {
                        problemaSintomaDAL.Guardar(problemaSintomaInfo);
                    }
                    transaction.Complete();
                }
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private List<ProblemaSintomaInfo> ArmarListaGuardar(ProblemaSintomaInfo info)
        {
           var listaGuardar = (from prob in info.ListaSintomas
                               where prob.ProblemaSintomaID == 0 || (prob.ProblemaSintomaID > 0 && prob.UsuarioModificacionID != null)
                            select new ProblemaSintomaInfo
                                {
                                    ProblemaSintomaID = prob.ProblemaSintomaID,
                                    ProblemaID = info.ProblemaID,
                                    SintomaID = prob.SintomaID,
                                    Activo = prob.Activo,
                                    UsuarioCreacionID = prob.UsuarioCreacionID,
                                    UsuarioModificacionID = prob.UsuarioModificacionID
                                }).ToList();
            return listaGuardar;
        }
    }
}
