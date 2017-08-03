using System.Collections.Generic;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    public abstract class SintomaAccessor : BLToolkit.DataAccess.DataAccessor<SintomaInfo>
    {
        //[BLToolkit.DataAccess.SqlQuery(SqlText = "Update Sintoma Set FechaModificacion = GETDATE() Where SintomaID = @sintomaID")]
        //public abstract void ActualizarFechaModificacion(int sintomaID);

        /// <summary>
        /// Obtiene una lista de sintomas por problema.
        /// </summary>
        /// <param name="problemaID"></param>
        /// <returns></returns>
        [BLToolkit.DataAccess.SprocName("DeteccionGanado_ObtenerSintomas")]
        public abstract List<SintomaInfo> ObtenerPorProblema(int problemaID);
    }
}
