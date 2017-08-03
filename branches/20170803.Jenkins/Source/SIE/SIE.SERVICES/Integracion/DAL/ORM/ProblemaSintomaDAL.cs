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

namespace SIE.Services.Integracion.DAL.ORM
{
    internal class ProblemaSintomaDAL : BaseDAL
    {
        ProblemaSintomaAccessor problemaSintomaAccessor;

        protected override void inicializar()
        {
            problemaSintomaAccessor = da.inicializarAccessor<ProblemaSintomaAccessor>();
        }

        protected override void destruir()
        {
            problemaSintomaAccessor = null;
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
                ResultadoInfo<ProblemaSintomaInfo> result = new ResultadoInfo<ProblemaSintomaInfo>();
                var condicion = da.Tabla<ProblemaSintomaInfo>().Where(e => e.Activo == filtro.Activo);
                if (filtro.ProblemaSintomaID > 0)
                {
                    condicion = condicion.Where(e => e.ProblemaSintomaID == filtro.ProblemaSintomaID);
                }
                if (filtro.Problema != null && filtro.Problema.ProblemaID > 0)
                {
                    condicion = condicion.Where(e => e.ProblemaID == filtro.Problema.ProblemaID);
                }

                if ((filtro.Problema != null && filtro.Problema.TipoProblema != null) && filtro.Problema.TipoProblema.TipoProblemaId > 0)
                {

                    condicion = (from con in condicion
                                join pro in da.Tabla<ProblemaInfo>() on con.ProblemaID equals pro.ProblemaID
                                where pro.TipoProblemaID == filtro.Problema.TipoProblema.TipoProblemaId
                                select con);
                }
                
                result.TotalRegistros = condicion.Count();

                int inicio = pagina.Inicio;
                int limite = pagina.Limite;
                if (inicio > 1)
                {
                    int limiteReal = (limite - inicio) + 1;
                    inicio = (limite / limiteReal);
                    limite = limiteReal;
                }
                var paginado = condicion
                                .OrderBy(e => e.ProblemaID)
                                .Skip((inicio - 1) * limite)
                                .Take(limite).ToList();



                CargarProblemas(paginado);
                CargarSintomas(paginado);

              

                result.Lista = paginado;

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

        /// <summary>
        /// Carga los objetos de Problema de la lista ProblemaSintoma
        /// </summary>
        /// <returns></returns>
        private void CargarProblemas(IEnumerable<ProblemaSintomaInfo> lista)
        {
            var problemaDAL = new ProblemaDAL();
            IList<ProblemaInfo> listaProblemas = problemaDAL.ObtenerTodosCompleto();

            foreach (var problemaSintoma in lista)
            {
                ProblemaInfo problema =
                    listaProblemas.FirstOrDefault(pro => pro.ProblemaID == problemaSintoma.ProblemaID);
                if (problema == null)
                {
                    continue;
                }
                problemaSintoma.Problema = problema;
            }
        }

        /// <summary>
        /// Carga los objetos de Sintoma de la lista ProblemaSintoma
        /// </summary>
        /// <returns></returns>
        private void CargarSintomas(IEnumerable<ProblemaSintomaInfo> lista)
        {
            var sintomaDAL = new SintomaDAL();
            IQueryable<SintomaInfo> listaSintomas = sintomaDAL.ObtenerTodos();

            foreach (var problemaSintoma in lista)
            {
                SintomaInfo sintoma =
                    listaSintomas.FirstOrDefault(pro => pro.SintomaID == problemaSintoma.SintomaID);
                if (sintoma == null)
                {
                    continue;
                }
                problemaSintoma.Sintoma = sintoma;
            }
            
        }

        /// <summary>
        /// Obtiene una lista de ProblemaSintoma
        /// </summary>
        /// <returns></returns>
        public IQueryable<ProblemaSintomaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<ProblemaSintomaInfo>();
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
        public List<ProblemaSintomaInfo> ObtenerListaProblemasSintomaTodos()
        {
            try
            {
                Logger.Info();
                var problemasSintomas = da.Tabla<ProblemaSintomaInfo>();
                var lista = problemasSintomas.ToList();
                CargarProblemas(lista);
                CargarSintomas(lista);

                return lista;
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
        public IQueryable<ProblemaSintomaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e => e.Activo == estatus);
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
                return this.ObtenerTodos().Where(e => e.ProblemaSintomaID == problemaSintomaId).FirstOrDefault();
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
        public int Guardar(ProblemaSintomaInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.ProblemaSintomaID > 0)
                {
                    id = da.Actualizar<ProblemaSintomaInfo>(info);
                    problemaSintomaAccessor.ActualizarFechaModificacion(info.ProblemaSintomaID);
                }
                else
                {
                    id = da.Insertar<ProblemaSintomaInfo>(info);
                }
                return id;
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
