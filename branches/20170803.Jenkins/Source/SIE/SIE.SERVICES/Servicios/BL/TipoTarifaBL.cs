
using System;
using System.Collections.Generic;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class TipoTarifaBL
    {
        /// <summary>
        /// Obtiene una lista de tipotarifainfo
        /// </summary>
        /// <returns></returns>
        internal List<TipoTarifaInfo> Obtenertodos()
        {
            try
            {
                var tipoTarifaDal = new TipoTarifaDAL();
                return tipoTarifaDal.ObtenerTodos();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }
    }
}
