using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Base.Infos;
using SIE.Base.Interfaces;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using System.Reflection;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class PrecioProductoPL
    {
        public void Guardar(PrecioProductoInfo info)
        {
            try
            {
                Logger.Info();
                var precioProductoBL = new PrecioProductoBL();
                precioProductoBL.Guardar(info);
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
        public ResultadoInfo<PrecioProductoInfo> ObtenerPorPagina(PaginacionInfo pagina, PrecioProductoInfo filtros)
        {
            try
            {
                Logger.Info();
                var precioProductoBL = new PrecioProductoBL();
                ResultadoInfo<PrecioProductoInfo> result = precioProductoBL.ObtenerPorPagina(pagina, filtros);

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
