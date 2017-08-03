using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    public abstract class TipoCuentaAccessor : BLToolkit.DataAccess.DataAccessor<TipoCuentaInfo>
    {
        [BLToolkit.DataAccess.SqlQuery(SqlText = "UPDATE TipoCuenta SET FechaModificacion = GETDATE() WHERE TipoCuentaID = @id")]
        public abstract void ActualizarFechaModificacion(int id);
    }
}
