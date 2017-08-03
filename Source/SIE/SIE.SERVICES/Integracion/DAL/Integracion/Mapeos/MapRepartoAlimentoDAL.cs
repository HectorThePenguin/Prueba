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
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    public class MapRepartoAlimentoDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<RepartoAlimentoInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<RepartoAlimentoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new RepartoAlimentoInfo
                             {
								RepartoAlimentoID = info.Field<int>("RepartoAlimentoID"),
								TipoServicio = new TipoServicioInfo { TipoServicioId = info.Field<int>("TipoServicioID"), Descripcion = info.Field<string>("TipoServicio") },
								CamionReparto = new CamionRepartoInfo { CamionRepartoID = info.Field<int>("CamionRepartoID"), NumeroEconomico = info.Field<string>("NumeroEconomico") },
								Usuario = new UsuarioInfo { UsuarioID = info.Field<int>("UsuarioID")},
								HorometroInicial = info.Field<int>("HorometroInicial"),
								HorometroFinal = info.Field<int>("HorometroFinal"),
								OdometroInicial = info.Field<int>("OdometroInicial"),
								OdometroFinal = info.Field<int>("OdometroFinal"),
								LitrosDiesel = info.Field<int>("LitrosDiesel"),
								FechaReparto = info.Field<DateTime>("FechaReparto"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<RepartoAlimentoInfo>
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
        /// Método que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static List<RepartoAlimentoInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<RepartoAlimentoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new RepartoAlimentoInfo
                             {
								RepartoAlimentoID = info.Field<int>("RepartoAlimentoID"),
                                TipoServicio = new TipoServicioInfo { TipoServicioId = info.Field<int>("TipoServicioID"), Descripcion = info.Field<string>("TipoServicio") },
                                CamionReparto = new CamionRepartoInfo { CamionRepartoID = info.Field<int>("CamionRepartoID"), NumeroEconomico = info.Field<string>("NumeroEconomico") },
                                Usuario = new UsuarioInfo { UsuarioID = info.Field<int>("UsuarioID") },
								HorometroInicial = info.Field<int>("HorometroInicial"),
								HorometroFinal = info.Field<int>("HorometroFinal"),
								OdometroInicial = info.Field<int>("OdometroInicial"),
								OdometroFinal = info.Field<int>("OdometroFinal"),
								LitrosDiesel = info.Field<int>("LitrosDiesel"),
								FechaReparto = info.Field<DateTime>("FechaReparto"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();
                return lista;
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
        public static RepartoAlimentoInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                RepartoAlimentoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new RepartoAlimentoInfo
                             {
								RepartoAlimentoID = info.Field<int>("RepartoAlimentoID"),
                                TipoServicio = new TipoServicioInfo { TipoServicioId = info.Field<int>("TipoServicioID"), Descripcion = info.Field<string>("TipoServicio") },
                                CamionReparto = new CamionRepartoInfo { CamionRepartoID = info.Field<int>("CamionRepartoID"), NumeroEconomico = info.Field<string>("NumeroEconomico") },
                                Usuario = new UsuarioInfo { UsuarioID = info.Field<int>("UsuarioID") },
								HorometroInicial = info.Field<int>("HorometroInicial"),
								HorometroFinal = info.Field<int>("HorometroFinal"),
								OdometroInicial = info.Field<int>("OdometroInicial"),
								OdometroFinal = info.Field<int>("OdometroFinal"),
								LitrosDiesel = info.Field<int>("LitrosDiesel"),
								FechaReparto = info.Field<DateTime>("FechaReparto"),
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
        public static RepartoAlimentoInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                RepartoAlimentoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new RepartoAlimentoInfo
                             {
								RepartoAlimentoID = info.Field<int>("RepartoAlimentoID"),
                                TipoServicio = new TipoServicioInfo { TipoServicioId = info.Field<int>("TipoServicioID"), Descripcion = info.Field<string>("TipoServicio") },
                                CamionReparto = new CamionRepartoInfo { CamionRepartoID = info.Field<int>("CamionRepartoID"), NumeroEconomico = info.Field<string>("NumeroEconomico") },
                                Usuario = new UsuarioInfo { UsuarioID = info.Field<int>("UsuarioID") },
								HorometroInicial = info.Field<int>("HorometroInicial"),
								HorometroFinal = info.Field<int>("HorometroFinal"),
								OdometroInicial = info.Field<int>("OdometroInicial"),
								OdometroFinal = info.Field<int>("OdometroFinal"),
								LitrosDiesel = info.Field<int>("LitrosDiesel"),
								FechaReparto = info.Field<DateTime>("FechaReparto"),
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
        public static RepartoAlimentoInfo ObtenerPorConsultarRepartos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtDetalle = ds.Tables[ConstantesDAL.DtDetalle];
                DataTable dtTiemposMuertos = ds.Tables[ConstantesDAL.DtTiemposMuertos];
                RepartoAlimentoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new RepartoAlimentoInfo
                         {
                             RepartoAlimentoID = info.Field<int>("RepartoAlimentoID"),
                             TipoServicioID = info.Field<int>("TipoServicioID"),
                             CamionRepartoID = info.Field<int>("CamionRepartoID"),
                             UsuarioIDReparto = info.Field<int>("UsuarioIDReparto"),
                             HorometroInicial = info.Field<int>("HorometroInicial"),
                             HorometroFinal = info.Field<int>("HorometroFinal"),
                             OdometroInicial = info.Field<int>("OdometroInicial"),
                             OdometroFinal = info.Field<int>("OdometroFinal"),
                             LitrosDiesel = info.Field<int>("LitrosDiesel"),
                             FechaReparto = info.Field<DateTime>("FechaReparto"),
                             ListaGridRepartos = (from detalle in dtDetalle.AsEnumerable()
                                                      select new GridRepartosModel
                                                          {
                                                              RepartoAlimentoID = detalle.Field<int>("RepartoAlimentoID"),
                                                              RepartoAlimentoDetalleID = detalle.Field<int>("RepartoAlimentoDetalleID"),
                                                              NumeroTolva = detalle.Field<string>("Tolva"),
                                                              Servicio = info.Field<int>("TipoServicioID"),
                                                              Reparto = detalle.Field<int>("FolioReparto"),
                                                              RacionFormula = detalle.Field<int>("FormulaIDRacion"),
                                                              KilosEmbarcados = detalle.Field<int>("KilosEmbarcados"),
                                                              KilosRepartidos = detalle.Field<int>("KilosRepartidos"),
                                                              Sobrante = detalle.Field<int>("Sobrante"),
                                                              PesoFinal = detalle.Field<int>("PesoFinal"),
                                                              CorralInicio = detalle.Field<string>("CorralInicio"),
                                                              CorralFinal = detalle.Field<string>("CorralFinal"),
                                                              HoraInicioReparto = detalle.Field<string>("HoraRepartoInicio"),
                                                              HoraFinalReparto = detalle.Field<string>("HoraRepartoFinal"),
                                                              Observaciones = detalle.Field<string>("Observaciones"),
                                                          }).ToList(),
                                                          ListaTiempoMuerto = (from tiempo in dtTiemposMuertos.AsEnumerable()
                                                                                   select new TiempoMuertoInfo
                                                                                       {
                                                                                           TiempoMuertoID = tiempo.Field<int>("TiempoMuertoID"),
                                                                                           RepartoAlimentoID = tiempo.Field<int>("RepartoAlimentoID"),
                                                                                           HoraInicio = tiempo.Field<string>("HoraInicio"),
                                                                                           HoraFin = tiempo.Field<string>("HoraFin"),
                                                                                           CausaTiempoMuerto = new CausaTiempoMuertoInfo
                                                                                               {
                                                                                                   CausaTiempoMuertoID = tiempo.Field<int>("CausaTiempoMuertoID"),
                                                                                                   Descripcion = tiempo.Field<string>("CausaTiempoMuerto")
                                                                                               }
                                                                                       }).ToList()
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
        public static List<RepartoAlimentoInfo> ObtenerPorImprimirRepartos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtDetalle = ds.Tables[ConstantesDAL.DtDetalle];
                DataTable dtTiemposMuertos = ds.Tables[ConstantesDAL.DtTiemposMuertos];
                List<RepartoAlimentoInfo> entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new RepartoAlimentoInfo
                         {
                             RepartoAlimentoID = info.Field<int>("RepartoAlimentoID"),
                             TipoServicioID = info.Field<int>("TipoServicioID"),
                             Usuario = new UsuarioInfo
                                 {
                                     UsuarioID = info.Field<int>("UsuarioIDReparto"),
                                     Nombre = info.Field<string>("UsuarioReparto"),
                                 },
                                 CamionReparto = new CamionRepartoInfo
                                     {
                                         CamionRepartoID = info.Field<int>("CamionRepartoID"),
                                         NumeroEconomico = info.Field<string>("NumeroEconomico"),
                                     },
                             HorometroInicial = info.Field<int>("HorometroInicial"),
                             HorometroFinal = info.Field<int>("HorometroFinal"),
                             OdometroInicial = info.Field<int>("OdometroInicial"),
                             OdometroFinal = info.Field<int>("OdometroFinal"),
                             LitrosDiesel = info.Field<int>("LitrosDiesel"),
                             FechaReparto = info.Field<DateTime>("FechaReparto"),
                             ListaGridRepartos = (from detalle in dtDetalle.AsEnumerable()
                                                  where info.Field<int>("RepartoAlimentoID") == detalle.Field<int>("RepartoAlimentoID")
                                                  select new GridRepartosModel
                                                  {
                                                      RepartoAlimentoID = detalle.Field<int>("RepartoAlimentoID"),
                                                      RepartoAlimentoDetalleID = detalle.Field<int>("RepartoAlimentoDetalleID"),
                                                      NumeroTolva = detalle.Field<string>("Tolva"),
                                                      Servicio = info.Field<int>("TipoServicioID"),
                                                      Reparto = detalle.Field<int>("FolioReparto"),
                                                      RacionFormula = detalle.Field<int>("FormulaIDRacion"),
                                                      KilosEmbarcados = detalle.Field<int>("KilosEmbarcados"),
                                                      KilosRepartidos = detalle.Field<int>("KilosRepartidos"),
                                                      Sobrante = detalle.Field<int>("Sobrante"),
                                                      PesoFinal = detalle.Field<int>("PesoFinal"),
                                                      CorralInicio = detalle.Field<string>("CorralInicio"),
                                                      CorralFinal = detalle.Field<string>("CorralFinal"),
                                                      HoraInicioReparto = detalle.Field<string>("HoraRepartoInicio"),
                                                      HoraFinalReparto = detalle.Field<string>("HoraRepartoFinal"),
                                                      Observaciones = detalle.Field<string>("Observaciones"),
                                                  }).ToList(),
                                                  ListaTiempoMuerto = (from tiempoMuerto in dtTiemposMuertos.AsEnumerable()
                                                                           where info.Field<int>("RepartoAlimentoID") == tiempoMuerto.Field<int>("RepartoAlimentoID")
                                                                           select new TiempoMuertoInfo
                                                                               {
                                                                                   HoraInicio = tiempoMuerto.Field<string>("HoraInicio"),
                                                                                   HoraFin = tiempoMuerto.Field<string>("HoraFin"),
                                                                                   CausaTiempoMuerto = new CausaTiempoMuertoInfo
                                                                                       {
                                                                                           CausaTiempoMuertoID = tiempoMuerto.Field<int>("CausaTiempoMuertoId"),
                                                                                           Descripcion = tiempoMuerto.Field<string>("CausaTiempoMuerto")
                                                                                       }
                                                                               }).ToList()
                         }).ToList();
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

