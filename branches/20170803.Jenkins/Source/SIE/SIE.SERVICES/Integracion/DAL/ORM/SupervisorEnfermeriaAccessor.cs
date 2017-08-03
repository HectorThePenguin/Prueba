using System.Collections.Generic;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    public abstract class SupervisorEnfermeriaAccessor : BLToolkit.DataAccess.DataAccessor<SupervisorEnfermeriaInfo>
    {
        [BLToolkit.DataAccess.SprocName("SupervisorEnfermeria_ObtenerPorPagina")]
        public abstract List<SupervisorEnfermeriaInfo> ObtenerPorPagina(int organizacionID, int supervisorEnfermeriaID, int operadorID, int enfermeriaID, bool activo);
    }
}