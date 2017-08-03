using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class MezcladoraBL
    {
        /// <summary>
        /// ObtenerPorID
        /// </summary>
        /// <param name="mezcladoraInfo"></param>
        /// <returns></returns>
        internal MezcladoraInfo ObtenerPorID(MezcladoraInfo mezcladoraInfo)
        {
            MezcladoraInfo info;
            try
            {
                Logger.Info();
                var mezcladoraDAL = new MezcladoraDAL();
                info = mezcladoraDAL.ObtenerPorID(mezcladoraInfo);
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
        /// ObtenerPorPagina
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<MezcladoraInfo> ObtenerPorPagina(PaginacionInfo pagina, MezcladoraInfo filtro)
        {
            try
            {
                Logger.Info();
                var mezcladoraDAL = new MezcladoraDAL();
                return  mezcladoraDAL.ObtenerPorPagina(pagina, filtro);
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
        internal List<CalidadMezcladoFactorInfo> ObtenerCalidadMezcladoFactor()
        {
            try
            {
                Logger.Info();
                var mezcladoraDAL = new MezcladoraDAL();
                List<CalidadMezcladoFactorInfo> lista = mezcladoraDAL.ObtenerCalidadMezcladoFactor();
                List<CalidadMezcladoFactorInfo> listaCaculada = CalcularPesos(lista);
                return listaCaculada;

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
        /// CalcularPesos
        /// </summary>
        /// <param name="lista"></param>
        /// <returns></returns>
        internal List<CalidadMezcladoFactorInfo> CalcularPesos(List<CalidadMezcladoFactorInfo> lista)
        {
            int sumaPesoBH = 0;
            int sumaPesoBS = 0;
            decimal sumaMateriaSeca = 0;
            decimal sumaHumedad = 0;


            List<CalidadMezcladoFactorInfo> listaResultado = new List<CalidadMezcladoFactorInfo>( );
            foreach (var calidadMezcladoFactorInfo in lista)
            {
                if (calidadMezcladoFactorInfo.PesoBH != 0 && calidadMezcladoFactorInfo.Muestra != "Promedio")
                {

                    calidadMezcladoFactorInfo.MateriaSeca = ((decimal)calidadMezcladoFactorInfo.PesoBS /
                                                             (decimal)calidadMezcladoFactorInfo.PesoBH) * 100;
                    calidadMezcladoFactorInfo.Humedad = 100 - calidadMezcladoFactorInfo.MateriaSeca;

                    sumaPesoBH += calidadMezcladoFactorInfo.PesoBH;
                    sumaPesoBS += calidadMezcladoFactorInfo.PesoBS;
                    sumaMateriaSeca += calidadMezcladoFactorInfo.MateriaSeca;
                    sumaHumedad += calidadMezcladoFactorInfo.Humedad;
                    calidadMezcladoFactorInfo.Humedad = 100 - calidadMezcladoFactorInfo.MateriaSeca;
                    listaResultado.Add(calidadMezcladoFactorInfo);
                }

            }
            
            var promedios = new CalidadMezcladoFactorInfo
            {
                Muestra = "Promedio",
                PesoBH = sumaPesoBH/3,
                PesoBS = sumaPesoBS/3,
                MateriaSeca = sumaMateriaSeca/3,
                Humedad = sumaHumedad/3,
                PesoBHHabilitado = true,
                PesoSHHabilitado = true


            };
            listaResultado.Add(promedios);
            return listaResultado;
        }
        /// <summary>
        /// GuardarConsultaFactor
        /// </summary>
        /// <param name="listaCalidadMezcladoInfos"></param>
        internal void GuardarConsultaFactor(List<CalidadMezcladoFactorInfo> listaCalidadMezcladoInfos)
        {
            try
            {
                Logger.Info();
                var mezcladoraDAL = new MezcladoraDAL();
   
                 mezcladoraDAL.GuardarConsultaFactor(listaCalidadMezcladoInfos);
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
        internal CalidadMezcladoFormulasAlimentoInfo ObtenerCalidadMezcladoFormulaAlimento(CalidadMezcladoFormulasAlimentoInfo calidadMezcladoFormulaAlimentoInfo)
        {
            CalidadMezcladoFormulasAlimentoInfo regreso;
            try
            {
                Logger.Info();
                var mezcladoraDAL = new MezcladoraDAL();

                regreso = mezcladoraDAL.ObtenerCalidadMezcladoFormulaAlimento(calidadMezcladoFormulaAlimentoInfo);
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
