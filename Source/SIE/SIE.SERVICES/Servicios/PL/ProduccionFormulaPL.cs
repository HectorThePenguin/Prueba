using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ProduccionFormulaPL
    {
        /// <summary>
        /// Guarda la produccion de una formula, ademas graba el detalle
        /// </summary>
        /// <param name="produccionFormula"></param>
        /// <returns></returns>
        public ProduccionFormulaInfo GuardarProduccionFormula(ProduccionFormulaInfo produccionFormula)
        {
            ProduccionFormulaInfo retorno = null;

            try
            {
                Logger.Info();
                var produccionFormulaBl = new ProduccionFormulaBL();
                retorno = produccionFormulaBl.GuardarProduccionFormula(produccionFormula);
            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return retorno;
        }

        /// <summary>
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ProduccionFormulaInfo> ObtenerPorPagina(PaginacionInfo pagina, ProduccionFormulaInfo filtro)
        {
            try
            {
                Logger.Info();
                var produccionFormulaBL = new ProduccionFormulaBL();
                ResultadoInfo<ProduccionFormulaInfo> result = produccionFormulaBL.ObtenerPorPagina(pagina, filtro);
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
        /// <param name="produccionFormulaID"></param>
        /// <returns></returns>
        public ProduccionFormulaInfo ObtenerPorID(int produccionFormulaID)
        {
            try
            {
                Logger.Info();
                var produccionFormulaBL = new ProduccionFormulaBL();
                ProduccionFormulaInfo result = produccionFormulaBL.ObtenerPorID(produccionFormulaID);
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
        /// <param name="produccionFormula"></param>
        /// <returns></returns>
        public ProduccionFormulaInfo ObtenerPorFolioMovimiento(ProduccionFormulaInfo produccionFormula)
        {
            try
            {
                Logger.Info();
                var produccionFormulaBL = new ProduccionFormulaBL();
                ProduccionFormulaInfo result = produccionFormulaBL.ObtenerPorFolioMovimiento(produccionFormula);
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
        /// Obtiene una lista de produccion de formula
        /// </summary>
        /// <param name="organizacionID"> </param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        public List<ProduccionFormulaInfo> ObtenerProduccionFormulaConciliacion(int organizacionID, DateTime fechaInicio, DateTime fechaFinal)
        {
            try
            {
                Logger.Info();
                var produccionFormulaBL = new ProduccionFormulaBL();
                List<ProduccionFormulaInfo> result =
                    produccionFormulaBL.ObtenerProduccionFormulaConciliacion(organizacionID, fechaInicio, fechaFinal);
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
        /// Guarda la produccion de una formula, ademas graba el detalle
        /// </summary>
        /// <param name="produccionFormula"></param>
        /// <returns></returns>
        public ProduccionFormulaAutomaticaModel GuardarProduccionFormulaLista(List<ProduccionFormulaInfo> produccionFormula,DateTime fecha)
        {
            ProduccionFormulaAutomaticaModel retorno = null;

            try
            {
                Logger.Info();
                var produccionFormulaBl = new ProduccionFormulaBL();
                retorno = produccionFormulaBl.GuardarProduccionFormulaLista(produccionFormula,fecha);
            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return retorno;
        }

        /// <summary>
        /// Guarda la produccion de una formula, ademas graba el detalle
        /// </summary>
        /// <param name="produccionFormula"></param>
        /// <returns></returns>
        public List<ProduccionFormulaInfo> ResumenProduccionFormulaLista(List<ProduccionFormulaInfo> produccionFormula)
        {
            try
            {
                Logger.Info();
                var produccionFormulaBl = new ProduccionFormulaBL();
                produccionFormula = produccionFormulaBl.ResumenProduccionFormulaLista(produccionFormula);
            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return produccionFormula;
        }


    }
}
