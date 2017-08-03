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
    public class CalidadGanadoPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una Calidad Ganado
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public void Guardar(CalidadGanadoInfo info)
        {
            try
            {
                Logger.Info();
                var calidadGanadoBL = new CalidadGanadoBL();
                calidadGanadoBL.Guardar(info);
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CalidadGanadoInfo> ObtenerPorPagina(PaginacionInfo pagina, CalidadGanadoInfo filtro)
        {
            try
            {
                Logger.Info();
                var calidadGanadoBL = new CalidadGanadoBL();
                ResultadoInfo<CalidadGanadoInfo> result = calidadGanadoBL.ObtenerPorPagina(pagina, filtro);

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
        ///     Obtiene una entidad CalidadGanado por su Entrada Id
        /// </summary>
        /// <returns></returns>
        public IList<CalidadGanadoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var calidadGanadoBL = new CalidadGanadoBL();
                IList<CalidadGanadoInfo> result = calidadGanadoBL.ObtenerTodos();

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
        ///    Obtiene una lista de CalidadGanado filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<CalidadGanadoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var calidadGanadoBL = new CalidadGanadoBL();
                IList<CalidadGanadoInfo> result = calidadGanadoBL.ObtenerTodos(estatus);

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
        ///    Obtiene una CalidadGanado por su Id
        /// </summary>
        /// <param name="calidadGanadoID"></param>
        /// <returns></returns>
        public CalidadGanadoInfo ObtenerPorID(int calidadGanadoID)
        {
            try
            {
                Logger.Info();
                var calidadGanadoBL = new CalidadGanadoBL();
                CalidadGanadoInfo result = calidadGanadoBL.ObtenerPorID(calidadGanadoID);

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
        /// Obtiene la Lista de Entrada Ganado Calidad
        /// </summary>
        /// <returns></returns>
        public List<EntradaGanadoCalidadInfo> ObtenerListaCalidadGanado()
        {
            try
            {
                Logger.Info();
                var calidadGanadoBL = new CalidadGanadoBL();
                List<EntradaGanadoCalidadInfo> result = calidadGanadoBL.ObtenerListaCalidadGanado();
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
        /// /// <param name="sexo"></param>
        /// <returns></returns>
        public CalidadGanadoInfo ObtenerPorDescripcion(string descripcion, Sexo sexo)
        {
            try
            {
                Logger.Info();
                var calidadGanadoBL = new CalidadGanadoBL();
                CalidadGanadoInfo result = calidadGanadoBL.ObtenerPorDescripcion(descripcion, sexo);
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
        ///    Obtiene una lista de CalidadGanado filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public List<CalidadGanadoInfo> ObtenerTodosCapturaCalidad(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var calidadGanadoBL = new CalidadGanadoBL();
                List<CalidadGanadoInfo> result = calidadGanadoBL.ObtenerTodosCapturaCalidad(estatus);
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

