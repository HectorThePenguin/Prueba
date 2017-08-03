using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    public abstract class TipoPreguntaAccessor : BLToolkit.DataAccess.DataAccessor<TipoPreguntaInfo>
    {
        [BLToolkit.DataAccess.SqlQuery(SqlText = "UPDATE TipoPregunta SET FechaModificacion = GETDATE() WHERE TipoPreguntaID = @id")]
        public abstract void ActualizaFechaModificacion(int id);
    }
}
