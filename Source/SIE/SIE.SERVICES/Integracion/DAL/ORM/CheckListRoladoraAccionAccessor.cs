using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    public abstract class CheckListRoladoraAccionAccessor : BLToolkit.DataAccess.DataAccessor<CheckListRoladoraAccionInfo>
    {
        [BLToolkit.DataAccess.SqlQuery(SqlText = "Update CheckListRoladoraAccion Set FechaModificacion = GETDATE() Where CheckListRoladoraAccionID = @checkListRoladoraAccionID")]
        public abstract void ActualizarFechaModificacion(int? checkListRoladoraAccionID);
    }
}
