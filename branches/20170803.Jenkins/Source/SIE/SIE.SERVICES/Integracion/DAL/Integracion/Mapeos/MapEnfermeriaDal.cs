using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal  class MapEnfermeriaDal
    {
        /// <summary>
        /// Obtiene los datos de los animales que se encuentran enfermos de un corral determinado
        /// </summary>
        /// <param name="ds">Data set</param>
        /// <param name="organizacionId">Identificador </param>
        /// <returns>Lista de animales enfermos de un corral determinado</returns>
        internal  static IList<AnimalDeteccionInfo> ObtenerAnimalesEnfermeriaPorCorral(DataSet ds, int organizacionId)
        {
            IList<AnimalDeteccionInfo> animalEnfermeriaInfo;
            try
            {
                Logger.Info();

                var configuracionParametrosDal = new ConfiguracionParametrosDAL();
                var parametro = new ConfiguracionParametrosInfo
                {
                    OrganizacionID = organizacionId,
                    TipoParametro = (int)TiposParametrosEnum.Imagenes,
                    Clave = ParametrosEnum.ubicacionFotos.ToString()
                };
                var operadorDal = new OperadorDAL();
                var problemas = new ProblemaDAL();
                var corralDal = new CorralDAL();
                var parRuta = configuracionParametrosDal.ObtenerPorOrganizacionTipoParametroClave(parametro);
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                animalEnfermeriaInfo = (from info in dt.AsEnumerable()
                                        select new AnimalDeteccionInfo
                                        {
                                            DeteccionID = info.Field<int>("DeteccionID"),
                                            RutaFotoDeteccion = parRuta.Valor + info.Field<string>("FotoDeteccion"),
                                            Problemas =  problemas.ObtenerProblemasDeteccion(new AnimalDeteccionInfo{DeteccionID = info.Field<int>("DeteccionID"), EstatusDeteccion = 0},null),
                                            GradoEnfermedad = new GradoInfo{Descripcion = info.Field<string>("DescripcionGrado"), GradoID = info.Field<int>("GradoID"), NivelGravedad = info.Field<string>("NivelGravedad")} ,
                                            NombreDetector = info.Field<string>("NombreDetector"),
                                            DescripcionGanado =  new DescripcionGanadoInfo
                                            {
                                                DescripcionGanadoID = info.Field<int>("DescripcionGanadoID"),
                                                Descripcion = info.Field<string>("DescripcionGanado")
                                            },
                                            Detector = operadorDal.ObtenerPorID(info.Field<int>("OperadorID")),
                                            FechaDeteccion = info.Field<DateTime>("FechaDeteccion"),
                                            Animal =  new AnimalInfo{Arete = info["Arete"]== DBNull.Value ? null: info.Field<string>("Arete"),
                                                                     AreteMetalico = info["AreteMetalico"]== DBNull.Value ? null: info.Field<string>("AreteMetalico")
                                                                    },
                                            EnfermeriaCorral = info["CorralID"] == DBNull.Value ? null : new EnfermeriaInfo
                                            {
                                                FolioEntrada = info["FolioEntrada"] == DBNull.Value ? 0: info.Field<int>("FolioEntrada"),
                                                Corral = info["CorralID"] == DBNull.Value ? null : corralDal.ObtenerPorId(info.Field<int>("CorralID"))
                                            },
                                            
                                            
                                        }).ToList();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return animalEnfermeriaInfo;

        }

        /// <summary>
        /// Obtiene los datos de los animales que se encuentran enfermos de un corral determinado
        /// </summary>
        /// <param name="ds">Data set</param>
        /// <param name="organizacionId">Identificador </param>
        /// <returns>Lista de animales enfermos de un corral determinado</returns>
        internal static IList<AnimalDeteccionInfo> ObtenerAnimalesEnfermeriaPorCorralSinActivo(DataSet ds, int organizacionId)
        {
            IList<AnimalDeteccionInfo> animalEnfermeriaInfo;
            try
            {
                Logger.Info();

                var configuracionParametrosDal = new ConfiguracionParametrosDAL();
                var parametro = new ConfiguracionParametrosInfo
                {
                    OrganizacionID = organizacionId,
                    TipoParametro = (int)TiposParametrosEnum.Imagenes,
                    Clave = ParametrosEnum.ubicacionFotos.ToString()
                };
                var operadorDal = new OperadorDAL();
                var problemas = new ProblemaDAL();
                var corralDal = new CorralDAL();
                var parRuta = configuracionParametrosDal.ObtenerPorOrganizacionTipoParametroClave(parametro);
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                animalEnfermeriaInfo = (from info in dt.AsEnumerable()
                                        select new AnimalDeteccionInfo
                                        {
                                            DeteccionID = info.Field<int>("DeteccionID"),
                                            RutaFotoDeteccion = parRuta.Valor + info.Field<string>("FotoDeteccion"),
                                            Problemas = problemas.ObtenerProblemasDeteccionSinActivo(new AnimalDeteccionInfo { DeteccionID = info.Field<int>("DeteccionID"), EstatusDeteccion = 0 }, null),
                                            GradoEnfermedad = new GradoInfo { Descripcion = info.Field<string>("DescripcionGrado"), GradoID = info.Field<int>("GradoID"), NivelGravedad = info.Field<string>("NivelGravedad") },
                                            NombreDetector = info.Field<string>("NombreDetector"),
                                            DescripcionGanado = new DescripcionGanadoInfo
                                            {
                                                DescripcionGanadoID = info.Field<int>("DescripcionGanadoID"),
                                                Descripcion = info.Field<string>("DescripcionGanado")
                                            },
                                            Detector = operadorDal.ObtenerPorID(info.Field<int>("OperadorID")),
                                            FechaDeteccion = info.Field<DateTime>("FechaDeteccion"),
                                            Animal = new AnimalInfo
                                            {
                                                Arete = info["Arete"] == DBNull.Value ? null : info.Field<string>("Arete"),
                                                AreteMetalico = info["AreteMetalico"] == DBNull.Value ? null : info.Field<string>("AreteMetalico")
                                            },
                                            EnfermeriaCorral = info["CorralID"] == DBNull.Value ? null : new EnfermeriaInfo
                                            {
                                                FolioEntrada = info["FolioEntrada"] == DBNull.Value ? 0 : info.Field<int>("FolioEntrada"),
                                                Corral = info["CorralID"] == DBNull.Value ? null : corralDal.ObtenerPorId(info.Field<int>("CorralID"))
                                            },


                                        }).ToList();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return animalEnfermeriaInfo;

        }

        /// <summary>
        /// Metodo para obtener la ultima deteccion que sele realizo al animal
        /// </summary>
        /// <param name="ds">Data set</param>
        /// <param name="organizacionId">Identificador </param>
        /// <returns>Lista de animales enfermos de un corral determinado</returns>
        internal static IList<AnimalDeteccionInfo> ObtenerAnimalDetectadoPorAreteUltimaDeteccion(DataSet ds, int organizacionId)
        {
            IList<AnimalDeteccionInfo> animalEnfermeriaInfo;
            try
            {
                Logger.Info();

                var configuracionParametrosDal = new ConfiguracionParametrosDAL();
                var parametro = new ConfiguracionParametrosInfo
                {
                    OrganizacionID = organizacionId,
                    TipoParametro = (int)TiposParametrosEnum.Imagenes,
                    Clave = ParametrosEnum.ubicacionFotos.ToString()
                };
                var operadorDal = new OperadorDAL();
                var problemas = new ProblemaDAL();
                var corralDal = new CorralDAL();
                var parRuta = configuracionParametrosDal.ObtenerPorOrganizacionTipoParametroClave(parametro);
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                animalEnfermeriaInfo = (from info in dt.AsEnumerable()
                                        select new AnimalDeteccionInfo
                                        {
                                            DeteccionID = info.Field<int>("DeteccionID"),
                                            RutaFotoDeteccion = parRuta.Valor + info.Field<string>("FotoDeteccion"),
                                            Problemas = problemas.ObtenerProblemasDeteccionUltimaDeteccion(new AnimalDeteccionInfo { DeteccionID = info.Field<int>("DeteccionID"), EstatusDeteccion = 0 }, null),
                                            GradoEnfermedad = new GradoInfo { Descripcion = info.Field<string>("DescripcionGrado"), GradoID = info.Field<int>("GradoID"), NivelGravedad = info.Field<string>("NivelGravedad") },
                                            NombreDetector = info.Field<string>("NombreDetector"),
                                            DescripcionGanado = new DescripcionGanadoInfo
                                            {
                                                DescripcionGanadoID = info.Field<int>("DescripcionGanadoID"),
                                                Descripcion = info.Field<string>("DescripcionGanado")
                                            },
                                            Detector = operadorDal.ObtenerPorID(info.Field<int>("OperadorID")),
                                            FechaDeteccion = info.Field<DateTime>("FechaDeteccion"),
                                            Animal = new AnimalInfo
                                            {
                                                Arete = info["Arete"] == DBNull.Value ? null : info.Field<string>("Arete"),
                                                AreteMetalico = info["AreteMetalico"] == DBNull.Value ? null : info.Field<string>("AreteMetalico")
                                            },
                                            EnfermeriaCorral = info["CorralID"] == DBNull.Value ? null : new EnfermeriaInfo
                                            {
                                                FolioEntrada = info["FolioEntrada"] == DBNull.Value ? 0 : info.Field<int>("FolioEntrada"),
                                                Corral = info["CorralID"] == DBNull.Value ? null : corralDal.ObtenerPorId(info.Field<int>("CorralID"))
                                            },


                                        }).ToList();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return animalEnfermeriaInfo;

        }


        /// <summary>
        /// Obtiene los corrales en los que fueron detectados animales enfermo
        /// </summary>
        /// <param name="ds">Data set que contiene los datos</param>
        /// <param name="organizacionId">Organizacion a la que pertenecen los corrales</param>
        /// <returns>Lista de los corrales con ganado enfermo</returns>
        internal  static ResultadoInfo<EnfermeriaInfo> ObtenerCorralesConGanadoDetectadoEnfermo(DataSet ds, int organizacionId)
        {
            ResultadoInfo<EnfermeriaInfo> CorralesConDeteccionDeEnfermos;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];


                IList<EnfermeriaInfo> lista = (from info in dt.AsEnumerable()
                                        select new EnfermeriaInfo
                                        {
                                            Corral = new CorralInfo { 
                                                CorralID = info.Field<int>("CorralID"), 
                                                Codigo = info.Field<string>("Codigo"), 
                                                TipoCorral = new TipoCorralInfo
                                                {
                                                    TipoCorralID = info.Field<int>("TipoCorralID"),
                                                    GrupoCorralID = info.Field<int>("GrupoCorralID")
                                                }},
                                            FolioEntrada = info["FolioEntrada"] == DBNull.Value ? 0 : info.Field<int>("FolioEntrada"),
                                            TotalCabezas = info.Field<int>("Cabezas")
                                        }).ToList();

                CorralesConDeteccionDeEnfermos = new ResultadoInfo<EnfermeriaInfo>
                {
                    Lista = lista,
                    TotalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
                };

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return CorralesConDeteccionDeEnfermos;
        }
        /// <summary>
        /// Mapea los datos de la lista de grados
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static IList<GradoInfo> ObtenerGrados(DataSet ds)
        {
            IList<GradoInfo> grados;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                grados = (from info in dt.AsEnumerable()
                          select new GradoInfo
                                               {
                                                   GradoID = info.Field<int>("GradoID"),
                                                   Descripcion = info.Field<string>("Descripcion"),
                                                   NivelGravedad = info.Field<string>("NivelGravedad")
                                              }).ToList();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return grados;
        }

        /// <summary>
        /// Mapea los datos de la lista de grados
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="tratamiento"></param>
        /// <returns></returns>
        internal  static IList<HistorialClinicoInfo> ObtenerHistorialClinico(DataSet ds,TratamientoInfo tratamiento)
        {
            IList<HistorialClinicoInfo> historial;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                var problemasDal = new ProblemaDAL();
                 
                historial = (from info in dt.AsEnumerable()
                             select new HistorialClinicoInfo
                          {
                              DeteccionId = info["DeteccionAnimalID"] == DBNull.Value ? 0 : info.Field<int>("DeteccionAnimalID"),
                              AnimalMovimientoId = info.Field<long>("AnimalMovimientoID"),
                              FechaEntrada = info.Field<DateTime>("FechaEntrada"),
                              FechaSalida = info["FechaSalida"] == DBNull.Value ? new DateTime(): info.Field<DateTime>("FechaSalida"),
                              Peso = info.Field<int>("Peso"),
                              Temperatura = info.Field<decimal>("Temperatura"),
                              Tratamiento = info.Field<string>("Tratamientos"),
                              CodigoTratamiento = info.Field<string>("CodigoTratamientos"),
                              GradoEnfermedad = new GradoInfo {
                                  GradoID = info["GradoID"] == DBNull.Value ? 0 : info.Field<int>("GradoID"),
                                  Descripcion = info["DescripcionGrado"] == DBNull.Value ? string.Empty : info.Field<string>("DescripcionGrado"),
                                  NivelGravedad = info["NivelGravedad"] == DBNull.Value ? string.Empty : info.Field<string>("NivelGravedad") 
                              },
                              ListaProblemas = problemasDal.ObtenerProblemasDeteccion(
                                    new AnimalDeteccionInfo
                                    {
                                        DeteccionID = info["DeteccionAnimalID"] == DBNull.Value ? 0 : info.Field<int>("DeteccionAnimalID"),
                                        EstatusDeteccion = 1
                                    }, 
                                    new TratamientoInfo
                                    {
                                        OrganizacionId = tratamiento.OrganizacionId, 
                                        Sexo = tratamiento.Sexo, 
                                        Peso = info.Field<int>("Peso"),
                                        CodigoTratamiento = tratamiento.CodigoTratamiento
                                    })
                          }).ToList();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return historial;
        }
        /// <summary>
        /// Obtiene la enfermeria por operador
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static List<EnfermeriaInfo> ObtenerEnfermeriasPorOperadorID(DataSet ds)
        {
            List<EnfermeriaInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new EnfermeriaInfo
                          {
                              EnfermeriaID = info.Field<int>("EnfermeriaID"),
                              Descripcion = info.Field<string>("Descripcion")
                          }).ToList();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        /// <summary>
        ///     Método para obtener los datos de la compra
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static DatosCompra ObtenerDatosCompra(DataSet ds)
        {
            DatosCompra datoscompra = null;
            try
            {

                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                datoscompra = new DatosCompra();
                foreach (DataRow dr in dt.Rows)
                {
                    datoscompra.FechaInicio = Convert.ToDateTime(dr["FechaEntrada"]);
                    datoscompra.Origen = Convert.ToString(dr["TipoOrganizacion"]);
                    datoscompra.Proveedor = Convert.ToString(dr["Proveedor"]);
                    datoscompra.TipoOrigen = Convert.ToInt32(dr["TipoOrganizacionID"]);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return datoscompra;
        }

        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<EnfermeriaInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<EnfermeriaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new EnfermeriaInfo
                         {
                             EnfermeriaID = info.Field<int>("EnfermeriaID"),
                             Organizacion = string.Format("{0}", info.Field<int>("OrganizacionID")),
                             OrganizacionInfo = new OrganizacionInfo {
                                 OrganizacionID = info.Field<int>("OrganizacionID"),
                                 Descripcion = info.Field<string>("Organizacion"),
                             },
                             Descripcion = info.Field<string>("Descripcion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<EnfermeriaInfo>
                    {
                        Lista = lista,
                        TotalRegistros = totalRegistros
                    };
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static EnfermeriaInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                EnfermeriaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new EnfermeriaInfo
                         {
                             EnfermeriaID = info.Field<int>("EnfermeriaID"),
                             Organizacion = string.Format("{0}", info.Field<int>("OrganizacionID")),
                             Descripcion = info.Field<string>("Descripcion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).First();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static EnfermeriaInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                EnfermeriaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new EnfermeriaInfo
                             {
                                 EnfermeriaID = info.Field<int>("EnfermeriaID"),
                                 OrganizacionInfo =
                                     new OrganizacionInfo
                                         {
                                             OrganizacionID = info.Field<int>("OrganizacionID"),
                                             Descripcion = info.Field<string>("Organizacion")
                                         },
                                 Descripcion = info.Field<string>("Descripcion"),
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).First();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
