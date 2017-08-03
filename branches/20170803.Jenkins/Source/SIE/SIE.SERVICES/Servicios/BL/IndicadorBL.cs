using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class IndicadorBL
    {
        /// <summary>
        /// Método que obtiene el indicador por su identificador
        /// </summary>
        /// <param name="indicadorInfo"></param>
        /// <returns></returns>
        public IndicadorInfo ObtenerPorId(IndicadorInfo indicadorInfo)
        {
            try
            {
                Logger.Info();
                var indicadorDAL = new IndicadorDAL();
                IndicadorInfo result = indicadorDAL.ObtenerPorId(indicadorInfo);
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
        /// Método que obtiene el indicador por contratoID
        /// </summary>
        /// <param name="indicadorInfo"></param>
        /// <returns></returns>
        public IndicadorInfo ObtenerPorContratoID(IndicadorInfo indicadorInfo)
        {
            try
            {
                Logger.Info();
                var indicadorDAL = new IndicadorDAL();
                IndicadorInfo result = indicadorDAL.ObtenerPorContratoID(indicadorInfo);
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
        /// Metodo para Guardar/Modificar una entidad Indicador
        /// </summary>
        /// <param name="info"></param>
        public int Guardar(IndicadorInfo info)
        {
            try
            {
                Logger.Info();
                var indicadorDAL = new IndicadorDAL();
                int result = info.IndicadorId;
                if (info.IndicadorId == 0)
                {
                    result = indicadorDAL.Crear(info);
                }
                else
                {
                    indicadorDAL.Actualizar(info);
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
        public ResultadoInfo<IndicadorInfo> ObtenerPorPagina(PaginacionInfo pagina, IndicadorInfo filtro)
        {
            try
            {
                Logger.Info();
                var indicadorDAL = new IndicadorDAL();
                ResultadoInfo<IndicadorInfo> result = indicadorDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de Indicador
        /// </summary>
        /// <returns></returns>
        public IList<IndicadorInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var indicadorDAL = new IndicadorDAL();
                IList<IndicadorInfo> result = indicadorDAL.ObtenerTodos();
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
        public IList<IndicadorInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var indicadorDAL = new IndicadorDAL();
                IList<IndicadorInfo> result = indicadorDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad Indicador por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public IndicadorInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var indicadorDAL = new IndicadorDAL();
                IndicadorInfo result = indicadorDAL.ObtenerPorDescripcion(descripcion);
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
