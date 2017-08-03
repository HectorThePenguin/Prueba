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
    internal class ClaseCostoProductoDAL : BaseDAL
    {
        ClaseCostoProductoAccessor claseCostoProductoAccessor;

        protected override void inicializar()
        {
            claseCostoProductoAccessor = da.inicializarAccessor<ClaseCostoProductoAccessor>();
        }

        protected override void destruir()
        {
            claseCostoProductoAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de ClaseCostoProducto
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ClaseCostoProductoInfo> ObtenerPorPagina(PaginacionInfo pagina, ClaseCostoProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var result = new ResultadoInfo<ClaseCostoProductoInfo>();
                var condicion = da.Tabla<ClaseCostoProductoInfo>().Where(e=> e.Activo == filtro.Activo);
                if (filtro.ClaseCostoProductoID > 0)
                {
                    condicion = condicion.Where(e=> e.ClaseCostoProductoID == filtro.ClaseCostoProductoID);
                }
                
                int inicio = pagina.Inicio;
                int limite = pagina.Limite;
                if (inicio > 1)
                {
                    int limiteReal = (limite - inicio) + 1;
                    inicio = (limite / limiteReal);
                    limite = limiteReal;
                }
                var paginado = condicion
                                .OrderBy(e => e.ClaseCostoProductoID)
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
        /// Obtiene una lista de ClaseCostoProducto
        /// </summary>
        /// <returns></returns>
        public IQueryable<ClaseCostoProductoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<ClaseCostoProductoInfo>();
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
        /// Obtiene una lista de ClaseCostoProducto filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<ClaseCostoProductoInfo> ObtenerTodos(EstatusEnum estatus)
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
        /// Obtiene una entidad de ClaseCostoProducto por su Id
        /// </summary>
        /// <param name="claseCostoProductoId">Obtiene una entidad ClaseCostoProducto por su Id</param>
        /// <returns></returns>
        public ClaseCostoProductoInfo ObtenerPorID(int claseCostoProductoId)
        {
            try
            {
                Logger.Info();
                return ObtenerTodos().FirstOrDefault(e => e.ClaseCostoProductoID == claseCostoProductoId);
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
        /// Metodo para Guardar/Modificar una entidad ClaseCostoProducto
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(ClaseCostoProductoInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.ClaseCostoProductoID > 0)
                {
                    info.FechaCreacion = da.FechaServidor();
                    id = da.Actualizar<ClaseCostoProductoInfo>(info);
                }
                else
                {
                    id = da.Insertar<ClaseCostoProductoInfo>(info);
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
        /// Obtiene una lista de Productos con la entidad de unidad cargada
        /// </summary>
        /// <returns></returns>
        public IQueryable<ClaseCostoProductoInfo> ObtenerConCuentaSAP(IQueryable<ClaseCostoProductoInfo> query)
        {
            try
            {
                Logger.Info();
                var tblCuentaSAP = base.da.Tabla<CuentaSAPInfo>();
                query = from q in query
                        join c in tblCuentaSAP on q.CuentaSAPID equals c.CuentaSAPID
                        select new ClaseCostoProductoInfo ( q, c );

                return query;
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
