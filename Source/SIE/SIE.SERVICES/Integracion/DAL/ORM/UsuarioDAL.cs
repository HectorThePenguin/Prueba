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
    internal class UsuarioDAL : BaseDAL
    {
        UsuarioAccessor usuarioAccessor;

        protected override void inicializar()
        {
            usuarioAccessor = da.inicializarAccessor<UsuarioAccessor>();
        }

        protected override void destruir()
        {
            usuarioAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de Usuario
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<UsuarioInfo> ObtenerPorPagina(PaginacionInfo pagina, UsuarioInfo filtro)
        {
            try
            {
                Logger.Info();
                var result = new ResultadoInfo<UsuarioInfo>();
                var condicion = da.Tabla<UsuarioInfo>().Where(e=> e.Activo == filtro.Activo);
                if (filtro.UsuarioID > 0)
                {
                    condicion = condicion.Where(e=> e.UsuarioID == filtro.UsuarioID);
                }
                if (!string.IsNullOrEmpty(filtro.Nombre))
                {
                    condicion = condicion.Where(e=> e.Nombre.Contains(filtro.Nombre));
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
                                .OrderBy(e => e.UsuarioID)
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
        /// Obtiene una lista de Usuario
        /// </summary>
        /// <returns></returns>
        public IQueryable<UsuarioInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<UsuarioInfo>();
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
        /// Obtiene una lista de Usuario filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<UsuarioInfo> ObtenerTodos(EstatusEnum estatus)
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
        /// Obtiene una entidad de Usuario por su Id
        /// </summary>
        /// <param name="usuarioId">Obtiene una entidad Usuario por su Id</param>
        /// <returns></returns>
        public UsuarioInfo ObtenerPorID(int usuarioId)
        {
            try
            {
                Logger.Info();
                return ObtenerTodos().FirstOrDefault(e => e.UsuarioID == usuarioId);
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
        /// Obtiene una entidad de Usuario por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad Usuario por su descripcion</param>
        /// <returns></returns>
        public UsuarioInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                return ObtenerTodos().FirstOrDefault(e => e.Nombre.ToLower() == descripcion.ToLower());
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
        /// Metodo para Guardar/Modificar una entidad Usuario
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(UsuarioInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.UsuarioID > 0)
                {
                    id = da.Actualizar<UsuarioInfo>(info);
                }
                else
                {
                    id = da.Insertar<UsuarioInfo>(info);
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
