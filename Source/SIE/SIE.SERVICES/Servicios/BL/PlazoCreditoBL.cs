using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    internal class PlazoCreditoBL
    {
        internal ResultadoInfo<PlazoCreditoInfo> PlazoCredito_ObtenerPlazosCreditoPorFiltro(PaginacionInfo pagina, PlazoCreditoInfo filtro)
        {
            try
            {
                Logger.Info();
                var dal = new PlazoCreditoDAL();
                ResultadoInfo<PlazoCreditoInfo> result = dal.PlazoCredito_ObtenerPlazosCreditoPorFiltro(pagina, filtro);
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

        internal List<PlazoCreditoInfo> PlazoCredito_ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var dal = new PlazoCreditoDAL();
                var result = dal.PlazoCredito_ObtenerTodos();
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

        internal PlazoCreditoInfo PlazoCredito_ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var dal = new PlazoCreditoDAL();
                PlazoCreditoInfo result = dal.PlazoCredito_ObtenerPorDescripcion(descripcion);
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

        internal int PlazoCredito_Guardar(PlazoCreditoInfo info)
        {
            try
            {
                Logger.Info();
                var dal = new PlazoCreditoDAL();
                int result = info.PlazoCreditoID;
                if (info.PlazoCreditoID == 0)
                {
                    result = dal.PlazoCredito_Crear(info);
                }
                else
                {
                    dal.PlazoCredito_Actualizar(info);
                }
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

        internal ConfiguracionCreditoInfo PlazoCredito_ValidarConfiguracion(int plazoCredito)
        {
            try
            {
                Logger.Info();
                var dal = new PlazoCreditoDAL();
                var result = dal.PlazoCredito_ValidarConfiguracion(plazoCredito);
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
