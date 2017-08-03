using System;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Reportes;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapReporteVentaGanadoDAL
    {
        /// <summary>
        /// Obtiene una clase con los 
        /// datos necesarios para generar
        /// el reporte de venta de ganado
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ReporteVentaGanadoDatos ObtenerDatosReporte(DataSet ds)
        {
            ReporteVentaGanadoDatos resultado;
            try
            {
                Logger.Info();
                DataTable dtMovimientosEnfermeria = ds.Tables[ConstantesDAL.DtMovimientosEnfermeria];
                DataTable dtMovimientosProduccion = ds.Tables[ConstantesDAL.DtMovimientosProduccion];
                DataTable dtMovimientosTratamientos = ds.Tables[ConstantesDAL.DtTratamientos];
                DataTable dtFolios = ds.Tables[ConstantesDAL.DtFolios];
                DataTable dtAnimales = ds.Tables[ConstantesDAL.DtAnimalesReporteVenta];

                resultado = new ReporteVentaGanadoDatos
                                {
                                    MovimientosProduccion = (from movs in dtMovimientosProduccion.AsEnumerable()
                                                             select new ReporteVentaGanadoMovimientosProduccion
                                                                        {
                                                                            AnimalID = movs.Field<long>("AnimalID"),
                                                                            AnimalMovimientoID =
                                                                                movs.Field<long>("AnimalMovimientoID"),
                                                                            Corral =
                                                                                movs.Field<string>("CorralProduccion"),
                                                                            FechaMovimiento =
                                                                                movs.Field<DateTime>("FechaMovimiento")
                                                                        }).ToList(),
                                    Folios = (from movs in dtFolios.AsEnumerable()
                                              select new ReporteVentaGanadoFolio
                                                         {
                                                             FolioEntrada = movs.Field<int>("FolioEntrada"),
                                                             Organizacion = movs.Field<string>("Organizacion"),
                                                             EntradaGanadoID = movs.Field<int>("EntradaGanadoID"),
                                                             FechaEntrada = movs.Field<DateTime>("FechaEntrada"),
                                                             OrganizacionOrigenID =
                                                                 movs.Field<int>("OrganizacionOrigenID"),
                                                             Proveedor = movs.Field<string>("Proveedor"),
                                                             TipoOrganizacionID = movs.Field<int>("TipoOrganizacionID"),
                                                             AnimalID = movs.Field<int>("AnimalID")
                                                         }).ToList(),
                                    Tratamientos = (from movs in dtMovimientosTratamientos.AsEnumerable()
                                                    select new ReporteVentaGanadoTratamiento
                                                               {
                                                                   AnimalID = movs.Field<long>("AnimalID"),
                                                                   AnimalMovimientoID =
                                                                       movs.Field<long>("AnimalMovimientoID"),
                                                                   FechaMovimiento =
                                                                       movs.Field<DateTime>("FechaMovimiento"),
                                                                   CodigoTratamiento =
                                                                       movs.Field<int>("CodigoTratamiento"),
                                                                   Producto = movs.Field<string>("Producto"),
                                                               }).ToList(),
                                    Animales = (from movs in dtAnimales.AsEnumerable()
                                                select new ReporteVentaGanadoAnimal
                                                           {
                                                               AnimalID = movs.Field<long>("AnimalID"),
                                                               Arete = movs.Field<string>("Arete"),
                                                               CausaSalida = movs.Field<string>("CausaSalida"),
                                                               FolioEntrada = movs.Field<long>("FolioEntrada"),
                                                               OrganizacionIDEntrada =
                                                                   movs.Field<int>("OrganizacionIDEntrada"),
                                                               Sexo = movs.Field<string>("Sexo"),
                                                               TipoGanado = movs.Field<string>("TipoGanado"),
                                                               TipoGanadoID = movs.Field<int>("TipoGanadoID"),
                                                               Peso = movs.Field<int>("Peso"),
                                                               FolioTicket = movs.Field<int>("FolioTicket")
                                                           }).ToList(),
                                    MovimientosSalida = (from movs in dtMovimientosEnfermeria.AsEnumerable()
                                                         select new ReporteVentaGanadoMovimientosSalida
                                                                    {
                                                                        AnimalID = movs.Field<long>("AnimalID"),
                                                                        AnimalMovimientoID = movs.Field<long>("AnimalMovimientoID"),
                                                                        Corral = movs.Field<string>("Corral"),
                                                                        Enfermeria = movs.Field<string>("Enfermeria"),
                                                                        FechaMovimiento = movs.Field<DateTime>("FechaMovimiento"),
                                                                    }).ToList()
                                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
    }
}
