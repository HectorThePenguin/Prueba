using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    public abstract class RoladoraAccessor : BLToolkit.DataAccess.DataAccessor<RoladoraInfo>
    {
        [BLToolkit.DataAccess.SqlQuery(SqlText = "Update Roladora Set FechaModificacion = GETDATE() Where RoladoraID = @roladoraID")]
        public abstract void ActualizarFechaModificacion(int roladoraID);
    }
}
