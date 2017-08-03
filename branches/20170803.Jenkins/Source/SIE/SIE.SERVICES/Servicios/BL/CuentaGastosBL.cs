using System;
using System.Collections.Generic;
using SIE.Base.Infos;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    internal class CuentaGastosBL
    {
        internal void Guardar(CuentaGastosInfo info)
        {
            try
            {
                Logger.Info();
                var cuentaGastoDAL = new CuentaGastosDAL();
                if (info.CuentaGastoID != 0)
                {
                    cuentaGastoDAL.Actualizar(info);
                }
                else
                {
                    cuentaGastoDAL.Guardar(info);
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
        }

        internal ResultadoInfo<CuentaGastosInfo> ObtenerPorPagina(PaginacionInfo pagina, CuentaGastosInfo filtros)
        {
            try
            {
                Logger.Info();
                var cuentaGastosDAL = new CuentaGastosDAL();
                ResultadoInfo<CuentaGastosInfo> result = cuentaGastosDAL.ObtenerPorPagina(pagina, filtros);
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

        internal List<CuentaGastosInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var cuentaGastosDAL = new CuentaGastosDAL();
                List<CuentaGastosInfo> result = cuentaGastosDAL.ObtenerTodos();
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
