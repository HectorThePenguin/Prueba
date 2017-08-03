using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Integracion.DAL.ORM
{
    internal class ProblemaTratamientoDAL : BaseDAL
    {
        ProblemaTratamientoAccessor problemaTratamientoAccessor;

        protected override void inicializar()
        {
            problemaTratamientoAccessor = da.inicializarAccessor<ProblemaTratamientoAccessor>();
        }

        protected override void destruir()
        {
            problemaTratamientoAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de ProblemaTratamiento
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ProblemaTratamientoInfo> ObtenerPorPagina(PaginacionInfo pagina, ProblemaTratamientoInfo filtro)
        {
            try
            {
                Logger.Info();
                ResultadoInfo<ProblemaTratamientoInfo> result = new ResultadoInfo<ProblemaTratamientoInfo>();
                var condicion = da.Tabla<ProblemaTratamientoInfo>().Where(e=> e.Activo == filtro.Activo);
                if (filtro.ProblemaTratamientoID > 0)
                {
                    condicion = condicion.Where(e=> e.ProblemaTratamientoID == filtro.ProblemaTratamientoID);
                }
                if (filtro.Problema != null && filtro.Problema.ProblemaID > 0)
                {
                    condicion = condicion.Where(e => e.ProblemaID == filtro.Problema.ProblemaID);
                }

                int inicio = pagina.Inicio;
                int limite = pagina.Limite;
                if (inicio > 1)
                {
                    int limiteReal = (limite - inicio) + 1;
                    inicio = (limite / limiteReal);
                    limite = limiteReal;
                }

                IEnumerable<ProblemaTratamientoInfo> paginado;

                if (filtro.Tratamiento != null && filtro.Tratamiento.CodigoTratamiento > 0)
                {
                    List<TratamientoInfo> listaTratamientos = ObtenerTratamientos().ToList();
                    //List<TratamientoInfo> condicionTratamiento = new List<TratamientoInfo>();
                    var condicionNueva = condicion.ToList();
                    if (filtro.Organizacion != null && filtro.Organizacion.OrganizacionID != 0)
                    {
                        var condicionTratamiento = (from con in condicionNueva
                                                    join tra in listaTratamientos on con.TratamientoID equals tra.TratamientoID
                                                    where tra.CodigoTratamiento == filtro.Tratamiento.CodigoTratamiento && tra.Organizacion.OrganizacionID == filtro.Organizacion.OrganizacionID
                                                    select con).ToList();
                        result.TotalRegistros = condicionTratamiento.Count();
                        paginado = condicionTratamiento
                                    .OrderBy(e => e.ProblemaTratamientoID)
                                    .Skip((inicio - 1) * limite)
                                    .Take(limite);
                    }
                    else
                    {
                        var condicionTratamiento = (from con in condicionNueva
                                                    join tra in listaTratamientos on con.TratamientoID equals tra.TratamientoID
                                                    where tra.CodigoTratamiento == filtro.Tratamiento.CodigoTratamiento
                                                    select con).ToList();
                        result.TotalRegistros = condicionTratamiento.Count();
                        paginado = condicionTratamiento
                                    .OrderBy(e => e.ProblemaTratamientoID)
                                    .Skip((inicio - 1) * limite)
                                    .Take(limite);
                    }

                }
                else
                {
                    if (filtro.Organizacion != null && filtro.Organizacion.OrganizacionID != 0)
                    {
                        List<TratamientoInfo> listaTratamientos = ObtenerTratamientos().ToList();
                        var condicionNueva = condicion.ToList();

                        var condicionTratamiento = (from con in condicionNueva
                                                    join tra in listaTratamientos on con.TratamientoID equals tra.TratamientoID
                                                    where tra.Organizacion.OrganizacionID == filtro.Organizacion.OrganizacionID
                                                    select con).ToList();
                        result.TotalRegistros = condicionTratamiento.Count();
                        paginado = condicionTratamiento
                                    .OrderBy(e => e.ProblemaTratamientoID)
                                    .Skip((inicio - 1) * limite)
                                    .Take(limite);
                    }
                    else
                    {
                        result.TotalRegistros = condicion.Count();
                        paginado = condicion
                                   .OrderBy(e => e.ProblemaTratamientoID)
                                   .Skip((inicio - 1) * limite)
                                   .Take(limite);
                    }
                }

                result.Lista = paginado.ToList();
                CargarProblemas(result.Lista);
                CargarTratamientos(result.Lista);

               
                return result;
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

        /// <summary>
        /// Obtiene un Tratamiento Problema para ver si existe
        /// </summary>
        /// <param name="filtro">Objeto Info que contiene los filtros para buscar el Problema Tratamiento</param>
        /// <returns></returns>
        public ProblemaTratamientoInfo ObtenerProblemaTratamientoExiste(ProblemaTratamientoInfo filtro)
        {
            try
            {
                Logger.Info();
                //var problemaDAL = new ProblemaTratamientoDAL();
                //IQueryable<ProblemaTratamientoInfo> listaProblemas = problemaDAL.ObtenerTodos();
                ProblemaTratamientoInfo result = da.Tabla<ProblemaTratamientoInfo>().FirstOrDefault(
                    e => (
                        e.Activo == filtro.Activo
                        && e.ProblemaID == filtro.ProblemaID
                        && e.TratamientoID == filtro.Tratamiento.TratamientoID));
                //ProblemaTratamientoInfo result = listaProblemas.FirstOrDefault(
                //    e=> (
                //        e.Activo == filtro.Activo
                //        && e.ProblemaID == filtro.ProblemaID
                //        && e.Tratamiento.TratamientoID == 
                //        )
                //    )
                return result;
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

        private void CargarProblemas(IEnumerable<ProblemaTratamientoInfo> lista)
        {
            var problemaDAL = new ProblemaDAL();
            IQueryable<ProblemaInfo> listaProblemas = problemaDAL.ObtenerTodos();
            foreach (ProblemaTratamientoInfo problemaTratamiento in lista)
            {
                ProblemaInfo problema =
                    listaProblemas.FirstOrDefault(pro => pro.ProblemaID == problemaTratamiento.ProblemaID);
                if(problema == null)
                {
                    continue;
                }
                problemaTratamiento.Problema = problema;
            }
        }

        private void CargarTratamientos(IEnumerable<ProblemaTratamientoInfo> lista)
        {
            var tratamientoDAL = new TratamientoDAL();
            IList<TratamientoInfo> listaTratamientos = tratamientoDAL.ObtenerTodos();
            foreach (ProblemaTratamientoInfo problemaTratamiento in lista)
            {
                TratamientoInfo tratamiento =
                    listaTratamientos.FirstOrDefault(tra => tra.TratamientoID == problemaTratamiento.TratamientoID);
                if (tratamiento == null)
                {
                    continue;
                }
                problemaTratamiento.Tratamiento = tratamiento;
            }
        }

        private IList<TratamientoInfo> ObtenerTratamientos()
        {
            var tratamientoDAL = new TratamientoDAL();
            IList<TratamientoInfo> listaTratamientos = tratamientoDAL.ObtenerTodos();
            return listaTratamientos;
        }

        /// <summary>
        /// Obtiene una lista de ProblemaTratamiento
        /// </summary>
        /// <returns></returns>
        public IQueryable<ProblemaTratamientoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<ProblemaTratamientoInfo>();
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

        /// <summary>
        /// Obtiene una lista de ProblemaTratamiento filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<ProblemaTratamientoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.Activo == estatus);
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

        /// <summary>
        /// Obtiene una entidad de ProblemaTratamiento por su Id
        /// </summary>
        /// <param name="problemaTratamientoId">Obtiene una entidad ProblemaTratamiento por su Id</param>
        /// <returns></returns>
        public ProblemaTratamientoInfo ObtenerPorID(int problemaTratamientoId)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.ProblemaTratamientoID == problemaTratamientoId).FirstOrDefault();
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

        /// <summary>
        /// Metodo para Guardar/Modificar una entidad ProblemaTratamiento
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(ProblemaTratamientoInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.ProblemaTratamientoID > 0)
                {
                    id = da.Actualizar<ProblemaTratamientoInfo>(info);
                    problemaTratamientoAccessor.ActualizarFechaModificacion(info.ProblemaTratamientoID);
                }
                else
                {
                    id = da.Insertar<ProblemaTratamientoInfo>(info);
                }
                return id;
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

    }
}
