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
    public class MapProduccionDiariaDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<ProduccionDiariaInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ProduccionDiariaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ProduccionDiariaInfo
                             {
								ProduccionDiariaID = info.Field<int>("ProduccionDiariaID"),
								Turno = info.Field<int>("Turno"),
								LitrosInicial = info.Field<decimal>("LitrosInicial"),
								LitrosFinal = info.Field<decimal>("LitrosFinal"),
								HorometroInicial = info.Field<int>("HorometroInicial"),
								HorometroFinal = info.Field<int>("HorometroFinal"),
								FechaProduccion = info.Field<DateTime>("FechaProduccion"),
								Usuario = new UsuarioInfo { UsuarioID = info.Field<int>("UsuarioID")},
								Observaciones = info.Field<string>("Observaciones"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<ProduccionDiariaInfo>
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
        public static List<ProduccionDiariaInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ProduccionDiariaInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ProduccionDiariaInfo
                             {
								ProduccionDiariaID = info.Field<int>("ProduccionDiariaID"),
								Turno = info.Field<int>("Turno"),
								LitrosInicial = info.Field<decimal>("LitrosInicial"),
								LitrosFinal = info.Field<decimal>("LitrosFinal"),
								HorometroInicial = info.Field<int>("HorometroInicial"),
								HorometroFinal = info.Field<int>("HorometroFinal"),
								FechaProduccion = info.Field<DateTime>("FechaProduccion"),
								Usuario = new UsuarioInfo { UsuarioID = info.Field<int>("UsuarioID")},
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
        public static ProduccionDiariaInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ProduccionDiariaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ProduccionDiariaInfo
                             {
								ProduccionDiariaID = info.Field<int>("ProduccionDiariaID"),
								Turno = info.Field<int>("Turno"),
								LitrosInicial = info.Field<decimal>("LitrosInicial"),
								LitrosFinal = info.Field<decimal>("LitrosFinal"),
								HorometroInicial = info.Field<int>("HorometroInicial"),
								HorometroFinal = info.Field<int>("HorometroFinal"),
								FechaProduccion = info.Field<DateTime>("FechaProduccion"),
								Usuario = new UsuarioInfo { UsuarioID = info.Field<int>("UsuarioID")},
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
        public static ProduccionDiariaInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ProduccionDiariaInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ProduccionDiariaInfo
                             {
								ProduccionDiariaID = info.Field<int>("ProduccionDiariaID"),
								Turno = info.Field<int>("Turno"),
								LitrosInicial = info.Field<decimal>("LitrosInicial"),
								LitrosFinal = info.Field<decimal>("LitrosFinal"),
								HorometroInicial = info.Field<int>("HorometroInicial"),
								HorometroFinal = info.Field<int>("HorometroFinal"),
								FechaProduccion = info.Field<DateTime>("FechaProduccion"),
								Usuario = new UsuarioInfo { UsuarioID = info.Field<int>("UsuarioID")},
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
    }
}

