using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    public abstract class CheckListRoladoraAccessor : BLToolkit.DataAccess.DataAccessor<CheckListRoladoraInfo>
    {
        [BLToolkit.DataAccess.SqlQuery(SqlText = "Update CheckListRoladora Set FechaModificacion = GETDATE() Where CheckListRoladoraID = @checkListRoladoraID")]
        public abstract void ActualizarFechaModificacion(int checkListRoladoraID);
    }
}
