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
    internal class TipoCreditoBL
    {
        internal ResultadoInfo<TipoCreditoInfo> TipoCredito_ObtenerPlazosCreditoPorFiltro(PaginacionInfo pagina, TipoCreditoInfo filtro)
        {
            try
            {
                Logger.Info();
                var dal = new TipoCreditoDAL();
                ResultadoInfo<TipoCreditoInfo> result = dal.TipoCredito_ObtenerTiposCreditoPorFiltro(pagina, filtro);
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

        internal TipoCreditoInfo TipoCredito_ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var dal = new TipoCreditoDAL();
                TipoCreditoInfo result = dal.TipoCredito_ObtenerPorDescripcion(descripcion);
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

        internal int TipoCredito_Guardar(TipoCreditoInfo info)
        {
            try
            {
                Logger.Info();
                var dal = new TipoCreditoDAL();
                int result = info.TipoCreditoID;
                if (info.TipoCreditoID == 0)
                {
                    result = dal.TipoCredito_Crear(info);
                }
                else
                {
                    dal.TipoCredito_Actualizar(info);
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

        internal List<TipoCreditoInfo> TipoCredito_ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var dal = new TipoCreditoDAL();
                var result = dal.TipoCredito_ObtenerTodos();
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

        internal ConfiguracionCreditoInfo TipoCredito_ValidarConfiguracion(int tipoCredito)
        {
            try
            {
                Logger.Info();
                var dal = new TipoCreditoDAL();
                var result = dal.TipoCredito_ValidarConfiguracion(tipoCredito);
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
