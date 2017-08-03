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
    public class MapProgramaciondeFletesDAL
    {
        /// <summary>
        /// Obtener Programacion de fletes
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static List<ContratoInfo> ObtenerProgramacionFletes(DataSet ds)
        {
            List<ContratoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select new ContratoInfo
                             {
                                 ContratoId = info.Field<int>("ContratoID"),
                                 Folio = info.Field<int>("Folio"),
                                 Organizacion = new OrganizacionInfo()
                                 { 
                                     Descripcion = info.Field<string>("Descripcion"),
                                     OrganizacionID = info.Field<int>("OrganizacionID")
                                 },
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
        /// <summary>
        /// Programacion de fletes detalle
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static List<ProgramaciondeFletesInfo> ObtenerProgramacionFletesDetalle(DataSet ds)
        {
            List<ProgramaciondeFletesInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select new ProgramaciondeFletesInfo
                             {
                                 Flete = new FleteInfo()
                                 {
                                     FleteID = info["FleteID"] == DBNull.Value ? 0 : info.Field<int>("FleteID"),
                                     Proveedor = new ProveedorInfo()
                                     {
                                         ProveedorID = info["ProveedorID"] == DBNull.Value ? 0 : info.Field<int>("ProveedorID"),
                                         Descripcion = info["DescripcionProveedor"] == DBNull.Value ? String.Empty : info.Field<string>("DescripcionProveedor"),
                                         CodigoSAP = info["CodigoSapProveedor"] == DBNull.Value ? String.Empty : info.Field<string>("CodigoSapProveedor"),
                                     },
                                     Observaciones = info["Observaciones"] == DBNull.Value ? String.Empty : info.Field<string>("Observaciones"),
                                     TipoTarifa = new TipoTarifaInfo() { TipoTarifaId = info["TipoTarifaID"] == DBNull.Value ? 0 : info.Field<int>("TipoTarifaID"), Descripcion = info["Descripcion"] == DBNull.Value ? String.Empty : info.Field<string>("Descripcion") },
                                     MermaPermitida = info["MermaPermitida"] == DBNull.Value ? 0 : info.Field<decimal>("MermaPermitida"),
                                     Activo = info["Activo"] == DBNull.Value ? 0 : info.Field<bool>("Activo").BoolAEnum(),
                                     ContratoID = info["ContratoID"] == DBNull.Value ? 0 :  info["ContratoID"] == DBNull.Value ? 0 : info.Field<int>("ContratoID"),
                                     Opcion = 0
                                 },
                                 }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
        /// <summary>
        /// Obtiene la programacion de fletes por pagina
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<ContratoInfo> ObtenerProgramacionFletesPorPagina(DataSet ds)
        {
            ResultadoInfo<ContratoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<ContratoInfo> listaContratos = (from info in dt.AsEnumerable()
                    select new ContratoInfo
                    {
                        ContratoId = info.Field<int>("ContratoID"),
                        Folio = info.Field<int>("Folio"),
                        Organizacion = new OrganizacionInfo
                        {
                            Descripcion = info.Field<string>("Descripcion"),
                            OrganizacionID = info.Field<int>("OrganizacionID")
                        },
                        Activo = info.Field<bool>("Activo").BoolAEnum(),
                    }).ToList();

                resultado = new ResultadoInfo<ContratoInfo>
                {
                    Lista = listaContratos,
                    TotalRegistros =
                        Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
                };

                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

        }
    }
}
