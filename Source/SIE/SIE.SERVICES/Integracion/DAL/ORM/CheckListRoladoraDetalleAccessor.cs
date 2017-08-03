using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    public abstract class CheckListRoladoraDetalleAccessor : BLToolkit.DataAccess.DataAccessor<CheckListRoladoraDetalleInfo>
    {
        [BLToolkit.DataAccess.SqlQuery(SqlText = "Update CheckListRoladoraDetalle Set FechaModificacion = GETDATE() Where CheckListRoladoraDetalleID = @checkListRoladoraDetalleID")]
        public abstract void ActualizarFechaModificacion(int checkListRoladoraDetalleID);
    }
}
