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
    internal class CheckListRoladoraGeneralDAL : BaseDAL
    {
        CheckListRoladoraGeneralAccessor checkListRoladoraGeneralAccessor;

        protected override void inicializar()
        {
            checkListRoladoraGeneralAccessor = da.inicializarAccessor<CheckListRoladoraGeneralAccessor>();
        }

        protected override void destruir()
        {
            checkListRoladoraGeneralAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de CheckListRoladoraGeneral
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CheckListRoladoraGeneralInfo> ObtenerPorPagina(PaginacionInfo pagina, CheckListRoladoraGeneralInfo filtro)
        {
            try
            {
                Logger.Info();
                ResultadoInfo<CheckListRoladoraGeneralInfo> result = new ResultadoInfo<CheckListRoladoraGeneralInfo>();
                var condicion = da.Tabla<CheckListRoladoraGeneralInfo>().Where(e=> e.Activo == filtro.Activo);
                if (filtro.CheckListRoladoraGeneralID > 0)
                {
                    condicion = condicion.Where(e=> e.CheckListRoladoraGeneralID == filtro.CheckListRoladoraGeneralID);
                }
                if (filtro.CheckListRoladoraGeneralID > 0)
                {
                    condicion = condicion.Where(e=> e.CheckListRoladoraGeneralID == filtro.CheckListRoladoraGeneralID);
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
                                .OrderBy(e => e.CheckListRoladoraGeneralID)
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
        /// Obtiene una lista de CheckListRoladoraGeneral
        /// </summary>
        /// <returns></returns>
        public IQueryable<CheckListRoladoraGeneralInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<CheckListRoladoraGeneralInfo>();
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
        /// Obtiene una lista de CheckListRoladoraGeneral filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<CheckListRoladoraGeneralInfo> ObtenerTodos(EstatusEnum estatus)
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
        /// Obtiene una entidad de CheckListRoladoraGeneral por su Id
        /// </summary>
        /// <param name="checkListRoladoraGeneralId">Obtiene una entidad CheckListRoladoraGeneral por su Id</param>
        /// <returns></returns>
        public CheckListRoladoraGeneralInfo ObtenerPorID(int checkListRoladoraGeneralId)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.CheckListRoladoraGeneralID == checkListRoladoraGeneralId).FirstOrDefault();
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

        ///// <summary>
        ///// Obtiene una entidad de CheckListRoladoraGeneral por su descripcion
        ///// </summary>
        ///// <param name="descripción">Obtiene una entidad CheckListRoladoraGeneral por su descripcion</param>
        ///// <returns></returns>
        //public CheckListRoladoraGeneralInfo ObtenerPorDescripcion(string descripcion)
        //{
        //    try
        //    {
        //        Logger.Info();
        //        return this.ObtenerTodos().Where(e=> e.CheckListRoladoraGeneralID.ToLower() == descripcion.ToLower()).FirstOrDefault();
        //    }
        //    catch(ExcepcionGenerica)
        //    {
        //        throw;
        //    }
        //    catch(Exception ex)
        //    {
        //        Logger.Error(ex);
        //        throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
        //    }
        //}

        /// <summary>
        /// Metodo para Guardar/Modificar una entidad CheckListRoladoraGeneral
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(CheckListRoladoraGeneralInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.CheckListRoladoraGeneralID > 0)
                {
                    id = da.Actualizar<CheckListRoladoraGeneralInfo>(info);
                    checkListRoladoraGeneralAccessor.ActualizarFechaModificacion(info.CheckListRoladoraGeneralID);
                }
                else
                {
                    id = da.Insertar<CheckListRoladoraGeneralInfo>(info);
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
