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
    internal class PreguntaDAL : BaseDAL
    {
        PreguntaAccessor preguntaAccessor;

        protected override void inicializar()
        {
            preguntaAccessor = da.inicializarAccessor<PreguntaAccessor>();
        }

        protected override void destruir()
        {
            preguntaAccessor = null;
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
                ResultadoInfo<PreguntaInfo> result = new ResultadoInfo<PreguntaInfo>();
                var condicion = da.Tabla<PreguntaInfo>().Where(e=> e.Estatus == filtro.Estatus);
                if (filtro.PreguntaID > 0)
                {
                    condicion = condicion.Where(e=> e.PreguntaID == filtro.PreguntaID);
                }
                if (!string.IsNullOrEmpty(filtro.Descripcion))
                {
                    condicion = condicion.Where(e=> e.Descripcion.Contains(filtro.Descripcion));
                }
                if(filtro.TipoPregunta != null && filtro.TipoPregunta.TipoPreguntaID != 0)
                {
                    condicion = condicion.Where(e => e.TipoPreguntaID == filtro.TipoPregunta.TipoPreguntaID);
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
                                .OrderBy(e => e.Descripcion)
                                .Skip((inicio - 1) * limite)
                                .Take(limite);



                result.Lista = paginado.ToList();
                AgregarTipoPregunta(result.Lista);
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

        private void AgregarTipoPregunta(IEnumerable<PreguntaInfo> lista )
        {
            var tipoPreguntaDAL = new TipoPreguntaDAL();
            IQueryable<TipoPreguntaInfo> tiposPregunta = tipoPreguntaDAL.ObtenerTodos();
            foreach(PreguntaInfo pregunta in lista)
                {
                    TipoPreguntaInfo tipo =
                        tiposPregunta.FirstOrDefault(ti => ti.TipoPreguntaID == pregunta.TipoPreguntaID);
                    if(tipo == null)
                    {
                        continue;
                    }
                    pregunta.TipoPregunta = tipo;
                }
        }

        /// <summary>
        /// Obtiene una lista de Pregunta
        /// </summary>
        /// <returns></returns>
        public IQueryable<PreguntaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<PreguntaInfo>();
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
        /// Obtiene una lista de Pregunta filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<PreguntaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.Estatus == estatus);
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
        /// Obtiene una entidad de Pregunta por su Id
        /// </summary>
        /// <param name="preguntaId">Obtiene una entidad Pregunta por su Id</param>
        /// <returns></returns>
        public PreguntaInfo ObtenerPorID(int preguntaId)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.PreguntaID == preguntaId).FirstOrDefault();
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
        /// Obtiene una entidad de Pregunta por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad Pregunta por su descripcion</param>
        /// <returns></returns>
        public PreguntaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.Descripcion.ToLower() == descripcion.ToLower()).FirstOrDefault();
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
        /// Metodo para Guardar/Modificar una entidad Pregunta
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(PreguntaInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.PreguntaID > 0)
                {
                    id = da.Actualizar<PreguntaInfo>(info);
                }
                else
                {
                    id = da.Insertar<PreguntaInfo>(info);
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
