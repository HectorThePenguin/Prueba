using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Services.Info.Info;
using System.Data;
using SIE.Services.Info.Constantes;
using SIE.Base.Log;
using SIE.Base.Exepciones;
using System.Reflection;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapInterfaceSalidaDAL
    {
        /// <summary>
        /// Obtiene una lista de objetos Inteface Salida
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<InterfaceSalidaInfo> ObtenerTodos(DataSet ds)
        {
            List<InterfaceSalidaInfo> interfacesSalidaInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                interfacesSalidaInfo = (from info in dt.AsEnumerable()
                                        select new InterfaceSalidaInfo
                                        {
                                            Activo = info.Field<bool>("Activo").BoolAEnum(),
                                            Cabezas = info.Field<int>("Cabezas"),
                                            EsRuteo = info.Field<bool>("EsRuteo"),
                                            FechaRegistro = info.Field<DateTime>("FechaRegistro"),
                                            FechaSalida = info.Field<DateTime>("FechaSalida"),
                                            OrganizacionDestino = new OrganizacionInfo
                                            {
                                                OrganizacionID = info.Field<int>("OrganizacionDestinoID"),
                                                Descripcion = info.Field<string>("OrganizacionDestinoDescripcion"),
                                                TipoOrganizacion = new TipoOrganizacionInfo
                                                    {
                                                        TipoOrganizacionID = info.Field<int>("TipoOrganizacionDestinoID")
                                                        ,
                                                        Descripcion = info.Field<string>("TipoOrganizacionDestinoDescripcion")
                                                    }
                                            },
                                            Organizacion = new OrganizacionInfo
                                            {
                                                OrganizacionID = info.Field<int>("OrganizacionID"),
                                                Descripcion = info.Field<string>("OrganizacionDescripcion"),
                                                TipoOrganizacion = new TipoOrganizacionInfo
                                                {
                                                    TipoOrganizacionID = info.Field<int>("TipoOrganizacionID")
                                                    ,
                                                    Descripcion = info.Field<string>("TipoOrganizacionDescripcion")
                                                }
                                            },
                                            SalidaID = info.Field<int>("SalidaID")
                                            ,
                                            ListaInterfaceDetalle = (from detalle in ds.Tables[ConstantesDAL.DtDetalle].AsEnumerable()
                                                                    select new InterfaceSalidaDetalleInfo
                                                                    {
                                                                        Cabezas = detalle.Field<int>("CabezasDetalle"),
                                                                        Importe = detalle.Field<decimal>("Importe"),
                                                                        PrecioKG = detalle.Field<decimal>("PrecioKG")
                                                    ,
                                                                        TipoGanado = new TipoGanadoInfo
                                                                        {
                                                                            TipoGanadoID = detalle.Field<int>("TipoGanadoID"),
                                                                            Descripcion = detalle.Field<string>("TipoGanadoDescripcion")
                                                                        },
                                                                        ListaInterfaceSalidaAnimal = (from animal in ds.Tables[ConstantesDAL.DtDetalleAnimal].AsEnumerable()
                                                                                                      select new InterfaceSalidaAnimalInfo
                                                                                                     {
                                                                                                         Arete = animal.Field<string>("Arete"),
                                                                                                         PesoCompra = animal.Field<decimal>("PesoCompra"),
                                                                                                         PesoOrigen = animal.Field<decimal>("PesoOrigen")
                                                                                                     }
                                                                                                ).ToList()
                                                                    }
                                                               ).ToList()
                                        }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return interfacesSalidaInfo;
        }

        /// <summary>
        /// Obtiene un Objeto Inteface Salida
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static InterfaceSalidaInfo ObtenerParametrosPorID(DataSet ds)
        {
            InterfaceSalidaInfo interfaceSalidaInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                interfaceSalidaInfo = (from info in dt.AsEnumerable()
                                       select new InterfaceSalidaInfo
                                       {
                                           Activo = info.Field<bool>("Activo").BoolAEnum(),
                                           Cabezas = info.Field<int>("Cabezas"),
                                           EsRuteo = info.Field<bool>("EsRuteo"),
                                           FechaRegistro = info.Field<DateTime>("FechaRegistro"),
                                           FechaSalida = info.Field<DateTime>("FechaSalida"),
                                           OrganizacionDestino = new OrganizacionInfo
                                           {
                                               OrganizacionID = info.Field<int>("OrganizacionDestinoID"),
                                               Descripcion = info.Field<string>("OrganizacionDestinoDescripcion"),
                                               TipoOrganizacion = new TipoOrganizacionInfo
                                               {
                                                   TipoOrganizacionID = info.Field<int>("TipoOrganizacionDestinoID")
                                                   ,
                                                   Descripcion = info.Field<string>("TipoOrganizacionDestinoDescripcion")
                                               }
                                           },
                                           Organizacion = new OrganizacionInfo
                                           {
                                               OrganizacionID = info.Field<int>("OrganizacionID"),
                                               Descripcion = info.Field<string>("OrganizacionDescripcion"),
                                               TipoOrganizacion = new TipoOrganizacionInfo
                                               {
                                                   TipoOrganizacionID = info.Field<int>("TipoOrganizacionID")
                                                   ,
                                                   Descripcion = info.Field<string>("TipoOrganizacionDescripcion")
                                               }
                                           },
                                           SalidaID = info.Field<int>("SalidaID")
                                           ,
                                           ListaInterfaceDetalle = (from detalle in ds.Tables[ConstantesDAL.DtDetalle].AsEnumerable()
                                                                   select new InterfaceSalidaDetalleInfo
                                                                   {
                                                                       Cabezas = detalle.Field<int>("CabezasDetalle"),
                                                                       Importe = detalle.Field<decimal>("Importe"),
                                                                       PrecioKG = detalle.Field<decimal>("PrecioKG")
                                                   ,
                                                                       TipoGanado = new TipoGanadoInfo
                                                                       {
                                                                           TipoGanadoID = detalle.Field<int>("TipoGanadoID"),
                                                                           Descripcion = detalle.Field<string>("TipoGanadoDescripcion")
                                                                       },
                                                                       ListaInterfaceSalidaAnimal = (from animal in ds.Tables[ConstantesDAL.DtDetalleAnimal].AsEnumerable()
                                                                                                     select new InterfaceSalidaAnimalInfo
                                                                                                     {
                                                                                                         Arete = animal.Field<string>("Arete"),
                                                                                                         PesoCompra = animal.Field<decimal>("PesoCompra"),
                                                                                                         PesoOrigen = animal.Field<decimal>("PesoOrigen")
                                                                                                     }
                                                                                               ).ToList()
                                                                   }
                                                              ).ToList()
                                       }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return interfaceSalidaInfo;
        }

        /// <summary>
        /// Obtiene un Objeto Inteface Salida
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static InterfaceSalidaInfo ObtenerPorSalidaOrganizacion(DataSet ds)
        {
            InterfaceSalidaInfo interfaceSalidaInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                interfaceSalidaInfo = (from info in dt.AsEnumerable()
                                       select new InterfaceSalidaInfo
                                       {
                                           Activo = info.Field<bool>("Activo").BoolAEnum(),
                                           Cabezas = info.Field<int>("Cabezas"),
                                           EsRuteo = info.Field<bool>("EsRuteo"),
                                           FechaRegistro = info.Field<DateTime>("FechaRegistro"),
                                           FechaSalida = info.Field<DateTime>("FechaSalida"),
                                           OrganizacionDestino = new OrganizacionInfo
                                           {
                                               OrganizacionID = info.Field<int>("OrganizacionDestinoID")
                                           },
                                           Organizacion = new OrganizacionInfo
                                           {
                                               OrganizacionID = info.Field<int>("OrganizacionID")
                                           },
                                           SalidaID = info.Field<int>("SalidaID"),
                                           ListaInterfaceDetalle = (from detalle in ds.Tables[ConstantesDAL.DtDetalle].AsEnumerable()
                                                                   select new InterfaceSalidaDetalleInfo
                                                                   {
                                                                       Cabezas = detalle.Field<int>("Cabezas"),
                                                                       Importe = detalle.Field<decimal>("Importe"),
                                                                       PrecioKG = detalle.Field<decimal>("PrecioKG"),
                                                                       SalidaID = detalle.Field<int>("SalidaID"),
                                                                       Organizacion = new OrganizacionInfo
                                                                           {
                                                                               OrganizacionID = detalle.Field<int>("OrganizacionID")
                                                                           },
                                                                       FechaRegistro = detalle.Field<DateTime>("FechaRegistro"),
                                                                       UsuarioRegistro = detalle.Field<string>("UsuarioRegistro"),
                                                                       TipoGanado = new TipoGanadoInfo
                                                                       {
                                                                           TipoGanadoID = detalle.Field<int>("TipoGanadoID"),
                                                                           Descripcion = detalle.Field<string>("TipoGanado")
                                                                       },
                                                                       ListaInterfaceSalidaAnimal = (from animal in ds.Tables[ConstantesDAL.DtDetalleAnimal].AsEnumerable()
                                                                                                     where detalle.Field<int>("OrganizacionID").Equals(animal.Field<int>("OrganizacionID"))
                                                                                                     && detalle.Field<int>("SalidaID").Equals(animal.Field<int>("SalidaID"))
                                                                                                     && detalle.Field<int>("TipoGanadoID").Equals(animal.Field<int>("TipoGanadoID"))
                                                                                                     select new InterfaceSalidaAnimalInfo
                                                                                                     {
                                                                                                         Organizacion = new OrganizacionInfo
                                                                                                             {
                                                                                                                 OrganizacionID = animal.Field<int>("OrganizacionID")
                                                                                                             },
                                                                                                         SalidaID = animal.Field<int>("SalidaID"),
                                                                                                         Arete = animal.Field<string>("Arete"),
                                                                                                         FechaCompra = animal.Field<DateTime>("FechaCompra"),
                                                                                                         PesoCompra = animal.Field<decimal>("PesoCompra"),
                                                                                                         PesoOrigen = animal.Field<decimal>("PesoOrigen"),
                                                                                                         TipoGanado = new TipoGanadoInfo
                                                                                                             {
                                                                                                                 TipoGanadoID = animal.Field<int>("TipoGanadoID"),
                                                                                                                 Descripcion = animal.Field<string>("TipoGanado")
                                                                                                             },
                                                                                                         FechaRegistro = animal.Field<DateTime>("FechaRegistro"),
                                                                                                         UsuarioRegistro = animal.Field<string>("UsuarioRegistro"),
                                                                                                         ListaSalidaCostos = (from costo in ds.Tables[ConstantesDAL.DtDetalleCosto].AsEnumerable()
                                                                                                                              where animal.Field<int>("OrganizacionID").Equals(costo.Field<int>("OrganizacionID"))
                                                                                                                                 && animal.Field<int>("SalidaID").Equals(costo.Field<int>("SalidaID"))
                                                                                                                                 && animal.Field<string>("Arete").Equals(costo.Field<string>("Arete"))
                                                                                                                                 && animal.Field<DateTime>("FechaCompra").Equals(costo.Field<DateTime>("FechaCompra"))
                                                                                                                              select new InterfaceSalidaCostoInfo
                                                                                                                                  {
                                                                                                                                      Organizacion = new OrganizacionInfo
                                                                                                                                          {
                                                                                                                                              OrganizacionID = costo.Field<int>("OrganizacionID")
                                                                                                                                          },
                                                                                                                                      SalidaID = costo.Field<int>("SalidaID"),
                                                                                                                                      Arete = costo.Field<string>("Arete"),
                                                                                                                                      FechaCompra = costo.Field<DateTime>("FechaCompra"),
                                                                                                                                      Costo = new CostoInfo
                                                                                                                                          {
                                                                                                                                              CostoID = costo.Field<int>("CostoID"),
                                                                                                                                              Descripcion = costo.Field<string>("Costo"),
                                                                                                                                              ClaveContable = costo.Field<string>("ClaveContableCosto"),
                                                                                                                                              Retencion = new RetencionInfo
                                                                                                                                              {
                                                                                                                                                  RetencionID = costo.Field<int?>("RetencionID") != null ? costo.Field<int>("RetencionID") : 0,
                                                                                                                                                  Descripcion = costo.Field<string>("Retencion") ?? string.Empty
                                                                                                                                              }
                                                                                                                                          },
                                                                                                                                      Importe = costo.Field<decimal>("Importe"),
                                                                                                                                      FechaRegistro = costo.Field<DateTime>("FechaRegistro"),
                                                                                                                                      UsuarioRegistro = costo.Field<string>("UsuarioRegistro")
                                                                                                                                  }).ToList()

                                                                                                     }
                                                                                               ).ToList()
                                                                   }
                                                              ).ToList()
                                       }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return interfaceSalidaInfo;
        }

        /// <summary>
        /// Obtiene un Objeto Inteface Salida
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<InterfaceSalidaInfo> ObtenerPorEmbarqueID(DataSet ds)
        {
            List<InterfaceSalidaInfo> interfaceSalidaInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                interfaceSalidaInfo = (from info in dt.AsEnumerable()
                                       select new InterfaceSalidaInfo
                                       {
                                           Activo = info.Field<bool>("Activo").BoolAEnum(),
                                           Cabezas = info.Field<int>("Cabezas"),
                                           EsRuteo = info.Field<bool>("EsRuteo"),
                                           FechaRegistro = info.Field<DateTime>("FechaRegistro"),
                                           FechaSalida = info.Field<DateTime>("FechaSalida"),
                                           OrganizacionDestino = new OrganizacionInfo
                                           {
                                               OrganizacionID = info.Field<int>("OrganizacionDestinoID")
                                           },
                                           Organizacion = new OrganizacionInfo
                                           {
                                               OrganizacionID = info.Field<int>("OrganizacionID")
                                           },
                                           SalidaID = info.Field<int>("SalidaID"),
                                           ListaInterfaceDetalle = (from detalle in ds.Tables[ConstantesDAL.DtDetalle].AsEnumerable()
                                                                    select new InterfaceSalidaDetalleInfo
                                                                    {
                                                                        Cabezas = detalle.Field<int>("Cabezas"),
                                                                        Importe = detalle.Field<decimal>("Importe"),
                                                                        PrecioKG = detalle.Field<decimal>("PrecioKG"),
                                                                        SalidaID = detalle.Field<int>("SalidaID"),
                                                                        Organizacion = new OrganizacionInfo
                                                                        {
                                                                            OrganizacionID = detalle.Field<int>("OrganizacionID")
                                                                        },
                                                                        FechaRegistro = detalle.Field<DateTime>("FechaRegistro"),
                                                                        UsuarioRegistro = detalle.Field<string>("UsuarioRegistro"),
                                                                        TipoGanado = new TipoGanadoInfo
                                                                        {
                                                                            TipoGanadoID = detalle.Field<int>("TipoGanadoID"),
                                                                            Descripcion = detalle.Field<string>("TipoGanado")
                                                                        },
                                                                        ListaInterfaceSalidaAnimal = (from animal in ds.Tables[ConstantesDAL.DtDetalleAnimal].AsEnumerable()
                                                                                                      where detalle.Field<int>("OrganizacionID").Equals(animal.Field<int>("OrganizacionID"))
                                                                                                      && detalle.Field<int>("SalidaID").Equals(animal.Field<int>("SalidaID"))
                                                                                                      && detalle.Field<int>("TipoGanadoID").Equals(animal.Field<int>("TipoGanadoID"))
                                                                                                      select new InterfaceSalidaAnimalInfo
                                                                                                      {
                                                                                                          Organizacion = new OrganizacionInfo
                                                                                                          {
                                                                                                              OrganizacionID = animal.Field<int>("OrganizacionID")
                                                                                                          },
                                                                                                          SalidaID = animal.Field<int>("SalidaID"),
                                                                                                          Arete = animal.Field<string>("Arete"),
                                                                                                          FechaCompra = animal.Field<DateTime>("FechaCompra"),
                                                                                                          PesoCompra = animal.Field<decimal>("PesoCompra"),
                                                                                                          PesoOrigen = animal.Field<decimal>("PesoOrigen"),
                                                                                                          TipoGanado = new TipoGanadoInfo
                                                                                                          {
                                                                                                              TipoGanadoID = animal.Field<int>("TipoGanadoID"),
                                                                                                              Descripcion = animal.Field<string>("TipoGanado")
                                                                                                          },
                                                                                                          FechaRegistro = animal.Field<DateTime>("FechaRegistro"),
                                                                                                          UsuarioRegistro = animal.Field<string>("UsuarioRegistro"),
                                                                                                          ListaSalidaCostos = (from costo in ds.Tables[ConstantesDAL.DtDetalleCosto].AsEnumerable()
                                                                                                                               where animal.Field<int>("OrganizacionID").Equals(costo.Field<int>("OrganizacionID"))
                                                                                                                                  && animal.Field<int>("SalidaID").Equals(costo.Field<int>("SalidaID"))
                                                                                                                                  && animal.Field<string>("Arete").Equals(costo.Field<string>("Arete"))
                                                                                                                                  && animal.Field<DateTime>("FechaCompra").Equals(costo.Field<DateTime>("FechaCompra"))
                                                                                                                               select new InterfaceSalidaCostoInfo
                                                                                                                               {
                                                                                                                                   Organizacion = new OrganizacionInfo
                                                                                                                                   {
                                                                                                                                       OrganizacionID = costo.Field<int>("OrganizacionID")
                                                                                                                                   },
                                                                                                                                   SalidaID = costo.Field<int>("SalidaID"),
                                                                                                                                   Arete = costo.Field<string>("Arete"),
                                                                                                                                   FechaCompra = costo.Field<DateTime>("FechaCompra"),
                                                                                                                                   Costo = new CostoInfo
                                                                                                                                   {
                                                                                                                                       CostoID = costo.Field<int>("CostoID"),
                                                                                                                                       Descripcion = costo.Field<string>("Costo"),
                                                                                                                                       ClaveContable = costo.Field<string>("ClaveContableCosto"),
                                                                                                                                       Retencion = new RetencionInfo
                                                                                                                                       {
                                                                                                                                           RetencionID = costo.Field<int?>("RetencionID") != null ? costo.Field<int>("RetencionID") : 0,
                                                                                                                                           Descripcion = costo.Field<string>("Retencion") ?? string.Empty
                                                                                                                                       }
                                                                                                                                   },
                                                                                                                                   Importe = costo.Field<decimal>("Importe"),
                                                                                                                                   FechaRegistro = costo.Field<DateTime>("FechaRegistro"),
                                                                                                                                   UsuarioRegistro = costo.Field<string>("UsuarioRegistro")
                                                                                                                               }).ToList()

                                                                                                      }
                                                                                                ).ToList()
                                                                    }
                                                              ).ToList()
                                       }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return interfaceSalidaInfo;
        }
    }
}
