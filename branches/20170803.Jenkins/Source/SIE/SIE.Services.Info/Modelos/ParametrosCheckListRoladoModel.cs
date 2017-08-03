using System.Collections.Generic;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Modelos
{
    public class ParametrosCheckListRoladoModel
    {
        public List<CheckListRoladoraInfo> Roladoras { get; set; }
        public int HumedadGranoRoladoBodega { get; set; }
        public int HumedadGranoEnteroBodega { get; set; }
        public int SuperavitAdicionAguaSurfactante { get; set; }
        public decimal SurfactanteInicio { get; set; }
        public decimal SurfactanteFinal { get; set; }
        public decimal ContadorAguaInicial { get; set; }
        public decimal ContadorAguaFinal { get; set; }
        public decimal ConsumoAguaLitro { get; set; }
        public decimal TotalGranoEntreroPP { get; set; }
        public decimal TotalGranoEnteroBodega { get; set; }
        public decimal TotalGranoProcesado { get; set; }
        public decimal SuperavitGranoRolado { get; set; }
        public decimal TotalGranoRolado { get; set; }
        public decimal ConsumoDieselCalderas { get; set; }
        public decimal DieseToneladaGranoRolado { get; set; }
    }
}
