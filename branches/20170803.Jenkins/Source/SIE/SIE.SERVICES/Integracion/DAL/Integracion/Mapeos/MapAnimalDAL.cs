using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using System.Data;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapAnimalDAL
    {
        /// <summary>
        ///     Método asigna el registro del animal obtenido
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static AnimalInfo ObtenerAnimal(DataSet ds)
        {
            AnimalInfo animalInfo;
            try
            {

                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
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
                    if (dr["CausaRechadoID"] is DBNull)
                    {
                        animalInfo.CausaRechadoID = 0;
                    }
                    else
                    {
                        animalInfo.CausaRechadoID = Convert.ToInt32(dr["CausaRechadoID"]);
                    }
                    animalInfo.Venta = Convert.ToBoolean(dr["Venta"]);
                    animalInfo.Cronico = Convert.ToBoolean(dr["Cronico"]);
                    animalInfo.Activo = Convert.ToBoolean(dr["Activo"]);
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

        /// <summary>
        ///     Método asigna el registro del animal movimiento obtenido
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static List<AnimalMovimientoInfo> ObtenerAnimalMovimientoFechaUltimoMovimiento(DataSet ds)
        {
            List<AnimalMovimientoInfo> animalMovimientoInfo;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                animalMovimientoInfo = (from info in dt.AsEnumerable()
                                        select new AnimalMovimientoInfo
                             {
                                 LoteID = info.Field<int>("LoteID"),
                                 FechaMovimiento = info.Field<DateTime>("FechaMovimiento")
                             }).ToList();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return animalMovimientoInfo;

        }

        /// <summary>
        ///     Método asigna el registro del animal movimiento obtenido
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static List<AnimalInfo> ObtenerAnimalesPorLote(DataSet ds)
        {
            List<AnimalInfo> animalInfo;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var dtMovimientos = ds.Tables[ConstantesDAL.DtDetalleAnimalMovimiento];
                animalInfo = (from info in dt.AsEnumerable()
                              select new AnimalInfo
                                        {
                                            AnimalID = info.Field<long>("AnimalID"),
                                            Arete = info.Field<string>("Arete"),
                                            AreteMetalico = info.Field<string>("AreteMetalico"),
                                            FechaCompra = info.Field<DateTime>("FechaCompra"),
                                            TipoGanado = new TipoGanadoInfo
                                                {
                                                    TipoGanadoID = info.Field<int>("TipoGanadoID"),
                                                    Descripcion = info.Field<string>("TipoGanado"),
                                                    Sexo = info.Field<string>("Sexo")[0] == 'H' ? Sexo.Hembra : Sexo.Macho,
                                                },
                                            CalidadGanadoID = info.Field<int>("CalidadGanadoID"),
                                            ClasificacionGanado = new ClasificacionGanadoInfo
                                                {
                                                    ClasificacionGanadoID = info.Field<int>("ClasificacionGanadoID"),
                                                    Descripcion = info.Field<string>("ClasificacionGanado"),
                                                },
                                            PesoCompra = info.Field<int>("PesoCompra"),
                                            OrganizacionIDEntrada = info.Field<int>("OrganizacionIDEntrada"),
                                            FolioEntrada = Convert.ToInt32(info["FolioEntrada"]),
                                            PesoLlegada = info.Field<int>("PesoLlegada"),
                                            Paletas = info.Field<int>("Paletas"),
                                            CausaRechadoID = info.Field<int?>("CausaRechadoID") != null ? info.Field<int>("CausaRechadoID") : 0,
                                            Venta = info.Field<bool>("Venta"),
                                            Cronico = info.Field<bool>("Cronico"),
                                            FechaEntrada = info.Field<DateTime>("FechaEntrada"),
                                            ListaAnimalesMovimiento = (from movimiento in dtMovimientos.AsEnumerable()
                                                                       where info.Field<long>("AnimalID") == movimiento.Field<long>("AnimalID")
                                              select new AnimalMovimientoInfo
                                                  {
                                                      AnimalID = movimiento.Field<long>("AnimalID"),
                                                      AnimalMovimientoID = movimiento.Field<long>("AnimalMovimientoID"),
                                                      OrganizacionID = movimiento.Field<int>("OrganizacionID"),
                                                      CorralID = movimiento.Field<int>("CorralID"),
                                                      LoteID = movimiento.Field<int>("LoteID"),
                                                      FechaMovimiento = movimiento.Field<DateTime>("FechaMovimiento"),
                                                      Peso = movimiento.Field<int>("Peso"),
                                                      Temperatura = Convert.ToDouble(movimiento["Temperatura"]),
                                                      TipoMovimientoID = movimiento.Field<int>("TipoMovimientoID"),
                                                      TrampaID = movimiento.Field<int>("TrampaID"),
                                                      OperadorID = movimiento.Field<int>("OperadorID"),
                                                      Observaciones = movimiento.Field<string>("Observaciones")
                                                  }).ToList()
                                        }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return animalInfo;

        }

        /// <summary>
        /// Obtiene una lista de Animales por su Disponibilidad
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<AnimalInfo> ObtenerAnimalesPorLoteDisponibilidad(DataSet ds)
        {
            List<AnimalInfo> animalInfo;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var dtMovimientos = ds.Tables[ConstantesDAL.DtDetalleAnimalMovimiento];
                animalInfo = (from info in dt.AsEnumerable()
                              select new AnimalInfo
                              {
                                  AnimalID = info.Field<long>("AnimalID"),
                                  Arete = info.Field<string>("Arete"),
                                  AreteMetalico = info.Field<string>("AreteMetalico"),
                                  FechaCompra = info.Field<DateTime>("FechaCompra"),
                                  FechaEntrada = info.Field<DateTime>("FechaEntrada"),
                                  TipoGanado = new TipoGanadoInfo
                                  {
                                      TipoGanadoID = info.Field<int>("TipoGanadoID"),
                                      Descripcion = info.Field<string>("TipoGanado"),
                                      Sexo = info.Field<string>("Sexo")[0] == 'H' ? Sexo.Hembra : Sexo.Macho,
                                  },
                                  CalidadGanadoID = info.Field<int>("CalidadGanadoID"),
                                  ClasificacionGanado = new ClasificacionGanadoInfo
                                  {
                                      ClasificacionGanadoID = info.Field<int>("ClasificacionGanadoID"),
                                      Descripcion = info.Field<string>("ClasificacionGanado"),
                                  },
                                  PesoCompra = info.Field<int>("PesoCompra"),
                                  OrganizacionIDEntrada = info.Field<int>("OrganizacionIDEntrada"),
                                  FolioEntrada = Convert.ToInt32(info["FolioEntrada"]),
                                  PesoLlegada = info.Field<int>("PesoLlegada"),
                                  Paletas = info.Field<int>("Paletas"),
                                  CausaRechadoID = info.Field<int?>("CausaRechadoID") != null ? info.Field<int>("CausaRechadoID") : 0,
                                  Venta = info.Field<bool>("Venta"),
                                  Cronico = info.Field<bool>("Cronico"),
                                  LoteID = info.Field<int>("LoteID"),
                              }).ToList();

                var listaAnimalesMovimiento = (from movimiento in dtMovimientos.AsEnumerable()
                                               select new AnimalMovimientoInfo
                                               {
                                                   AnimalID = movimiento.Field<long>("AnimalID"),
                                                   AnimalMovimientoID = movimiento.Field<long>("AnimalMovimientoID"),
                                                   OrganizacionID = movimiento.Field<int>("OrganizacionID"),
                                                   CorralID = movimiento.Field<int>("CorralID"),
                                                   LoteID = movimiento.Field<int>("LoteID"),
                                                   FechaMovimiento = movimiento.Field<DateTime>("FechaMovimiento"),
                                                   Peso = movimiento.Field<int>("Peso"),
                                                   Temperatura = Convert.ToDouble(movimiento["Temperatura"]),
                                                   TipoMovimientoID = movimiento.Field<int>("TipoMovimientoID"),
                                                   TrampaID = movimiento.Field<int>("TrampaID"),
                                                   OperadorID = movimiento.Field<int>("OperadorID"),
                                                   Observaciones = movimiento.Field<string>("Observaciones")
                                               }).ToList();

                animalInfo.ForEach(animal =>
                {
                    animal.ListaAnimalesMovimiento =
                        listaAnimalesMovimiento.Where(mov => mov.AnimalID == animal.AnimalID && mov.Activo == EstatusEnum.Activo).ToList();
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return animalInfo;
        }

        /// <summary>
        /// Obtiene una lista de Animales por Codigo de Corral
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static List<AnimalInfo> ObtenerAnimalesPorCodigoCorral(DataSet ds)
        {
            List<AnimalInfo> animalInfo;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                animalInfo = (from info in dt.AsEnumerable()
                              select new AnimalInfo
                              {
                                  AnimalID = info.Field<long>("AnimalID"),
                                  Arete = info.Field<string>("Arete"),
                                  AreteMetalico = info.Field<string>("AreteMetalico")
                              }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return animalInfo;
        }

        /// <summary>
        /// Obtiene una lista de Animales por Codigo de Corral
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<AnimalInfo> ObtenerAnimalesRecepcionPorCodigoCorral(DataSet ds)
        {
            List<AnimalInfo> animalInfo;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                animalInfo = (from info in dt.AsEnumerable()
                              select new AnimalInfo
                              {
                                  Arete = info.Field<string>("Arete"),
                                  AreteMetalico = info.Field<string>("AreteMetalico")
                              }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return animalInfo;
        }

        internal  static List<AnimalInfo> ObtenerListadoAnimales(DataSet ds)
        {
            AnimalInfo animalInfo;
            List<AnimalInfo> listaAnimales;

            try
            {
                Logger.Info();
                listaAnimales = new List<AnimalInfo>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    animalInfo = new AnimalInfo();
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
                    if (dr["CausaRechadoID"] is DBNull)
                    {
                        animalInfo.CausaRechadoID = 0;
                    }
                    else
                    {
                        animalInfo.CausaRechadoID = Convert.ToInt32(dr["CausaRechadoID"]);
                    }
                    animalInfo.Venta = Convert.ToBoolean(dr["Venta"]);
                    animalInfo.Cronico = Convert.ToBoolean(dr["Cronico"]);
                    animalInfo.Activo = Convert.ToBoolean(dr["Activo"]);
                    animalInfo.UsuarioCreacionID = (int) (dr["UsuarioCreacionID"] is DBNull ? 0 : dr["UsuarioCreacionID"]);
                    animalInfo.UsuarioModificacionID = (int)(dr["UsuarioModificacionID"] is DBNull ? 0 : dr["UsuarioModificacionID"]);
                    animalInfo.TipoGanado = new TipoGanadoInfo
                    {
                        TipoGanadoID = Convert.ToInt32(dr["TipoGanadoID"]),
                        Sexo = ((Convert.ToChar(dr["Sexo"]).CompareTo('M') == 0) ? Sexo.Macho : Sexo.Hembra),
                    };

                    listaAnimales.Add(animalInfo);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return listaAnimales;
        }

        internal  static List<AnimalSalidaInfo> ObtenerListadoAnimalesSalida(DataSet ds)
        {
            AnimalSalidaInfo animalSalidaInfo;
            List<AnimalSalidaInfo> listaAnimales;

            try
            {
                Logger.Info();
                listaAnimales = new List<AnimalSalidaInfo>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    animalSalidaInfo = new AnimalSalidaInfo
                                           {
                                               AnimalId = Convert.ToInt32(dr["AnimalID"]),
                                               AnimalSalidaId = Convert.ToInt32(dr["AnimalSalidaID"]),
                                               LoteId = Convert.ToInt32(dr["LoteID"]),
                                               CorraletaId = Convert.ToInt32(dr["CorraletaID"]),
                                               TipoMovimientoId = Convert.ToInt32(dr["TipoMovimientoID"])
                                           };
                    listaAnimales.Add(animalSalidaInfo);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return listaAnimales;
        }

        internal  static AnimalSalidaInfo ObtenerAnimalSalida(DataSet ds)
        {
            AnimalSalidaInfo animalSalidaInfo = null;

            try
            {
                Logger.Info();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    animalSalidaInfo = new AnimalSalidaInfo
                    {
                        AnimalId = Convert.ToInt32(dr["AnimalID"]),
                        LoteId = Convert.ToInt32(dr["LoteID"]),
                        CorralId = Convert.ToInt32(dr["CorralID"]),
                        CorraletaId = Convert.ToInt32(dr["CorraletaID"])
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return animalSalidaInfo;
        }

        internal static AnimalSalidaInfo ObtenerAnimalSalidaDatos(DataSet ds)
        {
            AnimalSalidaInfo animalSalidaInfo = null;

            try
            {
                Logger.Info();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    animalSalidaInfo = new AnimalSalidaInfo
                    {
                        AnimalId = Convert.ToInt32(dr["AnimalID"]),
                        LoteId = Convert.ToInt32(dr["LoteID"]),
                        CorraletaId = Convert.ToInt32(dr["CorraletaID"])
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return animalSalidaInfo;
        }

        internal  static List<AnimalInfo> ObtenerAreteCorraleta(DataSet ds)
        {
            AnimalInfo animalInfo;
            List<AnimalInfo> listaAnimales;

            try
            {
                Logger.Info();
                listaAnimales = new List<AnimalInfo>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    animalInfo = new AnimalInfo
                    {
                        AnimalID = Convert.ToInt32(dr["AnimalID"]),
                        Arete = Convert.ToString(dr["Arete"])
                    };
                    listaAnimales.Add(animalInfo);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return listaAnimales;
        }

        internal  static AnimalSalidaInfo ObtenerAnimalSalidaInfo(DataSet ds)
        {
            AnimalSalidaInfo animalSalidaInfo = null;
            try
            {
                Logger.Info();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    animalSalidaInfo = new AnimalSalidaInfo
                                           {
                                               AnimalId = Convert.ToInt32(dr["AnimalID"]),
                                               AnimalSalidaId = Convert.ToInt32(dr["AnimalSalidaID"]),
                                               LoteId = Convert.ToInt32(dr["LoteID"]),
                                               CorraletaId = Convert.ToInt32(dr["CorraletaID"])
                                           };
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return animalSalidaInfo;
        }

        internal static string ObtenerExisteAreteSalida(DataSet ds)
        {
            var retorno = string.Empty;
            try
            {
                Logger.Info();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    retorno = dr["Arete"].ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return retorno;
        }

        /// <summary>
        /// Map para obtener los animales por folio Entrada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<AnimalInfo> ObtenerAnimalesPorFolioEntrada(DataSet ds)
        {
            AnimalInfo animalInfo;
            List<AnimalInfo> listaAnimales;

            try
            {
                Logger.Info();
                listaAnimales = new List<AnimalInfo>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    animalInfo = new AnimalInfo
                    {
                        AnimalID = Convert.ToInt64(dr["AnimalID"]),
                        Arete = Convert.ToString(dr["Arete"]),
                        PesoCompra = Convert.ToInt32(dr["PesoCompra"]),
                        TipoGanadoID = Convert.ToInt32(dr["TipoGanadoID"]),
                        OrganizacionIDEntrada = Convert.ToInt32(dr["OrganizacionIDEntrada"]),
                        FolioEntrada = Convert.ToInt32(dr["FolioEntrada"]),
                        Activo = Convert.ToBoolean(dr["Activo"])
                    };
                    listaAnimales.Add(animalInfo);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return listaAnimales;
        }

        /// <summary>
        /// Obtiene un objeto con una lista de
        /// movimientos de animal
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<AnimalInfo> ObtenerUltimoMovimientoPagina(DataSet ds)
        {
            ResultadoInfo<AnimalInfo> resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                List<AnimalInfo> movimientos = (from info in dt.AsEnumerable()
                                                select new AnimalInfo
                                                           {
                                                               AnimalID = info.Field<long>("AnimalID"),
                                                               Arete = info.Field<string>("Arete"),
                                                               AreteMetalico = info.Field<string>("AreteMetalico")
                                                           }).ToList();
                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtDetalle].Rows[0]["TotalReg"]);
                resultado = new ResultadoInfo<AnimalInfo>
                                {
                                    Lista = movimientos,
                                    TotalRegistros = totalRegistros
                                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene un animal por su arete
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static AnimalInfo ObtenerAnimalesMuertos(DataSet ds)
        {
            AnimalInfo resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                resultado = (from info in dt.AsEnumerable()
                             select new AnimalInfo
                                        {
                                            AnimalID = info.Field<long>("AnimalID"),
                                            Arete = info.Field<string>("Arete"),
                                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene una lista de Animales por su Disponibilidad
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<AnimalMovimientoInfo> ObtenerAnimalesPorLoteReimplante(DataSet ds)
        {
            List<AnimalMovimientoInfo> listaAnimalesMovimiento;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                listaAnimalesMovimiento = (from movimiento in dt.AsEnumerable()
                                               select new AnimalMovimientoInfo
                                               {
                                                   AnimalID = movimiento.Field<long>("AnimalID"),
                                                   AnimalMovimientoID = movimiento.Field<long>("AnimalMovimientoID"),
                                                   OrganizacionID = movimiento.Field<int>("OrganizacionID"),
                                                   CorralID = movimiento.Field<int>("CorralID"),
                                                   LoteID = movimiento.Field<int>("LoteID"),
                                                   FechaMovimiento = movimiento.Field<DateTime>("FechaMovimiento"),
                                                   Peso = movimiento.Field<int>("Peso"),
                                                   Temperatura = Convert.ToDouble(movimiento["Temperatura"]),
                                                   TipoMovimientoID = movimiento.Field<int>("TipoMovimientoID"),
                                                   TrampaID = movimiento.Field<int>("TrampaID"),
                                                   OperadorID = movimiento.Field<int>("OperadorID"),
                                                   Observaciones = movimiento.Field<string>("Observaciones")
                                               }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return listaAnimalesMovimiento;
        }

        internal static int ObtenerDiasUltimaDeteccion(DataSet ds)
        {
            int resultado = 0;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                
                foreach (var registro in dt.AsEnumerable())
                {
                    resultado = registro.Field<int>("Dias");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        internal static List<AnimalInfo> ObtenerAnimalesPorLoteID(DataSet ds)
        {
            AnimalInfo animalInfo;
            List<AnimalInfo> listaAnimales;

            try
            {
                Logger.Info();
                listaAnimales = new List<AnimalInfo>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    animalInfo = new AnimalInfo
                                     {
                                         AnimalID = Convert.ToInt32(dr["AnimalID"]),
                                         Arete = Convert.ToString(dr["Arete"]),
                                         AreteMetalico = Convert.ToString(dr["AreteMetalico"]),
                                         FechaCompra = Convert.ToDateTime(dr["FechaCompra"]),
                                         TipoGanadoID = Convert.ToInt32(dr["TipoGanadoID"]),
                                         CalidadGanadoID = Convert.ToInt32(dr["CalidadGanadoID"]),
                                         ClasificacionGanadoID = Convert.ToInt32(dr["ClasificacionGanadoID"]),
                                         PesoCompra = Convert.ToInt32(dr["PesoCompra"]),
                                         OrganizacionIDEntrada = Convert.ToInt32(dr["OrganizacionIDEntrada"]),
                                         FolioEntrada = Convert.ToInt32(dr["FolioEntrada"]),
                                         PesoLlegada = Convert.ToInt32(dr["PesoLlegada"]),
                                         Paletas = Convert.ToInt32(dr["Paletas"])
                                     };
                    if (dr["CausaRechadoID"] is DBNull)
                    {
                        animalInfo.CausaRechadoID = 0;
                    }
                    else
                    {
                        animalInfo.CausaRechadoID = Convert.ToInt32(dr["CausaRechadoID"]);
                    }
                    animalInfo.Venta = Convert.ToBoolean(dr["Venta"]);
                    animalInfo.Cronico = Convert.ToBoolean(dr["Cronico"]);
                    animalInfo.Activo = Convert.ToBoolean(dr["Activo"]);
                    animalInfo.UsuarioCreacionID = (int)(dr["UsuarioCreacionID"] is DBNull ? 0 : dr["UsuarioCreacionID"]);
                    animalInfo.UsuarioModificacionID = (int)(dr["UsuarioModificacionID"] is DBNull ? 0 : dr["UsuarioModificacionID"]);

                    listaAnimales.Add(animalInfo);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return listaAnimales;
        }

        /// <summary>
        /// Obtiene una lista de animales movimiento por su lote
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        internal static List<AnimalInfo> ObtenerPorLoteXML(IDataReader reader)
        {
            var animales = new List<AnimalInfo>();
            try
            {
                Logger.Info();
                AnimalInfo animal;
                while (reader.Read())
                {
                    animal = new AnimalInfo
                                 {
                                     AnimalID = Convert.ToInt64(reader["AnimalID"]),
                                     Arete = Convert.ToString(reader["Arete"]),
                                     AreteMetalico = Convert.ToString(reader["AreteMetalico"]),
                                     FechaCompra = Convert.ToDateTime(reader["FechaCompra"]),
                                     TipoGanado = new TipoGanadoInfo
                                                      {
                                                          TipoGanadoID = Convert.ToInt32(reader["TipoGanadoID"]),
                                                          Descripcion = Convert.ToString(reader["TipoGanado"])
                                                      },
                                     CalidadGanadoID = Convert.ToInt32(reader["CalidadGanadoID"]),
                                     CalidadGanado = new CalidadGanadoInfo
                                                         {
                                                             CalidadGanadoID = Convert.ToInt32(reader["CalidadGanadoID"]),
                                                             Sexo = Convert.ToString(reader["Sexo"]).StringASexoEnum()
                                                         },
                                     ClasificacionGanado = new ClasificacionGanadoInfo
                                                               {
                                                                   ClasificacionGanadoID = Convert.ToInt32(reader["ClasificacionGanadoID"]),
                                                                   Descripcion = Convert.ToString(reader["ClasificacionGanado"])
                                                               },
                                     PesoCompra = Convert.ToInt32(reader["PesoCompra"]),
                                     OrganizacionIDEntrada = Convert.ToInt32(reader["OrganizacionIDEntrada"]),
                                     FolioEntrada = Convert.ToInt32(reader["FolioEntrada"]),
                                     PesoLlegada = Convert.ToInt32(reader["PesoLlegada"]),
                                     Paletas = Convert.ToInt32(reader["Paletas"]),
                                 };
                    animales.Add(animal);
                }
                reader.NextResult();
                var movimientos = new List<AnimalMovimientoInfo>();
                AnimalMovimientoInfo movimiento;
                while (reader.Read())
                {
                    movimiento = new AnimalMovimientoInfo
                                     {
                                         AnimalID = Convert.ToInt64(reader["AnimalID"]),
                                         AnimalMovimientoID = Convert.ToInt64(reader["AnimalMovimientoID"]),
                                         OrganizacionID = Convert.ToInt32(reader["OrganizacionID"]),
                                         CorralID = Convert.ToInt32(reader["CorralID"]),
                                         LoteID = Convert.ToInt32(reader["LoteID"]),
                                     };
                    movimientos.Add(movimiento);
                }
                animales.ForEach(datos =>
                                     {
                                         datos.ListaAnimalesMovimiento =
                                             movimientos.Where(
                                                 id => id.AnimalID == datos.AnimalID).ToList();
                                     });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return animales;
        }

        /// <summary>
        /// Obtiene una lista de Animales por su Disponibilidad
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<AnimalInfo> ObtenerAnimalesPorLoteXML(DataSet ds)
        {
            List<AnimalInfo> animalInfo;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                animalInfo = (from info in dt.AsEnumerable()
                              select new AnimalInfo
                              {
                                  AnimalID = info.Field<long>("AnimalID"),
                                  Arete = info.Field<string>("Arete"),
                                  AreteMetalico = info.Field<string>("AreteMetalico"),
                                  FechaCompra = info.Field<DateTime>("FechaCompra"),
                                  FechaEntrada = info.Field<DateTime>("FechaEntrada"),
                                  TipoGanado = new TipoGanadoInfo
                                  {
                                      TipoGanadoID = info.Field<int>("TipoGanadoID"),
                                      Descripcion = info.Field<string>("TipoGanado"),
                                      Sexo = info.Field<string>("Sexo")[0] == 'H' ? Info.Enums.Sexo.Hembra : Info.Enums.Sexo.Macho,
                                  },
                                  CalidadGanadoID = info.Field<int>("CalidadGanadoID"),
                                  ClasificacionGanado = new ClasificacionGanadoInfo
                                  {
                                      ClasificacionGanadoID = info.Field<int>("ClasificacionGanadoID"),
                                      Descripcion = info.Field<string>("ClasificacionGanado"),
                                  },
                                  PesoCompra = info.Field<int>("PesoCompra"),
                                  OrganizacionIDEntrada = info.Field<int>("OrganizacionIDEntrada"),
                                  FolioEntrada = Convert.ToInt32(info["FolioEntrada"]),
                                  PesoLlegada = info.Field<int>("PesoLlegada"),
                                  Paletas = info.Field<int>("Paletas"),
                                  CausaRechadoID = info.Field<int?>("CausaRechadoID") != null ? info.Field<int>("CausaRechadoID") : 0,
                                  Venta = info.Field<bool>("Venta"),
                                  Cronico = info.Field<bool>("Cronico"),
                                  LoteID = info.Field<int>("LoteID"),
                              }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return animalInfo;
        }

        /// <summary>
        /// Obtiene una lista de animales
        /// sacrificados
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<AnimalInfo> ObtenerAnimalesPorLoteSacrificadosXML(DataSet ds)
        {
            List<AnimalInfo> animalInfo;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                animalInfo = (from info in dt.AsEnumerable()
                              select new AnimalInfo
                                         {
                                             AnimalID = info.Field<long>("AnimalID"),
                                             LoteID = info.Field<int>("LoteID"),
                                         }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return animalInfo;
        }

        /// <summary>
        /// Obtiene una lista de animales por
        /// arete y organizacion
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<AnimalInfo> ObtenerAnimalesPorAreteOrganizacionXML(DataSet ds)
        {
            List<AnimalInfo> animalInfo;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                animalInfo = (from info in dt.AsEnumerable()
                              select new AnimalInfo
                              {
                                  AnimalID = info.Field<long>("AnimalID"),
                                  Arete = info.Field<string>("Arete"),
                                  AreteMetalico = info.Field<string>("AreteMetalico"),
                                  FechaCompra = info.Field<DateTime>("FechaCompra"),
                                  TipoGanado = new TipoGanadoInfo
                                  {
                                      TipoGanadoID = info.Field<int>("TipoGanadoID"),
                                  },
                                  CalidadGanadoID = info.Field<int>("CalidadGanadoID"),
                                  ClasificacionGanado = new ClasificacionGanadoInfo
                                  {
                                      ClasificacionGanadoID = info.Field<int>("ClasificacionGanadoID"),
                                  },
                                  PesoCompra = info.Field<int>("PesoCompra"),
                                  OrganizacionIDEntrada = info.Field<int>("OrganizacionIDEntrada"),
                                  FolioEntrada = Convert.ToInt32(info["FolioEntrada"]),
                                  PesoLlegada = info.Field<int>("PesoLlegada"),
                                  Paletas = info.Field<int>("Paletas"),
                                  CausaRechadoID = info.Field<int?>("CausaRechadoID") ?? 0,
                                  Venta = info.Field<bool>("Venta"),
                                  Cronico = info.Field<bool>("Cronico"),
                              }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return animalInfo;

        }

        /// <summary>
        /// Obtiene una lista de animales
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        internal static List<AnimalInfo> ObtenerPorXML(IDataReader reader)
        {
            var animales = new List<AnimalInfo>();
            try
            {
                Logger.Info();
                AnimalInfo animal;
                while (reader.Read())
                {
                    animal = new AnimalInfo
                    {
                        AnimalID = Convert.ToInt64(reader["AnimalID"]),
                        Arete = Convert.ToString(reader["Arete"]),
                        AreteMetalico = Convert.ToString(reader["AreteMetalico"]),
                        FechaCompra = Convert.ToDateTime(reader["FechaCompra"]),
                        TipoGanado = new TipoGanadoInfo
                        {
                            TipoGanadoID = Convert.ToInt32(reader["TipoGanadoID"]),
                            Descripcion = Convert.ToString(reader["TipoGanado"])
                        },
                        CalidadGanadoID = Convert.ToInt32(reader["CalidadGanadoID"]),
                        CalidadGanado = new CalidadGanadoInfo
                        {
                            CalidadGanadoID = Convert.ToInt32(reader["CalidadGanadoID"]),
                            Sexo = Convert.ToString(reader["Sexo"]).StringASexoEnum()
                        },
                        ClasificacionGanado = new ClasificacionGanadoInfo
                        {
                            ClasificacionGanadoID = Convert.ToInt32(reader["ClasificacionGanadoID"]),
                            Descripcion = Convert.ToString(reader["ClasificacionGanado"])
                        },
                        PesoCompra = Convert.ToInt32(reader["PesoCompra"]),
                        OrganizacionIDEntrada = Convert.ToInt32(reader["OrganizacionIDEntrada"]),
                        FolioEntrada = Convert.ToInt32(reader["FolioEntrada"]),
                        PesoLlegada = Convert.ToInt32(reader["PesoLlegada"]),
                        Paletas = Convert.ToInt32(reader["Paletas"]),
                    };
                    animales.Add(animal);
                }
                reader.NextResult();
                var movimientos = new List<AnimalMovimientoInfo>();
                AnimalMovimientoInfo movimiento;
                while (reader.Read())
                {
                    movimiento = new AnimalMovimientoInfo
                    {
                        AnimalID = Convert.ToInt64(reader["AnimalID"]),
                        AnimalMovimientoID = Convert.ToInt64(reader["AnimalMovimientoID"]),
                        OrganizacionID = Convert.ToInt32(reader["OrganizacionID"]),
                        CorralID = Convert.ToInt32(reader["CorralID"]),
                        LoteID = Convert.ToInt32(reader["LoteID"]),
                    };
                    movimientos.Add(movimiento);
                }
                animales.ForEach(datos =>
                {
                    datos.ListaAnimalesMovimiento =
                        movimientos.Where(
                            id => id.AnimalID == datos.AnimalID).ToList();
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return animales;
        }

        /// <summary>
        /// Obtiene el valor retornado en la consulta de verificacion de arete
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static bool VerificarExistenciaArete(DataSet ds)
        {
            bool Encontrado = false;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                if (dt.Rows[0][0].ToString() == "1")
                {
                    Encontrado = true;
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return Encontrado;

        }

        internal static List<ControlSacrificioInfo.SincronizacionSIAP> PlancharAretesObtenerAnimalId(DataSet ds)
        {
            List<ControlSacrificioInfo.SincronizacionSIAP> planchadoInfo;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                planchadoInfo = (from info in dt.AsEnumerable()
                                  select new ControlSacrificioInfo.SincronizacionSIAP
                                  {
                                      Arete = info.Field<string>("Arete"),
                                      AnimalID = info.Field<long?>("AnimalID")
                                  }).ToList();
                return planchadoInfo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static List<AnimalInfo> ObtenerAnimalPorAretes(DataSet ds)
        {
            var listaAnimalInfo = new List<AnimalInfo>();
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                listaAnimalInfo = (from info in dt.AsEnumerable()
                                   select new AnimalInfo
                                   {
                                       AnimalID = info.Field<long>("AnimalID"),
                                       Arete = info.Field<string>("Arete"),
                                       AreteMetalico = info.Field<string>("AreteMetalico"),
                                       FechaCompra = info.Field<DateTime>("FechaCompra"),
                                       TipoGanadoID = info.Field<int>("TipoGanadoID"),
                                       CalidadGanadoID = info.Field<int>("CalidadGanadoID"),
                                       ClasificacionGanadoID = info.Field<int>("ClasificacionGanadoID"),
                                       PesoCompra = info.Field<int>("PesoCompra"),
                                       OrganizacionIDEntrada = info.Field<int>("OrganizacionIDEntrada"),
                                       FolioEntrada = Convert.ToInt32(info["FolioEntrada"]),
                                       PesoLlegada = info.Field<int>("PesoLlegada"),
                                       Paletas = info.Field<int>("Paletas"),
                                       Venta = info.Field<bool>("Venta"),
                                       Cronico = info.Field<bool>("Cronico"),
                                       Activo = info.Field<bool>("Activo") ? Convert.ToBoolean(EstatusEnum.Inactivo) : Convert.ToBoolean(EstatusEnum.Activo),
                                       FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                                       UsuarioCreacionID = info.Field<int>("UsuarioCreacionID"),
                                       Historico = info.Field<int>("Historico") != 0,
                                       FechaEntrada = info.Field<DateTime>("FechaEntrada"),
                                       
                                       Proveedor = new ProveedorInfo()
                                       {
                                           Descripcion = string.IsNullOrEmpty(info.Field<string>("Proveedor")) ? string.Empty : info.Field<string>("Proveedor")
                                       },
                                       UltimoMovimiento = new AnimalMovimientoInfo()
                                       {
                                           AnimalMovimientoID = info.Field<long?>("AnimalMovimientoID") != null ? info.Field<long>("AnimalMovimientoID") : 0,
                                           FechaMovimiento = info.Field<DateTime?>("FechaMovimiento") != null ? info.Field<DateTime>("FechaMovimiento") : new DateTime(),
                                           TipoMovimiento = new TipoMovimientoInfo()
                                           {
                                               TipoMovimientoID = info.Field<int?>("TipoMovimientoID") != null ? info.Field<int>("TipoMovimientoID") : 0,
                                               Descripcion = info.Field<string>("Descripcion") ?? String.Empty,
                                           },
                                       },
                                       Origen = info.Field<string>("Origen")

                                   }).ToList();


            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return listaAnimalInfo;
        }

        /// <summary>
        /// Metodo para Mapear los Animales del corral de deteccion
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<AnimalInfo> ObtenerAnimalesPorCorralDeteccion(DataSet ds)
        {
            List<AnimalInfo> animalInfo;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                animalInfo = (from info in dt.AsEnumerable()
                              select new AnimalInfo
                              {
                                  Arete = info.Field<string>("Arete"),
                                  AreteMetalico = info.Field<string>("AreteMetalico")
                              }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return animalInfo;
        }
        /// Metodo para Mapear los Animales del corral de deteccion
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static AnimalInfo ObtenerAnimalAntiguoCorral(DataSet ds)
        {
            AnimalInfo animalInfo;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                animalInfo = (from info in dt.AsEnumerable()
                              select new AnimalInfo
                              {
                                  AnimalID = info.Field<long>("AnimalID"),
                                  Arete = info.Field<string>("Arete"),
                                  AreteMetalico = info.Field<string>("AreteMetalico"),
                                  FechaCompra = info.Field<DateTime>("FechaCompra"),
                                  TipoGanadoID = info.Field<int>("TipoGanadoID"),
                                  CalidadGanadoID = info.Field<int>("CalidadGanadoID"),
                                  ClasificacionGanadoID = info.Field<int>("ClasificacionGanadoID"),
                                  PesoCompra = info.Field<int>("PesoCompra"),
                                  OrganizacionIDEntrada = info.Field<int>("OrganizacionIDEntrada"),
                                  FolioEntrada = Convert.ToInt32(info.Field<long>("FolioEntrada")),
                                  PesoLlegada = info.Field<int>("PesoLlegada"),
                                  Paletas = info.Field<int>("Paletas"),
                                  CausaRechadoID = info.Field<int?>("CausaRechadoID") ?? 0,
                                  Venta = info.Field<bool>("Venta"),
                                  Cronico = info.Field<bool>("Cronico"),
                                  CambioSexo = info.Field<bool>("CambioSexo"),
                              }).FirstOrDefault();
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
