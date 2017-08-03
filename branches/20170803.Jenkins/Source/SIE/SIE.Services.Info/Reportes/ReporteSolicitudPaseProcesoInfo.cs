using System;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Reportes
{
    public class ReporteSolicitudPaseProcesoInfo
    {
        public int Codigo { get; set; }
        public string Ingrediente { get; set; }
        public decimal TMExistenciaAlmacenPA { get; set; }
        public decimal TMConsumoDia { get; set; }
        public decimal TMCapacidadAlmacenaje { get; set; }
        public decimal TMSugeridasSolicitar { get; set; }
        public decimal TMRequeridasProduccion { get; set; }
        public string Organizacion { get; set; }
        public string Titulo { get; set; }
        public DateTime Fecha { get; set; }

    }
}
