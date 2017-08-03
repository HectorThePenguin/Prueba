using System.Collections.Generic;
using SIE.Services.Info.Info;
namespace SIE.Services.Info.Modelos
{
    public class ImpresionCheckListRoladoModel
    {
        public CheckListRoladoraGeneralInfo CheckListRoladoraGeneral { get; set; }
        public List<CheckListRoladoraHorometroInfo> Horometros { get; set; }
        public List<CheckListRoladoraDetalleInfo> Detalles { get; set; }
        public int Hora { get; set; }
    }
}
