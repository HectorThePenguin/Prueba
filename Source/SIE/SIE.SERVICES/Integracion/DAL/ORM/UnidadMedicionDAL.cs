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
    internal class UnidadMedicionDAL : BaseDAL
    {
        UnidadMedicionAccessor unidadMedicionAccessor;

        protected override void inicializar()
        {
            unidadMedicionAccessor = da.inicializarAccessor<UnidadMedicionAccessor>();
        }

        protected override void destruir()
        {
            unidadMedicionAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de UnidadMedicion
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<UnidadMedicionInfo> ObtenerPorPagina(PaginacionInfo pagina, UnidadMedicionInfo filtro)
        {
            try
            {
                Logger.Info();
                var result = new ResultadoInfo<UnidadMedicionInfo>();
                var condicion = da.Tabla<UnidadMedicionInfo>().Where(e=> e.Activo == filtro.Activo);
                if (filtro.UnidadID > 0)
                {
                    condicion = condicion.Where(e=> e.UnidadID == filtro.UnidadID);
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
        /// Obtiene una lista de UnidadMedicion
        /// </summary>
        /// <returns></returns>
        public IQueryable<UnidadMedicionInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<UnidadMedicionInfo>();
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
        /// Obtiene una lista de UnidadMedicion filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<UnidadMedicionInfo> ObtenerTodos(EstatusEnum estatus)
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
        /// Obtiene una entidad de UnidadMedicion por su Id
        /// </summary>
        /// <param name="unidadMedicionId">Obtiene una entidad UnidadMedicion por su Id</param>
        /// <returns></returns>
        public UnidadMedicionInfo ObtenerPorID(int unidadMedicionId)
        {
            try
            {
                Logger.Info();
                return ObtenerTodos().FirstOrDefault(e => e.UnidadID == unidadMedicionId);
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
        /// Obtiene una entidad de UnidadMedicion por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad UnidadMedicion por su descripcion</param>
        /// <returns></returns>
        public UnidadMedicionInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                return ObtenerTodos().FirstOrDefault(e => e.Descripcion.ToLower() == descripcion.ToLower());
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
        /// Metodo para Guardar/Modificar una entidad UnidadMedicion
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(UnidadMedicionInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.UnidadID > 0)
                {
                    info.FechaModificacion = da.FechaServidor();
                    id = da.Actualizar<UnidadMedicionInfo>(info);
                }
                else
                {
                    id = da.Insertar<UnidadMedicionInfo>(info);
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
