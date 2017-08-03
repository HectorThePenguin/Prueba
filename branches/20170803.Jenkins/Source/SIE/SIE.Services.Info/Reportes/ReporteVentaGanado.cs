using System;

namespace SIE.Services.Info.Reportes
{
    public class ReporteVentaGanado
    {
        /// <summary>
        /// Enfermeria a la que esta
        /// asignado el animal
        /// </summary>
        public string Enfermeria { get; set; }
        /// <summary>
        /// Fecha en que entro a enfermeria
        /// </summary>
        public string Fecha { get; set; }
        /// <summary>
        /// Codigo del corral
        /// </summary>
        public string Codigo { get; set; }
        /// <summary>
        /// Tipo de ganado del animal
        /// </summary>
        public string TipoGanado { get; set; }
        /// <summary>
        /// Arete que tiene asignado el animal
        /// </summary>
        public string Arete { get; set; }
        /// <summary>
        /// Partida del animal
        /// </summary>
        public long Partida { get; set; }
        /// <summary>
        /// Fecha de llegada del animal
        /// </summary>
        public string FechaLlegada { get; set; }
        /// <summary>
        /// Organizacion origen del animal
        /// </summary>
        public string Origen { get; set; }
        /// <summary>
        /// Genero del animal
        /// </summary>
        public string Sexo { get; set; }
        /// <summary>
        /// Dias que paso en engorda
        /// </summary>
        public int DiasEngorda { get; set; }
        /// <summary>
        /// Causa por la cual esta en venta
        /// </summary>
        public string Causa { get; set; }
        /// <summary>
        /// Carral a donde se dirige el animal
        /// </summary>
        public string LugarDestino { get; set; }        
        /// <summary>
        /// Productos que se le aplicaron en el tratamiento
        /// </summary>
        public string PrimerMedicamento { get; set; }
        /// <summary>
        /// Fecha de su primer medicamento
        /// </summary>
        public string FechaPrimerMedicamento { get; set; }
        /// <summary>
        /// Productos que se le aplicaron en el tratamiento
        /// </summary>
        public string SegundoMedicamento { get; set; }
        /// <summary>
        /// Fecha en que se le aplica su segundo tratamiento
        /// </summary>
        public string FechaSegundoMedicamento { get; set; }
        /// <summary>
        /// Productos que se le aplicaron en el tratamiento
        /// </summary>
        public string TercerMedicamento { get; set; }
        /// <summary>
        /// Fecha en la que se le aplica el tercer tratamiento
        /// </summary>
        public string FechaTercerMedicamento { get; set; }


        //Datos del reporte
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public string Organizacion { get; set; }
        public string Titulo { get; set; }
        /// <summary>
        /// Fecha inicio con formato
        /// </summary>
        public string FechaInicioConFormato
        {
            get
            {
                return FechaInicial.ToString("dd/MM/yyyy");

            }

        }
        /// <summary>
        /// Fecha fin con formato
        /// </summary>
        public string FechaFinConFormato
        {
            get
            {
                return FechaFinal.ToString("dd/MM/yyyy");

            }

        }

        public string FechaCompuesta
        {
            get { return String.Format("Del {0} al {1}", FechaInicioConFormato, FechaFinConFormato); }
        }

        /// <summary>
        /// Peso del animal al momento de su venta
        /// </summary>
        public int Peso { get; set; }

        /// <summary>
        /// Folio de la venta
        /// </summary>
        public int FolioTicket { get; set; }
    }
}
