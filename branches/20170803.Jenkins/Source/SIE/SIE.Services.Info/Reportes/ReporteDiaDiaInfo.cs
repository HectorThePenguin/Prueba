using System;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Reportes
{
    public class ReporteDiaDiaInfo
    {
        /// <summary>
        /// Fecha en que Sale el Embarque
        /// </summary>
        public String FechaEmbarque { get; set; }
        /// <summary>
        /// Fecha en que se Recibe el Embarque
        /// </summary>
        public String FechaLlegada { get; set; }
        /// <summary>
        /// Nombre del Comprador
        /// </summary>
        public String Comprador { get; set; }
        /// <summary>
        /// Origen del Embarque
        /// </summary>
        public String Origen { get; set; }
        /// <summary>
        /// Nombre del Proveedor
        /// </summary>
        public String Proveedor { get; set; }
        /// <summary>
        /// Nombre de la Region del Embarque
        /// </summary>
        //public String Region { get; set; }
        /// <summary>
        /// Numero de Entrada para el Ganado
        /// </summary>
        public int EntradaGanado { get; set; }
        /// <summary>
        /// Numero de Cabezas con Calidad 1
        /// </summary>
        public int Calidad1 { get; set; }
        /// <summary>
        /// Numero de Cabezas con Calidad 1.5
        /// </summary>
        public int Calidad15 { get; set; }
        /// <summary>
        /// Numero de Cabezas con Calidad 2
        /// </summary>
        public int Calidad2 { get; set; }
        /// <summary>
        /// Numero de Cabezas con Calidad 3
        /// </summary>
        public int Calidad3 { get; set; }
        /// <summary>
        /// Numero de Cabezas con Calidad 3.5
        /// </summary>
        public int Calidad35 { get; set; }
        /// <summary>
        /// Numero de Cabezas con Calidad 3.5L
        /// </summary>
        public int Calidad35L { get; set; }
        /// <summary>
        /// Numero de Cabezas con Calidad 3.5P
        /// </summary>
        public int Calidad35P { get; set; }
        /// <summary>
        /// Numero de Cabezas con Calidad 3.5 VT2
        /// </summary>
        public int Calidad35VT2 { get; set; }
        /// <summary>
        /// Numero de Cabezas con Calidad 3.5 PR
        /// </summary>
        public int Calidad35PR { get; set; }
        /// <summary>
        /// Numero de Cabezas con Calidad 4
        /// </summary>
        public int Calidad4 { get; set; }
        /// <summary>
        /// Numero de Cabezas con Calidad 5
        /// </summary>
        public int Calidad5 { get; set; }
        /// <summary>
        /// Numero de Cabezas con Calidad 6
        /// </summary>
        public int Calidad6 { get; set; }
        /// <summary>
        /// Numero de Cabezas con Calidad 7
        /// </summary>
        public int Calidad7 { get; set; }
        /// <summary>
        /// Numero de Cabezas con Calidad (3.5VT2,4,5,6,3.5L,3.5P,7)
        /// </summary>
        public int TotalRechazo { get; set; }
        /// <summary>
        /// Porcentaje de Rechazo
        /// </summary>
        public decimal PorcentajeRechazo { get; set; }
        /// <summary>
        /// Numero de Cabezas con Respuesta : 
        /// Número de Enfermos Grado 2, Número de Enfermos Grado 3, Número de Enfermos Grado 4. 
        /// </summary>
        public int MorbilidadAltoRiesgo { get; set; }
        /// <summary>
        /// Porcentaje de Morbilidad de Alto Riesgo,
        /// se toman en cuanta tambien las Muertas
        /// </summary>
        public decimal PorcentajeMorbilidadAltoRiesgo { get; set; }
        /// <summary>
        /// Numero de Cabezas Muertas
        /// </summary>
        public int MuertosDeducibles { get; set; }
        /// <summary>
        /// Cabezas Recibidas en Entrada de Ganado
        /// </summary>
        public int CabezasTotales { get; set; }
        /// <summary>
        /// Numero de Cabezas Machos
        /// </summary>
        public int CabezasMachos { get; set; }
        /// <summary>
        /// Numero de Cabezas Hembras
        /// </summary>
        public int CabezasHembras { get; set; }
        /// <summary>
        /// Peso Promedio de Machos
        /// </summary>
        public int PesoPromedioMachos { get; set; }
        /// <summary>
        /// Peso Promedio Hembras
        /// </summary>
        public int PesoPromedioHembras { get; set; }
        /// <summary>
        /// Diferencia entre el Peso con que se Compra el Ganado
        /// y el Peso con el que Llega
        /// </summary>
        public decimal MermaReal { get; set; }
        /// <summary>
        /// Merma que se espera que tendra el ganado
        /// </summary>
        public decimal MermaEsperada { get; set; }
        /// <summary>
        /// Merma Esperada menos Merma Real del Ganado
        /// </summary>
        //public decimal DiferenciaMerma { get; set; }
        /// <summary>
        /// Horas que duro el ganado transitando
        /// </summary>
        public decimal HorasTransito { get; set; }
        /// <summary>
        /// Horas esperadas para el transito del ganado
        /// </summary>
        public decimal HorasEsperadas { get; set; }
        /// <summary>
        /// Horas Esperadas menos Horas Transito
        /// </summary>
        public decimal DiferenciaHoras { get; set; }
        /// <summary>
        /// Horas Reales en que Hubo Descanso para el Ganado
        /// </summary>
        //public decimal HorasDescansoReal { get; set; }
        /// <summary>
        /// Horas esperadas para que el Ganado Descanse
        /// </summary>
        //public decimal HorasDescansoEsperadas { get; set; }
        /// <summary>
        /// Dias en que se mantuvo el Ganado en un Centro
        /// </summary>
        public int DiasCentro { get; set; }
        /// <summary>
        /// Costo de Kilogramo de Alimento
        /// </summary>
        //public decimal CostoKGAlimento { get; set; }
        /// <summary>
        /// Costo de Alimento por Cabeza de Ganado
        /// </summary>
        public decimal CostoAlimentoCabeza { get; set; }
        /// <summary>
        /// Consumo diario de Alimento
        /// </summary>
        //public decimal ConsumoDiario { get; set; }
        /// <summary>
        /// Numero de Salida
        /// </summary>
        public int NumeroSalidaFactura { get; set; }
        /// <summary>
        /// Precio por Kilo Machos
        /// </summary>
        public decimal PrecioMachos { get; set; }
        /// <summary>
        /// Precio por Kilo Hembras
        /// </summary>
        public decimal PrecioHembras { get; set; }
        /// <summary>
        /// Costo Comision por Kilo
        /// </summary>
        public decimal ComisionPorKilo { get; set; }
        /// <summary>
        /// Costo Flete por Kilo
        /// </summary>
        public decimal FletePorKilo { get; set; }
        /// <summary>
        /// Costo Alimento por Kilo
        /// </summary>
        public decimal AlimentoPorKilo { get; set; }
        /// <summary>
        /// Costo Guia por Kilo
        /// </summary>
        public decimal GuiaPorKilo { get; set; }
        /// <summary>
        /// Otros Costos por Kilo
        /// </summary>
        public decimal OtrosPorKilo { get; set; }
        /// <summary>
        /// Costo de Seguro por Corral por Kilo
        /// </summary>
        public decimal SeguroCorralPorKilo { get; set; }
        /// <summary>
        /// Costo de Seguro de Transporte Por Kilo
        /// </summary>
        public decimal SeguroTransportePorKilo { get; set; }
        /// <summary>
        /// Costo Integrado por Machos
        /// </summary>
        public decimal CostoIntegradoMachos { get; set; }
        /// <summary>
        /// Costo Integrado por Hembras
        /// </summary>
        public decimal CostoIntegradoHembras { get; set; }
        /// <summary>
        /// Total de Kilos Machos
        /// </summary>
        public int TotalKilosMachos { get; set; }
        /// <summary>
        /// Total de Kilos Hembras
        /// </summary>
        public int TotalKilosHembras { get; set; }
        /// <summary>
        /// Kilos Totales
        /// </summary>
        public int TotalKilos { get; set; }
        /// <summary>
        /// Ocupacion por Embarque  (1 / Registros de Embarque Detalle)
        /// </summary>
        public decimal JaulasSalida { get; set; }
        /// <summary>
        /// Kilos con los que llego el Ganado
        /// </summary>
        public int KilosLlegada { get; set; }
        /// <summary>
        /// Kilos con los que se esparaba que llegada el Ganado
        /// </summary>
        //public decimal KilosLlegadaEsperados { get; set; }
        /// <summary>
        /// Costo de Compra por Machos
        /// </summary>
        public decimal CostoCompraMachos { get; set; }
        /// <summary>
        /// CostoCompraHembras
        /// </summary>
        public decimal CostoCompraHembras { get; set; }
        /// <summary>
        /// Costo de Comision
        /// </summary>
        public decimal Comision { get; set; }
        /// <summary>
        /// Costo de Alimento en Centro
        /// </summary>
        public decimal AlimentoEnCentro { get; set; }
        /// <summary>
        /// Costo de Fletes
        /// </summary>
        public decimal Fletes { get; set; }
        /// <summary>
        /// Costo de Guias de Transito
        /// </summary>
        public decimal GuiasDeTransito { get; set; }
        /// <summary>
        /// Costo de Baños
        /// </summary>
        public decimal Baños { get; set; }
        /// <summary>
        /// Costo de Pruebas BT y TR
        /// </summary>
        public decimal PruebasBTTR { get; set; }
        /// <summary>
        /// Costo de Renta
        /// </summary>
        public decimal Renta { get; set; }
        /// <summary>
        /// Costo del Medicamento en Centro
        /// </summary>
        //public decimal MedicamentoEnCentro { get; set; }
        /// <summary>
        /// Costo del Medicamento en Cadis
        /// </summary>
        //public decimal MedicamentoEnCadis { get; set; }
        /// <summary>
        /// Costo del Medicamento en Pradera
        /// </summary>
        //public decimal MedicamentoEnPradera { get; set; }

         /// <summary>
         /// Costo del Medicamento 
         /// </summary>
        public decimal Medicamento { get; set; }

        /// <summary>
        /// Costo del Seguro de Ganado en Centro
        /// </summary>
        public decimal SeguroDeGanadoEnCentro { get; set; }
        /// <summary>
        /// Costo del Seguro de Transportes
        /// </summary>
        public decimal SeguroDeTransportes { get; set; }
        /// <summary>
        /// Costo de Pradera
        /// </summary>
        public decimal CostoDePradera { get; set; }
        /// <summary>
        /// Costo de Impuesto Predial
        /// </summary>
        public decimal ImpuestoPredial { get; set; }
        /// <summary>
        /// Costo de Alimento en Descanso
        /// </summary>
        public decimal AlimentoEnDescanso { get; set; }
        /// <summary>
        /// Costo de Manejo de Ganado
        /// </summary>
        public decimal ManejoDeGanado { get; set; }
        /// <summary>
        /// Costo Total por Machos
        /// </summary>
        public decimal CostoTotalMachos { get; set; }
        /// <summary>
        /// Costo Total por Hembras
        /// </summary>
        public decimal CostoTotalHembras { get; set; }
        /// <summary>
        /// Proveedor del Servicio de Flete
        /// </summary>
        public string LineaFletera { get; set; }
        /// <summary>
        /// Persona que Transporto el Ganado
        /// </summary>
        public string Chofer { get; set; }
        /// <summary>
        /// Placas del Tracto con que se registro la entrada
        /// </summary>
        public string Placas { get; set; }
        /// <summary>
        /// Hora de entrada del Ganado
        /// </summary>
        public string HoraLlegada { get; set; }
        /// <summary>
        /// Demoras del Transporte
        /// </summary>
        public decimal Demoras { get; set; }
        /// <summary>
        /// Gastos Indirectos de Transporte
        /// </summary>
        public decimal GastosIndirectos { get; set; }
        /// <summary>
        /// Cantidad de Kilometros Recorridos
        /// </summary>
        public decimal Kilometros { get; set; }
        public string Titulo { get; set; }
        /// <summary>
        /// Organizacion del informe
        /// </summary>
        public string Organizacion { get; set; }
        /// <summary>
        /// Fecha Inicio
        /// </summary>
        public DateTime FechaInicio { get; set; }
        /// <summary>
        /// Fecha Fin
        /// </summary>
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

        public string FechaCompuesta
        {
            get { return String.Format("Del {0} al {1}", FechaInicioConFormato, FechaFinConFormato); }
        }
        /// <summary>
        /// CorralOrigen
        /// </summary>
        public string CodigoCorralOrigen { get; set; }
    }
}
