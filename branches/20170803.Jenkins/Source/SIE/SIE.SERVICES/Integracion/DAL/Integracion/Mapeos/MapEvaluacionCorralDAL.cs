using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal  class MapEvaluacionCorralDAL
    {
        internal static ResultadoInfo<EvaluacionCorralInfo> ObtenerPorPagina(DataSet ds)
        {
            ResultadoInfo<EvaluacionCorralInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<EvaluacionCorralInfo> lista = (from info in dt.AsEnumerable()
                                        select new EvaluacionCorralInfo
                                        {
                                            EvaluacionID = info.Field<int>("EvaluacionID"),
                                            FolioEvaluacion = info.Field<int>("FolioEvaluacion"),
                                            Organizacion = new OrganizacionInfo()
                                                {
                                                    OrganizacionID = info.Field<int>("OrganizacionID")
                                                },
                                            OrganizacionOrigen = new OrganizacionInfo()
                                                {
                                                    OrganizacionID = info.Field<int>("OrganizacionOrigenID"),
                                                    Descripcion = info.Field<string>("DescripcionOrigen")
                                                },
                                            Corral = new CorralInfo()
                                            {
                                                CorralID = info.Field<int>("CorralID"),
                                                Codigo = info.Field<string>("Codigo").Trim()
                                            },
                                            Lote = new LoteInfo()
                                                {
                                                    LoteID = info.Field<int>("LoteID"),
                                                    Lote = info.Field<string>("Lote").Trim()
                                                },
                                            PesoLlegada = info.Field<decimal>("PesoBruto") - info.Field<decimal>("PesoTara"),
                                            FolioOrigen = info.Field<int>("FolioOrigen"),
                                            FechaRecepcion = info.Field<DateTime>("FechaRecepcion"),
                                            FechaEvaluacion = info.Field<DateTime>("FechaEvaluacion"),
                                            HoraEvaluacion = info.Field<DateTime>("FechaEvaluacion").ToShortTimeString(),
                                            Cabezas = info.Field<int>("Cabezas"),
                                            EsMetafilaxia = info.Field<bool>("EsMetafilaxia"),
                                            Operador = new OperadorInfo()
                                                {
                                                    OperadorID = info.Field<int>("OperadorID")
                                                },
                                            NivelGarrapata = (NivelGarrapata)Enum.Parse(typeof (NivelGarrapata),info.Field<int>("NivelGarrapata").ToString()),
                                            MetafilaxiaAutorizada = info.Field<bool>("MetafilaxiaAutorizada"),
                                            Justificacion = info.Field<string>("Justificacion"),
                                            Activo = info.Field<bool>("Activo").BoolAEnum(),
                                            FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                                            UsuarioCreacionID = info.Field<int>("UsuarioCreacionID"),
                                            OrganizacionOrigenAgrupadas = info.Field<string>("OrganizacionOrigen"),
                                            PartidasAgrupadas = info.Field<string>("Partidas")
                                        }).ToList();

                resultado = new ResultadoInfo<EvaluacionCorralInfo>
                                {
                                    Lista = lista,
                                    TotalRegistros =
                                        Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
                                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        
        }
    }
}
