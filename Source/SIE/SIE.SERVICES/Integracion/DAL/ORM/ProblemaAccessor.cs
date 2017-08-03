using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    public abstract class ProblemaAccessor : BLToolkit.DataAccess.DataAccessor<ProblemaInfo>
    {
        [BLToolkit.DataAccess.SqlQuery(
            SqlText = "UPDATE Problema SET FechaModificacion = GETDATE() WHERE ProblemaID = @id")]
        public abstract void ActualizaFechaModificacion(int id);
    }
}
