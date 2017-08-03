using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    public abstract class PrecioPACAccessor : BLToolkit.DataAccess.DataAccessor<PrecioPACInfo>
    {
        [BLToolkit.DataAccess.SqlQuery(SqlText = "Update PrecioPAC Set FechaModificacion = GETDATE() Where PrecioPACID = @precioPACID")]
        public abstract void ActualizarFechaModificacion(int precioPACID);
    }
}
