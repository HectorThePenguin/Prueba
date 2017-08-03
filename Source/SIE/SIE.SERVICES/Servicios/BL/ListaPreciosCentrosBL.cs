using System;
using System.Collections.Generic;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    internal class ListaPreciosCentrosBL
    {
        internal List<ListaPreciosCentrosInfo> ObtenerListaPreciosCentros(ListaPreciosCentrosInfo info)
        {
            List<ListaPreciosCentrosInfo> result;
            try
            {
                Logger.Info();
                var dal = new ListaPreciosCentrosDAL();
                result = dal.ObtenerListaPreciosCentros(info);
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

        internal bool Guardar(List<ListaPreciosCentrosInfo> precios, int usuarioId)
        {
            var result = false;

            try
            {
                var dal = new ListaPreciosCentrosDAL();
                result = dal.Guardar(precios, usuarioId);
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
    }
}
