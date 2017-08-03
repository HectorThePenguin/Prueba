using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    public abstract class CuentaAlmacenSubFamiliaAccessor : BLToolkit.DataAccess.DataAccessor<CuentaAlmacenSubFamiliaInfo>
    {
        [BLToolkit.DataAccess.SqlQuery(SqlText = "Update CuentaAlmacenSubFamilia Set FechaModificacion = GETDATE() Where CuentaAlmacenSubFamiliaID = @cuentaAlmacenSubFamiliaID)")]
        public abstract void ActualizarFechaModificacion(int cuentaAlmacenSubFamiliaID);
    }
}
