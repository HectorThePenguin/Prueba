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
    internal class FormulaBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Formula
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(FormulaInfo info)
        {
            try
            {
                Logger.Info();
                var formulaDAL = new FormulaDAL();
                int result = info.FormulaId;
                if (info.FormulaId == 0)
                {
                    result = formulaDAL.Crear(info);
                }
                else
                {
                    formulaDAL.Actualizar(info);
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
        internal ResultadoInfo<FormulaInfo> ObtenerPorPagina(PaginacionInfo pagina, FormulaInfo filtro)
        {
            try
            {
                Logger.Info();
                var formulaDAL = new FormulaDAL();
                ResultadoInfo<FormulaInfo> result = formulaDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de Formula
        /// </summary>
        /// <returns></returns>
        internal IList<FormulaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var formulaDAL = new FormulaDAL();
                IList<FormulaInfo> result = formulaDAL.ObtenerTodos();
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
        internal IList<FormulaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var formulaDAL = new FormulaDAL();
                IList<FormulaInfo> result = formulaDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad Formula por su Id
        /// </summary>
        /// <param name="formulaID">Obtiene una entidad Formula por su Id</param>
        /// <returns></returns>
        internal FormulaInfo ObtenerPorID(int formulaID)
        {
            try
            {
                Logger.Info();
                var formulaDAL = new FormulaDAL();
                FormulaInfo result = formulaDAL.ObtenerPorID(formulaID);
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
        /// Obtiene una entidad Formula por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal FormulaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var formulaDAL = new FormulaDAL();
                FormulaInfo result = formulaDAL.ObtenerPorDescripcion(descripcion);
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
        /// 
        /// </summary>
        /// <param name="tipoFormulaId"></param>
        /// <returns></returns>
        internal IList<FormulaInfo> ObtenerPorTipoFormulaId(int tipoFormulaId)
        {
            IList<FormulaInfo> formulaInfo;
            try
            {
                Logger.Info();
                var formulaDAL = new FormulaDAL();
                formulaInfo = formulaDAL.ObtenerPorFormulaId(tipoFormulaId);
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
            return formulaInfo;
        }

        internal List<FormulaInfo> ObtenerFormulaDescripcionPorIDs(List<int> formulasID)
        {
            List<FormulaInfo> formulaInfo;
            try
            {
                Logger.Info();
                var formulaDAL = new FormulaDAL();
                formulaInfo = formulaDAL.ObtenerFormulaDescripcionPorIDs(formulasID);
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
            return formulaInfo;
        }

        /// <summary>
        /// Obtiene la formula por su clave para
        /// opcion de Calidad pase a proceso
        /// </summary>
        /// <param name="formulaID"></param>
        /// <returns></returns>
        internal FormulaInfo ObtenerPorFormulaIDCalidadPaseProceso(int formulaID)
        {
            try
            {
                Logger.Info();
                var formulaDAL = new FormulaDAL();
                FormulaInfo result = formulaDAL.ObtenerPorFormulaIDCalidadPaseProceso(formulaID);
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

        internal ResultadoInfo<FormulaInfo> ObtenerPorPaseCalidadPaginado(PaginacionInfo pagina, FormulaInfo formula)
        {
            try
            {
                Logger.Info();
                var formulaDAL = new FormulaDAL();
                ResultadoInfo<FormulaInfo> result = formulaDAL.ObtenerPorPaseCalidadPaginado(pagina, formula);
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
        /// Obtiene un lista de Formula
        /// </summary>
        /// <returns></returns>
        internal IList<FormulaInfo> ObtenerFormulasConfiguradas(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var formulaDAL = new FormulaDAL();
                IList<FormulaInfo> result = formulaDAL.ObtenerFormulasConfiguradas(estatus);
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
        ///<summary>
        /// Obtiene una lista de la tabla RotoMix para cargar el commobox del mismo nombre "RotoMix"
        /// </summary>
        /// <returns></returns>
        internal IList<RotoMixInfo> ObtenerRotoMixConfiguradas(int organizacionId)
        {
            try
            {
                Logger.Info();
                var formulaDAL = new FormulaDAL();
                IList<RotoMixInfo> result = formulaDAL.ObtenerRotoMixConfiguradas(organizacionId);
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
        ///<summary>
        /// Obtiene el número de batch que deberá mostrarse en el texbox "txtBatch"
        /// Este dato se inicializado en 1, por rotomix y por día.
        /// </summary>
        /// <returns></returns>
        internal int CantidadBatch(int organizacionId, int rotoMix)
        {
            try
            {
                Logger.Info();
                var formulaDAL = new FormulaDAL();
                int result = formulaDAL.CantidadBatch(organizacionId, rotoMix);
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
