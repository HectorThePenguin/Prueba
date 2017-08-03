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
    public class EstadoComederoPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad EstadoComedero
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(EstadoComederoInfo info)
        {
            try
            {
                Logger.Info();
                var estadoComederoBL = new EstadoComederoBL();
                int result = estadoComederoBL.Guardar(info);
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
        public ResultadoInfo<EstadoComederoInfo> ObtenerPorPagina(PaginacionInfo pagina, EstadoComederoInfo filtro)
        {
            try
            {
                Logger.Info();
                var estadoComederoBL = new EstadoComederoBL();
                ResultadoInfo<EstadoComederoInfo> result = estadoComederoBL.ObtenerPorPagina(pagina, filtro);
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
        public IList<EstadoComederoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var estadoComederoBL = new EstadoComederoBL();
                IList<EstadoComederoInfo> result = estadoComederoBL.ObtenerTodos();
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
        public IList<EstadoComederoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var estadoComederoBL = new EstadoComederoBL();
                IList<EstadoComederoInfo> result = estadoComederoBL.ObtenerTodos(estatus);
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
        /// <param name="estadoComederoID"></param>
        /// <returns></returns>
        public EstadoComederoInfo ObtenerPorID(int estadoComederoID)
        {
            try
            {
                Logger.Info();
                var estadoComederoBL = new EstadoComederoBL();
                EstadoComederoInfo result = estadoComederoBL.ObtenerPorID(estadoComederoID);
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
        public EstadoComederoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var estadoComederoBL = new EstadoComederoBL();
                EstadoComederoInfo result = estadoComederoBL.ObtenerPorDescripcion(descripcion);
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
        /// Funcion que retorna los kilogramos calculados con el ajuste base
        /// </summary>
        /// <param name="estadoComedero"></param>
        /// <param name="kilogramosProgramados"></param>
        /// <returns>Entero de los kilogramos calculados</returns>
        public int ObtenerKilogramosCalculados(EstadoComederoInfo estadoComedero, int kilogramosProgramados)
        {
            int kilogramosCalculados = 0;
            try
            {
                Logger.Info();
                if (estadoComedero.EstadoComederoID != (int)EstadoComederoEnum.NoServir && estadoComedero.EstadoComederoID != (int)EstadoComederoEnum.NoGenerar)
                {
                    if (estadoComedero.AjusteBase > 0)
                    {
                        kilogramosCalculados = Convert.ToInt32(kilogramosProgramados + (kilogramosProgramados * estadoComedero.AjusteBase / 100));
                    }
                    else
                    {
                        kilogramosCalculados = kilogramosProgramados;
                    }
                }
                else
                {
                    kilogramosCalculados = 0;
                }
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
            return kilogramosCalculados;
        }
    }
}

