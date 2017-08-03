using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Base.Infos;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using System.Collections.Generic;

namespace SIE.Services.Servicios.PL
{
    public class AutoFacturacionPL
    {
        public ResultadoInfo<AutoFacturacionInfo> ObtenerPorFiltro(PaginacionInfo pagina, AutoFacturacionInfo info)
        {
            try
            {
                Logger.Info();
                var autoFacturacionBL = new AutoFacturacionBL();
                var lista = autoFacturacionBL.ObtenerAutoFacturacion(pagina, info);
                return lista;
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

        public int Guardar(AutoFacturacionInfo info)
        {
            try
            {
                Logger.Info();
                var bl = new AutoFacturacionBL();
                var result = bl.Guardar(info);
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

        public List<AutoFacturacionInfo> ObtenerImagenes(AutoFacturacionInfo info)
        {
            try
            {
                Logger.Info();
                var bl = new AutoFacturacionBL();
                return bl.ObtenerImagenes(info);
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
