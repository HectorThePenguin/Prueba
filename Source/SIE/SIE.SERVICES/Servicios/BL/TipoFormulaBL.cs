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
    internal class TipoFormulaBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad TipoFormula
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(TipoFormulaInfo info)
        {
            try
            {
                Logger.Info();
                var tipoFormulaDAL = new TipoFormulaDAL();
                int result = info.TipoFormulaID;
                if (info.TipoFormulaID == 0)
                {
                    result = tipoFormulaDAL.Crear(info);
                }
                else
                {
                    tipoFormulaDAL.Actualizar(info);
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
        internal ResultadoInfo<TipoFormulaInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoFormulaInfo filtro)
        {
            try
            {
                Logger.Info();
                var tipoFormulaDAL = new TipoFormulaDAL();
                ResultadoInfo<TipoFormulaInfo> result = tipoFormulaDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de TipoFormula
        /// </summary>
        /// <returns></returns>
        internal IList<TipoFormulaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var tipoFormulaDAL = new TipoFormulaDAL();
                IList<TipoFormulaInfo> result = tipoFormulaDAL.ObtenerTodos();
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
        internal IList<TipoFormulaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoFormulaDAL = new TipoFormulaDAL();
                IList<TipoFormulaInfo> result = tipoFormulaDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad TipoFormula por su Id
        /// </summary>
        /// <param name="tipoFormulaID">Obtiene una entidad TipoFormula por su Id</param>
        /// <returns></returns>
        internal TipoFormulaInfo ObtenerPorID(int tipoFormulaID)
        {
            try
            {
                Logger.Info();
                var tipoFormulaDAL = new TipoFormulaDAL();
                TipoFormulaInfo result = tipoFormulaDAL.ObtenerPorID(tipoFormulaID);
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
        /// Obtiene una entidad TipoFormula por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal TipoFormulaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tipoFormulaDAL = new TipoFormulaDAL();
                TipoFormulaInfo result = tipoFormulaDAL.ObtenerPorDescripcion(descripcion);
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

