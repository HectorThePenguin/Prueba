using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    public abstract class CheckListRoladoraGeneralAccessor : BLToolkit.DataAccess.DataAccessor<CheckListRoladoraGeneralInfo>
    {
        [BLToolkit.DataAccess.SqlQuery(SqlText = "Update CheckListRoladoraGeneral Set FechaModificacion = GETDATE() Where CheckListRoladoraGeneralID = @checkListRoladoraGeneralID")]
        public abstract void ActualizarFechaModificacion(int checkListRoladoraGeneralID);
    }
}
