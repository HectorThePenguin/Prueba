using System;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    public abstract class ProblemaTratamientoAccessor : BLToolkit.DataAccess.DataAccessor<ProblemaTratamientoInfo>
    {
        [BLToolkit.DataAccess.SqlQuery(SqlText = "Update ProblemaTratamiento Set FechaModificacion = GETDATE() Where ProblemaTratamientoID = @problemaTratamientoID")]
        public abstract void ActualizarFechaModificacion(int problemaTratamientoID);
    }
}
