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
    public class PlazoCreditoPL
    {
        public ResultadoInfo<PlazoCreditoInfo> PlazoCredito_ObtenerPlazosCreditoPorFiltro(PaginacionInfo pagina, PlazoCreditoInfo filtro)
        {
            try
            {
                Logger.Info();
                var bl = new PlazoCreditoBL();
                ResultadoInfo<PlazoCreditoInfo> result = bl.PlazoCredito_ObtenerPlazosCreditoPorFiltro(pagina, filtro);
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

        public List<PlazoCreditoInfo> PlazoCredito_ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var bl = new PlazoCreditoBL();
                var result = bl.PlazoCredito_ObtenerTodos();
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

        public PlazoCreditoInfo PlazoCredito_ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var bl = new PlazoCreditoBL();
                PlazoCreditoInfo result = bl.PlazoCredito_ObtenerPorDescripcion(descripcion);
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

        public int PlazoCredito_Guardar(PlazoCreditoInfo info)
        {
            try
            {
                Logger.Info();
                var bl = new PlazoCreditoBL();
                int result = bl.PlazoCredito_Guardar(info);
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

        public ConfiguracionCreditoInfo PlazoCredito_ValidarConfiguracion(int plazoCredito)
        {
            try
            {
                Logger.Info();
                var bl = new PlazoCreditoBL();
                var result = bl.PlazoCredito_ValidarConfiguracion(plazoCredito);
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
