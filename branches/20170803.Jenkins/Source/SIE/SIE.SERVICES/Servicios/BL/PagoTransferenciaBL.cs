using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class PagoTransferenciaBL
    {
        internal int Guardar(PagoTransferenciaInfo pago)
        {            
            try
            {
                Logger.Info();
                var pagoDAL = new PagoTransferenciaDAL();
                return pagoDAL.Guardar(pago);
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

        internal ResultadoInfo<PagoTransferenciaInfo> ObtenerPorPagina(PaginacionInfo pagina, PagoTransferenciaInfo filtro, int centro, int folio)
        {
            ResultadoInfo<PagoTransferenciaInfo> result;
            try
            {
                Logger.Info();
                var pagoDAL = new PagoTransferenciaDAL();
                result = pagoDAL.ObtenerPorPagina(pagina, filtro, centro, folio);
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
            return result;
        }
    }
}
