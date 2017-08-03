using System;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class AnimalConsumoInfo
    {
        public int AnimalConsumoId { get; set; }
        public long AnimalId { get; set; }
        public long RepartoId { get; set; }
        public int FormulaServidaId { get; set; }
        /// <summary>
        /// FormulaServida
        /// </summary>
        public FormulaInfo FormulaServida { get; set; }
        public decimal Cantidad { get; set; }
        public TipoServicioEnum TipoServicio { get; set; }
        public DateTime Fecha { get; set; }
        public EstatusEnum Activo { get; set; }
        public int UsuarioCreacionId { get; set; }
        public int UsuarioModificacionId { get; set; }

        public int Dias { get; set; }
        public decimal Kilos { get; set; }
        public decimal Promedio { get; set; }

    }
}
