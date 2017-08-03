using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ListaPreciosCentrosPL
    {
        public List<ListaPreciosCentrosInfo> ObtenerListaPreciosCentros(ListaPreciosCentrosInfo info)
        {
            List<ListaPreciosCentrosInfo> precios;
            try
            {
                Logger.Info();
                var bl = new ListaPreciosCentrosBL();
                precios = bl.ObtenerListaPreciosCentros(info);
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
            return precios;
        }

        public bool Guardar(List<ListaPreciosCentrosInfo> precios, int usuarioId)
        {
            var result = false;

            try
            {
                var bl = new ListaPreciosCentrosBL();
                result = bl.Guardar(precios, usuarioId);
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
    }
}
