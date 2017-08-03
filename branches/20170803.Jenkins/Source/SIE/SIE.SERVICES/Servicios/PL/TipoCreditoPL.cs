using System;
using System.Collections.Generic;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using System.Reflection;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class TipoCreditoPL
    {
        public ResultadoInfo<TipoCreditoInfo> TipoCredito_ObtenerPlazosCreditoPorFiltro(PaginacionInfo pagina, TipoCreditoInfo filtro)
        {
            try
            {
                Logger.Info();
                var bl = new TipoCreditoBL();
                ResultadoInfo<TipoCreditoInfo> result = bl.TipoCredito_ObtenerPlazosCreditoPorFiltro(pagina, filtro);
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

        public TipoCreditoInfo TipoCredito_ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var bl = new TipoCreditoBL();
                TipoCreditoInfo result = bl.TipoCredito_ObtenerPorDescripcion(descripcion);
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

        public List<TipoCreditoInfo> TipoCredito_ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var bl = new TipoCreditoBL();
                var result = bl.TipoCredito_ObtenerTodos();
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

        public int TipoCredito_Guardar(TipoCreditoInfo info)
        {
            try
            {
                Logger.Info();
                var bl = new TipoCreditoBL();
                int result = bl.TipoCredito_Guardar(info);
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

        public ConfiguracionCreditoInfo TipoCredito_ValidarConfiguracion(int tipoCredito)
        {
            try
            {
                Logger.Info();
                var bl = new TipoCreditoBL();
                var result = bl.TipoCredito_ValidarConfiguracion(tipoCredito);
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

