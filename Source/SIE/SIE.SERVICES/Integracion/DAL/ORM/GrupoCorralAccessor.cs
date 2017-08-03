using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    public abstract class GrupoCorralAccessor : BLToolkit.DataAccess.DataAccessor<GrupoCorralInfo>
    {
        [BLToolkit.DataAccess.SqlQuery(SqlText = "UPDATE GrupoCorral SET FechaModificacion = GETDATE() WHERE GrupoCorralID = @id")]
        public abstract void ActualizarFechaModificacion(int id);
    }
}
