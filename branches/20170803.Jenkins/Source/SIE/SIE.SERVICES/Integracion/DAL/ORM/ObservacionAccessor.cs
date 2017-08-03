using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    public abstract class ObservacionAccessor : BLToolkit.DataAccess.DataAccessor<ObservacionInfo>
    {
        [BLToolkit.DataAccess.SqlQuery(SqlText = "UPDATE Observacion SET FechaModificacion = GETDATE() WHERE ObservacionID = @id")]
        public abstract void ActualizarFechaModificacion(int id);
    }
}
