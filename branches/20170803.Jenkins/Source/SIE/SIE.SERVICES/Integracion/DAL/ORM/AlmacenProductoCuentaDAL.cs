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
    internal class AlmacenProductoCuentaDAL : BaseDAL
    {
        AlmacenProductoCuentaAccessor almacenProductoCuentaAccessor;

        protected override void inicializar()
        {
            almacenProductoCuentaAccessor = da.inicializarAccessor<AlmacenProductoCuentaAccessor>();
        }

        protected override void destruir()
        {
            almacenProductoCuentaAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de AlmacenProductoCuenta
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<AlmacenProductoCuentaInfo> ObtenerPorPagina(PaginacionInfo pagina, AlmacenProductoCuentaInfo filtro)
        {
            try
            {
                Logger.Info();
                ResultadoInfo<AlmacenProductoCuentaInfo> result = new ResultadoInfo<AlmacenProductoCuentaInfo>();
                var condicion = da.Tabla<AlmacenProductoCuentaInfo>().Where(e=> e.Activo == filtro.Activo);
                if (filtro.AlmacenProductoCuentaID > 0)
                {
                    condicion = condicion.Where(e=> e.AlmacenProductoCuentaID == filtro.AlmacenProductoCuentaID);
                }
                //if (!string.IsNullOrEmpty(filtro.AlmacenProductoCuentaID))
                //{
                //    condicion = condicion.Where(e=> e.AlmacenProductoCuentaID.Contains(filtro.AlmacenProductoCuentaID));
                //}
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
                                .OrderBy(e => e.AlmacenProductoCuentaID)
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
        /// Obtiene una lista de AlmacenProductoCuenta
        /// </summary>
        /// <returns></returns>
        public IQueryable<AlmacenProductoCuentaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<AlmacenProductoCuentaInfo>();
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
        /// Obtiene una lista de AlmacenProductoCuenta filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<AlmacenProductoCuentaInfo> ObtenerTodos(EstatusEnum estatus)
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
        /// Obtiene una entidad de AlmacenProductoCuenta por su Id
        /// </summary>
        /// <param name="almacenProductoCuentaId">Obtiene una entidad AlmacenProductoCuenta por su Id</param>
        /// <returns></returns>
        public AlmacenProductoCuentaInfo ObtenerPorID(int almacenProductoCuentaId)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.AlmacenProductoCuentaID == almacenProductoCuentaId).FirstOrDefault();
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
        /// Metodo para Guardar/Modificar una entidad AlmacenProductoCuenta
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(AlmacenProductoCuentaInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.AlmacenProductoCuentaID > 0)
                {
                    id = da.Actualizar<AlmacenProductoCuentaInfo>(info);
                    almacenProductoCuentaAccessor.ActualizarFechaModificacion(info.AlmacenProductoCuentaID);
                }
                else
                {
                    id = da.Insertar<AlmacenProductoCuentaInfo>(info);
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
        /// Obtiene una entidad de AlmacenProductoCuenta almacen id y producto id
        /// </summary>
        /// <param name="info">Obtiene una entidad AlmacenProductoCuenta por almacen id y producto id</param>
        /// <returns></returns>
        public AlmacenProductoCuentaInfo ObtenerPorAlmacenIDProductoID(AlmacenProductoCuentaInfo info)
        {
            try
            {
                Logger.Info();
                var lista = da.Tabla<AlmacenProductoCuentaInfo>();
                AlmacenProductoCuentaInfo resultado =
                    lista.FirstOrDefault(
                        alma => alma.AlmacenID == info.Almacen.AlmacenID && alma.ProductoID == info.Producto.ProductoId);
                return resultado;
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
        /// Obtiene los productos cuenta por clave de
        /// almacen
        /// </summary>
        /// <param name="almacenID"></param>
        /// <returns></returns>
        internal List<AlmacenProductoCuentaInfo> ObtenerPorAlmacenID(int almacenID)
        {
            try
            {
                Logger.Info();
                var lista = da.Tabla<AlmacenProductoCuentaInfo>();
                List<AlmacenProductoCuentaInfo> resultado =
                    lista.Where(claveAlmacen => claveAlmacen.AlmacenID == almacenID).ToList();
                return resultado;
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
