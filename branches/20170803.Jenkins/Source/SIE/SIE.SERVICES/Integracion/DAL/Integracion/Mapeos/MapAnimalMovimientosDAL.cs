using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapAnimalMovimientosDAL
    {

        internal static AnimalMovimientoInfo ObtenerAnimalMovimiento(DataSet ds)
        {
            AnimalMovimientoInfo animalMovimientoInfo;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                animalMovimientoInfo = new AnimalMovimientoInfo();
                foreach (DataRow dr in dt.Rows)
                {
                    animalMovimientoInfo.AnimalID = Convert.ToInt32(dr["AnimalID"]);
                    animalMovimientoInfo.AnimalMovimientoID = Convert.ToInt64(dr["AnimalMovimientoID"]);
                    animalMovimientoInfo.OrganizacionID = Convert.ToInt32(dr["OrganizacionID"]);
                    animalMovimientoInfo.CorralID = Convert.ToInt32(dr["CorralID"]);
                    animalMovimientoInfo.LoteID = Convert.ToInt32(dr["LoteID"]);
                    animalMovimientoInfo.FechaMovimiento = Convert.ToDateTime(dr["FechaMovimiento"]);
                    animalMovimientoInfo.Peso = Convert.ToInt32(dr["Peso"]);
                    animalMovimientoInfo.Temperatura = Convert.ToDouble(dr["Temperatura"]);
                    animalMovimientoInfo.TipoMovimientoID = Convert.ToInt32(dr["TipoMovimientoID"]);
                    animalMovimientoInfo.TrampaID = Convert.ToInt32(dr["TrampaID"]);
                    animalMovimientoInfo.OperadorID = Convert.ToInt32(dr["OperadorID"]);
                    animalMovimientoInfo.Observaciones = Convert.ToString(dr["Observaciones"]);
                    animalMovimientoInfo.Activo = Convert.ToInt32(dr["Activo"]) == 0 ? EstatusEnum.Inactivo : EstatusEnum.Activo;
                    animalMovimientoInfo.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]);
                    animalMovimientoInfo.UsuarioCreacionID = Convert.ToInt32(dr["UsuarioCreacionID"]);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return animalMovimientoInfo;
        }

        internal static AnimalInfo ObtenerAnimalPorArete(DataSet ds)
        {
            AnimalInfo animalInfo;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                animalInfo = new AnimalInfo();
                foreach (DataRow dr in dt.Rows)
                {
                    animalInfo.AnimalID = Convert.ToInt32(dr["AnimalID"]);
                    animalInfo.Arete = Convert.ToString(dr["Arete"]);
                    animalInfo.AreteMetalico = Convert.ToString(dr["AreteMetalico"]);
                    animalInfo.FechaCompra = Convert.ToDateTime(dr["FechaCompra"]);
                    animalInfo.TipoGanadoID = Convert.ToInt32(dr["TipoGanadoID"]);
                    animalInfo.CalidadGanadoID = Convert.ToInt32(dr["CalidadGanadoID"]);
                    animalInfo.ClasificacionGanadoID = Convert.ToInt32(dr["ClasificacionGanadoID"]);
                    animalInfo.PesoCompra = Convert.ToInt32(dr["PesoCompra"]);
                    animalInfo.OrganizacionIDEntrada = Convert.ToInt32(dr["OrganizacionIDEntrada"]);
                    animalInfo.FolioEntrada = Convert.ToInt32(dr["FolioEntrada"]);
                    animalInfo.PesoLlegada = Convert.ToInt32(dr["PesoLlegada"]);
                    animalInfo.Paletas = Convert.ToInt32(dr["Paletas"]);
                    animalInfo.Venta = Convert.ToBoolean(dr["Venta"]);
                    animalInfo.Cronico = Convert.ToBoolean(dr["Cronico"]);
                    animalInfo.Activo = Convert.ToInt32(dr["Activo"]) == 0 ? Convert.ToBoolean(EstatusEnum.Inactivo) : Convert.ToBoolean(EstatusEnum.Activo);
                    animalInfo.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]);
                    animalInfo.UsuarioCreacionID = Convert.ToInt32(dr["UsuarioCreacionID"]);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return animalInfo;
        }

        internal static int ObtenerPesoProyectado(DataSet ds)
        {
            int resultado = 0;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                foreach (DataRow dr in dt.Rows)
                {
                    resultado = Convert.ToInt32(dr["PesoProyectado"]);
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resultado;
        }

        internal static int ObtenerExisteSalida(DataSet ds)
        {
            int resultado = 0;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                foreach (DataRow dr in dt.Rows)
                {
                    resultado = Convert.ToInt32(dr["AnimalID"]);
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resultado;
        }

        internal static string ObtenerExisteVentaDetalle(DataSet ds)
        {
            string resultado = "";
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                foreach (DataRow dr in dt.Rows)
                {
                    resultado = dr["Arete"].ToString();
                }
                if (resultado == "")
                {
                    resultado = "0";
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resultado;
        }

        internal static int ObtenerLoteSalidaAnimal(DataSet ds)
        {
            int resultado = 0;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                foreach (DataRow dr in dt.Rows)
                {
                    resultado = Convert.ToInt32(dr["LoteID"]);
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resultado;
        }

        /// <summary>
        /// Obtiene el Ultimo Movimiento por animal
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<AnimalMovimientoInfo> ObtenerUltimoMovimientoAnimal(DataSet ds)
        {
            List<AnimalMovimientoInfo> movimientos;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                movimientos = (from info in dt.AsEnumerable()
                               select new AnimalMovimientoInfo
                                          {
                                              AnimalID = info.Field<long>("AnimalID"),
                                              TipoMovimientoID = info.Field<int>("TipoMovimientoID"),
                                              OrganizacionID = info.Field<int>("OrganizacionID"),
                                              Peso = info.Field<int>("Peso")
                                          }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return movimientos;
        }

        public static List<AnimalMovimientoInfo> ObtenerAnimalesNoReimplantados(DataSet ds)
        {
            AnimalMovimientoInfo animalMovimientoInfo;
            List<AnimalMovimientoInfo> listaAnimalMovimientoInfo;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                
                listaAnimalMovimientoInfo = new List<AnimalMovimientoInfo>();
                foreach (DataRow dr in dt.Rows)
                {
                    animalMovimientoInfo = new AnimalMovimientoInfo();
                    animalMovimientoInfo.AnimalID = Convert.ToInt32(dr["AnimalID"]);
                    animalMovimientoInfo.AnimalMovimientoID = Convert.ToInt64(dr["AnimalMovimientoID"]);
                    animalMovimientoInfo.OrganizacionID = Convert.ToInt32(dr["OrganizacionID"]);
                    animalMovimientoInfo.CorralID = dr["CorralID"] != null ? Convert.ToInt32(dr["CorralID"]) : 0;
                    animalMovimientoInfo.CorralIDOrigen = dr["CorralIDOrigen"] != null ?  Convert.ToInt32(dr["CorralIDOrigen"]) : 0;
                    animalMovimientoInfo.LoteID = dr["LoteID"] != null ? Convert.ToInt32(dr["LoteID"]) : 0;
                    animalMovimientoInfo.LoteIDOrigen = dr["LoteIDOrigen"] != null ? Convert.ToInt32(dr["LoteIDOrigen"]) : 0;
                    animalMovimientoInfo.FechaMovimiento = Convert.ToDateTime(dr["FechaMovimiento"]);
                    animalMovimientoInfo.Peso = Convert.ToInt32(dr["Peso"]);
                    animalMovimientoInfo.Temperatura = Convert.ToDouble(dr["Temperatura"]);
                    animalMovimientoInfo.TipoMovimientoID = Convert.ToInt32(dr["TipoMovimientoID"]);
                    animalMovimientoInfo.TrampaID = Convert.ToInt32(dr["TrampaID"]);
                    animalMovimientoInfo.OperadorID = Convert.ToInt32(dr["OperadorID"]);
                    animalMovimientoInfo.Observaciones = Convert.ToString(dr["Observaciones"]);
                    animalMovimientoInfo.Activo = Convert.ToInt32(dr["Activo"]) == 0 ? EstatusEnum.Inactivo : EstatusEnum.Activo;
                    animalMovimientoInfo.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]);
                    animalMovimientoInfo.UsuarioCreacionID = Convert.ToInt32(dr["UsuarioCreacionID"]);

                    listaAnimalMovimientoInfo.Add(animalMovimientoInfo);
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaAnimalMovimientoInfo;
        }

        internal static List<AnimalMovimientoInfo> ObtenerUltimoMovimientoAnimal(IDataReader reader)
        {
            var movimientos = new List<AnimalMovimientoInfo>();
            try
            {
                Logger.Info();
                AnimalMovimientoInfo elemento;
                while (reader.Read())
                {
                    elemento = new AnimalMovimientoInfo
                                   {
                                       AnimalID = Convert.ToInt32(reader["AnimalID"]),
                                       AnimalMovimientoID = Convert.ToInt64(reader["AnimalMovimientoID"]),
                                       OrganizacionID = Convert.ToInt32(reader["OrganizacionID"]),
                                       CorralID = Convert.ToInt32(reader["CorralID"]),
                                       LoteID = Convert.ToInt32(reader["LoteID"]),
                                       FechaMovimiento = Convert.ToDateTime(reader["FechaMovimiento"]),
                                       Peso = Convert.ToInt32(reader["Peso"]),
                                       Temperatura = Convert.ToDouble(reader["Temperatura"]),
                                       TipoMovimientoID = Convert.ToInt32(reader["TipoMovimientoID"]),
                                       TrampaID = Convert.ToInt32(reader["TrampaID"]),
                                       OperadorID = Convert.ToInt32(reader["OperadorID"]),
                                       Observaciones = Convert.ToString(reader["Observaciones"]),
                                       LoteIDOrigen = Convert.ToInt32(reader["LoteIDOrigen"]),
                                       AnimalMovimientoIDAnterior = Convert.ToInt64(reader["AnimalMovimientoIDAnterior"]),
                                   };
                    movimientos.Add(elemento);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return movimientos;
        }

        internal static List<AnimalMovimientoInfo> ObtenerMovimientosPorArete(DataSet ds)
        {
            List<AnimalMovimientoInfo> movimientos;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                movimientos = (from info in dt.AsEnumerable()
                               select new AnimalMovimientoInfo
                                   {
                                       AnimalID = info.Field<long>("AnimalID"),
                                       AnimalMovimientoID = info.Field<long>("AnimalMovimientoID"),
                                       CorralID = info.Field<int>("CorralID"),
                                       LoteID = info.Field<int>("LoteID"),
                                       FechaMovimiento = info.Field<DateTime>("FechaMovimiento"),
                                       OrganizacionID = info.Field<int>("OrganizacionID"),
                                       Peso = info.Field<int>("Peso"),
                                       Temperatura = Convert.ToDouble(info["Temperatura"]),
                                       TipoMovimientoID = info.Field<int>("TipoMovimientoID"),
                                       TrampaID = info.Field<int>("TrampaID"),
                                       OperadorID = info.Field<int>("OperadorID"),
                                       Observaciones = info.Field<string>("Observaciones"),
                                       LoteIDOrigen = info.Field<int?>("LoteIDOrigen") != null ? info.Field<int>("LoteIDOrigen") : 0,
                                       AnimalMovimientoIDAnterior = info.Field<long?>("AnimalMovimientoIDAnterior") != null ? info.Field<long>("AnimalMovimientoIDAnterior") : 0,
                                       Activo = info.Field<bool>("Activo").BoolAEnum(),
                                       TipoCorralID = info.Field<int>("TipoCorralID"),
                                       GrupoCorralID = info.Field<int>("GrupoCorralID")
                                   }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return movimientos;
        }

        /// <summary>
        /// Obtiene una mapeo de animal movimiento
        /// </summary>
        /// <returns></returns>
        internal static IMapBuilderContext<AnimalMovimientoInfo> ObtenerAnimalMovimientoMuertos()
        {
            try
            {
                Logger.Info();
                IMapBuilderContext<AnimalMovimientoInfo> mapAnimalMovimiento = MapeoBasico();
                return mapAnimalMovimiento;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        private static IMapBuilderContext<AnimalMovimientoInfo> MapeoBasico()
        {
            try
            {
                Logger.Info();
                IMapBuilderContext<AnimalMovimientoInfo> mapAnimalMovimiento =
                    MapBuilder<AnimalMovimientoInfo>.MapNoProperties();
                mapAnimalMovimiento.Map(x => x.AnimalID).ToColumn("AnimalID");
                mapAnimalMovimiento.Map(x => x.AnimalMovimientoID).ToColumn("AnimalMovimientoID");
                mapAnimalMovimiento.Map(x => x.OrganizacionID).ToColumn("OrganizacionID");
                mapAnimalMovimiento.Map(x => x.FechaMovimiento).ToColumn("FechaMovimiento");
                mapAnimalMovimiento.Map(x => x.Arete).ToColumn("Arete");
                return mapAnimalMovimiento;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static List<AnimalMovimientoInfo> ObtenerAnimalMovimientoXML(DataSet ds)
        {
            var animalMovimientoInfoReturn = new List<AnimalMovimientoInfo>();
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                
                foreach (DataRow dr in dt.Rows)
                {
                    var animalMovimientoInfo = new AnimalMovimientoInfo();
                    animalMovimientoInfo.AnimalID = Convert.ToInt32(dr["AnimalID"]);
                    animalMovimientoInfo.AnimalMovimientoID = Convert.ToInt64(dr["AnimalMovimientoID"]);
                    animalMovimientoInfo.OrganizacionID = Convert.ToInt32(dr["OrganizacionID"]);
                    animalMovimientoInfo.CorralID = Convert.ToInt32(dr["CorralID"]);
                    animalMovimientoInfo.LoteID = Convert.ToInt32(dr["LoteID"]);
                    animalMovimientoInfo.FechaMovimiento = Convert.ToDateTime(dr["FechaMovimiento"]);
                    animalMovimientoInfo.Peso = Convert.ToInt32(dr["Peso"]);
                    animalMovimientoInfo.Temperatura = Convert.ToDouble(dr["Temperatura"]);
                    animalMovimientoInfo.TipoMovimientoID = Convert.ToInt32(dr["TipoMovimientoID"]);
                    animalMovimientoInfo.TrampaID = Convert.ToInt32(dr["TrampaID"]);
                    animalMovimientoInfo.OperadorID = Convert.ToInt32(dr["OperadorID"]);
                    animalMovimientoInfo.Observaciones = Convert.ToString(dr["Observaciones"]);
                    animalMovimientoInfo.Activo = Convert.ToInt32(dr["Activo"]) == 0 ? EstatusEnum.Inactivo : EstatusEnum.Activo;
                    animalMovimientoInfo.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]);
                    animalMovimientoInfo.UsuarioCreacionID = Convert.ToInt32(dr["UsuarioCreacionID"]);

                    animalMovimientoInfoReturn.Add(animalMovimientoInfo);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return animalMovimientoInfoReturn;
        }

        /// <summary>
        /// Obtiene una lista de animales no reimplantados
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<AnimalMovimientoInfo> ObtenerAnimalesNoReimplantadosXML(DataSet ds)
        {
            AnimalMovimientoInfo animalMovimientoInfo;
            List<AnimalMovimientoInfo> listaAnimalMovimientoInfo;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                listaAnimalMovimientoInfo = new List<AnimalMovimientoInfo>();
                foreach (DataRow dr in dt.Rows)
                {
                    animalMovimientoInfo = new AnimalMovimientoInfo();
                    animalMovimientoInfo.AnimalID = Convert.ToInt32(dr["AnimalID"]);
                    animalMovimientoInfo.AnimalMovimientoID = Convert.ToInt64(dr["AnimalMovimientoID"]);
                    animalMovimientoInfo.OrganizacionID = Convert.ToInt32(dr["OrganizacionID"]);
                    animalMovimientoInfo.CorralID = Convert.ToInt32(dr["CorralID"]);
                    animalMovimientoInfo.CorralIDOrigen = Convert.ToInt32(dr["CorralIDOrigen"]);
                    animalMovimientoInfo.LoteID = Convert.ToInt32(dr["LoteID"]);
                    animalMovimientoInfo.LoteIDOrigen = Convert.ToInt32(dr["LoteIDOrigen"]);
                    animalMovimientoInfo.FechaMovimiento = Convert.ToDateTime(dr["FechaMovimiento"]);
                    animalMovimientoInfo.Peso = Convert.ToInt32(dr["Peso"]);
                    animalMovimientoInfo.Temperatura = Convert.ToDouble(dr["Temperatura"]);
                    animalMovimientoInfo.TipoMovimientoID = Convert.ToInt32(dr["TipoMovimientoID"]);
                    animalMovimientoInfo.TrampaID = Convert.ToInt32(dr["TrampaID"]);
                    animalMovimientoInfo.OperadorID = Convert.ToInt32(dr["OperadorID"]);
                    animalMovimientoInfo.Observaciones = Convert.ToString(dr["Observaciones"]);
                    animalMovimientoInfo.Activo = Convert.ToInt32(dr["Activo"]) == 0 ? EstatusEnum.Inactivo : EstatusEnum.Activo;
                    animalMovimientoInfo.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]);
                    animalMovimientoInfo.UsuarioCreacionID = Convert.ToInt32(dr["UsuarioCreacionID"]);

                    listaAnimalMovimientoInfo.Add(animalMovimientoInfo);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return listaAnimalMovimientoInfo;
        }

        /// <summary>
        /// Obtiene la trazabilidad de los Movimientos de un animal
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="animal"></param>
        /// <returns></returns>
        internal static AnimalInfo ObtenerTrazabilidadAnimalMovimiento(DataSet ds, AnimalInfo animal)
        {
            var animalInfo = animal;
            try
            {
                Logger.Info();
                /* Se obtienen los movimientos del animal */
                animalInfo.ListaAnimalesMovimiento = new List<AnimalMovimientoInfo>();
                var dt = ds.Tables[ConstantesDAL.DtDatosTrazabilidadMovimientos];
                animalInfo.ListaAnimalesMovimiento = (from info in dt.AsEnumerable()
                    select new AnimalMovimientoInfo
                    {
                        AnimalID = info.Field<long>("AnimalID"),
                        AnimalMovimientoID = info.Field<long>("AnimalMovimientoID"),
                        TipoMovimiento = new TipoMovimientoInfo()
                        {
                            TipoMovimientoID = info.Field<int>("TipoMovimientoID"),
                            Descripcion = info.Field<string>("Descripcion")
                        },
                        FechaMovimiento = info.Field<DateTime>("FechaMovimiento"),
                        Peso = info.Field<int>("Peso"),
                        LoteOrigen = new LoteInfo()
                        {
                            LoteID = info.Field<int?>("LoteIDOrigen") != null ? info.Field<int>("LoteIDOrigen") : 0,
                            Lote = info.Field<string>("LoteOrigen") ?? String.Empty,
                            Corral = new CorralInfo()
                            {
                                CorralID = info.Field<int?>("CorralIDOrigen") != null ? info.Field<int>("CorralIDOrigen") : 0,
                                Codigo = info.Field<string>("CodigoOrigen") ?? String.Empty,
                            }
                        },
                        LoteDestino = new LoteInfo()
                        {
                            LoteID = info.Field<int>("LoteIDDestino"),
                            Lote = info.Field<string>("LoteDestino"),
                            Corral = new CorralInfo()
                            {
                                CorralID = info.Field<int>("CorralIDDestino"),
                                Codigo = info.Field<string>("CodigoDestino")
                            }
                        },
                        Trampa = new TrampaInfo
                        {
                            TrampaID = info.Field<int>("TrampaID"),
                            Descripcion = info.Field<string>("DescripcionTrampa"),
                            HostName = info.Field<string>("HostName"),
                        },
                        Usuario = new UsuarioInfo()
                        {
                            UsuarioID = info.Field<int>("UsuarioID"),
                            Nombre = info.Field<string>("Nombre")
                        }
                    }).ToList();

                /* Se obtienen los costos del animal */
                animalInfo.ListaCostosAnimal = new List<AnimalCostoInfo>();
                dt = ds.Tables[ConstantesDAL.DtDatosTrazabilidadCostos];
                animalInfo.ListaCostosAnimal = (from info in dt.AsEnumerable()
                    select new AnimalCostoInfo
                    {
                        AnimalID = info.Field<long>("AnimalID"),
                        Costo = new CostoInfo()
                        {
                            CostoID = info.Field<int>("CostoID"),
                            Descripcion = info.Field<string>("Descripcion")
                        },
                        Importe = info.Field<decimal>("Importe"),
                    }).ToList();

                /* Se obtienen los consumos del animal */
                animalInfo.ListaConsumosAnimal = new List<AnimalConsumoInfo>();
                dt = ds.Tables[ConstantesDAL.DtDatosTrazabilidadConsumos];
                animalInfo.ListaConsumosAnimal = (from info in dt.AsEnumerable()
                    select new AnimalConsumoInfo
                    {
                        FormulaServida = new FormulaInfo()
                        {
                            FormulaId = info.Field<int>("FormulaIDServida"),
                            Descripcion = info.Field<string>("Descripcion")
                        },
                        Dias = info.Field<int>("Dias"),
                        Kilos = info.Field<decimal>("Kilos"),
                        Promedio = info.Field<decimal>("Promedio"),
                    }).ToList();

                // Se se obtienen movimientos se buscan tratamientos aplicados
                if (animalInfo.ListaAnimalesMovimiento.Any())
                {
                    /* Se obtienen los productos aplicados por cada movimiento del animal */
                    var tratamientoAplicadoInfo = new List<TratamientoAplicadoInfo>();
                    dt = ds.Tables[ConstantesDAL.DtDatosTrazabilidadMovimientosProductos];
                    tratamientoAplicadoInfo = (from info in dt.AsEnumerable()
                        select new TratamientoAplicadoInfo
                        {
                            AnimalMovimientoID = info.Field<long>("AnimalMovimientoID"),
                            Producto = new ProductoInfo()
                            {
                                ProductoId = info.Field<int>("ProductoID"),
                                Descripcion = info.Field<string>("Descripcion"),
                                UnidadMedicion = new UnidadMedicionInfo()
                                {
                                    ClaveUnidad = info.Field<string>("ClaveUnidad"),
                                }
                            },
                            Cantidad = info.Field<decimal>("Cantidad"),
                            Importe = info.Field<decimal>("Importe"),
                        }).ToList();
                    //si se obtienen tratamientos se asignan a los movimientos encontrados
                    if (tratamientoAplicadoInfo.Any())
                    {
                        foreach (var animalMovimiento in animalInfo.ListaAnimalesMovimiento)
                        {
                            animalMovimiento.ListaTratamientosAplicados = new List<TratamientoAplicadoInfo>();
                            AnimalMovimientoInfo movimiento = animalMovimiento;
                            foreach (var aplicadoInfo in tratamientoAplicadoInfo.Where(
                                aplicadoInfo => aplicadoInfo.AnimalMovimientoID == movimiento.AnimalMovimientoID))
                            {
                                animalMovimiento.ListaTratamientosAplicados.Add(aplicadoInfo);
                            }
                        }
                    }
                }

                /* Se obtienen los costos de abasto del animal */
                animalInfo.ListaCostosAbastoAnimal = new List<AnimalCostoInfo>();
                dt = ds.Tables[ConstantesDAL.DtDatosTrazabilidadCostosAbasto];
                animalInfo.ListaCostosAbastoAnimal = (from info in dt.AsEnumerable()
                    select new AnimalCostoInfo
                    {
                        AnimalID = info.Field<long>("AnimalID"),
                        Costo = new CostoInfo()
                        {
                            CostoID = info.Field<int>("CostoID"),
                            Descripcion = info.Field<string>("Descripcion")
                        },
                        Importe = Convert.ToDecimal(info.Field<double>("ImporteAbasto")),
                    }).ToList();

                /* Se obtienen los consumos de abasto del animal */
                animalInfo.ListaConsumoAbastoAnimal = new List<AnimalConsumoInfo>();
                dt = ds.Tables[ConstantesDAL.DtDatosTrazabilidadConsumosAbasto];
                animalInfo.ListaConsumoAbastoAnimal = (from info in dt.AsEnumerable()
                    select new AnimalConsumoInfo
                    {
                        Dias = info.Field<int>("Dias"),
                        FormulaServida = new FormulaInfo{
                            Descripcion = info.Field<string>("Descripcion")
                        },
                        Cantidad = decimal.Parse(info.Field<double>("Cantidad").ToString())
                        
                    }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return animalInfo;
        }
    }
}