using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    public abstract class CheckListRoladoraRangoAccessor : BLToolkit.DataAccess.DataAccessor<CheckListRoladoraRangoInfo>
    {
        [BLToolkit.DataAccess.SqlQuery(SqlText = "Update CheckListRoladoraRango Set FechaModificacion = GETDATE() Where CheckListRoladoraRangoID = @checkListRoladoraRangoID")]
        public abstract void ActualizarFechaModificacion(int checkListRoladoraRangoID);
    }
}
