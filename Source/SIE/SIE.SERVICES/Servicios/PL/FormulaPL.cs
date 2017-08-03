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
    public class FormulaPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Formula
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(FormulaInfo info)
        {
            try
            {
                Logger.Info();
                var formulaBL = new FormulaBL();
                int result = formulaBL.Guardar(info);
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
        public ResultadoInfo<FormulaInfo> ObtenerPorPagina(PaginacionInfo pagina, FormulaInfo filtro)
        {
            try
            {
                Logger.Info();
                var formulaBL = new FormulaBL();
                ResultadoInfo<FormulaInfo> result = formulaBL.ObtenerPorPagina(pagina, filtro);
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

        public FormulaInfo ObtenerActivoPorId(FormulaInfo filtro)
        {
            ResultadoInfo<FormulaInfo> listaFormulasInfo;
            FormulaInfo formulaInfo = null;
            try
            {
                Logger.Info();
                PaginacionInfo pagina = new PaginacionInfo();
                pagina.Inicio = 1;
                pagina.Limite = 1;
                var formulaBl = new FormulaBL();
                listaFormulasInfo = formulaBl.ObtenerPorPagina(pagina, filtro);
                if (listaFormulasInfo != null)
                {
                    formulaInfo = listaFormulasInfo.Lista[0];
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
            return formulaInfo;
        }

        /// <summary>
        /// Obtiene una lista
        /// </summary>
        /// <returns></returns>
        public IList<FormulaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var formulaBL = new FormulaBL();
                IList<FormulaInfo> result = formulaBL.ObtenerTodos();
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
        public IList<FormulaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var formulaBL = new FormulaBL();
                IList<FormulaInfo> result = formulaBL.ObtenerTodos(estatus);
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
        /// <param name="formulaID"></param>
        /// <returns></returns>
        public FormulaInfo ObtenerPorID(int formulaID)
        {
            try
            {
                Logger.Info();
                var formulaBL = new FormulaBL();
                FormulaInfo result = formulaBL.ObtenerPorID(formulaID);
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
        public FormulaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var formulaBL = new FormulaBL();
                FormulaInfo result = formulaBL.ObtenerPorDescripcion(descripcion);
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
        public IList<FormulaInfo> ObtenerPorTipoFormulaID(int tipoFormulaId)
        {
            IList<FormulaInfo> formulaInfo;
            try
            {
                Logger.Info();
                var formulaBl = new FormulaBL();
                formulaInfo = formulaBl.ObtenerPorTipoFormulaId(tipoFormulaId);
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
        public FormulaInfo ObtenerPorFormulaIDCalidadPaseProceso(int formulaID)
        {
            try
            {
                Logger.Info();
                var formulaBL = new FormulaBL();
                FormulaInfo result = formulaBL.ObtenerPorFormulaIDCalidadPaseProceso(formulaID);
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

        public ResultadoInfo<FormulaInfo> ObtenerPorPaseCalidadPaginado(PaginacionInfo pagina, FormulaInfo formula)
        {
            try
            {
                Logger.Info();
                var formulaBL = new FormulaBL();
                ResultadoInfo<FormulaInfo> result = formulaBL.ObtenerPorPaseCalidadPaginado(pagina, formula);
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
        public IList<FormulaInfo> ObtenerFormulasConfiguradas(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var formulaBL = new FormulaBL();
                IList<FormulaInfo> result = formulaBL.ObtenerFormulasConfiguradas(estatus);
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
        /// Obtiene una lista de la tabla RotoMix para cargar el commobox del mismo nombre "RotoMix"
        /// </summary>
        /// <returns></returns>
        public IList<RotoMixInfo> ObtenerRotoMixConfiguradas(int organizacionId)
        {
            try
            {
                Logger.Info();
                var formulaBL = new FormulaBL();
                IList<RotoMixInfo> result = formulaBL.ObtenerRotoMixConfiguradas(organizacionId);
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
        /// Obtiene el número de batch que deberá mostrarse en el texbox "txtBatch"
        /// Este dato se inicializado en 1, por rotomix y por día.
        /// </summary>
        /// <returns></returns>
        public int CantidadBatch (int organizacionId, int rotoMix)
        {
            try
            {
                Logger.Info();
                var formulaBL = new FormulaBL();
                int result = formulaBL.CantidadBatch (organizacionId, rotoMix);
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
