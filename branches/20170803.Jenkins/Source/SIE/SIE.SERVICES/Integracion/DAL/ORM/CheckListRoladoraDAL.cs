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
    internal class CheckListRoladoraDAL : BaseDAL
    {
        CheckListRoladoraAccessor checkListRoladoraAccessor;

        protected override void inicializar()
        {
            checkListRoladoraAccessor = da.inicializarAccessor<CheckListRoladoraAccessor>();
        }

        protected override void destruir()
        {
            checkListRoladoraAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de CheckListRoladora
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CheckListRoladoraInfo> ObtenerPorPagina(PaginacionInfo pagina, CheckListRoladoraInfo filtro)
        {
            try
            {
                Logger.Info();
                ResultadoInfo<CheckListRoladoraInfo> result = new ResultadoInfo<CheckListRoladoraInfo>();
                var condicion = da.Tabla<CheckListRoladoraInfo>().Where(e=> e.Activo == filtro.Activo);
                if (filtro.CheckListRoladoraID > 0)
                {
                    condicion = condicion.Where(e=> e.CheckListRoladoraID == filtro.CheckListRoladoraID);
                }
                if (filtro.CheckListRoladoraID > 0)
                {
                    condicion = condicion.Where(e => e.CheckListRoladoraID == filtro.CheckListRoladoraID);
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
                                .OrderBy(e => e.CheckListRoladoraID)
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
        /// Obtiene una lista de CheckListRoladora
        /// </summary>
        /// <returns></returns>
        public IQueryable<CheckListRoladoraInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<CheckListRoladoraInfo>();
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
        /// Obtiene una lista de CheckListRoladora filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<CheckListRoladoraInfo> ObtenerTodos(EstatusEnum estatus)
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
        /// Obtiene una entidad de CheckListRoladora por su Id
        /// </summary>
        /// <param name="checkListRoladoraId">Obtiene una entidad CheckListRoladora por su Id</param>
        /// <returns></returns>
        public CheckListRoladoraInfo ObtenerPorID(int checkListRoladoraId)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.CheckListRoladoraID == checkListRoladoraId).FirstOrDefault();
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
        ///// Obtiene una entidad de CheckListRoladora por su descripcion
        ///// </summary>
        ///// <param name="descripcion">Obtiene una entidad CheckListRoladora por su descripcion</param>
        ///// <returns></returns>
        //public CheckListRoladoraInfo ObtenerPorDescripcion(string descripcion)
        //{
        //    try
        //    {
        //        Logger.Info();
        //        return this.ObtenerTodos().Where(e=> e.CheckListRoladoraID.ToLower() == descripcion.ToLower()).FirstOrDefault();
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
        /// Metodo para Guardar/Modificar una entidad CheckListRoladora
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(CheckListRoladoraInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.CheckListRoladoraID > 0)
                {
                    id = da.Actualizar<CheckListRoladoraInfo>(info);
                    checkListRoladoraAccessor.ActualizarFechaModificacion(info.CheckListRoladoraID);
                }
                else
                {
                    id = da.Insertar<CheckListRoladoraInfo>(info);
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
