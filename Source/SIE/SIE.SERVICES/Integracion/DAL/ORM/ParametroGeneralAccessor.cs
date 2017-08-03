using System.Collections.Generic;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    public abstract class ParametroGeneralAccessor : BLToolkit.DataAccess.DataAccessor<ParametroGeneralInfo>
    {
        [BLToolkit.DataAccess.SqlQuery(SqlText = "Update ParametroGeneral Set FechaModificacion = GETDATE() Where ParametroGeneralID = @parametroGeneralID)")]
        public abstract void ActualizarFechaModificacion(int parametroGeneralID);

        /// <summary>
        /// Obtiene el Parametro General por Clave
        /// </summary>
        /// <param name="clave"></param>
        /// <returns></returns>
        [BLToolkit.DataAccess.SprocName("ParametroGeneral_ObtenerPorClave")]
        public abstract ParametroGeneralInfo ObtenerParametroPorClave(string clave);
    }
}
