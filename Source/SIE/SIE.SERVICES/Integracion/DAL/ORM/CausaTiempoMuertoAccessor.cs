using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    public abstract class CausaTiempoMuertoAccessor : BLToolkit.DataAccess.DataAccessor<CausaTiempoMuertoInfo>
    {
        [BLToolkit.DataAccess.SqlQuery(SqlText = "Update CausaTiempoMuerto Set FechaModificacion = GETDATE() Where CausaTiempoMuertoID = @causaTiempoMuertoID)")]
        public abstract void ActualizarFechaModificacion(int causaTiempoMuertoID);
    }
}
