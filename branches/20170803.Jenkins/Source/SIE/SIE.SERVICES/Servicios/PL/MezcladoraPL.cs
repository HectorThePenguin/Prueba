using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class MezcladoraPL
    {
        /// <summary>
        ///      Obtiene camion reparto por su Id
        /// </summary>
        /// <returns> </returns>
        public MezcladoraInfo ObtenerPorID(MezcladoraInfo mezcladoraInfo)
        {
            MezcladoraInfo info;
            // var  organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            try
            {
                Logger.Info();
                var mezcladoraBl = new MezcladoraBL();
                info = mezcladoraBl.ObtenerPorID(mezcladoraInfo);
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
            return info;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<MezcladoraInfo> ObtenerPorPagina(PaginacionInfo pagina, MezcladoraInfo filtro)
        {
            try
            {
                Logger.Info();
                var mezcladoraBl = new MezcladoraBL();
                return mezcladoraBl.ObtenerPorPagina(pagina, filtro);
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
        /// ObtenerCalidadMezcladoFactor
        /// </summary>
        /// <returns></returns>
        public List<CalidadMezcladoFactorInfo> ObtenerCalidadMezcladoFactor()
        {
            try
            {
                Logger.Info();
                var mezcladoraBl = new MezcladoraBL();
                return mezcladoraBl.ObtenerCalidadMezcladoFactor();
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
        /// Calcula datos
        /// </summary>
        /// <param name="listaCalidadMezcladoInfos"></param>
        /// <returns></returns>
        public List<CalidadMezcladoFactorInfo> CalcularPesos(List<CalidadMezcladoFactorInfo> listaCalidadMezcladoInfos)
        {
            try
            {
                Logger.Info();
                var mezcladoraBl = new MezcladoraBL();
                return mezcladoraBl.CalcularPesos(listaCalidadMezcladoInfos);
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
        /// Consulta factor
        /// </summary>
        /// <param name="listaCalidadMezcladoInfos"></param>
        public void GuardarConsultaFactor(List<CalidadMezcladoFactorInfo> listaCalidadMezcladoInfos)
        {
            try
            {
                Logger.Info();
                var mezcladoraBl = new MezcladoraBL();
                mezcladoraBl.GuardarConsultaFactor(listaCalidadMezcladoInfos);
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
        /// ObtenerCalidadMezcladoFormulaAlimento
        /// </summary>
        /// <param name="calidadMezcladoFormulaAlimentoInfo"></param>
        /// <returns></returns>
        public CalidadMezcladoFormulasAlimentoInfo ObtenerCalidadMezcladoFormulaAlimento(CalidadMezcladoFormulasAlimentoInfo calidadMezcladoFormulaAlimentoInfo)
        {
            CalidadMezcladoFormulasAlimentoInfo regreso;
            try
            {
                Logger.Info();
                var mezcladoraBl = new MezcladoraBL();
                regreso = mezcladoraBl.ObtenerCalidadMezcladoFormulaAlimento(calidadMezcladoFormulaAlimentoInfo);
                return regreso;
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
