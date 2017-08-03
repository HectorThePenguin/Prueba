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
    internal class CheckListRoladoraAccionDAL : BaseDAL
    {
        CheckListRoladoraAccionAccessor checkListRoladoraAccionAccessor;

        protected override void inicializar()
        {
            checkListRoladoraAccionAccessor = da.inicializarAccessor<CheckListRoladoraAccionAccessor>();
        }

        protected override void destruir()
        {
            checkListRoladoraAccionAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de CheckListRoladoraAccion
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CheckListRoladoraAccionInfo> ObtenerPorPagina(PaginacionInfo pagina, CheckListRoladoraAccionInfo filtro)
        {
            try
            {
                Logger.Info();
                ResultadoInfo<CheckListRoladoraAccionInfo> result = new ResultadoInfo<CheckListRoladoraAccionInfo>();
                var condicion = da.Tabla<CheckListRoladoraAccionInfo>().Where(e=> e.Activo == filtro.Activo);
                if (filtro.CheckListRoladoraAccionID > 0)
                {
                    condicion = condicion.Where(e=> e.CheckListRoladoraAccionID == filtro.CheckListRoladoraAccionID);
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
        /// Obtiene una lista de CheckListRoladoraAccion
        /// </summary>
        /// <returns></returns>
        public IQueryable<CheckListRoladoraAccionInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<CheckListRoladoraAccionInfo>();
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
        /// Obtiene una lista de CheckListRoladoraAccion filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<CheckListRoladoraAccionInfo> ObtenerTodos(EstatusEnum estatus)
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
        /// Obtiene una entidad de CheckListRoladoraAccion por su Id
        /// </summary>
        /// <param name="checkListRoladoraAccionId">Obtiene una entidad CheckListRoladoraAccion por su Id</param>
        /// <returns></returns>
        public CheckListRoladoraAccionInfo ObtenerPorID(int checkListRoladoraAccionId)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.CheckListRoladoraAccionID == checkListRoladoraAccionId).FirstOrDefault();
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
        /// Obtiene una entidad de CheckListRoladoraAccion por su descripcion
        /// </summary>
        /// <param name="descripción">Obtiene una entidad CheckListRoladoraAccion por su descripcion</param>
        /// <returns></returns>
        public CheckListRoladoraAccionInfo ObtenerPorDescripcion(string descripcion)
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
        /// Metodo para Guardar/Modificar una entidad CheckListRoladoraAccion
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(CheckListRoladoraAccionInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.CheckListRoladoraAccionID > 0)
                {
                    id = da.Actualizar<CheckListRoladoraAccionInfo>(info);
                    checkListRoladoraAccionAccessor.ActualizarFechaModificacion(info.CheckListRoladoraAccionID);
                }
                else
                {
                    id = da.Insertar<CheckListRoladoraAccionInfo>(info);
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
