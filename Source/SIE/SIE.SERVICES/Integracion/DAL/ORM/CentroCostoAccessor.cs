using System.Collections.Generic;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    public abstract class CentroCostoAccessor : BLToolkit.DataAccess.DataAccessor<CentroCostoInfo>
    {
        /// <summary>
        /// Obtiene una lista de centros costo por autorizador
        /// </summary>
        /// <param name="centroCostoID">Identificador del centro costo</param>
        /// <param name="autorizadorID">Identificador del autorizador del centro costo</param>
        /// <returns></returns>
        [BLToolkit.DataAccess.SprocName("CentroCosto_ObtenerPorAutorizador")]
        public abstract IList<CentroCostoInfo> ObtenerPorAutorizador(int centroCostoID, int autorizadorID);
    }
}
