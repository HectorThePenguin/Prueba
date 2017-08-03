using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class LoteProyeccionPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad LoteProyeccion
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(LoteProyeccionInfo info)
        {
            try
            {
                Logger.Info();
                var loteProyeccionBL = new LoteProyeccionBL();
                int result = loteProyeccionBL.Guardar(info);
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
        public ResultadoInfo<LoteProyeccionInfo> ObtenerPorPagina(PaginacionInfo pagina, LoteProyeccionInfo filtro)
        {
            try
            {
                Logger.Info();
                var loteProyeccionBL = new LoteProyeccionBL();
                ResultadoInfo<LoteProyeccionInfo> result = loteProyeccionBL.ObtenerPorPagina(pagina, filtro);
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
        public IList<LoteProyeccionInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var loteProyeccionBL = new LoteProyeccionBL();
                IList<LoteProyeccionInfo> result = loteProyeccionBL.ObtenerTodos();
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
        public IList<LoteProyeccionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var loteProyeccionBL = new LoteProyeccionBL();
                IList<LoteProyeccionInfo> result = loteProyeccionBL.ObtenerTodos(estatus);
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
        /// <param name="loteProyeccionID"></param>
        /// <returns></returns>
        public LoteProyeccionInfo ObtenerPorID(int loteProyeccionID)
        {
            try
            {
                Logger.Info();
                var loteProyeccionBL = new LoteProyeccionBL();
                LoteProyeccionInfo result = loteProyeccionBL.ObtenerPorID(loteProyeccionID);
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
        public LoteProyeccionInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var loteProyeccionBL = new LoteProyeccionBL();
                LoteProyeccionInfo result = loteProyeccionBL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene la proyeccion por lote
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        public LoteProyeccionInfo ObtenerPorLote(LoteInfo lote)
        {
            try
            {
                Logger.Info();
                var loteProyeccionBL = new LoteProyeccionBL();
                LoteProyeccionInfo result = loteProyeccionBL.ObtenerPorLote(lote);
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
        /// Obtiene la proyeccion por lote
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        public LoteProyeccionInfo ObtenerPorLoteCompleto(LoteInfo lote)
        {
            try
            {
                Logger.Info();
                var loteProyeccionBL = new LoteProyeccionBL();
                LoteProyeccionInfo result = loteProyeccionBL.ObtenerPorLoteCompleto(lote);
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
        /// Metodo para guardar la configuracion de reimplantes
        /// </summary>
        /// <param name="configuracionReimplante">Representa el Contexto de lo que se va a guardar</param>
        public int GuardarConfiguracionReimplante(ConfiguracionReimplanteModel configuracionReimplante)
        {
            try
            {
                Logger.Info();
                var loteProyeccionBL = new LoteProyeccionBL();
                int result = loteProyeccionBL.GuardarConfiguracionReimplante(configuracionReimplante);
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
        /// Obtiene una entidad LoteProyeccion por su Id
        /// </summary>
        /// <param name="loteProyeccionID">Obtiene una entidad LoteProyeccion por su Id</param>
        /// <returns></returns>
        public LoteProyeccionInfo ObtenerBitacoraPorLoteProyeccionID(int loteProyeccionID)
        {
            try
            {
                Logger.Info();
                var loteProyeccionBL = new LoteProyeccionBL();
                LoteProyeccionInfo result = loteProyeccionBL.ObtenerBitacoraPorLoteProyeccionID(loteProyeccionID);
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

