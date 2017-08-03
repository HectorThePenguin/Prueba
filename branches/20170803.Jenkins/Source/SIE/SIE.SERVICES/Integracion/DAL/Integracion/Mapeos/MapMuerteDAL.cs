using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Services.Info.Info;
using System.Data;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    /// <summary>
    /// Clase para administrar los mapeos de datos del acceso a datos para Muerte
    /// </summary>
    internal class MapMuerteDAL
    {
        /// <summary>
        /// Mapa de datos para la lista del grid de salida por muerte en necropsia
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<MuerteInfo> ObtenerListaParaSalidaNecropsia(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                IList<MuerteInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new MuerteInfo()
                         {
                             /* MU.MuerteId, MU.Arete, MU.AreteMetalico, MU.Observaciones, MU.LoteId, MU.OperadorDeteccion, MU.FechaDeteccion, MU.FotoDeteccion,
			                    MU.OperadorRecoleccion, MU.FechaRecoleccion, MU.OperadorNecropsia, MU.FechaNecropsia, MU.EstatusID, MU.ProblemaID, MU.FechaCreacion,
		                        LT.Lote, CR.Codigo,MU.Activo, LT.OrganizacionId */
                             OrganizacionId = info.Field<int>("OrganizacionId"),
                             MuerteId = info.Field<int>("MuerteId"),
                             Arete = info.Field<string>("Arete").Trim(),
                             AreteMetalico = info["AreteMetalico"] == DBNull.Value ? String.Empty : info.Field<string>("AreteMetalico"),
                             Observaciones = info.Field<string>("Observaciones").Trim(),
                             LoteId = info.Field<int>("LoteId"),
                             OperadorDeteccionId = info.Field<int>("OperadorDeteccion"),
                             FechaDeteccion = info["FechaDeteccion"] == DBNull.Value ? new DateTime(1900,1,1) : info.Field<DateTime>("FechaDeteccion"),
                             FotoDeteccion = info["FotoDeteccion"] == DBNull.Value ? String.Empty : info.Field<string>("FotoDeteccion").Trim(),
                             OperadorRecoleccionId = info.Field<int>("OperadorRecoleccion"),
                             FechaRecoleccion = info["FechaRecoleccion"] == DBNull.Value ? new DateTime(1900, 1, 1) : info.Field<DateTime>("FechaRecoleccion"),
                             OperadorNecropsiaId = info["OperadorNecropsia"] == DBNull.Value ? 0 : info.Field<int>("OperadorNecropsia"),
                             FechaNecropsia = info["FechaNecropsia"] == DBNull.Value ? new DateTime(1900, 1, 1) : info.Field<DateTime>("FechaNecropsia"),
                             EstatusId = info.Field<int>("EstatusID"),
                             ProblemaId = info["ProblemaID"] == DBNull.Value ? 0 : info.Field<int>("ProblemaID"),
                             FechaCreacion = info["FechaCreacion"] == DBNull.Value ? new DateTime(1900, 1, 1) : info.Field<DateTime>("FechaCreacion"),
                             LoteCodigo = info.Field<string>("Lote").Trim(),
                             CorralCodigo = info.Field<string>("Codigo").Trim(),
                             Activo = info.Field<bool>("Activo").BoolAEnum()
                             
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
        /// Mapa de datos para la lista del grid de salida por muerte en necropsia
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<MuerteInfo> ObtenerListaParaRecepcionNecropsia(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                IList<MuerteInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new MuerteInfo()
                         {
                             /* MU.MuerteId, MU.Arete, MU.AreteMetalico, MU.Observaciones, MU.LoteId, MU.OperadorDeteccion, MU.FechaDeteccion, MU.FotoDeteccion,
			                    MU.OperadorRecoleccion, MU.FechaRecoleccion, MU.OperadorNecropsia, MU.FechaNecropsia, MU.EstatusID, MU.ProblemaID, MU.FechaCreacion,
		                        LT.Lote, CR.Codigo,MU.Activo, LT.OrganizacionId */
                             OrganizacionId = info.Field<int>("OrganizacionId"),
                             MuerteId = info.Field<int>("MuerteId"),
                             Arete = info.Field<string>("Arete").Trim(),
                             AreteMetalico = info.Field<string>("AreteMetalico").Trim(),
                             Observaciones = info.Field<string>("Observaciones").Trim(),
                             LoteId = info.Field<int>("LoteId"),
                             OperadorDeteccionId = info.Field<int>("OperadorDeteccion"),
                             FechaDeteccion = info.Field<DateTime>("FechaDeteccion"),
                             FotoDeteccion = info.Field<string>("FotoDeteccion").Trim(),
                             OperadorRecoleccionId = info.Field<int>("OperadorRecoleccion"),
                             FechaRecoleccion = info.Field<DateTime>("FechaRecoleccion"),
                             EstatusId = info.Field<int>("EstatusID"),
                             ProblemaId = info.Field<int>("ProblemaID"),
                             FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                             LoteCodigo = info.Field<string>("Lote").Trim(),
                             CorralCodigo = info.Field<string>("Codigo").Trim(),
                             Activo = info.Field<bool>("Activo").BoolAEnum()

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
        /// Obtiene los datos mapeados de ganado muerto
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static MuerteInfo ObtenerGanadoMuertoPorArete(DataSet ds)
        {
            try
            {

                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                MuerteInfo result =
                      (from info in dt.AsEnumerable()
                       select
                           new MuerteInfo
                           {
                             /* MU.MuerteId, MU.Arete, MU.AreteMetalico, MU.Observaciones, MU.LoteId, MU.OperadorDeteccion, MU.FechaDeteccion, MU.FotoDeteccion,
			                    MU.OperadorRecoleccion, MU.FechaRecoleccion, MU.OperadorNecropsia, MU.FechaNecropsia, MU.EstatusID, MU.ProblemaID, MU.FechaCreacion,
		                        LT.Lote, LT.OrganizacionId, CR.Codigo,MU.Activo, AnimalId, peso */
                               OrganizacionId = info.Field<int>("OrganizacionId"),
                               AnimalId = info.Field<long>("AnimalId"),
                               MuerteId = info.Field<int>("MuerteId"),
                               Arete = info.Field<string>("Arete").Trim(),
                               AreteMetalico = info["AreteMetalico"] == DBNull.Value ? String.Empty : info.Field<string>("AreteMetalico"),
                               Observaciones = info.Field<string>("Observaciones").Trim(),
                               LoteId = info.Field<int>("LoteId"),
                               OperadorDeteccionId = info["OperadorDeteccion"] == DBNull.Value ? 0 : info.Field<int>("OperadorDeteccion"),
                               FechaDeteccion = info["FechaDeteccion"] == DBNull.Value ? new DateTime(1900, 1, 1) : info.Field<DateTime>("FechaDeteccion"),
                               FotoDeteccion = info["FotoDeteccion"] == DBNull.Value ? String.Empty : info.Field<string>("FotoDeteccion").Trim(),
                               OperadorRecoleccionId = info.Field<int>("OperadorRecoleccion"),
                               FechaRecoleccion = info["FechaRecoleccion"] == DBNull.Value ? new DateTime(1900, 1, 1) : info.Field<DateTime>("FechaRecoleccion"),
                               OperadorNecropsiaId = info["OperadorNecropsia"] == DBNull.Value ? 0 : info.Field<int>("OperadorNecropsia"),
                               FechaNecropsia = info["FechaNecropsia"] == DBNull.Value ? new DateTime(1900, 1, 1) : info.Field<DateTime>("FechaNecropsia"),
                               EstatusId = info.Field<int>("EstatusID"),
                               ProblemaId = info["ProblemaID"] == DBNull.Value ? 0 : info.Field<int>("ProblemaID"),
                               FechaCreacion = info["FechaNecropsia"] == DBNull.Value ? new DateTime(1900, 1, 1) : info.Field<DateTime>("FechaCreacion"),
                               LoteCodigo = info.Field<string>("Lote").Trim(),
                               CorralCodigo = info.Field<string>("Codigo").Trim(),
                               CorralId =  info.Field<int>("CorralId"),
                               Activo = info.Field<bool>("Activo").BoolAEnum(),
                               Peso = info["Peso"] == DBNull.Value ? 0 : info.Field<int>("Peso")
                               //FechaRecoleccion = info["FechaRecoleccion"] == DBNull.Value ? new DateTime(1900, 1, 1) : info.Field<DateTime>("FechaRecoleccion"),
                         }).First();

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }


        /// <summary>
        /// Mapea los datos ue se utilizan en el grid de la lista de movimientos(Muertes) a cancelar
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<MuerteInfo> ObtenerInformacionCancelarMovimiento(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                IList<MuerteInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new MuerteInfo()
                         {
                             MuerteId = info.Field<int>("MuerteID"),
                             Arete = info.Field<string>("Arete").Trim(),
                             AreteMetalico = info.Field<string>("AreteMetalico").Trim(),
                             CorralCodigo = info.Field<string>("Codigo").Trim(),
                             FechaDeteccion = info.Field<DateTime>("FechaDeteccion"),
                             OperadorDeteccionInfo = new OperadorInfo
                             {
                                 OperadorID = info.Field<int>("OperadorDeteccion"),
                                 Nombre = info.Field<string>("OperadorDeteccionNombre")
                             },
                             OperadorRecoleccionInfo = new OperadorInfo
                             {
                                 OperadorID = info.Field<int>("OperadorRecoleccion"),
                                 Nombre = info.Field<string>("OperadorRecoleccionNombre")
                             },
                             
                             

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
        /// Mapea los datos de la tabla muerte
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<MuerteInfo> ObtenerAretesMuertosRecoleccion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                var dalOperador = new OperadorDAL();
                IList<MuerteInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new MuerteInfo()
                         {
                             OrganizacionId = info.Field<int>("OrganizacionId"),
                             MuerteId = info.Field<int>("MuerteId"),
                             Arete = info.Field<string>("Arete").Trim(),
                             AreteMetalico = info.Field<string>("AreteMetalico").Trim(),
                             Observaciones = info.Field<string>("Observaciones").Trim(),
                             LoteId = info.Field<int>("LoteId"),
                             LoteCodigo = info.Field<string>("Lote").Trim(),
                             CorralId = info.Field<int>("CorralId"),
                             CorralCodigo = info.Field<string>("Codigo").Trim(),
                             OperadorDeteccionId = info.Field<int>("OperadorDeteccion"),
                             OperadorDeteccionInfo = dalOperador.ObtenerPorID(info.Field<int>("OperadorDeteccion")),
                             FechaDeteccion = info.Field<DateTime>("FechaDeteccion"),
                             FotoDeteccion = info.Field<string>("FotoDeteccion").Trim(),
                             EstatusId = info.Field<int>("EstatusID"),
                             ProblemaId = info.Field<int>("ProblemaID"),
                             FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             Comentarios = info.Field<string>("Comentarios").Trim()
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
        /// Mapea los datos para las muertes con fecha necropsia
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<SalidaGanadoMuertoInfo> ObtenerMuertesFechaNecropsia(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                IList<SalidaGanadoMuertoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new SalidaGanadoMuertoInfo()
                         {
                             MuerteID = info.Field<int>("MuerteID"),
                             FolioSalida = info.Field<int>("FolioSalida"),
                             CodigoCorral = info.Field<string>("Corral"),
                             Arete = info.Field<string>("Arete").Trim(),
                             AreteTestigo = info.Field<string>("AreteTestigo").Trim(),
                             Sexo = info.Field<string>("Sexo").Trim(),
                             TipoGanado = info.Field<string>("TipoGanado"),
                             Peso = info.Field<int>("Peso"),
                             Causa = info.Field<string>("Causa")
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
        /// Obtiene los aretes muertos en el dia para ese lote
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<MuerteInfo> ObtenerGanadoMuertoPorLoteID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                IList<MuerteInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new MuerteInfo
                         {
                             MuerteId = info.Field<int>("MuerteID"),
                             Arete = info.Field<string>("Arete"),
                             AreteMetalico = info.Field<string>("AreteMetalico"),
                             Observaciones = info.Field<string>("Observaciones"),
                             LoteId = info.Field<int>("LoteID"),
                             OperadorDeteccionId = info.Field<int>("OperadorDeteccion"),
                             FechaDeteccion = info.Field<DateTime>("FechaDeteccion"),
                             FotoDeteccion = info.Field<string>("FotoDeteccion"),
                             OperadorRecoleccionId = info.Field<int?>("OperadorRecoleccion") != null ? info.Field<int>("OperadorRecoleccion") : 0,
                             FechaRecoleccion = info.Field<DateTime?>("FechaRecoleccion") != null ? info.Field<DateTime>("FechaRecoleccion") : DateTime.MinValue,
                             OperadorNecropsiaId = info.Field<int?>("OperadorNecropsia") != null ? info.Field<int>("OperadorNecropsia") : 0,
                             FechaNecropsia = info.Field<DateTime?>("FechaNecropsia") != null ? info.Field<DateTime>("FechaNecropsia") : DateTime.MinValue,
                             FotoNecropsia = info.Field<string>("FotoNecropsia"),
                             FolioSalida = info.Field<int?>("FolioSalida") != null ? info.Field<int>("FolioSalida") : 0,
                             OperadorCancelacion = info.Field<int?>("OperadorCancelacion") != null ? info.Field<int>("OperadorCancelacion") : 0,
                             FechaCancelacion = info.Field<DateTime?>("FechaCancelacion") != null ? info.Field<DateTime>("FechaCancelacion") : DateTime.MinValue,
                             MotivoCancelacion = info.Field<string>("MotivoCancelacion"),
                             EstatusId = info.Field<int>("EstatusID"),
                             ProblemaId = info.Field<int?>("ProblemaID") != null ? info.Field<int>("ProblemaID") :0,
                             Comentarios = info.Field<string>("Comentarios")
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
        /// Obtiene los datos mapeados de ganado muerto
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static MuerteInfo ObtenerMuertoPorArete(DataSet ds)
        {
            try
            {

                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                MuerteInfo result =
                      (from info in dt.AsEnumerable()
                       select
                           new MuerteInfo
                           {
                               OrganizacionId = info.Field<int>("OrganizacionId"),
                               MuerteId = info.Field<int>("MuerteId"),
                               Arete = info.Field<string>("Arete").Trim(),
                               AreteMetalico = info["AreteMetalico"] == DBNull.Value ? String.Empty : info.Field<string>("AreteMetalico"),
                               Observaciones = info.Field<string>("Observaciones").Trim(),
                               LoteId = info.Field<int>("LoteId"),
                               OperadorDeteccionId = info["OperadorDeteccion"] == DBNull.Value ? 0 : info.Field<int>("OperadorDeteccion"),
                               FechaDeteccion = info["FechaDeteccion"] == DBNull.Value ? new DateTime(1900, 1, 1) : info.Field<DateTime>("FechaDeteccion"),
                               FotoDeteccion = info["FotoDeteccion"] == DBNull.Value ? String.Empty : info.Field<string>("FotoDeteccion").Trim(),
                               OperadorRecoleccionId = info["OperadorRecoleccion"] == DBNull.Value ? 0 : info.Field<int>("OperadorRecoleccion"),
                               FechaRecoleccion = info["FechaRecoleccion"] == DBNull.Value ? new DateTime(1900, 1, 1) : info.Field<DateTime>("FechaRecoleccion"),
                               OperadorNecropsiaId = info["OperadorNecropsia"] == DBNull.Value ? 0 : info.Field<int>("OperadorNecropsia"),
                               FechaNecropsia = info["FechaNecropsia"] == DBNull.Value ? new DateTime(1900, 1, 1) : info.Field<DateTime>("FechaNecropsia"),
                               EstatusId = info.Field<int>("EstatusID"),
                               ProblemaId = info["ProblemaID"] == DBNull.Value ? 0 : info.Field<int>("ProblemaID"),
                               FechaCreacion = info["FechaNecropsia"] == DBNull.Value ? new DateTime(1900, 1, 1) : info.Field<DateTime>("FechaCreacion"),
                               LoteCodigo = info.Field<string>("Lote").Trim(),
                               CorralCodigo = info.Field<string>("Codigo").Trim(),
                               CorralId = info.Field<int>("CorralId"),
                               Activo = info.Field<bool>("Activo").BoolAEnum(),
                           }).First();

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
