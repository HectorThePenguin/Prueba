using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Services.Info.Info;
using System.Data;
using SIE.Services.Info.Constantes;
using SIE.Base.Log;
using SIE.Base.Exepciones;
using System.Reflection;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapInterfaceSalidaAnimalDAL
    {
        /// <summary>
        /// Mapeo de los campos de la tabla del arete InterfaceAnimal.
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static InterfaceSalidaAnimalInfo ObtenerNumeroAreteIndividual(DataSet ds)
        {
            InterfaceSalidaAnimalInfo interfaceSalidoAnimalInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                interfaceSalidoAnimalInfo = (from info in dt.AsEnumerable()
                                             select new InterfaceSalidaAnimalInfo
                             {
                                 SalidaID = info.Field<int>("SalidaID"),
                                 Arete = info.Field<string>("Arete"),
                                 AreteMetalico = info.Field<string>("AreteMetalico"),
                                 FechaCompra = info.Field<DateTime>("FechaCompra"),
                                 PesoCompra = info.Field<decimal>("PesoCompra"),
                                 TipoGanado = new TipoGanadoDAL().ObtenerPorID(info.Field<int>("TipoGanadoID")),
                                 PesoOrigen = info.Field<decimal>("PesoOrigen"),
                                 FechaRegistro = info.Field<DateTime>("FechaRegistro"),
                                 UsuarioRegistro = info.Field<string>("UsuarioRegistro"),
                                 Partida = info.Field<int>("Partida"),
                                 CorralID = info.Field<int>("CorralID"),
                                 Organizacion = new OrganizacionInfo
                                 {
                                     OrganizacionID = info.Field<int>("OrganizacionID"),
                                 }
                             }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return interfaceSalidoAnimalInfo;
        }
        /// <summary>
        /// Obtiene todos los aretes en base a la salida.
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<InterfaceSalidaAnimalInfo> ObtenerAretesInterfazSalidaAnimal(DataSet ds)
        {
            List<InterfaceSalidaAnimalInfo> interfaceSalidoAnimalInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                interfaceSalidoAnimalInfo = (from info in dt.AsEnumerable()
                                             select new InterfaceSalidaAnimalInfo
                                             {
                                                 SalidaID = info.Field<int>("SalidaID"),
                                                 Arete = info.Field<string>("Arete"),
                                                 FechaCompra = info.Field<DateTime>("FechaCompra"),
                                                 PesoCompra = info.Field<decimal>("PesoCompra"),
                                                 TipoGanado = new TipoGanadoDAL().ObtenerPorID(info.Field<int>("TipoGanadoID")),
                                                 PesoOrigen = info.Field<decimal>("PesoOrigen"),
                                                 FechaRegistro = info.Field<DateTime>("FechaRegistro"),
                                                 UsuarioRegistro = info.Field<string>("UsuarioRegistro"),
                                                 Partida = info.Field<int>("Partida"),
                                                 CorralID = info.Field<int>("CorralID")
                                             }).ToList<InterfaceSalidaAnimalInfo>();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return interfaceSalidoAnimalInfo;
        }

        /// <summary>
        /// Obtiene los datos de Interfaz Salida Animal
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<InterfaceSalidaAnimalInfo> ObtenerInterfazSalidaAnimal(DataSet ds)
        {
            List<InterfaceSalidaAnimalInfo> interfaceSalidoAnimalInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                interfaceSalidoAnimalInfo = (from info in dt.AsEnumerable()
                                             select new InterfaceSalidaAnimalInfo
                                             {                                                 
                                                 TipoGanado = new TipoGanadoInfo
                                                                  {
                                                                      TipoGanadoID = info.Field<int>("TipoGanadoID"),
                                                                      Descripcion = info.Field<string>("TipoGanado")
                                                                  },
                                                 Organizacion = new OrganizacionInfo
                                                                    {
                                                                        OrganizacionID = info.Field<int>("OrganizacionID"),
                                                                    },
                                                  SalidaID = info.Field<int>("SalidaID"),
                                                  Arete = info.Field<string>("Arete"),
                                                  FechaCompra = info.Field<DateTime>("FechaCompra"),
                                                  PesoCompra = info.Field<decimal>("PesoCompra"),
                                                  PesoOrigen = info.Field<decimal>("PesoOrigen"),
                                             }).ToList<InterfaceSalidaAnimalInfo>();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return interfaceSalidoAnimalInfo;
        }

        /// <summary>
        /// Obtiene una lista de interface salida animal
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<InterfaceSalidaAnimalInfo> ObtenerInterfazSalidaAnimalPorEntradas(DataSet ds)
        {
            List<InterfaceSalidaAnimalInfo> interfaceSalidoAnimalInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                interfaceSalidoAnimalInfo = (from info in dt.AsEnumerable()
                                             select new InterfaceSalidaAnimalInfo
                                                        {
                                                            Organizacion = new OrganizacionInfo
                                                                               {
                                                                                   OrganizacionID =
                                                                                       info.Field<int>("OrganizacionID"),
                                                                               },
                                                            SalidaID = info.Field<int>("SalidaID"),
                                                            Arete = info.Field<string>("Arete"),
                                                            FechaCompra = info.Field<DateTime>("FechaCompra"),
                                                            PesoCompra = info.Field<decimal>("PesoCompra"),
                                                            PesoOrigen = info.Field<decimal>("PesoOrigen"),
                                                            TipoGanado = new TipoGanadoInfo
                                                                             {
                                                                                 TipoGanadoID =
                                                                                     info.Field<int>("TipoGanadoID")
                                                                             }
                                                        }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return interfaceSalidoAnimalInfo;
        }
        /// <summary>
        /// Mapeo de interfaz salida animal
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<InterfaceSalidaAnimalInfo> ObtenerInterfazSalidaAnimalPorEntradaGanado(DataSet ds)
        {
            List<InterfaceSalidaAnimalInfo> interfaceSalidoAnimalInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                interfaceSalidoAnimalInfo = (from info in dt.AsEnumerable()
                                             select new InterfaceSalidaAnimalInfo
                                             {
                                                 Organizacion = new OrganizacionInfo
                                                 {
                                                     OrganizacionID =
                                                         info.Field<int>("OrganizacionID"),
                                                 },
                                                 SalidaID = info.Field<int>("SalidaID"),
                                                 Arete = info.Field<string>("Arete"),
                                                 FechaCompra = info.Field<DateTime>("FechaCompra"),
                                                 PesoCompra = info.Field<decimal>("PesoCompra"),
                                                 PesoOrigen = info.Field<decimal>("PesoOrigen"),
                                                 TipoGanado = new TipoGanadoInfo
                                                 {
                                                     TipoGanadoID =
                                                         info.Field<int>("TipoGanadoID")
                                                 },
                                                 FechaRegistro = info.Field<DateTime>("FechaRegistro"),
                                                 UsuarioRegistro = info.Field<string>("UsuarioRegistro"),
                                                 AreteMetalico = info.Field<string>("AreteMetalico"),
                                                 AnimalID = info.Field<long?>("AnimalID") == null ? 0 : info.Field<long>("AnimalID")

                                             }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return interfaceSalidoAnimalInfo;
        }
        /// <summary>
        /// Mapeo de los campos de la tabla del arete InterfaceAnimal.
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static InterfaceSalidaAnimalInfo ObtenerNumeroAreteIndividualPartidaActiva(DataSet ds)
        {
            InterfaceSalidaAnimalInfo interfaceSalidoAnimalInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                interfaceSalidoAnimalInfo = (from info in dt.AsEnumerable()
                                             select new InterfaceSalidaAnimalInfo
                                             {
                                                 SalidaID = info.Field<int>("SalidaID"),
                                                 Arete = info.Field<string>("Arete"),
                                                 AreteMetalico = info.Field<string>("AreteMetalico"),
                                                 FechaCompra = info.Field<DateTime>("FechaCompra"),
                                                 PesoCompra = info.Field<decimal>("PesoCompra"),
                                                 TipoGanado = new TipoGanadoDAL().ObtenerPorID(info.Field<int>("TipoGanadoID")),
                                                 PesoOrigen = info.Field<decimal>("PesoOrigen"),
                                                 FechaRegistro = info.Field<DateTime>("FechaRegistro"),
                                                 UsuarioRegistro = info.Field<string>("UsuarioRegistro"),
                                                 Partida = info.Field<int>("Partida"),
                                                 CorralID = info.Field<int>("CorralID"),
                                                 Organizacion = new OrganizacionInfo
                                                 {
                                                     OrganizacionID = info.Field<int>("OrganizacionID"),
                                                 }
                                             }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return interfaceSalidoAnimalInfo;
        }
    }
}
