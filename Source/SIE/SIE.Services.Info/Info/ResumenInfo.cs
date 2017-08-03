using System;

namespace SIE.Services.Info.Info
{
    public class ResumenInfo
    {
        /// <summary>
        /// Fecha Inicio
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 1, TipoDato = TypeCode.DateTime, AceptaVacio = false)]
        public DateTime FechaInicio { get; set; }
        /// <summary>
        /// FechaDisponibilidad
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 2, TipoDato = TypeCode.DateTime, AceptaVacio = false)]
        public DateTime FechaDisponibilidad { get; set; }
        /// <summary>
        /// OrganizacionID
        /// </summary>
        public int OrganizacionID { get; set; }
        /// <summary>
        /// Corral
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 3, TipoDato = TypeCode.String, AceptaVacio = false)]
        public string Corral { get; set; }
        /// <summary>
        /// Lote
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 4, TipoDato = TypeCode.String, AceptaVacio = true)]
        public string Lote { get; set; }
        /// <summary>
        /// TipoGanadoID
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 5, TipoDato = TypeCode.String, AceptaVacio = false)]
        public String TipoGanado { get; set; }
        /// <summary>
        /// Cabezas
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 6, TipoDato = TypeCode.Double, AceptaVacio = false)]
        public double Cabezas { get; set; }
        /// <summary>
        /// DiasEng
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 7, TipoDato = TypeCode.Double, AceptaVacio = false)]
        public double DiasEng { get; set; }
        /// <summary>
        /// GananciaDiaria
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 8, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double GananciaDiaria { get; set; }
        /// <summary>
        /// PesoOrigen
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 9, TipoDato = TypeCode.Double, AceptaVacio = false)]
        public double PesoOrigen { get; set; }
        /// <summary>
        /// PesoProyectado
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 10, TipoDato = TypeCode.Double, AceptaVacio = false)]
        public double PesoProyectado { get; set; }
        /// <summary>
        /// FormulaActual
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 11, TipoDato = TypeCode.String, AceptaVacio = true)]
        public string FormulaActual { get; set; }
        /// <summary>
        /// Consumo
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 12, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double Consumo { get; set; }
        /// <summary>
        /// Ganado
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 13, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double Ganado { get; set; }
        /// <summary>
        /// Alimento
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 14, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double Alimento { get; set; }
        /// <summary>
        /// Comision
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 15, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double Comision { get; set; }
        /// <summary>
        /// Fletes
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 16, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double Fletes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 17, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double ImpPred { get; set; }
        /// <summary>
        /// GuiaTrans
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 18, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double GuiaTrans { get; set; }
        /// <summary>
        /// SeguroDeCorrales
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 19, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double SeguroDeCorrales { get; set; }
        /// <summary>
        /// SeguroDeTransporte
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 20, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double SeguroDeTransporte { get; set; }
        /// <summary>
        /// GastoDeEngorda
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 21, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double GastoDeEngorda { get; set; }
        /// <summary>
        /// GastoDeCentro
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 22, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double GastoDeCentro { get; set; }
        /// <summary>
        /// GastoDePlanta
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 23, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double GastoDePlanta { get; set; }
        /// <summary>
        /// CertificadoyBanos
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 24, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double CertificadoyBanos { get; set; }
        /// <summary>
        /// AlimentoCentros
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 25, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double AlimentoCentros { get; set; }
        /// <summary>
        /// Pruebas
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 26, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double Pruebas { get; set; }
        /// <summary>
        /// MedicamentoEnCentros
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 27, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double MedicamentoEnCentros { get; set; }
        /// <summary>
        /// Renta
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 28, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double Renta { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 29, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double SeguroDeEnfermedadesExoticas { get; set; }
        /// <summary>
        /// MedicamentoDeEnfermerias
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 30, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double MedicamentoDeEnfermerias { get; set; }
        /// <summary>
        /// MedicamentoDeImpolante
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 31, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double MedicamentoDeImpolante { get; set; }
        /// <summary>
        /// MedicamentoDeReimplante
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 32, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double MedicamentoDeReimplante { get; set; }
        /// <summary>
        /// GastosIndirectos
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 33, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double GastosIndirectos { get; set; }
        /// <summary>
        /// SeguroDeGanadoEnCentro
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 34, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double SeguroDeGanadoEnCentro { get; set; }
        /// <summary>
        /// MedicamentoEnPradera
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 35, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double MedicamentoEnPradera { get; set; }
        /// <summary>
        /// AlimentacionEnCadis
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 36, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double AlimentacionEnCadis { get; set; }
        /// <summary>
        /// MedicamentoEnCadis
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 37, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double MedicamentoEnCadis { get; set; }
        /// <summary>
        /// GastosMateriasPrimas
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 38, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double GastosMateriasPrimas { get; set; }

        /// <summary>
        /// GastosFinancieros
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 39, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double GastosFinancieros { get; set; }
        /// <summary>
        /// GastosPraderasFijos
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 40, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double GastosPraderasFijos { get; set; }
        /// <summary>
        /// SeguroAltaMortandadPradera
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 41, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double SeguroAltaMortandadPradera { get; set; }
        /// <summary>
        /// AlimentoEnDescanso
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 42, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double AlimentoEnDescanso { get; set; }
        /// <summary>
        /// MedicamentoEnDescanso
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 43, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double MedicamentoEnDescanso { get; set; }
        /// <summary>
        /// ManejoDeGanado
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 44, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double ManejoDeGanado { get; set; }
        /// <summary>
        /// CostoDePradera
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 45, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double CostoDePradera { get; set; }
        /// <summary>
        /// Demoras
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 46, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double Demoras { get; set; }
        /// <summary>
        /// Maniobras
        /// </summary>
        [Atributos.AtributoCargaMPPA(Celda = 47, TipoDato = TypeCode.Double, AceptaVacio = true)]
        public double Maniobras { get; set; }

        #region Propiedades Ajenas al Excel
        /// <summary>
        /// Mensaje que marca si el renglon tiene conflictos
        /// </summary>
        public string MensajeAlerta { get; set; }

        #endregion Propiedades Ajenas al Excel
    }
}
