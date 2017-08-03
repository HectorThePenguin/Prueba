using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Info.Enums;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapVentaGanadoDAL
    {
        internal static VentaGanadoInfo ObtenerVentaGanadoPorTicket(DataSet ds)
        {
            try
            {
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                VentaGanadoInfo result =
                    (from info in dt.AsEnumerable()
                     select
                         new VentaGanadoInfo
                         {
                             VentaGanadoID = info.Field<int>("VentaGanadoID")
                             ,FolioTicket = info.Field<int>("FolioTicket")
                             ,FolioFactura = info["FolioFactura"] == DBNull.Value ? "0" : info.Field<string>("FolioFactura")
                             ,ClienteID = info.Field<int>("ClienteID")
                             ,CodigoSAP = info.Field<string>("CodigoSAP")
                             ,FechaVenta = info.Field<DateTime>("FechaVenta")
                             ,PesoTara = info.Field<decimal>("PesoTara")
                             ,PesoBruto = info.Field<decimal>("PesoBruto")
                             ,LoteID = info["LoteID"] == DBNull.Value ? 0 : info.Field<int>("LoteID")
                             ,CodigoCorral = info["Codigo"] == DBNull.Value ? "" : info.Field<string>("Codigo")
                             ,Activo = info.Field<bool>("Activo")
                             ,UsuarioCreacionID = info["UsuarioCreacionID"] == DBNull.Value ? 1 : info.Field<int>("UsuarioCreacionID")
                             ,UsuarioModificacionID = info["UsuarioModificacionID"] == DBNull.Value ? 1 : info.Field<int>("UsuarioModificacionID")
                             ,TotalCabezas = info["TotalCabezas"] == DBNull.Value ? 0 : info.Field<int>("TotalCabezas")
                         }).First();
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static List<ContenedorVentaGanado> ObtenerVentaGanadoPorTicketPoliza(DataSet ds)
        {
            try
            {
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ContenedorVentaGanado> result =
                    (from info in dt.AsEnumerable()
                     select
                         new ContenedorVentaGanado
                             {
                                 CausaPrecio = new CausaPrecioInfo
                                                   {
                                                       Precio = info.Field<decimal>("Precio"),
                                                       CausaPrecioID = info.Field<int>("CausaPrecioID"),
                                                   },
                                 Cliente = new ClienteInfo
                                               {
                                                   ClienteID = info.Field<int>("ClienteID"),
                                                   CodigoSAP = info.Field<string>("CodigoSAP"),
                                               },
                                 VentaGanado = new VentaGanadoInfo
                                                   {
                                                       FolioTicket = info.Field<int>("FolioTicket"),
                                                       PesoBruto = info.Field<decimal>("PesoBruto"),
                                                       PesoTara = info.Field<decimal>("PesoTara"),
                                                       FechaVenta = info.Field<DateTime>("FechaVenta"),
                                                       ClienteID = info.Field<int>("ClienteID")
                                                   },
                                 VentaGanadoDetalle = new VentaGanadoDetalleInfo
                                                          {
                                                              Arete = info.Field<string>("Arete"),
                                                              Animal = new AnimalInfo
                                                                           {
                                                                               AnimalID = info.Field<long>("AnimalID"),
                                                                               OrganizacionIDEntrada =
                                                                                   info.Field<int>(
                                                                                       "OrganizacionIDEntrada"),
                                                                               TipoGanado = new TipoGanadoInfo
                                                                                                {
                                                                                                    TipoGanadoID = info.Field<int>("TipoGanadoID"),
                                                                                                    Descripcion = info.Field<string>("TipoGanado")
                                                                                                }
                                                                           },
                                                              FotoVenta = info.Field<string>("FotoVenta")
                                                          },
                             }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene los costos de las ventas intensivas
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ContenedorVentaGanado> ObtenerVentaGanadoIntensivoPorTicketPoliza(DataSet ds)
        {
            try
            {
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ContenedorVentaGanado> result =
                    (from info in dt.AsEnumerable()
                     select
                         new ContenedorVentaGanado
                         {
                             Importe = info.Field<decimal>("Importe"),
                             CostoId = info.Field<int>("CostoID"),
                             CausaPrecio = new CausaPrecioInfo
                             {
                                 Precio = info.Field<decimal>("Precio"),
                                 CausaPrecioID = info.Field<int>("CausaPrecioID"),
                             },
                             Cliente = new ClienteInfo
                             {
                                 ClienteID = info.Field<int>("ClienteID"),
                                 CodigoSAP = info.Field<string>("CodigoSAP"),
                             },
                             VentaGanado = new VentaGanadoInfo
                             {
                                 FolioTicket = info.Field<int>("FolioTicket"),
                                 PesoBruto = info.Field<decimal>("PesoBruto"),
                                 PesoTara = info.Field<decimal>("PesoTara"),
                                 FechaVenta = info.Field<DateTime>("FechaVenta"),
                                 ClienteID = info.Field<int>("ClienteID")
                             },
                             TotalCabezas = info.Field<Int16>("Cabezas")
                             
                         }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una venta por su folio ticket
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static VentaGanadoInfo ObtenerVentaGanadoPorFolioTicket(DataSet ds)
        {
            try
            {
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                VentaGanadoInfo result =
                    (from info in dt.AsEnumerable()
                     select
                         new VentaGanadoInfo
                             {
                                 FolioTicket = info.Field<int>("FolioTicket"),
                                 VentaGanadoID = info.Field<int>("VentaGanadoID"),
                                 NombreCliente =
                                     string.Format("{0}-{1}", info.Field<string>("CodigoSAP"),
                                                   info.Field<string>("Descripcion")),
                                 FechaVenta = info.Field<DateTime>("FechaVenta")
                             }).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un listado de ventas de ganado paginado
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<VentaGanadoInfo> ObtenerVentaGanadoPorPagina(DataSet ds)
        {
            try
            {
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<VentaGanadoInfo> lista = 
                    (from info in dt.AsEnumerable()
                     select
                         new VentaGanadoInfo
                         {
                             FolioTicket = info.Field<int>("FolioTicket"),
                             VentaGanadoID = info.Field<int>("VentaGanadoID"),
                             NombreCliente =
                                 string.Format("{0}-{1}", info.Field<string>("CodigoSAP"),
                                               info.Field<string>("Descripcion")),
                             FechaVenta = info.Field<DateTime>("FechaVenta")
                         }).ToList();
                dt = ds.Tables[ConstantesDAL.DtDetalle];
                int totalRegistros = Convert.ToInt32(dt.Rows[0]["TotalReg"]);
                var result = new ResultadoInfo<VentaGanadoInfo>
                             {
                                 Lista = lista,
                                 TotalRegistros = totalRegistros
                             };
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista con las ventas de ganado
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ContenedorVentaGanado> ObtenerVentaGanadoPorFechaConciliacion(DataSet ds)
        {
            try
            {
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ContenedorVentaGanado> result =
                    (from info in dt.AsEnumerable()
                     select
                         new ContenedorVentaGanado
                         {
                             CausaPrecio = new CausaPrecioInfo
                             {
                                 Precio = info.Field<decimal>("Precio"),
                                 CausaPrecioID = info.Field<int>("CausaPrecioID"),
                             },
                             Cliente = new ClienteInfo
                             {
                                 ClienteID = info.Field<int>("ClienteID"),
                                 CodigoSAP = info.Field<string>("CodigoSAP"),
                             },
                             VentaGanado = new VentaGanadoInfo
                             {
                                 FolioTicket = info.Field<int>("FolioTicket"),
                                 PesoBruto = info.Field<decimal>("PesoBruto"),
                                 PesoTara = info.Field<decimal>("PesoTara"),
                                 FechaVenta = info.Field<DateTime>("FechaVenta"),
                                 ClienteID = info.Field<int>("ClienteID"),
                                 VentaGanadoID = info.Field<int>("VentaGanadoID"),
                             },
                             VentaGanadoDetalle = new VentaGanadoDetalleInfo
                             {
                                 Arete = info.Field<string>("Arete"),
                                 Animal = new AnimalInfo
                                 {
                                     AnimalID = info.Field<long>("AnimalID"),
                                     OrganizacionIDEntrada =
                                         info.Field<int>(
                                             "OrganizacionIDEntrada"),
                                     TipoGanado = new TipoGanadoInfo
                                     {
                                         TipoGanadoID = info.Field<int>("TipoGanadoID"),
                                         Descripcion = info.Field<string>("TipoGanado")
                                     }
                                 },
                             },
                         }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
