using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Info.Reportes;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapReporteDiaDiaDAL
    {
        /// <summary>
        /// Obtiene una lista de Tipo de Ganado Reporte Dia a Dia
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ReporteDiaDiaInfo> ObtenerTipoGanadoReporteDiaDia(DataSet ds)
        {
            List<ReporteDiaDiaInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new ReporteDiaDiaInfo
                                    {
                                        
                                        FechaEmbarque = info.Field<string>("FechaEmbarque"),
                                        FechaLlegada = info.Field<string>("FechaLlegada"),
                                        Comprador = info.Field<string>("Comprador"),
                                        Origen = info.Field<string>("Origen"),
                                        Proveedor = info.Field<string>("Proveedor"),
                                        EntradaGanado = info.Field<int>("EntradaGanado"),
                                        NumeroSalidaFactura = info.Field<int>("NumeroSalidaFactura"),
                                        Calidad1 = info.Field<int>("Calidad1"),
                                        Calidad15 = info.Field<int>("Calidad15"),
                                        Calidad2 = info.Field<int>("Calidad2"),
                                        Calidad3 = info.Field<int>("Calidad3"),
                                        Calidad35 = info.Field<int>("Calidad35"),
                                        Calidad35L = info.Field<int>("Calidad35L"),
                                        Calidad35P = info.Field<int>("Calidad35P"),
                                        Calidad35PR = info.Field<int>("Calidad35PR"),
                                        Calidad35VT2 = info.Field<int>("Calidad35VT2"),
                                        Calidad4 = info.Field<int>("Calidad4"),
                                        Calidad5 = info.Field<int>("Calidad5"),
                                        Calidad6 = info.Field<int>("Calidad6"),
                                        Calidad7 = info.Field<int>("Calidad7"),
                                        MuertosDeducibles = info.Field<int>("MuertosDeducibles"),
                                        CabezasTotales = info.Field<int>("CabezasTotales"),
                                        CabezasMachos = info.Field<int>("CabezasMachos"),
                                        CabezasHembras = info.Field<int>("CabezasHembras"),
                                        HorasTransito = info.Field<decimal>("HorasTransito"),
                                        HorasEsperadas = info.Field<decimal>("HorasEsperadas"),
                                        DiferenciaHoras = info.Field<decimal>("DiferenciaHoras"),
                                        JaulasSalida = info.Field<decimal>("JaulasSalida"),
                                        KilosLlegada = info.Field<int>("KilosLlegada"),
                                        Comision = info.Field<decimal>("Comision"),
                                        AlimentoEnCentro = info.Field<decimal>("AlimentoEnCentro"),
                                        Fletes = info.Field<decimal>("Fletes"),
                                        GuiasDeTransito = info.Field<decimal>("GuiasDeTransito"),
                                        Baños = info.Field<decimal>("Banios"),
                                        PruebasBTTR = info.Field<decimal>("PruebasBTTR"),
                                        Renta = info.Field<decimal>("Renta"),
                                        //MedicamentoEnCentro = info.Field<decimal>("MedicamentoEnCentro"),
                                        //MedicamentoEnCadis = info.Field<decimal>("MedicamentoEnCadis"),
                                        //MedicamentoEnPradera = info.Field<decimal>("MedicamentoEnPradera"),
                                        Medicamento = info.Field<decimal>("Medicamento"),
                                        SeguroDeGanadoEnCentro = info.Field<decimal>("SeguroDeGanadoEnCentro"),
                                        SeguroDeTransportes = info.Field<decimal>("SeguroDeTransportes"),
                                        CostoDePradera = info.Field<decimal>("CostoDePradera"),
                                        ImpuestoPredial = info.Field<decimal>("ImpuestoPredial"),
                                        AlimentoEnDescanso = info.Field<decimal>("AlimentoEnDescanso"),
                                        ManejoDeGanado = info.Field<decimal>("ManejoDeGanado"),
                                        LineaFletera = info.Field<string>("LineaFletera"),
                                        Chofer = info.Field<string>("Chofer"),
                                        Placas = info.Field<string>("Placas"),
                                        HoraLlegada = string.Format(" {0}", info.Field<string>("HoraLlegada")),
                                        GastosIndirectos = info.Field<decimal>("GastosIndirectos"),
                                        Kilometros = info.Field<decimal>("Kilometros"),
                                        MermaReal = info.Field<decimal>("MermaReal"),
                                        MermaEsperada = info.Field<decimal>("MermaEsperada"),
                                        DiasCentro = info.Field<int>("DiasCentro"),
                                        PorcentajeRechazo = info.Field<decimal>("PorcentajeRechazo"),
                                        MorbilidadAltoRiesgo = info.Field<int>("MorbilidadAltoRiesgo"),
                                        PorcentajeMorbilidadAltoRiesgo = info.Field<decimal>("PorcentajeMorbilidadAltoRiesgo"),
                                        TotalRechazo = info.Field<int>("TotalRechazo"),
                                        //CostoKGAlimento = info.Field<decimal>("CostoKGAlimento"),
                                        PrecioMachos = info.Field<decimal>("PrecioMachos"),
                                        PrecioHembras = info.Field<decimal>("PrecioHembras"),
                                        ComisionPorKilo = info.Field<decimal>("ComisionPorKilo"),
                                        FletePorKilo = info.Field<decimal>("FletePorKilo"),
                                        AlimentoPorKilo = info.Field<decimal>("AlimentoPorKilo"),
                                        GuiaPorKilo = info.Field<decimal>("GuiaPorKilo"),
                                        OtrosPorKilo = info.Field<decimal>("OtrosPorKilo"),
                                        SeguroTransportePorKilo = info.Field<decimal>("SeguroTransportePorKilo"),
                                        CostoIntegradoMachos = info.Field<decimal>("CostoIntegradoMachos"),
                                        CostoIntegradoHembras = info.Field<decimal>("CostoIntegradoHembras"),
                                        TotalKilosMachos = info.Field<int>("TotalKilosMachos"),
                                        TotalKilosHembras = info.Field<int>("TotalKilosHembras"),
                                        TotalKilos = info.Field<int>("TotalKilos"),
                                        CostoCompraMachos = info.Field<decimal>("CostoCompraMachos"),
                                        CostoCompraHembras = info.Field<decimal>("CostoCompraHembras"),
                                        CostoTotalMachos = info.Field<decimal>("CostoTotalMachos"),
                                        CostoTotalHembras = info.Field<decimal>("CostoTotalHembras"),
                                        SeguroCorralPorKilo = info.Field<decimal>("SeguroCorralPorKilo"),
                                        Demoras = info.Field<decimal>("Demoras"),
                                        PesoPromedioMachos = info.Field<int>("PesoPromedioMachos"),
                                        PesoPromedioHembras = info.Field<int>("PesoPromedioHembras"),
                                        CostoAlimentoCabeza = info.Field<decimal>("CostoAlimentoCabeza"),
                                        CodigoCorralOrigen = info.Field<string>("CodigoCorral") 
                                    }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }
    }
}
