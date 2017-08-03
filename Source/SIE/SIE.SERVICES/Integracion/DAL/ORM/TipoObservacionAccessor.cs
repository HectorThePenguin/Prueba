using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    public abstract class TipoObservacionAccessor : BLToolkit.DataAccess.DataAccessor<TipoObservacionInfo>
    {
        [BLToolkit.DataAccess.SqlQuery(SqlText = "UPDATE TipoObservacion SET FechaModificacion = GETDATE() WHERE TipoObservacionID = @id")]
        public abstract void ActualizaFechaModificacion(int id);
    }
}
