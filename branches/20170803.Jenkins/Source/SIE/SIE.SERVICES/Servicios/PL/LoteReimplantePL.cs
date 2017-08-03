using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class LoteReimplantePL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad LoteReimplante
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(LoteReimplanteInfo info)
        {
            try
            {
                Logger.Info();
                var loteReimplanteBL = new LoteReimplanteBL();
                int result = loteReimplanteBL.Guardar(info);
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
        public ResultadoInfo<LoteReimplanteInfo> ObtenerPorPagina(PaginacionInfo pagina, LoteReimplanteInfo filtro)
        {
            try
            {
                Logger.Info();
                var loteReimplanteBL = new LoteReimplanteBL();
                ResultadoInfo<LoteReimplanteInfo> result = loteReimplanteBL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una lista
        /// </summary>
        /// <returns></returns>
        public IList<LoteReimplanteInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var loteReimplanteBL = new LoteReimplanteBL();
                IList<LoteReimplanteInfo> result = loteReimplanteBL.ObtenerTodos();
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
        ///  Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<LoteReimplanteInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var loteReimplanteBL = new LoteReimplanteBL();
                IList<LoteReimplanteInfo> result = loteReimplanteBL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad por su Id
        /// </summary>
        /// <param name="loteReimplanteID"></param>
        /// <returns></returns>
        public LoteReimplanteInfo ObtenerPorID(int loteReimplanteID)
        {
            try
            {
                Logger.Info();
                var loteReimplanteBL = new LoteReimplanteBL();
                LoteReimplanteInfo result = loteReimplanteBL.ObtenerPorID(loteReimplanteID);
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
        /// Obtiene una entidad por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public LoteReimplanteInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var loteReimplanteBL = new LoteReimplanteBL();
                LoteReimplanteInfo result = loteReimplanteBL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene una entidad por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public LoteReimplanteInfo ObtenerPorLote(LoteInfo lote){
            try
            {
                Logger.Info();
                var loteReimplanteBL = new LoteReimplanteBL();
                LoteReimplanteInfo result = loteReimplanteBL.ObtenerPorLote(lote);
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

