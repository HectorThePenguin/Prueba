using System.Collections.Generic;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    public abstract class PesajeMateriaPrimaAccessor : BLToolkit.DataAccess.DataAccessor<PesajeMateriaPrimaInfo>
    {
        [BLToolkit.DataAccess.SqlQuery(SqlText = "Update PesajeMateriaPrima Set FechaModificacion = GETDATE() Where PesajeMateriaPrimaID = @pesajeMateriaPrimaID)")]
        public abstract void ActualizarFechaModificacion(int pesajeMateriaPrimaID);

        [BLToolkit.DataAccess.SprocName("PesajeMateriaPrima_ObtenerProductoTicket")]
        public abstract List<FiltroTicketInfo> ObtenerTicketsFiltros(int organizacionID, string ticket);

        [BLToolkit.DataAccess.SprocName("Ticket_ObtenerValoresProduccionMolino")]
        public abstract List<FiltroTicketProduccionMolino> ObtenerValoresTicketProduccion(int organizacionID, string ticket, int indicadorID);
    }
}
