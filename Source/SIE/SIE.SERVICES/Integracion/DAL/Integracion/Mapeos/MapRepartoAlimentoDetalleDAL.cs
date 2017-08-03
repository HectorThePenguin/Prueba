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
    public class MapRepartoAlimentoDetalleDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<RepartoAlimentoDetalleInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<RepartoAlimentoDetalleInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new RepartoAlimentoDetalleInfo
                             {
								RepartoAlimentoDetalleID = info.Field<int>("RepartoAlimentoDetalleID"),
								RepartoAlimento = new RepartoAlimentoInfo { RepartoAlimentoID = info.Field<int>("RepartoAlimentoID")},
								FolioReparto = info.Field<int>("FolioReparto"),
                                FormulaIDRacion = info.Field<int>("FormulaIDRacion"),
								Tolva = info.Field<string>("Tolva"),
								KilosEmbarcados = info.Field<int>("KilosEmbarcados"),
								KilosRepartidos = info.Field<int>("KilosRepartidos"),
								Sobrante = info.Field<int>("Sobrante"),
								CorralInicio = new CorralInfo { CorralID = info.Field<int>("CorralID"), Codigo = info.Field<string>("Codigo") },
								CorralFinal = new CorralInfo { CorralID = info.Field<int>("CorralID"), Codigo = info.Field<string>("Codigo") },
								HoraRepartoInicio = info.Field<string>("HoraRepartoInicio"),
								HoraRepartoFinal = info.Field<string>("HoraRepartoFinal"),
								Observaciones = info.Field<string>("Observaciones"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<RepartoAlimentoDetalleInfo>
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
        public static List<RepartoAlimentoDetalleInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<RepartoAlimentoDetalleInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new RepartoAlimentoDetalleInfo
                             {
                                 RepartoAlimentoDetalleID = info.Field<int>("RepartoAlimentoDetalleID"),
                                 RepartoAlimento = new RepartoAlimentoInfo { RepartoAlimentoID = info.Field<int>("RepartoAlimentoID") },
                                 FolioReparto = info.Field<int>("FolioReparto"),
                                 FormulaIDRacion = info.Field<int>("FormulaIDRacion"),
                                 Tolva = info.Field<string>("Tolva"),
                                 KilosEmbarcados = info.Field<int>("KilosEmbarcados"),
                                 KilosRepartidos = info.Field<int>("KilosRepartidos"),
                                 Sobrante = info.Field<int>("Sobrante"),
                                 CorralInicio = new CorralInfo { CorralID = info.Field<int>("CorralID"), Codigo = info.Field<string>("Codigo") },
                                 CorralFinal = new CorralInfo { CorralID = info.Field<int>("CorralID"), Codigo = info.Field<string>("Codigo") },
                                 HoraRepartoInicio = info.Field<string>("HoraRepartoInicio"),
                                 HoraRepartoFinal = info.Field<string>("HoraRepartoFinal"),
                                 Observaciones = info.Field<string>("Observaciones"),
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
        public static RepartoAlimentoDetalleInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                RepartoAlimentoDetalleInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new RepartoAlimentoDetalleInfo
                             {
                                 RepartoAlimentoDetalleID = info.Field<int>("RepartoAlimentoDetalleID"),
                                 RepartoAlimento = new RepartoAlimentoInfo { RepartoAlimentoID = info.Field<int>("RepartoAlimentoID") },
                                 FolioReparto = info.Field<int>("FolioReparto"),
                                 FormulaIDRacion = info.Field<int>("FormulaIDRacion"),
                                 Tolva = info.Field<string>("Tolva"),
                                 KilosEmbarcados = info.Field<int>("KilosEmbarcados"),
                                 KilosRepartidos = info.Field<int>("KilosRepartidos"),
                                 Sobrante = info.Field<int>("Sobrante"),
                                 CorralInicio = new CorralInfo { CorralID = info.Field<int>("CorralID"), Codigo = info.Field<string>("Codigo") },
                                 CorralFinal = new CorralInfo { CorralID = info.Field<int>("CorralID"), Codigo = info.Field<string>("Codigo") },
                                 HoraRepartoInicio = info.Field<string>("HoraRepartoInicio"),
                                 HoraRepartoFinal = info.Field<string>("HoraRepartoFinal"),
                                 Observaciones = info.Field<string>("Observaciones"),
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
        public static RepartoAlimentoDetalleInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                RepartoAlimentoDetalleInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new RepartoAlimentoDetalleInfo
                             {
                                 RepartoAlimentoDetalleID = info.Field<int>("RepartoAlimentoDetalleID"),
                                 RepartoAlimento = new RepartoAlimentoInfo { RepartoAlimentoID = info.Field<int>("RepartoAlimentoID") },
                                 FolioReparto = info.Field<int>("FolioReparto"),
                                 FormulaIDRacion = info.Field<int>("FormulaIDRacion"),
                                 Tolva = info.Field<string>("Tolva"),
                                 KilosEmbarcados = info.Field<int>("KilosEmbarcados"),
                                 KilosRepartidos = info.Field<int>("KilosRepartidos"),
                                 Sobrante = info.Field<int>("Sobrante"),
                                 CorralInicio = new CorralInfo { CorralID = info.Field<int>("CorralID"), Codigo = info.Field<string>("Codigo") },
                                 CorralFinal = new CorralInfo { CorralID = info.Field<int>("CorralID"), Codigo = info.Field<string>("Codigo") },
                                 HoraRepartoInicio = info.Field<string>("HoraRepartoInicio"),
                                 HoraRepartoFinal = info.Field<string>("HoraRepartoFinal"),
                                 Observaciones = info.Field<string>("Observaciones"),
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
        /// Método que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static List<RepartoAlimentoDetalleInfo> ObtenerPorRepartoAlimentoID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<RepartoAlimentoDetalleInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new RepartoAlimentoDetalleInfo
                         {
                             RepartoAlimentoDetalleID = info.Field<int>("RepartoAlimentoDetalleID"),
                             RepartoAlimento = new RepartoAlimentoInfo { RepartoAlimentoID = info.Field<int>("RepartoAlimentoID") },
                             FolioReparto = info.Field<int>("FolioReparto"),
                             FormulaIDRacion = info.Field<int>("FormulaIDRacion"),
                             Tolva = info.Field<string>("Tolva"),
                             KilosEmbarcados = info.Field<int>("KilosEmbarcados"),
                             KilosRepartidos = info.Field<int>("KilosRepartidos"),
                             Sobrante = info.Field<int>("Sobrante"),
                             CorralInicio = new CorralInfo { CorralID = info.Field<int>("CorralIDInicio"), Codigo = info.Field<string>("CorralInicio") },
                             CorralFinal = new CorralInfo { CorralID = info.Field<int>("CorralIDFinal"), Codigo = info.Field<string>("CorralFinal") },
                             HoraRepartoInicio = info.Field<string>("HoraRepartoInicio"),
                             HoraRepartoFinal = info.Field<string>("HoraRepartoFinal"),
                             Observaciones = info.Field<string>("Observaciones"),
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
    }
}

