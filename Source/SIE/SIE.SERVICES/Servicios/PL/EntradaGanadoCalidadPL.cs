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
    public class EntradaGanadoCalidadPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad EntradaGanadoCalidad
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(EntradaGanadoCalidadInfo info)
        {
            try
            {
                Logger.Info();
                var entradaGanadoCalidadBL = new EntradaGanadoCalidadBL();
                int result = entradaGanadoCalidadBL.Guardar(info);
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
        public ResultadoInfo<EntradaGanadoCalidadInfo> ObtenerPorPagina(PaginacionInfo pagina, EntradaGanadoCalidadInfo filtro)
        {
            try
            {
                Logger.Info();
                var entradaGanadoCalidadBL = new EntradaGanadoCalidadBL();
                ResultadoInfo<EntradaGanadoCalidadInfo> result = entradaGanadoCalidadBL.ObtenerPorPagina(pagina, filtro);
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
        public IList<EntradaGanadoCalidadInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var entradaGanadoCalidadBL = new EntradaGanadoCalidadBL();
                IList<EntradaGanadoCalidadInfo> result = entradaGanadoCalidadBL.ObtenerTodos();
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
        public IList<EntradaGanadoCalidadInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var entradaGanadoCalidadBL = new EntradaGanadoCalidadBL();
                IList<EntradaGanadoCalidadInfo> result = entradaGanadoCalidadBL.ObtenerTodos(estatus);
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
        /// <param name="entradaGanadoCalidadID"></param>
        /// <returns></returns>
        public EntradaGanadoCalidadInfo ObtenerPorID(int entradaGanadoCalidadID)
        {
            try
            {
                Logger.Info();
                var entradaGanadoCalidadBL = new EntradaGanadoCalidadBL();
                EntradaGanadoCalidadInfo result = entradaGanadoCalidadBL.ObtenerPorID(entradaGanadoCalidadID);
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
        public EntradaGanadoCalidadInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var entradaGanadoCalidadBL = new EntradaGanadoCalidadBL();
                EntradaGanadoCalidadInfo result = entradaGanadoCalidadBL.ObtenerPorDescripcion(descripcion);
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
        ///  Obtiene una lista filtrando por Entrada Ganado Id
        /// </summary>
        /// <returns></returns>
        public List<EntradaGanadoCalidadInfo> ObtenerEntradaGanadoId(int entragaGanadoId)
        {
            try
            {
                Logger.Info();
                var entradaGanadoCalidadBL = new EntradaGanadoCalidadBL();
                List<EntradaGanadoCalidadInfo> result = entradaGanadoCalidadBL.ObtenerPorEntradaGanadoId(entragaGanadoId);
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

