using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    public abstract class TipoPACAccessor : BLToolkit.DataAccess.DataAccessor<TipoPACInfo>
    {
        [BLToolkit.DataAccess.SqlQuery(SqlText = "Update TipoPAC Set FechaModificacion = GETDATE() Where TipoPACID = @tipoPACID")]
        public abstract void ActualizarFechaModificacion(int tipoPACID);
    }
}
