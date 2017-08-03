using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    public abstract class AlmacenProductoCuentaAccessor : BLToolkit.DataAccess.DataAccessor<AlmacenProductoCuentaInfo>
    {
        [BLToolkit.DataAccess.SqlQuery(SqlText = "Update AlmacenProductoCuenta Set FechaModificacion = GETDATE() Where AlmacenProductoCuentaID = @almacenProductoCuentaID)")]
        public abstract void ActualizarFechaModificacion(int almacenProductoCuentaID);
    }
}
