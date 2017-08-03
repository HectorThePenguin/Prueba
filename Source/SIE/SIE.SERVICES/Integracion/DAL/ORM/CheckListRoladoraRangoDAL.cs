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
    internal class CheckListRoladoraRangoDAL : BaseDAL
    {
        CheckListRoladoraRangoAccessor checkListRoladoraRangoAccessor;

        protected override void inicializar()
        {
            checkListRoladoraRangoAccessor = da.inicializarAccessor<CheckListRoladoraRangoAccessor>();
        }

        protected override void destruir()
        {
            checkListRoladoraRangoAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de CheckListRoladoraRango
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CheckListRoladoraRangoInfo> ObtenerPorPagina(PaginacionInfo pagina, CheckListRoladoraRangoInfo filtro)
        {
            try
            {
                Logger.Info();
                ResultadoInfo<CheckListRoladoraRangoInfo> result = new ResultadoInfo<CheckListRoladoraRangoInfo>();
                var condicion = da.Tabla<CheckListRoladoraRangoInfo>().Where(e=> e.Activo == filtro.Activo);
                if (filtro.CheckListRoladoraRangoID > 0)
                {
                    condicion = condicion.Where(e=> e.CheckListRoladoraRangoID == filtro.CheckListRoladoraRangoID);
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
        /// Obtiene una lista de CheckListRoladoraRango
        /// </summary>
        /// <returns></returns>
        public IQueryable<CheckListRoladoraRangoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<CheckListRoladoraRangoInfo>();
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
        /// Obtiene una lista de CheckListRoladoraRango filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<CheckListRoladoraRangoInfo> ObtenerTodos(EstatusEnum estatus)
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
        /// Obtiene una entidad de CheckListRoladoraRango por su Id
        /// </summary>
        /// <param name="checkListRoladoraRangoId">Obtiene una entidad CheckListRoladoraRango por su Id</param>
        /// <returns></returns>
        public CheckListRoladoraRangoInfo ObtenerPorID(int checkListRoladoraRangoId)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.CheckListRoladoraRangoID == checkListRoladoraRangoId).FirstOrDefault();
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
        /// Obtiene una entidad de CheckListRoladoraRango por su descripcion
        /// </summary>
        /// <param name="descripcion"> </param>
        /// <returns></returns>
        public CheckListRoladoraRangoInfo ObtenerPorDescripcion(string descripcion)
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
        /// Metodo para Guardar/Modificar una entidad CheckListRoladoraRango
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(CheckListRoladoraRangoInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.CheckListRoladoraRangoID > 0)
                {
                    id = da.Actualizar<CheckListRoladoraRangoInfo>(info);
                    checkListRoladoraRangoAccessor.ActualizarFechaModificacion(info.CheckListRoladoraRangoID);
                }
                else
                {
                    id = da.Insertar<CheckListRoladoraRangoInfo>(info);
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
