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

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal  static class MapEmbarqueDAL
    {
        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static ResultadoInfo<EntradaEmbarqueInfo> ObtenerTodosEntradaEmbarque(DataSet ds)
        {
            ResultadoInfo<EntradaEmbarqueInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<EntradaEmbarqueInfo> listaEmbarque = (from embarque in dt.AsEnumerable()
                                                           select new EntradaEmbarqueInfo
                                                               {
                                                                   EmbarqueID = embarque.Field<int>("EmbarqueID"),
                                                                   FolioEmbarque = embarque.Field<int>("FolioEmbarque"),
                                                                   TipoOrganizacion = embarque.Field<string>("TipoOrganizacion"),
                                                                   OrganizacionOrigen = embarque.Field<string>("OrganizacionOrigen"),
                                                                   OrganizacionDestino = embarque.Field<string>("OrganizacionDestino"),
                                                                   FechaLlegada = embarque.Field<DateTime>("FechaLlegada"),
                                                                   FechaSalida = embarque.Field<DateTime>("FechaSalida"),
                                                                   TipoEmbarque = embarque.Field<string>("TipoEmbarque"),
                                                                   Chofer = embarque.Field<string>("Chofer"),
                                                                   PlacaCamion = embarque.Field<string>("PlacaCamion"),
                                                               }).ToList();
                resultado = new ResultadoInfo<EntradaEmbarqueInfo>
                    {
                        Lista = listaEmbarque,
                        TotalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
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
        ///     Metodo que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static EmbarqueInfo ObtenerPorID(DataSet ds)
        {
            EmbarqueInfo result;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtDetalle = ds.Tables[ConstantesDAL.DtDetalle];
                DataTable dtCostos = ds.Tables[ConstantesDAL.DtCostos];

                result = (from info in dt.AsEnumerable()
                          select new EmbarqueInfo
                          {
                              EmbarqueID = info.Field<int>("EmbarqueID"),
                              Organizacion = new OrganizacionInfo
                              {
                                  OrganizacionID = info.Field<int>("OrganizacionID"),
                                  Descripcion = info.Field<string>("Organizacion")
                                  ,
                                  TipoOrganizacion = new TipoOrganizacionInfo
                                  {
                                      TipoOrganizacionID =
                                          info.Field<int>(
                                              "TipoOrganizacionID"),
                                      Descripcion =
                                          info.Field<string>(
                                              "TipoOrganizacion"),
                                  }
                              },
                              FolioEmbarque = info.Field<int>("FolioEmbarque"),
                              TipoEmbarque =
                                  new TipoEmbarqueInfo
                                  {
                                      TipoEmbarqueID = info.Field<int>("TipoEmbarqueID"),
                                      Descripcion = info.Field<string>("TipoEmbarque")
                                  },
                              Estatus = info.Field<int>("Estatus"),
                              ListaEscala = (from escala in dtDetalle.AsEnumerable()
                                             where
                                                 escala.Field<int>("EmbarqueID") ==
                                                 info.Field<int>("EmbarqueID")
                                             select new EmbarqueDetalleInfo
                                             {
                                                 ListaCostoEmbarqueDetalle = (from costo in dtCostos.AsEnumerable()
                                                                              where costo.Field<int>("EmbarqueDetalleID") ==
                                                                              escala.Field<int>("EmbarqueDetalleID")
                                                                              select new CostoEmbarqueDetalleInfo
                                                                              {
                                                                                  EmbarqueDetalleID = costo.Field<int>("EmbarqueDetalleID"),
                                                                                  Importe = costo.Field<decimal>("Importe"),
                                                                                  Orden = escala.Field<int>("Orden"),
                                                                                  Costo = new CostoInfo
                                                                                  {
                                                                                      CostoID = costo.Field<int>("CostoID"),
                                                                                      ImporteCosto = costo.Field<decimal>("Importe")
                                                                                  }
                                                                              }).ToList<CostoEmbarqueDetalleInfo>(),
                                                 EmbarqueDetalleID =
                                                     escala.Field<int>(
                                                         "EmbarqueDetalleID"),
                                                 EmbarqueID =
                                                     escala.Field<int>("EmbarqueID"),
                                                 Proveedor =
                                                     new ProveedorInfo
                                                     {
                                                         ProveedorID =
                                                             escala.Field<int>("ProveedorID"),
                                                         Descripcion =
                                                             escala.Field<string>("Proveedor"),
                                                         Correo = 
                                                             escala.Field<string>("CorreoElectronico"),
                                                     },
                                                 Chofer =
                                                     new ChoferInfo
                                                     {
                                                         ChoferID =
                                                             escala.Field<int>("ChoferID"),
                                                         Nombre =
                                                             escala.Field<string>("Nombre"),
                                                         ApellidoPaterno =
                                                             escala.Field<string>(
                                                                 "ApellidoPaterno"),
                                                         ApellidoMaterno =
                                                             escala.Field<string>(
                                                                 "ApellidoMaterno")
                                                     },
                                                 Jaula =
                                                     new JaulaInfo
                                                     {
                                                         JaulaID =
                                                             escala.Field<int>("JaulaID"),
                                                         PlacaJaula =
                                                             escala.Field<string>("PlacaJaula")
                                                     },
                                                 Camion =
                                                     new CamionInfo
                                                     {
                                                         CamionID =
                                                             escala.Field<int>("CamionID"),
                                                         PlacaCamion =
                                                             escala.Field<string>(
                                                                 "PlacaCamion")
                                                     },
                                                 OrganizacionOrigen =
                                                     new OrganizacionInfo
                                                     {
                                                         OrganizacionID =
                                                             escala.Field<int>(
                                                                 "OrganizacionOrigenID"),
                                                         Descripcion =
                                                             escala.Field<string>("Origen"),
                                                         TipoOrganizacion =
                                                             new TipoOrganizacionInfo
                                                             {
                                                                 TipoOrganizacionID =
                                                                     escala.Field<int>(
                                                                         "TipoOrganizacionOrigenID"),
                                                                 Descripcion =
                                                                     escala.Field<string>(
                                                                         "Origen")
                                                             }
                                                     },
                                                 OrganizacionDestino =
                                                     new OrganizacionInfo
                                                     {
                                                         OrganizacionID =
                                                             escala.Field<int>(
                                                                 "OrganizacionDestinoID"),
                                                         Descripcion =
                                                             escala.Field<string>("Destino"),
                                                         TipoOrganizacion =
                                                             new TipoOrganizacionInfo
                                                             {
                                                                 TipoOrganizacionID =
                                                                     escala.Field<int>(
                                                                         "TipoOrganizacionDestinoID")
                                                             }
                                                     },
                                                 FechaSalida =
                                                     escala.Field<DateTime>("FechaSalida"),
                                                 FechaLlegada =
                                                     escala.Field<DateTime>("FechaLlegada"),
                                                 Activo = escala.Field<bool>("Activo").BoolAEnum(),
                                                 Comentarios = escala.Field<string>("Comentarios"),
                                                 Horas = escala.Field<decimal>("Horas"),
                                                 Recibido = escala.Field<bool>("Recibido"),
                                                 Orden = escala.Field<int>("Orden")
                                             }
                                            ).ToList()
                          }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return result;
        }

        /// <summary>
        ///     Metodo que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static EmbarqueInfo ObtenerPorFolioEmbarque(DataSet ds)
        {
            EmbarqueInfo result;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtDetalle = ds.Tables[ConstantesDAL.DtDetalle];

                result = (from info in dt.AsEnumerable()
                          select new EmbarqueInfo
                          {
                              EmbarqueID = info.Field<int>("EmbarqueID"),
                              Organizacion = new OrganizacionInfo
                              {
                                  OrganizacionID = info.Field<int>("OrganizacionID"),
                                  Descripcion = info.Field<string>("Organizacion")
                                  ,
                                  TipoOrganizacion = new TipoOrganizacionInfo
                                  {
                                      TipoOrganizacionID = info.Field<int>("TipoOrganizacionID"),
                                      Descripcion = info.Field<string>("TipoOrganizacion"),
                                  }
                              },
                              FolioEmbarque = info.Field<int>("FolioEmbarque"),
                              TipoEmbarque =
                                  new TipoEmbarqueInfo
                                  {
                                      TipoEmbarqueID = info.Field<int>("TipoEmbarqueID"),
                                      Descripcion = info.Field<string>("TipoEmbarque")
                                  },
                              Estatus = info.Field<int>("Estatus"),
                              ListaEscala = (from escala in dtDetalle.AsEnumerable()
                                             where
                                                 escala.Field<int>("EmbarqueID") ==
                                                 info.Field<int>("EmbarqueID")
                                             select new EmbarqueDetalleInfo
                                             {
                                                 EmbarqueDetalleID =
                                                     escala.Field<int>(
                                                         "EmbarqueDetalleID"),
                                                 EmbarqueID =
                                                     escala.Field<int>("EmbarqueID"),
                                                 Proveedor =
                                                     new ProveedorInfo
                                                     {
                                                         ProveedorID =
                                                             escala.Field<int>("ProveedorID"),
                                                         Descripcion =
                                                             escala.Field<string>("Proveedor")
                                                     },
                                                 Chofer =
                                                     new ChoferInfo
                                                     {
                                                         ChoferID = escala.Field<int>("ChoferID"),
                                                         Nombre = escala.Field<string>("Nombre"),
                                                         ApellidoPaterno =
                                                             escala.Field<string>(
                                                                 "ApellidoPaterno"),
                                                         ApellidoMaterno =
                                                             escala.Field<string>(
                                                                 "ApellidoMaterno")
                                                     },
                                                 Jaula =
                                                     new JaulaInfo
                                                     {
                                                         JaulaID = escala.Field<int>("JaulaID"),
                                                         PlacaJaula =
                                                             escala.Field<string>("PlacaJaula")
                                                     },
                                                 Camion =
                                                     new CamionInfo
                                                     {
                                                         CamionID = escala.Field<int>("CamionID"),
                                                         PlacaCamion =
                                                             escala.Field<string>("PlacaCamion")
                                                     },
                                                 OrganizacionOrigen =
                                                     new OrganizacionInfo
                                                     {
                                                         OrganizacionID =
                                                             escala.Field<int>(
                                                                 "OrganizacionOrigenID"),
                                                         Descripcion =
                                                             escala.Field<string>("Origen"),
                                                         TipoOrganizacion = new TipoOrganizacionInfo
                                                         {
                                                             TipoOrganizacionID = escala.Field<int>(
                                                                  "TipoOrganizacionOrigenID"),
                                                             Descripcion = escala.Field<string>("Origen")
                                                         }
                                                     },
                                                 OrganizacionDestino =
                                                     new OrganizacionInfo
                                                     {
                                                         OrganizacionID =
                                                             escala.Field<int>(
                                                                 "OrganizacionDestinoID"),
                                                         Descripcion = escala.Field<string>("Destino"),
                                                         TipoOrganizacion = new TipoOrganizacionInfo { TipoOrganizacionID = escala.Field<int>("TipoOrganizacionDestinoID") }
                                                     },
                                                 FechaSalida =
                                                     escala.Field<DateTime>("FechaSalida"),
                                                 FechaLlegada =
                                                     escala.Field<DateTime>("FechaLlegada"),
                                                 Activo = escala.Field<bool>("Activo").BoolAEnum(),
                                                 Comentarios = escala.Field<string>("Comentarios"),
                                                 Horas = escala.Field<decimal>("Horas"),
                                                 Recibido = escala.Field<bool>("Recibido")
                                             }
                                            ).ToList()
                          }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return result;
        }

        /// <summary>
        /// Metodo para obtener una lista de entradas de ganado activas paginada 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static ResultadoInfo<EmbarqueInfo> ObtenerPorPagina(DataSet ds)
        {
            ResultadoInfo<EmbarqueInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                IList<EmbarqueInfo> lista = (from info in dt.AsEnumerable()
                                             select new EmbarqueInfo
                                             {
                                                 EmbarqueID =
                                                     info.Field<int>("EmbarqueID"),
                                                 FolioEmbarque = info.Field<int>("FolioEmbarque"),
                                                 Estatus = info.Field<int>("Estatus"),
                                                 TipoEmbarque =
                                                     new TipoEmbarqueInfo
                                                     {
                                                         TipoEmbarqueID = info.Field<int>("TipoEmbarqueID"),
                                                         Descripcion = info.Field<string>("TipoEmbarque")
                                                     },
                                                 Organizacion =
                                                     new OrganizacionInfo
                                                     {
                                                         OrganizacionID = info.Field<int>("OrganizacionID"),
                                                         TipoOrganizacion = new TipoOrganizacionInfo { TipoOrganizacionID = info.Field<int>("TipoOrganizacionDestinoID") },
                                                         Descripcion = info.Field<string>("Organizacion")
                                                     },
                                                 ListaEscala = (from escala in dt.AsEnumerable()
                                                                where
                                                                    escala.Field<int>("EmbarqueID") == info.Field<int>("EmbarqueID")
                                                                select
                                                                    new EmbarqueDetalleInfo
                                                                    {
                                                                        EmbarqueID = escala.Field<int>("EmbarqueID"),
                                                                        Chofer = new ChoferInfo
                                                                        {
                                                                            ChoferID = escala.Field<int>("ChoferID"),
                                                                            Nombre = escala.Field<string>("Nombre"),
                                                                            ApellidoPaterno = escala.Field<string>("ApellidoPaterno"),
                                                                            ApellidoMaterno = escala.Field<string>("ApellidoMaterno")
                                                                        },

                                                                        Camion = new CamionInfo
                                                                        {
                                                                            CamionID = escala.Field<int>("CamionID"),
                                                                            PlacaCamion = escala.Field<string>("PlacaCamion")
                                                                        },
                                                                        OrganizacionOrigen = new OrganizacionInfo
                                                                        {
                                                                            OrganizacionID = escala.Field<int>("OrganizacionOrigenID"),
                                                                            Descripcion = escala.Field<string>("Origen"),
                                                                            TipoOrganizacion = new TipoOrganizacionInfo { TipoOrganizacionID = escala.Field<int>("TipoOrganizacionOrigenID"), Descripcion = escala.Field<string>("TipoOrganizacionOrigen") }
                                                                        },
                                                                        OrganizacionDestino = new OrganizacionInfo
                                                                        {
                                                                            OrganizacionID = escala.Field<int>("OrganizacionDestinoID"),
                                                                            Descripcion = escala.Field<string>("Destino"),
                                                                            TipoOrganizacion = new TipoOrganizacionInfo { TipoOrganizacionID = escala.Field<int>("TipoOrganizacionDestinoID") }
                                                                        },
                                                                        //Comentarios = escala.Field<string>("Comentarios"),
                                                                        FechaSalida = escala.Field<DateTime>("FechaSalida"),
                                                                        FechaLlegada = escala.Field<DateTime>("FechaLlegada")
                                                                    }
                                                               ).ToList()
                                             }).ToList();
                resultado = new ResultadoInfo<EmbarqueInfo>
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
            return resultado;
        }

        /// <summary>
        /// Metodo que obtiene una Programacion de Embarque
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static EmbarqueInfo ObtenerPorFolioEmbarqueOrganizacion(DataSet ds)
        {
            EmbarqueInfo progracionEmbarqueInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtDetalle = ds.Tables[ConstantesDAL.DtDetalle];
                DataTable dtCostos = ds.Tables[ConstantesDAL.DtCostos];

                progracionEmbarqueInfo = (from info in dt.AsEnumerable()
                                          select new EmbarqueInfo
                                          {
                                              EmbarqueID = info.Field<int>("EmbarqueID"),
                                              Organizacion = new OrganizacionInfo
                                              {
                                                  OrganizacionID = info.Field<int>("OrganizacionID"),
                                                  Descripcion = info.Field<string>("Organizacion")
                                                  ,
                                                  TipoOrganizacion = new TipoOrganizacionInfo
                                                  {
                                                      TipoOrganizacionID = info.Field<int>("TipoOrganizacionID"),
                                                      Descripcion = info.Field<string>("TipoOrganizacion"),
                                                  }
                                              },
                                              FolioEmbarque = info.Field<int>("FolioEmbarque"),
                                              TipoEmbarque =
                                                  new TipoEmbarqueInfo
                                                  {
                                                      TipoEmbarqueID = info.Field<int>("TipoEmbarqueID"),
                                                      Descripcion = info.Field<string>("TipoEmbarque")
                                                  },
                                              Estatus = info.Field<int>("Estatus"),
                                              ListaEscala = (from escala in dtDetalle.AsEnumerable()
                                                             where
                                                                 escala.Field<int>("EmbarqueID") ==
                                                                 info.Field<int>("EmbarqueID")
                                                             select new EmbarqueDetalleInfo
                                                             {
                                                                 ListaCostoEmbarqueDetalle = (from costo in dtCostos.AsEnumerable()
                                                                                              where costo.Field<int>("EmbarqueDetalleID") ==
                                                                                              escala.Field<int>("EmbarqueDetalleID")
                                                                                              select new CostoEmbarqueDetalleInfo
                                                                                              {
                                                                                                  EmbarqueDetalleID = costo.Field<int>("EmbarqueDetalleID"),
                                                                                                  Importe = costo.Field<decimal>("Importe"),
                                                                                                  Orden = escala.Field<int>("Orden"),
                                                                                                  Costo = new CostoInfo
                                                                                                  {
                                                                                                      CostoID = costo.Field<int>("CostoID"),
                                                                                                      ImporteCosto = costo.Field<decimal>("Importe")
                                                                                                  }
                                                                                              }).ToList<CostoEmbarqueDetalleInfo>(),
                                                                 EmbarqueDetalleID =
                                                                     escala.Field<int>(
                                                                         "EmbarqueDetalleID"),
                                                                 EmbarqueID =
                                                                     escala.Field<int>("EmbarqueID"),
                                                                 Proveedor =
                                                                     new ProveedorInfo
                                                                     {
                                                                         ProveedorID =
                                                                             escala.Field<int>("ProveedorID"),
                                                                         Descripcion =
                                                                             escala.Field<string>("Proveedor"),
                                                                         CodigoSAP = escala.Field<string>("CodigoSAP"),
                                                                         Correo = escala.Field<string>("CorreoElectronico")
                                                                     },
                                                                 Chofer =
                                                                     new ChoferInfo
                                                                     {
                                                                         ChoferID = escala.Field<int>("ChoferID"),
                                                                         Nombre = escala.Field<string>("Nombre"),
                                                                         ApellidoPaterno =
                                                                             escala.Field<string>(
                                                                                 "ApellidoPaterno"),
                                                                         ApellidoMaterno =
                                                                             escala.Field<string>(
                                                                                 "ApellidoMaterno")
                                                                     },
                                                                 Jaula =
                                                                     new JaulaInfo
                                                                     {
                                                                         JaulaID = escala.Field<int>("JaulaID"),
                                                                         PlacaJaula =
                                                                             escala.Field<string>("PlacaJaula")
                                                                     },
                                                                 Camion =
                                                                     new CamionInfo
                                                                     {
                                                                         CamionID = escala.Field<int>("CamionID"),
                                                                         PlacaCamion =
                                                                             escala.Field<string>("PlacaCamion")
                                                                     },
                                                                 OrganizacionOrigen =
                                                                     new OrganizacionInfo
                                                                     {
                                                                         OrganizacionID =
                                                                             escala.Field<int>(
                                                                                 "OrganizacionOrigenID"),
                                                                         Descripcion =
                                                                             escala.Field<string>("Origen"),
                                                                         TipoOrganizacion = new TipoOrganizacionInfo
                                                                         {
                                                                             TipoOrganizacionID = escala.Field<int>(
                                                                                  "TipoOrganizacionOrigenID"),
                                                                             Descripcion = escala.Field<string>("TipoOrganizacionOrigen")
                                                                         }
                                                                     },
                                                                 OrganizacionDestino =
                                                                     new OrganizacionInfo
                                                                     {
                                                                         OrganizacionID =
                                                                             escala.Field<int>(
                                                                                 "OrganizacionDestinoID"),
                                                                         Descripcion = escala.Field<string>("Destino"),
                                                                         TipoOrganizacion = new TipoOrganizacionInfo { TipoOrganizacionID = escala.Field<int>("TipoOrganizacionDestinoID"), Descripcion = escala.Field<string>("TipoOrganizacionDestino") }
                                                                     },
                                                                 Comentarios = escala.Field<string>("Comentarios"),
                                                                 FechaSalida =
                                                                     escala.Field<DateTime>("FechaSalida"),
                                                                 FechaLlegada =
                                                                     escala.Field<DateTime>("FechaLlegada"),
                                                                 Activo = escala.Field<bool>("Activo").BoolAEnum(),
                                                                 Horas = escala.Field<decimal>("Horas"),
                                                                 Orden = escala.Field<int>("Orden"),
                                                                 Kilometros = escala.Field<decimal>("Kilometros"),
                                                                 Recibido = escala.Field<bool>("Recibido"),
                                                             }
                                                            ).ToList()
                                          }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return progracionEmbarqueInfo;
        }

        /// <summary>
        ///     Metodo que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static IList<EmbarqueDetalleInfo> ObtenerEscalasPorEmbarqueID(DataSet ds)
        {
            IList<EmbarqueDetalleInfo> result;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                result = (from escala in dt.AsEnumerable()
                          select new EmbarqueDetalleInfo
                          {
                              EmbarqueDetalleID = escala.Field<int>("EmbarqueDetalleID"),
                              EmbarqueID = escala.Field<int>("EmbarqueID"),
                              Proveedor = new ProveedorInfo { ProveedorID = escala.Field<int>("ProveedorID") },
                              Chofer = new ChoferInfo { ChoferID = escala.Field<int>("ChoferID") },
                              Jaula = new JaulaInfo { JaulaID = escala.Field<int>("JaulaID") },
                              Camion = new CamionInfo { CamionID = escala.Field<int>("CamionID") },
                              OrganizacionOrigen = new OrganizacionInfo { OrganizacionID = escala.Field<int>("OrganizacionOrigenID") },
                              OrganizacionDestino = new OrganizacionInfo { OrganizacionID = escala.Field<int>("OrganizacionDestinoID") },
                              FechaSalida = escala.Field<DateTime>("FechaSalida"),
                              FechaLlegada = escala.Field<DateTime>("FechaLlegada"),
                              Horas = escala.Field<decimal>("Horas"),
                              Recibido = escala.Field<bool>("Recibido"),
                              Comentarios = escala.Field<string>("Comentarios"),
                              Activo = escala.Field<bool>("Activo").BoolAEnum(),
                              Orden = escala.Field<int>("Orden")
                          }
                         ).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return result;
        }
    }
}