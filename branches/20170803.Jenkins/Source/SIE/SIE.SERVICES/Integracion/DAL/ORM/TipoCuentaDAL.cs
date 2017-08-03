using System;
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
    internal class TipoCuentaDAL : BaseDAL
    {
        TipoCuentaAccessor tipoCuentaAccessor;

        protected override void inicializar()
        {
            tipoCuentaAccessor = da.inicializarAccessor<TipoCuentaAccessor>();
        }

        protected override void destruir()
        {
            tipoCuentaAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de TipoCuenta
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<TipoCuentaInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoCuentaInfo filtro)
        {
            try
            {
                Logger.Info();
                var result = new ResultadoInfo<TipoCuentaInfo>();
                var condicion = da.Tabla<TipoCuentaInfo>().Where(e => e.Activo == filtro.Activo);
                if (filtro.TipoCuentaID > 0)
                {
                    condicion = condicion.Where(e => e.TipoCuentaID == filtro.TipoCuentaID);
                }
                if (!string.IsNullOrEmpty(filtro.Descripcion))
                {
                    condicion = condicion.Where(e => e.Descripcion.Contains(filtro.Descripcion));
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
        /// Obtiene una lista de TipoCuenta
        /// </summary>
        /// <returns></returns>
        public IQueryable<TipoCuentaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<TipoCuentaInfo>();
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
        /// Obtiene una lista de TipoCuenta filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<TipoCuentaInfo> ObtenerTodos(EstatusEnum estatus)
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
        /// Obtiene una entidad de TipoCuenta por su Id
        /// </summary>
        /// <param name="tipoCuentaId">Obtiene una entidad TipoCuenta por su Id</param>
        /// <returns></returns>
        public TipoCuentaInfo ObtenerPorID(int tipoCuentaId)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.TipoCuentaID == tipoCuentaId).FirstOrDefault();
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
        /// Obtiene una entidad de TipoCuenta por su descripcion
        /// </summary>
        /// <param name="tipoCuentaId">Obtiene una entidad TipoCuenta por su descripcion</param>
        /// <returns></returns>
        public TipoCuentaInfo ObtenerPorDescripcion(string descripcion)
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
        /// Metodo para Guardar/Modificar una entidad TipoCuenta
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(TipoCuentaInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.TipoCuentaID > 0)
                {
                    id = da.Actualizar<TipoCuentaInfo>(info);
                    tipoCuentaAccessor.ActualizarFechaModificacion(info.TipoCuentaID);
                }
                else
                {
                    id = da.Insertar<TipoCuentaInfo>(info);
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
