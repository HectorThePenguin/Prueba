using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    public abstract class CamionRepartoAccessor : BLToolkit.DataAccess.DataAccessor<CamionRepartoInfo>
    {
        [BLToolkit.DataAccess.SqlQuery(SqlText = "UPDATE CamionReparto SET FechaModificacion = GETDATE() WHERE CamionRepartoID = @id")]
        public abstract void ActualizaFechaModificacion(int id);
    }
}
