using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    public abstract class TipoServicioAccessor : BLToolkit.DataAccess.DataAccessor<TipoServicioInfo>
    {
        [BLToolkit.DataAccess.SqlQuery(SqlText = "UPDATE TipoServicio SET FechaModificacion = GETDATE() WHERE TipoServicioID = @id")]
        public abstract void ActualizaFechaModificacion(int id);
    }
}
