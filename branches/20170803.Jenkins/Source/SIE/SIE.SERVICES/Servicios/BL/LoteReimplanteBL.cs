using System;
using System.Collections.Generic;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    internal class LoteReimplanteBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad LoteReimplante
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(LoteReimplanteInfo info)
        {
            try
            {
                Logger.Info();
                var loteReimplanteDAL = new LoteReimplanteDAL();
                int result = info.LoteReimplanteID;
                if (info.LoteReimplanteID == 0)
                {
                    result = loteReimplanteDAL.Crear(info);
                }
                else
                {
                    loteReimplanteDAL.Actualizar(info);
                }
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
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<LoteReimplanteInfo> ObtenerPorPagina(PaginacionInfo pagina, LoteReimplanteInfo filtro)
        {
            try
            {
                Logger.Info();
                var loteReimplanteDAL = new LoteReimplanteDAL();
                ResultadoInfo<LoteReimplanteInfo> result = loteReimplanteDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de LoteReimplante
        /// </summary>
        /// <returns></returns>
        internal IList<LoteReimplanteInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var loteReimplanteDAL = new LoteReimplanteDAL();
                IList<LoteReimplanteInfo> result = loteReimplanteDAL.ObtenerTodos();
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
        /// Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal IList<LoteReimplanteInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var loteReimplanteDAL = new LoteReimplanteDAL();
                IList<LoteReimplanteInfo> result = loteReimplanteDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad LoteReimplante por su Id
        /// </summary>
        /// <param name="loteReimplanteID">Obtiene una entidad LoteReimplante por su Id</param>
        /// <returns></returns>
        internal LoteReimplanteInfo ObtenerPorID(int loteReimplanteID)
        {
            try
            {
                Logger.Info();
                var loteReimplanteDAL = new LoteReimplanteDAL();
                LoteReimplanteInfo result = loteReimplanteDAL.ObtenerPorID(loteReimplanteID);
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
        /// Obtiene una entidad LoteReimplante por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad LoteReimplante por su Id</param>
        /// <returns></returns>
        internal LoteReimplanteInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var loteReimplanteDAL = new LoteReimplanteDAL();
                LoteReimplanteInfo result = loteReimplanteDAL.ObtenerPorDescripcion(descripcion);
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
        /// Metodo para Guardar/Modificar una entidad LoteReimplante
        /// </summary>
        /// <param name="listaReimplantes"></param>
        internal int GuardarListaReimplantes(List<LoteReimplanteInfo> listaReimplantes)
        {
            try
            {
                Logger.Info();
                var loteReimplanteDAL = new LoteReimplanteDAL();
                int result = loteReimplanteDAL.GuardarListaReimplantes(listaReimplantes);
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
        /// Obtiene lote reimplante de un lote
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        public LoteReimplanteInfo ObtenerPorLote(LoteInfo lote)
        {
            try
            {
                Logger.Info();
                var loteReimplanteDal = new LoteReimplanteDAL();
                var result = loteReimplanteDal.ObtenerPorLote(lote);
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
        /// Obtiene lote reimplante de un lote
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        public List<LoteReimplanteInfo> ObtenerListaPorLote(LoteInfo lote)
        {
            try
            {
                Logger.Info();
                var loteReimplanteDal = new LoteReimplanteDAL();
                var result = loteReimplanteDal.ObtenerListaPorLote(lote);
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
        /// Obtiene una lista de lotes reimplante
        /// </summary>
        /// <param name="organizacionId"> </param>
        /// <param name="lotes"></param>
        /// <returns></returns>
        internal IList<LoteReimplanteInfo> ObtenerPorLoteXML(int organizacionId, IList<LoteInfo> lotes)
        {
            try
            {
                Logger.Info();
                var loteReimplanteDal = new LoteReimplanteDAL();
                IList<LoteReimplanteInfo> result = loteReimplanteDal.ObtenerPorLoteXML(organizacionId, lotes);
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
    }
}

