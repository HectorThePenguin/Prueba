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
    public class CausaSalidaPL
    {

        public CausaSalidaInfo ObtenerPorTipoMovimiento(int tipoMovimientoEnum)
        {
            try
            {
                Logger.Info();
                var causaSalida = new CausaSalidaBL();
                CausaSalidaInfo result = causaSalida.ObtenerPorTipoMovimiento(tipoMovimientoEnum);
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

        public List<CausaSalidaInfo> ObtenerPorTipoMovimientoLista(int tipoMovimientoEnum)
        {
            try
            {
                Logger.Info();
                var causaSalida = new CausaSalidaBL();
                List<CausaSalidaInfo> result = causaSalida.ObtenerPorTipoMovimientoLista(tipoMovimientoEnum);
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
        /// Metodo para Guardar/Modificar una entidad CausaSalida
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(CausaSalidaInfo info)
        {
            try
            {
                Logger.Info();
                var causaSalidaBL = new CausaSalidaBL();
                int result = causaSalidaBL.Guardar(info);
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
        public ResultadoInfo<CausaSalidaInfo> ObtenerPorPagina(PaginacionInfo pagina, CausaSalidaInfo filtro)
        {
            try
            {
                Logger.Info();
                var causaSalidaBL = new CausaSalidaBL();
                ResultadoInfo<CausaSalidaInfo> result = causaSalidaBL.ObtenerPorPagina(pagina, filtro);
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
        public IList<CausaSalidaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var causaSalidaBL = new CausaSalidaBL();
                IList<CausaSalidaInfo> result = causaSalidaBL.ObtenerTodos();
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
        public IList<CausaSalidaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var causaSalidaBL = new CausaSalidaBL();
                IList<CausaSalidaInfo> result = causaSalidaBL.ObtenerTodos(estatus);
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
        /// <param name="causaSalidaID"></param>
        /// <returns></returns>
        public CausaSalidaInfo ObtenerPorID(int causaSalidaID)
        {
            try
            {
                Logger.Info();
                var causaSalidaBL = new CausaSalidaBL();
                CausaSalidaInfo result = causaSalidaBL.ObtenerPorID(causaSalidaID);
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
        public CausaSalidaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var causaSalidaBL = new CausaSalidaBL();
                CausaSalidaInfo result = causaSalidaBL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene una Causa Salida por su Descripción y Tipo de Movimiento
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public CausaSalidaInfo ObtenerPorTipoMovimientoDescripcion(CausaSalidaInfo filtro)
        {
            try
            {
                Logger.Info();
                var causaSalidaBL = new CausaSalidaBL();
                CausaSalidaInfo result = causaSalidaBL.ObtenerPorTipoMovimientoDescripcion(filtro);
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
