using System;
using System.Collections.Generic;
using SIE.Base.Infos;
using SIE.Services.Info.Info;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using System.Reflection;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class CuentaGastosPL
    {
        public void Guardar(CuentaGastosInfo info)
        {
            try
            {
                Logger.Info();
                var cuentaGastosBL = new CuentaGastosBL();
                cuentaGastosBL.Guardar(info);
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
        /// Obtener por pagina
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public ResultadoInfo<CuentaGastosInfo> ObtenerPorPagina(PaginacionInfo pagina, CuentaGastosInfo filtros)
        {
            try
            {
                Logger.Info();
                var cuentaGastosBL = new CuentaGastosBL();
                ResultadoInfo<CuentaGastosInfo> result = cuentaGastosBL.ObtenerPorPagina(pagina, filtros);

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

        public List<CuentaGastosInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var cuentaGastosBL = new CuentaGastosBL();
                List<CuentaGastosInfo> result = cuentaGastosBL.ObtenerTodos();

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
