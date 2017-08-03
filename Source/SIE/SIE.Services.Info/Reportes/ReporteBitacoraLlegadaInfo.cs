
using System;
using SIE.Services.Info.Info;
using System.IO;

namespace SIE.Services.Info.Reportes
{

    public class ReporteBitacoraLlegadaInfo
    {
        public int Folio { get; set; }
        public string Empresa { get; set; }
        public string Chofer { get; set; }
        public string AccesoA { get; set; }
        public string Marca { get; set; }
        public string Placa { get; set; }
        public string Color { get; set; }
        public string Producto { get; set; }
        public decimal PorcentajeHumedad { get; set; }
        public TimeSpan TiempoEstandar { get; set; }

        public DateTime Llegada { get; set; }
        public DateTime FechaLlegada { get { return (DateTime)Llegada; } }

        public DateTime Entrada { get; set; }
        public DateTime FechaEntrada { get { return (DateTime)Entrada; } }

        public DateTime Pesaje { get; set; }
        public DateTime FechaPesaje { get { return (DateTime)Pesaje; } }

        public DateTime Destara { get; set; }
        public DateTime FechaDestara { get { return (DateTime)Destara; } }

        public DateTime InicioDescarga { get; set; }
        public DateTime FinDescarga { get; set; }
        
        private DateTime FechaDefault = DateTime.Parse("1900-01-01");

        public TimeSpan EsperaLlegadaIngresoGanadera { get {
            TimeSpan espera = new TimeSpan();
            if (Llegada != null && Entrada != null)
            {
                DateTime tmpLlegada = (DateTime)Llegada;
                DateTime tmpEntrada = (DateTime)Entrada;
                espera = tmpEntrada.Subtract(tmpLlegada);
            }
           return espera;
        }}

        public TimeSpan EsperaLlegadaPesaje { get {
            TimeSpan espera = new TimeSpan();
            if (Llegada != FechaDefault && Pesaje != FechaDefault){
                DateTime tmpLlegada = (DateTime)Llegada;
                DateTime tmpPesaje = (DateTime)Pesaje;
                espera = tmpPesaje.Subtract(tmpLlegada);
            }
            return espera;
        }}

        public TimeSpan EsperaIngresoGanaderaPesaje { get {
            TimeSpan espera = new TimeSpan();
            if (Entrada != FechaDefault && Pesaje != FechaDefault){
                DateTime tmpEntrada = (DateTime)Entrada;
                DateTime tmpPesaje = (DateTime)Pesaje;
                espera = tmpPesaje.Subtract(tmpEntrada);
            }
            return espera;
        }}

        public TimeSpan LlegadaDestara { get {
            TimeSpan espera = new TimeSpan();
            if (Llegada != FechaDefault && Destara != FechaDefault){
                DateTime tmpLlegada = (DateTime)Llegada;
                DateTime tmpDestara = (DateTime)Destara;
                espera = tmpDestara.Subtract(tmpLlegada);
            }
            return espera;
        }}

        public string Titulo { get; set; }
        public string Organizacion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        /// <summary>
        /// Fecha inicio con formato
        /// </summary>
        public string FechaInicioConFormato
        {
            get
            {
                return FechaInicio.ToString("dd/MM/yyyy");

            }

        }
        /// <summary>
        /// Fecha fin con formato
        /// </summary>
        public string FechaFinConFormato
        {
            get
            {
                return FechaFin.ToString("dd/MM/yyyy");

            }

        }
        /// <summary>
        /// Formato para mostrado del periodo en el informe
        /// </summary>
        public string FechaEntreCadenas
        {
            get
            {
                return string.Format("De {0} al {1}",
                    FechaInicioConFormato,
                    FechaFinConFormato);

            }

        }

        public int Indicador { get; set; }
    }
}
