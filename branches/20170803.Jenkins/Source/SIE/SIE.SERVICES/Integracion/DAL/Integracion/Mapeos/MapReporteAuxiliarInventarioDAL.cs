using System;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Reportes;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapReporteAuxiliarInventarioDAL
    {
        /// <summary>
        /// Obtiene una entidad de Corral Reporte Auxiliar
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static CorralReporteAuxiliarInventarioInfo ObtenerCorralReporteAuxiliarInventario(DataSet ds)
        {
            CorralReporteAuxiliarInventarioInfo corral;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                corral = (from info in dt.AsEnumerable()
                          select new CorralReporteAuxiliarInventarioInfo
                         {
                             Clasificacion = info.Field<string>("Clasificacion"),
                             ClasificacionID = info.Field<int>("ClasificacionID"),
                             Corral = info.Field<string>("Codigo"),
                             CorralID = info.Field<int>("CorralID"),
                             FechaInicio = info.Field<DateTime>("FechaInicio"),
                             Lote = info.Field<string>("Lote"),
                             LoteID = info.Field<int>("LoteID"),
                             TipoGanado = info.Field<string>("TipoGanado"),
                             TipoGanadoID = info.Field<int>("TipoGanadoID"),
                             TipoCorral = info.Field<string>("TipoCorral"),
                             TipoCorralID = info.Field<int>("TipoCorralID")
                         }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return corral;
        }

        /// <summary>
        /// Obtiene una lista con los datos para el 
        /// Reporte Auxiliar de Inventario
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ReporteAuxiliarInventarioInfo ObtenerReporteAuxuliarInventario(DataSet ds)
        {
            ReporteAuxiliarInventarioInfo resultado;
            try
            {
                Logger.Info();
                DataTable dtMovimientosAnimal = ds.Tables[ConstantesDAL.DtMovimientosAnimal];
                DataTable dtEntradas = ds.Tables[ConstantesDAL.DtEntradas];
                DataTable dtLotes = ds.Tables[ConstantesDAL.DtLotes];
                DataTable dtMovimientos = ds.Tables[ConstantesDAL.DtMovimientos];
                DataTable dtInterfaceSalidaAnimal = ds.Tables[ConstantesDAL.DtInterfaceSalidaAnimal];
                DataTable dtAnimales = ds.Tables[ConstantesDAL.DtAnimales];

                resultado = new ReporteAuxiliarInventarioInfo
                {
                    AnimalesMovimiento = (from info in dtMovimientosAnimal.AsEnumerable()
                                            select new AnimalMovimientoInfo
                                            {
                                                AnimalID = info.Field<int>("AnimalID"),
                                                AnimalMovimientoID = info.Field<long>("AnimalMovimientoID"),
                                                OrganizacionID = info.Field<int>("OrganizacionID"),
                                                CorralID = info.Field<int>("CorralID"),
                                                LoteID = info.Field<int>("LoteID"),
                                                FechaMovimiento = info.Field<DateTime>("FechaMovimiento"),
                                                TipoMovimientoID = info.Field<int>("TipoMovimientoID"),
                                                FolioEntrada = info["FolioEntrada"] == null ? 0 : info.Field<long>("FolioEntrada"),
                                                TipoOrganizacionID = info.Field<int>("TipoOrganizacionID"),
                                                CorralOrigen = info.Field<string>("CorralOrigen")
                                            }).ToList(),
                    EntradasGanado = (from info in dtEntradas.AsEnumerable()
                                        select new EntradaGanadoInfo
                                        {
                                            FolioOrigen = info.Field<int>("FolioOrigen"),
                                            CorralID = info.Field<int>("CorralID"),
                                            CodigoCorral = info.Field<string>("CodigoCorral"),
                                            LoteID = info.Field<int>("LoteID"),
                                            FechaEntrada = info.Field<DateTime>("FechaEntrada"),
                                            FolioEntrada = info.Field<int>("FolioEntrada"),
                                            OrganizacionOrigenID = info.Field<int>("OrganizacionOrigenID"),
                                            TipoOrigen = info.Field<int>("TipoOrganizacionID"),
                                            CabezasRecibidas = info.Field<int>("CabezasRecibidas"),
                                        }).ToList(),
                    Lotes = (from info in dtLotes.AsEnumerable()
                                select new LoteInfo
                                        {
                                            Lote = info.Field<string>("Lote"),
                                            LoteID = info.Field<int>("LoteID"),
                                            Cabezas = info.Field<int>("Cabezas"),
                                            CabezasInicio = info.Field<int>("CabezasInicio"),
                                            FechaInicio = info.Field<DateTime>("FechaInicio"),
                                            TipoProcesoID = info.Field<int>("TipoProcesoID"),
                                            Corral = new CorralInfo
                                            {
                                                CorralID = info.Field<int>("CorralID"),
                                                Codigo = info.Field<string>("Codigo"),
                                                TipoCorral = new TipoCorralInfo
                                                {
                                                    TipoCorralID = info.Field<int>("TipoCorralID"),
                                                    GrupoCorral = new GrupoCorralInfo
                                                    {
                                                        GrupoCorralID = info.Field<int>("GrupoCorralID"),
                                                        Descripcion = info.Field<string>("GrupoCorral")
                                                    }
                                                }
                                            },
                                        }).ToList(),
                    TiposMovimiento = (from info in dtMovimientos.AsEnumerable()
                                        select new TipoMovimientoInfo
                                        {
                                            TipoMovimientoID = info.Field<int>("TipoMovimientoID"),
                                            EsEntrada = info.Field<bool>("EsEntrada"),
                                            EsSalida = info.Field<bool>("EsSalida"),
                                            ClaveCodigo = info.Field<string>("ClaveCodigo"),
                                        }).ToList(),
                    InterfaceSalidasAnimal = (from info in dtInterfaceSalidaAnimal.AsEnumerable()
                                                select new InterfaceSalidaAnimalInfo
                                                {
                                                    SalidaID = info.Field<int>("SalidaID"),
                                                    Arete = info.Field<string>("Arete"),
                                                    CorralID = info.Field<int>("CorralID"),
                                                    Partida = info.Field<int>("Id")
                                                }).ToList(),
                    Animales = (from info in dtAnimales.AsEnumerable()
                                select new AnimalInfo
                                {
                                    AnimalID = info.Field<int>("AnimalID"),
                                    Arete = info.Field<string>("Arete"),
                                    FolioEntrada = info["FolioEntrada"] == null ? 0 : info.Field<int>("FolioEntrada"),
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
