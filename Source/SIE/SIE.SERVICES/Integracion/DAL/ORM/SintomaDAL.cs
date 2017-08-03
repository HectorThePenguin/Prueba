using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BLToolkit.Data.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    internal class SintomaDAL : BaseDAL
    {
        SintomaAccessor sintomaAccessor;

        protected override void inicializar()
        {
            sintomaAccessor = da.inicializarAccessor<SintomaAccessor>();
        }

        protected override void destruir()
        {
            sintomaAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de Sintoma
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<SintomaInfo> ObtenerPorPagina(PaginacionInfo pagina, SintomaInfo filtro)
        {
            try
            {
                Logger.Info();
                var result = new ResultadoInfo<SintomaInfo>();
                var condicion = da.Tabla<SintomaInfo>().Where(e=> e.Activo == filtro.Activo);
                if (filtro.SintomaID > 0)
                {
                    condicion = condicion.Where(e=> e.SintomaID == filtro.SintomaID);
                }
                if (!string.IsNullOrEmpty(filtro.Descripcion))
                {
                    condicion = condicion.Where(e=> e.Descripcion.Contains(filtro.Descripcion));
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
        /// Obtiene una lista de Sintoma
        /// </summary>
        /// <returns></returns>
        public IQueryable<SintomaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<SintomaInfo>();
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
        /// Obtiene una lista de Sintoma filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<SintomaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return ObtenerTodos().Where(e=> e.Activo == estatus);
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
        /// Obtiene una entidad de Sintoma por su Id
        /// </summary>
        /// <param name="sintomaId">Obtiene una entidad Sintoma por su Id</param>
        /// <returns></returns>
        public SintomaInfo ObtenerPorID(int sintomaId)
        {
            try
            {
                Logger.Info();
                return ObtenerTodos().Where(e=> e.SintomaID == sintomaId).FirstOrDefault();
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
        /// Obtiene una entidad de Sintoma por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad Sintoma por su descripcion</param>
        /// <returns></returns>
        public SintomaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                return ObtenerTodos().Where(e=> e.Descripcion.ToLower() == descripcion.ToLower()).FirstOrDefault();
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
        /// Metodo para Guardar/Modificar una entidad Sintoma
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(SintomaInfo info)
        {
            try
            {
                Logger.Info();
                int id;
                if (info.SintomaID > 0)
                {
                    info.FechaModificacion = da.FechaServidor();
                    id = da.Actualizar<SintomaInfo>(info);
                    //sintomaAccessor.ActualizarFechaModificacion(info.SintomaID);
                }
                else
                {
                    id = da.Insertar<SintomaInfo>(info);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="problema"></param>
        /// <returns></returns>
        public IList<SintomaInfo> ObtenerPorProblema(int problema)
        {
            try
            {
                Logger.Info();
                return sintomaAccessor.ObtenerPorProblema(problema);
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
