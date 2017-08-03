using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Base.Infos;

namespace SIE.Services.Servicios.PL
{
    public class PagoTransferenciaPL
    {
        public int GuardarPago(PagoTransferenciaInfo pago)
        {
            try
            {
                Logger.Info();
                var pagoBL = new PagoTransferenciaBL();
                return pagoBL.Guardar(pago);
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

        public ResultadoInfo<PagoTransferenciaInfo> ObtenerPorPagina(PaginacionInfo pagina, PagoTransferenciaInfo filtro, int centro, int folio)
        {
            var result = new ResultadoInfo<PagoTransferenciaInfo>();
            try
            {
                Logger.Info();
                var pagoBL = new PagoTransferenciaBL();
                result = pagoBL.ObtenerPorPagina(pagina, filtro, centro, folio);
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
