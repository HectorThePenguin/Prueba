using System;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Reportes
{
    public class ReporteLectorComederosInfo
    {
        public OrganizacionInfo Organizacion { get; set; }
        public DateTime Fecha { get; set; }
        public string Diferencia { get; set; }
        public string Codigo { get; set; }
        public int EstadoComedero { get; set; } //int tabla reparto detalle
        public string FormulaManiana { get; set; } //varchar tabla formula
        public int CantidadManiana { get; set; } //int tabla reparto detalle
        public string FormulaTarde { get; set; } //varchar tabla formula
        public int CantididadTarde { get; set; } //int tabla reparto detalle
        public int TotalHoy { get; set; } //cantidad mañana + cantidad tarde
        public int EstadoComederoAyer { get; set; } //int tabla reparto detalle
        public string FormulaManianaAyer { get; set; } //varchar tabla formula
        public int CantidadManianaAyer { get; set; } //int tabla reparto detalle
        public string FormulaTardeAyer { get; set; } //varchar tabla formula
        public int CantidadTardeAyer { get; set; }//int tabla reparto detalle
        public int TotalAyer { get; set; } //cantidad mañana + cantidad tarde
        public string Titulo { get; set; }
        public string NombreOrganizacion { get; set; }
    }
}
