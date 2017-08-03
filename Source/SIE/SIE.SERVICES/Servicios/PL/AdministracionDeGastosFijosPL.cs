using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class AdministracionDeGastosFijosPL
    {
        /// <summary>
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<AdministracionDeGastosFijosInfo> ObtenerPorPagina(PaginacionInfo pagina, AdministracionDeGastosFijosInfo filtro)
        {
            try
            {
                Logger.Info();
                var administracionDeGastosFijosBL = new AdministracionDeGastosFijosBL();
                ResultadoInfo<AdministracionDeGastosFijosInfo> result = administracionDeGastosFijosBL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una lista de gastos fijos de la embarcacion tarifa
        /// </summary>
        /// <returns></returns>
        public ResultadoInfo<AdministracionDeGastosFijosInfo> ObtenerTodos(TarifarioInfo filtro)
        {
            try
            {
                Logger.Info();
                var administracionDeGastosFijosBL = new AdministracionDeGastosFijosBL();
                ResultadoInfo<AdministracionDeGastosFijosInfo> result = administracionDeGastosFijosBL.ObtenerTodos(filtro);
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
        /// Crea o actualiza un gasto fijo
        /// </summary>
        /// <param name="gastos"></param>
        public void Guardar(AdministracionDeGastosFijosInfo gastos)
        {
            try
            {
                Logger.Info();
                var gastosFijosBL = new AdministracionDeGastosFijosBL();
                gastosFijosBL.Guardar(gastos);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Valida que la descripción del gasto fijo a registrar/editar no exista en la bd
        /// </summary>
        /// <param name="gastos"></param>
        /// <returns></returns>
        public List<AdministracionDeGastosFijosInfo> ValidarDescripcion(AdministracionDeGastosFijosInfo gastos)
        {
            List<AdministracionDeGastosFijosInfo> resultado;
            try
            {
                Logger.Info();
                var gastosFijosBL = new AdministracionDeGastosFijosBL();
                resultado = gastosFijosBL.ValidarDescripcion(gastos);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
    }
}
