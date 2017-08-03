using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    public class AdministracionDeGastosFijosBL
    {

        /// <summary>
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<AdministracionDeGastosFijosInfo> ObtenerPorPagina(PaginacionInfo pagina, AdministracionDeGastosFijosInfo filtro)
        {
            try
            {
                Logger.Info();
                var administracionDeGastosFijosDAL = new AdministracionDeGastosFijosDAL();
                ResultadoInfo<AdministracionDeGastosFijosInfo> result = administracionDeGastosFijosDAL.ObtenerPorPagina(pagina, filtro);
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
        internal ResultadoInfo<AdministracionDeGastosFijosInfo> ObtenerTodos(TarifarioInfo filtro)
        {
            try
            {
                Logger.Info();
                var administracionDeGastosFijosDAL = new AdministracionDeGastosFijosDAL();
                ResultadoInfo<AdministracionDeGastosFijosInfo> result = administracionDeGastosFijosDAL.ObtenerTodos(filtro);
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
        internal void Guardar(AdministracionDeGastosFijosInfo gastos)
        {
            try
            {
                Logger.Info();
                var gastosFijosDAL = new AdministracionDeGastosFijosDAL();
                if (gastos.GastoFijoID != 0)
                {
                    gastosFijosDAL.ActualizarGastoFijo(gastos);
                }
                else
                {
                    gastosFijosDAL.CrearGastoFijo(gastos);
                }
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
        internal List<AdministracionDeGastosFijosInfo> ValidarDescripcion(AdministracionDeGastosFijosInfo gastos)
        {
            List<AdministracionDeGastosFijosInfo> resultado;
            try
            {
                var gastosFijosDAL = new AdministracionDeGastosFijosDAL();

                resultado = gastosFijosDAL.ValidarDescripcion(gastos);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw  new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
    }
}
