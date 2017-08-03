using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    public abstract class CausaRechazoAccessor : BLToolkit.DataAccess.DataAccessor<CausaRechazoInfo>
    {
        [BLToolkit.DataAccess.SqlQuery(SqlText = "UPDATE CausaRechazo SET FechaModificacion = GETDATE() WHERE CausaRechazoID = @id")]
        public abstract void ActualizaFechaModificacion(int id);
    }
}
