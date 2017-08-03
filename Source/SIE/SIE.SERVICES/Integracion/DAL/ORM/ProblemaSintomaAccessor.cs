using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    public abstract class ProblemaSintomaAccessor : BLToolkit.DataAccess.DataAccessor<ProblemaSintomaInfo>
    {
        [BLToolkit.DataAccess.SqlQuery(SqlText = "Update ProblemaSintoma Set FechaModificacion = GETDATE() Where ProblemaSintomaID = @problemaSintomaID")]
        public abstract void ActualizarFechaModificacion(int problemaSintomaID);
    }
}
