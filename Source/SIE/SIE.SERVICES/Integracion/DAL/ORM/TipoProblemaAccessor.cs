using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    public abstract class TipoProblemaAccessor : BLToolkit.DataAccess.DataAccessor<TipoProblemaInfo>
    {
        [BLToolkit.DataAccess.SqlQuery(
            SqlText = "UPDATE TipoProblema SET FechaModificacion = GETDATE() WHERE TipoProblemaID = @id")]
        public abstract void ActualizaFechaModificacion(int id);
    }
}
