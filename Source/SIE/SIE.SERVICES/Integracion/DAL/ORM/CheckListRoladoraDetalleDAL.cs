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
    internal class CheckListRoladoraDetalleDAL : BaseDAL
    {
        CheckListRoladoraDetalleAccessor checkListRoladoraDetalleAccessor;

        protected override void inicializar()
        {
            checkListRoladoraDetalleAccessor = da.inicializarAccessor<CheckListRoladoraDetalleAccessor>();
        }

        protected override void destruir()
        {
            checkListRoladoraDetalleAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de CheckListRoladoraDetalle
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CheckListRoladoraDetalleInfo> ObtenerPorPagina(PaginacionInfo pagina, CheckListRoladoraDetalleInfo filtro)
        {
            try
            {
                Logger.Info();
                ResultadoInfo<CheckListRoladoraDetalleInfo> result = new ResultadoInfo<CheckListRoladoraDetalleInfo>();
                var condicion = da.Tabla<CheckListRoladoraDetalleInfo>().Where(e=> e.Activo == filtro.Activo);
                if (filtro.CheckListRoladoraDetalleID > 0)
                {
                    condicion = condicion.Where(e=> e.CheckListRoladoraDetalleID == filtro.CheckListRoladoraDetalleID);
                }
                if (filtro.CheckListRoladoraDetalleID > 0)
                {
                    condicion = condicion.Where(e=> e.CheckListRoladoraDetalleID == filtro.CheckListRoladoraDetalleID);
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
                                .OrderBy(e => e.CheckListRoladoraDetalleID)
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
        /// Obtiene una lista de CheckListRoladoraDetalle
        /// </summary>
        /// <returns></returns>
        public IQueryable<CheckListRoladoraDetalleInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<CheckListRoladoraDetalleInfo>();
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
        /// Obtiene una lista de CheckListRoladoraDetalle filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<CheckListRoladoraDetalleInfo> ObtenerTodos(EstatusEnum estatus)
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
        /// Obtiene una entidad de CheckListRoladoraDetalle por su Id
        /// </summary>
        /// <param name="checkListRoladoraDetalleId">Obtiene una entidad CheckListRoladoraDetalle por su Id</param>
        /// <returns></returns>
        public CheckListRoladoraDetalleInfo ObtenerPorID(int checkListRoladoraDetalleId)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.CheckListRoladoraDetalleID == checkListRoladoraDetalleId).FirstOrDefault();
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
        ///// Obtiene una entidad de CheckListRoladoraDetalle por su descripcion
        ///// </summary>
        ///// <param name="descripción">Obtiene una entidad CheckListRoladoraDetalle por su descripcion</param>
        ///// <returns></returns>
        //public CheckListRoladoraDetalleInfo ObtenerPorDescripcion(string descripcion)
        //{
        //    try
        //    {
        //        Logger.Info();
        //        return this.ObtenerTodos().Where(e=> e.CheckListRoladoraDetalleID.ToLower() == descripcion.ToLower()).FirstOrDefault();
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
        /// Metodo para Guardar/Modificar una entidad CheckListRoladoraDetalle
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(CheckListRoladoraDetalleInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.CheckListRoladoraDetalleID > 0)
                {
                    id = da.Actualizar<CheckListRoladoraDetalleInfo>(info);
                    checkListRoladoraDetalleAccessor.ActualizarFechaModificacion(info.CheckListRoladoraDetalleID);
                }
                else
                {
                    id = da.Insertar<CheckListRoladoraDetalleInfo>(info);
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
