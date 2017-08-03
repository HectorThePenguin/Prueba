
using System;
using System.Collections.Generic;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class TipoTarifaPL
    {
        /// <summary>
        /// Obtiene una lista de tipotarifainfo
        /// </summary>
        /// <returns></returns>
        public List<TipoTarifaInfo> ObtenerTodos()
        {
            try
            {
                var tipoTarifaBl = new TipoTarifaBL();
                return tipoTarifaBl.Obtenertodos();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }
    }
}
