using System;
using System.Transactions;
using System.Collections.Generic;
using System.Data.SqlClient;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;
using SIE.Services.Integracion.DAL.Excepciones;
using System.Linq;

namespace SIE.Services.Servicios.BL
{
    internal class PrecioProductoBL
    {
        internal void Guardar(PrecioProductoInfo info)
        {
            try
            {
                Logger.Info();
                var precioProductoDAL = new PrecioProductoDAL();
                precioProductoDAL.Guardar(info);
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

        internal ResultadoInfo<PrecioProductoInfo> ObtenerPorPagina(PaginacionInfo pagina, PrecioProductoInfo filtros)
        {
            try
            {
                Logger.Info();
                var precioProductoDAL = new PrecioProductoDAL();
                ResultadoInfo<PrecioProductoInfo> result = precioProductoDAL.ObtenerPorPagina(pagina, filtros);
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
