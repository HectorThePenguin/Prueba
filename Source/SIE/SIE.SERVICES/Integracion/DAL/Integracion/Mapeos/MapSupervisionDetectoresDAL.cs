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
    public class MapSupervisionDetectoresDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<SupervisionDetectoresInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<SupervisionDetectoresInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new SupervisionDetectoresInfo
                             {
								SupervisionDetectoresId = info.Field<int>("SupervisionDetectoresID"),
								OrganizacionId = info.Field<int>("OrganizacionID"),
								OperadorId = info.Field<int>("OperadorID"),
								FechaSupervision = info.Field<DateTime>("FechaSupervision"),
								CriterioSupervisionId = info.Field<int>("CriterioSupervisionID"),
								Observaciones = info.Field<string>("Observaciones"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<SupervisionDetectoresInfo>
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
        public static List<SupervisionDetectoresInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<SupervisionDetectoresInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new SupervisionDetectoresInfo
                             {
                                 SupervisionDetectoresId = info.Field<int>("SupervisionDetectoresID"),
                                 OrganizacionId = info.Field<int>("OrganizacionID"),
                                 OperadorId = info.Field<int>("OperadorID"),
                                 FechaSupervision = info.Field<DateTime>("FechaSupervision"),
                                 CriterioSupervisionId = info.Field<int>("CriterioSupervisionID"),
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
        public static SupervisionDetectoresInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                SupervisionDetectoresInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new SupervisionDetectoresInfo
                             {
                                 SupervisionDetectoresId = info.Field<int>("SupervisionDetectoresID"),
                                 OrganizacionId = info.Field<int>("OrganizacionID"),
                                 OperadorId = info.Field<int>("OperadorID"),
                                 FechaSupervision = info.Field<DateTime>("FechaSupervision"),
                                 CriterioSupervisionId = info.Field<int>("CriterioSupervisionID"),
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
        public static SupervisionDetectoresInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                SupervisionDetectoresInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new SupervisionDetectoresInfo
                             {
                                 SupervisionDetectoresId = info.Field<int>("SupervisionDetectoresID"),
                                 OrganizacionId = info.Field<int>("OrganizacionID"),
                                 OperadorId = info.Field<int>("OperadorID"),
                                 FechaSupervision = info.Field<DateTime>("FechaSupervision"),
                                 CriterioSupervisionId = info.Field<int>("CriterioSupervisionID"),
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
        public static List<ImpresionSupervisionDetectoresModel> ObtenerSupervisionDetectoresImpresion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DataTable dtDetalle = ds.Tables[ConstantesDAL.DtDetalle];
                List<ImpresionSupervisionDetectoresModel> entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new ImpresionSupervisionDetectoresModel
                         {
                             SupervisionDetectoresID = info.Field<int>("SupervisionDetectoresID"),
                             Organizacion = info.Field<string>("Organizacion"),
                             Detector = info.Field<string>("Detector"),
                             FechaSupervision = info.Field<DateTime>("FechaSupervision"),
                             CriterioSupervision = info.Field<string>("CriterioSupervision"),
                             ValorInicial = info.Field<int>("ValorInicial"),
                             ValorFinal = info.Field<int>("ValorFinal"),
                             CodigoColor = info.Field<string>("CodigoColor"),
                             Observaciones = info.Field<string>("Observaciones"),
                             Detalles = (from det in dtDetalle.AsEnumerable()
                                             where det.Field<int>("SupervisionDetectoresID") == info.Field<int>("SupervisionDetectoresID")
                                             select new ImpresionSupervisionDetectoresDetalleModel
                                                 {
                                                     SupervisionDetectoresID = det.Field<int>("SupervisionDetectoresID"),
                                                     PreguntaID = det.Field<int>("PreguntaID"),
                                                     Respuesta = det.Field<int>("Respuesta"),
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

