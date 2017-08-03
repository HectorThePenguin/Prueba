using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Base.Infos;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class AutoFacturacionBL
    {
        internal ResultadoInfo<AutoFacturacionInfo> ObtenerAutoFacturacion(PaginacionInfo pagina, AutoFacturacionInfo info)
        {
            ResultadoInfo<AutoFacturacionInfo> lista;
            try
            {
                Logger.Info();
                var dal = new AutoFacturacionDAL();
                lista = dal.ObtenerAutoFacturacion(pagina, info);
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
            return lista;
        }

        internal int Guardar(AutoFacturacionInfo info)
        {
            var result = 0;
            try
            {
                Logger.Info();
                var dal = new AutoFacturacionDAL();
                result = dal.Guardar(info);
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

        internal List<AutoFacturacionInfo> ObtenerImagenes(AutoFacturacionInfo info)
        {
            try
            {
                Logger.Info();
                var dal = new AutoFacturacionDAL();
                return dal.ObtenerImagenes(info);
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
